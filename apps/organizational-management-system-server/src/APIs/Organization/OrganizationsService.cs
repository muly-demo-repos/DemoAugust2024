using OrganizationalManagementSystem.Infrastructure;

namespace OrganizationalManagementSystem.APIs;

public class OrganizationsService : OrganizationsServiceBase
{
    public OrganizationsService(OrganizationalManagementSystemDbContext context)
        : base(context) { }
}
