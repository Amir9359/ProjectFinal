using System.Collections.Generic;
using Domain.Catalogs;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Seeds
{
    public class DatabaseContextSeed
    {
        public static void CatalogSeed(ModelBuilder modelBuilder)
        {
            foreach (var catalog in GetCatalogTypes())
            {
                modelBuilder.Entity<CatalogType>().HasData(catalog);
            }
           
        }

        private static IEnumerable<CatalogType> GetCatalogTypes()
        {
            return new List<CatalogType>()
            {
                new CatalogType() {  Id=1,  Type="کالای دیجیتال"},

                new CatalogType() {  Id= 2,  Type="لوازم جانبی گوشی" , ParentCatalogTypeId = 1},
                new CatalogType() {  Id= 3,  Type="پایه نگهدارنده گوشی" , ParentCatalogTypeId=2},
                new CatalogType() {  Id= 4,  Type="پاور بانک (شارژر همراه)", ParentCatalogTypeId=2},
                new CatalogType() {  Id= 5,  Type="کیف و کاور گوشی", ParentCatalogTypeId=2},
            };
        }

    }
}