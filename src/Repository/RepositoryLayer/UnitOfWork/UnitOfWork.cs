using CoreLayer.UnitOfWorks;

namespace RepositoryLayer.UnitOfWork;
public class UnitOfWork : IUnitOfWorks
{
    private readonly AppDbContext _context;

    public UnitOfWork(AppDbContext context) => _context = context;


    public void Commit()
    {
        _context.SaveChanges();
    }

    public async Task CommitAsync()
    {
        await _context.SaveChangesAsync();
    }
}
