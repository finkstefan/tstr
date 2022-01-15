using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ExamRegistrationService.Models
{
    /// <summary>
    /// Model za kreiranje prijave ispita
    /// </summary>
    public class ExamRegistrationCreationDto : IValidatableObject
    {
        #region Student
        /// <summary>
        /// ID studenta.
        /// </summary>
        public Guid StudentId { get; set; }

        /// <summary>
        /// Indeks studenta (npr. IT1/2020).
        /// </summary>
        [Required(ErrorMessage = "Obavezno je uneti Index studenta.")]
        public string StudentIndex { get; set; }

        /// <summary>
        /// Ime studenta.
        /// </summary>
        [MaxLength(15)]
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

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (StudentFirstName == StudentLastName)
            {
                yield return new ValidationResult(
                    "Student ne može da ima isto ime i prezime",
                    new[] { "ExamRegistrationCreationDto" });
            }

            if (StudentEnrolledYear < SubjectTerm || (StudentEnrolledYear == SubjectTerm && StudentCurrentSemester < SubjectSemester))
            {
                yield return new ValidationResult(
                   "Nije moguće kreirati prijavu pošto se ispit ne nalazi u studentovoj trenutno upisanoj godini i semestru.",
                   new[] { "ExamRegistrationCreationDto" });
            }
        }
        #endregion
    }
}
