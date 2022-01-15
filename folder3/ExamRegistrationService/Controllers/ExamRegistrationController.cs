using AutoMapper;
using ExamRegistrationService.Data;
using ExamRegistrationService.Entities;
using ExamRegistrationService.Models;
using ExamRegistrationService.Models.Exceptions;
using ExamRegistrationService.ServiceCalls;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;

namespace ExamRegistrationService.Controllers
{   
    [ApiController]
    [Route("api/examRegistrations")]
    [Produces("application/json", "application/xml")] //Sve akcije kontrolera mogu da vraćaju definisane formate
    [Authorize] //Ovom kontroleru mogu da pristupaju samo autorizovani korisnici
    public class ExamRegistrationController : ControllerBase //Daje nam pristup korisnim poljima i metodama
    {
        private readonly IExamRegistrationRepository examRegistrationRepository;
        private readonly LinkGenerator linkGenerator; //Služi za generisanje putanje do neke akcije (videti primer u metodu CreateExamRegistration)
        private readonly IMapper mapper;
        private readonly IExamBillingService billingService;

        //Pomoću dependency injection-a dodajemo potrebne zavisnosti
        public ExamRegistrationController(IExamRegistrationRepository examRegistrationRepository, LinkGenerator linkGenerator, IMapper mapper, IExamBillingService billingService)
        {
            this.examRegistrationRepository = examRegistrationRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
            this.billingService = billingService;
        }

        /// <summary>
        /// Vraća sve prijave ispita za zadate filtere.
        /// </summary>
        /// <param name="studentIndex">Indeks studenta (npr. IT1/2020)</param>
        /// <param name="subjectCode">Šifra predmeta (npr. S12020)</param>
        /// <param name="subjectName">Naziv predmeta</param>
        /// <returns>Lista prijava ispita</returns>
        /// <response code="200">Vraća listu prijava ispita</response>
        /// <response code="404">Nije pronađena ni jedna jedina prijava ispita</response>
        [HttpGet]
        [HttpHead] //Podržavamo i HTTP head zahtev koji nam vraća samo zaglavlja u odgovoru    
        [ProducesResponseType(StatusCodes.Status200OK)] //Eksplicitno definišemo šta sve može ova akcija da vrati
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<List<ExamRegistrationDto>> GetExamRegistrations(string studentIndex, string subjectCode, string subjectName)
        {
            var registrations = examRegistrationRepository.GetExamRegistrations(studentIndex, subjectCode, subjectName);

            //Ukoliko nismo pronašli ni jednu prijavu vratiti status 204 (NoContent)
            if (registrations == null || registrations.Count == 0)
            {
                return NoContent();
            }

            //Ukoliko smo našli neke prijava vratiti status 200 i listu pronađenih prijava (DTO objekti)
            return Ok(mapper.Map<List<ExamRegistrationDto>>(registrations));
        }

        /// <summary>
        /// Vraća jednu prijavu ispita na osnovu ID-ja prijave.
        /// </summary>
        /// <param name="examRegistrationId">ID prijave ispita</param>
        /// <returns></returns>
        /// <response code="200">Vraća traženu prijavu ispita</response>
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ExamRegistration))] //Kada se koristi IActionResult
        [HttpGet("{examRegistrationId}")] //Dodatak na rutu koja je definisana na nivou kontrolera
        public ActionResult<ExamRegistrationDto> GetExamRegistration(Guid examRegistrationId) //Na ovaj parametar će se mapirati ono što je prosleđeno u ruti
        {
            var registration = examRegistrationRepository.GetExamRegistrationById(examRegistrationId);

            if (registration == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<ExamRegistrationDto>(registration));
        }

        /// <summary>
        /// Kreira novu prijavu ispita.
        /// </summary>
        /// <param name="examRegistration">Model prijave ispita</param>
        /// <returns>Potvrdu o kreiranoj prijavi.</returns>
        /// <remarks>
        /// Primer zahteva za kreiranje nove prijave ispita \
        /// POST /api/ExamRegistration \
        /// {     \
        ///     "studentIndex": "IT2/2019", \
        ///     "studentId": "2841defc-761e-40d8-b8a3-d3e58516dca7", \
        ///     "studentFirstName": "Marko", \
        ///     "studentLastName": "Marković", \
        ///     "studentEnrolledYear": 3, \
        ///     "studentCurrentSemester": 2, \
        ///     "subjectId": "4563cf92-b8d0-4574-9b40-a725f884da36", \
        ///     "subjectCode": "S32020", \
        ///     "subjectName": "Subject 3", \
        ///     "subjectTerm": 2, \
        ///     "subjectSemester": 2, \
        ///     "subjectExamTime": "2020-11-15T10:30:00" \
        ///}
        /// </remarks>
        /// <response code="200">Vraća kreiranu prijavu ispita</response>
        /// <response code="500">Došlo je do greške na serveru prilikom prijave ispita</response>
        [HttpPost]
        [Consumes("application/json")]        
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<ExamRegistrationConfirmationDto> CreateExamRegistration([FromBody] ExamRegistrationCreationDto examRegistration)
        {
            try
            {
                ExamRegistration examRegistrationEntity = mapper.Map<ExamRegistration>(examRegistration);
                ExamRegistrationConfirmation confirmation = examRegistrationRepository.CreateExamRegistration(examRegistrationEntity);
                examRegistrationRepository.SaveChanges(); //Perzistiramo promene

                //Generisati identifikator novokreiranog resursa
                string location = linkGenerator.GetPathByAction("GetExamRegistration", "ExamRegistration", new { examRegistrationId = confirmation.ExamRegistrationId });

                var billingInfo = mapper.Map<ExamRegistrationBillDto>(examRegistration);

                //TODO: Implementirati logiku tako da cena ne mora da bude fiksna
                billingInfo.Ammount = 200M; //Fiksna cena prijave ispita
                billingInfo.ExamId = confirmation.ExamRegistrationId;
                bool billed = billingService.BillStudentAccount(billingInfo);

                //Ukoliko iz nekog razloga ne uspemo da naplatimo prijavu ispita ista se briše
                if (!billed)
                {
                    examRegistrationRepository.DeleteExamRegistration(confirmation.ExamRegistrationId);
                    throw new BillingException("Neuspešna prijava ispita. Postoji problem sa naplatom. Molimo kontaktirajte administratora"); //Bacamo izuzetak koji će biti uhvaćen i vraćen kao status 500
                }

                //Vratiti status 201 (Created), zajedno sa identifikatorom novokreiranog resursa (prijave ispita) i samom prijavom ispita.
                return Created(location, mapper.Map<ExamRegistrationConfirmationDto>(confirmation));
                //return CreatedAtRoute(); //Druga opcija za vraćanje kreiranog resursa i lokacije
            }
            catch (BillingException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            catch (Exception ex)
            {
                //TODO: Logovati ex
                //Ukoliko nastane neka greška na servisu vratiti status 500 (InternalServerError).
                return StatusCode(StatusCodes.Status500InternalServerError, "Create error");
            }
        }

        /// <summary>
        /// Ažurira jednu prijavu ispita.
        /// </summary>
        /// <param name="examRegistration">Model prijave ispita koji se ažurira</param>
        /// <returns>Potvrdu o modifikovanoj prijavi.</returns>
        /// <response code="200">Vraća ažuriranu prijavu ispita</response>
        /// <response code="400">Prijava ispita koja se ažurira nije pronađena</response>
        /// <response code="500">Došlo je do greške na serveru prilikom ažuriranja prijave ispita</response>
        [HttpPut]
        [Consumes("application/json")]        
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<ExamRegistrationDto> UpdateExamRegistration(ExamRegistrationUpdateDto examRegistration)
        {
            try
            {
                //Proveriti da li uopšte postoji prijava koju pokušavamo da ažuriramo.
                var oldExamRegistration = examRegistrationRepository.GetExamRegistrationById(examRegistration.ExamRegistrationId);
                if (oldExamRegistration == null)
                {
                    return NotFound(); //Ukoliko ne postoji vratiti status 404 (NotFound).
                }
                ExamRegistration examRegistrationEntity = mapper.Map<ExamRegistration>(examRegistration);

                mapper.Map(examRegistrationEntity, oldExamRegistration); //Update objekta koji treba da sačuvamo u bazi                

                examRegistrationRepository.SaveChanges(); //Perzistiramo promene
                return Ok(mapper.Map<ExamRegistrationDto>(oldExamRegistration));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Update error");
            }
        }

        /// <summary>
        /// Vrši brisanje jedne prijave ispita na osnovu ID-ja prijave.
        /// </summary>
        /// <param name="examRegistrationId">ID prijave ispita</param>
        /// <returns>Status 204 (NoContent)</returns>
        /// <response code="204">Prijava ispita uspešno obrisana</response>
        /// <response code="404">Nije pronađena prijava ispita za brisanje</response>
        /// <response code="500">Došlo je do greške na serveru prilikom brisanja prijave ispita</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpDelete("{examRegistrationId}")]
        public IActionResult DeleteExamRegistration(Guid examRegistrationId)
        {
            //TODO: Dodati logiku da se studentu vrate sredstva na račun ukoliko se obriše prijava ispita
            try
            {
                var registration = examRegistrationRepository.GetExamRegistrationById(examRegistrationId);

                if (registration == null)
                {
                    return NotFound();
                }

                examRegistrationRepository.DeleteExamRegistration(examRegistrationId);
                examRegistrationRepository.SaveChanges();
                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete error");
            }
        }
        
        /// <summary>
        /// Vraća opcije za rad sa prijavama ispita
        /// </summary>
        /// <returns></returns>
        [HttpOptions]
        [AllowAnonymous] //Dozvoljavamo pristup anonimnim korisnicima
        public IActionResult GetExamRegistrationOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }
    }
}
