using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AvantRestAPI.Models
{
    public class ContractorComparer : IEqualityComparer<Contractor>
    {
        public bool Equals(Contractor x, Contractor y)
        {
            if (ReferenceEquals(x, y)) return true;
            if (ReferenceEquals(x, null)) return false;
            if (ReferenceEquals(y, null)) return false;
            if (x.GetType() != y.GetType()) return false;
            return x.FullName == y.FullName 
                   && x.Name == y.Name 
                   && x.INN == y.INN
                   && x.KPP == y.KPP
                   && x.Type == y.Type
                   && x.Id == y.Id;
        }

        public int GetHashCode(Contractor obj)
        {
            return (obj.FullName != null ? obj.FullName.GetHashCode() : 0);
        }
    }
}
