using BussinessObject;
using DataAccessLayer;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class UserRepository
    {
        public List<User> GetUsers() => UserDAO.Instance.GetUsers();
        public User GetUserById(int userId) => UserDAO.Instance.GetUseryId(userId);
        public User GetUserByEmailAndPassword(string email, string password) => UserDAO.Instance.GetUserByEmailAndPassword(email, password);

        public bool UpdateUser(User user) => UserDAO.Instance.UpdateUser(user);
       
    }
}
