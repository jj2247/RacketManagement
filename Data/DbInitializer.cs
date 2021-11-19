using RacketManagement.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace RacketManagement.Data
{
  public static class DbInitializer
  {
    public static void Initialize(RacketManagementContext context)
    {
      context.Database.EnsureCreated();
      if (context.Rackets.Any())
      {
        return;
      }

      if (context.Brands.Any())
      {
        return;
      }

      var rackets = new Racket[]
      {
        new Racket{name="Tecnifibre T-Fight 305 G1"},
        new Racket{name="Tecnifibre T-Fight 305 G2"},
        new Racket{name="Tecnifibre T-Fight 305 G3"},
        new Racket{name="Tecnifibre T-Fight 305 G4"},
        new Racket{name="Tecnifibre T-Fight 315 G1"},
        new Racket{name="Tecnifibre T-Fight 315 G2"},
      };
      foreach (Racket r in rackets)
      {
        context.Rackets.Add(r);
      }
      context.SaveChanges();

      var brands = new Brand[]
      {
        new Brand{name="Tecnifibre"},
        new Brand{name="Prince"},
        new Brand{name="Wilson"},
        new Brand{name="Babolat"},
      };
      context.Brands.AddRange(brands);

      var roles = new IdentityRole[]
      {
        new IdentityRole{Id="1", Name="Administrator"},
        new IdentityRole{Id="2", Name="Manager"},
        new IdentityRole{Id="3", Name="Staff"}
      };
      context.Roles.AddRange(roles);

      var user = new ApplicationUser
      {
        FirstName = "Bob",
        LastName = "Dilon",
        City = "Ljubljana",
        Email = "bob@example.com",
        NormalizedEmail = "XXXX@EXAMPLE.COM",
        UserName = "bob@example.com",
        NormalizedUserName = "bob@example.com",
        PhoneNumber = "+111111111111",
        EmailConfirmed = true,
        PhoneNumberConfirmed = true,
        SecurityStamp = Guid.NewGuid().ToString("D")
      };

      if (!context.Users.Any(u => u.UserName == user.UserName))
      {
        var password = new PasswordHasher<ApplicationUser>();
        var hashed = password.HashPassword(user,"Testni123!");
        user.PasswordHash = hashed;
        context.Users.Add(user);
      }

      var UserRoles = new IdentityUserRole<string>[]
      {
        new IdentityUserRole<string>{RoleId = roles[0].Id, UserId=user.Id},
        new IdentityUserRole<string>{RoleId = roles[1].Id, UserId=user.Id},
      };
      context.UserRoles.AddRange(UserRoles);
      
      context.SaveChanges();
    }
  }
}