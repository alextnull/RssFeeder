using System;
using System.Globalization;
using System.Xml.Serialization;

namespace RssFeeder.Models
{
    /// <summary>
    /// Элемент ленты.
    /// </summary>
    [XmlRoot("item")]
    public class FeedItem
    {
        /// <summary>
        /// Заголовок.
        /// </summary>
        [XmlElement(ElementName = "title")]
        public string Title { get; set; }

        /// <summary>
        /// Дата публикации.
        /// </summary>
        [XmlElement(ElementName = "pubDate")]
        public string PublishDateString { get; set; }

        /// <summary>
        /// Ссылка на статью.
        /// </summary>
        [XmlElement(ElementName = "link")]
        public string Link { get; set; }

        /// <summary>
        /// Описание.
        /// </summary>
        [XmlElement(ElementName = "description")]
        public string Description { get; set; }

        /// <summary>
        /// Дата публикации.
        /// </summary>
        public DateTimeOffset? PublishDate
        {
            get
            {
                DateTimeOffset? publishDate = null;
                try
                {
                    var parseFormat = "ddd, dd MMM yyyy HH:mm:ss Z";
                    publishDate = DateTimeOffset.ParseExact(PublishDateString, parseFormat,
                                                   CultureInfo.InvariantCulture,
                                                   DateTimeStyles.None);
                }
                catch
                {
                }

                return publishDate;
            }
        }
    }
}
