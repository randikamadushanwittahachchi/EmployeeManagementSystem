using BaseLibrary.Entities;
using Microsoft.AspNetCore.Mvc;
using ServerLibrary.Repositores.Contracts;

namespace Server.Controllers;
[Route("api/[controller]")]
[ApiController]
public class EmployeeController : GenericController<Employee>
{
    // Injected Generic Repository
    public EmployeeController(IGenericRepositoryInterface<Employee> genericRepository): base(genericRepository)
    {

    }
}
