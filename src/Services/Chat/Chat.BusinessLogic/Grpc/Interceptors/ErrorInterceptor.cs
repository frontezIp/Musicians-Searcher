using Grpc.Core;
using Grpc.Core.Interceptors;
using Microsoft.Extensions.Logging;

namespace Chat.BusinessLogic.Grpc.Interceptors
{
    public class ErrorInterceptor : Interceptor
    {
        private readonly ILogger<ErrorInterceptor> _logger;

        public ErrorInterceptor(ILogger<ErrorInterceptor> logger)
        {
            _logger = logger;
        }

        public override AsyncUnaryCall<TResponse> AsyncUnaryCall<TRequest, TResponse>(TRequest request, ClientInterceptorContext<TRequest, TResponse> context, AsyncUnaryCallContinuation<TRequest, TResponse> continuation)
        {
            LogCall(context.Method);

            var call = continuation(request, context);

            return new AsyncUnaryCall<TResponse>(HandleResponse(call.ResponseAsync), call.ResponseHeadersAsync, call.GetStatus, call.GetTrailers, call.Dispose);
        }

        private async Task<TResponse> HandleResponse<TResponse>(Task<TResponse> task)
        {
            try
            {
                var response = await task;
                _logger.LogInformation("Response was successfully received");
                return response;
            }
            catch (RpcException ex)
            {
                _logger.LogError("Grpc related error occured. Status - {StatusCode}, message - {Message}", ex.StatusCode, ex.Message);
                throw new Exception("Grpc related error occured");

            }
        }

        private void LogCall<TRequest, TResponse>(Method<TRequest, TResponse> method) where TRequest : class where TResponse : class
        {
            _logger.LogInformation("Starting call. Type: {MethodType}. Request: {Request)}. Response: {Response)}", method.Type, typeof(TRequest), typeof(TResponse));
        }
    }
}
