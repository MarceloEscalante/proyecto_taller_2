using System.Collections.Generic;
using System.Threading.Tasks;
using Sistema_ModParts.Models;

namespace Sistema_ModParts.Repositories;

public interface IRolRepository
{
    Task<IEnumerable<Rol>> GetAllAsync();
}
