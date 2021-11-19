using RacketManagement.Models;
using System;
using System.Linq;

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
      foreach(Brand b in brands) {
        context.Brands.Add(b);
      }
      context.SaveChanges();
    }
  }
}