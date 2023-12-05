using Microsoft.AspNetCore.Identity;

namespace ContentAnalyzer.Gateway.EFCoreIdentity;

public class Token : IdentityUser
{
    public override string? UserName { get; set; }
    public string TokenData { get; set; }
}