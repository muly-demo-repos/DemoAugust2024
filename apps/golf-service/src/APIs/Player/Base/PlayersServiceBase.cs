using GolfService.APIs;
using GolfService.APIs.Common;
using GolfService.APIs.Dtos;
using GolfService.APIs.Errors;
using GolfService.APIs.Extensions;
using GolfService.Infrastructure;
using GolfService.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace GolfService.APIs;

public abstract class PlayersServiceBase : IPlayersService
{
    protected readonly GolfServiceDbContext _context;

    public PlayersServiceBase(GolfServiceDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one Player
    /// </summary>
    public async Task<Player> CreatePlayer(PlayerCreateInput createDto)
    {
        var player = new PlayerDbModel
        {
            CreatedAt = createDto.CreatedAt,
            FirstName = createDto.FirstName,
            LastName = createDto.LastName,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            player.Id = createDto.Id;
        }
        if (createDto.GamesAsPlayerOne != null)
        {
            player.GamesAsPlayerOne = await _context
                .Games.Where(game => createDto.GamesAsPlayerOne.Select(t => t.Id).Contains(game.Id))
                .ToListAsync();
        }

        if (createDto.GamesAsPlayerTwo != null)
        {
            player.GamesAsPlayerTwo = await _context
                .Games.Where(game => createDto.GamesAsPlayerTwo.Select(t => t.Id).Contains(game.Id))
                .ToListAsync();
        }

        _context.Players.Add(player);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<PlayerDbModel>(player.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one Player
    /// </summary>
    public async Task DeletePlayer(PlayerWhereUniqueInput uniqueId)
    {
        var player = await _context.Players.FindAsync(uniqueId.Id);
        if (player == null)
        {
            throw new NotFoundException();
        }

        _context.Players.Remove(player);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many Players
    /// </summary>
    public async Task<List<Player>> Players(PlayerFindManyArgs findManyArgs)
    {
        var players = await _context
            .Players.Include(x => x.GamesAsPlayerOne)
            .ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return players.ConvertAll(player => player.ToDto());
    }

    /// <summary>
    /// Meta data about Player records
    /// </summary>
    public async Task<MetadataDto> PlayersMeta(PlayerFindManyArgs findManyArgs)
    {
        var count = await _context.Players.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one Player
    /// </summary>
    public async Task<Player> Player(PlayerWhereUniqueInput uniqueId)
    {
        var players = await this.Players(
            new PlayerFindManyArgs { Where = new PlayerWhereInput { Id = uniqueId.Id } }
        );
        var player = players.FirstOrDefault();
        if (player == null)
        {
            throw new NotFoundException();
        }

        return player;
    }

    /// <summary>
    /// Update one Player
    /// </summary>
    public async Task UpdatePlayer(PlayerWhereUniqueInput uniqueId, PlayerUpdateInput updateDto)
    {
        var player = updateDto.ToModel(uniqueId);

        if (updateDto.GamesAsPlayerOne != null)
        {
            player.GamesAsPlayerOne = await _context
                .Games.Where(game => updateDto.GamesAsPlayerOne.Select(t => t).Contains(game.Id))
                .ToListAsync();
        }

        if (updateDto.GamesAsPlayerTwo != null)
        {
            player.GamesAsPlayerTwo = await _context
                .Games.Where(game => updateDto.GamesAsPlayerTwo.Select(t => t).Contains(game.Id))
                .ToListAsync();
        }

        _context.Entry(player).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Players.Any(e => e.Id == player.Id))
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
    /// Connect multiple GamesAsPlayerOne records to Player
    /// </summary>
    public async Task ConnectGamesAsPlayerOne(
        PlayerWhereUniqueInput uniqueId,
        GameWhereUniqueInput[] childrenIds
    )
    {
        var parent = await _context
            .Players.Include(x => x.GamesAsPlayerOne)
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

        var childrenToConnect = children.Except(parent.GamesAsPlayerOne);

        foreach (var child in childrenToConnect)
        {
            parent.GamesAsPlayerOne.Add(child);
        }

        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Disconnect multiple GamesAsPlayerOne records from Player
    /// </summary>
    public async Task DisconnectGamesAsPlayerOne(
        PlayerWhereUniqueInput uniqueId,
        GameWhereUniqueInput[] childrenIds
    )
    {
        var parent = await _context
            .Players.Include(x => x.GamesAsPlayerOne)
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
            parent.GamesAsPlayerOne?.Remove(child);
        }
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find multiple GamesAsPlayerOne records for Player
    /// </summary>
    public async Task<List<Game>> FindGamesAsPlayerOne(
        PlayerWhereUniqueInput uniqueId,
        GameFindManyArgs playerFindManyArgs
    )
    {
        var games = await _context
            .Games.Where(m => m.Player_1Id == uniqueId.Id)
            .ApplyWhere(playerFindManyArgs.Where)
            .ApplySkip(playerFindManyArgs.Skip)
            .ApplyTake(playerFindManyArgs.Take)
            .ApplyOrderBy(playerFindManyArgs.SortBy)
            .ToListAsync();

        return games.Select(x => x.ToDto()).ToList();
    }

    /// <summary>
    /// Update multiple GamesAsPlayerOne records for Player
    /// </summary>
    public async Task UpdateGamesAsPlayerOne(
        PlayerWhereUniqueInput uniqueId,
        GameWhereUniqueInput[] childrenIds
    )
    {
        var player = await _context
            .Players.Include(t => t.GamesAsPlayerOne)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (player == null)
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

        player.GamesAsPlayerOne = children;
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Connect multiple GamesAsPlayerTwo records to Player
    /// </summary>
    public async Task ConnectGamesAsPlayerTwo(
        PlayerWhereUniqueInput uniqueId,
        GameWhereUniqueInput[] childrenIds
    )
    {
        var parent = await _context
            .Players.Include(x => x.GamesAsPlayerTwo)
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

        var childrenToConnect = children.Except(parent.GamesAsPlayerTwo);

        foreach (var child in childrenToConnect)
        {
            parent.GamesAsPlayerTwo.Add(child);
        }

        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Disconnect multiple GamesAsPlayerTwo records from Player
    /// </summary>
    public async Task DisconnectGamesAsPlayerTwo(
        PlayerWhereUniqueInput uniqueId,
        GameWhereUniqueInput[] childrenIds
    )
    {
        var parent = await _context
            .Players.Include(x => x.GamesAsPlayerTwo)
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
            parent.GamesAsPlayerTwo?.Remove(child);
        }
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find multiple GamesAsPlayerTwo records for Player
    /// </summary>
    public async Task<List<Game>> FindGamesAsPlayerTwo(
        PlayerWhereUniqueInput uniqueId,
        GameFindManyArgs playerFindManyArgs
    )
    {
        var games = await _context
            .Games.Where(m => m.Player_2Id == uniqueId.Id)
            .ApplyWhere(playerFindManyArgs.Where)
            .ApplySkip(playerFindManyArgs.Skip)
            .ApplyTake(playerFindManyArgs.Take)
            .ApplyOrderBy(playerFindManyArgs.SortBy)
            .ToListAsync();

        return games.Select(x => x.ToDto()).ToList();
    }

    /// <summary>
    /// Update multiple GamesAsPlayerTwo records for Player
    /// </summary>
    public async Task UpdateGamesAsPlayerTwo(
        PlayerWhereUniqueInput uniqueId,
        GameWhereUniqueInput[] childrenIds
    )
    {
        var player = await _context
            .Players.Include(t => t.GamesAsPlayerTwo)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (player == null)
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

        player.GamesAsPlayerTwo = children;
        await _context.SaveChangesAsync();
    }
}
