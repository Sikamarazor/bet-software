using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BETSoftware.Entities;

namespace BETSoftware.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(User user);
    }
}