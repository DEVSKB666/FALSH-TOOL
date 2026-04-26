using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using <PrivateImplementationDetails>{68F2EF73-9355-4257-ADA6-397CF8BB8E72};

namespace Attr_2
{
	// Token: 0x02000009 RID: 9
	public partial class Form_9 : Form
	{
		// Token: 0x06000035 RID: 53 RVA: 0x000040FC File Offset: 0x000022FC
		public Form_9()
		{
			this.M_36();
		}

		// Token: 0x06000036 RID: 54 RVA: 0x0000410A File Offset: 0x0000230A
		private void M_36(object A_1, EventArgs A_2)
		{
			if (!string.IsNullOrWhiteSpace(this.M_36.Text))
			{
				base.Close();
				return;
			}
			MessageBox.Show("Enter password to open the file", "Password empty!", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
		}

		// Token: 0x06000037 RID: 55 RVA: 0x0000413C File Offset: 0x0000233C
		private void M_37(object A_1, EventArgs A_2)
		{
			this.M_36.Text = string.Empty;
			base.Close();
		}

		// Token: 0x06000039 RID: 57 RVA: 0x0000417C File Offset: 0x0000237C
		private void M_36()
		{
			this.M_36 = new TextBox();
			this.M_36 = new Button();
			this.M_37 = new Button();
			base.SuspendLayout();
			this.M_36.Location = new Point(12, 12);
			this.M_36.Name = "TbOpenPassword";
			this.M_36.Size = new Size(209, 20);
			this.M_36.TabIndex = 1;
			this.M_36.Location = new Point(12, 38);
			this.M_36.Name = "BtnOK";
			this.M_36.Size = new Size(77, 20);
			this.M_36.TabIndex = 2;
			this.M_36.Text = "OK";
			this.M_36.UseVisualStyleBackColor = true;
			this.M_36.Click += this.M_36;
			this.M_37.Location = new Point(144, 38);
			this.M_37.Name = "BtnCancel";
			this.M_37.Size = new Size(77, 20);
			this.M_37.TabIndex = 3;
			this.M_37.Text = "Cancel";
			this.M_37.UseVisualStyleBackColor = true;
			this.M_37.Click += this.M_37;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new Size(233, 69);
			base.Controls.Add(this.M_37);
			base.Controls.Add(this.M_36);
			base.Controls.Add(this.M_36);
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
		private Button M_36;

		// Token: 0x0400002A RID: 42
		private Button M_37;

		// Token: 0x0400002B RID: 43
		public TextBox M_36;
	}
}
