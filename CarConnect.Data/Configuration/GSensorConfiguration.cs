using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarConnect.Model;

namespace CarConnect.Data.Configuration
{
    public class GSensorConfiguration : EntityTypeConfiguration<GSensor>
    {
        public GSensorConfiguration()
        {
            ToTable("g_sensor");
            Property(g => g.Id).IsRequired();
            Property(g => g.CarId).IsRequired();
            Property(g => g.X).IsRequired();
            Property(g => g.Y).IsRequired();
            Property(g => g.Z).IsRequired();
            Property(g => g.Time).IsRequired();
        }
    }
}
