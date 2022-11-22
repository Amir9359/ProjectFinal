using Application.Catalogs.CatalogItems.GetCatalogItemPDP;
using Application.Catalogs.CatalogItems.GetCatalogItemPLP;
using Application.Comments.Comment;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.EndPoint.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
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

        [HttpGet]
        public IActionResult Get([FromQuery] CatlogPLPRequestDto result)
        {
            return Ok(_catalogItemService.Execute(result));
        }

        [HttpGet]
        [Route("PDP")]
        public IActionResult Get([FromQuery] string slug)
        {
            return Ok(_catalogItemPdpService.Execute(slug));
        }

        [HttpPost]
        public IActionResult Post([FromBody] CommentDto comment)
        {
            var sendComment = new SendCommentCommand(comment);
            var result = _mediator.Send(sendComment).Result;
            return Ok(result);
        }
    }
}
