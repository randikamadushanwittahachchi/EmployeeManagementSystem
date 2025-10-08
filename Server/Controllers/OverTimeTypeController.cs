using BaseLibrary.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServerLibrary.Repositores.Contracts;

namespace Server.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Policy = "AuthorizedOnly")]

public class OverTimeTypeController : GenericController<OverTimeType>
{
    public OverTimeTypeController(IGenericRepositoryInterface<OverTimeType> overTimeRepository) : base(overTimeRepository) { }
}
