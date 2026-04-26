using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using <PrivateImplementationDetails>{68F2EF73-9355-4257-ADA6-397CF8BB8E72};

namespace Form_4
{
	// Token: 0x020000F5 RID: 245
	public partial class Form_4 : Form
	{
		// Token: 0x06000465 RID: 1125
		[DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
		private static extern IntPtr M_4(int, int, int, int, int, int);

		// Token: 0x06000466 RID: 1126 RVA: 0x00027998 File Offset: 0x00025B98
		public Form_4()
		{
			base.Size = new Size(500, 500);
			base.FormBorderStyle = FormBorderStyle.None;
			base.StartPosition = FormStartPosition.CenterScreen;
			this.BackColor = Color.FromArgb(15, 15, 15);
			this.DoubleBuffered = true;
			base.TopMost = true;
			base.ShowInTaskbar = false;
			try
			{
				base.Region = Region.FromHrgn(\u2000.\u00A0(0, 0, base.Width, base.Height, 40, 40));
			}
			catch
			{
			}
			this.M_4 = new Timer
			{
				Interval = 30
			};
			this.M_4.Tick += this.M_4;
			this.M_4.Start();
		}

		// Token: 0x06000467 RID: 1127 RVA: 0x00027A6C File Offset: 0x00025C6C
		public void M_4(string A_1, int A_2 = -1)
		{
			\u2000.\u00A0 u00A = new \u2000.\u00A0();
			u00A.\u00A0 = this;
			u00A.\u00A0 = A_1;
			u00A.\u00A0 = A_2;
			if (base.InvokeRequired)
			{
				base.Invoke(new Action(u00A.\u00A0));
				return;
			}
			this.M_4 = u00A.\u00A0;
			if (u00A.\u00A0 >= 0)
			{
				this.M_4 = u00A.\u00A0;
			}
		}

		// Token: 0x06000468 RID: 1128 RVA: 0x00027AD4 File Offset: 0x00025CD4
		protected override void OnPaint(PaintEventArgs A_1)
		{
			Graphics graphics = A_1.Graphics;
			graphics.SmoothingMode = SmoothingMode.AntiAlias;
			graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
			using (SolidBrush solidBrush = new SolidBrush(Color.FromArgb(10, 10, 10)))
			{
				graphics.FillRectangle(solidBrush, base.ClientRectangle);
			}
			using (PathGradientBrush pathGradientBrush = new PathGradientBrush(this.M_4(new RectangleF(0f, 0f, (float)base.Width, (float)base.Height))))
			{
				pathGradientBrush.CenterColor = Color.FromArgb(20, 231, 76, 60);
				pathGradientBrush.SurroundColors = new Color[]
				{
					Color.Transparent
				};
				graphics.FillPath(pathGradientBrush, this.M_4(new RectangleF(0f, 0f, (float)base.Width, (float)base.Height)));
			}
			float num = (float)base.Width / 2f;
			float num2 = 180f;
			float num3 = 180f;
			graphics.TranslateTransform(num, num2);
			graphics.RotateTransform(this.M_4);
			using (Pen pen = new Pen(Color.FromArgb(231, 76, 60), 4f))
			{
				pen.StartCap = LineCap.Round;
				pen.EndCap = LineCap.Round;
				graphics.DrawArc(pen, -num3 / 2f, -num3 / 2f, num3, num3, 0f, 90f);
				graphics.DrawArc(pen, -num3 / 2f, -num3 / 2f, num3, num3, 180f, 90f);
			}
			graphics.ResetTransform();
			using (Pen pen2 = new Pen(Color.FromArgb(40, 255, 255, 255), 1f))
			{
				graphics.DrawEllipse(pen2, num - (num3 - 25f) / 2f, num2 - (num3 - 25f) / 2f, num3 - 25f, num3 - 25f);
			}
			using (Font font = new Font("Impact", 60f, FontStyle.Italic))
			{
				string text = "MZA";
				SizeF sizeF = graphics.MeasureString(text, font);
				graphics.DrawString(text, font, Brushes.White, num - sizeF.Width / 2f, num2 - sizeF.Height / 2f);
			}
			float num4 = 320f;
			using (Font font2 = new Font("Leelawadee UI", 24f, FontStyle.Bold))
			{
				string text2 = "MZA-TUNER";
				SizeF sizeF2 = graphics.MeasureString(text2, font2);
				graphics.DrawString(text2, font2, Brushes.WhiteSmoke, num - sizeF2.Width / 2f, num4);
			}
			using (Font font3 = new Font("Consolas", 10f, FontStyle.Bold))
			{
				string text3 = "เวอร์ชัน 2.0.26 พรีเมียม";
				SizeF sizeF3 = graphics.MeasureString(text3, font3);
				graphics.DrawString(text3, font3, new SolidBrush(Color.FromArgb(180, 231, 76, 60)), num - sizeF3.Width / 2f, num4 + 50f);
			}
			using (Font font4 = new Font("Leelawadee UI", 11f, FontStyle.Regular))
			{
				SizeF sizeF4 = graphics.MeasureString(this.M_4, font4);
				graphics.DrawString(this.M_4.ToUpper(), font4, Brushes.DimGray, num - sizeF4.Width / 2f, num4 + 80f);
			}
			int num5 = 320;
			int num6 = 4;
			float x = num - (float)(num5 / 2);
			float y = 450f;
			graphics.FillRectangle(new SolidBrush(Color.FromArgb(30, 30, 30)), x, y, (float)num5, (float)num6);
			if (this.M_6 > 0f)
			{
				float width = this.M_6 / 100f * (float)num5;
				using (SolidBrush solidBrush2 = new SolidBrush(Color.FromArgb(231, 76, 60)))
				{
					graphics.FillRectangle(solidBrush2, x, y, width, (float)num6);
				}
			}
			using (Font font5 = new Font("Segoe UI", 8f, FontStyle.Bold))
			{
				string text4 = "พัฒนาโดยทีมงาน MZA-TUNER TEAM © 2026";
				SizeF sizeF5 = graphics.MeasureString(text4, font5);
				using (SolidBrush solidBrush3 = new SolidBrush(Color.FromArgb(50, 50, 50)))
				{
					graphics.DrawString(text4, font5, solidBrush3, num - sizeF5.Width / 2f, (float)(base.Height - 30));
				}
			}
		}

		// Token: 0x06000469 RID: 1129 RVA: 0x00027FE0 File Offset: 0x000261E0
		private GraphicsPath M_4(RectangleF A_1)
		{
			GraphicsPath graphicsPath = new GraphicsPath();
			graphicsPath.AddEllipse(A_1);
			return graphicsPath;
		}

		// Token: 0x0600046A RID: 1130 RVA: 0x00027FF0 File Offset: 0x000261F0
		private GraphicsPath M_4(Rectangle A_1, int A_2)
		{
			GraphicsPath graphicsPath = new GraphicsPath();
			graphicsPath.AddArc(A_1.X, A_1.Y, A_2, A_2, 180f, 90f);
			graphicsPath.AddArc(A_1.X + A_1.Width - A_2, A_1.Y, A_2, A_2, 270f, 90f);
			graphicsPath.AddArc(A_1.X + A_1.Width - A_2, A_1.Y + A_1.Height - A_2, A_2, A_2, 0f, 90f);
			graphicsPath.AddArc(A_1.X, A_1.Y + A_1.Height - A_2, A_2, A_2, 90f, 90f);
			graphicsPath.CloseFigure();
			return graphicsPath;
		}

		// Token: 0x0600046C RID: 1132 RVA: 0x000280E0 File Offset: 0x000262E0
		[CompilerGenerated]
		private void M_4(object A_1, EventArgs A_2)
		{
			float num = (float)this.M_4 - this.M_6;
			if (Math.Abs(num) > 0.05f)
			{
				this.M_6 += num * 0.1f;
			}
			else
			{
				this.M_6 = (float)this.M_4;
			}
			this.M_4 += 4f;
			if (this.M_4 >= 360f)
			{
				this.M_4 = 0f;
			}
			base.Invalidate();
		}

		// Token: 0x04000346 RID: 838
		private float M_4;

		// Token: 0x04000347 RID: 839
		private float M_6;

		// Token: 0x04000348 RID: 840
		private int M_4;

		// Token: 0x04000349 RID: 841
		private string M_4 = "กำลังเริ่มระบบ MZA-TUNER...";

		// Token: 0x020000F6 RID: 246
		[CompilerGenerated]
		private sealed class Attr_2
		{
			// Token: 0x0600046E RID: 1134 RVA: 0x0002815C File Offset: 0x0002635C
			internal void M_4()
			{
				this.M_4.\u00A0 = this.M_4;
				if (this.M_4 >= 0)
				{
					this.M_4.\u00A0 = this.M_4;
				}
			}

			// Token: 0x0400034A RID: 842
			public \u2000 \u00A0;

			// Token: 0x0400034B RID: 843
			public string M_4;

			// Token: 0x0400034C RID: 844
			public int M_4;
		}
	}
}
