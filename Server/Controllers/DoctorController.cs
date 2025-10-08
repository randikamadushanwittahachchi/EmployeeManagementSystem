using BaseLibrary.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServerLibrary.Repositores.Contracts;

namespace Server.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Policy = "AuthorizedOnly")]
public class DoctorController : GenericController<Doctor>
{
    public DoctorController(IGenericRepositoryInterface<Doctor> doctorRespository) : base(doctorRespository)
    {
        
    }
}
