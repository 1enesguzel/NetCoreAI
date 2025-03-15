namespace NetCoreAI.Project02_ApiConsumeAI.Dtos
{
    public class GetByIdCustomerDto
    {
        public int customerId { get; set; }
        public string customerName { get; set; }
        public string customerSurname { get; set; }
        public decimal customerBalance { get; set; }
    }
}
