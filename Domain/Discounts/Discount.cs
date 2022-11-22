using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;
using Domain.Attributes;
using Domain.Catalogs;

namespace Domain.Discounts
{
    [Audtable]
    public class Discount
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool UsePersentage { get; set; }
        public int DiscountPersentage { get; set; }
        public int DiscountAmount { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool RequieredCoponeCode { get; set; }
        public string CoponeCode { get; set; }

        [NotMapped]
        public DiscountType DiscountType
        {
            get => (DiscountType)this.DiscountTypeId;
            set => this.DiscountTypeId = (int)value;
        }
        public int DiscountTypeId { get; set; }
        public ICollection<CatalogItem> CatalogItems { get; set; }

        public int LimitationTimes { get; set; }
        [NotMapped]
        public DiscountLimitationType DiscountLimitation
        {
            get => (DiscountLimitationType)this.DiscountLimitationId;
            set => this.DiscountLimitationId = (int)value;
        }
        public int DiscountLimitationId { get; set; }

        public int GetDiscountAmount(int amount)
        {
            // اگر استفاده از درتخفیف روشن بود  مقدار ان را حساب و بر میگردانیم و
            // اگر شرط  درست نبود باید مقدار تخفیف که ست کردیم برگردانیم 
            var result = 0;
            if (UsePersentage)
            {
                result = ((amount * DiscountPersentage) / 100);
            }
            else
            {
                result = DiscountAmount;
            }

            return result;
        }
    }


    public enum DiscountType
    {
        [Display(Name = "تخفیف برای محصولات")]
        AssignedProduct = 1,
        [Display(Name = "تخفیف برای دسته بندی")]
        AssignedToCategories = 2,
        [Display(Name = "تخفیف برای مشتری")]
        AssignedToUser = 3,
        [Display(Name = "تخفیف برای برند")]
        AssignedToBrand = 3,
    }
   
    ///  محدودیت تعداد استفاده
    public enum DiscountLimitationType
    {
 
        [Display(Name = "بدونه محدودیت تعداد")]
        Unlimited = 0,
 
        [Display(Name = "فقط N بار")]
        NTimesOnly = 1,
 
        [Display(Name = "N بار به ازای هر مشتری")]
        NTimesPerCustomer = 2,
    }
}