using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCashbook.Core.Entity;
public class FinMaster
{
  [Key]
  public int FinId { get; set; }
  public string? FinYear { get; set; } = string.Empty;
  public DateTime? CreatedDate { get; set; }
  public string CreatedBy {  get; set; } = string.Empty;
  public DateTime? ModifiedDate { get; set; }
  public string ModifiedBy { get; set; } = string.Empty;
  public bool IsActive {  get; set; }
  public bool IsDeleted { get; set; }
}
