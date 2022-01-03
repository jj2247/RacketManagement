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

      var brands = new Brand[]
      {
        new Brand{name="Tecnifibre"},
        new Brand{name="Prince"},
        new Brand{name="Wilson"},
        new Brand{name="Babolat"},
      };
      context.Brands.AddRange(brands);

      var models = new Model[]
      {
        new Model{name="T-Fight 305"},
        new Model{name="T-Fight 315"},
        new Model{name="T-Fight 295"},
        new Model{name="T-Fight 285"},
        new Model{name="T-Fight 270"},
      };
      context.Models.AddRange(models);

      var gripsizes = new GripSize[]
      {
        new GripSize{size="L1"},
        new GripSize{size="L2"},
        new GripSize{size="L3"},
        new GripSize{size="L4"}
      };
      context.GripSizes.AddRange(gripsizes);

      var rackets = new Racket[]
      {
        new Racket{BrandID=1, GripSizeID=1, ModelID=3},
        new Racket{BrandID=3, GripSizeID=2, ModelID=1},
        new Racket{BrandID=2, GripSizeID=3, ModelID=2},
      };
      context.Rackets.AddRange(rackets);

      var roles = new IdentityRole[]
      {
        new IdentityRole{Id="1", Name="Administrator"},
        new IdentityRole{Id="2", Name="Customer"}
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

      var user2 = new ApplicationUser
      {
        FirstName = "John",
        LastName = "Doe",
        City = "Ljubljana",
        Email = "john@example.com",
        NormalizedEmail = "XXXX@EXAMPLE.COM",
        UserName = "john@example.com",
        NormalizedUserName = "john@example.com",
        PhoneNumber = "+222222222222",
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

      if (!context.Users.Any(u => u.UserName == user.UserName))
      {
        var password = new PasswordHasher<ApplicationUser>();
        var hashed = password.HashPassword(user2,"Testni123!");
        user2.PasswordHash = hashed;
        context.Users.Add(user2);
      }

      var loans = new Loan[]
      {
        new Loan{UserId=user.Id, RacketID=1},
        new Loan{UserId=user.Id, RacketID=2},
        new Loan{UserId=user.Id, RacketID=3},
      };
      context.Loans.AddRange(loans);

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