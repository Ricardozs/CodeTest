namespace CodeTestApi.Domain.Entities
{
    public class RentalPeriod
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public required string RentedBy { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
