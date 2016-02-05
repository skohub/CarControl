using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarConnect.Model
{
    public class Car
    {
        public int CarId { get; set; }
        public string Name { get; set; }
        public string Imei { get; set; }
        public string Hash { get; set; }
    }
}
