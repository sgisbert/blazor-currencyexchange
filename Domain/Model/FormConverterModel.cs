using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Model
{
    public class FormConverterModel
    {
        public DateTime CurrencyDate;
        [Required]
        public String OriginCurrency;
        [Required]
        public String TargetCurrency;
        [Required]
        public long Amount;
    }
}
