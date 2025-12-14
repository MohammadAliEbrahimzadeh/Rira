using Microsoft.EntityFrameworkCore;
using Rira.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rira.Persistence.Repsitories;

public class UserRepository
{
    private readonly RiraDbContext _context;

    public UserRepository(RiraDbContext context)
    {
        _context = context;
    }


    public async Task<bool> RecordsExistsAsync(CancellationToken cancellationToken) =>
        await _context.Users.AnyAsync(cancellationToken);


    public async Task<bool> DuplicateNationalCode(string nationalCode, CancellationToken cancellationToken) =>
        await _context.Users.AnyAsync(x => x.NationalCode != null && x.NationalCode!.Equals(nationalCode), cancellationToken);

}
