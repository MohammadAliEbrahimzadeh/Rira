using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rira.Application.DTOs.Requests;

public class CreateUserDto
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? NationalCode { get; set; }

    public DateTime BirthDate { get; set; }
}
