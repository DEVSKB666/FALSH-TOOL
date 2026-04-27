using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

namespace ns1
{
	// Token: 0x020000E4 RID: 228
	public partial class GForm15 : Form
	{
		// Token: 0x0600040B RID: 1035 RVA: 0x000373E0 File Offset: 0x000355E0
		public GForm15(string string_2, string string_3, GEnum1 genum1_1)
		{
			this.string_0 = string_2;
			this.string_1 = string_3;
			this.genum1_0 = genum1_1;
			base.FormBorderStyle = FormBorderStyle.None;
			base.ShowInTaskbar = false;
			base.TopMost = true;
			base.StartPosition = FormStartPosition.Manual;
			base.Size = new Size(380, 85);
			this.BackColor = Color.Black;
			this.DoubleBuffered = true;
			base.Opacity = 0.0;
			this.timer_0 = new Timer();
			this.timer_0.Interval = 10;
			this.timer_0.Tick += this.timer_0_Tick;
			this.timer_0.Start();
			this.method_0();
		}

		// Token: 0x0600040C RID: 1036 RVA: 0x000374A4 File Offset: 0x000356A4
		private void method_0()
		{
			using (GraphicsPath graphicsPath = this.method_1(base.ClientRectangle, 8))
			{
				base.Region = new Region(graphicsPath);
			}
		}

		// Token: 0x0600040D RID: 1037 RVA: 0x000374E8 File Offset: 0x000356E8
		private void timer_0_Tick(object sender, EventArgs e)
		{
			if (!this.bool_0)
			{
				if (base.Opacity < 0.9800000190734863)
				{
					base.Opacity += 0.15000000596046448;
					if (this.float_0 > 0f)
					{
						this.float_0 -= this.float_0 * 0.4f + 1f;
					}
					if (this.float_0 < 0f)
					{
						this.float_0 = 0f;
					}
					GForm15.smethod_0();
					return;
				}
				if (this.int_0 < 250)
				{
					this.int_0++;
					return;
				}
				this.bool_0 = true;
				return;
			}
			else
			{
				if (base.Opacity > 0.009999999776482582)
				{
					base.Opacity -= 0.014999999664723873;
					this.float_0 += 0.3f;
					GForm15.smethod_0();
					return;
				}
				this.timer_0.Stop();
				base.Close();
				return;
			}
		}

		// Token: 0x0600040E RID: 1038 RVA: 0x000375E8 File Offset: 0x000357E8
		protected override void OnFormClosed(FormClosedEventArgs e)
		{
			List<GForm15> obj = GForm15.list_0;
			lock (obj)
			{
				GForm15.list_0.Remove(this);
				GForm15.smethod_0();
			}
			base.OnFormClosed(e);
		}

		// Token: 0x0600040F RID: 1039 RVA: 0x0003763C File Offset: 0x0003583C
		private static void smethod_0()
		{
			Rectangle workingArea = Screen.PrimaryScreen.WorkingArea;
			int num = workingArea.Bottom - 10;
			List<GForm15> obj = GForm15.list_0;
			lock (obj)
			{
				for (int i = 0; i < GForm15.list_0.Count; i++)
				{
					GForm15 gform = GForm15.list_0[i];
					num -= gform.Height + 10;
					gform.Location = new Point(workingArea.Right - gform.Width - 10 + (int)gform.float_0, num);
				}
			}
		}

		// Token: 0x06000410 RID: 1040 RVA: 0x000376E8 File Offset: 0x000358E8
		public static void smethod_1(string string_2, string string_3, GEnum1 genum1_1)
		{
			GForm15 gform = new GForm15(string_2, string_3, genum1_1);
			List<GForm15> obj = GForm15.list_0;
			lock (obj)
			{
				GForm15.list_0.Add(gform);
				GForm15.smethod_0();
			}
			gform.Show();
		}

		// Token: 0x06000411 RID: 1041 RVA: 0x00037740 File Offset: 0x00035940
		protected override void OnPaint(PaintEventArgs pevent)
		{
			Graphics graphics = pevent.Graphics;
			graphics.SmoothingMode = SmoothingMode.AntiAlias;
			graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
			Rectangle clientRectangle = base.ClientRectangle;
			Color color = (this.genum1_0 == GEnum1.const_0) ? Color.FromArgb(0, 255, 120) : Color.FromArgb(0, 180, 255);
			using (LinearGradientBrush linearGradientBrush = new LinearGradientBrush(clientRectangle, Color.FromArgb(35, 35, 38), Color.FromArgb(10, 10, 12), 45f))
			{
				graphics.FillRectangle(linearGradientBrush, clientRectangle);
			}
			using (Pen pen = new Pen(Color.FromArgb(50, 255, 255, 255), 1f))
			{
				graphics.DrawPath(pen, this.method_1(new Rectangle(0, 0, clientRectangle.Width - 1, clientRectangle.Height - 1), 8));
			}
			Rectangle rect = new Rectangle(0, 15, 4, base.Height - 30);
			using (GraphicsPath graphicsPath = new GraphicsPath())
			{
				graphicsPath.AddRectangle(new Rectangle(-5, 10, 15, base.Height - 20));
				using (PathGradientBrush pathGradientBrush = new PathGradientBrush(graphicsPath))
				{
					pathGradientBrush.CenterColor = Color.FromArgb(80, color);
					pathGradientBrush.SurroundColors = new Color[]
					{
						Color.Transparent
					};
					graphics.FillPath(pathGradientBrush, graphicsPath);
				}
			}
			using (SolidBrush solidBrush = new SolidBrush(color))
			{
				graphics.FillRectangle(solidBrush, rect);
			}
			int num = 40;
			Rectangle rect2 = new Rectangle(20, (base.Height - 40) / 2, 40, 40);
			using (GraphicsPath graphicsPath2 = new GraphicsPath())
			{
				graphicsPath2.AddEllipse(rect2.X - 5, rect2.Y - 5, num + 10, num + 10);
				using (PathGradientBrush pathGradientBrush2 = new PathGradientBrush(graphicsPath2))
				{
					pathGradientBrush2.CenterColor = Color.FromArgb(40, color);
					pathGradientBrush2.SurroundColors = new Color[]
					{
						Color.Transparent
					};
					graphics.FillPath(pathGradientBrush2, graphicsPath2);
				}
			}
			using (Pen pen2 = new Pen(Color.FromArgb(100, color), 1.5f))
			{
				graphics.DrawEllipse(pen2, rect2);
			}
			using (Font font = new Font("Segoe UI Black", 14f))
			{
				string text = "A";
				SizeF sizeF = graphics.MeasureString(text, font);
				using (SolidBrush solidBrush2 = new SolidBrush(Color.White))
				{
					graphics.DrawString(text, font, solidBrush2, (float)rect2.X + ((float)rect2.Width - sizeF.Width) / 2f + 1f, (float)rect2.Y + ((float)rect2.Height - sizeF.Height) / 2f);
				}
			}
			using (Pen pen3 = new Pen(Color.FromArgb(80, color), 1f))
			{
				graphics.DrawLine(pen3, clientRectangle.Width - 8, 2, clientRectangle.Width - 2, 2);
				graphics.DrawLine(pen3, clientRectangle.Width - 2, 2, clientRectangle.Width - 2, 8);
				graphics.DrawLine(pen3, clientRectangle.Width - 8, clientRectangle.Height - 2, clientRectangle.Width - 2, clientRectangle.Height - 2);
				graphics.DrawLine(pen3, clientRectangle.Width - 2, clientRectangle.Height - 2, clientRectangle.Width - 2, clientRectangle.Height - 8);
			}
			using (Font font2 = new Font("Segoe UI Semibold", 11f))
			{
				using (Font font3 = new Font("Segoe UI", 9.5f))
				{
					using (SolidBrush solidBrush3 = new SolidBrush(Color.White))
					{
						graphics.DrawString(this.string_0, font2, solidBrush3, (float)(rect2.Right + 15), (float)(rect2.Y - 2));
					}
					using (SolidBrush solidBrush4 = new SolidBrush(Color.FromArgb(180, 180, 185)))
					{
						RectangleF layoutRectangle = new RectangleF((float)(rect2.Right + 15), (float)(rect2.Y + 22), (float)(base.Width - rect2.Right - 30), 40f);
						graphics.DrawString(this.string_1, font3, solidBrush4, layoutRectangle);
					}
				}
			}
		}

		// Token: 0x06000412 RID: 1042 RVA: 0x000210C4 File Offset: 0x0001F2C4
		private GraphicsPath method_1(Rectangle rectangle_0, int int_1)
		{
			GraphicsPath graphicsPath = new GraphicsPath();
			int num = int_1 * 2;
			graphicsPath.AddArc(rectangle_0.X, rectangle_0.Y, num, num, 180f, 90f);
			graphicsPath.AddArc(rectangle_0.Right - num, rectangle_0.Y, num, num, 270f, 90f);
			graphicsPath.AddArc(rectangle_0.Right - num, rectangle_0.Bottom - num, num, num, 0f, 90f);
			graphicsPath.AddArc(rectangle_0.X, rectangle_0.Bottom - num, num, num, 90f, 90f);
			graphicsPath.CloseFigure();
			return graphicsPath;
		}

		// Token: 0x0400031A RID: 794
		private static List<GForm15> list_0 = new List<GForm15>();

		// Token: 0x0400031B RID: 795
		private string string_0;

		// Token: 0x0400031C RID: 796
		private string string_1;

		// Token: 0x0400031D RID: 797
		private GEnum1 genum1_0;

		// Token: 0x0400031E RID: 798
		private Timer timer_0;

		// Token: 0x0400031F RID: 799
		private bool bool_0;

		// Token: 0x04000320 RID: 800
		private float float_0 = 20f;

		// Token: 0x04000321 RID: 801
		private int int_0;
	}
}
