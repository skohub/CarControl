using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarConnect.Data.Infrastructure;
using CarConnect.Model;

namespace CarConnect.Data.Repositories
{
    public interface IGpsLocationRepository : IRepository<GpsLocation> { }

    public class GpsLocationRepository : RepositoryBase<GpsLocation>, IGpsLocationRepository
    {
        public GpsLocationRepository(IDbFactory dbFactory) : base(dbFactory) { }
    }

}
