using System;
using Deneme6.Models;

namespace Deneme6.Services
{
    public interface IUserAction
    {
        void AddUser(User user);
        User GetUser(string mail, string password);
        void UpdateUser(User user);
        List<User> ListUsers();
        void DeactivateUser(int userId);
    }

}

