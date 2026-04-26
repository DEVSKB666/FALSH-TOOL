using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using <PrivateImplementationDetails>{68F2EF73-9355-4257-ADA6-397CF8BB8E72};

namespace Attr_3
{
	// Token: 0x020000E0 RID: 224
	public class Type_55 : Control
	{
		// Token: 0x060003E7 RID: 999 RVA: 0x000245DC File Offset: 0x000227DC
		public float \u00A0()
		{
			return this.\u00A0;
		}

		// Token: 0x060003E8 RID: 1000 RVA: 0x000245E4 File Offset: 0x000227E4
		public void \u00A0(float A_1)
		{
			this.\u00A0 = A_1;
			this.\u2001 = A_1 / this.\u1680 * 270f;
			base.Invalidate();
		}

		// Token: 0x060003E9 RID: 1001 RVA: 0x00024607 File Offset: 0x00022807
		public float \u1680()
		{
			return this.\u1680;
		}

		// Token: 0x060003EA RID: 1002 RVA: 0x0002460F File Offset: 0x0002280F
		public void \u1680(float A_1)
		{
			this.\u1680 = A_1;
			base.Invalidate();
		}

		// Token: 0x060003EB RID: 1003 RVA: 0x0002461E File Offset: 0x0002281E
		public string \u2000()
		{
			return this.\u00A0;
		}

		// Token: 0x060003EC RID: 1004 RVA: 0x00024626 File Offset: 0x00022826
		public void \u00A0(string A_1)
		{
			this.\u00A0 = A_1;
			base.Invalidate();
		}

		// Token: 0x060003ED RID: 1005 RVA: 0x00024635 File Offset: 0x00022835
		public Color \u2001()
		{
			return this.\u00A0;
		}

		// Token: 0x060003EE RID: 1006 RVA: 0x0002463D File Offset: 0x0002283D
		public void \u00A0(Color A_1)
		{
			this.\u00A0 = A_1;
			base.Invalidate();
		}

		// Token: 0x060003EF RID: 1007 RVA: 0x0002464C File Offset: 0x0002284C
		public Type_55()
		{
			base.Size = new Size(180, 180);
			this.DoubleBuffered = true;
			this.BackColor = Color.Transparent;
			this.\u00A0 = new Timer
			{
				Interval = 20
			};
			this.Attr_2.Tick += this.\u00A0;
			this.Attr_2.Start();
		}

		// Token: 0x060003F0 RID: 1008 RVA: 0x000246E8 File Offset: 0x000228E8
		protected override void OnPaint(PaintEventArgs A_1)
		{
			Graphics graphics = A_1.Graphics;
			graphics.SmoothingMode = SmoothingMode.AntiAlias;
			graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
			int num = base.Width / 2;
			int num2 = base.Height / 2;
			int num3 = Math.Min(base.Width, base.Height) / 2 - 20;
			using (Pen pen = new Pen(Color.FromArgb(40, this.\u00A0), 8f))
			{
				pen.StartCap = LineCap.Round;
				pen.EndCap = LineCap.Round;
				graphics.DrawArc(pen, num - num3, num2 - num3, num3 * 2, num3 * 2, 135, 270);
			}
			using (Pen pen2 = new Pen(Color.FromArgb(80, this.\u00A0), 12f))
			{
				pen2.StartCap = LineCap.Round;
				pen2.EndCap = LineCap.Round;
				if (this.\u2000 > 0f)
				{
					graphics.DrawArc(pen2, (float)(num - num3), (float)(num2 - num3), (float)(num3 * 2), (float)(num3 * 2), 135f, this.\u2000);
				}
			}
			using (Pen pen3 = new Pen(this.\u00A0, 4f))
			{
				pen3.StartCap = LineCap.Round;
				pen3.EndCap = LineCap.Round;
				if (this.\u2000 > 0f)
				{
					graphics.DrawArc(pen3, (float)(num - num3), (float)(num2 - num3), (float)(num3 * 2), (float)(num3 * 2), 135f, this.\u2000);
				}
			}
			using (Font font = new Font("Bahnschrift SemiBold", 24f, FontStyle.Bold))
			{
				using (Font font2 = new Font("Consolas", 10f, FontStyle.Bold))
				{
					string text = ((int)this.\u00A0).ToString();
					SizeF sizeF = graphics.MeasureString(text, font);
					SizeF sizeF2 = graphics.MeasureString(this.\u00A0, font2);
					graphics.DrawString(text, font, new SolidBrush(Color.FromArgb(100, 0, 255, 255)), (float)num - sizeF.Width / 2f + 1f, (float)num2 - sizeF.Height / 2f);
					graphics.DrawString(text, font, new SolidBrush(Color.FromArgb(100, 255, 0, 0)), (float)num - sizeF.Width / 2f - 1f, (float)num2 - sizeF.Height / 2f);
					graphics.DrawString(text, font, new SolidBrush(Color.White), (float)num - sizeF.Width / 2f, (float)num2 - sizeF.Height / 2f);
					graphics.DrawString(this.\u00A0, font2, new SolidBrush(Color.FromArgb(180, Color.White)), (float)num - sizeF2.Width / 2f, (float)(num2 + 20));
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

		// Token: 0x060003F1 RID: 1009 RVA: 0x00024B04 File Offset: 0x00022D04
		protected override void Dispose(bool A_1)
		{
			if (A_1)
			{
				Timer u00A = this.\u00A0;
				if (u00A != null)
				{
					u00A.Dispose();
				}
			}
			base.Dispose(A_1);
		}

		// Token: 0x060003F2 RID: 1010 RVA: 0x00024B24 File Offset: 0x00022D24
		[CompilerGenerated]
		private void \u00A0(object A_1, EventArgs A_2)
		{
			float num = this.\u2001 - this.\u2000;
			this.\u2000 += num * 0.15f;
			if (Math.Abs(num) < 0.1f)
			{
				this.\u2000 = this.\u2001;
			}
			base.Invalidate();
		}

		// Token: 0x0400030A RID: 778
		private float \u00A0;

		// Token: 0x0400030B RID: 779
		private float \u1680 = 10000f;

		// Token: 0x0400030C RID: 780
		private string \u00A0 = "RPM";

		// Token: 0x0400030D RID: 781
		private Color \u00A0 = Color.Red;

		// Token: 0x0400030E RID: 782
		private Timer \u00A0;

		// Token: 0x0400030F RID: 783
		private float \u2000;

		// Token: 0x04000310 RID: 784
		private float \u2001;

		// Token: 0x04000311 RID: 785
		private Random \u00A0 = new Random();
	}
}
