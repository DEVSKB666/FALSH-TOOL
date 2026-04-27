using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace ns0
{
	// Token: 0x02000009 RID: 9
	public partial class GForm2 : Form
	{
		// Token: 0x06000035 RID: 53 RVA: 0x0000C410 File Offset: 0x0000A610
		public GForm2()
		{
			this.method_0();
		}

		// Token: 0x06000036 RID: 54 RVA: 0x0000C41E File Offset: 0x0000A61E
		private void button_0_Click(object sender, EventArgs e)
		{
			if (!string.IsNullOrWhiteSpace(this.textBox_0.Text))
			{
				base.Close();
				return;
			}
			MessageBox.Show("Enter password to open the file", "Password empty!", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
		}

		// Token: 0x06000037 RID: 55 RVA: 0x0000C450 File Offset: 0x0000A650
		private void button_1_Click(object sender, EventArgs e)
		{
			this.textBox_0.Text = string.Empty;
			base.Close();
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00017F14 File Offset: 0x00016114
		private void method_0()
		{
			this.textBox_0 = new TextBox();
			this.button_0 = new Button();
			this.button_1 = new Button();
			base.SuspendLayout();
			this.textBox_0.Location = new Point(12, 12);
			this.textBox_0.Name = "TbOpenPassword";
			this.textBox_0.Size = new Size(209, 20);
			this.textBox_0.TabIndex = 1;
			this.button_0.Location = new Point(12, 38);
			this.button_0.Name = "BtnOK";
			this.button_0.Size = new Size(77, 20);
			this.button_0.TabIndex = 2;
			this.button_0.Text = "OK";
			this.button_0.UseVisualStyleBackColor = true;
			this.button_0.Click += this.button_0_Click;
			this.button_1.Location = new Point(144, 38);
			this.button_1.Name = "BtnCancel";
			this.button_1.Size = new Size(77, 20);
			this.button_1.TabIndex = 3;
			this.button_1.Text = "Cancel";
			this.button_1.UseVisualStyleBackColor = true;
			this.button_1.Click += this.button_1_Click;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new Size(233, 69);
			base.Controls.Add(this.button_1);
			base.Controls.Add(this.button_0);
			base.Controls.Add(this.textBox_0);
			base.FormBorderStyle = FormBorderStyle.FixedSingle;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "OpenPassword";
			base.ShowIcon = false;
			base.StartPosition = FormStartPosition.CenterScreen;
			this.Text = "รหัสเปิดXDFและADX";
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x04000029 RID: 41
		private Button button_0;

		// Token: 0x0400002A RID: 42
		private Button button_1;

		// Token: 0x0400002B RID: 43
		public TextBox textBox_0;
	}
}
