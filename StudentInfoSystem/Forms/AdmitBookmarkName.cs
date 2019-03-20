using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Whu_Navigation;

namespace Whu_Navigation
{
    public partial class AdmitBookmarkName : Form
    { 

        //用于保存主窗体对象
        public MainForm m_frmMain;
        public AdmitBookmarkName(MainForm mfrm)
        {
            if (mfrm != null)
            {
                InitializeComponent();
                m_frmMain = mfrm;
            }
        }
        //确认后创建书签
        private void tbAdmit_Click(object sender, EventArgs e)
        {
            if (m_frmMain != null || tbBookmarkName.Text == "")
            {
                m_frmMain.createBookmark(tbBookmarkName.Text);
            }
            this.Close();
        }
    }
}
