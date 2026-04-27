using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace ns0
{
	// Token: 0x02000004 RID: 4
	public partial class GForm0 : Form
	{
		// Token: 0x06000003 RID: 3 RVA: 0x0000C2DF File Offset: 0x0000A4DF
		public GForm0()
		{
			this.method_3();
		}

		// Token: 0x06000004 RID: 4 RVA: 0x0000C303 File Offset: 0x0000A503
		private void method_0(object sender, EventArgs e)
		{
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00015FC4 File Offset: 0x000141C4
		public static string smethod_0(string string_2, string string_3)
		{
			string result = "";
			RijndaelManaged rijndaelManaged = new RijndaelManaged();
			MD5CryptoServiceProvider md5CryptoServiceProvider = new MD5CryptoServiceProvider();
			try
			{
				byte[] array = new byte[32];
				byte[] sourceArray = md5CryptoServiceProvider.ComputeHash(Encoding.ASCII.GetBytes(string_3));
				Array.Copy(sourceArray, 0, array, 0, 16);
				Array.Copy(sourceArray, 0, array, 15, 16);
				rijndaelManaged.Key = array;
				rijndaelManaged.Mode = CipherMode.ECB;
				ICryptoTransform cryptoTransform = rijndaelManaged.CreateDecryptor();
				byte[] array2 = Convert.FromBase64String(string_2);
				result = Encoding.UTF8.GetString(cryptoTransform.TransformFinalBlock(array2, 0, array2.Length));
			}
			catch (Exception)
			{
			}
			return result;
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00016060 File Offset: 0x00014260
		public string method_1(string string_2, string string_3)
		{
			RijndaelManaged rijndaelManaged = new RijndaelManaged();
			MD5CryptoServiceProvider md5CryptoServiceProvider = new MD5CryptoServiceProvider();
			string result = "";
			try
			{
				byte[] array = new byte[32];
				byte[] sourceArray = md5CryptoServiceProvider.ComputeHash(Encoding.BigEndianUnicode.GetBytes(string_3));
				Array.Copy(sourceArray, 0, array, 6, 16);
				Array.Copy(sourceArray, 0, array, 2, 16);
				rijndaelManaged.Key = array;
				rijndaelManaged.Mode = CipherMode.ECB;
				ICryptoTransform cryptoTransform = rijndaelManaged.CreateDecryptor();
				byte[] array2 = Convert.FromBase64String(string_2);
				result = Encoding.UTF7.GetString(cryptoTransform.TransformFinalBlock(array2, 0, array2.Length));
			}
			catch (Exception)
			{
			}
			return result;
		}

		// Token: 0x06000007 RID: 7 RVA: 0x0000C305 File Offset: 0x0000A505
		private void method_2(object sender, EventArgs e)
		{
			base.Close();
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000160FC File Offset: 0x000142FC
		private void button_0_Click(object sender, EventArgs e)
		{
			string path = string.Empty;
			OpenFileDialog openFileDialog = new OpenFileDialog();
			openFileDialog.Filter = "ACG Files(*.acg)|*.acg";
			openFileDialog.Multiselect = true;
			if (openFileDialog.ShowDialog() == DialogResult.OK)
			{
				path = openFileDialog.FileName;
				this.richTextBox_0.Text = File.ReadAllText(path);
			}
			string text = "";
			int num = 43712;
			string text2 = this.richTextBox_0.Text;
			try
			{
				double num2 = (double)text2.Length / (double)num - 1.0;
				for (double num3 = 0.0; num3 <= num2; num3 += 1.0)
				{
					string string_ = text2.Substring((int)Math.Round(Math.Round(num3 * (double)num)), num);
					text += GForm0.smethod_0(string_, this.string_1).ToString();
				}
			}
			catch (Exception)
			{
			}
			this.richTextBox_1.Text = text;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000161F0 File Offset: 0x000143F0
		private void button_2_Click(object sender, EventArgs e)
		{
			OpenFileDialog openFileDialog = new OpenFileDialog();
			openFileDialog.Filter = "ECU Files(*.ECU)|*.ECU";
			openFileDialog.Multiselect = true;
			if (openFileDialog.ShowDialog() == DialogResult.OK)
			{
				string fileName = openFileDialog.FileName;
				this.richTextBox_0.Text = File.ReadAllText(fileName);
			}
			string text = "";
			int num = 43712;
			string text2 = this.richTextBox_0.Text;
			try
			{
				double num2 = 0.0;
				double num3 = (double)text2.Length / (double)num - 1.0;
				for (double num4 = num2; num4 <= num3; num4 += 1.0)
				{
					string string_ = text2.Substring(checked((int)Math.Round(unchecked(num4 * (double)num))), num);
					text += GForm0.smethod_0(string_, this.string_0).ToString();
				}
			}
			catch (Exception)
			{
			}
			this.richTextBox_1.Clear();
			this.richTextBox_1.Text = text;
		}

		// Token: 0x0600000A RID: 10 RVA: 0x0000C30D File Offset: 0x0000A50D
		private void button_1_Click(object sender, EventArgs e)
		{
			Process.Start("C:\\Program Files\\HxD\\HXD.exe");
		}

		// Token: 0x0600000B RID: 11 RVA: 0x0000C303 File Offset: 0x0000A503
		private void GForm0_Load(object sender, EventArgs e)
		{
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000162E4 File Offset: 0x000144E4
		private void method_3()
		{
			this.button_0 = new Button();
			this.button_1 = new Button();
			this.button_2 = new Button();
			this.richTextBox_0 = new RichTextBox();
			this.richTextBox_1 = new RichTextBox();
			this.groupBox_0 = new GroupBox();
			this.groupBox_0.SuspendLayout();
			base.SuspendLayout();
			this.button_0.BackColor = Color.Red;
			this.button_0.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.button_0.Location = new Point(190, 12);
			this.button_0.Name = "button2";
			this.button_0.Size = new Size(172, 30);
			this.button_0.TabIndex = 0;
			this.button_0.Text = "ACG TO BIN";
			this.button_0.UseVisualStyleBackColor = false;
			this.button_0.Click += this.button_0_Click;
			this.button_1.BackColor = Color.Cyan;
			this.button_1.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.button_1.Location = new Point(12, 12);
			this.button_1.Name = "button3";
			this.button_1.Size = new Size(172, 31);
			this.button_1.TabIndex = 2;
			this.button_1.Text = "HXD EDITOR";
			this.button_1.UseVisualStyleBackColor = false;
			this.button_1.Click += this.button_1_Click;
			this.button_2.BackColor = Color.Red;
			this.button_2.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.button_2.Location = new Point(368, 12);
			this.button_2.Name = "button1";
			this.button_2.Size = new Size(172, 33);
			this.button_2.TabIndex = 3;
			this.button_2.Text = "ECU TO BIN";
			this.button_2.UseVisualStyleBackColor = false;
			this.button_2.Click += this.button_2_Click;
			this.richTextBox_0.Location = new Point(12, 435);
			this.richTextBox_0.Name = "rctLockFile";
			this.richTextBox_0.Size = new Size(549, 107);
			this.richTextBox_0.TabIndex = 4;
			this.richTextBox_0.Text = "";
			this.richTextBox_1.Location = new Point(6, 19);
			this.richTextBox_1.Name = "rtbBin";
			this.richTextBox_1.Size = new Size(516, 66);
			this.richTextBox_1.TabIndex = 5;
			this.richTextBox_1.Text = "";
			this.groupBox_0.Controls.Add(this.richTextBox_1);
			this.groupBox_0.ForeColor = Color.White;
			this.groupBox_0.ImeMode = ImeMode.On;
			this.groupBox_0.Location = new Point(12, 51);
			this.groupBox_0.Name = "groupBox1";
			this.groupBox_0.Size = new Size(528, 92);
			this.groupBox_0.TabIndex = 6;
			this.groupBox_0.TabStop = false;
			this.groupBox_0.Text = "ข้อมูลที่แปลงสำเร็จแล้ว";
			this.BackColor = Color.Black;
			base.ClientSize = new Size(549, 155);
			base.Controls.Add(this.groupBox_0);
			base.Controls.Add(this.richTextBox_0);
			base.Controls.Add(this.button_2);
			base.Controls.Add(this.button_1);
			base.Controls.Add(this.button_0);
			base.FormBorderStyle = FormBorderStyle.Fixed3D;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "Form3";
			base.ShowIcon = false;
			base.SizeGripStyle = SizeGripStyle.Show;
			base.StartPosition = FormStartPosition.CenterScreen;
			this.Text = "แปลงไฟล์ .ACG&.ECUเป็น BIN";
			base.TopMost = true;
			base.Load += this.GForm0_Load;
			this.groupBox_0.ResumeLayout(false);
			base.ResumeLayout(false);
		}

		// Token: 0x04000002 RID: 2
		private string string_0 = "@ecu_homdaa!&2023*mnhdaK#^&hcbaHBQD@0lanmBV#!";

		// Token: 0x04000003 RID: 3
		private string string_1 = "@Shinden@9919";

		// Token: 0x04000005 RID: 5
		private Button button_0;

		// Token: 0x04000006 RID: 6
		private Button button_1;

		// Token: 0x04000007 RID: 7
		private Button button_2;

		// Token: 0x04000008 RID: 8
		private RichTextBox richTextBox_0;

		// Token: 0x04000009 RID: 9
		private RichTextBox richTextBox_1;

		// Token: 0x0400000A RID: 10
		public GroupBox groupBox_0;
	}
}
