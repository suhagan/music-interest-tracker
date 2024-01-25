using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{

  public class Song
  {
    public int Id { get; set; }
    public string Title { get; set; }

    public virtual Artist Artist { get; set; }
    public virtual Genre Genre { get; set; }
    public virtual ICollection<User> Users { get; set; }
  }
}