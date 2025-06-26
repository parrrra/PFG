namespace GymPlanner.Application.Routines.Queries.GetRoutines;

public class GetRoutinesQuery
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public string? Filter { get; set; }
    public Guid? UserId { get; set; }
}
