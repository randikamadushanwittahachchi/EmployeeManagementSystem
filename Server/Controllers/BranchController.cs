using BaseLibrary.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServerLibrary.Repositores.Contracts;

namespace Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BranchController : GenericController<Branch>
{
    // Injected Generic Repository
    public BranchController(IGenericRepositoryInterface<Branch> genericReposiroty) : base(genericReposiroty)
    {
    }
}
