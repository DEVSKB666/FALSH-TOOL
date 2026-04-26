using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;
using <PrivateImplementationDetails>{68F2EF73-9355-4257-ADA6-397CF8BB8E72};

namespace Attr_3
{
	// Token: 0x0200008D RID: 141
	public class Type_43 : Control
	{
		// Token: 0x06000130 RID: 304 RVA: 0x0000A166 File Offset: 0x00008366
		public int \u00A0()
		{
			return this.\u00A0;
		}

		// Token: 0x06000131 RID: 305 RVA: 0x0000A16E File Offset: 0x0000836E
		public void \u00A0(int A_1)
		{
			this.\u00A0 = Math.Min(Math.Max(A_1, 0), this.\u1680);
			base.Invalidate();
		}

		// Token: 0x06000132 RID: 306 RVA: 0x0000A18E File Offset: 0x0000838E
		public int \u1680()
		{
			return this.\u1680;
		}

		// Token: 0x06000133 RID: 307 RVA: 0x0000A196 File Offset: 0x00008396
		public void \u1680(int A_1)
		{
			this.\u1680 = A_1;
			base.Invalidate();
		}

		// Token: 0x06000134 RID: 308 RVA: 0x0000A1A5 File Offset: 0x000083A5
		public int \u2000()
		{
			return this.\u2000;
		}

		// Token: 0x06000135 RID: 309 RVA: 0x0000A1AD File Offset: 0x000083AD
		public void \u2000(int A_1)
		{
			this.\u2000 = A_1;
			base.Invalidate();
		}

		// Token: 0x06000136 RID: 310 RVA: 0x0000A1BC File Offset: 0x000083BC
		public Color \u2001()
		{
			return this.\u00A0;
		}

		// Token: 0x06000137 RID: 311 RVA: 0x0000A1C4 File Offset: 0x000083C4
		public void \u00A0(Color A_1)
		{
			this.\u00A0 = A_1;
			base.Invalidate();
		}

		// Token: 0x06000138 RID: 312 RVA: 0x0000A1D4 File Offset: 0x000083D4
		public Type_43()
		{
			base.Size = new Size(100, 100);
			this.BackColor = Color.Black;
			this.DoubleBuffered = true;
		}

		// Token: 0x06000139 RID: 313 RVA: 0x0000A224 File Offset: 0x00008424
		protected override void OnPaint(PaintEventArgs A_1)
		{
			base.OnPaint(A_1);
			Graphics graphics = A_1.Graphics;
			graphics.SmoothingMode = SmoothingMode.HighQuality;
			graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
			graphics.Clear(this.BackColor);
			float startAngle = -90f;
			float num = (float)this.\u00A0 / (float)this.\u1680 * 360f;
			Rectangle rect = new Rectangle(this.\u2000 / 2, this.\u2000 / 2, base.Width - this.\u2000, base.Height - this.\u2000);
			using (Pen pen = new Pen(Color.FromArgb(45, 45, 48), (float)this.\u2000))
			{
				graphics.DrawEllipse(pen, rect);
			}
			if (num > 0f)
			{
				using (Pen pen2 = new Pen(this.\u00A0, (float)this.\u2000))
				{
					pen2.StartCap = LineCap.Round;
					pen2.EndCap = LineCap.Round;
					graphics.DrawArc(pen2, rect, startAngle, num);
				}
			}
			string text = string.Format("{0}%", this.\u00A0 * 100 / this.\u1680);
			using (Font font = new Font("Segoe UI", (float)(base.Height / 5), FontStyle.Bold))
			{
				SizeF sizeF = graphics.MeasureString(text, font);
				graphics.DrawString(text, font, Brushes.White, ((float)base.Width - sizeF.Width) / 2f, ((float)base.Height - sizeF.Height) / 2f);
			}
		}

		// Token: 0x040000A4 RID: 164
		private int \u00A0;

		// Token: 0x040000A5 RID: 165
		private int \u1680 = 100;

		// Token: 0x040000A6 RID: 166
		private int \u2000 = 8;

		// Token: 0x040000A7 RID: 167
		private Color \u00A0 = Color.Red;
	}
}
