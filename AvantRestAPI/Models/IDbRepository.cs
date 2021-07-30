using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AvantRestAPI.Models
{
    public interface IDbRepository
    {
        IEnumerable<Contractor> Contractors { get; }
        Contractor AddContractor(Contractor contractor);
        Contractor UpdateContractor(Contractor contractor);
        void RemoveContractor(Contractor contractor);
        void RemoveContractor(int id);
    }
}
