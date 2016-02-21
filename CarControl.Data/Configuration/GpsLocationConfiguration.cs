using System.Data.Entity.ModelConfiguration;
using CarConnect.Model;

namespace CarConnect.Data.Configuration
{
    public class GpsLocationConfiguration : EntityTypeConfiguration<GpsLocation>
    {
        public GpsLocationConfiguration()
        {
            ToTable("gps_location");
            Property(g => g.Id).IsRequired();
            Property(g => g.CarId).IsRequired();
            Property(g => g.Latitude).IsRequired();
            Property(g => g.Longitude).IsRequired();
            Property(g => g.Time).IsRequired();
        }
    }
}
