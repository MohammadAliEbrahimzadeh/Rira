using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rira.Application.DTOs;

public class PageDto<T>
{
    public List<T>? Data { get; set; }


    public int TotalCount { get; set; }
}
