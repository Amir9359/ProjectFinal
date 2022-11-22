using System.Threading;
using System.Threading.Tasks;
using Application.Catalogs.CatalogItems.CatalogItemComments;
using Application.Interfaces.Contexts;
using Domain.Catalogs;
using MediatR;

namespace Application.Comments.Comment
{
    public class SendCommentCommand : IRequest<SendCommentResponseDto>
    {
        public SendCommentCommand(CommentDto commentDto)
        {
            Comment = commentDto;
        }
        public CommentDto Comment { get; set; }
    }

    public class SendCommentHandler : IRequestHandler<SendCommentCommand, SendCommentResponseDto>
    {
        private readonly IDatabaseContext _context;

        public SendCommentHandler(IDatabaseContext context )
        {
            _context = context;
        }

        public Task<SendCommentResponseDto> Handle(SendCommentCommand request, CancellationToken cancellationToken)
        {
            var CatalogItem = _context.CatalogItems.Find(request.Comment.CatalogItemId);
            CatalogItemComment comment = new CatalogItemComment
            {
                Comment = request.Comment.Comment,
                Email = request.Comment.Email,
                Title = request.Comment.Title,
                CatalogItem = CatalogItem,
            };
            var entity = _context.CatalogItemComments.Add(comment);
            _context.SaveChanges();

            return Task.FromResult(new SendCommentResponseDto()
                {  Id = entity.Entity.Id  });
        }
    }


    public class SendCommentResponseDto
    {
        public int Id { get; set; }
    }
    public class CommentDto
    {
        public string Title { get; set; }
        public string Comment { get; set; }
        public string Email { get; set; }
        public int CatalogItemId { get; set; }
    }
}