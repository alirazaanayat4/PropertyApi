using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IUserRepository
    {
        List<UserDTO> GetAll();
        UserDTO GetByEmail(string email);
        void Add(UserDTO user);
        void Update(UserDTO user);
        void ResetPassword(string email, string password);
        void Delete(string email);
        void Save();
    }
}
