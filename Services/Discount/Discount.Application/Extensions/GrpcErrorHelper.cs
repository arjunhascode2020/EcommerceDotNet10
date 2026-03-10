using Google.Protobuf;
using Google.Protobuf.WellKnownTypes;
using Google.Rpc;
using Grpc.Core;
using GoogleStatus = Google.Rpc.Status;
using GrpcStatus = Grpc.Core.Status;

namespace Discount.Application.Extensions
{
    public static class GrpcErrorHelper
    {
        public static RpcException CreateValidationException(Dictionary<string, string> fieldErrors)
        {
            var filedViolations = new List<BadRequest.Types.FieldViolation>();
            foreach (var eror in fieldErrors)
            {
                filedViolations.Add(new BadRequest.Types.FieldViolation
                {
                    Field = eror.Key,
                    Description = eror.Value
                });
            }
            //now add bad request
            var badRequest = new BadRequest();
            badRequest.FieldViolations.AddRange(filedViolations);
            //now create status
            var status = new GoogleStatus
            {
                Code = (int)StatusCode.InvalidArgument,
                Message = "Validation failed for the request.",
                Details = { Any.Pack(badRequest) }
            };

            var trailers = new Metadata
            {
                { "grpc-status-details-bin", status.ToByteArray() }
            };

            return new RpcException(new GrpcStatus(StatusCode.InvalidArgument, status.Message), trailers);
        }
    }
}
