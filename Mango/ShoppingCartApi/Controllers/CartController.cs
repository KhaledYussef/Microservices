using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using ShoppingCartApi.Data;
using ShoppingCartApi.Models;
using ShoppingCartApi.Models.Dto;

namespace ShoppingCartApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly AppDbContext _db;

        public CartController(AppDbContext db)
        {
            _db = db;
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
    }
}
