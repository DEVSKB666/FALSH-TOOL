using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Attr_3
{
	// Token: 0x020000E6 RID: 230
	[ToolboxItem(true)]
	[DesignerCategory("Code")]
	public class Type_5A : Panel
	{
		// Token: 0x06000416 RID: 1046 RVA: 0x00025AF4 File Offset: 0x00023CF4
		public Type_5A()
		{
			base.Size = new Size(800, 32);
			this.BackColor = Color.FromArgb(180, 180, 180);
			this.DoubleBuffered = true;
		}

		// Token: 0x06000417 RID: 1047 RVA: 0x00025B30 File Offset: 0x00023D30
		protected override void OnPaint(PaintEventArgs A_1)
		{
			base.OnPaint(A_1);
			Graphics graphics = A_1.Graphics;
			using (LinearGradientBrush linearGradientBrush = new LinearGradientBrush(base.ClientRectangle, Color.FromArgb(210, 210, 210), Color.FromArgb(140, 140, 140), 90f))
			{
				graphics.FillRectangle(linearGradientBrush, base.ClientRectangle);
			}
			using (Pen pen = new Pen(Color.FromArgb(215, 15, 15), 1.5f))
			{
				graphics.DrawLine(pen, 0, 0, base.Width, 0);
			}
			using (LinearGradientBrush linearGradientBrush2 = new LinearGradientBrush(new Rectangle(0, 0, base.Width, base.Height / 2), Color.FromArgb(80, Color.White), Color.Transparent, 90f))
			{
				graphics.FillRectangle(linearGradientBrush2, 0, 0, base.Width, base.Height / 2);
			}
			using (Pen pen2 = new Pen(Color.FromArgb(80, 40, 40, 40), 1f))
			{
				graphics.DrawLine(pen2, 0, base.Height - 1, base.Width, base.Height - 1);
			}
		}
	}
}
