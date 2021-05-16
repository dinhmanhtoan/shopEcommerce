using System;
using System.Collections.Generic;
using System.Text;

namespace Model.ViewModel
{
    public class OptionVariationVm
    {
        // biết được sản phẩm nào
        //biết được Size , Color vd: Color
        public long OptionId { get; set; }
        public string OptionName {get;set;}
        // biết được giá trị vd: #fff
        public string OptionValues { get; set; }
    }
}
