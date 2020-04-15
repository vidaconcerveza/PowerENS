using Prism.Commands;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PowerENS.ViewModels
{
    class UpperMenuViewModel
    {
        public ICommand NavigateToMainPageCommand { get; set; }
        public ICommand NavigateToSettingPageCommand { get; set; }

        private readonly IRegionManager _regionManager;
        public UpperMenuViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
            NavigateToMainPageCommand = new DelegateCommand(() => NavigateTo("MainPageView"));
            NavigateToSettingPageCommand = new DelegateCommand(() => NavigateTo("SettingPageView"));
        }

        private void NavigateTo(string url)
        {
            _regionManager.RequestNavigate(PowerENS.Infrastructure.Region.ContentRegion, url);
        }
    }
}
