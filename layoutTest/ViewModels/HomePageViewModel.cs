using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using layoutTest.Data;

namespace layoutTest.ViewModels
{
    public class HomePageViewModel : ViewModelBase
    {
        private string? _title;
        private string? _subtitle;
        private string? _idNumber;
        private string? _nationality;
        private string? _errorMessage;
        private bool? _isViewVisible = true;
        bool _isNotLoading;
        bool _isLoading;

        private VoterRepository _voterRepository;

        public string? Title
        {
            get => _title;
            set
            {
                _title = value;
                OnPropertyChanged();
            }
        }
        public string? SubTitle
        {
            get => _subtitle;
            set
            {
                _subtitle = value;
                OnPropertyChanged();
            }
        }
        public string? IdNumber
        {
            get { return _idNumber; }
            set
            {
                _idNumber = value;
                OnPropertyChanged(nameof(_idNumber));
            }
        }
        public string? Nationality
        {
            get { return _nationality; }
            set
            {
                _nationality = value;
                OnPropertyChanged(nameof(_nationality));
            }
        }
        public string? ErrorMessage
        {
            get { return _errorMessage; }
            set
            {
                _errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
            }
        }
        public bool IsLoading
        {
            get { return _isLoading; }
            set
            {
                _isLoading = value;
                OnPropertyChanged(nameof(IsLoading));
                OnPropertyChanged(nameof(IsNotLoading));
            }
        }

        public bool IsNotLoading
        {
            get { return !_isLoading; }
        }
        public bool? IsViewVisible
        {
            get { return _isViewVisible; }
            set
            {
                _isViewVisible = value;
                OnPropertyChanged(nameof(IsViewVisible));
            }
        }

        public ICommand HomeCommand { get; }

        public HomePageViewModel()
        {

            _voterRepository = new VoterRepository();
            Title = "Registro de electores";
            SubTitle = "Por favor ingrese la nacionalidad y la cédula de identidad del ciudadano o ciudadana";

            //Resources.Add("BooleanToLoadingTextConverter", new BooleanToLoadingTextConverter());

            HomeCommand = new ViewModelCommand(ExecuteHomeCommand, CanExecuteHomeCommand);

        }

        private bool CanExecuteHomeCommand(object obj)
        {
            bool validData;
            if (string.IsNullOrWhiteSpace(IdNumber) || IdNumber.Length < 3 || string.IsNullOrWhiteSpace(Nationality)) {
                validData = false;
            } else
            {
                validData = true;
            }
            return validData;
        }

        private async void ExecuteHomeCommand(object obj)
        {
            IsLoading = true;

            try
            {
                var isValidVoter = await _voterRepository.FindVoterByIdNumber(Nationality, IdNumber);

                if (isValidVoter.Count() > 0)
                {
                    ErrorMessage = "Exito!!!";
                }
                else
                {
                    ErrorMessage = "No existe!!";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                // Restaura el estado de tu ViewModel para indicar que se ha completado la carga
                IsLoading = false;
            }


            //if (sender is ListViewItem item && item.IsSelected)
            //{
            //    Page newPage = null;

            //    switch (item.Name)
            //    {
            //        case "ItemUnidades":
            //            newPage = new VUnidades();
            //            break;
            //        case "ItemConductores":
            //            newPage = new VConductores();
            //            break;
            //        case "ItemRemediacion":
            //            newPage = new VRemediacion();
            //            break;
            //    }

            //    if (newPage != null)
            //    {
            //        // Navega a la nueva página
            //        this.NavigationService.Navigate(newPage);
            //    }
            //}
        }
        public class BooleanToLoadingTextConverter : IValueConverter
        {
            public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            {
                if (value is bool isLoading)
                {
                    return isLoading ? "Cargando..." : "Buscar";
                }

                return "Buscar";
            }

            public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            {
                throw new NotImplementedException();
            }
        }


    }
}
