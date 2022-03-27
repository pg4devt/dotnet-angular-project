namespace Northwind.Backend.Models
{
    public class ListRequest
    {
        /// <summary>
        /// Gets or sets the number of items to retrieve.
        /// </summary>
        public int? Top { get; set; }

        /// <summary>
        /// Gets or sets the number of items to skip before returning the <see cref="Top"/> items.
        /// </summary>
        public int? Skip { get; set; }
    }
}
