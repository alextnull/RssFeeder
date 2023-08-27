using NLog;
using RssFeeder.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net;
using System.ServiceModel.Syndication;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using RssFeeder.Helpers;

namespace RssFeeder.Classes
{
    /// <summary>
    /// Rss парсер.
    /// </summary>
    class RssParser
    {
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Парсит элементы ленты.
        /// </summary>
        /// <param name="url">Лента.</param>
        /// <returns>Элементы ленты.</returns>
        public IEnumerable<FeedItem> Parse(string url)
        {
            SyndicationFeed feed = null;

            try
            {
                var xml = GetXml(url);
                using var stringReader = new StringReader(xml);
                using var reader = XmlReader.Create(stringReader);
                feed = SyndicationFeed.Load(reader);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }

            if (feed is null)
            {
                yield break;
            }

            foreach (var item in feed.Items)
            {
                var rssXml = GerRssXml(item);
                var feedItem = GetFeedItem(rssXml);
                if (feedItem is not null)
                {
                    yield return feedItem;
                }
            }
        }

        /// <summary>
        /// Возвращает xml по url.
        /// </summary>
        /// <param name="url">Url.</param>
        /// <returns></returns>
        private string GetXml(string url)
        {
            string xml = null;
            try
            {
                var httpClientHandler = new HttpClientHandler()
                {
                    Proxy = new WebProxy(Settings.ProxyAddress, false)
                    {
                        Credentials = new NetworkCredential(Settings.ProxyUser, Settings.ProxyPassword)
                    },
                    PreAuthenticate = true,
                    UseProxy = true
                };
                using var httpClient = new HttpClient(httpClientHandler, disposeHandler: true);
                xml = httpClient.GetStringAsync(url).Result;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }

            return xml;
        }

        /// <summary>
        /// Преобразует элемент веб канала в формат rss.
        /// </summary>
        /// <param name="item">Элемент веб канала.</param>
        /// <returns>Элемент веб канала в формате rss.</returns>
        private string GerRssXml(SyndicationItem item)
        {
            var xmlBuilder = new StringBuilder();

            try
            {
                using var xmlWriter = XmlWriter.Create(xmlBuilder);
                item.SaveAsRss20(xmlWriter);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }

            return xmlBuilder.ToString();
        }

        /// <summary>
        /// Преобразует rss-xml в модель.
        /// </summary>
        /// <param name="xml">rss-xml.</param>
        /// <returns>Модель элемента ленты.</returns>
        private FeedItem GetFeedItem(string xml)
        {
            FeedItem feedItem = null;

            try
            {
                using var stringReader = new StringReader(xml);
                XmlSerializer serializer = new XmlSerializer(typeof(FeedItem));
                feedItem = serializer.Deserialize(stringReader) as FeedItem;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }

            return feedItem;
        }
    }
}
