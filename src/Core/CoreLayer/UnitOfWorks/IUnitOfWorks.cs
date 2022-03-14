namespace CoreLayer.UnitOfWorks;
public interface IUnitOfWorks
{
    Task CommitAsync();
    void Commit();
}

