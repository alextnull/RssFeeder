using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace RssFeeder.Infrastructure
{
    /// <summary>
    /// Абстрактная база для объектов, которые используют привязки.
    /// </summary>
    abstract class BindableBase : INotifyPropertyChanged
    {
        /// <inheritdoc />
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Устанавилвает значение свойству и уведомляет о его изменении.
        /// </summary>
        /// <typeparam name="T">Тип поля.</typeparam>
        /// <param name="field">Поле свойства.</param>
        /// <param name="value">Новое значение.</param>
        /// <param name="propertyName">Имя свойства.</param>
        /// <returns><see langword="true"/>, если значение свойства изменилось; иначе <see langword="false"/>.</returns>
        public bool SetProperty<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value))
            {
                return false;
            }
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        /// <summary>
        /// Уведомляет о изменении свойства.
        /// </summary>
        /// <param name="propertyName">Имя свойства.</param>
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
