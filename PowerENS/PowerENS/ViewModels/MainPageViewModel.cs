using LiveCharts.Configurations;
using LiveCharts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PowerENS.Chart;
using System.Threading;
using System.Data.SqlClient;
using System.Data;

namespace PowerENS.ViewModels
{
    class MainPageViewModel : INotifyPropertyChanged
    {
        string connString = "Data Source=(local);Initial Catalog=ION_Data;Integrated Security=SSPI;";

        #region Chart Axis Properties (Max, Min, Step, Unit, Value)
        private double _axisMax;
        private double _axisMin;
        private double _current;
        private double _power;
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

        public double Current
        {
            get { return _current; }
            set
            {
                _current = value;
                RaisePropertyChanged("Current");
            }
        }

        public double Power
        {
            get { return _power; }
            set
            {
                _power = value;
                RaisePropertyChanged("Power");
            }
        }
        #endregion Chart Axis (Max, Min, Step, Unit, Value

        

        public bool IsReading { get; set; }


        Models.MainPageModel mainPageModel;
        public ChartValues<SampleChart> CurrentChartValues { get; set; }
        public ChartValues<SampleChart> PowerChartValues { get; set; }
        public Func<double, string> DateTimeFormatter { get; set; }

        public MainPageViewModel()
        {
            mainPageModel = new Models.MainPageModel();
            
            var mapper = Mappers.Xy<SampleChart>()
                .X(model => model.DateTime.Ticks)
                .Y(model => model.Value);

            Charting.For<SampleChart>(mapper);

            CurrentChartValues = new ChartValues<SampleChart>();
            PowerChartValues = new ChartValues<SampleChart>();
            DateTimeFormatter = value => new DateTime((long)value).ToString("HH:mm");

            AxisStep = TimeSpan.FromMinutes(15).Ticks;
            AxisUnit = TimeSpan.TicksPerMinute;

            
            SetAxisLimits(DateTime.Now);
            IsReading = true;

            
            if (IsReading)
                Task.Factory.StartNew(RenderCurrentChart);

            if (IsReading)
                Task.Factory.StartNew(RenderPowerChart);
        }


        private void SetAxisLimits(DateTime now)
        {
            AxisMax = now.Ticks + TimeSpan.FromMinutes(0).Ticks;
            AxisMin = now.Ticks - TimeSpan.FromHours(4).Ticks;
        }

        #region Chart rendering (simulation)
        // Chart Rendering When the page loaded.
        private void RenderCurrentChart()
        {
            SqlConnection conn = null;
            try
            {
                while (IsReading)
                {
                    conn = new SqlConnection(connString);
                    conn.Open();

                    SqlCommand Cmd = new SqlCommand("SELECT TOP (15) [TimestampUTC],[Value] FROM[ION_Data].[dbo].[vCurrent] ORDER BY TimestampUTC DESC", conn);
                    SqlDataReader Sdr = Cmd.ExecuteReader();
                    
                    while (Sdr.Read())
                    {
                        CurrentChartValues.Add(new SampleChart
                        {
                            DateTime = Sdr.GetDateTime(0).AddHours(9),
                            Value = Sdr.GetDouble(1),
                        });
                    }

                    Current = Convert.ToDouble((CurrentChartValues[0].Value.ToString("N2")));

                    var now = DateTime.Now;
                    SetAxisLimits(now);

                    if (conn != null)
                    {
                        conn.Close();
                    }

                    Thread.Sleep(60000);
                    CurrentChartValues.Clear();                    
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                if(conn!=null)
                {
                    conn.Close();
                }
            }
        }

        private void RenderPowerChart()
        {
            SqlConnection conn = null;
            try
            {
                while (IsReading)
                {
                    conn = new SqlConnection(connString);
                    conn.Open();

                    SqlCommand Cmd = new SqlCommand("SELECT TOP (15) [TimestampUTC],[Value] FROM[ION_Data].[dbo].[vPower] ORDER BY TimestampUTC DESC", conn);
                    SqlDataReader Sdr2 = Cmd.ExecuteReader();

                    while (Sdr2.Read())
                    {
                        PowerChartValues.Add(new SampleChart
                        {
                            DateTime = Sdr2.GetDateTime(0).AddHours(9),
                            Value = Sdr2.GetDouble(1),
                        });
                    }

                    Power = Convert.ToDouble((PowerChartValues[0].Value.ToString("N2")));

                    var now = DateTime.Now;
                    SetAxisLimits(now);

                    if (conn != null)
                    {
                        conn.Close();
                    }

                    Thread.Sleep(60000);
                    PowerChartValues.Clear();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }

        #endregion Chart Rendering


        #region PropertyChanged Event Handler
        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged(string propertyName = null)
        {
            if(PropertyChanged != null)
            {
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion PropertyChanged Event Handler
    }
}
