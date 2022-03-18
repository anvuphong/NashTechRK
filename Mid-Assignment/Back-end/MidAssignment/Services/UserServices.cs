using MidAssignment.Data;
using MidAssignment.DTO;
using MidAssignment.Entities;
using MidAssignment.Interfaces;

namespace MidAssignment.Services
{
    public class UserServices : IUserServices
    {
        private readonly LibraryContext _context;
        public UserServices(LibraryContext context)
        {
            _context = context;
        }
        public User Authenticate(UserAuthenticationDTO userDTO)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserName.ToLower().Equals(userDTO.UserName) && u.Password.Equals(userDTO.Password));
            return user;
        }
    }
}