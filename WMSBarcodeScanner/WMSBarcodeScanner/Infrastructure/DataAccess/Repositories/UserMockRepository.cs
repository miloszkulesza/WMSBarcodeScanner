using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WMSBarcodeScanner.Infrastructure.DataAccess.Interfaces;
using WMSBarcodeScanner.Models;

namespace WMSBarcodeScanner.Infrastructure.DataAccess.Repositories
{
    public class UserMockRepository : IUserRepository
    {
        private readonly List<User> Users;

        public UserMockRepository()
        {
            Users = new List<User>()
            {
                new User
                {
                    Id = Guid.NewGuid().ToString(),
                    FirstName = "Miłosz",
                    LastName = "Kulesza",
                    Username = "mkulesza",
                    Password = "admin"
                }
            };
        }

        public async Task<bool> AddUserAsync(User User)
        {
            Users.Add(User);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateUserAsync(User user)
        {
            var oldUser = Users.Where((arg) => arg.Id == user.Id).FirstOrDefault();
            Users.Remove(oldUser);
            Users.Add(user);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteUserAsync(string id)
        {
            var oldUser = Users.Where((arg) => arg.Id == id).FirstOrDefault();
            Users.Remove(oldUser);

            return await Task.FromResult(true);
        }

        public async Task<User> GetUserByIdAsync(string id)
        {
            return await Task.FromResult(Users.FirstOrDefault(arg => arg.Id == id));
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            return await Task.FromResult(Users);
        }

        public async Task<User> LoginAsync(string username, string password)
        {
            var user = Users.FirstOrDefault(x => x.Username == username && x.Password == password);
            return await Task.FromResult(user);
        }
    }
}
