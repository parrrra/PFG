namespace GymPlanner.Application.Trainings.Queries.GetTrainings;

public class GetTrainingsQuery
{
    public Guid UserId { get; init; }
    public int Year { get; init; }
    public int Month { get; init; }
}