using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using <PrivateImplementationDetails>{68F2EF73-9355-4257-ADA6-397CF8BB8E72};

namespace Attr_3
{
	// Token: 0x02000090 RID: 144
	public static class Type_45
	{
		// Token: 0x06000146 RID: 326 RVA: 0x0000A3D0 File Offset: 0x000085D0
		public static DialogResult \u00A0(Form A_0, int A_1 = 10)
		{
			DialogResult result;
			using (\u204F.\u00A0 = new \u204F.\u00A0(A_0, A_1))
			{
				DialogResult dialogResult = \u204F.Attr_2.ShowDialog(A_0);
				\u204F.\u00A0 = null;
				result = dialogResult;
			}
			return result;
		}

		// Token: 0x06000147 RID: 327 RVA: 0x0000A41C File Offset: 0x0000861C
		public static void \u00A0()
		{
			if (\u204F.\u00A0 != null && \u204F.Attr_2.InvokeRequired)
			{
				\u204F.Attr_2.BeginInvoke(new Action(\u204F.Attr_3.Attr_2.\u00A0));
				return;
			}
			if (\u204F.\u00A0 != null)
			{
				\u204F.Attr_2.DialogResult = DialogResult.OK;
				\u204F.Attr_2.Close();
			}
		}

		// Token: 0x040000BB RID: 187
		private static \u204F.\u00A0 \u00A0;

		// Token: 0x02000091 RID: 145
		private class Attr_2 : Form
		{
			// Token: 0x06000148 RID: 328 RVA: 0x0000A484 File Offset: 0x00008684
			public \u00A0(Form A_1, int A_2)
			{
				this.\u00A0 = A_2;
				this.\u00A0 = DateTime.Now;
				base.FormBorderStyle = FormBorderStyle.None;
				base.ShowInTaskbar = false;
				base.StartPosition = FormStartPosition.CenterParent;
				base.TopMost = true;
				this.BackColor = Color.Black;
				base.Opacity = 0.75;
				base.Size = A_1.Size;
				base.Owner = A_1;
				try
				{
					string path = "C:\\MZATUNER\\headerColor.dat";
					if (File.Exists(path))
					{
						this.\u00A0 = ColorTranslator.FromHtml(File.ReadAllText(path).Trim());
					}
				}
				catch
				{
					this.\u00A0 = Color.Fuchsia;
				}
				this.DoubleBuffered = true;
				base.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
				this.\u00A0 = new System.Windows.Forms.Timer
				{
					Interval = 16
				};
				this.Attr_2.Tick += this.\u00A0;
				base.Load += this.\u1680;
			}

			// Token: 0x06000149 RID: 329 RVA: 0x0000A5A4 File Offset: 0x000087A4
			private void \u00A0(object A_1, EventArgs A_2)
			{
				\u204F.Attr_2.\u00A0 u00A = new \u204F.Attr_2.\u00A0();
				this.\u00A0 += 1L;
				TimeSpan timeSpan = DateTime.Now - this.\u00A0;
				double num = (double)this.\u00A0 - timeSpan.TotalSeconds;
				u00A.\u00A0 = (int)Math.Ceiling(num);
				this.\u00A0 = (float)(num / (double)this.\u00A0 * 360.0);
				if (u00A.\u00A0 != this.\u1680 && u00A.\u00A0 >= 0)
				{
					Task.Run(new Action(u00A.\u00A0));
					this.\u1680 = u00A.\u00A0;
				}
				if (num <= 0.0)
				{
					this.Attr_2.Stop();
					base.DialogResult = DialogResult.OK;
					base.Close();
				}
				base.Invalidate();
			}

			// Token: 0x0600014A RID: 330 RVA: 0x0000A670 File Offset: 0x00008870
			protected override void OnPaint(PaintEventArgs A_1)
			{
				Graphics graphics = A_1.Graphics;
				graphics.SmoothingMode = SmoothingMode.AntiAlias;
				graphics.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
				int num = base.Width / 2;
				int num2 = base.Height / 2;
				float num3 = (float)(Math.Sin((double)this.\u00A0 * 0.15) * 5.0);
				this.\u00A0(graphics, num, num2);
				this.\u00A0(graphics, num, num2, 180f + num3);
				int num4 = 110;
				using (Pen pen = new Pen(Color.FromArgb(60, this.\u00A0), 2f))
				{
					pen.DashStyle = DashStyle.Dash;
					graphics.DrawEllipse(pen, num - num4, num2 - num4, num4 * 2, num4 * 2);
					float startAngle = (float)(this.\u00A0 * -5L % 360L);
					graphics.DrawArc(new Pen(this.\u00A0, 3f), (float)(num - num4), (float)(num2 - num4), (float)(num4 * 2), (float)(num4 * 2), startAngle, 40f);
				}
				int num5 = 85;
				Rectangle rect = new Rectangle(num - num5, num2 - num5, num5 * 2, num5 * 2);
				int num6 = 30 + (int)(Math.Abs(Math.Sin((double)this.\u00A0 * 0.1)) * 20.0);
				for (int i = 0; i < 2; i++)
				{
					using (Pen pen2 = new Pen(Color.FromArgb(num6 - i * 10, this.\u00A0), (float)(10 + i * 5)))
					{
						graphics.DrawEllipse(pen2, rect);
					}
				}
				using (Pen pen3 = new Pen(this.\u00A0, 8f))
				{
					pen3.StartCap = LineCap.Round;
					pen3.EndCap = LineCap.Round;
					graphics.DrawArc(pen3, rect, -90f, this.\u00A0);
				}
				TimeSpan timeSpan = DateTime.Now - this.\u00A0;
				int num7 = (int)Math.Ceiling((double)this.\u00A0 - timeSpan.TotalSeconds);
				if (num7 < 0)
				{
					num7 = 0;
				}
				using (Font font = new Font("Impact", 68f + num3 / 2f, FontStyle.Regular))
				{
					string text = num7.ToString();
					SizeF sizeF = graphics.MeasureString(text, font);
					graphics.DrawString(text, font, Brushes.White, (float)num - sizeF.Width / 2f + 2f, (float)num2 - sizeF.Height / 2f);
				}
				this.\u00A0(graphics, num, num2, num5, num7);
			}

			// Token: 0x0600014B RID: 331 RVA: 0x0000A928 File Offset: 0x00008B28
			private void \u00A0(Graphics A_1, int A_2, int A_3)
			{
				using (Pen pen = new Pen(Color.FromArgb(12, this.\u00A0), 1f))
				{
					int num = 80;
					for (int i = -2; i <= 2; i++)
					{
						A_1.DrawLine(pen, A_2 - 180, A_3 + i * num, A_2 + 180, A_3 + i * num);
						A_1.DrawLine(pen, A_2 + i * num, A_3 - 180, A_2 + i * num, A_3 + 180);
					}
				}
			}

			// Token: 0x0600014C RID: 332 RVA: 0x0000A9B8 File Offset: 0x00008BB8
			private void \u00A0(Graphics A_1, int A_2, int A_3, float A_4)
			{
				using (Pen pen = new Pen(this.\u00A0, 3f))
				{
					int num = 20;
					float num2 = A_4 / 2f;
					A_1.DrawLine(pen, (float)A_2 - num2, (float)A_3 - num2, (float)A_2 - num2 + (float)num, (float)A_3 - num2);
					A_1.DrawLine(pen, (float)A_2 - num2, (float)A_3 - num2, (float)A_2 - num2, (float)A_3 - num2 + (float)num);
					A_1.DrawLine(pen, (float)A_2 + num2, (float)A_3 - num2, (float)A_2 + num2 - (float)num, (float)A_3 - num2);
					A_1.DrawLine(pen, (float)A_2 + num2, (float)A_3 - num2, (float)A_2 + num2, (float)A_3 - num2 + (float)num);
					A_1.DrawLine(pen, (float)A_2 - num2, (float)A_3 + num2, (float)A_2 - num2 + (float)num, (float)A_3 + num2);
					A_1.DrawLine(pen, (float)A_2 - num2, (float)A_3 + num2, (float)A_2 - num2, (float)A_3 + num2 - (float)num);
					A_1.DrawLine(pen, (float)A_2 + num2, (float)A_3 + num2, (float)A_2 + num2 - (float)num, (float)A_3 + num2);
					A_1.DrawLine(pen, (float)A_2 + num2, (float)A_3 + num2, (float)A_2 + num2, (float)A_3 + num2 - (float)num);
				}
			}

			// Token: 0x0600014D RID: 333 RVA: 0x0000AAD0 File Offset: 0x00008CD0
			private void \u00A0(Graphics A_1, int A_2, int A_3, int A_4, int A_5)
			{
				using (Font font = new Font("Microsoft Sans Serif", 18f, FontStyle.Bold))
				{
					string text = "ระบบ: กำลังลบไฟล์ข้อมูลเก่า";
					SizeF sizeF = A_1.MeasureString(text, font);
					A_1.DrawString(text, font, new SolidBrush(this.\u00A0), (float)A_2 - sizeF.Width / 2f, (float)(A_3 - A_4 - 70));
				}
				using (Font font2 = new Font("Microsoft Sans Serif", 10f, FontStyle.Regular))
				{
					string text2 = string.Format("โหมด: ค้นหากล่อง ECU | สถานะ: กำลังรอ... | เวลา: {0} วินาที", A_5);
					SizeF sizeF2 = A_1.MeasureString(text2, font2);
					A_1.DrawString(text2, font2, Brushes.White, (float)A_2 - sizeF2.Width / 2f, (float)(A_3 + A_4 + 40));
				}
			}

			// Token: 0x0600014E RID: 334 RVA: 0x0000ABB4 File Offset: 0x00008DB4
			protected override void OnKeyDown(KeyEventArgs A_1)
			{
				if (A_1.KeyCode == Keys.Escape)
				{
					base.DialogResult = DialogResult.Cancel;
					base.Close();
				}
				base.OnKeyDown(A_1);
			}

			// Token: 0x0600014F RID: 335 RVA: 0x0000ABD4 File Offset: 0x00008DD4
			[CompilerGenerated]
			private void \u1680(object A_1, EventArgs A_2)
			{
				this.Attr_2.Start();
			}

			// Token: 0x040000BC RID: 188
			private System.Windows.Forms.Timer \u00A0;

			// Token: 0x040000BD RID: 189
			private DateTime \u00A0;

			// Token: 0x040000BE RID: 190
			private int \u00A0;

			// Token: 0x040000BF RID: 191
			private float \u00A0 = 360f;

			// Token: 0x040000C0 RID: 192
			private long \u00A0;

			// Token: 0x040000C1 RID: 193
			private Color \u00A0 = Color.Fuchsia;

			// Token: 0x040000C2 RID: 194
			private int \u1680 = -1;

			// Token: 0x02000092 RID: 146
			[CompilerGenerated]
			private sealed class Attr_2
			{
				// Token: 0x06000151 RID: 337 RVA: 0x0000ABE4 File Offset: 0x00008DE4
				internal void \u00A0()
				{
					try
					{
						if (this.\u00A0 > 3)
						{
							Console.Beep(800, 100);
						}
						else if (this.\u00A0 > 0)
						{
							Console.Beep(1200, 150);
						}
						else
						{
							Console.Beep(1500, 80);
							Thread.Sleep(20);
							Console.Beep(1800, 120);
						}
					}
					catch
					{
					}
				}

				// Token: 0x040000C3 RID: 195
				public int \u00A0;
			}
		}

		// Token: 0x02000093 RID: 147
		[CompilerGenerated]
		[Serializable]
		private sealed class Attr_3
		{
			// Token: 0x06000154 RID: 340 RVA: 0x0000AC64 File Offset: 0x00008E64
			internal void \u00A0()
			{
				\u204F.Attr_2.DialogResult = DialogResult.OK;
				\u204F.Attr_2.Close();
			}

			// Token: 0x040000C4 RID: 196
			public static readonly \u204F.\u1680 \u00A0 = new \u204F.\u1680();

			// Token: 0x040000C5 RID: 197
			public static Action \u00A0;
		}
	}
}
