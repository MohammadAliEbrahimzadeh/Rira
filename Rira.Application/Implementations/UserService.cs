using Mapster;
using Microsoft.EntityFrameworkCore;
using Rira.Application.Contracts;
using Rira.Application.DTOs;
using Rira.Application.DTOs.Requests;
using Rira.Application.DTOs.Responses;
using Rira.Application.Helper;
using Rira.Domain.Models;
using Rira.Persistence;

namespace Rira.Application.Implementations;

public class UserService : IUserService
{
    private readonly UnitOfWork _unitOfWork;

    public UserService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = (UnitOfWork)unitOfWork;
    }

    public async Task<long> CreateUserAsync(CreateUserDto dto, CancellationToken cancellationToken)
    {
        if (await _unitOfWork.UserRepository.DuplicateNationalCode(dto.NationalCode!, cancellationToken))
            return 0;

        await _unitOfWork.AddAsync(dto.Adapt<User>(), cancellationToken);

        return await _unitOfWork.CommitAsync();
    }

    public async Task<long> DeleteUserAsync(long id, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.FindByIdAsync<User>(id, cancellationToken);

        if (user is null)
            return 0;

        _unitOfWork.Remove(user);

        await _unitOfWork.CommitAsync();

        return id;
    }

    public async Task<PageDto<GetUserDto>> GetUsersListAsync(GetFilterUsersDto dto, CancellationToken cancellationToken)
    {
        if (!await _unitOfWork.UserRepository.RecordsExistsAsync(cancellationToken))
            return new PageDto<GetUserDto>();

        var usersQuery = _unitOfWork.GetAsQueryable<User>();

        if (dto.Id > 0)
            usersQuery = usersQuery.Where(x => x.Id.Equals(dto.Id));

        if (!string.IsNullOrEmpty(dto.NationalCode))
            usersQuery = usersQuery.Where(x => x.NationalCode != null && x.NationalCode.Equals(dto.NationalCode));

        if (!string.IsNullOrEmpty(dto.FirstName))
            usersQuery = usersQuery.Where(x => x.FirstName != null && x.FirstName.StartsWith(dto.FirstName));

        if (!string.IsNullOrEmpty(dto.LastName))
            usersQuery = usersQuery.Where(x => x.LastName != null && x.LastName.StartsWith(dto.LastName));

        var usersQueryList = await usersQuery.PaginateWithCount(dto.PageNum, dto.PageSize, out int totalCount)
            .ProjectToType<GetUserDto>().ToListAsync(cancellationToken);

        return new PageDto<GetUserDto>
        {
            Data = usersQueryList,
            TotalCount = totalCount
        };
    }

    public async Task<long> UpdateUserAsync(long id, UpdateUserDto dto, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.FindByIdAsync<User>(id, cancellationToken);

        if (user is null)
            return 0;

        user.FirstName = dto.FirstName;
        user.BirthDate = dto.BirthDate;
        user.LastName = dto.LastName;

        _unitOfWork.Update(user);

        await _unitOfWork.CommitAsync();

        return user.Id;
    }
}
