using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PlatformService.Models;

namespace PlatformService.Data
{
	public class PlatformRepo : IPlatformRepo
	{
		private readonly AppDbContext _context;
		public PlatformRepo(AppDbContext context)
		{
			_context = context;
		}

		public async Task CreatePlatform(Platform platform)
		{
			if (platform == null)
			{
				throw new ArgumentNullException(nameof(platform));
			}

			await _context.Platforms.AddAsync(platform);
		}

		public async Task<IEnumerable<Platform>> GetAllPlatforms()
		{
			return await _context.Platforms.ToListAsync();
		}

		public Task<Platform> GetPlatformById(int id)
		{
			return _context.Platforms.FirstOrDefaultAsync(p => p.Id == id);
		}

		public bool SaveChanges()
		{
			return (_context.SaveChanges() >= 0);
		}
	}
}