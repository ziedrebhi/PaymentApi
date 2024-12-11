namespace PaymentApi.Server.Models
{
    public class Payment
    {
        public int Id { get; set; }
        public string Payer { get; set; }
        public string Payee { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
    }
}
