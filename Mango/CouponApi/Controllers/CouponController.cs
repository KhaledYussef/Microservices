using AutoMapper;

using CouponApi.Data;
using CouponApi.Models;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CouponApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CouponController(AppDbContext db, IMapper mapper) : ControllerBase
    {
        private readonly AppDbContext _db = db;
        private readonly IMapper _mapper = mapper;

        [HttpGet]
        public async Task<ResponseResult> Get()
        {
            var coupons = await _db.Coupons.ToListAsync();
            var result = _mapper.Map<List<CouponDto>>(coupons);
            return ResponseResult.Success(result);

        }

        [HttpGet("{id}")]
        public async Task<ResponseResult> Get(int id)
        {
            var coupon = await _db.Coupons.FindAsync(id);
            if (coupon == null)
            {
                return ResponseResult.Error("Not Found");
            }

            var result = _mapper.Map<CouponDto>(coupon);

            return ResponseResult.Success(result);
        }

        // get by code
        [HttpGet("GetByCode/{code}")]
        public async Task<ResponseResult> GetByCode(string code)
        {
            var coupon = await _db.Coupons.FirstOrDefaultAsync(c => c.Code == code);
            if (coupon == null)
            {
                return ResponseResult.Error("Not Found");
            }

            var result = _mapper.Map<CouponDto>(coupon);

            return ResponseResult.Success(result);
        }

        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        public async Task<ResponseResult> Post([FromBody] CouponDto coupon)
        {
            if (!ModelState.IsValid)
            {
                return ResponseResult.Error("Invalid Model");
            }

            var newCoupon = _mapper.Map<Coupon>(coupon);

            _db.Coupons.Add(newCoupon);
            await _db.SaveChangesAsync();

            return ResponseResult.Success(newCoupon);
        }


        [HttpPut]
        public async Task<ResponseResult> Put([FromBody] CouponDto coupon)
        {
            if (!ModelState.IsValid)
            {
                return ResponseResult.Error("Invalid Model");
            }

            var couponInDb = await _db.Coupons.FindAsync(coupon.Id);
            if (couponInDb == null)
            {
                return ResponseResult.Error("Not Found");
            }

            _mapper.Map(coupon, couponInDb);

            await _db.SaveChangesAsync();

            return ResponseResult.Success(couponInDb);
        }


        [HttpDelete("{id}")]
        public async Task<ResponseResult> Delete(int id)
        {
            var coupon = await _db.Coupons.FindAsync(id);
            if (coupon == null)
            {
                return ResponseResult.Error("Not Found");
            }

            _db.Coupons.Remove(coupon);
            await _db.SaveChangesAsync();

            return ResponseResult.Success("Deleted");
        }

    }
}
