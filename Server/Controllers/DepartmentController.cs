using BaseLibrary.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServerLibrary.Repositores.Contracts;

namespace Server.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Policy = "AuthorizedOnly")]

public class DepartmentController : GenericController<Department>
{
    // Injected Generic Repository
    public DepartmentController(IGenericRepositoryInterface<Department> genericReposiroty) : base(genericReposiroty)
    {
    }
}
