using Application.Common;
using Application.Common.DTOs;
using Application.Common.Interfaces;
using MediatR;

namespace Application.Auth.Queries.GetCurrentUserInfo;

public class GetCurrentUserInfoQuery : IRequest<Result<UserProfileDto>>
{
}

public class GetCurrentUserInfoQueryHandler(ICurrentUserService currentUserService, IAccountService accountService)
    : IRequestHandler<GetCurrentUserInfoQuery, Result<UserProfileDto>>
{
    public async Task<Result<UserProfileDto>> Handle(GetCurrentUserInfoQuery request, CancellationToken ct)
    {
        string userId = currentUserService.Id!;
        var userResult = await accountService.GetUserInformationAsync(userId);
        if (userResult.IsSuccess && userResult.Value != null)
            return Result<UserProfileDto>.Success(userResult.Value);

        return Result<UserProfileDto>.Failure(userResult.Error!, userResult.Code);
    }
}
