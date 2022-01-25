using Assignment1.Models;
using Microsoft.AspNetCore.Mvc;

namespace Assignment1.CustomActionResults
{
    public class PersonCsvResult : FileResult
    {
        private readonly IEnumerable<Person> _personData;
        public PersonCsvResult(IEnumerable<Person> personData, string fileDownloadName) : base("text/csv")
        {
            _personData = personData;
            FileDownloadName = fileDownloadName;
        }
        public async override Task ExecuteResultAsync(ActionContext context)
        {
            var response = context.HttpContext.Response;
            context.HttpContext.Response.Headers.Add("Content-Disposition", new[] { "attachment; filename=" + FileDownloadName });
            using (var streamWriter = new StreamWriter(response.Body))
            {
                await streamWriter.WriteLineAsync(
                  $"Firstname, Lastname, Gender"
                );
                foreach (var p in _personData)
                {
                    await streamWriter.WriteLineAsync(
                      $"{p.FirstName}, {p.LastName}, {p.Gender.ToString()}"
                    );
                    await streamWriter.FlushAsync();
                }
                await streamWriter.FlushAsync();
            }
        }
    }
}
