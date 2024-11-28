using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCashbook.Core.Entity;
public class UserCredential
{
  [Key]
  public long UserId { get; set; }
  public long RegId { get; set; }
  public string? Username { get; set; }
  public string? Password { get; set; }
  public DateTime CreatedDate { get; set; }
  public string? CreatedBy {  get; set; }
  public DateTime? ModifiedDate { get; set;}
  public string? ModifiedBy { get; set;}
  public bool IsActive { get; set; }
  public bool IsDeleted { get; set; }

}
