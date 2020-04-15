using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerENS.Models
{
    class MainPage
    {
        private double _temperature;
        public double temperature
        {
            get { return _temperature; }
            set { _temperature = value; }
        }

        private double _humidity;
        public double humidity
        {
            get { return _humidity; }
            set { _humidity = value; }
        }
    }
}
