using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Bookie.data.entities;
using Bookie.data.dtos.Comments;
using Bookie.data.Repositories;
using System.Linq;
using bookie.Data;
using Microsoft.AspNetCore.Authorization;
using Bookie.data.Auth.Model;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Identity;

namespace Bookie.controllers
{
    [ApiController]
    [Route("api/genres/{genreId}/books/{bookId}/comments")]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository commentRepository;
        private readonly UserManager<BookieRestUser> _userManager;
        private readonly IAuthorizationService authorizationService;
        public CommentController(ICommentRepository repo, IAuthorizationService authServise, UserManager<BookieRestUser> userManager)
        {
            commentRepository = repo;
            authorizationService=authServise;
            _userManager = userManager;
        }
        [HttpGet]
        public async Task<IEnumerable<CommentDto>> GetMany(int bookId)
        {
            var comments = await commentRepository.GetManyAsync();
            return comments.Select(x => new CommentDto(x.Id, x.BookId, DateTime.Now, x.Text, x.UserId,x.Username)).Where(y => y.BookID == bookId); 
        }

        [HttpGet]
        [Route("{commentId}")]
        public async Task<ActionResult<CommentDto>> Get(int bookId,int commentId)
        {
            var comment = await commentRepository.GetAsync(commentId, bookId);
            if (comment == null) return NotFound();
            return new CommentDto(comment.Id, comment.BookId, DateTime.Now, comment.Text,comment.UserId, comment.Username);
        }

        public async Task<ActionResult<CommentDto>> Create(CreateCommentDto createCommentDto,int bookId)
        {
            var user = _userManager.GetUserName(User);
            var comment = new Comment { Text = createCommentDto.Text, Date= DateTime.Now, Username= user, UserId= User.FindFirstValue(JwtRegisteredClaimNames.Sub) };
   
            await commentRepository.CreateAsync(comment,bookId);

            //201
            return Created("201", new CommentDto(comment.Id, comment.BookId,comment.Date, comment.Text,comment.UserId, comment.Username));
        }

        [HttpPut]
        [Route("{commentId}")]
        [Authorize(Roles = BookieRoles.BookieUser)]
        public async Task<ActionResult<CommentDto>> Update(int commentId,int bookId, UpdateCommentDto updateCommentDto)
        {
            var comment = await commentRepository.GetAsync(commentId, bookId);
            if (comment == null) return NotFound();
            var authRez = await authorizationService.AuthorizeAsync(User, comment, PolicyNames.ResourceOwner);
            if (!authRez.Succeeded)
            {
                return Forbid();
            }
            comment.Text = updateCommentDto.Text;
            await commentRepository.UpdateAsync(comment);
            

            return Ok(new CommentDto(comment.Id, comment.BookId, DateTime.Now, comment.Text,comment.UserId, comment.Username));
        }

        [HttpDelete]
        [Route("{commentId}")]
        public async Task<ActionResult> Remove(int commentId,int bookId)
        {
            var comment = await commentRepository.GetAsync(commentId, bookId);
            if (comment == null) return NotFound();
            await commentRepository.DeleteAsync(comment);

            //204
            return NoContent();
        }

        private string? CreateGenresResourceUri(
            CommentsSearchParameters commentsSearchParametersDto,
            ResourceUriType type)
        {
            return type switch
            {
                ResourceUriType.PreviousPage => Url.Link("GetComments",
                    new
                    {
                        pageNumber = commentsSearchParametersDto.pageNumber - 1,
                        pageSize = commentsSearchParametersDto.PageSize,
                    }),
                ResourceUriType.NextPage => Url.Link("GetComments",
                    new
                    {
                        pageNumber = commentsSearchParametersDto.pageNumber + 1,
                        pageSize = commentsSearchParametersDto.PageSize,
                    }),
                _ => Url.Link("GetComments",
                    new
                    {
                        pageNumber = commentsSearchParametersDto.pageNumber,
                        pageSize = commentsSearchParametersDto.PageSize,
                    })
            };
        }
    }
}
