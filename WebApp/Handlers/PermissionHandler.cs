using Microsoft.AspNetCore.Authorization;
using na.admin.DAO;

namespace WebApp.Handlers
{
    public class AuthorizationRequirement : IAuthorizationRequirement
    {

        public AuthorizationRequirement(string permissionName)
        {
            PermissionName = permissionName;
        }

        public string PermissionName { get; }
    }

    public class PermissionHandler : AuthorizationHandler<AuthorizationRequirement>
    {
        ActionPermitionDAO action = new ActionPermitionDAO();
        private readonly IHttpContextAccessor _httpContextAccessor;


        public PermissionHandler(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        protected async override Task HandleRequirementAsync(AuthorizationHandlerContext context, AuthorizationRequirement requirement)
        {
            if (context.Resource is HttpContext httpContext && httpContext.GetEndpoint() is RouteEndpoint endpoint)
            {
                endpoint.RoutePattern.RequiredValues.TryGetValue("controller", out var _controller);
                endpoint.RoutePattern.RequiredValues.TryGetValue("action", out var _action);

                endpoint.RoutePattern.RequiredValues.TryGetValue("page", out var _page);
                endpoint.RoutePattern.RequiredValues.TryGetValue("area", out var _area);
                var role = _httpContextAccessor.HttpContext.User.Claims.First(c => c.Type == "RoleId").Value;
                var listdata = action.Permissions_RetrieveByRole(Convert.ToInt32(role));
                var checkparmition = listdata.FirstOrDefault(s => s.ActionName == (string)_action && s.ControllerName == (string)_controller);
                // Check if a parent action is permitted then it'll allow child without checking for child permissions
                if (!string.IsNullOrWhiteSpace(requirement?.PermissionName) && !requirement.PermissionName.Equals("Authorization"))
                {
                    _action = requirement.PermissionName;
                }
                if (checkparmition!=null)
                {
                if (requirement != null && context.User.Identity?.IsAuthenticated == true && _controller != null && _action != null && checkparmition.ActionName != null && checkparmition.ControllerName != null)
                {
                    context.Succeed(requirement);
                }
                }
            }

            await Task.CompletedTask;
        }
    }
}
