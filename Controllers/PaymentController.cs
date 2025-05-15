using Microsoft.AspNetCore.Mvc;

namespace ProjectCostEstimatorAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : ControllerBase
    {
        public class PaymentRequest
        {
            public int ProjectId { get; set; }
            public decimal Amount { get; set; }
            public string Method { get; set; }
        }

        [HttpPost]
        public IActionResult SimulatePayment(PaymentRequest request)
        {
            return Ok(new
            {
                Message = "Payment successful (simulated)",
                request.ProjectId,
                request.Amount,
                request.Method,
                Timestamp = DateTime.UtcNow
            });
        }
    }
}
