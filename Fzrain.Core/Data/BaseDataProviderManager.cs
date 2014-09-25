using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fzrain.Core.Data
{
    public abstract class BaseDataProviderManager
    {
        protected BaseDataProviderManager(DataSettings settings)
        {
            if (settings == null)
                throw new ArgumentNullException("settings");
            this.Settings = settings;
        }
        protected DataSettings Settings { get; private set; }
        public abstract IDataProvider LoadDataProvider();
    }
}
