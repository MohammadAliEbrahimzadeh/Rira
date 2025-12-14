using Rira.Application.DTOs;
using Rira.Application.DTOs.Requests;
using Rira.Application.DTOs.Responses;

namespace Rira.Application.Contracts;

public interface IUserService
{
    Task<PageDto<GetUserDto>> GetUsersListAsync(GetFilterUsersDto dto, CancellationToken cancellationToken);

    Task<long> CreateUserAsync(CreateUserDto dto, CancellationToken cancellationToken);

    Task<long> UpdateUserAsync(long id, UpdateUserDto dto, CancellationToken cancellationToken);

    Task<long> DeleteUserAsync(long id, CancellationToken cancellationToken);
}
