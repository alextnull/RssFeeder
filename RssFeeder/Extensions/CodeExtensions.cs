namespace RssFeeder.Extensions
{
    /// <summary>
    /// Расширения.
    /// </summary>
    static class CodeExtensions
    {
        /// <summary>
        /// Преобразует строку в Int32.
        /// </summary>
        /// <param name="line">Строка.</param>
        /// <param name="defaultValue">Стандартное значение.</param>
        /// <returns></returns>
        public static int ToInt(this string line, int defaultValue)
        {
            if (int.TryParse(line, out int number))
            {
                return number;
            }
            return defaultValue;
        }
    }
}
