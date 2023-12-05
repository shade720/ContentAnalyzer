using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ContentAnalyzer.Gateway.EFCoreIdentity;

public class IdentificationContext : IdentityDbContext<Token>
{
    public IdentificationContext(DbContextOptions<IdentificationContext> options) : base(options)
    {
        Database.EnsureCreated();
    }
}