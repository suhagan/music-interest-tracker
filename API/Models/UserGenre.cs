using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{

  public class UserGenre
  {
    public int Id { get; set; }

    public virtual User User { get; set; }
    public virtual Genre Genre { get; set; }
  }
}