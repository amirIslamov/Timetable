using FilteringOrderingPagination.Models;
using FilteringOrderingPagination.Models.Paging;
using FilteringOrderingPagination.Options;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace FilteringOrderingPagination.AspNetCore;

public class FopRequestBinder: IModelBinder
{
    private DefaultPagingOptions _option;

    public FopRequestBinder(IOptions<DefaultPagingOptions> optionAccessor)
    {
        _option = optionAccessor.Value;
    }

    public async Task BindModelAsync(ModelBindingContext bindingContext)
    {
        try
        {
            var pagingJson = bindingContext.ValueProvider.GetValue("paging").FirstValue;
            var filterJson = bindingContext.ValueProvider.GetValue("filter").FirstValue;

            var fopRequestType = bindingContext.ModelType;

            object activatedRequest = Activator.CreateInstance(fopRequestType);

            var filterType = bindingContext.ModelType.GenericTypeArguments[1];
            var activatedFilter = Activator.CreateInstance(filterType);

            if (!string.IsNullOrEmpty(filterJson))
            {
                JsonConvert.PopulateObject(filterJson, activatedFilter);
            }

            var filterProperty = fopRequestType.GetProperty("Filter");
            filterProperty.SetValue(activatedRequest, activatedFilter);

            if (!string.IsNullOrEmpty(pagingJson))
            {
                var paging = JsonConvert.DeserializeObject(pagingJson, typeof(Paging));
                
                var pagingProperty = fopRequestType.GetProperty("Paging");
                pagingProperty.SetValue(activatedRequest, paging);
            }
            else
            {
                var paging = new Paging(_option.PageNum, _option.PageSize);
                
                var pagingProperty = fopRequestType.GetProperty("Paging");
                pagingProperty.SetValue(activatedRequest, paging);
            }
        
            bindingContext.Result = ModelBindingResult.Success(activatedRequest);
        }
        catch (Exception e)
        {
            bindingContext.Result = ModelBindingResult.Failed();
        }
    }
}