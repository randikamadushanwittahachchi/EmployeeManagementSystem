using BaseLibrary.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServerLibrary.Repositores.Contracts;

namespace Server.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Policy = "AuthorizedOnly")]
public class BranchController : GenericController<Branch>
{
    // Injected Generic Repository
    public BranchController(IGenericRepositoryInterface<Branch> genericReposiroty) : base(genericReposiroty)
    {
    }
}
