using FilteringOrderingPagination.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;

namespace FilteringOrderingPagination.AspNetCore;

public class FopRequestBinderProvider: IModelBinderProvider
{
    public IModelBinder GetBinder(ModelBinderProviderContext context)
    {
        if (HasGenericTypeBase(context.Metadata.ModelType, typeof(FopRequest<,>)))
        {
            return new BinderTypeModelBinder(typeof(FopRequestBinder));
        }

        return null;
    }
    
    private bool HasGenericTypeBase(Type type, Type genericType)
    {
        while (type != typeof(object))
        {
            if (type.IsGenericType && type.GetGenericTypeDefinition() == genericType) return true;
            type = type.BaseType;
        }

        return false;
    }
}