using BaseLibrary.Entities;
using Microsoft.AspNetCore.Mvc;
using ServerLibrary.Repositores.Contracts;

namespace Server.Controllers;

public class SanctionController : GenericController<Sanction>
{
    public SanctionController(IGenericRepositoryInterface<Sanction> sanctionRepository) : base(sanctionRepository) { }
}
