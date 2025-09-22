using Application.Common;
using Application.Common.DTOs;
using Application.Common.Interfaces;
using AutoMapper;
using MediatR;

namespace Application.Users.Queries;

public class GetUserInfoQuery : IRequest<Result<UserDto>>
{
    public string UserId { get; set; } = null!;
}

public class GetUserInfoQueryHandler(IAccountService accountService, IMapper mapper)
    : IRequestHandler<GetUserInfoQuery, Result<UserDto>>
{
    public async Task<Result<UserDto>> Handle(GetUserInfoQuery request, CancellationToken ct)
    {
        var userResult = await accountService.GetUserByIdAsync(request.UserId);
        if (userResult.IsSuccess && userResult.Value != null)
            return Result<UserDto>.Success(mapper.Map<UserDto>(userResult.Value));

        return Result<UserDto>.Failure(userResult.Error!, userResult.Code);
    }
}
