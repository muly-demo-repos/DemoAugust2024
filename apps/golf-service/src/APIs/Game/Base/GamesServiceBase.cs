using GolfService.APIs;
using GolfService.APIs.Common;
using GolfService.APIs.Dtos;
using GolfService.APIs.Errors;
using GolfService.APIs.Extensions;
using GolfService.Infrastructure;
using GolfService.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace GolfService.APIs;

public abstract class GamesServiceBase : IGamesService
{
    protected readonly GolfServiceDbContext _context;

    public GamesServiceBase(GolfServiceDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one Game
    /// </summary>
    public async Task<Game> CreateGame(GameCreateInput createDto)
    {
        var game = new GameDbModel
        {
            CreatedAt = createDto.CreatedAt,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            game.Id = createDto.Id;
        }
        if (createDto.Course != null)
        {
            game.Course = await _context
                .Courses.Where(course => createDto.Course.Id == course.Id)
                .FirstOrDefaultAsync();
        }

        if (createDto.Player_1 != null)
        {
            game.Player_1 = await _context
                .Players.Where(player => createDto.Player_1.Id == player.Id)
                .FirstOrDefaultAsync();
        }

        if (createDto.Player_2 != null)
        {
            game.Player_2 = await _context
                .Players.Where(player => createDto.Player_2.Id == player.Id)
                .FirstOrDefaultAsync();
        }

        _context.Games.Add(game);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<GameDbModel>(game.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one Game
    /// </summary>
    public async Task DeleteGame(GameWhereUniqueInput uniqueId)
    {
        var game = await _context.Games.FindAsync(uniqueId.Id);
        if (game == null)
        {
            throw new NotFoundException();
        }

        _context.Games.Remove(game);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many Games
    /// </summary>
    public async Task<List<Game>> Games(GameFindManyArgs findManyArgs)
    {
        var games = await _context
            .Games.Include(x => x.Player_1)
            .Include(x => x.Course)
            .ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return games.ConvertAll(game => game.ToDto());
    }

    /// <summary>
    /// Meta data about Game records
    /// </summary>
    public async Task<MetadataDto> GamesMeta(GameFindManyArgs findManyArgs)
    {
        var count = await _context.Games.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one Game
    /// </summary>
    public async Task<Game> Game(GameWhereUniqueInput uniqueId)
    {
        var games = await this.Games(
            new GameFindManyArgs { Where = new GameWhereInput { Id = uniqueId.Id } }
        );
        var game = games.FirstOrDefault();
        if (game == null)
        {
            throw new NotFoundException();
        }

        return game;
    }

    /// <summary>
    /// Update one Game
    /// </summary>
    public async Task UpdateGame(GameWhereUniqueInput uniqueId, GameUpdateInput updateDto)
    {
        var game = updateDto.ToModel(uniqueId);

        if (updateDto.Course != null)
        {
            game.Course = await _context
                .Courses.Where(course => updateDto.Course == course.Id)
                .FirstOrDefaultAsync();
        }

        if (updateDto.Player_1 != null)
        {
            game.Player_1 = await _context
                .Players.Where(player => updateDto.Player_1 == player.Id)
                .FirstOrDefaultAsync();
        }

        if (updateDto.Player_2 != null)
        {
            game.Player_2 = await _context
                .Players.Where(player => updateDto.Player_2 == player.Id)
                .FirstOrDefaultAsync();
        }

        _context.Entry(game).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Games.Any(e => e.Id == game.Id))
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
    /// Get a Course record for Game
    /// </summary>
    public async Task<Course> GetCourse(GameWhereUniqueInput uniqueId)
    {
        var game = await _context
            .Games.Where(game => game.Id == uniqueId.Id)
            .Include(game => game.Course)
            .FirstOrDefaultAsync();
        if (game == null)
        {
            throw new NotFoundException();
        }
        return game.Course.ToDto();
    }
}
