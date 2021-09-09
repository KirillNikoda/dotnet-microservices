using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlatformService.Data;
using PlatformService.Dtos;
using PlatformService.Models;

namespace PlatformService.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class PlatformController : ControllerBase
	{
		private readonly IPlatformRepo _repository;
		private readonly IMapper _mapper;
		public PlatformController(IPlatformRepo repository, IMapper mapper)
		{
			_mapper = mapper;
			_repository = repository;

		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<Platform>>> GetPlatforms()
		{
			return Ok(await _repository.GetAllPlatforms());
		}

		[HttpGet("{id}", Name = "GetPlatformById")]
		public async Task<ActionResult<Platform>> GetPlatformById(int id)
		{
			var platform = await _repository.GetPlatformById(id);

			if (platform != null)
			{
				return Ok(platform);
			}

			return NotFound();
		}

		[HttpPost]
		public async Task<ActionResult> CreatePlatform(PlatformCreateDto platformCreateDto)
		{
			var platformModel = _mapper.Map<Platform>(platformCreateDto);
			await _repository.CreatePlatform(platformModel);
			_repository.SaveChanges();
			return Ok();
		}
	}
}