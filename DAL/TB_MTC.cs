//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class TB_MTC
    {
        public int id { get; set; }
        public string 车牌号码 { get; set; }
        public string 进站时间 { get; set; }
        public string 出站时间 { get; set; }
        public string 进站点 { get; set; }
        public string 中间路径 { get; set; }
        public string 出站点 { get; set; }
        public Nullable<int> 扣费金额 { get; set; }
        public Nullable<int> 状态 { get; set; }
    }
}
