using BussinessObject;

namespace DataAccessLayer
{
    public class UserDAO
    {
        private static UserDAO? instance = null;
        private static readonly object instanceLock = new object();
        private readonly PawnShopContext _context;

        public static UserDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new UserDAO();
                    }
                    return instance;
                }
            }
        }

        public UserDAO()
        {
            _context = new PawnShopContext();
        }
        public List<User> GetUsers()
        {
            return _context.User.ToList();

        }
        public User GetUseryId(int userId)
        {

            return _context.User.FirstOrDefault(user => user.UserID == userId);
        }
        public User GetUserByEmailAndPassword(string email, string password)
        {

            return _context.User.FirstOrDefault(u => u.EmailAddress == email && u.Password == password);
        }
        public bool UpdateUser(User user)
        {
            var existingUser = _context.User.FirstOrDefault(u => u.UserID == user.UserID);
            if (existingUser != null)
            {
                existingUser.UserName = user.UserName;
                existingUser.Password = user.Password;
                existingUser.UserRealName = user.UserRealName;
                existingUser.Gender = user.Gender;
                existingUser.EmailAddress = user.EmailAddress;
                existingUser.Address = user.Address;
                existingUser.Telephone = user.Telephone;
                existingUser.Dob = user.Dob;
                existingUser.CID = user.CID;

                _context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
