using FilteringOrderingPagination.Models.Paging;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;

namespace FilteringOrderingPagination.AspNetCore;

public class PagingBinder : IModelBinder
{
    public async Task BindModelAsync(ModelBindingContext bindingContext)
    {
        var modelName = bindingContext.ModelName;
        var valueProviderResult = bindingContext.ValueProvider.GetValue(modelName);

        if (valueProviderResult == ValueProviderResult.None) return;

        var value = valueProviderResult.FirstValue;

        try
        {
            object result = new Paging(0, 25);
            if (!string.IsNullOrEmpty(value)) result = JsonConvert.DeserializeObject(value, bindingContext.ModelType);
            bindingContext.ModelState.SetModelValue(modelName, valueProviderResult);
            bindingContext.Result = ModelBindingResult.Success(result);
        }
        catch
        {
            bindingContext.ModelState.TryAddModelError(modelName, "An error occured while parsing JSON");
        }
    }
}