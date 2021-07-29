using LikeFinal_Version.Models;
using LikeVersionNew.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Like.Models
{
    public class UserRepo: IUserRepo<Users>
    {
        private readonly ContextData myDB;

        public UserRepo(ContextData MyDB)
        {
            myDB = MyDB;
        }
        public List<Users> GetAllUsers()
        {
            return myDB.Users.ToList();
        }
        public void AddUser(Users User)
        {
            myDB.Users.Add(User);
            myDB.SaveChanges();
        }
        public Users FindUserByName(string UserNmae)
        {
            return myDB.Users.Where(x => x.UserName == UserNmae).SingleOrDefault();
        }
        public Users FindUserByID(int id)
        {
            return myDB.Users.Find(id);
        }
        public void RemoveUser(int id)
        {
            myDB.Users.Remove(FindUserByID(id));
            myDB.SaveChanges();
        }
        public void EditUserData(Users Data)
        {
            Users searcheduser = FindUserByID(Data.UserID);
            searcheduser.UserPassword = Data.UserPassword;
            myDB.Users.Update(searcheduser);
            myDB.SaveChanges();
        }

        public Users FindUserByEmail(string Email)
        {
            return myDB.Users.Where(x => x.UserEmail == Email).SingleOrDefault();
        }
    }
}
