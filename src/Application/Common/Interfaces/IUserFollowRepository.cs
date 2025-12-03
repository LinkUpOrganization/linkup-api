namespace Application.Common.Interfaces;

public interface IUserFollowRepository
{
    Task<List<string>> GetFolloweeIdsAsync(string followerId, CancellationToken ct);
}