using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace ns1
{
	// Token: 0x02000095 RID: 149
	public class GClass4 : Button
	{
		// Token: 0x06000155 RID: 341 RVA: 0x0000C884 File Offset: 0x0000AA84
		public GEnum0 method_0()
		{
			return this.genum0_0;
		}

		// Token: 0x06000156 RID: 342 RVA: 0x0000C88C File Offset: 0x0000AA8C
		public void method_1(GEnum0 genum0_1)
		{
			this.genum0_0 = genum0_1;
			base.Invalidate();
		}

		// Token: 0x06000157 RID: 343 RVA: 0x0000C89B File Offset: 0x0000AA9B
		public int method_2()
		{
			return this.int_0;
		}

		// Token: 0x06000158 RID: 344 RVA: 0x0000C8A3 File Offset: 0x0000AAA3
		public void method_3(int int_2)
		{
			this.int_0 = int_2;
			this.method_6();
			base.Invalidate();
		}

		// Token: 0x06000159 RID: 345 RVA: 0x0000C8B8 File Offset: 0x0000AAB8
		public bool method_4()
		{
			return this.bool_2;
		}

		// Token: 0x0600015A RID: 346 RVA: 0x0001E634 File Offset: 0x0001C834
		public void method_5(bool bool_4)
		{
			this.bool_2 = bool_4;
			if (this.bool_2)
			{
				if (this.timer_0 == null)
				{
					this.timer_0 = new Timer
					{
						Interval = 30
					};
					this.timer_0.Tick += this.timer_0_Tick;
				}
				this.timer_0.Start();
				return;
			}
			Timer timer = this.timer_0;
			if (timer != null)
			{
				timer.Stop();
			}
			this.int_1 = 0;
			base.Invalidate();
		}

		// Token: 0x0600015B RID: 347 RVA: 0x0001E6AC File Offset: 0x0001C8AC
		public GClass4()
		{
			base.FlatStyle = FlatStyle.Flat;
			base.FlatAppearance.BorderSize = 0;
			this.BackColor = Color.Black;
			this.Cursor = Cursors.Hand;
			this.DoubleBuffered = true;
		}

		// Token: 0x0600015C RID: 348 RVA: 0x0000C8C0 File Offset: 0x0000AAC0
		protected override void OnResize(EventArgs eventargs)
		{
			base.OnResize(eventargs);
			this.method_6();
		}

		// Token: 0x0600015D RID: 349 RVA: 0x0001E700 File Offset: 0x0001C900
		private void method_6()
		{
			if (base.Width > 0 && base.Height > 0)
			{
				using (GraphicsPath graphicsPath = this.method_9(base.ClientRectangle, this.int_0))
				{
					base.Region = new Region(graphicsPath);
				}
			}
		}

		// Token: 0x0600015E RID: 350 RVA: 0x0000C303 File Offset: 0x0000A503
		protected override void OnPaintBackground(PaintEventArgs pevent)
		{
		}

		// Token: 0x0600015F RID: 351 RVA: 0x0000C8CF File Offset: 0x0000AACF
		protected override void OnMouseEnter(EventArgs e)
		{
			base.OnMouseEnter(e);
			this.bool_0 = true;
			base.Invalidate();
		}

		// Token: 0x06000160 RID: 352 RVA: 0x0000C8E5 File Offset: 0x0000AAE5
		protected override void OnMouseLeave(EventArgs e)
		{
			base.OnMouseLeave(e);
			this.bool_0 = false;
			base.Invalidate();
		}

		// Token: 0x06000161 RID: 353 RVA: 0x0000C8FB File Offset: 0x0000AAFB
		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);
			this.bool_1 = true;
			base.Invalidate();
		}

		// Token: 0x06000162 RID: 354 RVA: 0x0000C911 File Offset: 0x0000AB11
		protected override void OnMouseUp(MouseEventArgs mevent)
		{
			base.OnMouseUp(mevent);
			this.bool_1 = false;
			base.Invalidate();
		}

		// Token: 0x06000163 RID: 355 RVA: 0x0001E75C File Offset: 0x0001C95C
		protected override void OnPaint(PaintEventArgs pevent)
		{
			Graphics graphics = pevent.Graphics;
			graphics.Clear(this.BackColor);
			graphics.SmoothingMode = SmoothingMode.HighQuality;
			Color color = Color.White;
			Color color2;
			if (base.Enabled)
			{
				color2 = this.ForeColor;
				if (this.bool_1)
				{
					color2 = ControlPaint.Dark(color2, 0.1f);
				}
				else if (this.bool_0)
				{
					color2 = ControlPaint.Light(color2, 0.1f);
				}
			}
			else
			{
				color2 = Color.FromArgb(45, 45, 48);
				color = Color.FromArgb(120, 120, 120);
			}
			RectangleF rectangleF_ = new RectangleF(0f, 0f, (float)base.Width, (float)base.Height);
			using (GraphicsPath graphicsPath = this.method_8(rectangleF_, this.int_0))
			{
				using (SolidBrush solidBrush = new SolidBrush(color2))
				{
					graphics.FillPath(solidBrush, graphicsPath);
				}
				if (this.bool_2 && this.int_1 > 0)
				{
					using (SolidBrush solidBrush2 = new SolidBrush(Color.FromArgb(this.int_1, Color.White)))
					{
						graphics.FillPath(solidBrush2, graphicsPath);
					}
				}
			}
			using (GraphicsPath graphicsPath2 = this.method_8(rectangleF_, this.int_0))
			{
				using (LinearGradientBrush linearGradientBrush = new LinearGradientBrush(new RectangleF(0f, 0f, (float)base.Width, (float)(base.Height / 2)), Color.FromArgb(30, Color.White), Color.Transparent, 90f))
				{
					graphics.FillPath(linearGradientBrush, graphicsPath2);
				}
			}
			int num = base.Height / 2;
			Rectangle rectangle_ = new Rectangle(15, (base.Height - num) / 2, num, num);
			if (this.genum0_0 != GEnum0.const_0)
			{
				this.method_7(graphics, this.genum0_0, rectangle_, color);
				Rectangle bounds = new Rectangle(rectangle_.Right + 5, 0, base.Width - rectangle_.Right - 5, base.Height);
				TextRenderer.DrawText(graphics, this.Text, this.Font, bounds, color, TextFormatFlags.EndEllipsis | TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
				return;
			}
			TextRenderer.DrawText(graphics, this.Text, this.Font, this.DisplayRectangle, color, TextFormatFlags.EndEllipsis | TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
		}

		// Token: 0x06000164 RID: 356 RVA: 0x0001E9C4 File Offset: 0x0001CBC4
		private void method_7(Graphics graphics_0, GEnum0 genum0_1, Rectangle rectangle_0, Color color_0)
		{
			using (Pen pen = new Pen(color_0, 2.5f))
			{
				pen.StartCap = LineCap.Round;
				pen.EndCap = LineCap.Round;
				pen.LineJoin = LineJoin.Round;
				switch (genum0_1)
				{
				case GEnum0.const_1:
					graphics_0.DrawRectangle(pen, rectangle_0.X + rectangle_0.Width / 4, rectangle_0.Y + rectangle_0.Height / 3, rectangle_0.Width / 2, rectangle_0.Height / 2);
					graphics_0.DrawLine(pen, rectangle_0.X + rectangle_0.Width / 5, rectangle_0.Y + rectangle_0.Height / 3, rectangle_0.X + rectangle_0.Width * 4 / 5, rectangle_0.Y + rectangle_0.Height / 3);
					graphics_0.DrawLine(pen, rectangle_0.X + rectangle_0.Width / 2 - 2, rectangle_0.Y + rectangle_0.Height / 4, rectangle_0.X + rectangle_0.Width / 2 + 2, rectangle_0.Y + rectangle_0.Height / 4);
					break;
				case GEnum0.const_2:
				{
					GraphicsPath graphicsPath = new GraphicsPath();
					graphicsPath.AddLine(rectangle_0.X, rectangle_0.Y + rectangle_0.Height / 4, rectangle_0.X + rectangle_0.Width / 3, rectangle_0.Y + rectangle_0.Height / 4);
					graphicsPath.AddLine(rectangle_0.X + rectangle_0.Width / 3 + 2, rectangle_0.Y + rectangle_0.Height / 6, rectangle_0.X + rectangle_0.Width / 2, rectangle_0.Y + rectangle_0.Height / 6);
					graphicsPath.AddLine(rectangle_0.X + rectangle_0.Width, rectangle_0.Y + rectangle_0.Height / 6, rectangle_0.X + rectangle_0.Width, rectangle_0.Y + rectangle_0.Height * 4 / 5);
					graphicsPath.AddLine(rectangle_0.X, rectangle_0.Y + rectangle_0.Height * 4 / 5, rectangle_0.X, rectangle_0.Y + rectangle_0.Height / 4);
					graphics_0.DrawPath(pen, graphicsPath);
					break;
				}
				case GEnum0.const_3:
				{
					int num = rectangle_0.Width / 2;
					graphics_0.DrawRectangle(pen, rectangle_0.X + num / 2, rectangle_0.Y + num / 2, num, num);
					for (int i = 0; i < 3; i++)
					{
						int num2 = num / 4 * (i + 1);
						graphics_0.DrawLine(pen, rectangle_0.X + num / 2 - 4, rectangle_0.Y + num / 2 + num2, rectangle_0.X + num / 2, rectangle_0.Y + num / 2 + num2);
						graphics_0.DrawLine(pen, rectangle_0.X + num / 2 + num, rectangle_0.Y + num / 2 + num2, rectangle_0.X + num / 2 + num + 4, rectangle_0.Y + num / 2 + num2);
						graphics_0.DrawLine(pen, rectangle_0.X + num / 2 + num2, rectangle_0.Y + num / 2 - 4, rectangle_0.X + num / 2 + num2, rectangle_0.Y + num / 2);
						graphics_0.DrawLine(pen, rectangle_0.X + num / 2 + num2, rectangle_0.Y + num / 2 + num, rectangle_0.X + num / 2 + num2, rectangle_0.Y + num / 2 + num + 4);
					}
					break;
				}
				case GEnum0.const_4:
				{
					int num3 = rectangle_0.X + rectangle_0.Width / 2;
					int num4 = rectangle_0.Bottom - 2;
					graphics_0.DrawLine(pen, rectangle_0.X, num4 - 5, rectangle_0.X, num4);
					graphics_0.DrawLine(pen, rectangle_0.X, num4, rectangle_0.Right, num4);
					graphics_0.DrawLine(pen, rectangle_0.Right, num4, rectangle_0.Right, num4 - 5);
					graphics_0.DrawLine(pen, num3, rectangle_0.Y, num3, num4 - 4);
					graphics_0.DrawLine(pen, num3, num4 - 4, num3 - rectangle_0.Width / 3, num4 - 10);
					graphics_0.DrawLine(pen, num3, num4 - 4, num3 + rectangle_0.Width / 3, num4 - 10);
					using (SolidBrush solidBrush = new SolidBrush(Color.FromArgb(150, color_0)))
					{
						graphics_0.FillEllipse(solidBrush, rectangle_0.X + 2, rectangle_0.Y + 2, 3, 3);
						graphics_0.FillEllipse(solidBrush, rectangle_0.Right - 5, rectangle_0.Y + 2, 3, 3);
					}
					break;
				}
				}
			}
		}

		// Token: 0x06000165 RID: 357 RVA: 0x0001EEA0 File Offset: 0x0001D0A0
		private GraphicsPath method_8(RectangleF rectangleF_0, int int_2)
		{
			GraphicsPath graphicsPath = new GraphicsPath();
			float num = (float)(int_2 * 2);
			graphicsPath.AddArc(rectangleF_0.X, rectangleF_0.Y, num, num, 180f, 90f);
			graphicsPath.AddArc(rectangleF_0.Right - num, rectangleF_0.Y, num, num, 270f, 90f);
			graphicsPath.AddArc(rectangleF_0.Right - num, rectangleF_0.Bottom - num, num, num, 0f, 90f);
			graphicsPath.AddArc(rectangleF_0.X, rectangleF_0.Bottom - num, num, num, 90f, 90f);
			graphicsPath.CloseFigure();
			return graphicsPath;
		}

		// Token: 0x06000166 RID: 358 RVA: 0x0000C927 File Offset: 0x0000AB27
		private GraphicsPath method_9(Rectangle rectangle_0, int int_2)
		{
			return this.method_8(new RectangleF((float)rectangle_0.X, (float)rectangle_0.Y, (float)rectangle_0.Width, (float)rectangle_0.Height), int_2);
		}

		// Token: 0x06000167 RID: 359 RVA: 0x0001EF48 File Offset: 0x0001D148
		[CompilerGenerated]
		private void timer_0_Tick(object sender, EventArgs e)
		{
			if (this.bool_3)
			{
				this.int_1 += 5;
				if (this.int_1 >= 60)
				{
					this.bool_3 = false;
				}
			}
			else
			{
				this.int_1 -= 5;
				if (this.int_1 <= 0)
				{
					this.bool_3 = true;
				}
			}
			base.Invalidate();
		}

		// Token: 0x040000CC RID: 204
		private GEnum0 genum0_0;

		// Token: 0x040000CD RID: 205
		private bool bool_0;

		// Token: 0x040000CE RID: 206
		private bool bool_1;

		// Token: 0x040000CF RID: 207
		private int int_0 = 8;

		// Token: 0x040000D0 RID: 208
		private bool bool_2;

		// Token: 0x040000D1 RID: 209
		private Timer timer_0;

		// Token: 0x040000D2 RID: 210
		private int int_1;

		// Token: 0x040000D3 RID: 211
		private bool bool_3 = true;
	}
}
