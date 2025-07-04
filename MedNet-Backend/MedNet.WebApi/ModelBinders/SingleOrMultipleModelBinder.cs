using System.Text.Json;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace MedNet.WebApi.ModelBinders;

public class SingleOrMultipleModelBinder<T> : IModelBinder
{
    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
        var json = new StreamReader(bindingContext.HttpContext.Request.Body).ReadToEndAsync().Result;

        if (string.IsNullOrWhiteSpace(json))
        {
            bindingContext.Result = ModelBindingResult.Success(new List<T>());
            return Task.CompletedTask;
        }

        try
        {
            var result = JsonSerializer.Deserialize<List<T>>(json, JsonSerializerOptions.Web);
            if (result != null)
            {
                bindingContext.Result = ModelBindingResult.Success(result);
            }
            else
            {
                // try to deserialize a single object
                var singleItem = JsonSerializer.Deserialize<T>(json, JsonSerializerOptions.Web);
                if (singleItem is null)
                {
                    bindingContext.ModelState.AddModelError(bindingContext.ModelName, "Invalid input format.");
                    bindingContext.Result = ModelBindingResult.Failed();
                }
                else
                {
                    bindingContext.Result = ModelBindingResult.Success(new List<T> { singleItem });
                }
            }
        }
        catch
        {
            bindingContext.ModelState.AddModelError(bindingContext.ModelName, "Invalid input format.");
            bindingContext.Result = ModelBindingResult.Failed();
        }

        return Task.CompletedTask;
    }
}
