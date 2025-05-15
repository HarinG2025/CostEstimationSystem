using Microsoft.AspNetCore.Mvc;

namespace ProjectCostEstimatorAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectController : ControllerBase
    {
        private readonly DataService _data;

        public ProjectController(DataService data)
        {
            _data = data;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_data.Projects);
        }

        [HttpPost]
        public IActionResult Post(Project project)
        {
            project.Id = _data.Projects.Count + 1;
            _data.Projects.Add(project);
            return Ok(project);
        }

        [HttpGet("{id}/cost")]
        public IActionResult GetCost(int id)
        {
            var project = _data.Projects.FirstOrDefault(p => p.Id == id);
            if (project == null)
                return NotFound();

            var assumedHours = project.AssumedHours;

            // Staff cost calculation
            decimal staffCost = project.StaffIds
                .Select(staffId => _data.Staffs.FirstOrDefault(s => s.Id == staffId))
                .Where(s => s != null)
                .Sum(s => (s.MonthlySalary / 180) * assumedHours);

            // Office cost calculation
            decimal officeHourlyCost = _data.OfficeExpenses.Sum(e => e.MonthlyCost) / 180;
            decimal officeCost = officeHourlyCost * assumedHours;

            // Total
            decimal totalCost = staffCost + officeCost;
            decimal costPerHour = totalCost / assumedHours;

            var summary = new CostSummary
            {
                Project = project.Name,
                Hours = assumedHours,
                StaffCost = Math.Round(staffCost, 2),
                OfficeCost = Math.Round(officeCost, 2),
                TotalCost = Math.Round(totalCost, 2),
                CostPerHour = Math.Round(costPerHour, 2)
            };

            return Ok(summary);
        }
    }
}
