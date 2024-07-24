using System;
using Deneme6.Models;

namespace NoteApp.DataAccess.Context
{
	public class UserRepository : GenericRepository<User, BaseDbContext>, IUserRepository
	{

        public UserRepository(BaseDbContext context) : base(context)
        {
        }
    }
}

// BaseDbContext dışındakileri bir başka klasöre aktar.

// Repository