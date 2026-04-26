using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using <PrivateImplementationDetails>{68F2EF73-9355-4257-ADA6-397CF8BB8E72};

namespace Attr_3
{
	// Token: 0x020000E8 RID: 232
	public class Type_5C : Control
	{
		// Token: 0x06000421 RID: 1057 RVA: 0x00025F66 File Offset: 0x00024166
		public double \u00A0()
		{
			return this.\u00A0;
		}

		// Token: 0x06000422 RID: 1058 RVA: 0x00025F6E File Offset: 0x0002416E
		public void \u00A0(double A_1)
		{
			this.\u00A0 = A_1;
			this.\u2000();
			base.Invalidate();
		}

		// Token: 0x06000423 RID: 1059 RVA: 0x00025F83 File Offset: 0x00024183
		public string \u1680()
		{
			return this.\u00A0;
		}

		// Token: 0x06000424 RID: 1060 RVA: 0x00025F8B File Offset: 0x0002418B
		public void \u00A0(string A_1)
		{
			this.\u00A0 = A_1;
			base.Invalidate();
		}

		// Token: 0x06000425 RID: 1061 RVA: 0x00025F9C File Offset: 0x0002419C
		public Type_5C()
		{
			base.Size = new Size(135, 52);
			this.DoubleBuffered = true;
			this.BackColor = Color.Transparent;
			this.\u00A0 = new Timer
			{
				Interval = 40
			};
			this.Attr_2.Tick += this.\u00A0;
			this.Attr_2.Start();
		}

		// Token: 0x06000426 RID: 1062 RVA: 0x0002603C File Offset: 0x0002423C
		private void \u2000()
		{
			if (this.\u00A0 >= 13.0)
			{
				this.\u00A0 = Color.FromArgb(0, 255, 120);
				return;
			}
			if (this.\u00A0 >= 12.0)
			{
				this.\u00A0 = Color.FromArgb(50, 255, 50);
				return;
			}
			if (this.\u00A0 >= 11.2)
			{
				this.\u00A0 = Color.FromArgb(255, 200, 0);
				return;
			}
			this.\u00A0 = Color.FromArgb(255, 30, 30);
		}

		// Token: 0x06000427 RID: 1063 RVA: 0x000260D0 File Offset: 0x000242D0
		protected override void OnPaint(PaintEventArgs A_1)
		{
			Graphics graphics = A_1.Graphics;
			graphics.SmoothingMode = SmoothingMode.AntiAlias;
			graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
			Rectangle rectangle = new Rectangle(2, 2, base.Width - 5, base.Height - 5);
			using (GraphicsPath graphicsPath = this.\u00A0(rectangle, 12))
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
			int num2 = 24;
			Rectangle rect = new Rectangle(12, (base.Height - num2) / 2, num, num2);
			using (Pen pen4 = new Pen(this.\u00A0, 1.8f))
			{
				graphics.DrawRectangle(pen4, rect);
				graphics.FillRectangle(new SolidBrush(this.\u00A0), rect.X + 4, rect.Y - 3, num - 8, 3);
				float num3 = (float)((this.\u00A0 - 9.0) / 5.5);
				num3 = Math.Max(0.1f, Math.Min(1f, num3));
				int num4 = 4;
				int num5 = (int)Math.Round((double)(num3 * (float)num4));
				int num6 = (rect.Height - 6) / num4;
				int j = 0;
				while (j < num4)
				{
					Rectangle rect2 = new Rectangle(rect.X + 3, rect.Bottom - 3 - (j + 1) * num6, rect.Width - 6, num6 - 1);
					if (j < num5)
					{
						using (SolidBrush solidBrush = new SolidBrush(Color.FromArgb(180, this.\u00A0)))
						{
							graphics.FillRectangle(solidBrush, rect2);
							goto IL_28A;
						}
						goto IL_25E;
					}
					goto IL_25E;
					IL_28A:
					j++;
					continue;
					IL_25E:
					using (SolidBrush solidBrush2 = new SolidBrush(Color.FromArgb(30, this.\u00A0)))
					{
						graphics.FillRectangle(solidBrush2, rect2);
					}
					goto IL_28A;
				}
				using (LinearGradientBrush linearGradientBrush2 = new LinearGradientBrush(new Rectangle(rect.X, rect.Y, rect.Width, rect.Height / 2), Color.FromArgb(60, Color.White), Color.Transparent, 90f))
				{
					graphics.FillRectangle(linearGradientBrush2, rect.X + 1, rect.Y + 1, rect.Width - 2, rect.Height / 2);
				}
			}
			string text = this.Attr_2.ToString("F1");
			string s = "V";
			using (Font font = this.\u00A0("Bahnschrift SemiBold", 20f, FontStyle.Bold))
			{
				using (Font font2 = this.\u00A0("Bahnschrift", 10f, FontStyle.Bold))
				{
					float num7 = 40f + this.\u2001;
					float num8 = 6f + this.\u2002;
					using (SolidBrush solidBrush3 = new SolidBrush(Color.FromArgb((int)(this.\u00A0 / 5f), this.\u00A0)))
					{
						for (int k = 1; k <= 6; k++)
						{
							graphics.DrawString(text, font, solidBrush3, num7, num8);
						}
					}
					using (SolidBrush solidBrush4 = new SolidBrush(Color.FromArgb(40, this.\u00A0)))
					{
						graphics.DrawString(text, font, solidBrush4, num7 + 1.2f, num8 + 0.8f);
					}
					using (SolidBrush solidBrush5 = new SolidBrush(Color.FromArgb(180, Color.White)))
					{
						graphics.DrawString(text, font, solidBrush5, num7, num8);
					}
					using (SolidBrush solidBrush6 = new SolidBrush(Color.FromArgb(110, 0, 255, 255)))
					{
						graphics.DrawString(text, font, solidBrush6, num7 + 1.5f, num8);
					}
					using (SolidBrush solidBrush7 = new SolidBrush(Color.FromArgb(110, 255, 0, 0)))
					{
						graphics.DrawString(text, font, solidBrush7, num7 - 1.5f, num8);
					}
					graphics.DrawString(text, font, new SolidBrush(this.\u00A0), num7, num8);
					graphics.DrawString(":", font, new SolidBrush(this.\u00A0), num7 - 12f, num8 - 1f);
					graphics.DrawString(s, font2, new SolidBrush(Color.FromArgb(170, Color.White)), num7 + graphics.MeasureString(text, font).Width - 2f, num8 + 12f);
				}
			}
			using (Font font3 = new Font("Consolas", 6.5f, FontStyle.Bold))
			{
				Rectangle rect3 = new Rectangle(40, 35, 75, 12);
				using (SolidBrush solidBrush8 = new SolidBrush(Color.FromArgb(30, this.\u00A0)))
				{
					graphics.FillRectangle(solidBrush8, rect3);
				}
				graphics.DrawString(this.\u00A0, font3, new SolidBrush(Color.FromArgb(180, Color.White)), 42f, 36f);
				using (Pen pen5 = new Pen(Color.FromArgb(60, this.\u00A0), 1f))
				{
					graphics.DrawLine(pen5, 38, 35, 38, 47);
					graphics.DrawLine(pen5, 115, 35, 115, 47);
				}
			}
			using (LinearGradientBrush linearGradientBrush3 = new LinearGradientBrush(new Rectangle(0, (int)this.\u2000, base.Width, 8), Color.Transparent, Color.FromArgb(40, this.\u00A0), 90f))
			{
				graphics.FillRectangle(linearGradientBrush3, 0f, this.\u2000, (float)base.Width, 8f);
			}
			using (Pen pen6 = new Pen(Color.FromArgb(120, this.\u00A0), 2f))
			{
				int num9 = 6;
				graphics.DrawLine(pen6, rectangle.X, rectangle.Y, rectangle.X + num9, rectangle.Y);
				graphics.DrawLine(pen6, rectangle.X, rectangle.Y, rectangle.X, rectangle.Y + num9);
				graphics.DrawLine(pen6, rectangle.Right - num9, rectangle.Y, rectangle.Right, rectangle.Y);
				graphics.DrawLine(pen6, rectangle.Right, rectangle.Y, rectangle.Right, rectangle.Y + num9);
				graphics.DrawLine(pen6, rectangle.X, rectangle.Bottom - num9, rectangle.X, rectangle.Bottom);
				graphics.DrawLine(pen6, rectangle.X, rectangle.Bottom, rectangle.X + num9, rectangle.Bottom);
				graphics.DrawLine(pen6, rectangle.Right - num9, rectangle.Bottom, rectangle.Right, rectangle.Bottom);
				graphics.DrawLine(pen6, rectangle.Right, rectangle.Bottom, rectangle.Right - num9, rectangle.Bottom);
			}
		}

		// Token: 0x06000428 RID: 1064 RVA: 0x00026AB4 File Offset: 0x00024CB4
		private Font \u00A0(string A_1, float A_2, FontStyle A_3)
		{
			try
			{
				using (Font font = new Font(A_1, A_2, A_3))
				{
					if (font.Name == A_1)
					{
						return new Font(A_1, A_2, A_3);
					}
				}
			}
			catch
			{
			}
			return new Font(FontFamily.GenericSansSerif, A_2, A_3);
		}

		// Token: 0x06000429 RID: 1065 RVA: 0x00026B20 File Offset: 0x00024D20
		private GraphicsPath \u00A0(Rectangle A_1, int A_2)
		{
			GraphicsPath graphicsPath = new GraphicsPath();
			float num = (float)(A_2 * 2);
			graphicsPath.AddArc((float)A_1.X, (float)A_1.Y, num, num, 180f, 90f);
			graphicsPath.AddArc((float)A_1.Right - num, (float)A_1.Y, num, num, 270f, 90f);
			graphicsPath.AddArc((float)A_1.Right - num, (float)A_1.Bottom - num, num, num, 0f, 90f);
			graphicsPath.AddArc((float)A_1.X, (float)A_1.Bottom - num, num, num, 90f, 90f);
			graphicsPath.CloseFigure();
			return graphicsPath;
		}

		// Token: 0x0600042A RID: 1066 RVA: 0x00026BCD File Offset: 0x00024DCD
		protected override void Dispose(bool A_1)
		{
			if (A_1)
			{
				Timer u00A = this.\u00A0;
				if (u00A != null)
				{
					u00A.Dispose();
				}
			}
			base.Dispose(A_1);
		}

		// Token: 0x0600042B RID: 1067 RVA: 0x00026BEC File Offset: 0x00024DEC
		[CompilerGenerated]
		private void \u00A0(object A_1, EventArgs A_2)
		{
			if (this.\u00A0)
			{
				this.\u00A0 += 5f;
				if (this.\u00A0 >= 140f)
				{
					this.\u00A0 = false;
				}
			}
			else
			{
				this.\u00A0 -= 5f;
				if (this.\u00A0 <= 40f)
				{
					this.\u00A0 = true;
				}
			}
			this.\u2001 = (float)(this.Attr_2.NextDouble() * 0.4 - 0.2);
			this.\u2002 = (float)(this.Attr_2.NextDouble() * 0.4 - 0.2);
			this.\u1680 += 0.25f;
			this.\u2000 += 1.25f;
			if (this.\u2000 >= (float)base.Height)
			{
				this.\u2000 = -10f;
			}
			base.Invalidate();
		}

		// Token: 0x04000326 RID: 806
		private double \u00A0;

		// Token: 0x04000327 RID: 807
		private string \u00A0 = "OFF";

		// Token: 0x04000328 RID: 808
		private Timer \u00A0;

		// Token: 0x04000329 RID: 809
		private float \u00A0 = 50f;

		// Token: 0x0400032A RID: 810
		private bool \u00A0 = true;

		// Token: 0x0400032B RID: 811
		private Color \u00A0 = Color.Red;

		// Token: 0x0400032C RID: 812
		private float \u1680;

		// Token: 0x0400032D RID: 813
		private float \u2000;

		// Token: 0x0400032E RID: 814
		private Random \u00A0 = new Random();

		// Token: 0x0400032F RID: 815
		private float \u2001;

		// Token: 0x04000330 RID: 816
		private float \u2002;
	}
}
