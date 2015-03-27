using System;
using Fzrain.Core;
using Fzrain.Core.Data;

namespace Fzrain.Data
{
    public partial class EfDataProviderManager : BaseDataProviderManager
    {
        public EfDataProviderManager(DataSettings settings)
            : base(settings)
        {
        }

        public override IDataProvider LoadDataProvider()
        {

            var providerName = Settings.DataProvider;
            if (String.IsNullOrWhiteSpace(providerName))
                throw new FzrainException("Data Settings doesn't contain a providerName");

            switch (providerName.ToLowerInvariant())
            {
                case "sqlserver":
                    return new SqlServerDataProvider();
               // case "sqlce":
                  //  return new SqlCeDataProvider();
                default:
                    throw new FzrainException(string.Format("Not supported dataprovider name: {0}", providerName));
            }
        }

    }
}
