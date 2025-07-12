using BussinessObject;
using DataAccessLayer;

namespace Repository
{
    public class UserRepository
    {
        public List<User> GetUsers() => UserDAO.Instance.GetUsers();

        public User GetUserById(int userId) => UserDAO.Instance.GetUseryId(userId);

        public User GetUserByEmailAndPassword(string email, string password) =>
            UserDAO.Instance.GetUserByEmailAndPassword(email, password);

        public User GetUserByEmail(string email) => UserDAO.Instance.GetUserByEmail(email);

        public User GetUserByGoogleId(string googleId) => UserDAO.Instance.GetUserByGoogleId(googleId);

        public bool UpdateUser(User user) => UserDAO.Instance.UpdateUser(user);

        public bool CreateUser(User user) => UserDAO.Instance.CreateUser(user);

        public User CreateGoogleUser(string googleId, string email, string name, string picture) =>
            UserDAO.Instance.CreateGoogleUser(googleId, email, name, picture);
    }
}
