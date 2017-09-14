using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Kiosk
{
	public partial class CustomMessageBox : Form
	{
		public CustomMessageBox()
		{
			InitializeComponent();
		}
		static CustomMessageBox MsgBox; static DialogResult result = DialogResult.No;
		public static DialogResult Show(string text, string caption, string btnOK, string btnCancel) {
			MsgBox = new CustomMessageBox();			
			MsgBox.label1.Text = text;
			MsgBox.button1.Text = btnOK;
			MsgBox.button2.Text = btnCancel;
			MsgBox.ShowDialog();
			return result;
		}

		private void button1_Click(object sender, EventArgs e)
		{
			result = DialogResult.Yes; MsgBox.Close();
		}

        private void button2_Click(object sender, EventArgs e)
        {
            result = DialogResult.No; MsgBox.Close();
        }
    }
}
