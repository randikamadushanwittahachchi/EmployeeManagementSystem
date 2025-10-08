using BaseLibrary.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServerLibrary.Repositores.Contracts;

namespace Server.Controllers;


[Route("api/[controller]")]
[ApiController]
[Authorize(Policy = "AuthorizedOnly")]
public class EmployeeController : GenericController<Employee>
{
    // Injected Generic Repository
    public EmployeeController(IGenericRepositoryInterface<Employee> genericRepository): base(genericRepository)
    {

    }
}
