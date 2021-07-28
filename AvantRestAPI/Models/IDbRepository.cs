using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AvantRestAPI.Models
{
    interface IDbRepository
    {
        IEnumerable<Contractor> Contractors { get; }
    }
}
