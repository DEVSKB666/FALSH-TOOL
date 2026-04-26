using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using <PrivateImplementationDetails>{68F2EF73-9355-4257-ADA6-397CF8BB8E72};

namespace Attr_2
{
	// Token: 0x02000004 RID: 4
	public partial class Form_4 : Form
	{
		// Token: 0x06000003 RID: 3 RVA: 0x00002067 File Offset: 0x00000267
		public Form_4()
		{
			this.M_4();
		}

		// Token: 0x06000004 RID: 4 RVA: 0x0000208B File Offset: 0x0000028B
		private void M_4(object A_1, EventArgs A_2)
		{
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002090 File Offset: 0x00000290
		public static string M_4(string A_0, string A_1)
		{
			string result = "";
			RijndaelManaged rijndaelManaged = new RijndaelManaged();
			MD5CryptoServiceProvider md5CryptoServiceProvider = new MD5CryptoServiceProvider();
			try
			{
				byte[] array = new byte[32];
				byte[] sourceArray = md5CryptoServiceProvider.ComputeHash(Encoding.ASCII.GetBytes(A_1));
				Array.Copy(sourceArray, 0, array, 0, 16);
				Array.Copy(sourceArray, 0, array, 15, 16);
				rijndaelManaged.Key = array;
				rijndaelManaged.Mode = CipherMode.ECB;
				ICryptoTransform cryptoTransform = rijndaelManaged.CreateDecryptor();
				byte[] array2 = Convert.FromBase64String(A_0);
				result = Encoding.UTF8.GetString(cryptoTransform.TransformFinalBlock(array2, 0, array2.Length));
			}
			catch (Exception)
			{
			}
			return result;
		}

		// Token: 0x06000006 RID: 6 RVA: 0x0000212C File Offset: 0x0000032C
		public string M_6(string A_1, string A_2)
		{
			RijndaelManaged rijndaelManaged = new RijndaelManaged();
			MD5CryptoServiceProvider md5CryptoServiceProvider = new MD5CryptoServiceProvider();
			string result = "";
			try
			{
				byte[] array = new byte[32];
				byte[] sourceArray = md5CryptoServiceProvider.ComputeHash(Encoding.BigEndianUnicode.GetBytes(A_2));
				Array.Copy(sourceArray, 0, array, 6, 16);
				Array.Copy(sourceArray, 0, array, 2, 16);
				rijndaelManaged.Key = array;
				rijndaelManaged.Mode = CipherMode.ECB;
				ICryptoTransform cryptoTransform = rijndaelManaged.CreateDecryptor();
				byte[] array2 = Convert.FromBase64String(A_1);
				result = Encoding.UTF7.GetString(cryptoTransform.TransformFinalBlock(array2, 0, array2.Length));
			}
			catch (Exception)
			{
			}
			return result;
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000021C8 File Offset: 0x000003C8
		private void M_6(object A_1, EventArgs A_2)
		{
			base.Close();
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000021D0 File Offset: 0x000003D0
		private void \u2000(object A_1, EventArgs A_2)
		{
			string path = string.Empty;
			OpenFileDialog openFileDialog = new OpenFileDialog();
			openFileDialog.Filter = "ACG Files(*.acg)|*.acg";
			openFileDialog.Multiselect = true;
			if (openFileDialog.ShowDialog() == DialogResult.OK)
			{
				path = openFileDialog.FileName;
				this.M_4.Text = File.ReadAllText(path);
			}
			string text = "";
			int num = 43712;
			string text2 = this.M_4.Text;
			try
			{
				double num2 = (double)text2.Length / (double)num - 1.0;
				for (double num3 = 0.0; num3 <= num2; num3 += 1.0)
				{
					string text3 = text2.Substring((int)Math.Round(Math.Round(num3 * (double)num)), num);
					text += global::Attr_2.Form_4.\u00A0(text3, this.M_6).ToString();
				}
			}
			catch (Exception)
			{
			}
			this.M_6.Text = text;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000022C4 File Offset: 0x000004C4
		private void M_9(object A_1, EventArgs A_2)
		{
			OpenFileDialog openFileDialog = new OpenFileDialog();
			openFileDialog.Filter = "ECU Files(*.ECU)|*.ECU";
			openFileDialog.Multiselect = true;
			if (openFileDialog.ShowDialog() == DialogResult.OK)
			{
				string fileName = openFileDialog.FileName;
				this.M_4.Text = File.ReadAllText(fileName);
			}
			string text = "";
			int num = 43712;
			string text2 = this.M_4.Text;
			try
			{
				double num2 = 0.0;
				double num3 = (double)text2.Length / (double)num - 1.0;
				for (double num4 = num2; num4 <= num3; num4 += 1.0)
				{
					string text3 = text2.Substring(checked((int)Math.Round(unchecked(num4 * (double)num))), num);
					text += global::Attr_2.Form_4.\u00A0(text3, this.M_4).ToString();
				}
			}
			catch (Exception)
			{
			}
			this.M_6.Clear();
			this.M_6.Text = text;
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000023B8 File Offset: 0x000005B8
		private void M_8(object A_1, EventArgs A_2)
		{
			Process.Start("C:\\Program Files\\HxD\\HXD.exe");
		}

		// Token: 0x0600000B RID: 11 RVA: 0x0000208B File Offset: 0x0000028B
		private void M_9(object A_1, EventArgs A_2)
		{
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000023EC File Offset: 0x000005EC
		private void M_4()
		{
			this.M_4 = new Button();
			this.M_6 = new Button();
			this.M_8 = new Button();
			this.M_4 = new RichTextBox();
			this.M_6 = new RichTextBox();
			this.M_4 = new GroupBox();
			this.M_4.SuspendLayout();
			base.SuspendLayout();
			this.M_4.BackColor = Color.Red;
			this.M_4.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.M_4.Location = new Point(190, 12);
			this.M_4.Name = "button2";
			this.M_4.Size = new Size(172, 30);
			this.M_4.TabIndex = 0;
			this.M_4.Text = "ACG TO BIN";
			this.M_4.UseVisualStyleBackColor = false;
			this.M_4.Click += this.M_8;
			this.M_6.BackColor = Color.Cyan;
			this.M_6.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.M_6.Location = new Point(12, 12);
			this.M_6.Name = "button3";
			this.M_6.Size = new Size(172, 31);
			this.M_6.TabIndex = 2;
			this.M_6.Text = "HXD EDITOR";
			this.M_6.UseVisualStyleBackColor = false;
			this.M_6.Click += this.M_8;
			this.M_8.BackColor = Color.Red;
			this.M_8.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.M_8.Location = new Point(368, 12);
			this.M_8.Name = "button1";
			this.M_8.Size = new Size(172, 33);
			this.M_8.TabIndex = 3;
			this.M_8.Text = "ECU TO BIN";
			this.M_8.UseVisualStyleBackColor = false;
			this.M_8.Click += this.M_9;
			this.M_4.Location = new Point(12, 435);
			this.M_4.Name = "rctLockFile";
			this.M_4.Size = new Size(549, 107);
			this.M_4.TabIndex = 4;
			this.M_4.Text = "";
			this.M_6.Location = new Point(6, 19);
			this.M_6.Name = "rtbBin";
			this.M_6.Size = new Size(516, 66);
			this.M_6.TabIndex = 5;
			this.M_6.Text = "";
			this.M_4.Controls.Add(this.M_6);
			this.M_4.ForeColor = Color.White;
			this.M_4.ImeMode = ImeMode.On;
			this.M_4.Location = new Point(12, 51);
			this.M_4.Name = "groupBox1";
			this.M_4.Size = new Size(528, 92);
			this.M_4.TabIndex = 6;
			this.M_4.TabStop = false;
			this.M_4.Text = "ข้อมูลที่แปลงสำเร็จแล้ว";
			this.BackColor = Color.Black;
			base.ClientSize = new Size(549, 155);
			base.Controls.Add(this.M_4);
			base.Controls.Add(this.M_4);
			base.Controls.Add(this.M_8);
			base.Controls.Add(this.M_6);
			base.Controls.Add(this.M_4);
			base.FormBorderStyle = FormBorderStyle.Fixed3D;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "Form3";
			base.ShowIcon = false;
			base.SizeGripStyle = SizeGripStyle.Show;
			base.StartPosition = FormStartPosition.CenterScreen;
			this.Text = "แปลงไฟล์ .ACG&.ECUเป็น BIN";
			base.TopMost = true;
			base.Load += this.M_9;
			this.M_4.ResumeLayout(false);
			base.ResumeLayout(false);
		}

		// Token: 0x04000002 RID: 2
		private string M_4 = "@ecu_homdaa!&2023*mnhdaK#^&hcbaHBQD@0lanmBV#!";

		// Token: 0x04000003 RID: 3
		private string M_6 = "@Shinden@9919";

		// Token: 0x04000005 RID: 5
		private Button M_4;

		// Token: 0x04000006 RID: 6
		private Button M_6;

		// Token: 0x04000007 RID: 7
		private Button \u2000;

		// Token: 0x04000008 RID: 8
		private RichTextBox M_4;

		// Token: 0x04000009 RID: 9
		private RichTextBox M_6;

		// Token: 0x0400000A RID: 10
		public GroupBox M_4;
	}
}
