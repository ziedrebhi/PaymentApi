namespace PaymentApi.Client.Models
{
    public class Payment
    {
        public int Id { get; set; }
        public string Sender { get; set; } = string.Empty;
        public string Receiver { get; set; } = string.Empty;
        public decimal Amount { get; set; }
    }
}