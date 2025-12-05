using Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity;

public class ApplicationUser : IdentityUser
{
    public string DisplayName { get; set; } = null!;
    public List<Post> Posts { get; set; } = [];
    public List<UserFollow> Followers { get; set; } = [];
    public List<UserFollow> Followings { get; set; } = [];
}
