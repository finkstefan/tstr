using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamRegistrationService.Models
{
    /// <summary>
    /// Prestavlja model naplate registracije ispita
    /// </summary>
    public class ExamRegistrationBillDto
    {
        /// <summary>
        /// Id studenta koji je prijavio ispit
        /// </summary>
        public Guid StudentId { get; set; }

        /// <summary>
        /// Id ispita za koji se vrši uplata
        /// </summary>
        public Guid ExamId { get; set; }

        /// <summary>
        /// Iznos prijave ispita
        /// </summary>
        public decimal Ammount { get; set; }
    }
}
