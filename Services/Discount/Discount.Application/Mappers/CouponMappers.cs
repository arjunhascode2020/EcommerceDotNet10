using Discount.Application.Commands;
using Discount.Application.DTOs;
using Discount.Application.Responses;
using Discount.Core.Entities;
using Discount.Grpc.Protos;

namespace Discount.Application.Mappers
{
    public static class CouponMappers
    {
        public static CouponDto ToDto(this Coupon coupon)
        {
            return new CouponDto(coupon.Id, coupon.ProductName, coupon.Description, coupon.Amount);
        }

        public static Coupon ToEntity(this CreateDiscountCommand command)
        {
            return new Coupon
            {
                Id = 0, // Id will be set by the database
                ProductName = command.ProductName,
                Description = command.Discription,
                Amount = command.Amount
            };
        }
        public static Coupon ToEntity(this UpdateDiscountComand command)
        {
            return new Coupon
            {
                Id = command.Id,
                ProductName = command.ProductName,
                Description = command.Description,
                Amount = command.Amount
            };
        }

        public static CouponModel ToModel(this CouponDto couponDto)
        {
            return new CouponModel
            {
                Id = couponDto.Id,
                ProductName = couponDto.ProductName,
                Description = couponDto.Description,
                Amount = couponDto.Amount
            };
        }
        public static CreateDiscountCommand ToCreateCommand(this CouponModel couponModel)
        {
            return new CreateDiscountCommand(couponModel.ProductName, couponModel.Description, couponModel.Amount);
        }

        public static UpdateDiscountComand ToUpdateCommand(this CouponModel couponModel)
        {
            return new UpdateDiscountComand(couponModel.Id, couponModel.ProductName, couponModel.Description, couponModel.Amount);
        }
        public static ProtoResponse ToProtoResponse(this Response response)
        {
            return new ProtoResponse
            {
                Success = response.Success,
                Message = response.Message
            };
        }
    }
}
