using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace ns1
{
	// Token: 0x020000E8 RID: 232
	public class GControl2 : Control
	{
		// Token: 0x06000421 RID: 1057 RVA: 0x0000DBFD File Offset: 0x0000BDFD
		public double method_0()
		{
			return this.double_0;
		}

		// Token: 0x06000422 RID: 1058 RVA: 0x0000DC05 File Offset: 0x0000BE05
		public void method_1(double double_1)
		{
			this.double_0 = double_1;
			this.method_4();
			base.Invalidate();
		}

		// Token: 0x06000423 RID: 1059 RVA: 0x0000DC1A File Offset: 0x0000BE1A
		public string method_2()
		{
			return this.string_0;
		}

		// Token: 0x06000424 RID: 1060 RVA: 0x0000DC22 File Offset: 0x0000BE22
		public void method_3(string string_1)
		{
			this.string_0 = string_1;
			base.Invalidate();
		}

		// Token: 0x06000425 RID: 1061 RVA: 0x0003823C File Offset: 0x0003643C
		public GControl2()
		{
			base.Size = new Size(135, 52);
			this.DoubleBuffered = true;
			this.BackColor = Color.Transparent;
			this.timer_0 = new Timer
			{
				Interval = 40
			};
			this.timer_0.Tick += this.timer_0_Tick;
			this.timer_0.Start();
		}

		// Token: 0x06000426 RID: 1062 RVA: 0x000382DC File Offset: 0x000364DC
		private void method_4()
		{
			if (this.double_0 >= 13.0)
			{
				this.color_0 = Color.FromArgb(0, 255, 120);
				return;
			}
			if (this.double_0 >= 12.0)
			{
				this.color_0 = Color.FromArgb(50, 255, 50);
				return;
			}
			if (this.double_0 >= 11.2)
			{
				this.color_0 = Color.FromArgb(255, 200, 0);
				return;
			}
			this.color_0 = Color.FromArgb(255, 30, 30);
		}

		// Token: 0x06000427 RID: 1063 RVA: 0x00038370 File Offset: 0x00036570
		protected override void OnPaint(PaintEventArgs pevent)
		{
			Graphics graphics = pevent.Graphics;
			graphics.SmoothingMode = SmoothingMode.AntiAlias;
			graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
			Rectangle rectangle = new Rectangle(2, 2, base.Width - 5, base.Height - 5);
			using (GraphicsPath graphicsPath = this.method_6(rectangle, 12))
			{
				using (LinearGradientBrush linearGradientBrush = new LinearGradientBrush(rectangle, Color.FromArgb(20, 20, 20), Color.FromArgb(5, 5, 5), 90f))
				{
					graphics.FillPath(linearGradientBrush, graphicsPath);
				}
				using (Pen pen = new Pen(Color.FromArgb(12, 255, 255, 255), 1f))
				{
					for (int i = rectangle.Y; i < rectangle.Bottom; i += 2)
					{
						graphics.DrawLine(pen, rectangle.X, i, rectangle.Right, i);
					}
				}
				using (Pen pen2 = new Pen(Color.FromArgb(40, 0, 0, 0), 2f))
				{
					graphics.DrawPath(pen2, graphicsPath);
				}
				using (Pen pen3 = new Pen(Color.FromArgb(80, 80, 80), 1f))
				{
					graphics.DrawPath(pen3, graphicsPath);
				}
			}
			int num = 14;
			Rectangle rect = new Rectangle(12, (base.Height - 24) / 2, 14, 24);
			using (Pen pen4 = new Pen(this.color_0, 1.8f))
			{
				graphics.DrawRectangle(pen4, rect);
				graphics.FillRectangle(new SolidBrush(this.color_0), rect.X + 4, rect.Y - 3, num - 8, 3);
				float num2 = (float)((this.double_0 - 9.0) / 5.5);
				num2 = Math.Max(0.1f, Math.Min(1f, num2));
				int num3 = 4;
				int num4 = (int)Math.Round((double)(num2 * 4f));
				int num5 = (rect.Height - 6) / 4;
				int j = 0;
				while (j < num3)
				{
					Rectangle rect2 = new Rectangle(rect.X + 3, rect.Bottom - 3 - (j + 1) * num5, rect.Width - 6, num5 - 1);
					if (j < num4)
					{
						using (SolidBrush solidBrush = new SolidBrush(Color.FromArgb(180, this.color_0)))
						{
							graphics.FillRectangle(solidBrush, rect2);
							goto IL_28F;
						}
						goto IL_263;
					}
					goto IL_263;
					IL_28F:
					j++;
					continue;
					IL_263:
					using (SolidBrush solidBrush2 = new SolidBrush(Color.FromArgb(30, this.color_0)))
					{
						graphics.FillRectangle(solidBrush2, rect2);
					}
					goto IL_28F;
				}
				using (LinearGradientBrush linearGradientBrush2 = new LinearGradientBrush(new Rectangle(rect.X, rect.Y, rect.Width, rect.Height / 2), Color.FromArgb(60, Color.White), Color.Transparent, 90f))
				{
					graphics.FillRectangle(linearGradientBrush2, rect.X + 1, rect.Y + 1, rect.Width - 2, rect.Height / 2);
				}
			}
			string text = this.double_0.ToString("F1");
			string s = "V";
			using (Font font = this.method_5("Bahnschrift SemiBold", 20f, FontStyle.Bold))
			{
				using (Font font2 = this.method_5("Bahnschrift", 10f, FontStyle.Bold))
				{
					float num6 = 40f + this.float_3;
					float num7 = 6f + this.float_4;
					using (SolidBrush solidBrush3 = new SolidBrush(Color.FromArgb((int)(this.float_0 / 5f), this.color_0)))
					{
						for (int k = 1; k <= 6; k++)
						{
							graphics.DrawString(text, font, solidBrush3, num6, num7);
						}
					}
					using (SolidBrush solidBrush4 = new SolidBrush(Color.FromArgb(40, this.color_0)))
					{
						graphics.DrawString(text, font, solidBrush4, num6 + 1.2f, num7 + 0.8f);
					}
					using (SolidBrush solidBrush5 = new SolidBrush(Color.FromArgb(180, Color.White)))
					{
						graphics.DrawString(text, font, solidBrush5, num6, num7);
					}
					using (SolidBrush solidBrush6 = new SolidBrush(Color.FromArgb(110, 0, 255, 255)))
					{
						graphics.DrawString(text, font, solidBrush6, num6 + 1.5f, num7);
					}
					using (SolidBrush solidBrush7 = new SolidBrush(Color.FromArgb(110, 255, 0, 0)))
					{
						graphics.DrawString(text, font, solidBrush7, num6 - 1.5f, num7);
					}
					graphics.DrawString(text, font, new SolidBrush(this.color_0), num6, num7);
					graphics.DrawString(":", font, new SolidBrush(this.color_0), num6 - 12f, num7 - 1f);
					graphics.DrawString(s, font2, new SolidBrush(Color.FromArgb(170, Color.White)), num6 + graphics.MeasureString(text, font).Width - 2f, num7 + 12f);
				}
			}
			using (Font font3 = new Font("Consolas", 6.5f, FontStyle.Bold))
			{
				Rectangle rect3 = new Rectangle(40, 35, 75, 12);
				using (SolidBrush solidBrush8 = new SolidBrush(Color.FromArgb(30, this.color_0)))
				{
					graphics.FillRectangle(solidBrush8, rect3);
				}
				graphics.DrawString(this.string_0, font3, new SolidBrush(Color.FromArgb(180, Color.White)), 42f, 36f);
				using (Pen pen5 = new Pen(Color.FromArgb(60, this.color_0), 1f))
				{
					graphics.DrawLine(pen5, 38, 35, 38, 47);
					graphics.DrawLine(pen5, 115, 35, 115, 47);
				}
			}
			using (LinearGradientBrush linearGradientBrush3 = new LinearGradientBrush(new Rectangle(0, (int)this.float_2, base.Width, 8), Color.Transparent, Color.FromArgb(40, this.color_0), 90f))
			{
				graphics.FillRectangle(linearGradientBrush3, 0f, this.float_2, (float)base.Width, 8f);
			}
			using (Pen pen6 = new Pen(Color.FromArgb(120, this.color_0), 2f))
			{
				graphics.DrawLine(pen6, rectangle.X, rectangle.Y, rectangle.X + 6, rectangle.Y);
				graphics.DrawLine(pen6, rectangle.X, rectangle.Y, rectangle.X, rectangle.Y + 6);
				graphics.DrawLine(pen6, rectangle.Right - 6, rectangle.Y, rectangle.Right, rectangle.Y);
				graphics.DrawLine(pen6, rectangle.Right, rectangle.Y, rectangle.Right, rectangle.Y + 6);
				graphics.DrawLine(pen6, rectangle.X, rectangle.Bottom - 6, rectangle.X, rectangle.Bottom);
				graphics.DrawLine(pen6, rectangle.X, rectangle.Bottom, rectangle.X + 6, rectangle.Bottom);
				graphics.DrawLine(pen6, rectangle.Right - 6, rectangle.Bottom, rectangle.Right, rectangle.Bottom);
				graphics.DrawLine(pen6, rectangle.Right, rectangle.Bottom, rectangle.Right - 6, rectangle.Bottom);
			}
		}

		// Token: 0x06000428 RID: 1064 RVA: 0x00038D48 File Offset: 0x00036F48
		private Font method_5(string string_1, float float_5, FontStyle fontStyle_0)
		{
			try
			{
				using (Font font = new Font(string_1, float_5, fontStyle_0))
				{
					if (font.Name == string_1)
					{
						return new Font(string_1, float_5, fontStyle_0);
					}
				}
			}
			catch
			{
			}
			return new Font(FontFamily.GenericSansSerif, float_5, fontStyle_0);
		}

		// Token: 0x06000429 RID: 1065 RVA: 0x000280D0 File Offset: 0x000262D0
		private GraphicsPath method_6(Rectangle rectangle_0, int int_0)
		{
			GraphicsPath graphicsPath = new GraphicsPath();
			float num = (float)(int_0 * 2);
			graphicsPath.AddArc((float)rectangle_0.X, (float)rectangle_0.Y, num, num, 180f, 90f);
			graphicsPath.AddArc((float)rectangle_0.Right - num, (float)rectangle_0.Y, num, num, 270f, 90f);
			graphicsPath.AddArc((float)rectangle_0.Right - num, (float)rectangle_0.Bottom - num, num, num, 0f, 90f);
			graphicsPath.AddArc((float)rectangle_0.X, (float)rectangle_0.Bottom - num, num, num, 90f, 90f);
			graphicsPath.CloseFigure();
			return graphicsPath;
		}

		// Token: 0x0600042A RID: 1066 RVA: 0x0000DC31 File Offset: 0x0000BE31
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				Timer timer = this.timer_0;
				if (timer != null)
				{
					timer.Dispose();
				}
			}
			base.Dispose(disposing);
		}

		// Token: 0x0600042B RID: 1067 RVA: 0x00038DB4 File Offset: 0x00036FB4
		[CompilerGenerated]
		private void timer_0_Tick(object sender, EventArgs e)
		{
			if (this.bool_0)
			{
				this.float_0 += 5f;
				if (this.float_0 >= 140f)
				{
					this.bool_0 = false;
				}
			}
			else
			{
				this.float_0 -= 5f;
				if (this.float_0 <= 40f)
				{
					this.bool_0 = true;
				}
			}
			this.float_3 = (float)(this.random_0.NextDouble() * 0.4 - 0.2);
			this.float_4 = (float)(this.random_0.NextDouble() * 0.4 - 0.2);
			this.float_1 += 0.25f;
			this.float_2 += 1.25f;
			if (this.float_2 >= (float)base.Height)
			{
				this.float_2 = -10f;
			}
			base.Invalidate();
		}

		// Token: 0x04000326 RID: 806
		private double double_0;

		// Token: 0x04000327 RID: 807
		private string string_0 = "OFF";

		// Token: 0x04000328 RID: 808
		private Timer timer_0;

		// Token: 0x04000329 RID: 809
		private float float_0 = 50f;

		// Token: 0x0400032A RID: 810
		private bool bool_0 = true;

		// Token: 0x0400032B RID: 811
		private Color color_0 = Color.Red;

		// Token: 0x0400032C RID: 812
		private float float_1;

		// Token: 0x0400032D RID: 813
		private float float_2;

		// Token: 0x0400032E RID: 814
		private Random random_0 = new Random();

		// Token: 0x0400032F RID: 815
		private float float_3;

		// Token: 0x04000330 RID: 816
		private float float_4;
	}
}
