using GolfService.APIs;
using GolfService.APIs.Common;
using GolfService.APIs.Dtos;
using GolfService.APIs.Errors;
using GolfService.APIs.Extensions;
using GolfService.Infrastructure;
using GolfService.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace GolfService.APIs;

public abstract class CoursesServiceBase : ICoursesService
{
    protected readonly GolfServiceDbContext _context;

    public CoursesServiceBase(GolfServiceDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one Course
    /// </summary>
    public async Task<Course> CreateCourse(CourseCreateInput createDto)
    {
        var course = new CourseDbModel
        {
            CreatedAt = createDto.CreatedAt,
            Description = createDto.Description,
            Level = createDto.Level,
            Name = createDto.Name,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            course.Id = createDto.Id;
        }
        if (createDto.Games != null)
        {
            course.Games = await _context
                .Games.Where(game => createDto.Games.Select(t => t.Id).Contains(game.Id))
                .ToListAsync();
        }

        _context.Courses.Add(course);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<CourseDbModel>(course.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one Course
    /// </summary>
    public async Task DeleteCourse(CourseWhereUniqueInput uniqueId)
    {
        var course = await _context.Courses.FindAsync(uniqueId.Id);
        if (course == null)
        {
            throw new NotFoundException();
        }

        _context.Courses.Remove(course);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many Courses
    /// </summary>
    public async Task<List<Course>> Courses(CourseFindManyArgs findManyArgs)
    {
        var courses = await _context
            .Courses.Include(x => x.Games)
            .ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return courses.ConvertAll(course => course.ToDto());
    }

    /// <summary>
    /// Meta data about Course records
    /// </summary>
    public async Task<MetadataDto> CoursesMeta(CourseFindManyArgs findManyArgs)
    {
        var count = await _context.Courses.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one Course
    /// </summary>
    public async Task<Course> Course(CourseWhereUniqueInput uniqueId)
    {
        var courses = await this.Courses(
            new CourseFindManyArgs { Where = new CourseWhereInput { Id = uniqueId.Id } }
        );
        var course = courses.FirstOrDefault();
        if (course == null)
        {
            throw new NotFoundException();
        }

        return course;
    }

    /// <summary>
    /// Update one Course
    /// </summary>
    public async Task UpdateCourse(CourseWhereUniqueInput uniqueId, CourseUpdateInput updateDto)
    {
        var course = updateDto.ToModel(uniqueId);

        if (updateDto.Games != null)
        {
            course.Games = await _context
                .Games.Where(game => updateDto.Games.Select(t => t).Contains(game.Id))
                .ToListAsync();
        }

        _context.Entry(course).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Courses.Any(e => e.Id == course.Id))
            {
                throw new NotFoundException();
            }
            else
            {
                throw;
            }
        }
    }

    /// <summary>
    /// Connect multiple Games records to Course
    /// </summary>
    public async Task ConnectGames(
        CourseWhereUniqueInput uniqueId,
        GameWhereUniqueInput[] childrenIds
    )
    {
        var parent = await _context
            .Courses.Include(x => x.Games)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (parent == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .Games.Where(t => childrenIds.Select(x => x.Id).Contains(t.Id))
            .ToListAsync();
        if (children.Count == 0)
        {
            throw new NotFoundException();
        }

        var childrenToConnect = children.Except(parent.Games);

        foreach (var child in childrenToConnect)
        {
            parent.Games.Add(child);
        }

        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Disconnect multiple Games records from Course
    /// </summary>
    public async Task DisconnectGames(
        CourseWhereUniqueInput uniqueId,
        GameWhereUniqueInput[] childrenIds
    )
    {
        var parent = await _context
            .Courses.Include(x => x.Games)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (parent == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .Games.Where(t => childrenIds.Select(x => x.Id).Contains(t.Id))
            .ToListAsync();

        foreach (var child in children)
        {
            parent.Games?.Remove(child);
        }
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find multiple Games records for Course
    /// </summary>
    public async Task<List<Game>> FindGames(
        CourseWhereUniqueInput uniqueId,
        GameFindManyArgs courseFindManyArgs
    )
    {
        var games = await _context
            .Games.Where(m => m.CourseId == uniqueId.Id)
            .ApplyWhere(courseFindManyArgs.Where)
            .ApplySkip(courseFindManyArgs.Skip)
            .ApplyTake(courseFindManyArgs.Take)
            .ApplyOrderBy(courseFindManyArgs.SortBy)
            .ToListAsync();

        return games.Select(x => x.ToDto()).ToList();
    }

    /// <summary>
    /// Update multiple Games records for Course
    /// </summary>
    public async Task UpdateGames(
        CourseWhereUniqueInput uniqueId,
        GameWhereUniqueInput[] childrenIds
    )
    {
        var course = await _context
            .Courses.Include(t => t.Games)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (course == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .Games.Where(a => childrenIds.Select(x => x.Id).Contains(a.Id))
            .ToListAsync();

        if (children.Count == 0)
        {
            throw new NotFoundException();
        }

        course.Games = children;
        await _context.SaveChangesAsync();
    }
}
