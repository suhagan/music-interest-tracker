using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
  public class User
  {
    public int Id { get; set; }
    public string Username { get; set; }

    public virtual ICollection<Artist> Artists { get; set; }
    public virtual ICollection<Genre> Genres { get; set; }
    public virtual ICollection<Song> Songs { get; set; }
  }
}