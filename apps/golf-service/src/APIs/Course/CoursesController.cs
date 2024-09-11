using Microsoft.AspNetCore.Mvc;

namespace GolfService.APIs;

[ApiController()]
public class CoursesController : CoursesControllerBase
{
    public CoursesController(ICoursesService service)
        : base(service) { }
}
