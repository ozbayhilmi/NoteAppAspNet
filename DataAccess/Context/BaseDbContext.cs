using System;
using Microsoft.EntityFrameworkCore;

namespace NoteApp.DataAccess.Context
{
	public class BaseDbContext : DbContext
	{
		public BaseDbContext(DbContextOptions<BaseDbContext> options) : base(options)
		{
		}
	}
}

