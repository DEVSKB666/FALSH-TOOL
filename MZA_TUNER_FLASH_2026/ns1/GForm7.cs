using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace ns1
{
	// Token: 0x020000A7 RID: 167
	public partial class GForm7 : Form
	{
		// Token: 0x060001C4 RID: 452 RVA: 0x00023224 File Offset: 0x00021424
		public GForm7(Form form_0)
		{
			try
			{
				base.Owner = form_0;
				base.FormBorderStyle = FormBorderStyle.None;
				base.ShowInTaskbar = false;
				base.StartPosition = FormStartPosition.CenterParent;
				base.TopMost = true;
				this.BackColor = Color.FromArgb(10, 10, 10);
				base.Size = form_0.Size;
				try
				{
					string path = "C:\\MZATUNER\\headerColor.dat";
					if (File.Exists(path))
					{
						this.color_0 = ColorTranslator.FromHtml(File.ReadAllText(path).Trim());
					}
				}
				catch
				{
					this.color_0 = Color.Fuchsia;
				}
				base.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.DoubleBuffer, true);
				this.timer_0 = new Timer();
				this.timer_0.Interval = 30;
				this.timer_0.Tick += this.timer_0_Tick;
				base.Load += this.GForm7_Load;
			}
			catch (Exception ex)
			{
				MessageBox.Show("Error initializing HUD: " + ex.Message);
			}
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x00023350 File Offset: 0x00021550
		private void timer_0_Tick(object sender, EventArgs e)
		{
			this.float_0 -= 1.08f;
			this.int_1++;
			if (this.int_1 >= 33)
			{
				this.int_1 = 0;
				this.int_0--;
				if (this.int_0 > 0)
				{
					Console.Beep(1000, 50);
				}
				else
				{
					this.timer_0.Stop();
					Console.Beep(1500, 300);
					base.DialogResult = DialogResult.OK;
					base.Close();
				}
			}
			base.Invalidate();
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x000233E0 File Offset: 0x000215E0
		protected override void OnPaint(PaintEventArgs pevent)
		{
			Graphics graphics = pevent.Graphics;
			graphics.SmoothingMode = SmoothingMode.AntiAlias;
			graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
			int num = base.Width / 2;
			int num2 = base.Height / 2;
			int num3 = 120;
			Rectangle rect = new Rectangle(num - 120, num2 - 120, 240, 240);
			for (int i = 0; i < 8; i++)
			{
				using (Pen pen = new Pen(Color.FromArgb(12 - i, this.color_0), (float)(18 + i * 2)))
				{
					graphics.DrawEllipse(pen, rect);
				}
			}
			using (Pen pen2 = new Pen(this.color_0, 10f))
			{
				pen2.StartCap = LineCap.Round;
				pen2.EndCap = LineCap.Round;
				graphics.DrawArc(pen2, rect, -90f, this.float_0);
			}
			using (Font font = new Font("Segoe UI", 92f, FontStyle.Bold))
			{
				string text = this.int_0.ToString();
				SizeF sizeF = graphics.MeasureString(text, font);
				graphics.DrawString(text, font, Brushes.White, (float)num - sizeF.Width / 2f, (float)num2 - sizeF.Height / 2f);
			}
			using (Font font2 = new Font("Microsoft Sans Serif", 22f, FontStyle.Bold))
			{
				string text2 = "เตรียมพร้อมอัดไฟล์ลงกล่อง";
				SizeF sizeF2 = graphics.MeasureString(text2, font2);
				graphics.DrawString(text2, font2, new SolidBrush(this.color_0), (float)num - sizeF2.Width / 2f, (float)(num2 - num3 - 70));
			}
			using (Font font3 = new Font("Microsoft Sans Serif", 12f, FontStyle.Regular))
			{
				string text3 = "กรุณารอสักครู่ ระบบกำลังสื่อสารกับ ECU...";
				SizeF sizeF3 = graphics.MeasureString(text3, font3);
				graphics.DrawString(text3, font3, Brushes.Gray, (float)num - sizeF3.Width / 2f, (float)(num2 + num3 + 40));
			}
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x0000CC9D File Offset: 0x0000AE9D
		protected override void OnKeyDown(KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Escape)
			{
				base.Close();
			}
			base.OnKeyDown(e);
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x0000CCB6 File Offset: 0x0000AEB6
		[CompilerGenerated]
		private void GForm7_Load(object sender, EventArgs e)
		{
			this.timer_0.Start();
		}

		// Token: 0x04000139 RID: 313
		private Timer timer_0;

		// Token: 0x0400013A RID: 314
		private int int_0 = 10;

		// Token: 0x0400013B RID: 315
		private float float_0 = 360f;

		// Token: 0x0400013C RID: 316
		private int int_1;

		// Token: 0x0400013D RID: 317
		private Color color_0 = Color.Fuchsia;
	}
}
