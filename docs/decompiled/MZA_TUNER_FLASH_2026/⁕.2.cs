using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using <PrivateImplementationDetails>{68F2EF73-9355-4257-ADA6-397CF8BB8E72};

namespace Attr_3
{
	// Token: 0x020000A7 RID: 167
	public partial class Type_4B : Form
	{
		// Token: 0x060001C4 RID: 452 RVA: 0x0000FC80 File Offset: 0x0000DE80
		public Type_4B(Form A_1)
		{
			try
			{
				base.Owner = A_1;
				base.FormBorderStyle = FormBorderStyle.None;
				base.ShowInTaskbar = false;
				base.StartPosition = FormStartPosition.CenterParent;
				base.TopMost = true;
				this.BackColor = Color.FromArgb(10, 10, 10);
				base.Size = A_1.Size;
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
				base.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.DoubleBuffer, true);
				this.\u00A0 = new Timer();
				this.Attr_2.Interval = 30;
				this.Attr_2.Tick += this.\u00A0;
				base.Load += this.\u1680;
			}
			catch (Exception ex)
			{
				MessageBox.Show("Error initializing HUD: " + ex.Message);
			}
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x0000FDAC File Offset: 0x0000DFAC
		private void \u00A0(object A_1, EventArgs A_2)
		{
			this.\u00A0 -= 1.08f;
			this.\u1680++;
			if (this.\u1680 >= 33)
			{
				this.\u1680 = 0;
				this.\u00A0--;
				if (this.\u00A0 > 0)
				{
					Console.Beep(1000, 50);
				}
				else
				{
					this.Attr_2.Stop();
					Console.Beep(1500, 300);
					base.DialogResult = DialogResult.OK;
					base.Close();
				}
			}
			base.Invalidate();
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x0000FE3C File Offset: 0x0000E03C
		protected override void OnPaint(PaintEventArgs A_1)
		{
			Graphics graphics = A_1.Graphics;
			graphics.SmoothingMode = SmoothingMode.AntiAlias;
			graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
			int num = base.Width / 2;
			int num2 = base.Height / 2;
			int num3 = 120;
			Rectangle rect = new Rectangle(num - num3, num2 - num3, num3 * 2, num3 * 2);
			for (int i = 0; i < 8; i++)
			{
				using (Pen pen = new Pen(Color.FromArgb(12 - i, this.\u00A0), (float)(18 + i * 2)))
				{
					graphics.DrawEllipse(pen, rect);
				}
			}
			using (Pen pen2 = new Pen(this.\u00A0, 10f))
			{
				pen2.StartCap = LineCap.Round;
				pen2.EndCap = LineCap.Round;
				graphics.DrawArc(pen2, rect, -90f, this.\u00A0);
			}
			using (Font font = new Font("Segoe UI", 92f, FontStyle.Bold))
			{
				string text = this.Attr_2.ToString();
				SizeF sizeF = graphics.MeasureString(text, font);
				graphics.DrawString(text, font, Brushes.White, (float)num - sizeF.Width / 2f, (float)num2 - sizeF.Height / 2f);
			}
			using (Font font2 = new Font("Microsoft Sans Serif", 22f, FontStyle.Bold))
			{
				string text2 = "เตรียมพร้อมอัดไฟล์ลงกล่อง";
				SizeF sizeF2 = graphics.MeasureString(text2, font2);
				graphics.DrawString(text2, font2, new SolidBrush(this.\u00A0), (float)num - sizeF2.Width / 2f, (float)(num2 - num3 - 70));
			}
			using (Font font3 = new Font("Microsoft Sans Serif", 12f, FontStyle.Regular))
			{
				string text3 = "กรุณารอสักครู่ ระบบกำลังสื่อสารกับ ECU...";
				SizeF sizeF3 = graphics.MeasureString(text3, font3);
				graphics.DrawString(text3, font3, Brushes.Gray, (float)num - sizeF3.Width / 2f, (float)(num2 + num3 + 40));
			}
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x00010074 File Offset: 0x0000E274
		protected override void OnKeyDown(KeyEventArgs A_1)
		{
			if (A_1.KeyCode == Keys.Escape)
			{
				base.Close();
			}
			base.OnKeyDown(A_1);
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x0001008D File Offset: 0x0000E28D
		[CompilerGenerated]
		private void \u1680(object A_1, EventArgs A_2)
		{
			this.Attr_2.Start();
		}

		// Token: 0x04000139 RID: 313
		private Timer \u00A0;

		// Token: 0x0400013A RID: 314
		private int \u00A0 = 10;

		// Token: 0x0400013B RID: 315
		private float \u00A0 = 360f;

		// Token: 0x0400013C RID: 316
		private int \u1680;

		// Token: 0x0400013D RID: 317
		private Color \u00A0 = Color.Fuchsia;
	}
}
