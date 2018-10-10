using System.Linq;
using GarageSeller.Models;

namespace GarageSeller.Context
{
    public static class DbInitializer
    {
        public static void Initialize(GarageSellerContext ctx)
        {
            ctx.Database.EnsureCreated();

            if (!ctx.TransmissionTypes.Any())
            {
                ctx.TransmissionTypes.Add(new TransmissionType { Name = "Manual" });
                ctx.TransmissionTypes.Add(new TransmissionType { Name = "Automatic" });
            }

            if (!ctx.MotorTypes.Any())
            {
                ctx.MotorTypes.Add(new MotorType { Name = "Diesel" });
                ctx.MotorTypes.Add(new MotorType { Name = "Petrol" });
                ctx.MotorTypes.Add(new MotorType { Name = "Hybrid" });
                ctx.MotorTypes.Add(new MotorType { Name = "Electric" });
            }

            if (!ctx.Garages.Any())
            {
                ctx.Garages.Add(new Garage { GarageName = "HQ", Address = "Gran Road, Atlantis City, Mars", Email = "hq@garageseller.com", Phone = "04050107054" });
            }

            ctx.SaveChanges();
        }
    }
}
