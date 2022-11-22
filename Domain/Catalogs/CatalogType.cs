using System.Collections.ObjectModel;
using Domain.Attributes;

namespace Domain.Catalogs
{
    [Audtable]
    public class CatalogType
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public CatalogType ParentCatalogType { get; set; }
        public int? ParentCatalogTypeId { get; set; }
        public Collection<CatalogType> SubTypes{ get; set; }
    }
}