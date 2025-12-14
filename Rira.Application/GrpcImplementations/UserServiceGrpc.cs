using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Rira.Application.Contracts;
using Rira.Application.DTOs;
using Rira.Application.DTOs.Requests;
using Rira.Application.DTOs.Responses;
using Rira.Application.Helper;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Rira.Application.GrpcImplementations;

public class UserServiceGrpc : UserService.UserServiceBase
{
    private readonly IUserService _userService;

    public UserServiceGrpc(IUserService userService)
    {
        _userService = userService;
    }

    public override async Task<UserResponse> GetUserList(GetUserRequestFilter request, ServerCallContext context)
    {
        var result = await _userService.GetUsersListAsync(new GetFilterUsersDto()
        {
            FirstName = request.FirstName,
            Id = request.Id,
            LastName = request.LastName,
            NationalCode = request.NationalCode,
            PageNum = request.PageNum > 0 ? request.PageNum : 1,
            PageSize = request.PageSize > 0 ? request.PageSize : 10,
        }, context.CancellationToken);

        if (result.Data == null)
            throw new RpcException(new Status(StatusCode.NotFound, "No Data Was Found"));

        var users = result.Data.Select(u => new UserDto
        {
            Id = u.Id,
            FirstName = u.FirstName ?? string.Empty,
            LastName = u.LastName ?? string.Empty,
            NationalCode = u.NationalCode ?? string.Empty,
            BirthDate = u.BirthDatePersian ?? string.Empty,
            BirthDateTs = u.BirthDate.ToShortDateString(),
        }).ToList();

        return new UserResponse
        {
            User = { users },
            TotalCount = result.TotalCount
        };
    }

    public override async Task<Empty> CreateUser(CreateUserRequest request, ServerCallContext context)
    {
        if (Convert.ToDateTime(request.BirthDate) > DateTime.Now)
            throw new RpcException(new Status(StatusCode.InvalidArgument, "Birth date cannot be in the future"));

        if (!Regex.IsMatch(request.NationalCode, RegexHelper.NationalCodeRegex, RegexOptions.Compiled))
            throw new RpcException(new Status(StatusCode.InvalidArgument, "NationalCode Structure Is Invalid"));

        if (!Regex.IsMatch(request.FirstName, RegexHelper.NameRegex, RegexOptions.Compiled))
            throw new RpcException(new Status(StatusCode.InvalidArgument, "FirstName Structure Is Invalid"));

        if (!Regex.IsMatch(request.LastName, RegexHelper.NameRegex, RegexOptions.Compiled))
            throw new RpcException(new Status(StatusCode.InvalidArgument, "LastName Structure Is Invalid"));

        var insertedId = await _userService.CreateUserAsync(new CreateUserDto
        {
            BirthDate = Convert.ToDateTime(request.BirthDate),
            FirstName = request.FirstName,
            LastName = request.LastName,
            NationalCode = request.NationalCode,
        }, context.CancellationToken);

        if (insertedId <= 0)
            throw new RpcException(new Status(StatusCode.Internal, "NationalCode Is Duplicate"));

        return new Empty();
    }

    public async override Task<Empty> UpdateUser(UpdateUserRequest request, ServerCallContext context)
    {
        if (Convert.ToDateTime(request.BirthDate) > DateTime.Now)
            throw new RpcException(new Status(StatusCode.InvalidArgument, "Birth date cannot be in the future"));

        if (!Regex.IsMatch(request.FirstName, RegexHelper.NameRegex, RegexOptions.Compiled))
            throw new RpcException(new Status(StatusCode.InvalidArgument, "FirstName Structure Is Invalid"));

        if (!Regex.IsMatch(request.LastName, RegexHelper.NameRegex, RegexOptions.Compiled))
            throw new RpcException(new Status(StatusCode.InvalidArgument, "LastName Structure Is Invalid"));

        var insertedId = await _userService.UpdateUserAsync(request.Id, new UpdateUserDto
        {
            BirthDate = Convert.ToDateTime(request.BirthDate),
            FirstName = request.FirstName,
            LastName = request.LastName,
        }, context.CancellationToken);

        if (insertedId <= 0)
            throw new RpcException(new Status(StatusCode.Internal, "No Data Was Found"));

        return new Empty();
    }

    public async override Task<Empty> DeleteUser(DeleteUserRequest request, ServerCallContext context)
    {
        var insertedId = await _userService.DeleteUserAsync(request.Id, context.CancellationToken);

        if (insertedId <= 0)
            throw new RpcException(new Status(StatusCode.Internal, "No Data Was Found"));

        return new Empty();
    }
}
