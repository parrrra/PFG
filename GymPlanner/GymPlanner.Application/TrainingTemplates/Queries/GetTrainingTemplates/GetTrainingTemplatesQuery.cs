namespace GymPlanner.Application.TrainingTemplates.Queries.GetTrainingTemplates;

public class GetTrainingTemplatesQuery
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public string? Filter { get; set; }
    public Guid? UserId { get; set; }
}
