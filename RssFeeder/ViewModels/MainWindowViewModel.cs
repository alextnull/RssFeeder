using RssFeeder.Infrastructure;
using System.Collections.Generic;
using System.Windows.Input;

namespace RssFeeder.ViewModels
{
    /// <summary>
    /// Вью-модель главного окна.
    /// </summary>
    class MainWindowViewModel : ViewModelBase
    {
        private ViewModelBase _selectedViewModel;
        private readonly Dictionary<string, ViewModelBase> _viewModels;

        /// <summary>
        /// Отображаемая вью-модель.
        /// </summary>
        public ViewModelBase SelectedViewModel
        {
            get => _selectedViewModel;
            set => SetProperty(ref _selectedViewModel, value);
        }

        /// <summary>
        /// Команда для навигации.
        /// </summary>
        public ICommand NavigateCommand { get; set; }

        /// <summary>
        /// Переключает вью-модель.
        /// </summary>
        /// <param name="parameter">Вью-модель.</param>
        private void Navigate(object parameter)
        {
            var viewModelKey = parameter.ToString();

            if (viewModelKey is null)
            {
                return;
            }

            if (_viewModels.ContainsKey(viewModelKey))
            {
                SelectedViewModel = _viewModels[viewModelKey];
            }
        }

        /// <summary>
        /// Инициализирует вью-модель главного окна.
        /// </summary>
        public MainWindowViewModel()
        {
            _viewModels = new Dictionary<string, ViewModelBase>
            {
                ["Feed"] = new FeedViewModel { Name = "Лента" },
                ["Settings"] = new SettingsViewModel { Name = "Настройки" }
            };
            SelectedViewModel = _viewModels["Feed"];
            NavigateCommand = new RelayCommand(Navigate);
        }
    }
}
