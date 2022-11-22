using System;

namespace Infrastructure.CashHelpers
{
    public static class CashHelper
    {
        public static string GetHomePageChashKey()
        {
            return "HomeCash";
        }

        public static readonly TimeSpan DefaultDuration = TimeSpan.FromSeconds(60);
        private static readonly string _itemsKeyTemplate = "items-{0}-{1}-{2}";


        public static string GenerateCatalogItemCacheKey(int pageIndex, int itemsPage, int? typeId)
        {
            return string.Format(_itemsKeyTemplate, pageIndex, itemsPage, typeId);
        }
    }
}