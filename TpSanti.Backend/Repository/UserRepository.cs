using Microsoft.EntityFrameworkCore;
using TpSanti.Backend.Data;
using TpSanti.Backend.Data.Context;
using TpSanti.Backend.Models;
using TpSanti.Backend.Repository.IRepository;

namespace TpSanti.Backend.Repository
{
    public class UserRepository : IUserRepository
    {

        ApplicationDbContext _db;

        public UserRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public User Login(string username, string password)
        {
            var usuario = _db.User.FirstOrDefault(u => EF.Functions.Collate(u.Username, "SQL_Latin1_General_CP1_CS_AS") == username);
            var u = _db.User.FirstOrDefault(u => u.Username == username);

            if (usuario == null)
            {
                return null;
            }

            if (!ValidatePassword(password, usuario.Password))
            {
                return null;
            }

            return usuario;
        }

        private bool ValidatePassword(string password1, string password2)
        {
            if (password1 == password2)
            {
                return true;
            }
            return false;
        }


    }
}
