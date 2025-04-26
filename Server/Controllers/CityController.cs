using BaseLibrary.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServerLibrary.Repositores.Contracts;

namespace Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CityController : GenericController<City>
{
    // Injected Generic Repository
    public CityController(IGenericRepositoryInterface<City> genericReposiroty) : base(genericReposiroty)
    {
    }
}
