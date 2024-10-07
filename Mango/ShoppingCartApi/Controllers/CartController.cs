using Azure;

using MessageBus;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

using ShoppingCartApi.Data;
using ShoppingCartApi.Models;
using ShoppingCartApi.Models.Dto;
using ShoppingCartApi.Services;

namespace ShoppingCartApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly AppDbContext _db;
        private readonly IProductService _productService;
        private readonly ICouponService _couponService;
        private readonly IMessageBus _messageBus;
        private readonly IConfiguration _configuration;

        public CartController(AppDbContext db,
            IProductService productService,
            ICouponService couponService, 
            IMessageBus messageBus, 
            IConfiguration configuration)
        {
            _db = db;
            _productService = productService;
            _couponService = couponService;
            _messageBus = messageBus;
            _configuration = configuration;
        }

        [HttpPost("Upsert")]
        public async Task<ResponseResult> Upsert([FromBody] CartDTO cart)
        {
            try
            {
                var existingCart = await _db.Carts
                    .FirstOrDefaultAsync(x => x.UserId == cart.UserId);

                if (existingCart == null)
                {
                    var newCart = new Cart
                    {
                        UserId = cart.UserId,
                        Items = cart.Items.Select(x => new CartDetail
                        {
                            ProductId = x.ProductId,
                            Count = x.Count,
                        }).ToList()
                    };

                    _db.Carts.Add(newCart);
                    await _db.SaveChangesAsync();

                    return ResponseResult.Success(cart);
                }
                else
                {
                    var existingItems = _db.CartDetails
                         .Where(x => x.CartId == existingCart.Id)
                         .ToList();

                    foreach (var item in cart.Items)
                    {
                        var existingItem = existingItems
                            .FirstOrDefault(x => x.ProductId == item.ProductId);

                        if (existingItem == null)
                        {
                            existingCart.Items.Add(new CartDetail
                            {
                                ProductId = item.ProductId,
                                Count = item.Count,
                            });
                        }
                        else
                        {
                            existingItem.Count += item.Count;
                        }
                    }

                    await _db.SaveChangesAsync();

                    return ResponseResult.Success(cart);

                }
            }
            catch (Exception ex)
            {
                return ResponseResult.Error(ex.Message);
            }
        }

        [HttpPost("RemoveCart")]
        public async Task<ResponseResult> RemoveCart([FromBody] int cartDetailsId)
        {

            var cartDetail = await _db.CartDetails.FindAsync(cartDetailsId);
            if (cartDetail == null)
            {
                return ResponseResult.Error("Not Found");
            }

            _db.CartDetails.Remove(cartDetail);
            await _db.SaveChangesAsync();

            // check if cart has no cartDetails
            var cart = await _db.Carts.FindAsync(cartDetail.CartId);


            return ResponseResult.Success(cartDetail);
        }

        [HttpGet("GetCart/{userId}")]
        public async Task<ResponseResult> GetCart(string userId)
        {
            var cart = await _db.Carts
                .Include(x => x.Items)
                .AsSplitQuery()
                .FirstOrDefaultAsync(x => x.UserId == userId);

            if (cart is null)
                return ResponseResult.Success(new CartDTO());
            

            var products = await _productService.GetProducts();


            var model = new CartDTO
            {
                Id = cart.Id,
                UserId = cart.UserId,
                CouponCode = cart.CouponCode,
                Discount = cart.Discount,
                Items = cart.Items.Select(x => new CartDetailDTO
                {
                    Id = x.Id,
                    CartId = x.CartId,
                    ProductId = x.ProductId,
                    Count = x.Count,
                    Product = products?.FirstOrDefault(p => p.Id == x.ProductId)
                }).ToList()
            };

            foreach (var item in model.Items)
            {
                model.TotalPrice += (item.Count * item.Product?.Price ?? 0);
            }


            //apply coupon if any
            if (!string.IsNullOrEmpty(model.CouponCode))
            {
                CouponDto coupon = await _couponService.GetCoupon(model.CouponCode);
                if (coupon != null && model.TotalPrice > coupon.MinAmount)
                {
                    model.TotalPrice -= coupon.Discount;
                    model.Discount = coupon.Discount;
                }
            }

            return ResponseResult.Success(model);
        }


        [HttpPost("ApplyCoupon")]
        public async Task<object> ApplyCoupon([FromBody] CartDTO cartDto)
        {
            try
            {
                var cartFromDb = await _db.Carts.FirstAsync(u => u.UserId == cartDto.UserId);
                cartFromDb.CouponCode = cartDto.CouponCode;
                _db.Carts.Update(cartFromDb);
                await _db.SaveChangesAsync();

                return ResponseResult.Success("");
            }
            catch (Exception ex)
            {
                return ResponseResult.Error(ex.Message);
            }
           
        }


        [HttpPost("EmailCartRequest")]
        public async Task<object> EmailCartRequest([FromBody] CartDTO cartDto)
        {
            try
            {
                await _messageBus.Publish(cartDto, _configuration.GetValue<string>("TopicAndQueueNames:EmailShoppingCart")!);
                return ResponseResult.Success("");
            }
            catch (Exception ex)
            {
                return ResponseResult.Error(ex.Message);
            }
             
        }
    }
}
