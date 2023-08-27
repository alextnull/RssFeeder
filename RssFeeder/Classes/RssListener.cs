using System;
using System.Collections.Generic;
using System.Timers;
using RssFeeder.Models;

namespace RssFeeder.Classes
{
    /// <summary>
    /// Прослушиватель rss ленты.
    /// </summary>
    class RssListener
    {
        /// <summary>
        /// Происходит, когда обновляется лента.
        /// </summary>
        public event EventHandler<IEnumerable<FeedItem>> FeedUpdates;

        private readonly Timer _listenTimer;
        private readonly RssParser _rssParser;
        private readonly string _url;

        /// <summary>
        /// Начинает прослушивание.
        /// </summary>
        public void StartListen()
        {
            OnFeedUpdates();
            _listenTimer.Start();
        }

        /// <summary>
        /// Останавливает прослушивание.
        /// </summary>
        public void StopListen()
        {
            _listenTimer.Stop();
        }

        /// <summary>
        /// Уведомляет о обновлении ленты.
        /// </summary>
        protected void OnFeedUpdates()
        {
            if (FeedUpdates is null)
                return;

            var ribbonItems = _rssParser.Parse(_url);
            FeedUpdates.Invoke(this, ribbonItems);
        }

        /// <summary>
        /// Уведомляет о обновлении ленты при наступлении интервала.
        /// </summary>
        private void ListenTimerOnElapsed(object sender, ElapsedEventArgs e)
        {
            OnFeedUpdates();
        }

        /// <summary>
        /// Инициализирует прослушиватель rss ленты.
        /// </summary>
        /// <param name="url">Rss лента.</param>
        /// <param name="intervalSeconds">Интервал обновления.</param>
        public RssListener(string url, int intervalSeconds)
        {
            _url = url;
            _rssParser = new RssParser();
            _listenTimer = new Timer(intervalSeconds * 1000);
            _listenTimer.Elapsed += ListenTimerOnElapsed;
        }
    }
}
