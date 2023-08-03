using Newtonsoft.Json;

namespace Api.Dtos.BenefitCosts
{
    public class GetBenefitCostsDto
    {
        public int EmployeeId { get; set; }

        // Added because of compare failing (1.000 vs 1.0)
		[JsonConverter(typeof(CustomDecimalConverter))]
		public decimal Paycheck { get; set; }
    }
}
