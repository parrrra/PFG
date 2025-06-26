namespace GymPlanner.Application.Exercises.Queries.GetExercises;

public class GetExercisesQuery
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public string? Filter { get; set; }
    public Guid? BodyPartId { get; set; }
}