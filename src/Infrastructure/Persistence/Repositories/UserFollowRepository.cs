using Application.Common.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class UserFollowRepository(ApplicationDbContext dbContext) : IUserFollowRepository
{
    public IQueryable<string> GetFolloweeIds(string followerId)
    {
        return dbContext.UserFollows
            .Where(f => f.FollowerId == followerId)
            .Select(f => f.FolloweeId);
    }

    public async Task<List<string>> GetFolloweeIdsAsync(string followerId, CancellationToken ct)
    {
        return await dbContext.UserFollows
            .Where(f => f.FollowerId == followerId)
            .Select(f => f.FolloweeId)
            .ToListAsync(ct);
    }
}