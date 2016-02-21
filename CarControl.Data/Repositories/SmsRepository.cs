using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarConnect.Data.Infrastructure;
using CarConnect.Model;

namespace CarConnect.Data.Repositories
{
    public interface ISmsRepository : IRepository<Sms> { }

    public class SmsRepository : RepositoryBase<Sms>, ISmsRepository
    {
        public SmsRepository(IDbFactory dbFactory) : base(dbFactory) { }
    }
}
