using GolfService.APIs.Dtos;
using GolfService.Infrastructure.Models;

namespace GolfService.APIs.Extensions;

public static class GamesExtensions
{
    public static Game ToDto(this GameDbModel model)
    {
        return new Game
        {
            Course = model.CourseId,
            CreatedAt = model.CreatedAt,
            Id = model.Id,
            Player_1 = model.Player_1Id,
            Player_2 = model.Player_2Id,
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static GameDbModel ToModel(this GameUpdateInput updateDto, GameWhereUniqueInput uniqueId)
    {
        var game = new GameDbModel { Id = uniqueId.Id };

        if (updateDto.Course != null)
        {
            game.CourseId = updateDto.Course;
        }
        if (updateDto.CreatedAt != null)
        {
            game.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.Player_1 != null)
        {
            game.Player_1Id = updateDto.Player_1;
        }
        if (updateDto.Player_2 != null)
        {
            game.Player_2Id = updateDto.Player_2;
        }
        if (updateDto.UpdatedAt != null)
        {
            game.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return game;
    }
}
