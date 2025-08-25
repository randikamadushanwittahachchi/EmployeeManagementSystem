using BaseLibrary.Entities;
using Microsoft.AspNetCore.Mvc;
using ServerLibrary.Repositores.Contracts;

namespace Server.Controllers;

public class DoctorController : GenericController<Doctor>
{
    public DoctorController(IGenericRepositoryInterface<Doctor> doctorRespository) : base(doctorRespository)
    {
        
    }
}
