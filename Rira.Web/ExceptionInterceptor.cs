using Grpc.Core;
using Grpc.Core.Interceptors;
using System.Text.Json;

namespace Rira.Web;

public class ExceptionInterceptor : Interceptor
{
    private readonly ILogger<ExceptionInterceptor> _logger;

    public ExceptionInterceptor(ILogger<ExceptionInterceptor> logger)
    {
        _logger = logger;
    }

    public override async Task<TResponse> UnaryServerHandler<TRequest, TResponse>(TRequest request, ServerCallContext context, UnaryServerMethod<TRequest, TResponse> continuation)
    {
        try
        {
            return await continuation(request, context);
        }
        catch (RpcException ex)
        {
            throw;
        }
        catch (Exception ex)
        {
            var errorResponse = new
            {
                error = ex.Message,
                statusCode = StatusCode.Internal,
                timestamp = DateTime.UtcNow,
                traceId = context.GetHttpContext().TraceIdentifier
            };

            _logger.LogError(ex, "Unhandled Exception: {@Error}", errorResponse);

            throw new RpcException(new Status(
                StatusCode.Internal,
                JsonSerializer.Serialize(errorResponse)
            ));
        }
    }
}
