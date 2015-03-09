using System.ComponentModel;

namespace Fzrain.Core.Domain.Configuration
{
    /// <summary>
    /// Represents a setting
    /// </summary>
    public partial class Setting : BaseEntity
    {
    
       [DisplayName("参数名")]
        public string Name { get; set; }

        [DisplayName("参数值")]
        public string Value { get; set; }

   
        public string  Describe { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
