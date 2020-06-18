using System.Web.Http;
using Microsoft.AspNet.Identity;
using WebformsIntegratedBE_Standalone.Models;

namespace WebformsIntegratedBE_Standalone.ApiControllers
{
    [RoutePrefix("api/user")]
    public class UserController : ApiController
    {
        #region Methods
        [HttpGet]
        [Authorize]
        [Route("GenerateToken")]
        public string GenerateToken()
        {
            var username = User.Identity.GetUserName();
            var tenantName = ((System.Security.Claims.ClaimsIdentity)User.Identity).FindFirstValue("tenantName");

            var user = new UserInfo { UserName = username, TenantUniqueName = tenantName };
            var token = IzendaBoundary.IzendaTokenAuthorization.GetToken(user);

            return token;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("validateIzendaAuthToken")]
        public UserInfo ValidateIzendaAuthToken(string access_token)
        {
            var user = IzendaBoundary.IzendaTokenAuthorization.GetUserInfo(access_token);

            return user;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("GetIzendaAccessToken")]
        public IHttpActionResult GetIzendaAccessToken(string message)
        {
            var userInfo = IzendaBoundary.IzendaTokenAuthorization.DecryptIzendaAuthenticationMessage(message);
            var token = IzendaBoundary.IzendaTokenAuthorization.GetToken(userInfo);

            return Ok(new { Token = token });
        } 
        #endregion
    }
}
