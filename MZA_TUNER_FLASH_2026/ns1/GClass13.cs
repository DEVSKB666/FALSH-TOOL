using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace ns1
{
	// Token: 0x020000E5 RID: 229
	public class GClass13 : ProgressBar
	{
		// Token: 0x06000414 RID: 1044 RVA: 0x0000DB2A File Offset: 0x0000BD2A
		public GClass13()
		{
			base.SetStyle(ControlStyles.UserPaint, true);
			base.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
			base.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
			this.BackColor = Color.Black;
			this.ForeColor = Color.Lime;
		}

		// Token: 0x06000415 RID: 1045 RVA: 0x00037C84 File Offset: 0x00035E84
		protected override void OnPaint(PaintEventArgs pevent)
		{
			Graphics graphics = pevent.Graphics;
			graphics.SmoothingMode = SmoothingMode.AntiAlias;
			Rectangle rect = new Rectangle(0, 0, base.Width - 1, base.Height - 1);
			using (SolidBrush solidBrush = new SolidBrush(Color.FromArgb(15, 15, 15)))
			{
				graphics.FillRectangle(solidBrush, rect);
			}
			using (Pen pen = new Pen(Color.FromArgb(60, 60, 60), 1f))
			{
				graphics.DrawRectangle(pen, rect);
			}
			if (base.Value > 0)
			{
				float num = (float)(base.Value - base.Minimum) / (float)(base.Maximum - base.Minimum);
				int num2 = (int)((float)(base.Width - 4) * num);
				int num3 = 10;
				int num4 = 2;
				int num5 = 4;
				using (SolidBrush solidBrush2 = new SolidBrush(this.ForeColor))
				{
					while (num5 < num2 + 2 && num5 < base.Width - 6)
					{
						Rectangle rect2 = new Rectangle(num5, 3, num3, base.Height - 7);
						graphics.FillRectangle(solidBrush2, rect2);
						num5 += num3 + num4;
					}
				}
			}
			string text = ((int)((float)(base.Value - base.Minimum) / (float)(base.Maximum - base.Minimum) * 100f)).ToString() + "%";
			using (Font font = new Font("Segoe UI", 9f, FontStyle.Bold))
			{
				SizeF sizeF = graphics.MeasureString(text, font);
				graphics.DrawString(text, font, Brushes.Black, ((float)base.Width - sizeF.Width) / 2f + 1f, ((float)base.Height - sizeF.Height) / 2f + 1f);
				graphics.DrawString(text, font, Brushes.White, ((float)base.Width - sizeF.Width) / 2f, ((float)base.Height - sizeF.Height) / 2f);
			}
		}
	}
}
