using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AvantRestAPI.Models
{
    public interface IDbRepository
    {
        IEnumerable<Contractor> Contractors { get; }
        void AddContractor(Contractor contractor);
        void RemoveContractor(Contractor contractor);
        void RemoveContractor(int Id);
    }
}
