namespace CodeTestApi.Application.Validation_Services
{
    public class VehicleValidationService
    {
        public static bool IsVehicleManufactureDateValid(DateTime manufactureDate)
        {
            var currentYear = DateTime.Now.Year;
            return manufactureDate.Year >= currentYear - 5;
        }
    }
}
