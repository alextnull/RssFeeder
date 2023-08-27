using System.Configuration;
using RssFeeder.Extensions;

namespace RssFeeder.Helpers
{
    /// <summary>
    /// Настройки приложения.
    /// </summary>
    static class Settings
    {
        /// <summary>
        /// Лента.
        /// </summary>
        public static string RssFeed
        {
            get => ConfigurationManager.AppSettings["RssFeed"];
            set => ConfigurationManager.AppSettings["RssFeed"] = value;
        }

        /// <summary>
        /// Частота обновления ленты.
        /// </summary>
        public static int RssUpdateInterval
        {
            get => ConfigurationManager.AppSettings["RssUpdateInterval"].ToInt(30);
            set => ConfigurationManager.AppSettings["RssUpdateInterval"] = value.ToString();
        }

        /// <summary>
        /// Адрес прокси.
        /// </summary>
        public static string ProxyAddress
        {
            get => ConfigurationManager.AppSettings["ProxyAddress"];
            set => ConfigurationManager.AppSettings["ProxyAddress"] = value;
        }

        /// <summary>
        /// Пользовтель прокси.
        /// </summary>
        public static string ProxyUser
        {
            get => ConfigurationManager.AppSettings["ProxyUser"];
            set => ConfigurationManager.AppSettings["ProxyUser"] = value;
        }

        /// <summary>
        /// Пароль пользователя прокси.
        /// </summary>
        public static string ProxyPassword
        {
            get => ConfigurationManager.AppSettings["ProxyPassword"];
            set => ConfigurationManager.AppSettings["ProxyPassword"] = value;
        }
    }
}
