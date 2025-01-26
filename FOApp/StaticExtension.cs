using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FOApp
{
    [ContentProperty("Member")]
    public class StaticExtension : IMarkupExtension
    {
        public string Member { get; set; }
        public object ProvideValue(IServiceProvider serviceProvider)
        {
            return MainPage.MyFontSize;
        }
    }
   
}
