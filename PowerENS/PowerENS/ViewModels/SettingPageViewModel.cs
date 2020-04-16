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

        public event PropertyChangedEventHandler PropertyChanged;

        public SettingPageViewModel()
        {
            SendMessageCommand = new RelayCommand(SendMessage, CanSendMessage);
            _authToken = "ILtYnk20FBrnmxItVEbdpg==";
            _serverName = "vidaconcerveza";
            _service = 2010042059;
            _mobile = "01030171592";
            _message = "Alarm01_Temp too High in the site 32 at 2";
            _template = 10001;
        }

        private string _authToken { get; set; }
        private string _serverName { get; set; }
        private int _service { get; set; }
        private string _mobile { get; set; }
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
    }
}
