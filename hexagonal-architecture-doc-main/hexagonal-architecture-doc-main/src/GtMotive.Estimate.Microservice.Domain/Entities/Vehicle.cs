using System;

namespace GtMotive.Estimate.Microservice.Domain.Entities
{
    /// <summary>
    /// Entity representing a vehicle.
    /// </summary>
    public class Vehicle
    {
        /// <summary>
        /// Gets or sets the vehicle unique identifier.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the Brand of the vehicle.
        /// </summary>
        public string Brand { get; set; }

        /// <summary>
        /// Gets or sets the Model of the vehicle.
        /// </summary>
        public string Model { get; set; }

        /// <summary>
        /// Gets or sets the Year of the vehicle.
        /// </summary>
        public DateTime Year { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the vehicle is available.
        /// </summary>
        public bool IsAvailable { get; set; }

        /// <summary>
        /// Gets or sets the renter of the vehicle if any.
        /// </summary>
        public Guid? RentedByUser { get; set; }
    }
}
