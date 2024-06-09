using Microsoft.AspNetCore.Authorization;

namespace UserManagementApi.Authorization
{
    public class AdministradorHandler : AuthorizationHandler<AdministradorRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AdministradorRequirement requirement)
        {
            if (context.User.HasClaim(c => c.Type == "Administrador" && c.Value == "true"))
            {
                context.Succeed(requirement); // O usuário é um administrador
            }

            return Task.CompletedTask; // O usuário não é um administrador
        }
    }
}
