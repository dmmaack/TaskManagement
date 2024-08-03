using System.Security.Principal;
using System.Security.Claims;
using TaskManagement.Domain.DTO.UsersDTOs;

namespace TaskManagement.Api.Auth
{
    public static class AuthControl
    {
        public static LoggedUserDTO GetLoggedUser(ClaimsIdentity identity)
        {
            if (identity == null)
                return new LoggedUserDTO();

            List<Claim> claims = identity.Claims.ToList();

            return new LoggedUserDTO( 
                name: claims.Where(c => c.Type == ClaimTypes.Name).Select(c => c.Value).FirstOrDefault(),
                userRule: int.Parse(claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).FirstOrDefault()),
                id: long.Parse(claims.Where(c => c.Type == ClaimTypes.NameIdentifier).Select(c => c.Value).FirstOrDefault())
                );            
        }
    }
}