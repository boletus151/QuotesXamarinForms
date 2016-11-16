using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuotesXamarinForms.Views;
using Xamarin.Forms;

namespace QuotesXamarinForms
{
    public partial class App : Application
    {
        public App()
        {
            var navPage = new NavigationPage(new MainPage());
            this.MainPage = navPage;
        }
    }
}
