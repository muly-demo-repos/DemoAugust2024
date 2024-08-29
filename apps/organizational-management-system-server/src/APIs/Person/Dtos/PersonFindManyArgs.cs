using Microsoft.AspNetCore.Mvc;
using OrganizationalManagementSystem.APIs.Common;
using OrganizationalManagementSystem.Infrastructure.Models;

namespace OrganizationalManagementSystem.APIs.Dtos;

[BindProperties(SupportsGet = true)]
public class PersonFindManyArgs : FindManyInput<Person, PersonWhereInput> { }
