namespace CodeTestApi.Domain.Entities
{
    /// <summary>
    /// Entity representing an user.
    /// </summary>
    public class Vehicle
    {
        /// <summary>
        /// Gets or sets the vehicle's unique identifier.
        /// </summary>
        public required string Id { get; set; }

        /// <summary>
        /// Gets or sets the vehicle's brand.
        /// </summary>
        public required string Brand { get; set; }

        /// <summary>
        /// Gets or sets the vehicle's model.
        /// </summary>
        public required string Model { get; set; }

        /// <summary>
        /// Gets or sets the vehicle's manufacture date.
        /// </summary>
        public DateTime ManufactureDate { get; set; }

        /// <summary>
        /// Gets or sets the vehicle's price per day.
        /// </summary>
        public long PricePerDay { get; set; }

        /// <summary>
        /// Gets or sets the vehicle's periods where it's rented.
        /// </summary>
        public List<RentalPeriod> RentalPeriods { get; set; } = [];
    }
}
