using Prism.Autofac;
using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using PowerENS.Views;

namespace PowerENS
{
    class Bootstrapper:AutofacBootstrapper
    {
        protected override DependencyObject CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }
        protected override void InitializeShell()
        {
            Application.Current.MainWindow.Show();
        }

        protected override void ConfigureContainerBuilder(ContainerBuilder builder)
        {
            base.ConfigureContainerBuilder(builder);
            builder.RegisterTypeForNavigation<UpperMenuView>("UpperMenuView");
            builder.RegisterTypeForNavigation<MainPageView>("UpperMenuView");
            builder.RegisterTypeForNavigation<SettingPageView>("SettingPageView");

        }
    }
}
