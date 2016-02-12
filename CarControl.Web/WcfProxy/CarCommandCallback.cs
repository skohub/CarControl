using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CarControl.Contract;

namespace CarControl.Web.WcfProxy
{
    public class CarCommandCallback : ICarCommandCallback
    {
        public void Notify(string property, string value)
        {
            throw new NotImplementedException();
        }
    }
}