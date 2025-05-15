using Microsoft.AspNetCore.Mvc;


[ApiController]
[Route("api/[controller]")]
public class StaffController : ControllerBase {
    private readonly DataService _data;

    public StaffController(DataService data) => _data = data;

    [HttpGet] public IActionResult Get() => Ok(_data.Staffs);

    [HttpPost]
    public IActionResult Post(Staff s) {
        s.Id = _data.Staffs.Count + 1;
        _data.Staffs.Add(s);
        return Ok(s);
    }
}
