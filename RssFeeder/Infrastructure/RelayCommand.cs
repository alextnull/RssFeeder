using System;
using System.Windows.Input;

namespace RssFeeder.Infrastructure
{
    /// <summary>
    /// Команда.
    /// </summary>
    class RelayCommand : ICommand
    {
        /// <inheritdoc />
        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        private Action<object> _execute;
        private Func<object, bool> _canExecute;

        /// <summary>
        /// Проверяет может ли команды выполниться.
        /// </summary>
        /// <param name="parameter">Параметр.</param>
        /// <returns><see langword="true"/>, если выполнение команды возможно; иначе <see langword="false"/>.</returns>
        public bool CanExecute(object parameter) => _canExecute?.Invoke(parameter) ?? true;

        /// <summary>
        /// Выполняет команду.
        /// </summary>
        /// <param name="parameter">Параметр.</param>
        public void Execute(object parameter) => _execute?.Invoke(parameter);

        /// <summary>
        /// Инициализирует команду.
        /// </summary>
        /// <param name="execute">Обработчик при выполнении команды.</param>
        /// <param name="canExecute">Обработчик проверки возможности выполнения команды.</param>
        public RelayCommand(Action<object> execute, Func<object, bool> canExecute)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        /// <summary>
        /// Инициализирует команду.
        /// </summary>
        /// <param name="execute">Обработчик при выполнении команды.</param>
        public RelayCommand(Action<object> execute)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
        }
    }
}
