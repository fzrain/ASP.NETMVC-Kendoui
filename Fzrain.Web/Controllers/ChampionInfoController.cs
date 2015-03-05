using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Fzrain.Core.Data;
using Fzrain.Core.Domain.Lol;

namespace Fzrain.Web.Controllers
{
    public class ChampionInfoController : BaseListController<ChampionInfo>
    {
        public ChampionInfoController(IRepository<ChampionInfo> repository) : base(repository)
        {
        }
    }
}