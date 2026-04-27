using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

namespace ns1
{
	// Token: 0x0200008D RID: 141
	public class GControl0 : Control
	{
		// Token: 0x06000130 RID: 304 RVA: 0x0000C7C7 File Offset: 0x0000A9C7
		public int method_0()
		{
			return this.int_0;
		}

		// Token: 0x06000131 RID: 305 RVA: 0x0000C7CF File Offset: 0x0000A9CF
		public void method_1(int int_3)
		{
			this.int_0 = Math.Min(Math.Max(int_3, 0), this.int_1);
			base.Invalidate();
		}

		// Token: 0x06000132 RID: 306 RVA: 0x0000C7EF File Offset: 0x0000A9EF
		public int method_2()
		{
			return this.int_1;
		}

		// Token: 0x06000133 RID: 307 RVA: 0x0000C7F7 File Offset: 0x0000A9F7
		public void method_3(int int_3)
		{
			this.int_1 = int_3;
			base.Invalidate();
		}

		// Token: 0x06000134 RID: 308 RVA: 0x0000C806 File Offset: 0x0000AA06
		public int method_4()
		{
			return this.int_2;
		}

		// Token: 0x06000135 RID: 309 RVA: 0x0000C80E File Offset: 0x0000AA0E
		public void method_5(int int_3)
		{
			this.int_2 = int_3;
			base.Invalidate();
		}

		// Token: 0x06000136 RID: 310 RVA: 0x0000C81D File Offset: 0x0000AA1D
		public Color method_6()
		{
			return this.color_0;
		}

		// Token: 0x06000137 RID: 311 RVA: 0x0000C825 File Offset: 0x0000AA25
		public void method_7(Color color_1)
		{
			this.color_0 = color_1;
			base.Invalidate();
		}

		// Token: 0x06000138 RID: 312 RVA: 0x0001DBBC File Offset: 0x0001BDBC
		public GControl0()
		{
			base.Size = new Size(100, 100);
			this.BackColor = Color.Black;
			this.DoubleBuffered = true;
		}

		// Token: 0x06000139 RID: 313 RVA: 0x0001DC0C File Offset: 0x0001BE0C
		protected override void OnPaint(PaintEventArgs pevent)
		{
			base.OnPaint(pevent);
			Graphics graphics = pevent.Graphics;
			graphics.SmoothingMode = SmoothingMode.HighQuality;
			graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
			graphics.Clear(this.BackColor);
			float startAngle = -90f;
			float num = (float)this.int_0 / (float)this.int_1 * 360f;
			Rectangle rect = new Rectangle(this.int_2 / 2, this.int_2 / 2, base.Width - this.int_2, base.Height - this.int_2);
			using (Pen pen = new Pen(Color.FromArgb(45, 45, 48), (float)this.int_2))
			{
				graphics.DrawEllipse(pen, rect);
			}
			if (num > 0f)
			{
				using (Pen pen2 = new Pen(this.color_0, (float)this.int_2))
				{
					pen2.StartCap = LineCap.Round;
					pen2.EndCap = LineCap.Round;
					graphics.DrawArc(pen2, rect, startAngle, num);
				}
			}
			string text = string.Format("{0}%", this.int_0 * 100 / this.int_1);
			using (Font font = new Font("Segoe UI", (float)(base.Height / 5), FontStyle.Bold))
			{
				SizeF sizeF = graphics.MeasureString(text, font);
				graphics.DrawString(text, font, Brushes.White, ((float)base.Width - sizeF.Width) / 2f, ((float)base.Height - sizeF.Height) / 2f);
			}
		}

		// Token: 0x040000A4 RID: 164
		private int int_0;

		// Token: 0x040000A5 RID: 165
		private int int_1 = 100;

		// Token: 0x040000A6 RID: 166
		private int int_2 = 8;

		// Token: 0x040000A7 RID: 167
		private Color color_0 = Color.Red;
	}
}
