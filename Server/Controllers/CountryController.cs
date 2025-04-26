using BaseLibrary.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServerLibrary.Repositores.Contracts;

namespace Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CountryController : GenericController<Country>
{
    // Injected Generic Repository
    public CountryController(IGenericRepositoryInterface<Country> genericReposiroty) : base(genericReposiroty)
    {
    }
}
