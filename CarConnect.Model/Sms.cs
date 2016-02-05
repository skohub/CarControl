using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarConnect.Model
{
    public class Sms
    {
        public int Id { get; set; }
        public int CarId { get; set; }
        public string Text { get; set; }
        public string Direction { get; set; }
        public DateTime Time { get; set; }
    }
}
