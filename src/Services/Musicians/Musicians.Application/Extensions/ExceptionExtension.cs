using Grpc.Core;
using Microsoft.Extensions.Logging;
using Musicians.Domain.Exceptions.BadRequestException;
using Musicians.Domain.Exceptions.NotFoundException;

namespace Musicians.Application.Extensions
{
    public static class ExceptionExtension
    {
        public static RpcException Handle<T>(this Exception exception, ILogger<T> logger) 
        {
            return exception switch
            {
                NotFoundException => HandleNotFoundException((NotFoundException)exception, logger),
                BadRequestException => HandleBadRequestException((BadRequestException)exception, logger),
                _ => HandleDefault(exception, logger)
            };
        }

        private static RpcException HandleNotFoundException<T>(NotFoundException notFoundException, ILogger<T> logger)
        {
            logger.LogError(notFoundException, "Not found exception occured");

            var status = new Status(StatusCode.NotFound, notFoundException.Message);

            return new RpcException(status);
        }

        private static RpcException HandleBadRequestException<T>(BadRequestException badRequestException, ILogger<T> logger)
        {
            logger.LogError(badRequestException, "Bad request exception has occured");

            var status = new Status(StatusCode.InvalidArgument, badRequestException.Message);

            return new RpcException(status);
        }

        private static RpcException HandleDefault<T>(Exception exception, ILogger<T> logger)
        {
            logger.LogError(exception, "An error occured");

            var status = new Status(StatusCode.Internal, exception.Message);

            return new RpcException(status);
        }
    }
}
