using Microsoft.AspNetCore.Mvc;

namespace ProjectCostEstimatorAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OfficeExpenseController : ControllerBase
    {
        private readonly DataService _data;

        public OfficeExpenseController(DataService data)
        {
            _data = data;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_data.OfficeExpenses);
        }

        [HttpPost]
        public IActionResult Post(OfficeExpense expense)
        {
            expense.Id = _data.OfficeExpenses.Count + 1;
            _data.OfficeExpenses.Add(expense);
            return Ok(expense);
        }
    }
}
