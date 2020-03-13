using Site.BLL.DTO;
using Site.BLL.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Site.BLL.Interfaces
{
    public interface IUserService:IDisposable
    {
        Task<OperationDetails> Create(UserDTO userDTO);
        Task<ClaimsIdentity> Authenticate(UserDTO userDTO);
        Task SetInitialData(UserDTO adminDTO, List<string> roles);
        IEnumerable<UserDTO> GetUsers();
        Task<OperationDetails> Update(UserDTO userDTO);
    }
}
