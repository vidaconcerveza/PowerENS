using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PowerENS.Views;

namespace PowerENS.ViewModels
{
    class MainWindowViewModel:BindableBase
    {
        private readonly IRegionManager _regionManager;

        public MainWindowViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
            _regionManager.RegisterViewWithRegion(PowerENS.Infrastructure.Region.ContentRegion, typeof(MainPageView));
            _regionManager.RegisterViewWithRegion(PowerENS.Infrastructure.Region.UpperMenuRegion, typeof(UpperMenuView));
        }
    }
}
