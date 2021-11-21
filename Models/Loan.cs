using System.ComponentModel.DataAnnotations.Schema;

namespace RacketManagement.Models
{
  public class Loan
  {
    public int LoanID { get; set; }
    
    [ForeignKey("ApplicationUser")]
    public string UserId { get; set; }
    public int RacketID { get; set; }
    public ApplicationUser ApplicationUser { get; set; }
    public Racket Racket { get; set; }
  }
}