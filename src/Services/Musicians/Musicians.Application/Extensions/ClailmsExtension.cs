using Musicians.Domain.Exceptions;
using System.Security.Claims;

namespace Musicians.Application.Extensions
{
    public static class ClailmsExtension
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
