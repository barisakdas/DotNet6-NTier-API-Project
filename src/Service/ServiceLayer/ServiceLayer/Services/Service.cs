using CoreLayer.Interfaces.Repositories;
using CoreLayer.UnitOfWorks;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.Exceptions;

namespace ServiceLayer.Services;
public class Service<T> : IService<T> where T : class
{
    private readonly IGenericRepository<T> _repository;
    private readonly IUnitOfWorks _unitOfWorks;

    public Service(IGenericRepository<T> repository, IUnitOfWorks unitOfWorks) => (_repository, _unitOfWorks) = (repository, unitOfWorks);

    public async Task<T> AddAsync(T entity)
    {
        await _repository.AddAsync(entity);
        await _unitOfWorks.CommitAsync();

        return entity;
    }

    public async Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities)
    {
        await _repository.AddRangeAsync(entities);
        await _unitOfWorks.CommitAsync();

        return entities;
    }

    public async Task<bool> AnyAsync(Expression<Func<T, bool>> expression)
    {
        return await _repository.AnyAsync(expression);
    }

    public async Task DeleteAsync(T entity)
    {
        _repository.Delete(entity);
        await _unitOfWorks.CommitAsync();
    }

    public async Task DeleteRangeAsync(IEnumerable<T> entities)
    {
        _repository.DeleteRange(entities);
        await _unitOfWorks.CommitAsync();
    }

    public async Task<IEnumerable<T>> GetAll()
    {
        return await _repository.GetAll().ToListAsync();
    }

    public async Task<T> GetByIdAsync(int id)
    {
        var data = await _repository.GetByIdAsync(id);
        if (data is null)
            throw new NotFoundException($"{typeof(T).Name} ({id}) not found.");

        return data;
    }

    public async Task UpdateAsync(T entity)
    {
        _repository.Update(entity);
        await _unitOfWorks.CommitAsync();
    }

    public IQueryable<T> Where(Expression<Func<T, bool>> expression)
    {
        return _repository.Where(expression);
    }

}
