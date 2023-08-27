using RssFeeder.Helpers;
using RssFeeder.Infrastructure;

namespace RssFeeder.ViewModels
{
    /// <summary>
    /// Вью-модель настроек.
    /// </summary>
    class SettingsViewModel : ViewModelBase
    {
        /// <summary>
        /// Лента.
        /// </summary>
        public string RssFeed
        {
            get => Settings.RssFeed;
            set => Settings.RssFeed = value;
        }

        /// <summary>
        /// Частота обновления ленты.
        /// </summary>
        public int RssUpdateInterval
        {
            get => Settings.RssUpdateInterval;
            set => Settings.RssUpdateInterval = value;
        }

        /// <summary>
        /// Адрес прокси.
        /// </summary>
        public string ProxyAddress
        {
            get => Settings.ProxyAddress;
            set => Settings.ProxyAddress = value;
        }

        /// <summary>
        /// Пользовтель прокси.
        /// </summary>
        public string ProxyUser
        {
            get => Settings.ProxyUser;
            set => Settings.ProxyUser = value;
        }

        /// <summary>
        /// Пароль пользователя прокси.
        /// </summary>
        public string ProxyPassword
        {
            get => Settings.ProxyPassword;
            set => Settings.ProxyPassword = value;
        }
    }
}
