using Application.Common;
using Application.Common.Interfaces;
using Application.Common.Models;
using MediatR;

namespace Application.Auth.Queries.GetCurrentUserInfo;

public class GetCurrentUserInfoQuery : IRequest<Result<User>>
{
}

public class GetCurrentUserInfoQueryHandler(IUserService userService, IAccountService accountService)
    : IRequestHandler<GetCurrentUserInfoQuery, Result<User>>
{
    public async Task<Result<User>> Handle(GetCurrentUserInfoQuery request, CancellationToken ct)
    {
        string userId = userService.Id!;
        return await accountService.GetUserByIdAsync(userId);
    }
}
