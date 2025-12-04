using Domain.Entities;

namespace Application.Common.Interfaces;

public interface IReactionRepository
{
    Task<PostReaction?> GetReactionAsync(string postId, string userId, CancellationToken ct = default);
    Task AddReactionAsync(PostReaction reaction, CancellationToken ct = default);
    Task RemoveReactionAsync(PostReaction reaction, CancellationToken ct = default);
}
