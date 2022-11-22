using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces.Contexts;
using MediatR;
using MongoDB.Driver.Linq;

namespace Application.Comments.Query
{
    public class GetCommentsOfCatalogItem : IRequest<List<GetCommentDto>>
    {
        public int CataLogItemId { get; set; }
    }
    public class GetCommentsOfCatalogItemHandler: IRequestHandler<GetCommentsOfCatalogItem , List<GetCommentDto>>
    {
        private readonly IDatabaseContext _context;

        public GetCommentsOfCatalogItemHandler(IDatabaseContext context)
        {
            _context = context;
        }

        public Task<List<GetCommentDto>> Handle(GetCommentsOfCatalogItem request, CancellationToken cancellationToken)
        {
            var comments = _context.CatalogItemComments
                .Where(s => s.CatalogItemId == request.CataLogItemId)
                .Select(p => new GetCommentDto()
                {
                    Comment = p.Comment,
                    Id = p.Id,
                    Title = p.Title,
                }).ToList();

            return Task.FromResult(comments);

        }
    }

    public class GetCommentDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Comment { get; set; }
    }
}