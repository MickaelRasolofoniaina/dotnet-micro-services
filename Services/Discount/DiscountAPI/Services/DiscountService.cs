using DiscountAPI.Data;
using DiscountAPI.Models;
using Grpc.Core;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace DiscountAPI.Services;

public class DiscountService(DiscountContext discountContext) : DiscountProtoService.DiscountProtoServiceBase
{
    public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
    {
        var coupon = await discountContext.Coupons.FirstOrDefaultAsync(x => x.ProductName == request.ProductName) ?? new Coupon
        {
            ProductName = "No Discount",
            Amount = 0,
            Description = "No Discount Desc"
        };

        var result = coupon.Adapt<CouponModel>();

        return result;
    }

    public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
    {
        var coupon = request.Adapt<Coupon>() ?? throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid coupon"));

        discountContext.Coupons.Add(coupon);
        await discountContext.SaveChangesAsync();

        var result = coupon.Adapt<CouponModel>();

        return result;
    }

    public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
    {
        if (request.Coupon == null)
            throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid coupon"));

        var coupon = request.Coupon.Adapt<Coupon>();

        discountContext.Coupons.Update(coupon);
        await discountContext.SaveChangesAsync();

        var result = coupon.Adapt<CouponModel>();

        return result;
    }

    public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
    {
        var coupon = await discountContext.Coupons.FirstOrDefaultAsync(x => x.Id == request.Id) ?? throw new RpcException(new Status(StatusCode.NotFound, $"Discount with Id={request.Id} is not found"));

        discountContext.Coupons.Remove(coupon);
        await discountContext.SaveChangesAsync();

        var result = new DeleteDiscountResponse
        {
            Success = true
        };

        return result;
    }
}
