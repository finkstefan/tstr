using System;

namespace ExamRegistrationService.Entities
{
    /// <summary>
    /// Predstavlja prijavu ispita
    /// </summary>
    public class ExamRegistration
    {
        /// <summary>
        /// ID prijave ispita.
        /// </summary>
        public Guid ExamRegistrationId { get; set; }

        #region Student

        /// <summary>
        /// ID studenta.
        /// </summary>
        public Guid StudentId { get; set; }

        /// <summary>
        /// Indeks studenta (npr. IT1/2020).
        /// </summary>
        public string StudentIndex { get; set; }

        /// <summary>
        /// Ime studenta.
        /// </summary>
        public string StudentFirstName { get; set; }

        /// <summary>
        /// Prezime studenta.
        /// </summary>
        public string StudentLastName { get; set; }

        /// <summary>
        /// Trenutno upisana godina.
        /// </summary>
        public int StudentEnrolledYear { get; set; }

        /// <summary>
        /// Semestar koji trenutno sluša student.
        /// </summary>
        public int StudentCurrentSemester { get; set; }
        #endregion

        #region Exam

        /// <summary>
        /// ID predmeta.
        /// </summary>
        public Guid SubjectId { get; set; }

        /// <summary>
        /// Šifra predmeta.
        /// </summary>
        public string SubjectCode { get; set; }

        /// <summary>
        /// Naziv predmeta.
        /// </summary>
        public string SubjectName { get; set; }

        /// <summary>
        /// Godina u kojoj se drži predmet.
        /// </summary>
        public int SubjectTerm { get; set; }

        /// <summary>
        /// Semestar u kom se drži predmet.
        /// </summary>
        public int SubjectSemester { get; set; }

        /// <summary>
        /// Datum i vreme održavanja ispita.
        /// </summary>
        public DateTime SubjectExamTime { get; set; }
        #endregion
    }
}
