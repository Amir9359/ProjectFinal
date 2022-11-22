using Application.Catalogs.CatalogItems.GetCatalogItemPDP;
using Application.Catalogs.CatalogItems.GetCatalogItemPLP;
using Application.Catalogs.CatalogItems.UriComposer;
using Application.Comments.Comment;
using Application.Comments.Query;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebSite.Endpoint.Controllers
{
    public class ProductController : Controller
    {
        private readonly IGetCatalogItemPLPService _catalogItemService;
        private readonly IGetCatalogItemPDPService _catalogItemPdpService;
        private readonly IMediator _mediator;

        public ProductController(IGetCatalogItemPLPService catalogItemService, IGetCatalogItemPDPService catalogItemPdpService, IMediator mediator)
        {
            _catalogItemService = catalogItemService;
            _catalogItemPdpService = catalogItemPdpService;
            _mediator = mediator;
        }

        // GET
        public IActionResult Index(CatlogPLPRequestDto result)
        {
            var catalogs = _catalogItemService.Execute(result);

            return View(catalogs);
        }

        public IActionResult Details(string slug)
        {
            var CatalogItem = _catalogItemPdpService.Execute(slug);
            var GetComment = new GetCommentsOfCatalogItem()
                {CataLogItemId = CatalogItem.Id};
            var Result = _mediator.Send(GetComment).Result;

            return View(CatalogItem);
        }

        public IActionResult Comment(CommentDto comment, string slug)
        {
            var sendComment = new SendCommentCommand(comment);
            var result = _mediator.Send(sendComment).Result;

            return RedirectToAction("Details", new {slug = slug});
        }
    }
}