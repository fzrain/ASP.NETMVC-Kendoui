using System;

namespace Fzrain.Core.Domain.Lol
{
   public  class BrilliantTime:BaseEntity
    {
       public Guid ResourceName { get; set; }
       public string Describe { get; set; }
       public virtual  Battle Battle { get; set; }

    }
}
