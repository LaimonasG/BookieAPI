using Bookie.data.entities;
using Bookie.data.dtos.Books;
using Bookie.data.Repositories;
using bookie.Data;
using Bookie.data.Auth.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using static Bookie.data.dtos.Baskets.BasketsDto;

namespace Bookie.controllers
{
    [ApiController]
    [Route("api/baskets")]
    public class BasketController : ControllerBase
    {
        private readonly IBasketRepository basketRepository;
        private readonly IBookRepository bookRepository;
        private readonly IAuthorizationService authorizationService;

        public BasketController(IBasketRepository repo, IAuthorizationService authServise, IBookRepository bookRep)
        {
            bookRepository = bookRep;
            basketRepository = repo;
            authorizationService = authServise;
        }

        [HttpGet]
        [Authorize(Roles = BookieRoles.BookieUser)]
        public async Task<ActionResult<BasketDto>> Get()
        {
            var userId = User.FindFirstValue(JwtRegisteredClaimNames.Sub);
            var basket = await basketRepository.GetAsync(userId);
            if (basket == null) return NotFound();
            return new BasketDto(basket.Id,basket.UserId,basket.Books);
        }

        [HttpPost]
        [Authorize(Roles = BookieRoles.BookieUser)]
        public async Task<ActionResult<BasketDto>> Create()
        {
            var basket = new Basket { UserId = User.FindFirstValue(JwtRegisteredClaimNames.Sub)};
            //check if basket already exists
            await basketRepository.CreateBasketIfNone(basket,basket.UserId);

            //201
            return Created("201", new BasketDto(basket.Id,basket.UserId,basket.Books));
        }

        [HttpPut]
        [Authorize(Roles = BookieRoles.BookieUser)]
        public async Task<ActionResult<BasketDto>> AddBook(AddBookToBasketDto dto)
        {
            var book = await bookRepository.GetAsync(dto.bookId, dto.genreId);
            var userId = User.FindFirstValue(JwtRegisteredClaimNames.Sub);
            var basket = await basketRepository.GetAsync(userId);
            if (basket == null) return NotFound();
            if (book == null) return NotFound();
            if (basket.Books == null)
            {
                basket.Books= new List<Book>();
            }

            if (!basket.Books.Contains(book))
            {
                basket.Books.Add(book);
            }

            await basketRepository.UpdateAsync(basket);

            return Ok(new BasketDto(basket.Id, basket.UserId, basket.Books));
        }
    }
}
