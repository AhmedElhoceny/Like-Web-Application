using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LikeVersionNew.Models
{
    interface IUserRepo<T>
    {
        List<T> GetAllUsers();
        void AddUser(T User);
        T FindUserByName(string UserNmae);
        T FindUserByID(int id);
        T FindUserByEmail(string Email);
        void RemoveUser(int id);
        void EditUserData(T Data);
    }
}
