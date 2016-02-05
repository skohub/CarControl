using CarConnect.Data.Infrastructure;
using CarConnect.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace CarConnect.Data.Repositories
{
    public interface IFloatSensorRepository : IRepository<FloatSensorValue> { }

    public class FloatSensorRepository : RepositoryBase<FloatSensorValue>, IFloatSensorRepository
    {
        public FloatSensorRepository(IDbFactory dbFactory) : base(dbFactory) { }
    }
}
