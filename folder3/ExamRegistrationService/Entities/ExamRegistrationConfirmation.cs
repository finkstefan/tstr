using System;

namespace ExamRegistrationService.Entities
{
    /// <summary>
    /// Predstavlja potvrdu prijave ispita.
    /// </summary>
    public class ExamRegistrationConfirmation
    {
        /// <summary>
        /// ID prijave ispita.
        /// </summary>
        public Guid ExamRegistrationId { get; set; }

        /// <summary>
        /// Indeks studenta
        /// </summary>
        public string StudentIndex { get; set; }

        /// <summary>
        /// Ime studenta
        /// </summary>
        public string StudentFirstName { get; set; }

        /// <summary>
        /// Prezime studenta
        /// </summary>
        public string StudentLastName { get; set; }

        /// <summary>
        /// Šifra predmeta
        /// </summary>
        public string SubjectCode { get; set; }

        /// <summary>
        /// Naziv predmeta
        /// </summary>
        public string SubjectName { get; set; }

        /// <summary>
        /// Datum i vreme održavanja ispita.
        /// </summary>
        public DateTime ExamTime { get; set; }
    }
}
