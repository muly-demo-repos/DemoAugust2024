using Microsoft.AspNetCore.Mvc;

namespace OrganizationalManagementSystem.APIs;

[ApiController()]
public class OrganizationsController : OrganizationsControllerBase
{
    public OrganizationsController(IOrganizationsService service)
        : base(service) { }
}
