using System;

namespace ExamRegistrationService.Models
{
    /// <summary>
    /// DTO za potvrdu prijave ispita.
    /// </summary>
    public class ExamRegistrationConfirmationDto
    {
        /// <summary>
        /// ID prijave ispita.
        /// </summary>
        public Guid ExamRegistrationId { get; set; }

        /// <summary>
        /// Ime i prezime studenta.
        /// </summary>
        public string Student { get; set; }

        /// <summary>
        /// Šifra i naziv predmeta.
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// Datum i vreme održavanja ispita.
        /// </summary>
        public DateTime ExamTime { get; set; }
    }
}
