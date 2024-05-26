using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace hauzio.webapi.Security
{

    [Route("api/[controller]")]
    public class JwtBaseController: Controller
    {
        public string userID
        {

            get
            {
                return User.FindFirst(ClaimsIdentity.DefaultNameClaimType)?.Value ?? string.Empty;
            }
        }

        
    }
}
