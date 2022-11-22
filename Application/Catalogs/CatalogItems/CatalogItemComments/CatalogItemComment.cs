using Domain.Attributes;
using Domain.Catalogs;

namespace Application.Catalogs.CatalogItems.CatalogItemComments
{
    [Audtable]
    public class CatalogItemComment
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Email { get; set; }
        public string Comment { get; set; }
        public CatalogItem CatalogItem { get; set; }
        public int CatalogItemId { get; set; }
    }
}