namespace Application.Posts.Queries.GetPostClusters;

public class ClusterDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public int Count { get; set; }
}
