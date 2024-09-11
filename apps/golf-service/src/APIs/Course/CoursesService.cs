using GolfService.Infrastructure;

namespace GolfService.APIs;

public class CoursesService : CoursesServiceBase
{
    public CoursesService(GolfServiceDbContext context)
        : base(context) { }
}
