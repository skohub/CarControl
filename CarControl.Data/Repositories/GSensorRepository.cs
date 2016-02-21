using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarConnect.Data.Infrastructure;
using CarConnect.Model;

namespace CarConnect.Data.Repositories
{
    public interface IGSensorRepository : IRepository<GSensor> { }

    public class GSensorRepository : RepositoryBase<GSensor>, IGSensorRepository
    {
        public GSensorRepository(IDbFactory dbFactory) : base(dbFactory) { }
    }
}
