using System;
using System.Data.Entity;
using System.Linq;
using TicketHubApp.Interfaces;
using TicketHubApp.Models;
using TicketHubDataLibrary.Models;

namespace TicketHubApp.Services
{
    public class RoleManager
    {
        public enum Roles
        {
            Customer,
            ShopOwner,
            ShopEmployee
        }
        private DbContext _context;
        private IRepository<Role> _userRepository;
        private IRepository<Role> Repository
        {
            get
            {
                if (_context == null)
                {
                    _context = new TicketHubContext();
                }
                if (_userRepository == null)
                {
                    _userRepository = new GenericRepository<Role>(_context);
                }
                return _userRepository;
            }
        }
        public RoleManager()
        {
        }
        public RoleManager(DbContext context)
        {
            _context = context;
        }
        public bool HasRole(string name)
        {
            return Repository.GetAll().Any(x => x.Name == name);
        }

        public Role GetRole(Guid id)
        {
            return Repository.GetAll().First(x => x.Id == id);
        }
        public Role GetRole(Roles roleType)
        {
            return GetRole(Enum.GetName(typeof(Roles), roleType));
        }

        public Role GetRole(string name)
        {
            return Repository.GetAll().First(x => x.Name == name);
        }

        public Guid GetRoleId(string name)
        {
            return GetRole(name).Id;
        }
        public Guid GetRoleId(Roles roleType)
        {
            return GetRole(roleType).Id;
        }

        public string GetRoleName(Guid id)
        {
            return GetRole(id).Name;
        }
        public string GetRoleName(Roles roleType)
        {
            return GetRole(roleType).Name;
        }

        public Role Create(string name)
        {
            bool hasRole = HasRole(name);
            if (hasRole)
            {
                throw new ArgumentException($"Already has role: {name}");
            }
            Role role = new Role
            {
                Name = name
            };
            Repository.Create(role);
            return GetRole(name);
        }
    }
}
