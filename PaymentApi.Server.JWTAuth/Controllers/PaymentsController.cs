using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PaymentApi.Server.Data;
using PaymentApi.Server.Models;

namespace PaymentApi.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PaymentsController : ControllerBase
    {
        // Get all payments
        [HttpGet]
        public ActionResult<IEnumerable<Payment>> GetPayments()
        {
            var payments = PaymentRepository.GetPayments();
            return Ok(payments);
        }

        // Get a payment by ID
        [HttpGet("{id}")]
        public ActionResult<Payment> GetPayment(int id)
        {
            var payment = PaymentRepository.GetPaymentById(id);
            if (payment == null)
                return NotFound();
            return Ok(payment);
        }

        // Add a new payment
        [HttpPost]
        public ActionResult<Payment> PostPayment([FromBody] Payment payment)
        {
            if (payment == null)
                return BadRequest();

            payment.Id = PaymentRepository.GetPayments().Max(p => p.Id) + 1;  // Auto-increment ID
            PaymentRepository.AddPayment(payment);
            return CreatedAtAction(nameof(GetPayment), new { id = payment.Id }, payment);
        }

        // Update an existing payment
        [HttpPut("{id}")]
        public IActionResult PutPayment(int id, [FromBody] Payment payment)
        {
            if (payment == null)
                return BadRequest();

            PaymentRepository.UpdatePayment(id, payment);
            return NoContent();
        }

        // Delete a payment
        [HttpDelete("{id}")]
        public IActionResult DeletePayment(int id)
        {
            var payment = PaymentRepository.GetPaymentById(id);
            if (payment == null)
                return NotFound();

            PaymentRepository.DeletePayment(id);
            return NoContent();
        }
    }
}