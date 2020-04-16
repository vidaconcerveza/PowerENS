using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerENS.Models
{
    class MainPageModel
    {
        private double _voltage;
        
        public double voltage
        {
            get { return _voltage; }
            set { _voltage = value; }
        }

        private double _power;
        public double power
        {
            get { return _power; }
            set { _power = value; }
        }
    }
}
