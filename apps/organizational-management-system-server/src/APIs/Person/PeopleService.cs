using OrganizationalManagementSystem.Infrastructure;

namespace OrganizationalManagementSystem.APIs;

public class PeopleService : PeopleServiceBase
{
    public PeopleService(OrganizationalManagementSystemDbContext context)
        : base(context) { }
}
