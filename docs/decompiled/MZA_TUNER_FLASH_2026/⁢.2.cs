using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;
using <PrivateImplementationDetails>{68F2EF73-9355-4257-ADA6-397CF8BB8E72};

namespace Attr_3
{
	// Token: 0x020000E4 RID: 228
	public partial class Type_58 : Form
	{
		// Token: 0x0600040B RID: 1035 RVA: 0x00024F1C File Offset: 0x0002311C
		public Type_58(string A_1, string A_2, \u2061 A_3)
		{
			this.\u00A0 = A_1;
			this.\u1680 = A_2;
			this.\u00A0 = A_3;
			base.FormBorderStyle = FormBorderStyle.None;
			base.ShowInTaskbar = false;
			base.TopMost = true;
			base.StartPosition = FormStartPosition.Manual;
			base.Size = new Size(380, 85);
			this.BackColor = Color.Black;
			this.DoubleBuffered = true;
			base.Opacity = 0.0;
			this.\u00A0 = new Timer();
			this.Attr_2.Interval = 10;
			this.Attr_2.Tick += this.\u00A0;
			this.Attr_2.Start();
			this.\u00A0();
		}

		// Token: 0x0600040C RID: 1036 RVA: 0x00024FE0 File Offset: 0x000231E0
		private void \u00A0()
		{
			using (GraphicsPath graphicsPath = this.\u00A0(base.ClientRectangle, 8))
			{
				base.Region = new Region(graphicsPath);
			}
		}

		// Token: 0x0600040D RID: 1037 RVA: 0x00025024 File Offset: 0x00023224
		private void \u00A0(object A_1, EventArgs A_2)
		{
			if (!this.\u00A0)
			{
				if (base.Opacity < 0.9800000190734863)
				{
					base.Opacity += 0.15000000596046448;
					if (this.\u00A0 > 0f)
					{
						this.\u00A0 -= this.\u00A0 * 0.4f + 1f;
					}
					if (this.\u00A0 < 0f)
					{
						this.\u00A0 = 0f;
					}
					\u2062.\u1680();
					return;
				}
				if (this.\u00A0 < 250)
				{
					this.\u00A0++;
					return;
				}
				this.\u00A0 = true;
				return;
			}
			else
			{
				if (base.Opacity > 0.009999999776482582)
				{
					base.Opacity -= 0.014999999664723873;
					this.\u00A0 += 0.3f;
					\u2062.\u1680();
					return;
				}
				this.Attr_2.Stop();
				base.Close();
				return;
			}
		}

		// Token: 0x0600040E RID: 1038 RVA: 0x00025124 File Offset: 0x00023324
		protected override void OnFormClosed(FormClosedEventArgs A_1)
		{
			List<Type_58> u00A = \u2062.\u00A0;
			lock (u00A)
			{
				\u2062.Attr_2.Remove(this);
				\u2062.\u1680();
			}
			base.OnFormClosed(A_1);
		}

		// Token: 0x0600040F RID: 1039 RVA: 0x00025178 File Offset: 0x00023378
		private static void \u1680()
		{
			Rectangle workingArea = Screen.PrimaryScreen.WorkingArea;
			int num = workingArea.Bottom - 10;
			List<Type_58> u00A = \u2062.\u00A0;
			lock (u00A)
			{
				for (int i = 0; i < \u2062.Attr_2.Count; i++)
				{
					\u2062 u = \u2062.\u00A0[i];
					num -= u.Height + 10;
					u.Location = new Point(workingArea.Right - u.Width - 10 + (int)u.\u00A0, num);
				}
			}
		}

		// Token: 0x06000410 RID: 1040 RVA: 0x00025224 File Offset: 0x00023424
		public static void \u00A0(string A_0, string A_1, \u2061 A_2)
		{
			\u2062 u = new Type_58(A_0, A_1, A_2);
			List<Type_58> u00A = \u2062.\u00A0;
			lock (u00A)
			{
				\u2062.Attr_2.Add(u);
				\u2062.\u1680();
			}
			u.Show();
		}

		// Token: 0x06000411 RID: 1041 RVA: 0x0002527C File Offset: 0x0002347C
		protected override void OnPaint(PaintEventArgs A_1)
		{
			Graphics graphics = A_1.Graphics;
			graphics.SmoothingMode = SmoothingMode.AntiAlias;
			graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
			Rectangle clientRectangle = base.ClientRectangle;
			Color color = (this.\u00A0 == \u2061.\u00A0) ? Color.FromArgb(0, 255, 120) : Color.FromArgb(0, 180, 255);
			using (LinearGradientBrush linearGradientBrush = new LinearGradientBrush(clientRectangle, Color.FromArgb(35, 35, 38), Color.FromArgb(10, 10, 12), 45f))
			{
				graphics.FillRectangle(linearGradientBrush, clientRectangle);
			}
			using (Pen pen = new Pen(Color.FromArgb(50, 255, 255, 255), 1f))
			{
				graphics.DrawPath(pen, this.\u00A0(new Rectangle(0, 0, clientRectangle.Width - 1, clientRectangle.Height - 1), 8));
			}
			int width = 4;
			Rectangle rect = new Rectangle(0, 15, width, base.Height - 30);
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
			Rectangle rect2 = new Rectangle(20, (base.Height - num) / 2, num, num);
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
				int num2 = 8;
				graphics.DrawLine(pen3, clientRectangle.Width - num2, 2, clientRectangle.Width - 2, 2);
				graphics.DrawLine(pen3, clientRectangle.Width - 2, 2, clientRectangle.Width - 2, num2);
				graphics.DrawLine(pen3, clientRectangle.Width - num2, clientRectangle.Height - 2, clientRectangle.Width - 2, clientRectangle.Height - 2);
				graphics.DrawLine(pen3, clientRectangle.Width - 2, clientRectangle.Height - 2, clientRectangle.Width - 2, clientRectangle.Height - num2);
			}
			using (Font font2 = new Font("Segoe UI Semibold", 11f))
			{
				using (Font font3 = new Font("Segoe UI", 9.5f))
				{
					using (SolidBrush solidBrush3 = new SolidBrush(Color.White))
					{
						graphics.DrawString(this.\u00A0, font2, solidBrush3, (float)(rect2.Right + 15), (float)(rect2.Y - 2));
					}
					using (SolidBrush solidBrush4 = new SolidBrush(Color.FromArgb(180, 180, 185)))
					{
						RectangleF layoutRectangle = new RectangleF((float)(rect2.Right + 15), (float)(rect2.Y + 22), (float)(base.Width - rect2.Right - 30), 40f);
						graphics.DrawString(this.\u1680, font3, solidBrush4, layoutRectangle);
					}
				}
			}
		}

		// Token: 0x06000412 RID: 1042 RVA: 0x000257C8 File Offset: 0x000239C8
		private GraphicsPath \u00A0(Rectangle A_1, int A_2)
		{
			GraphicsPath graphicsPath = new GraphicsPath();
			int num = A_2 * 2;
			graphicsPath.AddArc(A_1.X, A_1.Y, num, num, 180f, 90f);
			graphicsPath.AddArc(A_1.Right - num, A_1.Y, num, num, 270f, 90f);
			graphicsPath.AddArc(A_1.Right - num, A_1.Bottom - num, num, num, 0f, 90f);
			graphicsPath.AddArc(A_1.X, A_1.Bottom - num, num, num, 90f, 90f);
			graphicsPath.CloseFigure();
			return graphicsPath;
		}

		// Token: 0x0400031A RID: 794
		private static List<Type_58> \u00A0 = new List<Type_58>();

		// Token: 0x0400031B RID: 795
		private string \u00A0;

		// Token: 0x0400031C RID: 796
		private string \u1680;

		// Token: 0x0400031D RID: 797
		private \u2061 \u00A0;

		// Token: 0x0400031E RID: 798
		private Timer \u00A0;

		// Token: 0x0400031F RID: 799
		private bool \u00A0;

		// Token: 0x04000320 RID: 800
		private float \u00A0 = 20f;

		// Token: 0x04000321 RID: 801
		private int \u00A0;
	}
}
