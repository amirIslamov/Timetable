using FilteringOrderingPagination.Models.Paging;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;

namespace FilteringOrderingPagination.AspNetCore;

public class PagingBinderProvider : IModelBinderProvider
{
    public IModelBinder GetBinder(ModelBinderProviderContext context)
    {
        if (context.Metadata.ModelType == typeof(Paging)) return new BinderTypeModelBinder(typeof(PagingBinder));

        return null;
    }
}