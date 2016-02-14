using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarConnect.Data.Infrastructure
{
    public class DbFactory: Disposable, IDbFactory
    {
        private StoreEntities _dbContext;

        protected override void DisposeCore()
        {
            _dbContext?.Dispose();
        }

        public StoreEntities Init()
        {
            return _dbContext ?? (_dbContext = new StoreEntities());
        }
    }
}
