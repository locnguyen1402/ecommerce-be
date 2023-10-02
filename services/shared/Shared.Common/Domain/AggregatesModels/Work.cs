namespace ECommerce.Shared.Common.Domain.AggregatesModels;
public class Work : BaseEntity
{
    public List<string> Subjects { get; set; } = new List<string>();
    public List<string> SubjectPeople { get; set; } = new List<string>();
    public List<string> SubjectTimes { get; set; } = new List<string>();
    public List<string> SubjectPlaces { get; set; } = new List<string>();
}