namespace Northwind.Backend.Models
{
    public abstract class ListResult<T>
    {
        /// <summary>
        /// The total count of filtered items that can be retrieved.
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// The items retrieved.
        /// </summary>
        public IEnumerable<T>? Items { get; set; }
    }
}
