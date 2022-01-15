using ExamRegistrationService.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ExamRegistrationService.ServiceCalls
{
    public class ExamBillingService : IExamBillingService
    {
        private readonly IConfiguration configuration;

        public ExamBillingService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public bool BillStudentAccount(ExamRegistrationBillDto bill)
        {
            using (HttpClient client = new HttpClient())
            {
                var x = configuration["Services:ExamBillingService"];
                Uri url = new Uri($"{ configuration["Services:ExamBillingService"] }api/examBillings");

                HttpContent content = new StringContent(JsonConvert.SerializeObject(bill));
                content.Headers.ContentType.MediaType = "application/json";

                HttpResponseMessage response = client.PostAsync(url, content).Result;
                if (!response.IsSuccessStatusCode)
                {
                    return false;
                }
                return true;
            }
        }
    }
}
