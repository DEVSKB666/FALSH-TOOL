using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using <PrivateImplementationDetails>{68F2EF73-9355-4257-ADA6-397CF8BB8E72};

namespace Attr_3
{
	// Token: 0x020000DD RID: 221
	public partial class Type_52 : Form
	{
		// Token: 0x060003CF RID: 975 RVA: 0x00023CA8 File Offset: 0x00021EA8
		public Type_52()
		{
			base.FormBorderStyle = FormBorderStyle.None;
			base.Size = new Size(500, 400);
			this.BackColor = Color.FromArgb(10, 10, 15);
			this.DoubleBuffered = true;
			base.StartPosition = FormStartPosition.CenterParent;
			base.MouseDown += this.\u00A0;
			base.MouseMove += this.\u1680;
			base.MouseUp += this.\u2000;
			this.\u00A0();
		}

		// Token: 0x060003D0 RID: 976 RVA: 0x00023D50 File Offset: 0x00021F50
		private void \u00A0()
		{
			Button button = new Button();
			button.Text = "✕";
			button.Size = new Size(30, 30);
			button.Location = new Point(base.Width - 35, 5);
			button.FlatStyle = FlatStyle.Flat;
			button.FlatAppearance.BorderSize = 0;
			button.ForeColor = Color.White;
			button.Click += this.\u00A0;
			base.Controls.Add(button);
			this.\u00A0 = this.\u00A0(52.4.ToString(), 200, 180);
			this.Attr_2.TextChanged += this.\u1680;
			base.Controls.Add(this.\u00A0);
			this.\u1680 = this.\u00A0(57.9.ToString(), 200, 230);
			this.Attr_3.TextChanged += this.\u2000;
			base.Controls.Add(this.\u1680);
		}

		// Token: 0x060003D1 RID: 977 RVA: 0x00023E6C File Offset: 0x0002206C
		private TextBox \u00A0(string A_1, int A_2, int A_3)
		{
			return new TextBox
			{
				Text = A_1,
				Location = new Point(A_2, A_3),
				Size = new Size(100, 25),
				BackColor = Color.FromArgb(30, 30, 40),
				ForeColor = Color.White,
				BorderStyle = BorderStyle.FixedSingle,
				Font = new Font("Bahnschrift", 12f, FontStyle.Bold),
				TextAlign = HorizontalAlignment.Center
			};
		}

		// Token: 0x060003D2 RID: 978 RVA: 0x00023EE4 File Offset: 0x000220E4
		protected override void OnPaint(PaintEventArgs A_1)
		{
			Graphics graphics = A_1.Graphics;
			graphics.SmoothingMode = SmoothingMode.AntiAlias;
			using (Font font = this.\u00A0("Bahnschrift SemiBold", 14f))
			{
				graphics.DrawString("MZA CC CALCULATOR PRO", font, Brushes.Aqua, 20f, 15f);
			}
			Rectangle rect = new Rectangle(20, 60, 460, 320);
			using (Pen pen = new Pen(Color.FromArgb(50, 0, 255, 200), 2f))
			{
				graphics.DrawRectangle(pen, rect);
			}
			double num = 3.141592653589793 * Math.Pow(this.\u00A0 / 2.0, 2.0) * this.\u1680 / 1000.0;
			using (Font font2 = this.\u00A0("Bahnschrift SemiBold", 45f))
			{
				graphics.DrawString(string.Format("{0:F1} CC", num), font2, Brushes.Lime, 50f, 80f);
			}
			graphics.DrawString("ENGINE DISPLACEMENT RESULT", this.Font, Brushes.Gray, 55f, 145f);
			graphics.DrawString("ลูกสูบ (มม.):", this.Font, Brushes.White, 55f, 185f);
			graphics.DrawString("ชัก (มม.):", this.Font, Brushes.White, 55f, 235f);
			graphics.DrawString("[ PRO TYPING ENABLED ]", this.Font, Brushes.DarkCyan, 55f, 330f);
		}

		// Token: 0x060003D3 RID: 979 RVA: 0x000240AC File Offset: 0x000222AC
		private Font \u00A0(string A_1, float A_2)
		{
			Font result;
			try
			{
				result = new Font(A_1, A_2, FontStyle.Bold);
			}
			catch
			{
				result = new Font(FontFamily.GenericSansSerif, A_2, FontStyle.Bold);
			}
			return result;
		}

		// Token: 0x060003D4 RID: 980 RVA: 0x000240E8 File Offset: 0x000222E8
		[CompilerGenerated]
		private void \u00A0(object A_1, MouseEventArgs A_2)
		{
			this.\u00A0 = true;
			this.\u00A0 = A_2.Location;
		}

		// Token: 0x060003D5 RID: 981 RVA: 0x00024100 File Offset: 0x00022300
		[CompilerGenerated]
		private void \u1680(object A_1, MouseEventArgs A_2)
		{
			if (this.\u00A0)
			{
				base.Location = new Point(base.Location.X - this.Attr_2.X + A_2.X, base.Location.Y - this.Attr_2.Y + A_2.Y);
			}
		}

		// Token: 0x060003D6 RID: 982 RVA: 0x00024162 File Offset: 0x00022362
		[CompilerGenerated]
		private void \u2000(object A_1, MouseEventArgs A_2)
		{
			this.\u00A0 = false;
		}

		// Token: 0x060003D7 RID: 983 RVA: 0x000021C8 File Offset: 0x000003C8
		[CompilerGenerated]
		private void \u00A0(object A_1, EventArgs A_2)
		{
			base.Close();
		}

		// Token: 0x060003D8 RID: 984 RVA: 0x0002416B File Offset: 0x0002236B
		[CompilerGenerated]
		private void \u1680(object A_1, EventArgs A_2)
		{
			double.TryParse(this.Attr_2.Text, out this.\u00A0);
			base.Invalidate();
		}

		// Token: 0x060003D9 RID: 985 RVA: 0x0002418A File Offset: 0x0002238A
		[CompilerGenerated]
		private void \u2000(object A_1, EventArgs A_2)
		{
			double.TryParse(this.Attr_3.Text, out this.\u1680);
			base.Invalidate();
		}

		// Token: 0x040002FF RID: 767
		private bool \u00A0;

		// Token: 0x04000300 RID: 768
		private Point \u00A0;

		// Token: 0x04000301 RID: 769
		private TextBox \u00A0;

		// Token: 0x04000302 RID: 770
		private TextBox \u1680;

		// Token: 0x04000303 RID: 771
		private double \u00A0 = 52.4;

		// Token: 0x04000304 RID: 772
		private double \u1680 = 57.9;
	}
}
