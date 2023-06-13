using TpSanti.Backend.Models;

namespace TpSanti.Backend.Repository.IRepository
{
    public interface IUserRepository
    {
        public User Login(string username, string password);
    }
}
