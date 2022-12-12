using Bookie.data.entities;
using Bookie.data.dtos.Books;
using Bookie.data.Repositories;
using bookie.Data;
using Bookie.data.Auth.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

namespace Bookie.controllers
{
    
    [ApiController]
    [Route ("api/genres/{genreId}/books")]
    public class BooksController : ControllerBase
    {
        private readonly IBookRepository bookRepository;
        private readonly IAuthorizationService authorizationService;

        public BooksController(IBookRepository repo,IAuthorizationService authServise)
        {
            bookRepository = repo;
            authorizationService = authServise;
        }
        [HttpGet]
        public async Task<IEnumerable<BookDto>> GetMany(int genreId)
        {
            var books = await bookRepository.GetManyAsync();
            return books.Select(x => new BookDto(x.Id, x.Name, x.GenreId, x.Author,x.Price,x.Quality, DateTime.Now,x.UserId)).Where(y => y.genreID == genreId);
        }

        [HttpGet]
        [Route("{bookId}")]
        public async Task<ActionResult<BookDto>> Get(int bookId, int genreId)
        {
            var book = await bookRepository.GetAsync(bookId, genreId);
            if (book == null) return NotFound();
            return new BookDto(book.Id,book.Name,book.GenreId, book.Author,book.Price,book.Quality, DateTime.Now,book.UserId);
        }

        [HttpPost]
        [Authorize(Roles=BookieRoles.BookieUser)]
        public async Task<ActionResult<BookDto>> Create(CreateBookDto createBookDto,int genreId)
        {
            var book = new Book { Name = createBookDto.name, Author = createBookDto.author,Price=createBookDto.price,Quality=createBookDto.quality, UserId = User.FindFirstValue(JwtRegisteredClaimNames.Sub) };
            await bookRepository.CreateAsync(book,genreId);

            //201
            return Created("201", new BookDto(book.Id,book.Name,book.GenreId, book.Author,book.Price,book.Quality, DateTime.Now,book.UserId));
        }

        [HttpPut]
        [Route("{bookId}")]
        [Authorize(Roles = BookieRoles.BookieUser)]
        public async Task<ActionResult<BookDto>> Update(int bookId, int genreId, UpdateBookDto updateBookDto)
        {
            var book = await bookRepository.GetAsync(bookId,genreId);
            if (book == null) return NotFound();
            var authRez=await authorizationService.AuthorizeAsync(User, book, PolicyNames.ResourceOwner);
            if (!authRez.Succeeded)
            {
                return Forbid();
            }
            book.Name = updateBookDto.name;
            await bookRepository.UpdateAsync(book);

            return Ok(new BookDto(book.Id, book.Name, book.GenreId, book.Author,book.Price,book.Quality, DateTime.Now,book.UserId));
        }

        [HttpDelete]
        [Route("{bookId}")]
        public async Task<ActionResult> Remove(int bookId,int genreId)
        {
            var book = await bookRepository.GetAsync(bookId,genreId);
            if (book == null) return NotFound();
            await bookRepository.DeleteAsync(book);

            //204
            return NoContent();
        }

        //private IEnumerable<LinkDto> CreateLinksForTopic(int commentId)
        //{
        //    yield return new LinkDto { Href = Url.Link("GetBook", new { commentId }), Rel = "self", Method = "GET" };
        //    yield return new LinkDto { Href = Url.Link("DeleteBook", new { commentId }), Rel = "delete_book", Method = "DELETE" };
        //}

        private string? CreateGenresResourceUri(
            BooksSearchParameters commentsSearchParametersDto,
            ResourceUriType type)
        {
            return type switch
            {
                ResourceUriType.PreviousPage => Url.Link("GetBooks",
                    new
                    {
                        pageNumber = commentsSearchParametersDto.pageNumber - 1,
                        pageSize = commentsSearchParametersDto.PageSize,
                    }),
                ResourceUriType.NextPage => Url.Link("GetBooks",
                    new
                    {
                        pageNumber = commentsSearchParametersDto.pageNumber + 1,
                        pageSize = commentsSearchParametersDto.PageSize,
                    }),
                _ => Url.Link("GetBooks",
                    new
                    {
                        pageNumber = commentsSearchParametersDto.pageNumber,
                        pageSize = commentsSearchParametersDto.PageSize,
                    })
            };
        }


    }
}
