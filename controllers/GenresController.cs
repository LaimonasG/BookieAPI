using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Bookie.data.entities;
using Bookie.data.dtos.Genres;
using Bookie.data.Repositories;
using bookie.Data;
using System.Linq;
using PagedList;
using Bookie.data.Auth.Model;
using Microsoft.AspNetCore.Authorization;

namespace Bookie.controllers
{
    [ApiController]
    [Route("api/genres")]
    public class GenresController : ControllerBase
    {
        private readonly IGenreRepository genreRepository;
        public GenresController(IGenreRepository repo)
        {
            genreRepository = repo;
        }
        [HttpGet]
        public async Task<IEnumerable<GenreDto>> GetMany()
        {
            var genres = await genreRepository.GetManyAsync();
            return genres.Select(x => new GenreDto(x.Id, Enum.GetName(typeof(GenreNames),x.Name)));
        }

       // [HttpGet(Name = "GetGenres")]
        public async Task<IEnumerable<GenreDto>> GetManyPaging([FromQuery] GenresSearchParameters parameters)
        {
            var genres = await genreRepository.GetManyAsync(parameters);
            var previousPageLink = genres.HasPrevious ?
            CreateGenresResourceUri(parameters,
                ResourceUriType.PreviousPage) : null;

            var nextPageLink = genres.HasNext ?
                CreateGenresResourceUri(parameters,
                    ResourceUriType.NextPage) : null;

            var paginationMetadata = new
            {
                totalCount = genres.TotalCount,
                pageSize = genres.PageSize,
                currentPage = genres.CurrentPage,
                totalPages = genres.TotalPages,
                previousPageLink,
                nextPageLink
            };

            // Pagination: 
            // {"resource": {}, "paging":{}}

            Response.Headers.Add("Pagination", JsonSerializer.Serialize(paginationMetadata));
            return genres.Select(x => new GenreDto(x.Id, Enum.GetName(typeof(GenreNames), x.Name)));
        }

        [HttpGet]
        [Route("{genreId}")]
        public async Task<ActionResult<GenreDto>> Get(int genreId)
        {
            var genre = await genreRepository.GetAsync(genreId);
            if (genre == null) return NotFound();
            return new GenreDto(genre.Id, Enum.GetName(typeof(GenreNames), genre.Name));
        }

        [HttpPost]
        public async Task<ActionResult<GenreDto>> Create(CreateGenreDto createGendreDto)
        {
            var genre = new Genre { Name = createGendreDto.name};
            await genreRepository.CreateAsync(genre);

            //201
            return Created("201", new GenreDto(genre.Id, Enum.GetName(typeof(GenreNames), genre.Name)));
        }

        [HttpPut]
        [Route("{genreId}")]
        [Authorize(Roles = BookieRoles.Admin)]
        public async Task<ActionResult<GenreDto>> Update(int genreId, UpdateGenreDto updateGenreDto)
        {
            var genre = await genreRepository.GetAsync(genreId);
            if (genre == null) return NotFound();
            genre.Name = updateGenreDto.name;
            await genreRepository.UpdateAsync(genre);         

            return Ok(new GenreDto(genre.Id, Enum.GetName(typeof(GenreNames), genre.Name)));
        }

        [HttpDelete]
        [Route("{genreId}")]
        public async Task<ActionResult> Remove(int genreId)
        {
            var genre = await genreRepository.GetAsync(genreId);
            if (genre == null) return NotFound();
            await genreRepository.DeleteAsync(genre);

            //204
            return NoContent();
        }

        private IEnumerable<LinkDto> CreateLinksForTopic(int genreId)
        {
            yield return new LinkDto { Href = Url.Link("GetGenre", new { genreId }), Rel = "self", Method = "GET" };
            yield return new LinkDto { Href = Url.Link("DeleteGenre", new { genreId }), Rel = "delete_genre", Method = "DELETE" };
        }

        private string? CreateGenresResourceUri(
            GenresSearchParameters genreSearchParametersDto,
            ResourceUriType type)
        {
            return type switch
            {
                ResourceUriType.PreviousPage => Url.Link("GetGenres",
                    new
                    {
                        pageNumber = genreSearchParametersDto.pageNumber - 1,
                        pageSize = genreSearchParametersDto.PageSize,
                    }),
                ResourceUriType.NextPage => Url.Link("GetGenres",
                    new
                    {
                        pageNumber = genreSearchParametersDto.pageNumber + 1,
                        pageSize = genreSearchParametersDto.PageSize,
                    }),
                _ => Url.Link("GetGenres",
                    new
                    {
                        pageNumber = genreSearchParametersDto.pageNumber,
                        pageSize = genreSearchParametersDto.PageSize,
                    })
            };
        }



    }
}
