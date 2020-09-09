using System;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography;
using TicketHubApp.Interfaces;
using TicketHubApp.Models;
using TicketHubApp.Models.ServiceModels;
using TicketHubDataLibrary.Models;
using static TicketHubApp.Services.RoleService;

namespace TicketHubApp.Services
{
    public class UserService
    {
        private DbContext _context;
        private IRepository<User> _userRepository;
        private IRepository<User> Repository
        {
            get
            {
                if (_context == null)
                {
                    _context = new TicketHubContext();
                }
                if (_userRepository == null)
                {
                    _userRepository = new GenericRepository<User>(_context);
                }
                return _userRepository;
            }
        }
        public int SaltLength { get; private set; }
        public int HashLength { get; private set; }
        public int Iteration { get; private set; }
        public UserService() : this(null)
        {
        }
        public UserService(DbContext context) : this(256, 256, 5, context)
        {
        }
        public UserService(int saltLength, int hasLength, int iteration, DbContext context)
        {
            SaltLength = saltLength;
            HashLength = hasLength;
            Iteration = iteration;
            _context = context;
        }
        /// <summary>
        /// 查詢使用者是否存在
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public bool HasUser(string email)
        {
            return Repository.GetAll().Any(x => x.Email == email);
        }

        /// <summary>
        /// 取得使用者
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public User GetUser(string email)
        {
            return Repository.GetAll().Include(x => x.Roles).First(x => x.Email == email);
        }
        public User GetUser(Guid id)
        {
            return Repository.GetAll().Include(x => x.Roles).First(x => x.Id == id);
        }
        public User AddUser(User user)
        {
            Repository.Create(user);
            return GetUser(user.Email);
        }
        /// <summary>
        /// 新增使用者角色
        /// </summary>
        /// <param name="email"></param>
        /// <param name="roleType"></param>
        /// <returns></returns>
        public void AddUserWithRole(string email, Roles roleType)
        {
            User user = GetUser(email);
            AddUserWithRole(user, roleType);
        }
        /// <summary>
        /// 新增使用者角色
        /// </summary>
        /// <param name="user"></param>
        /// <param name="roleType"></param>
        /// <returns></returns>
        public void AddUserWithRole(User user, Roles roleType)
        {
            Role role = new RoleService(_context).GetRole(roleType);
            user = GetUser(user.Id); //get user again with same context
            user.Roles.Add(role);
            Repository.SaveChanges();
        }
        /// <summary>
        /// 使用者是否有角色
        /// </summary>
        /// <param name="user"></param>
        /// <param name="roleType"></param>
        /// <returns></returns>
        public bool UserHasRole(User user, Roles roleType)
        {
            string roleName = new RoleService(_context).GetRoleName(roleType);
            user = GetUser(user.Id); //get user again with same context
            return user.Roles.Any(r => r.Name == roleName);
        }

        public UserToken GenerateUserToken(string password)
        {
            var salt = GenerateSalt(SaltLength);
            var hash = GenerateHash(GetByteArrayFromString(password), salt, Iteration, HashLength);

            UserToken token = new UserToken
            {
                PasswordSalt = GetStringFromByte(salt),
                PasswordHash = GetStringFromByte(hash),
                PasswordWorkFactor = Iteration
            };

            return token;
        }

        public bool IsValidUser(User user, string password)
        {
            if (user != null)
            {
                var hash = GenerateHash(GetByteArrayFromString(password), GetByteArrayFromString(user.PasswordSalt), Iteration, HashLength);
                var hasString = GetStringFromByte(hash);
                return user.PasswordHash.Equals(hasString);
            }
            return false;
        }

        private byte[] GenerateSalt(int length)
        {
            var bytes = new byte[length];

            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(bytes);
            }

            return bytes;
        }
        private byte[] GenerateHash(byte[] password, byte[] salt, int iterations, int length)
        {
            using (var deriveBytes = new Rfc2898DeriveBytes(password, salt, iterations))
            {
                return deriveBytes.GetBytes(length);
            }
        }

        private byte[] GetByteArrayFromString(string text)
        {
            return Convert.FromBase64String(text);
        }

        private string GetStringFromByte(byte[] byteArray)
        {
            return Convert.ToBase64String(byteArray);
        }
    }
}
