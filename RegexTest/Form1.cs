using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace RegexTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        Regex regex = null;
        private void btnStart_Click(object sender, EventArgs e)
        {
            regex = new Regex(txtRegex.Text.Trim());
            if (!regex.IsMatch(txtText.Text))
            {
                txtResult.Text = "不能匹配！";
                return;
            }
            MatchCollection matchCollection = regex.Matches(txtText.Text.Trim());
            StringBuilder sb = new StringBuilder();
            sb.Clear();
            int i = 1,j=1;
            foreach(Match match in matchCollection)
            {
                sb.AppendLine(string.Format("第{0}次匹配：{1}", i++, match.Value));
                foreach(Group group in match.Groups)
                {
                    sb.AppendLine(string.Format("第{0}次匹配的第{1}组：{2}", i,j++, group.Value));
                }
            }
            txtResult.Text = sb.ToString();
        }
    }
}
