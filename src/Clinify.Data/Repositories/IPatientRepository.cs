using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Clinify.Data.Entities;

namespace Clinify.Data.Repositories
{
    public interface IPatientRepository : IRepositoryAsync<Patient>
    {
    }
}
