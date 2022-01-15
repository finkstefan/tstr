using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ExamRegistrationService.Models
{
    /// <summary>
    /// Model za ažuriranje prijave ispita
    /// </summary>
    public class ExamRegistrationUpdateDto : IValidatableObject
    {
        public Guid ExamRegistrationId { get; set; }
        #region Student
        public Guid StudentId { get; set; }

        [Required(ErrorMessage = "Obavezno je uneti Index studenta.")] //napomena za ApiController
        public string StudentIndex { get; set; }

        [MaxLength(10)]
        public string StudentFirstName { get; set; }

        public string StudentLastName { get; set; }

        public int StudentEnrolledYear { get; set; }

        public int StudentCurrentSemester { get; set; }
        #endregion

        #region Exam
        public Guid SubjectId { get; set; }

        public string SubjectCode { get; set; }

        public string SubjectName { get; set; }

        public int SubjectTerm { get; set; }

        public int SubjectSemester { get; set; }

        public DateTime SubjectExamTime { get; set; }

        #endregion

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (StudentEnrolledYear < SubjectTerm || (StudentEnrolledYear == SubjectTerm && StudentCurrentSemester < SubjectSemester))
            {
                yield return new ValidationResult(
                   "Nije moguće kreirati prijavu pošto se ispit ne nalazi u studentovoj trenutno upisanoj godini i semestru.",
                   new[] { "ExamRegistrationCreationDto" });
            }
        }
    }
}
