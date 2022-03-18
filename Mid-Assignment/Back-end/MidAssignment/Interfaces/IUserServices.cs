using MidAssignment.DTO;
using MidAssignment.Entities;

namespace MidAssignment.Interfaces
{
    public interface IUserServices
    {
        public User Authenticate(UserAuthenticationDTO userDTO);

        public string Hi(){
            var hi = "hi";
            return hi;
        }
    }
}