using GolfService.APIs.Common;
using GolfService.APIs.Dtos;

namespace GolfService.APIs;

public interface ICoursesService
{
    /// <summary>
    /// Create one Course
    /// </summary>
    public Task<Course> CreateCourse(CourseCreateInput course);

    /// <summary>
    /// Delete one Course
    /// </summary>
    public Task DeleteCourse(CourseWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many Courses
    /// </summary>
    public Task<List<Course>> Courses(CourseFindManyArgs findManyArgs);

    /// <summary>
    /// Meta data about Course records
    /// </summary>
    public Task<MetadataDto> CoursesMeta(CourseFindManyArgs findManyArgs);

    /// <summary>
    /// Get one Course
    /// </summary>
    public Task<Course> Course(CourseWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one Course
    /// </summary>
    public Task UpdateCourse(CourseWhereUniqueInput uniqueId, CourseUpdateInput updateDto);

    /// <summary>
    /// Connect multiple Games records to Course
    /// </summary>
    public Task ConnectGames(CourseWhereUniqueInput uniqueId, GameWhereUniqueInput[] gamesId);

    /// <summary>
    /// Disconnect multiple Games records from Course
    /// </summary>
    public Task DisconnectGames(CourseWhereUniqueInput uniqueId, GameWhereUniqueInput[] gamesId);

    /// <summary>
    /// Find multiple Games records for Course
    /// </summary>
    public Task<List<Game>> FindGames(
        CourseWhereUniqueInput uniqueId,
        GameFindManyArgs GameFindManyArgs
    );

    /// <summary>
    /// Update multiple Games records for Course
    /// </summary>
    public Task UpdateGames(CourseWhereUniqueInput uniqueId, GameWhereUniqueInput[] gamesId);
}
