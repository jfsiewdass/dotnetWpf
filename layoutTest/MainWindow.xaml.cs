using layoutTest.ViewModels;
using layoutTest.Views;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace layoutTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            HomePage homePage = new HomePage();
            homePage.DataContext = new HomePageViewModel();
            ContentFrame.Navigate(homePage);
            string logo = $"{Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.ToString()}\\images\\logo_cne.png";
            Logo.Source = new BitmapImage(new Uri(logo));
            this.SizeChanged += MainWindow_SizeChanged;

        }

        private void btnHome_Click(object sender, RoutedEventArgs e)
        {
            HomePage homePage = new HomePage();
            homePage.DataContext = new HomePageViewModel();
            ContentFrame.Navigate(homePage);
        }

        private void btnAbout_Click(object sender, RoutedEventArgs e)
        {
            AboutPage aboutPage = new AboutPage();
            aboutPage.DataContext = new AboutPageViewModel();
            ContentFrame.Navigate(aboutPage);
        }
        private void MenuButton_Click(object sender, RoutedEventArgs e)
        {
            ToggleButton button = (ToggleButton)sender;
            switch (button.Name)
            {
                case "HomeButton":
                    HomePage homePage = new HomePage();
                    homePage.DataContext = new HomePageViewModel();
                    ContentFrame.Navigate(homePage);
                    break;
                case "AboutButton":
                    AboutPage aboutPage = new AboutPage();
                    aboutPage.DataContext = new AboutPageViewModel();
                    ContentFrame.Navigate(aboutPage);
                    break;
            }
        }
        private void MainWindow_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (e.NewSize.Width < 640)
            {
                // Configuración para pantallas pequeñas
                Column1.Width = new GridLength(1, GridUnitType.Star);
                Column2.Width = new GridLength(0);
            }
            else
            {
                // Configuración para pantallas grandes
                Column1.Width = new GridLength(1, GridUnitType.Star);
                Column2.Width = new GridLength(400); // Establece un ancho fijo para el grid
            }
        }
    }
}
