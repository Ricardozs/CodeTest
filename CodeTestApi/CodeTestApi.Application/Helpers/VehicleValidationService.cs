namespace CodeTestApi.Application.Helpers
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
