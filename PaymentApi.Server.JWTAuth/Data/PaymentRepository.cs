using PaymentApi.Server.Models;

namespace PaymentApi.Server.Data
{
    public static class PaymentRepository
    {
        // Static list to simulate in-memory data
        private static readonly List<Payment> Payments = new List<Payment>
        {
            new Payment { Id = 1, Payer = "John Doe", Payee = "Acme Corp", Amount = 100.50m, Date = DateTime.Now },
            new Payment { Id = 2, Payer = "Jane Smith", Payee = "Beta LLC", Amount = 200.75m, Date = DateTime.Now }
        };

        public static List<Payment> GetPayments() => Payments;

        public static Payment GetPaymentById(int id) => Payments.FirstOrDefault(p => p.Id == id);

        public static void AddPayment(Payment payment) => Payments.Add(payment);

        public static void UpdatePayment(int id, Payment updatedPayment)
        {
            var payment = GetPaymentById(id);
            if (payment != null)
            {
                payment.Payer = updatedPayment.Payer;
                payment.Payee = updatedPayment.Payee;
                payment.Amount = updatedPayment.Amount;
                payment.Date = updatedPayment.Date;
            }
        }

        public static void DeletePayment(int id) => Payments.RemoveAll(p => p.Id == id);
    }
}