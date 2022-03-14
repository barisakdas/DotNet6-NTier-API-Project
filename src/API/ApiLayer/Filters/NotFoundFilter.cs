namespace ApiLayer.Filters;
public class NotFoundFilter<T> : IAsyncActionFilter where T : BaseEntity
{
    private readonly IService<T> _service;

    public NotFoundFilter(IService<T> service) => _service = service;

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var idValue = context.ActionArguments.Values.FirstOrDefault();
        if (idValue is null)
            await next.Invoke(); return;

        var id = (int)idValue;
        var anyEntity = _service.AnyAsync(x => x.ID == id);

        if (anyEntity is null) await next.Invoke(); return;

        context.Result = new NotFoundObjectResult(CustomResponseDto<NoContentDto>.Fail(404, $"{typeof(T)}({id}) not found"));

        throw new NotImplementedException();
    }
}

