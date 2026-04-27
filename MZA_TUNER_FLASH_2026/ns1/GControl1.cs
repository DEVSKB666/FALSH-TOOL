using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace ns1
{
	// Token: 0x020000E0 RID: 224
	public class GControl1 : Control
	{
		// Token: 0x060003E7 RID: 999 RVA: 0x0000DA55 File Offset: 0x0000BC55
		public float method_0()
		{
			return this.float_0;
		}

		// Token: 0x060003E8 RID: 1000 RVA: 0x0000DA5D File Offset: 0x0000BC5D
		public void method_1(float float_4)
		{
			this.float_0 = float_4;
			this.float_3 = float_4 / this.float_1 * 270f;
			base.Invalidate();
		}

		// Token: 0x060003E9 RID: 1001 RVA: 0x0000DA80 File Offset: 0x0000BC80
		public float method_2()
		{
			return this.float_1;
		}

		// Token: 0x060003EA RID: 1002 RVA: 0x0000DA88 File Offset: 0x0000BC88
		public void method_3(float float_4)
		{
			this.float_1 = float_4;
			base.Invalidate();
		}

		// Token: 0x060003EB RID: 1003 RVA: 0x0000DA97 File Offset: 0x0000BC97
		public string method_4()
		{
			return this.string_0;
		}

		// Token: 0x060003EC RID: 1004 RVA: 0x0000DA9F File Offset: 0x0000BC9F
		public void method_5(string string_1)
		{
			this.string_0 = string_1;
			base.Invalidate();
		}

		// Token: 0x060003ED RID: 1005 RVA: 0x0000DAAE File Offset: 0x0000BCAE
		public Color method_6()
		{
			return this.color_0;
		}

		// Token: 0x060003EE RID: 1006 RVA: 0x0000DAB6 File Offset: 0x0000BCB6
		public void method_7(Color color_1)
		{
			this.color_0 = color_1;
			base.Invalidate();
		}

		// Token: 0x060003EF RID: 1007 RVA: 0x00036B68 File Offset: 0x00034D68
		public GControl1()
		{
			base.Size = new Size(180, 180);
			this.DoubleBuffered = true;
			this.BackColor = Color.Transparent;
			this.timer_0 = new Timer
			{
				Interval = 20
			};
			this.timer_0.Tick += this.timer_0_Tick;
			this.timer_0.Start();
		}

		// Token: 0x060003F0 RID: 1008 RVA: 0x00036C04 File Offset: 0x00034E04
		protected override void OnPaint(PaintEventArgs pevent)
		{
			Graphics graphics = pevent.Graphics;
			graphics.SmoothingMode = SmoothingMode.AntiAlias;
			graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
			int num = base.Width / 2;
			int num2 = base.Height / 2;
			int num3 = Math.Min(base.Width, base.Height) / 2 - 20;
			using (Pen pen = new Pen(Color.FromArgb(40, this.color_0), 8f))
			{
				pen.StartCap = LineCap.Round;
				pen.EndCap = LineCap.Round;
				graphics.DrawArc(pen, num - num3, num2 - num3, num3 * 2, num3 * 2, 135, 270);
			}
			using (Pen pen2 = new Pen(Color.FromArgb(80, this.color_0), 12f))
			{
				pen2.StartCap = LineCap.Round;
				pen2.EndCap = LineCap.Round;
				if (this.float_2 > 0f)
				{
					graphics.DrawArc(pen2, (float)(num - num3), (float)(num2 - num3), (float)(num3 * 2), (float)(num3 * 2), 135f, this.float_2);
				}
			}
			using (Pen pen3 = new Pen(this.color_0, 4f))
			{
				pen3.StartCap = LineCap.Round;
				pen3.EndCap = LineCap.Round;
				if (this.float_2 > 0f)
				{
					graphics.DrawArc(pen3, (float)(num - num3), (float)(num2 - num3), (float)(num3 * 2), (float)(num3 * 2), 135f, this.float_2);
				}
			}
			using (Font font = new Font("Bahnschrift SemiBold", 24f, FontStyle.Bold))
			{
				using (Font font2 = new Font("Consolas", 10f, FontStyle.Bold))
				{
					string text = ((int)this.float_0).ToString();
					SizeF sizeF = graphics.MeasureString(text, font);
					SizeF sizeF2 = graphics.MeasureString(this.string_0, font2);
					graphics.DrawString(text, font, new SolidBrush(Color.FromArgb(100, 0, 255, 255)), (float)num - sizeF.Width / 2f + 1f, (float)num2 - sizeF.Height / 2f);
					graphics.DrawString(text, font, new SolidBrush(Color.FromArgb(100, 255, 0, 0)), (float)num - sizeF.Width / 2f - 1f, (float)num2 - sizeF.Height / 2f);
					graphics.DrawString(text, font, new SolidBrush(Color.White), (float)num - sizeF.Width / 2f, (float)num2 - sizeF.Height / 2f);
					graphics.DrawString(this.string_0, font2, new SolidBrush(Color.FromArgb(180, Color.White)), (float)num - sizeF2.Width / 2f, (float)(num2 + 20));
				}
			}
			using (Pen pen4 = new Pen(Color.FromArgb(100, Color.White), 2f))
			{
				for (int i = 0; i <= 10; i++)
				{
					float num4 = (float)((double)((float)(135 + i * 27)) * 3.141592653589793 / 180.0);
					float x = (float)num + (float)Math.Cos((double)num4) * (float)(num3 - 5);
					float y = (float)num2 + (float)Math.Sin((double)num4) * (float)(num3 - 5);
					float x2 = (float)num + (float)Math.Cos((double)num4) * (float)(num3 + 5);
					float y2 = (float)num2 + (float)Math.Sin((double)num4) * (float)(num3 + 5);
					graphics.DrawLine(pen4, x, y, x2, y2);
				}
			}
		}

		// Token: 0x060003F1 RID: 1009 RVA: 0x0000DAC5 File Offset: 0x0000BCC5
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				Timer timer = this.timer_0;
				if (timer != null)
				{
					timer.Dispose();
				}
			}
			base.Dispose(disposing);
		}

		// Token: 0x060003F2 RID: 1010 RVA: 0x00037020 File Offset: 0x00035220
		[CompilerGenerated]
		private void timer_0_Tick(object sender, EventArgs e)
		{
			float num = this.float_3 - this.float_2;
			this.float_2 += num * 0.15f;
			if (Math.Abs(num) < 0.1f)
			{
				this.float_2 = this.float_3;
			}
			base.Invalidate();
		}

		// Token: 0x0400030A RID: 778
		private float float_0;

		// Token: 0x0400030B RID: 779
		private float float_1 = 10000f;

		// Token: 0x0400030C RID: 780
		private string string_0 = "RPM";

		// Token: 0x0400030D RID: 781
		private Color color_0 = Color.Red;

		// Token: 0x0400030E RID: 782
		private Timer timer_0;

		// Token: 0x0400030F RID: 783
		private float float_2;

		// Token: 0x04000310 RID: 784
		private float float_3;

		// Token: 0x04000311 RID: 785
		private Random random_0 = new Random();
	}
}
