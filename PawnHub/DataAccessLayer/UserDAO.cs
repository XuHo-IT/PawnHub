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

        public User GetUserByEmail(string email)
        {
            return _context.User.FirstOrDefault(u => u.EmailAddress == email);
        }

        public User GetUserByGoogleId(string googleId)
        {
            return _context.User.FirstOrDefault(u => u.GoogleId == googleId);
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
                existingUser.GoogleId = user.GoogleId;
                existingUser.ProfilePicture = user.ProfilePicture;
                existingUser.IsGoogleAccount = user.IsGoogleAccount;
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public bool CreateUser(User user)
        {
            try
            {
                _context.User.Add(user);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public User CreateGoogleUser(string googleId, string email, string name, string picture)
        {
            var user = new User
            {
                GoogleId = googleId,
                EmailAddress = email,
                UserRealName = name,
                UserName = email.Split('@')[0], // Lấy phần trước @ làm username
                ProfilePicture = picture,
                IsGoogleAccount = true,
                UserRole = 2, // 2 = Customer/Member (không phải Admin)
                // Các trường không bắt buộc sẽ để null
                Telephone = null,
                Gender = null,
                Address = null,
                CID = null,
                Password = null,
                Dob = null
            };

            _context.User.Add(user);
            _context.SaveChanges();
            return user;
        }

        public bool InsertUser(User user)
        {
            try
            {
                _context.User.Add(user);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public User GetUserByEmail(string email)
        {
            return _context.User.FirstOrDefault(u => u.EmailAddress.ToLower() == email.ToLower());
        }

        public User GetUserByUsername(string username)
        {
            return _context.User.FirstOrDefault(u => u.UserName.ToLower() == username.ToLower());
        }
    }
}
