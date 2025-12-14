using Rira.Persistence.Context;
using Rira.Persistence.Repsitories;

namespace Rira.Persistence;

public class UnitOfWork : BaseUnitOfWork
{
    private readonly RiraDbContext _context;
    private UserRepository? _userRepository;

    public UnitOfWork(RiraDbContext context) : base(context)
    {
        _context = context;
    }

    public UserRepository UserRepository => _userRepository ??= new UserRepository(_context);

}