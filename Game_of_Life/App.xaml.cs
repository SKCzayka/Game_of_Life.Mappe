using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Game_of_Life
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static ServiceProvider serviceProvider { get; private set; }
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            ServiceCollection services = new ServiceCollection();

            services
                .AddSingleton<MainWindow>()
                .AddTransient<Farbauswahl>()
                .AddTransient<Spiel>();

            serviceProvider= services.BuildServiceProvider();
            serviceProvider.GetRequiredService<MainWindow>().Show();
       
        }
    }
}
