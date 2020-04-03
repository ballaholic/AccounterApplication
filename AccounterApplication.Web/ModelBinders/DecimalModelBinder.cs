namespace AccounterApplication.Web.ModelBinders
{
    using System;
    using System.Globalization;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc.ModelBinding;

    public class DecimalModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var valueProviderResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);

            if (valueProviderResult == null)
            {
                return Task.CompletedTask;
            }

            var value = valueProviderResult.FirstValue;

            if (string.IsNullOrEmpty(value))
            {
                return Task.CompletedTask;
            }

            string errorMessage = string.Empty;

            if (CultureInfo.CurrentCulture.Name == "bg-BG")
            {
                value = value.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator).Trim();
                errorMessage = "Въведената стойност не е валидно число";
            }
            else if (CultureInfo.CurrentCulture.Name == "en-US")
            {
                value = value.Replace(",", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator).Trim();
                errorMessage = "Provided value is not a valid number";
            }

            if (decimal.TryParse(value, NumberStyles.AllowDecimalPoint, CultureInfo.CurrentCulture, out decimal parsedValue))
            {
                bindingContext.Result = ModelBindingResult.Success(parsedValue);
                return Task.CompletedTask;
            }

            bindingContext.ModelState.TryAddModelError(bindingContext.ModelName, errorMessage);
            return Task.CompletedTask;
        }
    }
}
