using BaseLibrary.Entities;
using Microsoft.AspNetCore.Mvc;
using ServerLibrary.Repositores.Contracts;

namespace Server.Controllers;

public class SanctionTypeController : GenericController<SanctionType>
{
    public SanctionTypeController(IGenericRepositoryInterface<SanctionType> sanctionTypeRepository) : base(sanctionTypeRepository) { }
}
