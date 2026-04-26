using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using <PrivateImplementationDetails>{68F2EF73-9355-4257-ADA6-397CF8BB8E72};

namespace Attr_3
{
	// Token: 0x020000E7 RID: 231
	public class Type_5B : CheckBox
	{
		// Token: 0x06000418 RID: 1048 RVA: 0x00025CA0 File Offset: 0x00023EA0
		public Color \u00A0()
		{
			return this.\u00A0;
		}

		// Token: 0x06000419 RID: 1049 RVA: 0x00025CA8 File Offset: 0x00023EA8
		public void \u00A0(Color A_1)
		{
			this.\u00A0 = A_1;
			base.Invalidate();
		}

		// Token: 0x0600041A RID: 1050 RVA: 0x00025CB7 File Offset: 0x00023EB7
		public Color \u1680()
		{
			return this.\u1680;
		}

		// Token: 0x0600041B RID: 1051 RVA: 0x00025CBF File Offset: 0x00023EBF
		public void \u1680(Color A_1)
		{
			this.\u1680 = A_1;
			base.Invalidate();
		}

		// Token: 0x0600041C RID: 1052 RVA: 0x00025CD0 File Offset: 0x00023ED0
		public Type_5B()
		{
			base.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
			this.Cursor = Cursors.Hand;
			base.Size = new Size(160, 25);
			this.Font = new Font("Segoe UI", 10f, FontStyle.Bold);
		}

		// Token: 0x0600041D RID: 1053 RVA: 0x00025D51 File Offset: 0x00023F51
		protected override void OnMouseEnter(EventArgs A_1)
		{
			base.OnMouseEnter(A_1);
			this.\u00A0 = true;
			base.Invalidate();
		}

		// Token: 0x0600041E RID: 1054 RVA: 0x00025D67 File Offset: 0x00023F67
		protected override void OnMouseLeave(EventArgs A_1)
		{
			base.OnMouseLeave(A_1);
			this.\u00A0 = false;
			base.Invalidate();
		}

		// Token: 0x0600041F RID: 1055 RVA: 0x00025D80 File Offset: 0x00023F80
		protected override void OnPaint(PaintEventArgs A_1)
		{
			Graphics graphics = A_1.Graphics;
			graphics.SmoothingMode = SmoothingMode.HighQuality;
			Graphics graphics2 = graphics;
			Control parent = base.Parent;
			graphics2.Clear((parent != null) ? parent.BackColor : Color.Black);
			int num = 40;
			int num2 = 20;
			int num3 = (base.Height - num2) / 2;
			Rectangle rectangle = new Rectangle(0, num3, num, num2);
			using (GraphicsPath graphicsPath = this.\u00A0(rectangle))
			{
				Color color = base.Checked ? this.\u00A0 : this.\u1680;
				if (this.\u00A0)
				{
					color = ControlPaint.Light(color, 0.1f);
				}
				using (SolidBrush solidBrush = new SolidBrush(color))
				{
					graphics.FillPath(solidBrush, graphicsPath);
				}
			}
			int num4 = num2 - 4;
			int x = base.Checked ? (rectangle.Right - num4 - 2) : (rectangle.Left + 2);
			Rectangle rect = new Rectangle(x, num3 + 2, num4, num4);
			using (SolidBrush solidBrush2 = new SolidBrush(this.\u2000))
			{
				graphics.FillEllipse(solidBrush2, rect);
			}
			if (!string.IsNullOrEmpty(this.Text))
			{
				Rectangle bounds = new Rectangle(num + 8, 0, base.Width - num - 8, base.Height);
				TextRenderer.DrawText(graphics, this.Text, this.Font, bounds, this.ForeColor, TextFormatFlags.EndEllipsis | TextFormatFlags.VerticalCenter);
			}
		}

		// Token: 0x06000420 RID: 1056 RVA: 0x00025F04 File Offset: 0x00024104
		private GraphicsPath \u00A0(Rectangle A_1)
		{
			GraphicsPath graphicsPath = new GraphicsPath();
			int height = A_1.Height;
			graphicsPath.AddArc(A_1.X, A_1.Y, height, height, 90f, 180f);
			graphicsPath.AddArc(A_1.Right - height, A_1.Y, height, height, 270f, 180f);
			graphicsPath.CloseFigure();
			return graphicsPath;
		}

		// Token: 0x04000322 RID: 802
		private Color \u00A0 = Color.FromArgb(0, 122, 204);

		// Token: 0x04000323 RID: 803
		private Color \u1680 = Color.FromArgb(100, 100, 100);

		// Token: 0x04000324 RID: 804
		private Color \u2000 = Color.White;

		// Token: 0x04000325 RID: 805
		private bool \u00A0;
	}
}
