using GolfService.APIs.Common;
using GolfService.Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;

namespace GolfService.APIs.Dtos;

[BindProperties(SupportsGet = true)]
public class PlayerFindManyArgs : FindManyInput<Player, PlayerWhereInput> { }
