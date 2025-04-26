using BaseLibrary.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServerLibrary.Repositores.Contracts;

namespace Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TownController : GenericController<Town>
{
    // Injected Generic Repository
    public TownController(IGenericRepositoryInterface<Town> genericReposiroty) : base(genericReposiroty)
    {
    }
}
