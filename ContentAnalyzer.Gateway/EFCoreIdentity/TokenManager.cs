using Microsoft.AspNetCore.Identity;
using Serilog;

namespace ContentAnalyzer.Gateway.EFCoreIdentity;

public class TokenManager
{
    private readonly UserManager<Token> _userManager;
    private readonly SignInManager<Token> _signInManager;

    public TokenManager(UserManager<Token> userManager, SignInManager<Token> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public async Task<IdentityResult> SetToken(Token token)
    {
        var result = await _userManager.CreateAsync(token, token.TokenData);
        await _signInManager.SignInAsync(token, false);
        return result;
    }

    public async Task<bool> CheckToken(Token token)
    {
        try
        {
            var result = await _signInManager.PasswordSignInAsync(token.UserName, token.TokenData, false, false);
            return result.Succeeded;
        }
        catch (Exception e)
        {
            Log.Error("Message: {0}\r\nStack Trace:{1}", e.Message, e.StackTrace);
            return false;
        }
    }
}