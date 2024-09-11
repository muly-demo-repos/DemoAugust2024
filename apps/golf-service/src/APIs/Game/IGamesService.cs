using GolfService.APIs.Common;
using GolfService.APIs.Dtos;

namespace GolfService.APIs;

public interface IGamesService
{
    /// <summary>
    /// Create one Game
    /// </summary>
    public Task<Game> CreateGame(GameCreateInput game);

    /// <summary>
    /// Delete one Game
    /// </summary>
    public Task DeleteGame(GameWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many Games
    /// </summary>
    public Task<List<Game>> Games(GameFindManyArgs findManyArgs);

    /// <summary>
    /// Meta data about Game records
    /// </summary>
    public Task<MetadataDto> GamesMeta(GameFindManyArgs findManyArgs);

    /// <summary>
    /// Get one Game
    /// </summary>
    public Task<Game> Game(GameWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one Game
    /// </summary>
    public Task UpdateGame(GameWhereUniqueInput uniqueId, GameUpdateInput updateDto);

    /// <summary>
    /// Get a Course record for Game
    /// </summary>
    public Task<Course> GetCourse(GameWhereUniqueInput uniqueId);
}
