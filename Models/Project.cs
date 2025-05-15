public class Project {
    public int Id { get; set; }
    public string Name { get; set; }
    public int AssumedHours { get; set; }
    public List<int> StaffIds { get; set; } = new();
}
