using Microsoft.AspNetCore.Mvc;

namespace OrganizationalManagementSystem.APIs;

[ApiController()]
public class PeopleController : PeopleControllerBase
{
    public PeopleController(IPeopleService service)
        : base(service) { }
}
