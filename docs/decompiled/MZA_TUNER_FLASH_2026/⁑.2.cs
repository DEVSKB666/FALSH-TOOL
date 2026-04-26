using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace Attr_3
{
	// Token: 0x02000095 RID: 149
	public class Type_47 : Button
	{
		// Token: 0x06000155 RID: 341 RVA: 0x0000AC7B File Offset: 0x00008E7B
		public \u2050 \u00A0()
		{
			return this.\u00A0;
		}

		// Token: 0x06000156 RID: 342 RVA: 0x0000AC83 File Offset: 0x00008E83
		public void \u00A0(\u2050 A_1)
		{
			this.\u00A0 = A_1;
			base.Invalidate();
		}

		// Token: 0x06000157 RID: 343 RVA: 0x0000AC92 File Offset: 0x00008E92
		public int \u1680()
		{
			return this.\u00A0;
		}

		// Token: 0x06000158 RID: 344 RVA: 0x0000AC9A File Offset: 0x00008E9A
		public void \u00A0(int A_1)
		{
			this.\u00A0 = A_1;
			this.\u2001();
			base.Invalidate();
		}

		// Token: 0x06000159 RID: 345 RVA: 0x0000ACAF File Offset: 0x00008EAF
		public bool \u2000()
		{
			return this.\u2000;
		}

		// Token: 0x0600015A RID: 346 RVA: 0x0000ACB8 File Offset: 0x00008EB8
		public void \u00A0(bool A_1)
		{
			this.\u2000 = A_1;
			if (this.\u2000)
			{
				if (this.\u00A0 == null)
				{
					this.\u00A0 = new Timer
					{
						Interval = 30
					};
					this.Attr_2.Tick += this.\u00A0;
				}
				this.Attr_2.Start();
				return;
			}
			Timer u00A = this.\u00A0;
			if (u00A != null)
			{
				u00A.Stop();
			}
			this.\u1680 = 0;
			base.Invalidate();
		}

		// Token: 0x0600015B RID: 347 RVA: 0x0000AD30 File Offset: 0x00008F30
		public Type_47()
		{
			base.FlatStyle = FlatStyle.Flat;
			base.FlatAppearance.BorderSize = 0;
			this.BackColor = Color.Black;
			this.Cursor = Cursors.Hand;
			this.DoubleBuffered = true;
		}

		// Token: 0x0600015C RID: 348 RVA: 0x0000AD81 File Offset: 0x00008F81
		protected override void OnResize(EventArgs A_1)
		{
			base.OnResize(A_1);
			this.\u2001();
		}

		// Token: 0x0600015D RID: 349 RVA: 0x0000AD90 File Offset: 0x00008F90
		private void \u2001()
		{
			if (base.Width > 0 && base.Height > 0)
			{
				using (GraphicsPath graphicsPath = this.\u00A0(base.ClientRectangle, this.\u00A0))
				{
					base.Region = new Region(graphicsPath);
				}
			}
		}

		// Token: 0x0600015E RID: 350 RVA: 0x0000208B File Offset: 0x0000028B
		protected override void OnPaintBackground(PaintEventArgs A_1)
		{
		}

		// Token: 0x0600015F RID: 351 RVA: 0x0000ADEC File Offset: 0x00008FEC
		protected override void OnMouseEnter(EventArgs A_1)
		{
			base.OnMouseEnter(A_1);
			this.\u00A0 = true;
			base.Invalidate();
		}

		// Token: 0x06000160 RID: 352 RVA: 0x0000AE02 File Offset: 0x00009002
		protected override void OnMouseLeave(EventArgs A_1)
		{
			base.OnMouseLeave(A_1);
			this.\u00A0 = false;
			base.Invalidate();
		}

		// Token: 0x06000161 RID: 353 RVA: 0x0000AE18 File Offset: 0x00009018
		protected override void OnMouseDown(MouseEventArgs A_1)
		{
			base.OnMouseDown(A_1);
			this.\u1680 = true;
			base.Invalidate();
		}

		// Token: 0x06000162 RID: 354 RVA: 0x0000AE2E File Offset: 0x0000902E
		protected override void OnMouseUp(MouseEventArgs A_1)
		{
			base.OnMouseUp(A_1);
			this.\u1680 = false;
			base.Invalidate();
		}

		// Token: 0x06000163 RID: 355 RVA: 0x0000AE44 File Offset: 0x00009044
		protected override void OnPaint(PaintEventArgs A_1)
		{
			Graphics graphics = A_1.Graphics;
			graphics.Clear(this.BackColor);
			graphics.SmoothingMode = SmoothingMode.HighQuality;
			Color color = Color.White;
			Color color2;
			if (base.Enabled)
			{
				color2 = this.ForeColor;
				if (this.\u1680)
				{
					color2 = ControlPaint.Dark(color2, 0.1f);
				}
				else if (this.\u00A0)
				{
					color2 = ControlPaint.Light(color2, 0.1f);
				}
			}
			else
			{
				color2 = Color.FromArgb(45, 45, 48);
				color = Color.FromArgb(120, 120, 120);
			}
			RectangleF rectangleF = new RectangleF(0f, 0f, (float)base.Width, (float)base.Height);
			using (GraphicsPath graphicsPath = this.\u00A0(rectangleF, this.\u00A0))
			{
				using (SolidBrush solidBrush = new SolidBrush(color2))
				{
					graphics.FillPath(solidBrush, graphicsPath);
				}
				if (this.\u2000 && this.\u1680 > 0)
				{
					using (SolidBrush solidBrush2 = new SolidBrush(Color.FromArgb(this.\u1680, Color.White)))
					{
						graphics.FillPath(solidBrush2, graphicsPath);
					}
				}
			}
			using (GraphicsPath graphicsPath2 = this.\u00A0(rectangleF, this.\u00A0))
			{
				using (LinearGradientBrush linearGradientBrush = new LinearGradientBrush(new RectangleF(0f, 0f, (float)base.Width, (float)(base.Height / 2)), Color.FromArgb(30, Color.White), Color.Transparent, 90f))
				{
					graphics.FillPath(linearGradientBrush, graphicsPath2);
				}
			}
			int num = base.Height / 2;
			int x = 15;
			Rectangle rectangle = new Rectangle(x, (base.Height - num) / 2, num, num);
			if (this.\u00A0 != \u2050.\u00A0)
			{
				this.\u00A0(graphics, this.\u00A0, rectangle, color);
				Rectangle bounds = new Rectangle(rectangle.Right + 5, 0, base.Width - rectangle.Right - 5, base.Height);
				TextRenderer.DrawText(graphics, this.Text, this.Font, bounds, color, TextFormatFlags.EndEllipsis | TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
				return;
			}
			TextRenderer.DrawText(graphics, this.Text, this.Font, this.DisplayRectangle, color, TextFormatFlags.EndEllipsis | TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
		}

		// Token: 0x06000164 RID: 356 RVA: 0x0000B0B0 File Offset: 0x000092B0
		private void \u00A0(Graphics A_1, \u2050 A_2, Rectangle A_3, Color A_4)
		{
			using (Pen pen = new Pen(A_4, 2.5f))
			{
				pen.StartCap = LineCap.Round;
				pen.EndCap = LineCap.Round;
				pen.LineJoin = LineJoin.Round;
				switch (A_2)
				{
				case \u2050.\u1680:
					A_1.DrawRectangle(pen, A_3.X + A_3.Width / 4, A_3.Y + A_3.Height / 3, A_3.Width / 2, A_3.Height / 2);
					A_1.DrawLine(pen, A_3.X + A_3.Width / 5, A_3.Y + A_3.Height / 3, A_3.X + A_3.Width * 4 / 5, A_3.Y + A_3.Height / 3);
					A_1.DrawLine(pen, A_3.X + A_3.Width / 2 - 2, A_3.Y + A_3.Height / 4, A_3.X + A_3.Width / 2 + 2, A_3.Y + A_3.Height / 4);
					break;
				case \u2050.\u2000:
				{
					GraphicsPath graphicsPath = new GraphicsPath();
					graphicsPath.AddLine(A_3.X, A_3.Y + A_3.Height / 4, A_3.X + A_3.Width / 3, A_3.Y + A_3.Height / 4);
					graphicsPath.AddLine(A_3.X + A_3.Width / 3 + 2, A_3.Y + A_3.Height / 6, A_3.X + A_3.Width / 2, A_3.Y + A_3.Height / 6);
					graphicsPath.AddLine(A_3.X + A_3.Width, A_3.Y + A_3.Height / 6, A_3.X + A_3.Width, A_3.Y + A_3.Height * 4 / 5);
					graphicsPath.AddLine(A_3.X, A_3.Y + A_3.Height * 4 / 5, A_3.X, A_3.Y + A_3.Height / 4);
					A_1.DrawPath(pen, graphicsPath);
					break;
				}
				case \u2050.\u2001:
				{
					int num = A_3.Width / 2;
					A_1.DrawRectangle(pen, A_3.X + num / 2, A_3.Y + num / 2, num, num);
					for (int i = 0; i < 3; i++)
					{
						int num2 = num / 4 * (i + 1);
						A_1.DrawLine(pen, A_3.X + num / 2 - 4, A_3.Y + num / 2 + num2, A_3.X + num / 2, A_3.Y + num / 2 + num2);
						A_1.DrawLine(pen, A_3.X + num / 2 + num, A_3.Y + num / 2 + num2, A_3.X + num / 2 + num + 4, A_3.Y + num / 2 + num2);
						A_1.DrawLine(pen, A_3.X + num / 2 + num2, A_3.Y + num / 2 - 4, A_3.X + num / 2 + num2, A_3.Y + num / 2);
						A_1.DrawLine(pen, A_3.X + num / 2 + num2, A_3.Y + num / 2 + num, A_3.X + num / 2 + num2, A_3.Y + num / 2 + num + 4);
					}
					break;
				}
				case \u2050.\u2002:
				{
					int num3 = A_3.X + A_3.Width / 2;
					int num4 = A_3.Bottom - 2;
					A_1.DrawLine(pen, A_3.X, num4 - 5, A_3.X, num4);
					A_1.DrawLine(pen, A_3.X, num4, A_3.Right, num4);
					A_1.DrawLine(pen, A_3.Right, num4, A_3.Right, num4 - 5);
					A_1.DrawLine(pen, num3, A_3.Y, num3, num4 - 4);
					A_1.DrawLine(pen, num3, num4 - 4, num3 - A_3.Width / 3, num4 - 10);
					A_1.DrawLine(pen, num3, num4 - 4, num3 + A_3.Width / 3, num4 - 10);
					using (SolidBrush solidBrush = new SolidBrush(Color.FromArgb(150, A_4)))
					{
						A_1.FillEllipse(solidBrush, A_3.X + 2, A_3.Y + 2, 3, 3);
						A_1.FillEllipse(solidBrush, A_3.Right - 5, A_3.Y + 2, 3, 3);
					}
					break;
				}
				}
			}
		}

		// Token: 0x06000165 RID: 357 RVA: 0x0000B588 File Offset: 0x00009788
		private GraphicsPath \u00A0(RectangleF A_1, int A_2)
		{
			GraphicsPath graphicsPath = new GraphicsPath();
			float num = (float)(A_2 * 2);
			graphicsPath.AddArc(A_1.X, A_1.Y, num, num, 180f, 90f);
			graphicsPath.AddArc(A_1.Right - num, A_1.Y, num, num, 270f, 90f);
			graphicsPath.AddArc(A_1.Right - num, A_1.Bottom - num, num, num, 0f, 90f);
			graphicsPath.AddArc(A_1.X, A_1.Bottom - num, num, num, 90f, 90f);
			graphicsPath.CloseFigure();
			return graphicsPath;
		}

		// Token: 0x06000166 RID: 358 RVA: 0x0000B62D File Offset: 0x0000982D
		private GraphicsPath \u00A0(Rectangle A_1, int A_2)
		{
			return this.\u00A0(new RectangleF((float)A_1.X, (float)A_1.Y, (float)A_1.Width, (float)A_1.Height), A_2);
		}

		// Token: 0x06000167 RID: 359 RVA: 0x0000B65C File Offset: 0x0000985C
		[CompilerGenerated]
		private void \u00A0(object A_1, EventArgs A_2)
		{
			if (this.\u2001)
			{
				this.\u1680 += 5;
				if (this.\u1680 >= 60)
				{
					this.\u2001 = false;
				}
			}
			else
			{
				this.\u1680 -= 5;
				if (this.\u1680 <= 0)
				{
					this.\u2001 = true;
				}
			}
			base.Invalidate();
		}

		// Token: 0x040000CC RID: 204
		private \u2050 \u00A0;

		// Token: 0x040000CD RID: 205
		private bool \u00A0;

		// Token: 0x040000CE RID: 206
		private bool \u1680;

		// Token: 0x040000CF RID: 207
		private int \u00A0 = 8;

		// Token: 0x040000D0 RID: 208
		private bool \u2000;

		// Token: 0x040000D1 RID: 209
		private Timer \u00A0;

		// Token: 0x040000D2 RID: 210
		private int \u1680;

		// Token: 0x040000D3 RID: 211
		private bool \u2001 = true;
	}
}
