using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace ns1
{
	// Token: 0x020000E7 RID: 231
	public class GClass15 : CheckBox
	{
		// Token: 0x06000418 RID: 1048 RVA: 0x0000DBA3 File Offset: 0x0000BDA3
		public Color method_0()
		{
			return this.color_0;
		}

		// Token: 0x06000419 RID: 1049 RVA: 0x0000DBAB File Offset: 0x0000BDAB
		public void method_1(Color color_3)
		{
			this.color_0 = color_3;
			base.Invalidate();
		}

		// Token: 0x0600041A RID: 1050 RVA: 0x0000DBBA File Offset: 0x0000BDBA
		public Color method_2()
		{
			return this.color_1;
		}

		// Token: 0x0600041B RID: 1051 RVA: 0x0000DBC2 File Offset: 0x0000BDC2
		public void method_3(Color color_3)
		{
			this.color_1 = color_3;
			base.Invalidate();
		}

		// Token: 0x0600041C RID: 1052 RVA: 0x00038030 File Offset: 0x00036230
		public GClass15()
		{
			base.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
			this.Cursor = Cursors.Hand;
			base.Size = new Size(160, 25);
			this.Font = new Font("Segoe UI", 10f, FontStyle.Bold);
		}

		// Token: 0x0600041D RID: 1053 RVA: 0x0000DBD1 File Offset: 0x0000BDD1
		protected override void OnMouseEnter(EventArgs e)
		{
			base.OnMouseEnter(e);
			this.bool_0 = true;
			base.Invalidate();
		}

		// Token: 0x0600041E RID: 1054 RVA: 0x0000DBE7 File Offset: 0x0000BDE7
		protected override void OnMouseLeave(EventArgs e)
		{
			base.OnMouseLeave(e);
			this.bool_0 = false;
			base.Invalidate();
		}

		// Token: 0x0600041F RID: 1055 RVA: 0x000380B4 File Offset: 0x000362B4
		protected override void OnPaint(PaintEventArgs pevent)
		{
			Graphics graphics = pevent.Graphics;
			graphics.SmoothingMode = SmoothingMode.HighQuality;
			Graphics graphics2 = graphics;
			Control parent = base.Parent;
			graphics2.Clear((parent != null) ? parent.BackColor : Color.Black);
			int num = 40;
			int num2 = 20;
			int num3 = (base.Height - 20) / 2;
			Rectangle rectangle_ = new Rectangle(0, num3, 40, 20);
			using (GraphicsPath graphicsPath = this.method_4(rectangle_))
			{
				Color color = base.Checked ? this.color_0 : this.color_1;
				if (this.bool_0)
				{
					color = ControlPaint.Light(color, 0.1f);
				}
				using (SolidBrush solidBrush = new SolidBrush(color))
				{
					graphics.FillPath(solidBrush, graphicsPath);
				}
			}
			int num4 = num2 - 4;
			int x = base.Checked ? (rectangle_.Right - num4 - 2) : (rectangle_.Left + 2);
			Rectangle rect = new Rectangle(x, num3 + 2, num4, num4);
			using (SolidBrush solidBrush2 = new SolidBrush(this.color_2))
			{
				graphics.FillEllipse(solidBrush2, rect);
			}
			if (!string.IsNullOrEmpty(this.Text))
			{
				Rectangle bounds = new Rectangle(num + 8, 0, base.Width - num - 8, base.Height);
				TextRenderer.DrawText(graphics, this.Text, this.Font, bounds, this.ForeColor, TextFormatFlags.EndEllipsis | TextFormatFlags.VerticalCenter);
			}
		}

		// Token: 0x06000420 RID: 1056 RVA: 0x00027568 File Offset: 0x00025768
		private GraphicsPath method_4(Rectangle rectangle_0)
		{
			GraphicsPath graphicsPath = new GraphicsPath();
			int height = rectangle_0.Height;
			graphicsPath.AddArc(rectangle_0.X, rectangle_0.Y, height, height, 90f, 180f);
			graphicsPath.AddArc(rectangle_0.Right - height, rectangle_0.Y, height, height, 270f, 180f);
			graphicsPath.CloseFigure();
			return graphicsPath;
		}

		// Token: 0x04000322 RID: 802
		private Color color_0 = Color.FromArgb(0, 122, 204);

		// Token: 0x04000323 RID: 803
		private Color color_1 = Color.FromArgb(100, 100, 100);

		// Token: 0x04000324 RID: 804
		private Color color_2 = Color.White;

		// Token: 0x04000325 RID: 805
		private bool bool_0;
	}
}
