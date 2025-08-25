using BaseLibrary.Entities;
using Microsoft.AspNetCore.Mvc;
using ServerLibrary.Repositores.Contracts;

namespace Server.Controllers;

public class VacationController : GenericController<Vacation>
{
    public VacationController(IGenericRepositoryInterface<Vacation> vacationRepository) : base(vacationRepository) { }
}
