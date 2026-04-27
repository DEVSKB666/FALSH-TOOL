using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace ns1
{
	// Token: 0x020000DD RID: 221
	public partial class GForm14 : Form
	{
		// Token: 0x060003CF RID: 975 RVA: 0x000362EC File Offset: 0x000344EC
		public GForm14()
		{
			base.FormBorderStyle = FormBorderStyle.None;
			base.Size = new Size(500, 400);
			this.BackColor = Color.FromArgb(10, 10, 15);
			this.DoubleBuffered = true;
			base.StartPosition = FormStartPosition.CenterParent;
			base.MouseDown += this.GForm14_MouseDown;
			base.MouseMove += this.GForm14_MouseMove;
			base.MouseUp += this.GForm14_MouseUp;
			this.method_0();
		}

		// Token: 0x060003D0 RID: 976 RVA: 0x00036394 File Offset: 0x00034594
		private void method_0()
		{
			Button button = new Button();
			button.Text = "✕";
			button.Size = new Size(30, 30);
			button.Location = new Point(base.Width - 35, 5);
			button.FlatStyle = FlatStyle.Flat;
			button.FlatAppearance.BorderSize = 0;
			button.ForeColor = Color.White;
			button.Click += this.method_3;
			base.Controls.Add(button);
			this.textBox_0 = this.method_1(52.4.ToString(), 200, 180);
			this.textBox_0.TextChanged += this.textBox_0_TextChanged;
			base.Controls.Add(this.textBox_0);
			this.textBox_1 = this.method_1(57.9.ToString(), 200, 230);
			this.textBox_1.TextChanged += this.textBox_1_TextChanged;
			base.Controls.Add(this.textBox_1);
		}

		// Token: 0x060003D1 RID: 977 RVA: 0x000364B0 File Offset: 0x000346B0
		private TextBox method_1(string string_0, int int_0, int int_1)
		{
			return new TextBox
			{
				Text = string_0,
				Location = new Point(int_0, int_1),
				Size = new Size(100, 25),
				BackColor = Color.FromArgb(30, 30, 40),
				ForeColor = Color.White,
				BorderStyle = BorderStyle.FixedSingle,
				Font = new Font("Bahnschrift", 12f, FontStyle.Bold),
				TextAlign = HorizontalAlignment.Center
			};
		}

		// Token: 0x060003D2 RID: 978 RVA: 0x00036528 File Offset: 0x00034728
		protected override void OnPaint(PaintEventArgs pevent)
		{
			Graphics graphics = pevent.Graphics;
			graphics.SmoothingMode = SmoothingMode.AntiAlias;
			using (Font font = this.method_2("Bahnschrift SemiBold", 14f))
			{
				graphics.DrawString("MZA CC CALCULATOR PRO", font, Brushes.Aqua, 20f, 15f);
			}
			Rectangle rect = new Rectangle(20, 60, 460, 320);
			using (Pen pen = new Pen(Color.FromArgb(50, 0, 255, 200), 2f))
			{
				graphics.DrawRectangle(pen, rect);
			}
			double num = 3.141592653589793 * Math.Pow(this.double_0 / 2.0, 2.0) * this.double_1 / 1000.0;
			using (Font font2 = this.method_2("Bahnschrift SemiBold", 45f))
			{
				graphics.DrawString(string.Format("{0:F1} CC", num), font2, Brushes.Lime, 50f, 80f);
			}
			graphics.DrawString("ENGINE DISPLACEMENT RESULT", this.Font, Brushes.Gray, 55f, 145f);
			graphics.DrawString("ลูกสูบ (มม.):", this.Font, Brushes.White, 55f, 185f);
			graphics.DrawString("ชัก (มม.):", this.Font, Brushes.White, 55f, 235f);
			graphics.DrawString("[ PRO TYPING ENABLED ]", this.Font, Brushes.DarkCyan, 55f, 330f);
		}

		// Token: 0x060003D3 RID: 979 RVA: 0x000366F0 File Offset: 0x000348F0
		private Font method_2(string string_0, float float_0)
		{
			Font result;
			try
			{
				result = new Font(string_0, float_0, FontStyle.Bold);
			}
			catch
			{
				result = new Font(FontFamily.GenericSansSerif, float_0, FontStyle.Bold);
			}
			return result;
		}

		// Token: 0x060003D4 RID: 980 RVA: 0x0000D9A1 File Offset: 0x0000BBA1
		[CompilerGenerated]
		private void GForm14_MouseDown(object sender, MouseEventArgs e)
		{
			this.bool_0 = true;
			this.point_0 = e.Location;
		}

		// Token: 0x060003D5 RID: 981 RVA: 0x0003672C File Offset: 0x0003492C
		[CompilerGenerated]
		private void GForm14_MouseMove(object sender, MouseEventArgs e)
		{
			if (this.bool_0)
			{
				base.Location = new Point(base.Location.X - this.point_0.X + e.X, base.Location.Y - this.point_0.Y + e.Y);
			}
		}

		// Token: 0x060003D6 RID: 982 RVA: 0x0000D9B6 File Offset: 0x0000BBB6
		[CompilerGenerated]
		private void GForm14_MouseUp(object sender, MouseEventArgs e)
		{
			this.bool_0 = false;
		}

		// Token: 0x060003D7 RID: 983 RVA: 0x0000C305 File Offset: 0x0000A505
		[CompilerGenerated]
		private void method_3(object sender, EventArgs e)
		{
			base.Close();
		}

		// Token: 0x060003D8 RID: 984 RVA: 0x0000D9BF File Offset: 0x0000BBBF
		[CompilerGenerated]
		private void textBox_0_TextChanged(object sender, EventArgs e)
		{
			double.TryParse(this.textBox_0.Text, out this.double_0);
			base.Invalidate();
		}

		// Token: 0x060003D9 RID: 985 RVA: 0x0000D9DE File Offset: 0x0000BBDE
		[CompilerGenerated]
		private void textBox_1_TextChanged(object sender, EventArgs e)
		{
			double.TryParse(this.textBox_1.Text, out this.double_1);
			base.Invalidate();
		}

		// Token: 0x040002FF RID: 767
		private bool bool_0;

		// Token: 0x04000300 RID: 768
		private Point point_0;

		// Token: 0x04000301 RID: 769
		private TextBox textBox_0;

		// Token: 0x04000302 RID: 770
		private TextBox textBox_1;

		// Token: 0x04000303 RID: 771
		private double double_0 = 52.4;

		// Token: 0x04000304 RID: 772
		private double double_1 = 57.9;
	}
}
