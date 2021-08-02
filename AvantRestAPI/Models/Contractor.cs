using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [Required]
        public string Name { get; set; }

        public string FullName { get; set; }

        [Required]
        [JsonConverter(typeof(StringEnumConverter))]
        public ContractorType Type { get; set; }

        [Required]
        public string Inn { get; set; }

        [Required]
        public string Kpp { get; set; }
    }
}
