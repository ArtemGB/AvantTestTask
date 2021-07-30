using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace AvantRestAPI.Models
{
    /// <summary>
    /// Контрагент
    /// </summary>
    public class Contractor : BaseModel
    {
        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                if (String.IsNullOrEmpty(value) || String.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Field Name should be filled.");
                }

                _name = value;
            }
        }
        public string FullName { get; set; }

        private ContractorType _type;

        [JsonConverter(typeof(StringEnumConverter))]
        public ContractorType Type
        {
            get => _type;
            set
            {
                if (!Enum.IsDefined(typeof(ContractorType), value))
                {
                    throw new ArgumentException("Field FullName should be filled.");
                }

                _type = value;
            }
        }

        private string _inn;
        public string Inn
        {
            get => _inn;
            set
            {
                if (String.IsNullOrEmpty(value) || String.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Field INN should be filled.");
                }

                _inn = value;
            }
        }

        private string _kpp;
        public string Kpp
        {
            get => _kpp;
            set
            {
                if (Type == ContractorType.Legal)
                    if (String.IsNullOrEmpty(value) || String.IsNullOrWhiteSpace(value))
                        throw new ArgumentException("Field KPP should be filled if contractor is a Legal.");
                _kpp = value;
            }
        }
    }
}
