using System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{

  public class UserSong
  {
    public int Id { get; set; }

    public virtual User User { get; set; }
    public virtual Song Song { get; set; }
  }
}