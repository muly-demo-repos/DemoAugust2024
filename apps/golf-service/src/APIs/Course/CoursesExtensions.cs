using GolfService.APIs.Dtos;
using GolfService.Infrastructure.Models;

namespace GolfService.APIs.Extensions;

public static class CoursesExtensions
{
    public static Course ToDto(this CourseDbModel model)
    {
        return new Course
        {
            CreatedAt = model.CreatedAt,
            Description = model.Description,
            Games = model.Games?.Select(x => x.Id).ToList(),
            Id = model.Id,
            Level = model.Level,
            Name = model.Name,
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static CourseDbModel ToModel(
        this CourseUpdateInput updateDto,
        CourseWhereUniqueInput uniqueId
    )
    {
        var course = new CourseDbModel
        {
            Id = uniqueId.Id,
            Description = updateDto.Description,
            Level = updateDto.Level,
            Name = updateDto.Name
        };

        if (updateDto.CreatedAt != null)
        {
            course.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.UpdatedAt != null)
        {
            course.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return course;
    }
}
