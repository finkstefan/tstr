using System;

namespace ExamRegistrationService.Models
{
    /// <summary>
    /// DTO za prijavu ispita
    /// </summary>
    public class ExamRegistrationDto
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
        /// Ime i prezime studenta.
        /// </summary>
        public string StudentName { get; set; }

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
        /// Šifra i naziv predmeta predmeta.
        /// </summary>
        public string Subject { get; set; }

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
