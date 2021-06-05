using System.Collections.Generic;
using System.Threading.Tasks;
using WMSBarcodeScanner.Models;

namespace WMSBarcodeScanner.Infrastructure.DataAccess.Interfaces
{
    public interface IUserRepository
    {
        Task<bool> AddUserAsync(User item);
        Task<bool> UpdateUserAsync(User item);
        Task<bool> DeleteUserAsync(string id);
        Task<User> GetUserByIdAsync(string id);
        Task<IEnumerable<User>> GetUsersAsync();
        Task<User> LoginAsync(string username, string password);
    }
}
