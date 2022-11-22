using System;
using System.Linq;
using System.Threading.Tasks;
using Application.Discounts.AddNewDiscount;
using MD.PersianDateTime.Standard;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Admin.Endpoint.Binders
{
    public class DiscountEntityBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
            {
                throw new ArgumentNullException(nameof(bindingContext));
            }
            
            var FieldName = bindingContext.FieldName;
            AddNewDiscountDto discountDto = new AddNewDiscountDto()
            {
                CouponCode = bindingContext.ValueProvider.GetValue($"{FieldName}.{nameof(discountDto.CouponCode)}")
                    .Values.ToString(),
                DiscountAmount = int.Parse(bindingContext.ValueProvider.
                    GetValue($"{FieldName}.{nameof(discountDto.DiscountAmount)}").Values.ToString()),

                DiscountLimitationId = int.Parse(bindingContext.ValueProvider
                    .GetValue($"{FieldName}.{nameof(discountDto.DiscountLimitationId)}").Values.ToString()),


                DiscountPercentage = int.Parse(bindingContext.ValueProvider
                    .GetValue($"{FieldName}.{nameof(discountDto.DiscountPercentage)}").Values.ToString()),

                DiscountTypeId = int.Parse(bindingContext.ValueProvider
                    .GetValue($"{FieldName}.{nameof(discountDto.DiscountTypeId)}").Values.ToString()),
                LimitationTimes = int.Parse(bindingContext.ValueProvider
                    .GetValue($"{FieldName}.{nameof(discountDto.LimitationTimes)}").Values.ToString()),

                UsePercentage = bool.Parse(bindingContext.ValueProvider
                    .GetValue($"{FieldName}.{nameof(discountDto.UsePercentage)}").FirstValue.ToString()),

                Name = bindingContext.ValueProvider
                    .GetValue($"{FieldName}.{nameof(discountDto.Name)}").Values.ToString(),

                RequiresCouponCode = bool.Parse(bindingContext.ValueProvider
                    .GetValue($"{FieldName}.{nameof(discountDto.RequiresCouponCode)}").FirstValue.ToString()),

                StartDate = PersianDateTime.Parse(bindingContext.ValueProvider
                    .GetValue($"{FieldName}.{nameof(discountDto.StartDate)}").Values.ToString()),  

                EndDate = PersianDateTime.Parse(bindingContext.ValueProvider
                    .GetValue($"{FieldName}.{nameof(discountDto.EndDate)}").Values.ToString()),
                
            };

            var appliedToCatalogItem = bindingContext.ValueProvider.
                GetValue($"{FieldName}.{nameof(discountDto.appliedToCatalogItem)}");

            if (!string.IsNullOrEmpty(appliedToCatalogItem.Values))
            {
                discountDto.appliedToCatalogItem = appliedToCatalogItem.Values.ToString().Split(',')
                    .Select(s => Int32.Parse(s)).ToList();
            }


            bindingContext.Result = ModelBindingResult.Success(discountDto);
            return Task.CompletedTask;
        }
    }
}