using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Bloggie.Web.Data;

public class AuthDbContext : IdentityDbContext
{
    public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        const string superAdminRoleId = "b3cc6801-e45d-4d4b-84b4-0b76747eddd5";
        const string adminRoleId = "e55bac6d-25f7-46b3-b3f9-f2e3c1882b11";
        const string userRoleId = "3fa85f64-5717-4562-b3fc-2c963f66afa6";

        // Seed roles
        var roles = new List<IdentityRole>
        {
            new()
            {
                Name = "SuperAdmin",
                NormalizedName = "SuperAdmin",
                Id = superAdminRoleId,
                ConcurrencyStamp = superAdminRoleId,
            },
            new()
            {
                Name = "Admin",
                NormalizedName = "Admin",
                Id = adminRoleId,
                ConcurrencyStamp = adminRoleId,
            },
            new()
            {
                Name = "User",
                NormalizedName = "User",
                Id = userRoleId,
                ConcurrencyStamp = userRoleId,
            }
        };
        builder.Entity<IdentityRole>().HasData(roles);
        const string superAdminUserid = "41cd8a1b-468f-4a41-b5ba-49f7a2047b77";
        var superAdminUser = new IdentityUser
        {
            Id = superAdminUserid,
            UserName = "superadmin@gmail.com",
            Email = "superadmin@gmail.com",
        };

        superAdminUser.PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(superAdminUser, "password");
        builder.Entity<IdentityUser>().HasData(superAdminUser);

        //add all roles to SA user
        var superAdminRoles = new List<IdentityUserRole<string>>
        {
            new()
            {
                RoleId = superAdminRoleId,
                UserId = superAdminUserid
            },
            new()
            {
                RoleId = adminRoleId,
                UserId = superAdminUserid
            },
            new()
            {
                RoleId = userRoleId,
                UserId = superAdminUserid
            }
        };
        builder.Entity<IdentityUserRole<string>>().HasData(superAdminRoles);
    }
}