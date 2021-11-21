using System.ComponentModel.DataAnnotations.Schema;

namespace RacketManagement.Models
{
  public class Racket
  {
    public int RacketID { get; set; }
    [ForeignKey("Brand")]
    public int BrandID { get; set; }
    public int GripSizeID { get; set; }
    public int ModelID { get; set; }

    public Brand Brand { get; set; }
    public GripSize GripSize { get; set; }
    public Model Model { get; set; }
  }
}