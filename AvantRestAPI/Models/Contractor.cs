﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        private string _fullName;
        public string FullName
        {
            get => _fullName;
            set
            {
                if (String.IsNullOrEmpty(value) || String.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Field FullName should be filled.");
                }

                _fullName = value;
            }
        }

        private ContractorType _type;
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
        public string INN
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
        public string KPP
        {
            get => _kpp;
            set
            {
                if (Type == ContractorType.Organization)
                    if (String.IsNullOrEmpty(value) || String.IsNullOrWhiteSpace(value))
                        throw new ArgumentException("Field KPP should be filled if contractor is a Organization.");
                _kpp = value;
            }
        }

        public Contractor()
        {

        }
    }
}