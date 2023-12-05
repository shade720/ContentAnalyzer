using System.Security.Authentication;
using ContentAnalyzer.Gateway.EFCoreIdentity;
using Grpc.Core;
using Grpc.Core.Interceptors;

namespace ContentAnalyzer.Gateway.Interceptors;

public class AuthenticationInterceptor : Interceptor
{
    private readonly TokenManager _tokenManager;
    private readonly ILogger _logger;

    public AuthenticationInterceptor(
        TokenManager tokenManager, 
        ILogger logger)
    {
        _tokenManager = tokenManager;
        _logger = logger;
    }

    public override async Task<TResponse> UnaryServerHandler<TRequest, TResponse>(TRequest request, ServerCallContext context,
        UnaryServerMethod<TRequest, TResponse> continuation)
    {
        var incomingUsername = context.RequestHeaders.GetValue("Username");
        var incomingPassword = context.RequestHeaders.GetValue("Password");
        if (string.IsNullOrEmpty(incomingUsername) || string.IsNullOrEmpty(incomingPassword))
            throw new AuthenticationException("No username or password provided");

        var extractedToken = new Token
        {
            UserName = incomingUsername, 
            TokenData = incomingPassword
        };

        if (await _tokenManager.CheckToken(extractedToken))
        {
            _logger.LogInformation("Request authenticated successfully. Username/Method: {Username} / {Method}",
                incomingUsername, context.Method);
            return await continuation(request, context);
        }

        _logger.LogError("Request not authenticated. Host/Username/Method: {Host} / {Username} / {Method}",
            context.Peer, incomingUsername, context.Method);
        throw new AuthenticationException("Username or password is not correct");
    }
}