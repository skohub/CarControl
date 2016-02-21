using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarConnect.Model;

namespace CarConnect.Data.Configuration
{
    public class SmsConfiguration : EntityTypeConfiguration<Sms>
    {
        public SmsConfiguration()
        {
            ToTable("sms");
            Property(g => g.Id).IsRequired();
            Property(g => g.CarId).IsRequired();
            Property(g => g.Text).IsRequired();
            Property(g => g.Direction).IsRequired();
            Property(g => g.Time).IsRequired();
        }
    }
}
