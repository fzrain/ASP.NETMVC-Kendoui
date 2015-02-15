using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fzrain.Core.Domain.Lol
{
   public  class BrilliantTime:BaseEntity
    {
       public Guid ResourceName { get; set; }
       public string Describe { get; set; }
       public virtual  Battle Battle { get; set; }

    }
}
