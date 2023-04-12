namespace ACMELegalApp.Models
{
    public class Case
    {
        public int CaseID { get; set; }
        public string CaseNumber { get; set; }
        public string CaseName { get; set; }
        public string CaseDescription { get; set; }
        public DateTime FilingDate { get; set; }
        public string Status { get; set; }
        public int AssignedTo { get; set; }
    }
}
