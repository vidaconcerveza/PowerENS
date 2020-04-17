using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using PowerENS.KakaoAPI;
using System.Windows.Media.Animation;
using PowerENS.Command;
using System.ComponentModel;

namespace PowerENS.ViewModels
{
    class SettingPageViewModel:INotifyPropertyChanged
    {
        public RelayCommand SendMessageCommand { get; set; }
        PowerENS.KakaoAPI.KakaoAPI api = new KakaoAPI.KakaoAPI();

        

        public SettingPageViewModel()
        {
            SendMessageCommand = new RelayCommand(SendMessage, CanSendMessage);
            _authToken = "dffjwyObkWj9rsLnS5H71Q==";
            _serverName = "PowerENS";
            _service = 2010042724;
            _mobile = "";
            _message = "UNICT님의 전력계통 상태 정보를 알려 드립니다.금일 발생기준 시간 9시입니다";
            _template = 10001;
        }

        private string _authToken { get; set; }
        private string _serverName { get; set; }
        private int _service { get; set; }
        private string _mobile;
        public string Mobile
        {
            get
            {
                return _mobile;
            }
            set
            {
                _mobile = value;
                RaisePropertyChanged("Mobile");
            }
        }
        private string _message { get; set; }
        private int _template { get; set; }


        private void SendMessage(object param)
        {
            Console.WriteLine(api.SendMessage(_authToken, _serverName, _service, _mobile, _message, _template));
        }

        private bool CanSendMessage(object param)
        {
            return true;
        }


        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged(string propertyName = null)
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
