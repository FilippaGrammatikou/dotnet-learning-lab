using BookReviewApp.DTO;
using BookReviewApp.Interfaces;
using BookReviewApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookReviewApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        readonly ICountryRepository _countryRepository;
        public CountriesController(ICountryRepository countryRepository)
        {
            _countryRepository = countryRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<CountryDto>))]
        public IActionResult GetCountries()
        {
            var countries = _countryRepository.GetCountries();
            var countryDtos = countries.Select(c => new CountryDto
            {
                Id = c.Id,
                Name = c.Name
            }).ToList();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(countries);
        }

        [HttpGet("{countryId}")]
        [ProducesResponseType(200, Type = typeof(CountryDto))]
        [ProducesResponseType(400)]
        public IActionResult GetCountry(int countryId)
        {
            if (!_countryRepository.CountryExists(countryId))
                return BadRequest("Country not found.");

            var country = _countryRepository.GetCountry(countryId);
            var countryDto = new CountryDto
            {
                Id = country.Id,
                Name = country.Name
            };
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(country);
        }

        [HttpGet("author/{authorId}")]
        [ProducesResponseType(200, Type = typeof(CountryDto))]
        [ProducesResponseType(400)]
        public IActionResult GetCountryByAuthor(int authorId)
        {
            var country = _countryRepository.GetCountryByAuthor(authorId);
            var countryDto = new CountryDto
            {
                Id = country.Id,
                Name = country.Name
            };
            if (country == null)
                return NotFound("Author not found or has no country assigned");
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(country);
        }

        [HttpGet("{countryId}/authors")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Author>))]
        [ProducesResponseType(400)]
        public IActionResult GetAuthorsFromCountry(int countryId)
        {
            if (!_countryRepository.CountryExists(countryId))
                return BadRequest("Country not found.");
            var authors = _countryRepository.GetAuthorsFromCountry(countryId);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(authors);
        }

        [HttpPut("{countryId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult UpdateCountry(int countryId, [FromBody] Country updatedCountry)
        {
            if (updatedCountry == null)
                return BadRequest("Country data is null.");
            if (countryId != updatedCountry.Id)
                return BadRequest("Country ID mismatch.");
            if (!_countryRepository.CountryExists(countryId))
                return BadRequest("Country not found.");
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var success = _countryRepository.UpdateCountry(updatedCountry);
            if (!success)
                return StatusCode(500, "An error occurred while updating the country.");
            return NoContent();
        }

    }
}
