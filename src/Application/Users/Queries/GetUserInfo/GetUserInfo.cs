using Application.Common;
using Application.Common.DTOs;
using Application.Common.Interfaces;
using MediatR;

namespace Application.Users.Queries.GetUserInfo;

public class GetUserInfoQuery : IRequest<Result<UserProfileDto>>
{
    public string UserId { get; set; } = null!;
}

public class GetUserInfoQueryHandler(IAccountService accountService, ICurrentUserService currentUserService)
    : IRequestHandler<GetUserInfoQuery, Result<UserProfileDto>>
{
    public async Task<Result<UserProfileDto>> Handle(GetUserInfoQuery request,
        CancellationToken ct)
    {
        var currentUserId = currentUserService.Id;
        var userResult = await accountService.GetUserInformationAsync(request.UserId, currentUserId);
        if (userResult.IsSuccess && userResult.Value != null)
            return Result<UserProfileDto>.Success(userResult.Value);

        return Result<UserProfileDto>.Failure(userResult.Error!, userResult.Code);
    }
}
