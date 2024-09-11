using GolfService.APIs.Dtos;
using GolfService.Infrastructure.Models;

namespace GolfService.APIs.Extensions;

public static class PlayersExtensions
{
    public static Player ToDto(this PlayerDbModel model)
    {
        return new Player
        {
            CreatedAt = model.CreatedAt,
            FirstName = model.FirstName,
            GamesAsPlayerOne = model.GamesAsPlayerOne?.Select(x => x.Id).ToList(),
            GamesAsPlayerTwo = model.GamesAsPlayerTwo?.Select(x => x.Id).ToList(),
            Id = model.Id,
            LastName = model.LastName,
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static PlayerDbModel ToModel(
        this PlayerUpdateInput updateDto,
        PlayerWhereUniqueInput uniqueId
    )
    {
        var player = new PlayerDbModel
        {
            Id = uniqueId.Id,
            FirstName = updateDto.FirstName,
            LastName = updateDto.LastName
        };

        if (updateDto.CreatedAt != null)
        {
            player.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.UpdatedAt != null)
        {
            player.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return player;
    }
}
