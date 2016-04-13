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
                new AccountInfo { QQ ="992270805",QQuin ="U16011369166049456098",Area ="水晶之痕",AreaId ="18",Name ="无处天涯"},
                new AccountInfo { QQ ="992270805",QQuin ="U16011369166049456098",Area ="影流",AreaId ="22",Name ="小鱼儿123"},
                new AccountInfo { QQ ="992270805",QQuin ="U16011369166049456098",Area ="战争学院",AreaId ="8",Name ="两刀死翘翘"},
                new AccountInfo { QQ ="992270805",QQuin ="U16011369166049456098",Area ="卡拉曼达",AreaId ="25",Name ="德玛西亚阵亡"},
                new AccountInfo { QQ ="992270805",QQuin ="U16011369166049456098",Area ="黑色玫瑰",AreaId ="14",Name ="小小人物12345"},
                new AccountInfo { QQ ="1245729165",QQuin ="U5591173367419861910",Area ="征服之海",AreaId ="24",Name ="啊瓜是你哥哥"},
                new AccountInfo { QQ ="992270805",QQuin ="U16011369166049456098",Area ="守望之海",AreaId ="23",Name ="小人物123"},
                new AccountInfo { QQ ="1470637750",QQuin ="U977419047860821799",Area ="钢铁烈阳",AreaId ="17",Name ="白道艹王子"},
                new AccountInfo { QQ ="2458737206",QQuin ="U14085412103664636748",Area ="皮尔特沃夫",AreaId ="7",Name ="Zaki丨天行者"},
            };
        }
    }
}
