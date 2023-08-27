namespace RssFeeder.Infrastructure
{
    /// <summary>
    /// Абстрактная база для вью-моделей.
    /// </summary>
    abstract class ViewModelBase : BindableBase
    {
        public string Name { get; set; }
    }
}
