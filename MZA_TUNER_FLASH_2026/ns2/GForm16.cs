using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace ns2
{
	// Token: 0x020000F5 RID: 245
	public partial class GForm16 : Form
	{
		// Token: 0x06000465 RID: 1125
		[DllImport("Gdi32.dll")]
		private static extern IntPtr CreateRoundRectRgn(int int_1, int int_2, int int_3, int int_4, int int_5, int int_6);

		// Token: 0x06000466 RID: 1126 RVA: 0x00039494 File Offset: 0x00037694
		public GForm16()
		{
			base.Size = new Size(500, 500);
			base.FormBorderStyle = FormBorderStyle.None;
			base.StartPosition = FormStartPosition.CenterScreen;
			this.BackColor = Color.FromArgb(15, 15, 15);
			this.DoubleBuffered = true;
			base.TopMost = true;
			base.ShowInTaskbar = false;
			try
			{
				base.Region = Region.FromHrgn(GForm16.CreateRoundRectRgn(0, 0, base.Width, base.Height, 40, 40));
			}
			catch
			{
			}
			this.timer_0 = new Timer
			{
				Interval = 30
			};
			this.timer_0.Tick += this.timer_0_Tick;
			this.timer_0.Start();
		}

		// Token: 0x06000467 RID: 1127 RVA: 0x00039568 File Offset: 0x00037768
		public void method_0(string string_1, int int_1 = -1)
		{
			GForm16.Class162 @class = new GForm16.Class162();
			@class.gform16_0 = this;
			@class.string_0 = string_1;
			@class.int_0 = int_1;
			if (base.InvokeRequired)
			{
				base.Invoke(new Action(@class.method_0));
				return;
			}
			this.string_0 = @class.string_0;
			if (@class.int_0 >= 0)
			{
				this.int_0 = @class.int_0;
			}
		}

		// Token: 0x06000468 RID: 1128 RVA: 0x000395D0 File Offset: 0x000377D0
		protected override void OnPaint(PaintEventArgs pevent)
		{
			Graphics graphics = pevent.Graphics;
			graphics.SmoothingMode = SmoothingMode.AntiAlias;
			graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
			using (SolidBrush solidBrush = new SolidBrush(Color.FromArgb(10, 10, 10)))
			{
				graphics.FillRectangle(solidBrush, base.ClientRectangle);
			}
			using (PathGradientBrush pathGradientBrush = new PathGradientBrush(this.method_1(new RectangleF(0f, 0f, (float)base.Width, (float)base.Height))))
			{
				pathGradientBrush.CenterColor = Color.FromArgb(20, 231, 76, 60);
				pathGradientBrush.SurroundColors = new Color[]
				{
					Color.Transparent
				};
				graphics.FillPath(pathGradientBrush, this.method_1(new RectangleF(0f, 0f, (float)base.Width, (float)base.Height)));
			}
			float num = (float)base.Width / 2f;
			float num2 = 180f;
			float num3 = 180f;
			graphics.TranslateTransform(num, num2);
			graphics.RotateTransform(this.float_0);
			using (Pen pen = new Pen(Color.FromArgb(231, 76, 60), 4f))
			{
				pen.StartCap = LineCap.Round;
				pen.EndCap = LineCap.Round;
				graphics.DrawArc(pen, -num3 / 2f, -num3 / 2f, num3, num3, 0f, 90f);
				graphics.DrawArc(pen, -num3 / 2f, -num3 / 2f, num3, num3, 180f, 90f);
			}
			graphics.ResetTransform();
			using (Pen pen2 = new Pen(Color.FromArgb(40, 255, 255, 255), 1f))
			{
				graphics.DrawEllipse(pen2, num - (num3 - 25f) / 2f, num2 - (num3 - 25f) / 2f, num3 - 25f, num3 - 25f);
			}
			using (Font font = new Font("Impact", 60f, FontStyle.Italic))
			{
				string text = "MZA";
				SizeF sizeF = graphics.MeasureString(text, font);
				graphics.DrawString(text, font, Brushes.White, num - sizeF.Width / 2f, num2 - sizeF.Height / 2f);
			}
			float num4 = 320f;
			using (Font font2 = new Font("Leelawadee UI", 24f, FontStyle.Bold))
			{
				string text2 = "MZA-TUNER";
				SizeF sizeF2 = graphics.MeasureString(text2, font2);
				graphics.DrawString(text2, font2, Brushes.WhiteSmoke, num - sizeF2.Width / 2f, num4);
			}
			using (Font font3 = new Font("Consolas", 10f, FontStyle.Bold))
			{
				string text3 = "เวอร์ชัน 2.0.26 พรีเมียม";
				SizeF sizeF3 = graphics.MeasureString(text3, font3);
				graphics.DrawString(text3, font3, new SolidBrush(Color.FromArgb(180, 231, 76, 60)), num - sizeF3.Width / 2f, num4 + 50f);
			}
			using (Font font4 = new Font("Leelawadee UI", 11f, FontStyle.Regular))
			{
				SizeF sizeF4 = graphics.MeasureString(this.string_0, font4);
				graphics.DrawString(this.string_0.ToUpper(), font4, Brushes.DimGray, num - sizeF4.Width / 2f, num4 + 80f);
			}
			int num5 = 320;
			int num6 = 4;
			float x = num - 160f;
			float y = 450f;
			graphics.FillRectangle(new SolidBrush(Color.FromArgb(30, 30, 30)), x, y, 320f, 4f);
			if (this.float_1 > 0f)
			{
				float width = this.float_1 / 100f * (float)num5;
				using (SolidBrush solidBrush2 = new SolidBrush(Color.FromArgb(231, 76, 60)))
				{
					graphics.FillRectangle(solidBrush2, x, y, width, (float)num6);
				}
			}
			using (Font font5 = new Font("Segoe UI", 8f, FontStyle.Bold))
			{
				string text4 = "พัฒนาโดยทีมงาน MZA-TUNER TEAM © 2026";
				SizeF sizeF5 = graphics.MeasureString(text4, font5);
				using (SolidBrush solidBrush3 = new SolidBrush(Color.FromArgb(50, 50, 50)))
				{
					graphics.DrawString(text4, font5, solidBrush3, num - sizeF5.Width / 2f, (float)(base.Height - 30));
				}
			}
		}

		// Token: 0x06000469 RID: 1129 RVA: 0x0000DD46 File Offset: 0x0000BF46
		private GraphicsPath method_1(RectangleF rectangleF_0)
		{
			GraphicsPath graphicsPath = new GraphicsPath();
			graphicsPath.AddEllipse(rectangleF_0);
			return graphicsPath;
		}

		// Token: 0x0600046A RID: 1130 RVA: 0x0001B50C File Offset: 0x0001970C
		private GraphicsPath method_2(Rectangle rectangle_0, int int_1)
		{
			GraphicsPath graphicsPath = new GraphicsPath();
			graphicsPath.AddArc(rectangle_0.X, rectangle_0.Y, int_1, int_1, 180f, 90f);
			graphicsPath.AddArc(rectangle_0.X + rectangle_0.Width - int_1, rectangle_0.Y, int_1, int_1, 270f, 90f);
			graphicsPath.AddArc(rectangle_0.X + rectangle_0.Width - int_1, rectangle_0.Y + rectangle_0.Height - int_1, int_1, int_1, 0f, 90f);
			graphicsPath.AddArc(rectangle_0.X, rectangle_0.Y + rectangle_0.Height - int_1, int_1, int_1, 90f, 90f);
			graphicsPath.CloseFigure();
			return graphicsPath;
		}

		// Token: 0x0600046C RID: 1132 RVA: 0x00039AE0 File Offset: 0x00037CE0
		[CompilerGenerated]
		private void timer_0_Tick(object sender, EventArgs e)
		{
			float num = (float)this.int_0 - this.float_1;
			if (Math.Abs(num) > 0.05f)
			{
				this.float_1 += num * 0.1f;
			}
			else
			{
				this.float_1 = (float)this.int_0;
			}
			this.float_0 += 4f;
			if (this.float_0 >= 360f)
			{
				this.float_0 = 0f;
			}
			base.Invalidate();
		}

		// Token: 0x04000346 RID: 838
		private float float_0;

		// Token: 0x04000347 RID: 839
		private float float_1;

		// Token: 0x04000348 RID: 840
		private int int_0;

		// Token: 0x04000349 RID: 841
		private string string_0 = "กำลังเริ่มระบบ MZA-TUNER...";

		// Token: 0x020000F6 RID: 246
		[CompilerGenerated]
		private sealed class Class162
		{
			// Token: 0x0600046E RID: 1134 RVA: 0x0000DD82 File Offset: 0x0000BF82
			internal void method_0()
			{
				this.gform16_0.string_0 = this.string_0;
				if (this.int_0 >= 0)
				{
					this.gform16_0.int_0 = this.int_0;
				}
			}

			// Token: 0x0400034A RID: 842
			public GForm16 gform16_0;

			// Token: 0x0400034B RID: 843
			public string string_0;

			// Token: 0x0400034C RID: 844
			public int int_0;
		}
	}
}
