using Domain.Attributes;

namespace Domain.Catalogs
{
    [Audtable]
    public class CatalogBrand
    {
        public int Id { get; set; }
        public string Brand { get; set; }
    }
}