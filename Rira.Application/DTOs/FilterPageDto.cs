using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rira.Application.DTOs;

public class FilterPageDto
{
    public FilterPageDto()
    {
        PageNum = 1;
        PageSize = 10;
    }

    public int PageSize { get; set; }

    public int PageNum { get; set; }
}
