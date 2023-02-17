using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Views
{
    public class NotespageFlyoutMenuItem
    {
        public NotespageFlyoutMenuItem()
        {
            TargetType = typeof(NotespageFlyoutMenuItem);
        }
        public int Id { get; set; }
        public string Title { get; set; }

        public Type TargetType { get; set; }
    }
}