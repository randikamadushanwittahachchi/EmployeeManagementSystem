using BaseLibrary.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServerLibrary.Repositores.Contracts;

namespace Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GeneralDeparmentController : GenericController<GeneralDepartment>
{
    // Injected Generic Repository
    public GeneralDeparmentController(IGenericRepositoryInterface<GeneralDepartment> genericReposiroty) : base(genericReposiroty)
    {
    }
}
