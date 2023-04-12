using ACMELegalApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using Dapper;

namespace ACMELegalApp.Controllers
{
    public class CasesController : Controller
    {
        private readonly string _connectionString;

        public CasesController(IConfiguration configuration)
        {
            _connectionString = "Server=LAPTOP-RVSGHA90\\SQLEXPRESS;Database=ACMELegalApp;Trusted_Connection=True;MultipleActiveResultSets=true";

        }

        public IActionResult AllCases()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var cases = connection.Query<Case>("GetAllCases", commandType: CommandType.StoredProcedure).ToList();

                return View(cases);
            }
        }

        // ToDo: Add a method to get a single case by ID
        public IActionResult Details(int id) 
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var cases = connection.Query<Case>("GetAllCases", commandType: CommandType.StoredProcedure).ToList();

                return View(cases.FirstOrDefault(c => c.CaseID == id));
            }
        }

        [HttpGet]
        public IActionResult GetAllCases()
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var cases = connection.Query<Case>("GetAllCases", commandType: System.Data.CommandType.StoredProcedure).ToList();

                return Ok(cases);
            }
        }

        [HttpGet("{userID}")]
        public IActionResult GetCasesByUser(int userID)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var parameters = new DynamicParameters();
                parameters.Add("@UserID", userID);

                var cases = connection.Query<Case>("GetCasesByUser", parameters, commandType: System.Data.CommandType.StoredProcedure).ToList();

                return Ok(cases);
            }
        }

        [HttpDelete("{caseID}")]
        public IActionResult DeleteCase(int caseID)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var parameters = new DynamicParameters();
                parameters.Add("@CaseID", caseID);

                connection.Execute("DeleteCase", parameters, commandType: System.Data.CommandType.StoredProcedure);

                return NoContent();
            }
        }

        [HttpPut("{caseID}")]
        public IActionResult UpdateCase(int caseID, [FromBody] Case updatedCase)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var parameters = new DynamicParameters();
                parameters.Add("@CaseID", caseID);
                parameters.Add("@CaseNumber", updatedCase.CaseNumber);
                parameters.Add("@CaseName", updatedCase.CaseName);
                parameters.Add("@CaseDescription", updatedCase.CaseDescription);
                parameters.Add("@FilingDate", updatedCase.FilingDate);
                parameters.Add("@Status", updatedCase.Status);
                parameters.Add("@AssignedTo", updatedCase.AssignedTo);

                connection.Execute("UpdateCase", parameters, commandType: System.Data.CommandType.StoredProcedure);

                return NoContent();
            }
        }
    }
}
