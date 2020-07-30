using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Forms;
using ListBox = System.Windows.Forms.ListBox;

namespace RegexTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            regs.Add(new KeyValue("E-MAIL", @"^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$"));
            regs.Add(new KeyValue("域名", @"[a-zA-Z0-9][-a-zA-Z0-9]{0,62}(/.[a-zA-Z0-9][-a-zA-Z0-9]{0,62})+/.?"));
            regs.Add(new KeyValue("InternetURL", @"[a-zA-z]+://[^\s]* 或 ^http://([\w-]+\.)+[\w-]+(/[\w-./?%&=]*)?$"));
            regs.Add(new KeyValue("手机号码", @"^(13[0-9]|14[5|7]|15[0|1|2|3|5|6|7|8|9]|18[0|1|2|3|5|6|7|8|9])\d{8}$"));
            //电话号码(“XXX-XXXXXXX”、”XXXX-XXXXXXXX”、”XXX-XXXXXXX”、”XXX-XXXXXXXX”、”XXXXXXX”和”XXXXXXXX)
            regs.Add(new KeyValue("电话号码", @"^(\d{3,4}-)|(\d{3.4}-)?\d{7,8}$"));
            regs.Add(new KeyValue("国内电话号码", @"\d{3}-\d{8}|\d{4}-\d{7}"));
            regs.Add(new KeyValue("身份证号", @"^([0-9]){7,18}(x|X)?$ 或 ^\d{8,18}|[0-9x]{8,18}|[0-9X]{8,18}?$"));
            //帐号是否合法(字母开头，允许5-16字节，允许字母数字下划线)            
            regs.Add(new KeyValue("帐号是否合法", @"^[a-zA-Z][a-zA-Z0-9_]{4,15}$"));
            //密码(以字母开头，长度在6~18之间，只能包含字母、数字和下划线)
            regs.Add(new KeyValue("密码", @"^[a-zA-Z]\w{5,17}$"));
            Regex reg = new Regex(@"^[a-zA-Z]\w{5,17}$");
            //强密码(必须包含大小写字母和数字的组合，不能使用特殊字符，长度在8-10之间)            
            regs.Add(new KeyValue("强密码", @"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{8,10}$"));
            regs.Add(new KeyValue("中文字符", @"[\u4e00-\u9fa5]"));
            regs.Add(new KeyValue("IP地址", @"((?:(?:25[0-5]|2[0-4]\\d|[01]?\\d?\\d)\\.){3}(?:25[0-5]|2[0-4]\\d|[01]?\\d?\\d))"));
            foreach(KeyValue kv in regs)
            {               
                lbRegex.Items.Add(kv);
            }
        }
        struct KeyValue
        {
            public string Key { get; set; }
            public string Value { get; set; }
            public KeyValue(string key,string value)
            {
                Key = key;
                Value = value;
            }
            public override string ToString()
            {
                return Key;
            }
        }
        Regex regex = null;
        List<KeyValue> regs = new List<KeyValue>();

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
            int i = 0, j = 0;
            foreach (Match match in matchCollection)
            {
                sb.AppendLine(string.Format("第{0}次匹配：{1}", i++, match.Value));
                foreach (Group group in match.Groups)
                {
                    sb.AppendLine(string.Format("第{0}次匹配的第{1}组：{2}", i, j++, group.Value));
                }
            }
            txtResult.Text = sb.ToString();
        }

        private void lbRegex_MeasureItem(object sender, MeasureItemEventArgs e)
        {
            e.ItemHeight = 26;
        }

        private void lbRegex_DrawItem(object sender, DrawItemEventArgs e)
        {
            try
            {
                e.DrawBackground();
                e.DrawFocusRectangle();
                ////让文字位于Item的中间
                //float difH = (e.Bounds.Height – e.Font.Height) / 2;
                //RectangleF rf = new RectangleF(e.Bounds.X, e.Bounds.Y + difH, e.Bounds.Width, e.Font.Height);
                //e.Graphics.DrawString(listBox1.Items[e.Index].ToString(), e.Font, new SolidBrush(e.ForeColor), rf);
                e.Graphics.DrawString(((ListBox)sender).Items[e.Index].ToString(), e.Font, new SolidBrush(Color.Black), e.Bounds);
            }
            catch
            { }
        }

        private void lbRegex_SelectedIndexChanged(object sender, EventArgs e)
        {
            KeyValue kv = (KeyValue)lbRegex.Items[lbRegex.SelectedIndex];
            Clipboard.SetText(kv.Value);
            txtRegex.Paste();
        }
    }
}
