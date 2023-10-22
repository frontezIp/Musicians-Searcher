using Hangfire.Annotations;
using Hangfire.Dashboard;

namespace Chat.API.Filters
{
    public class DashboardAuthorizationFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize([NotNull] DashboardContext context) => true;
    }
}
