using System.Collections.Generic;
using RssFeeder.Classes;
using RssFeeder.Infrastructure;
using RssFeeder.Models;
using System.Collections.ObjectModel;
using RssFeeder.Helpers;
using System.Windows.Input;

namespace RssFeeder.ViewModels
{
    /// <summary>
    /// Вью-модель ленты.
    /// </summary>
    class FeedViewModel : ViewModelBase
    {
        private ObservableCollection<FeedItem> _feedItems;

        /// <summary>
        /// Список элементов ленты.
        /// </summary>
        public ObservableCollection<FeedItem> FeedItems
        {
            get => _feedItems;
            set => SetProperty(ref _feedItems, value);
        }

        /// <summary>
        /// Команда после загрзуки.
        /// </summary>
        public ICommand LoadedCommand { get; set; }

        /// <summary>
        /// Начинает прослушивание.
        /// </summary>
        /// <param name="parameter"></param>
        private void Loaded(object parameter)
        {
            var rssParser = new RssListener(Settings.RssFeed, Settings.RssUpdateInterval);
            rssParser.FeedUpdates += OnFeedUpdates;
            rssParser.StartListen();
        }

        /// <summary>
        /// Обновляет список элементов ленты.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="feedItems"></param>
        private void OnFeedUpdates(object sender, IEnumerable<FeedItem> feedItems)
        {
            FeedItems = new ObservableCollection<FeedItem>(feedItems);
        }

        /// <summary>
        /// Инициализирует вью-модель ленты.
        /// </summary>
        public FeedViewModel()
        {
            LoadedCommand = new RelayCommand(Loaded);
        }
    }
}