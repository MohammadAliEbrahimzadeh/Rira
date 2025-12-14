using Mapster;
using Rira.Application.DTOs.Requests;
using Rira.Application.DTOs.Responses;
using Rira.Application.Helper;
using Rira.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rira.Application.Mappers;

public class UserMapper : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<User, GetUserDto>()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.FirstName, src => src.FirstName)
            .Map(dest => dest.LastName, src => src.LastName)
            .Map(dest => dest.NationalCode, src => src.NationalCode)
            .Map(dest => dest.BirthDatePersian, src => src.BirthDate.ConvertToPersian())
            .Map(dest => dest.BirthDate, src => src.BirthDate);

        config.NewConfig<CreateUserDto, User>()
            .Map(dest => dest.FirstName, src => src.FirstName)
            .Map(dest => dest.LastName, src => src.LastName)
            .Map(dest => dest.NationalCode, src => src.NationalCode)
            .Map(dest => dest.BirthDate, src => src.BirthDate);
    }
}
