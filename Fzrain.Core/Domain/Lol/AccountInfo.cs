using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fzrain.Core.Domain.Lol
{
    public class AccountInfo
    {
        public string Area { get; set; }
        public string  AreaId { get; set; }
        public string  QQ { get; set; }
        public string  QQuin { get; set; }
        public string  Name { get; set; }

        public static IEnumerable<AccountInfo> Data()
        {
            return new List<AccountInfo>
            {
                new AccountInfo { QQ ="250970574",QQuin ="U7796213511838403286",Area ="雷瑟守备",AreaId ="11",Name ="网络中断突然"},
                new AccountInfo { QQ ="250970574",QQuin ="U7796213511838403286",Area ="皮城警备",AreaId ="27",Name ="笨笨秒杀上帝"},
                new AccountInfo { QQ ="150490252",QQuin ="U3040557788097032300",Area ="艾欧尼亚",AreaId ="1",Name ="名将秒鱼"},
                new AccountInfo { QQ ="2241811791",QQuin ="U15978645692141229169",Area ="祖安",AreaId ="3",Name ="MyE丶血狱"},
                new AccountInfo { QQ ="1411085735",QQuin ="U4795783785692160110",Area ="裁决之地",AreaId ="13",Name ="maoxiahui1234"},
            };
        }
    }
}
