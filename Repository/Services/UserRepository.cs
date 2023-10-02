using Repository.EFModels;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Services
{
    public class UserRepository : IUserRepository
    {
        private readonly PropertyDatabaseContext _context;
        public UserRepository()
        {
            _context = new PropertyDatabaseContext();
        }

        public void Add(UserDTO user)
        {
            var entity = _context.Users.FirstOrDefault(e => e.Email.Equals(user.Email));
            if (entity == null)
            {
                _context.Users.Add(user.ToEntity());
            }
        }

        public void ResetPassword(string email, string password)
        {
            var user = _context.Users.FirstOrDefault(e => e.Email.Equals(email));
            if (user != null)
            {
                user.Password = password;
                _context.Users.Update(user);
            }
        }

        public void Delete(string email)
        {
            var user = _context.Users.FirstOrDefault(e => e.Email.Equals(email));
            if (user != null)
            {
                _context.Users.Remove(user);
            }
        }

        public List<UserDTO> GetAll()
        {
            List<UserDTO> users = new List<UserDTO>();

            var userEntities = _context.Users.ToList();
            userEntities.ForEach(x => users.Add(x.ToDTO()));

            return users;
        }

        public UserDTO GetByEmail(string email)
        {
            UserDTO userDTO = default;
            var user = _context.Users.FirstOrDefault(e => e.Email.Equals(email));
            if (user != null)
            {
                userDTO = user.ToDTO();
            }
            return userDTO;
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(UserDTO user)
        {
            _context.Users.Update(user.ToEntity());
        }
        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
