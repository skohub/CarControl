using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarConnect.Data.Configuration;
using CarConnect.Model;

namespace CarConnect.Data
{
    public class StoreEntities: DbContext
    {
        public StoreEntities() : base("name=StoreEntities") { }

        public DbSet<Car> Cars { get; set; }
        public DbSet<FloatSensorValue> Sensors { get; set; }
        public DbSet<GpsLocation> GpsLocations { get; set; }
        public DbSet<GSensor> GSensors { get; set; }
        public DbSet<Sms> Smses { get; set; }

        public virtual void Commit()
        {
            base.SaveChanges();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new CarConfiguration());
            modelBuilder.Configurations.Add(new FloatSensorConfiguration());
            modelBuilder.Configurations.Add(new GpsLocationConfiguration());
            modelBuilder.Configurations.Add(new GSensorConfiguration());
            modelBuilder.Configurations.Add(new SmsConfiguration());
        }
    }
}
