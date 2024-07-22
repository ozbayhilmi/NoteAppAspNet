using System;
using Deneme6.Models;

namespace Deneme6.Services
{
    public class UserAction : IUserAction
    {
        private string userFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "NoteApp", "users.txt");

        public void AddUser(User user)
        {
            user.UserId = GetNewUserId();
            var userData = $"{user.UserId}#{user.FirstName}#{user.LastName}#{user.MailAddress}#{user.Password}#{user.IsAdmin}#{user.IsActive}";
            File.AppendAllText(userFilePath, userData + Environment.NewLine);
        }

        public User GetUser(string mail, string password)
        {
            var users = File.ReadAllLines(userFilePath);
            foreach (var userLine in users)
            {
                var userProps = userLine.Split('#');
                if (userProps[3] == mail && userProps[4] == password)
                {
                    return new User
                    {
                        UserId = int.Parse(userProps[0]),
                        FirstName = userProps[1],
                        LastName = userProps[2],
                        MailAddress = userProps[3],
                        Password = userProps[4],
                        IsAdmin = bool.Parse(userProps[5]),
                        IsActive = bool.Parse(userProps[6])
                    };
                }
            }
            return null;
        }

        public void UpdateUser(User user)
        {
            var users = File.ReadAllLines(userFilePath).ToList();
            for (int i = 0; i < users.Count; i++)
            {
                var userProps = users[i].Split('#');
                if (int.Parse(userProps[0]) == user.UserId)
                {
                    users[i] = $"{user.UserId}#{user.FirstName}#{user.LastName}#{user.MailAddress}#{user.Password}#{user.IsAdmin}#{user.IsActive}";
                    break;
                }
            }
            File.WriteAllLines(userFilePath, users);
        }

        public List<User> ListUsers()
        {
            var users = File.ReadAllLines(userFilePath);
            return users.Select(userLine =>
            {
                var userProps = userLine.Split('#');
                return new User
                {
                    UserId = int.Parse(userProps[0]),
                    FirstName = userProps[1],
                    LastName = userProps[2],
                    MailAddress = userProps[3],
                    Password = userProps[4],
                    IsAdmin = bool.Parse(userProps[5]),
                    IsActive = bool.Parse(userProps[6])
                };
            }).ToList();
        }

        public void DeactivateUser(int userId)
        {
            var users = File.ReadAllLines(userFilePath).ToList();
            for (int i = 0; i < users.Count; i++)
            {
                var userProps = users[i].Split('#');
                if (int.Parse(userProps[0]) == userId)
                {
                    userProps[6] = "false";
                    users[i] = string.Join("#", userProps);
                    break;
                }
            }
            File.WriteAllLines(userFilePath, users);
        }

        private int GetNewUserId()
        {
            var users = File.ReadAllLines(userFilePath);
            if (users.Length == 0)
                return 1;
            return users.Select(u => int.Parse(u.Split('#')[0])).Max() + 1;
        }
    }


}

