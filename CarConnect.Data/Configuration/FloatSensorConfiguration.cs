using CarConnect.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarConnect.Data.Configuration
{
    public class FloatSensorConfiguration : EntityTypeConfiguration<FloatSensorValue>
    {
        public FloatSensorConfiguration()
        {
            ToTable("float_sensor");
            Property(g => g.Id).IsRequired();
            Property(g => g.CarId).IsRequired();
            Property(g => g.SensorName).IsRequired();
            Property(g => g.Value).IsRequired();
            Property(g => g.Time).IsRequired();
        }
    }
}
