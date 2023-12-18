using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.Dto;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly ICountryRepository _countryRepository;
        private readonly IMapper _mapper;
        
        public CountryController(ICountryRepository countryRepository, IMapper mapper)
        {
            _countryRepository = countryRepository;
            _mapper = mapper;
        }   
        /// <summary>
        /// 获取所有国家
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Country>))]
        public IActionResult GetCountries()
        {
            var countries = _mapper.Map<List<CountryDto>>(_countryRepository.GetCountries());
            
            if (!ModelState.IsValid)
                return BadRequest();
            
            return Ok(countries);
        }
        /// <summary>
        /// 根据国家id获取国家
        /// </summary>
        /// <param name="countryId"></param>
        /// <returns></returns>
        [HttpGet("{countryId:int}")]
        [ProducesResponseType(200, Type = typeof(Country))]
        [ProducesResponseType(400)]
        public IActionResult GetCountry(int countryId)
        {
            if (!_countryRepository.CountryExists(countryId))
                return NotFound();
            
            var country = _mapper.Map<CountryDto>(_countryRepository.GetCountry(countryId));
            
            if (!ModelState.IsValid)
                return BadRequest();
            
            return Ok(country);
        }
        /// <summary>
        /// 根据拥有者id获取国家
        /// </summary>
        /// <param name="ownerId"></param>
        /// <returns></returns>
        [HttpGet("owner/{ownerId:int}")]
        [ProducesResponseType(200, Type = typeof(Country))]
        public IActionResult GetCountryOfAOwner(int ownerId)
        {
            var country = _mapper.Map<CountryDto>(_countryRepository.GetCountryByOwner(ownerId));
            
            if (!ModelState.IsValid)
                return BadRequest();
            
            return Ok(country);
        }
    }
}