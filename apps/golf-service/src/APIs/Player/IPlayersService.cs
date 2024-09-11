using GolfService.APIs.Common;
using GolfService.APIs.Dtos;

namespace GolfService.APIs;

public interface IPlayersService
{
    /// <summary>
    /// Create one Player
    /// </summary>
    public Task<Player> CreatePlayer(PlayerCreateInput player);

    /// <summary>
    /// Delete one Player
    /// </summary>
    public Task DeletePlayer(PlayerWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many Players
    /// </summary>
    public Task<List<Player>> Players(PlayerFindManyArgs findManyArgs);

    /// <summary>
    /// Meta data about Player records
    /// </summary>
    public Task<MetadataDto> PlayersMeta(PlayerFindManyArgs findManyArgs);

    /// <summary>
    /// Get one Player
    /// </summary>
    public Task<Player> Player(PlayerWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one Player
    /// </summary>
    public Task UpdatePlayer(PlayerWhereUniqueInput uniqueId, PlayerUpdateInput updateDto);

    /// <summary>
    /// Connect multiple GamesAsPlayerOne records to Player
    /// </summary>
    public Task ConnectGamesAsPlayerOne(
        PlayerWhereUniqueInput uniqueId,
        GameWhereUniqueInput[] gamesId
    );

    /// <summary>
    /// Disconnect multiple GamesAsPlayerOne records from Player
    /// </summary>
    public Task DisconnectGamesAsPlayerOne(
        PlayerWhereUniqueInput uniqueId,
        GameWhereUniqueInput[] gamesId
    );

    /// <summary>
    /// Find multiple GamesAsPlayerOne records for Player
    /// </summary>
    public Task<List<Game>> FindGamesAsPlayerOne(
        PlayerWhereUniqueInput uniqueId,
        GameFindManyArgs GameFindManyArgs
    );

    /// <summary>
    /// Update multiple GamesAsPlayerOne records for Player
    /// </summary>
    public Task UpdateGamesAsPlayerOne(
        PlayerWhereUniqueInput uniqueId,
        GameWhereUniqueInput[] gamesId
    );

    /// <summary>
    /// Connect multiple GamesAsPlayerTwo records to Player
    /// </summary>
    public Task ConnectGamesAsPlayerTwo(
        PlayerWhereUniqueInput uniqueId,
        GameWhereUniqueInput[] gamesId
    );

    /// <summary>
    /// Disconnect multiple GamesAsPlayerTwo records from Player
    /// </summary>
    public Task DisconnectGamesAsPlayerTwo(
        PlayerWhereUniqueInput uniqueId,
        GameWhereUniqueInput[] gamesId
    );

    /// <summary>
    /// Find multiple GamesAsPlayerTwo records for Player
    /// </summary>
    public Task<List<Game>> FindGamesAsPlayerTwo(
        PlayerWhereUniqueInput uniqueId,
        GameFindManyArgs GameFindManyArgs
    );

    /// <summary>
    /// Update multiple GamesAsPlayerTwo records for Player
    /// </summary>
    public Task UpdateGamesAsPlayerTwo(
        PlayerWhereUniqueInput uniqueId,
        GameWhereUniqueInput[] gamesId
    );
}
