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

namespace ns1
{
	// Token: 0x02000090 RID: 144
	public static class GClass3
	{
		// Token: 0x06000146 RID: 326 RVA: 0x0001DDB8 File Offset: 0x0001BFB8
		public static DialogResult smethod_0(Form form_0, int int_0 = 10)
		{
			DialogResult result;
			using (GClass3.form0_0 = new GClass3.Form0(form_0, int_0))
			{
				DialogResult dialogResult = GClass3.form0_0.ShowDialog(form_0);
				GClass3.form0_0 = null;
				result = dialogResult;
			}
			return result;
		}

		// Token: 0x06000147 RID: 327 RVA: 0x0001DE04 File Offset: 0x0001C004
		public static void smethod_1()
		{
			if (GClass3.form0_0 != null && GClass3.form0_0.InvokeRequired)
			{
				GClass3.form0_0.BeginInvoke(new Action(GClass3.Class124.class124_0.method_0));
				return;
			}
			if (GClass3.form0_0 != null)
			{
				GClass3.form0_0.DialogResult = DialogResult.OK;
				GClass3.form0_0.Close();
			}
		}

		// Token: 0x040000BB RID: 187
		private static GClass3.Form0 form0_0;

		// Token: 0x02000091 RID: 145
		private class Form0 : Form
		{
			// Token: 0x06000148 RID: 328 RVA: 0x0001DE6C File Offset: 0x0001C06C
			public Form0(Form form_0, int int_2)
			{
				this.int_0 = int_2;
				this.dateTime_0 = DateTime.Now;
				base.FormBorderStyle = FormBorderStyle.None;
				base.ShowInTaskbar = false;
				base.StartPosition = FormStartPosition.CenterParent;
				base.TopMost = true;
				this.BackColor = Color.Black;
				base.Opacity = 0.75;
				base.Size = form_0.Size;
				base.Owner = form_0;
				try
				{
					string path = EC2D41B1-A2F9-4664-90D8-86645EE2E753.smethod_243();
					if (File.Exists(path))
					{
						this.color_0 = ColorTranslator.FromHtml(File.ReadAllText(path).Trim());
					}
				}
				catch
				{
					this.color_0 = Color.Fuchsia;
				}
				this.DoubleBuffered = true;
				base.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
				this.timer_0 = new System.Windows.Forms.Timer
				{
					Interval = 16
				};
				this.timer_0.Tick += this.timer_0_Tick;
				base.Load += this.Form0_Load;
			}

			// Token: 0x06000149 RID: 329 RVA: 0x0001DF8C File Offset: 0x0001C18C
			private void timer_0_Tick(object sender, EventArgs e)
			{
				GClass3.Form0.Class123 @class = new GClass3.Form0.Class123();
				this.long_0 += 1L;
				TimeSpan timeSpan = DateTime.Now - this.dateTime_0;
				double num = (double)this.int_0 - timeSpan.TotalSeconds;
				@class.int_0 = (int)Math.Ceiling(num);
				this.float_0 = (float)(num / (double)this.int_0 * 360.0);
				if (@class.int_0 != this.int_1 && @class.int_0 >= 0)
				{
					Task.Run(new Action(@class.method_0));
					this.int_1 = @class.int_0;
				}
				if (num <= 0.0)
				{
					this.timer_0.Stop();
					base.DialogResult = DialogResult.OK;
					base.Close();
				}
				base.Invalidate();
			}

			// Token: 0x0600014A RID: 330 RVA: 0x0001E05C File Offset: 0x0001C25C
			protected override void OnPaint(PaintEventArgs pevent)
			{
				Graphics graphics = pevent.Graphics;
				graphics.SmoothingMode = SmoothingMode.AntiAlias;
				graphics.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
				int num = base.Width / 2;
				int num2 = base.Height / 2;
				float num3 = (float)(Math.Sin((double)this.long_0 * 0.15) * 5.0);
				this.method_0(graphics, num, num2);
				this.method_1(graphics, num, num2, 180f + num3);
				int num4 = 110;
				using (Pen pen = new Pen(Color.FromArgb(60, this.color_0), 2f))
				{
					pen.DashStyle = DashStyle.Dash;
					graphics.DrawEllipse(pen, num - num4, num2 - num4, num4 * 2, num4 * 2);
					float startAngle = (float)(this.long_0 * -5L % 360L);
					graphics.DrawArc(new Pen(this.color_0, 3f), (float)(num - num4), (float)(num2 - num4), (float)(num4 * 2), (float)(num4 * 2), startAngle, 40f);
				}
				int int_ = 85;
				Rectangle rect = new Rectangle(num - 85, num2 - 85, 170, 170);
				int num5 = 30 + (int)(Math.Abs(Math.Sin((double)this.long_0 * 0.1)) * 20.0);
				for (int i = 0; i < 2; i++)
				{
					using (Pen pen2 = new Pen(Color.FromArgb(num5 - i * 10, this.color_0), (float)(10 + i * 5)))
					{
						graphics.DrawEllipse(pen2, rect);
					}
				}
				using (Pen pen3 = new Pen(this.color_0, 8f))
				{
					pen3.StartCap = LineCap.Round;
					pen3.EndCap = LineCap.Round;
					graphics.DrawArc(pen3, rect, -90f, this.float_0);
				}
				TimeSpan timeSpan = DateTime.Now - this.dateTime_0;
				int num6 = (int)Math.Ceiling((double)this.int_0 - timeSpan.TotalSeconds);
				if (num6 < 0)
				{
					num6 = 0;
				}
				using (Font font = new Font(EC2D41B1-A2F9-4664-90D8-86645EE2E753.smethod_50(), 68f + num3 / 2f, FontStyle.Regular))
				{
					string text = num6.ToString();
					SizeF sizeF = graphics.MeasureString(text, font);
					graphics.DrawString(text, font, Brushes.White, (float)num - sizeF.Width / 2f + 2f, (float)num2 - sizeF.Height / 2f);
				}
				this.method_2(graphics, num, num2, int_, num6);
			}

			// Token: 0x0600014B RID: 331 RVA: 0x0001E320 File Offset: 0x0001C520
			private void method_0(Graphics graphics_0, int int_2, int int_3)
			{
				using (Pen pen = new Pen(Color.FromArgb(12, this.color_0), 1f))
				{
					int num = 80;
					for (int i = -2; i <= 2; i++)
					{
						graphics_0.DrawLine(pen, int_2 - 180, int_3 + i * num, int_2 + 180, int_3 + i * num);
						graphics_0.DrawLine(pen, int_2 + i * num, int_3 - 180, int_2 + i * num, int_3 + 180);
					}
				}
			}

			// Token: 0x0600014C RID: 332 RVA: 0x0001E3B0 File Offset: 0x0001C5B0
			private void method_1(Graphics graphics_0, int int_2, int int_3, float float_1)
			{
				using (Pen pen = new Pen(this.color_0, 3f))
				{
					float num = float_1 / 2f;
					graphics_0.DrawLine(pen, (float)int_2 - num, (float)int_3 - num, (float)int_2 - num + 20f, (float)int_3 - num);
					graphics_0.DrawLine(pen, (float)int_2 - num, (float)int_3 - num, (float)int_2 - num, (float)int_3 - num + 20f);
					graphics_0.DrawLine(pen, (float)int_2 + num, (float)int_3 - num, (float)int_2 + num - 20f, (float)int_3 - num);
					graphics_0.DrawLine(pen, (float)int_2 + num, (float)int_3 - num, (float)int_2 + num, (float)int_3 - num + 20f);
					graphics_0.DrawLine(pen, (float)int_2 - num, (float)int_3 + num, (float)int_2 - num + 20f, (float)int_3 + num);
					graphics_0.DrawLine(pen, (float)int_2 - num, (float)int_3 + num, (float)int_2 - num, (float)int_3 + num - 20f);
					graphics_0.DrawLine(pen, (float)int_2 + num, (float)int_3 + num, (float)int_2 + num - 20f, (float)int_3 + num);
					graphics_0.DrawLine(pen, (float)int_2 + num, (float)int_3 + num, (float)int_2 + num, (float)int_3 + num - 20f);
				}
			}

			// Token: 0x0600014D RID: 333 RVA: 0x0001E4DC File Offset: 0x0001C6DC
			private void method_2(Graphics graphics_0, int int_2, int int_3, int int_4, int int_5)
			{
				using (Font font = new Font(EC2D41B1-A2F9-4664-90D8-86645EE2E753.smethod_7(), 18f, FontStyle.Bold))
				{
					string text = EC2D41B1-A2F9-4664-90D8-86645EE2E753.smethod_880();
					SizeF sizeF = graphics_0.MeasureString(text, font);
					graphics_0.DrawString(text, font, new SolidBrush(this.color_0), (float)int_2 - sizeF.Width / 2f, (float)(int_3 - int_4 - 70));
				}
				using (Font font2 = new Font(EC2D41B1-A2F9-4664-90D8-86645EE2E753.smethod_7(), 10f, FontStyle.Regular))
				{
					string text2 = string.Format(EC2D41B1-A2F9-4664-90D8-86645EE2E753.smethod_881(), int_5);
					SizeF sizeF2 = graphics_0.MeasureString(text2, font2);
					graphics_0.DrawString(text2, font2, Brushes.White, (float)int_2 - sizeF2.Width / 2f, (float)(int_3 + int_4 + 40));
				}
			}

			// Token: 0x0600014E RID: 334 RVA: 0x0000C834 File Offset: 0x0000AA34
			protected override void OnKeyDown(KeyEventArgs e)
			{
				if (e.KeyCode == Keys.Escape)
				{
					base.DialogResult = DialogResult.Cancel;
					base.Close();
				}
				base.OnKeyDown(e);
			}

			// Token: 0x0600014F RID: 335 RVA: 0x0000C854 File Offset: 0x0000AA54
			[CompilerGenerated]
			private void Form0_Load(object sender, EventArgs e)
			{
				this.timer_0.Start();
			}

			// Token: 0x040000BC RID: 188
			private System.Windows.Forms.Timer timer_0;

			// Token: 0x040000BD RID: 189
			private DateTime dateTime_0;

			// Token: 0x040000BE RID: 190
			private int int_0;

			// Token: 0x040000BF RID: 191
			private float float_0 = 360f;

			// Token: 0x040000C0 RID: 192
			private long long_0;

			// Token: 0x040000C1 RID: 193
			private Color color_0 = Color.Fuchsia;

			// Token: 0x040000C2 RID: 194
			private int int_1 = -1;

			// Token: 0x02000092 RID: 146
			[CompilerGenerated]
			private sealed class Class123
			{
				// Token: 0x06000151 RID: 337 RVA: 0x0001E5C0 File Offset: 0x0001C7C0
				internal void method_0()
				{
					try
					{
						if (this.int_0 > 3)
						{
							Console.Beep(800, 100);
						}
						else if (this.int_0 > 0)
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
				public int int_0;
			}
		}

		// Token: 0x02000093 RID: 147
		[CompilerGenerated]
		[Serializable]
		private sealed class Class124
		{
			// Token: 0x06000154 RID: 340 RVA: 0x0000C86D File Offset: 0x0000AA6D
			internal void method_0()
			{
				GClass3.form0_0.DialogResult = DialogResult.OK;
				GClass3.form0_0.Close();
			}

			// Token: 0x040000C4 RID: 196
			public static readonly GClass3.Class124 class124_0 = new GClass3.Class124();

			// Token: 0x040000C5 RID: 197
			public static Action action_0;
		}
	}
}
