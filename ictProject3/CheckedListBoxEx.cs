using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ictProject3
{
    public class CheckedListBoxEx : CheckedListBox
    {
        public CheckedListBoxEx() { }

        private const int LB_ADDSTRING = 0x180;
        private const int LB_INSERTSTRING = 0x181;
        private const int LB_DELETESTRING = 0x182;
        private const int LB_RESETCONTENT = 0x184;

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == LB_ADDSTRING ||
                m.Msg == LB_INSERTSTRING ||
                m.Msg == LB_DELETESTRING ||
                m.Msg == LB_RESETCONTENT)
            {
                ItemsChanged(this, EventArgs.Empty);
            }
            base.WndProc(ref m);
        }

        public event EventHandler ItemsChanged = delegate { };
    }
}
