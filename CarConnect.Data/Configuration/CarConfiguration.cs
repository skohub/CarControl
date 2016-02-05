using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarConnect.Model;

namespace CarConnect.Data.Configuration
{
    public class CarConfiguration: EntityTypeConfiguration<Car>
    {
        public CarConfiguration()
        {
            ToTable("car");
            Property(g => g.CarId).IsRequired();
            Property(g => g.Name).IsRequired();
        }
        

    }
}
