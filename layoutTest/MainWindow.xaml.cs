using layoutTest.ViewModels;
using layoutTest.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public ObservableCollection<Persona> Personas { get; set; }
        public MainWindow()
        {
           
            InitializeComponent();
            Personas = new ObservableCollection<Persona>
            {
                new Persona { Name = "Cantidad de registros", Value = "30" },
                new Persona { Name = "Reclamos al día", Value = "25" },
                new Persona { Name = "Registros sin transmitir", Value = "25" },
                new Persona { Name = "Fecha de última transmisión", Value = "dd / mm / yyyy" },
                new Persona { Name = "Datos de ejemplo", Value = "25" },
                new Persona { Name = "Datos de ejemplo", Value = "25" },
                // Más personas...
            };
            this.DataContext = this;
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

        private void ButtonOpenMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonOpenMenu.Visibility = Visibility.Collapsed;
            ButtonCloseMenu.Visibility = Visibility.Visible;
        }

        private void ButtonCloseMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonOpenMenu.Visibility = Visibility.Visible;
            ButtonCloseMenu.Visibility = Visibility.Collapsed;
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var listView = sender as ListView;
            if (listView != null && listView.SelectedItem != null)
            {
                var selectedItem = listView.ItemContainerGenerator.ContainerFromItem(listView.SelectedItem) as ListViewItem;

                if (selectedItem != null)
                {
                    switch (selectedItem.Name)
                    {
                        case "HomeMenu":
                            HomePage homePage = new HomePage();
                            homePage.DataContext = new HomePageViewModel();
                            ContentFrame.Navigate(homePage);
                            break;
                        case "AboutMenu":
                            AboutPage aboutPage = new AboutPage();
                            aboutPage.DataContext = new AboutPageViewModel();
                            ContentFrame.Navigate(aboutPage);
                            break;
                    }
                }
            }
        }
        private void ShutDownApp(object sender, SelectionChangedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        
    }
    public class Persona
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }

    
};
