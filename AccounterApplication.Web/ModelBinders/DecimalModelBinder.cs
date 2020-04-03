namespace AccounterApplication.Web.ModelBinders
{
    using System.Globalization;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc.ModelBinding;

    using AccounterApplication.Common.GlobalConstants;

    using Resources = Common.LocalizationResources.ModelBinders.ModelBinderResources;

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

            string errorMessage = Resources.NotValidNumber;

            if (CultureInfo.CurrentCulture.Name == SystemConstants.BulgarianLocale)
            {
                value = value.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator).Trim();
            }
            else if (CultureInfo.CurrentCulture.Name == SystemConstants.EnglishLocale)
            {
                value = value.Replace(",", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator).Trim();
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
