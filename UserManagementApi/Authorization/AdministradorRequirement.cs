using Microsoft.AspNetCore.Authorization;

namespace UserManagementApi.Authorization
{
    public class AdministradorRequirement : IAuthorizationRequirement
    {
        public AdministradorRequirement() { }
    }
}
