using AutoMapper;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using ProductApi.Data;
using ProductApi.Dto;
using ProductApi.Models;

namespace ProductApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
 
    public class ProductsController(AppDbContext db, IMapper mapper) : ControllerBase
    {
        private readonly AppDbContext _db = db;
        private readonly IMapper _mapper = mapper;

        [HttpGet]
        public async Task<ResponseResult> Get()
        {
            var coupons = await _db.Products.ToListAsync();
            var result = _mapper.Map<List<ProductDto>>(coupons);
            return ResponseResult.Success(result);

        }

        [HttpGet("{id}")]
        public async Task<ResponseResult> Get(int id)
        {
            var coupon = await _db.Products.FindAsync(id);
            if (coupon == null)
            {
                return ResponseResult.Error("Not Found");
            }

            var result = _mapper.Map<ProductDto>(coupon);

            return ResponseResult.Success(result);
        }

        

        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        public async Task<ResponseResult> Post([FromBody] ProductDto coupon)
        {
            if (!ModelState.IsValid)
            {
                return ResponseResult.Error("Invalid Model");
            }

            var newCoupon = _mapper.Map<Product>(coupon);

            _db.Products.Add(newCoupon);
            await _db.SaveChangesAsync();

            return ResponseResult.Success(newCoupon);
        }


        [HttpPut]
        [Authorize(Roles = "ADMIN")]
        public async Task<ResponseResult> Put([FromBody] ProductDto coupon)
        {
            if (!ModelState.IsValid)
            {
                return ResponseResult.Error("Invalid Model");
            }

            var couponInDb = await _db.Products.FindAsync(coupon.Id);
            if (couponInDb == null)
            {
                return ResponseResult.Error("Not Found");
            }

            _mapper.Map(coupon, couponInDb);

            await _db.SaveChangesAsync();

            return ResponseResult.Success(couponInDb);
        }


        [HttpDelete("{id}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<ResponseResult> Delete(int id)
        {
            var coupon = await _db.Products.FindAsync(id);
            if (coupon == null)
            {
                return ResponseResult.Error("Not Found");
            }

            _db.Products.Remove(coupon);
            await _db.SaveChangesAsync();

            return ResponseResult.Success("Deleted");
        }

    }
}
