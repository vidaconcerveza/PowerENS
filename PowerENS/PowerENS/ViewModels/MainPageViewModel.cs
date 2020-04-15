using LiveCharts.Configurations;
using LiveCharts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PowerENS.Chart;
using System.ComponentModel;
using System.Threading;

namespace PowerENS.ViewModels
{
    class MainPageViewModel : INotifyPropertyChanged
    {

        private double _axisMax;
        private double _axisMin;
        private double _voltage;
        public double AxisStep { get; set; }
        public double AxisUnit { get; set; }
        public double AxisMax
        {
            get { return _axisMax; }
            set
            {
                _axisMax = value;
                RaisePropertyChanged("AxisMax");
            }
        }
        public double AxisMin
        {
            get { return _axisMin; }
            set
            {
                _axisMin = value;
                RaisePropertyChanged("AxisMin");
            }
        }

        public double Voltage
        {
            get { return _voltage; }
            set
            {
                _voltage = value;
                RaisePropertyChanged("Voltage");
            }
        }

        public bool IsReading { get; set; }


        Models.MainPage mainPageModel;
        public ChartValues<SampleChart> ChartValues { get; set; }
        public Func<double, string> DateTimeFormatter { get; set; }

        public MainPageViewModel()
        {
            mainPageModel = new Models.MainPage();
            
            var mapper = Mappers.Xy<SampleChart>()
                .X(model => model.DateTime.Ticks)
                .Y(model => model.Value);

            Charting.For<SampleChart>(mapper);

            ChartValues = new ChartValues<SampleChart>();
            DateTimeFormatter = value => new DateTime((long)value).ToString("mm:ss");

            AxisStep = TimeSpan.FromSeconds(1).Ticks;
            AxisUnit = TimeSpan.TicksPerSecond;

            
            SetAxisLimits(DateTime.Now);
            IsReading = true;

            if (IsReading)
                Task.Factory.StartNew(Read);
        }



        private void SetAxisLimits(DateTime now)
        {
            AxisMax = now.Ticks + TimeSpan.FromSeconds(1).Ticks;
            AxisMin = now.Ticks - TimeSpan.FromSeconds(20).Ticks;
        }

        private void Read()
        {
            var r = new Random();
            while(IsReading)
            {
                var now = DateTime.Now;
                _voltage = r.Next(20, 24);

                ChartValues.Add(new SampleChart
                {
                    DateTime = now,
                    Value = _voltage
                }) ;



                SetAxisLimits(now);
                if (ChartValues.Count > 150)
                    ChartValues.RemoveAt(0);

                Thread.Sleep(1000);
                Console.WriteLine("Updating.."+_voltage.ToString());
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged(string propertyName = null)
        {
            if(PropertyChanged != null)
            {
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
