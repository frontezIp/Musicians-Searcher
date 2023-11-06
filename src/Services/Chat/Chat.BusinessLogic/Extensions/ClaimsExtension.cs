using Chat.BusinessLogic.Exceptions;
using System.Security.Claims;

namespace Chat.BusinessLogic.Extensions
{
    public static class ClaimsExtension
    {
        public static Guid GetUserIdFromClaims(this ClaimsPrincipal principal)
        {
            if (principal == null)
                throw new NotAuthenticatedException();

            var claim = principal.FindFirst(ClaimTypes.NameIdentifier);

            return new Guid(claim!.Value);
        }
    }
}
