using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using <PrivateImplementationDetails>{68F2EF73-9355-4257-ADA6-397CF8BB8E72};

namespace Attr_3
{
	// Token: 0x020000D9 RID: 217
	public partial class Type_51 : Form
	{
		// Token: 0x060003B7 RID: 951
		[DllImport("user32.dll", EntryPoint = "ReleaseCapture")]
		public static extern bool \u00A0();

		// Token: 0x060003B8 RID: 952
		[DllImport("user32.dll", EntryPoint = "SendMessage")]
		public static extern int \u00A0(IntPtr, int, int, int);

		// Token: 0x060003B9 RID: 953 RVA: 0x00022AB4 File Offset: 0x00020CB4
		public Type_51()
		{
			base.Size = new Size(450, 300);
			base.FormBorderStyle = FormBorderStyle.None;
			this.BackColor = this.\u00A0;
			base.StartPosition = FormStartPosition.CenterParent;
			this.DoubleBuffered = true;
			base.Region = Region.FromHrgn(\u205B.\u00A0(0, 0, base.Width, base.Height, 20, 20));
			this.\u1680();
		}

		// Token: 0x060003BA RID: 954 RVA: 0x00022B58 File Offset: 0x00020D58
		private void \u1680()
		{
			\u205B.\u1680 u = new \u205B.\u1680();
			u.\u00A0 = this;
			u.\u00A0 = new Panel
			{
				Size = new Size(base.Width, 45),
				Location = new Point(0, 0),
				BackColor = this.\u1680
			};
			u.Attr_2.MouseDown += u.\u00A0;
			u.Attr_2.Paint += u.\u00A0;
			Label label = new Label
			{
				Text = "✕",
				Font = new Font("Segoe UI", 13f, FontStyle.Bold),
				ForeColor = Color.White,
				BackColor = Color.Transparent,
				AutoSize = true,
				Cursor = Cursors.Hand,
				Location = new Point(base.Width - 30, 10)
			};
			label.Click += u.\u00A0;
			u.Attr_2.Controls.Add(label);
			base.Controls.Add(u.\u00A0);
			this.\u00A0 = new RadioButton
			{
				Text = "48KB ROM",
				ForeColor = Color.LightGray,
				Font = new Font("Segoe UI", 10f, FontStyle.Bold),
				Location = new Point(110, 65),
				AutoSize = true,
				Cursor = Cursors.Hand
			};
			this.Attr_2.CheckedChanged += u.\u1680;
			this.\u1680 = new RadioButton
			{
				Text = "64KB ROM",
				ForeColor = Color.LightGray,
				Font = new Font("Segoe UI", 10f, FontStyle.Bold),
				Location = new Point(250, 65),
				AutoSize = true,
				Checked = true,
				Cursor = Cursors.Hand
			};
			this.Attr_3.CheckedChanged += u.\u2000;
			base.Controls.Add(this.\u00A0);
			base.Controls.Add(this.\u1680);
			Panel value = this.\u00A0(new Point(35, 110), "2000", out this.\u00A0);
			base.Controls.Add(value);
			Panel value2 = this.\u00A0(new Point(245, 110), "2000", out this.\u1680);
			base.Controls.Add(value2);
			u.\u00A0 = new \u205B.\u00A0
			{
				Text = "📂 เลือกไฟล์ .bin (ต้นฉบับ)",
				Location = new Point(35, 157),
				Size = new Size(380, 36)
			};
			u.Attr_2.Click += u.\u2001;
			base.Controls.Add(u.\u00A0);
			\u205B.\u00A0 u00A = new \u205B.\u00A0
			{
				Text = "[-] ใช้งาน (แพทช์ข้อมูล)",
				Location = new Point(35, 203),
				Size = new Size(380, 36)
			};
			u00A.Click += u.\u2002;
			base.Controls.Add(u00A);
			\u205B.\u00A0 u00A2 = new \u205B.\u00A0
			{
				Text = "[+] บันทึกไฟล์ออก",
				Location = new Point(35, 249),
				Size = new Size(380, 36)
			};
			u00A2.Click += u.\u2003;
			base.Controls.Add(u00A2);
		}

		// Token: 0x060003BB RID: 955 RVA: 0x00022EDC File Offset: 0x000210DC
		protected override void OnPaint(PaintEventArgs A_1)
		{
			base.OnPaint(A_1);
			A_1.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
			using (Pen pen = new Pen(Color.FromArgb(22, 22, 25), 1f))
			{
				for (int i = 0; i < base.Width; i += 25)
				{
					A_1.Graphics.DrawLine(pen, i, 45, i, base.Height);
				}
				for (int j = 45; j < base.Height; j += 25)
				{
					A_1.Graphics.DrawLine(pen, 0, j, base.Width, j);
				}
			}
			using (GraphicsPath graphicsPath = \u205B.\u00A0(new Rectangle(0, 0, base.Width - 1, base.Height - 1), 20))
			{
				using (Pen pen2 = new Pen(Color.FromArgb(120, 220, 25, 35), 2f))
				{
					A_1.Graphics.DrawPath(pen2, graphicsPath);
				}
			}
		}

		// Token: 0x060003BC RID: 956 RVA: 0x00022FF8 File Offset: 0x000211F8
		private Panel \u00A0(Point A_1, string A_2, out TextBox A_3)
		{
			\u205B.\u2000 u = new \u205B.\u2000();
			u.\u00A0 = new Panel
			{
				Size = new Size(170, 35),
				Location = A_1,
				BackColor = this.\u00A0
			};
			u.Attr_2.Paint += u.\u00A0;
			A_3 = new TextBox
			{
				Text = A_2,
				BorderStyle = BorderStyle.None,
				BackColor = Color.White,
				ForeColor = Color.Black,
				Font = new Font("Segoe UI", 12f, FontStyle.Bold),
				TextAlign = HorizontalAlignment.Center,
				Size = new Size(160, 25),
				Location = new Point(5, 7)
			};
			u.Attr_2.Controls.Add(A_3);
			return u.\u00A0;
		}

		// Token: 0x060003BD RID: 957 RVA: 0x000230D4 File Offset: 0x000212D4
		private void \u2000()
		{
			if (this.\u00A0 == null)
			{
				return;
			}
			try
			{
				if (this.Attr_2.Checked && this.Attr_2.Length >= 36628)
				{
					ushort num = (ushort)((int)this.\u00A0[36626] | (int)this.\u00A0[36627] << 8);
					ushort num2 = (ushort)((int)this.\u00A0[36628] | (int)this.\u00A0[36629] << 8);
					this.Attr_2.Text = Math.Round((double)num * 0.274653, 0).ToString();
					this.Attr_3.Text = Math.Round((double)num2 * 0.274653, 0).ToString();
				}
				else if (this.Attr_3.Checked && this.Attr_2.Length >= 39337)
				{
					ushort num3 = (ushort)((int)this.\u00A0[39333] | (int)this.\u00A0[39334] << 8);
					ushort num4 = (ushort)((int)this.\u00A0[39335] | (int)this.\u00A0[39336] << 8);
					this.Attr_2.Text = Math.Round((double)num3 * 0.274653, 0).ToString();
					this.Attr_3.Text = Math.Round((double)num4 * 0.274653, 0).ToString();
				}
			}
			catch
			{
			}
		}

		// Token: 0x060003BE RID: 958 RVA: 0x00023260 File Offset: 0x00021460
		private static GraphicsPath \u00A0(Rectangle A_0, int A_1)
		{
			int num = A_1 * 2;
			GraphicsPath graphicsPath = new GraphicsPath();
			graphicsPath.AddArc(A_0.X, A_0.Y, num, num, 180f, 90f);
			graphicsPath.AddArc(A_0.X + A_0.Width - num, A_0.Y, num, num, 270f, 90f);
			graphicsPath.AddArc(A_0.X + A_0.Width - num, A_0.Y + A_0.Height - num, num, num, 0f, 90f);
			graphicsPath.AddArc(A_0.X, A_0.Y + A_0.Height - num, num, num, 90f, 90f);
			graphicsPath.CloseFigure();
			return graphicsPath;
		}

		// Token: 0x060003BF RID: 959
		[DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
		private static extern IntPtr \u00A0(int, int, int, int, int, int);

		// Token: 0x040002F2 RID: 754
		private Color \u00A0 = Color.FromArgb(17, 17, 19);

		// Token: 0x040002F3 RID: 755
		private Color \u1680 = Color.FromArgb(220, 25, 35);

		// Token: 0x040002F4 RID: 756
		public TextBox \u00A0;

		// Token: 0x040002F5 RID: 757
		public TextBox \u1680;

		// Token: 0x040002F6 RID: 758
		private RadioButton \u00A0;

		// Token: 0x040002F7 RID: 759
		private RadioButton \u1680;

		// Token: 0x040002F8 RID: 760
		private byte[] \u00A0;

		// Token: 0x040002F9 RID: 761
		private string \u00A0 = "";

		// Token: 0x020000DA RID: 218
		private class Attr_2 : Control
		{
			// Token: 0x060003C0 RID: 960 RVA: 0x00023324 File Offset: 0x00021524
			public \u00A0()
			{
				this.DoubleBuffered = true;
				this.Cursor = Cursors.Hand;
			}

			// Token: 0x060003C1 RID: 961 RVA: 0x0002333E File Offset: 0x0002153E
			protected override void OnMouseEnter(EventArgs A_1)
			{
				this.\u00A0 = true;
				base.Invalidate();
				base.OnMouseEnter(A_1);
			}

			// Token: 0x060003C2 RID: 962 RVA: 0x00023354 File Offset: 0x00021554
			protected override void OnMouseLeave(EventArgs A_1)
			{
				this.\u00A0 = false;
				base.Invalidate();
				base.OnMouseLeave(A_1);
			}

			// Token: 0x060003C3 RID: 963 RVA: 0x0002336C File Offset: 0x0002156C
			protected override void OnPaint(PaintEventArgs A_1)
			{
				base.OnPaint(A_1);
				A_1.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
				Rectangle rectangle = new Rectangle(0, 0, base.Width - 1, base.Height - 1);
				Color color = this.\u00A0 ? Color.FromArgb(255, 45, 55) : Color.FromArgb(235, 30, 40);
				Color color2 = this.\u00A0 ? Color.FromArgb(210, 20, 30) : Color.FromArgb(200, 20, 30);
				Color color3 = this.\u00A0 ? Color.FromArgb(255, 100, 100) : Color.FromArgb(255, 60, 70);
				using (GraphicsPath graphicsPath = \u205B.\u00A0(rectangle, 8))
				{
					using (LinearGradientBrush linearGradientBrush = new LinearGradientBrush(rectangle, color, color2, 90f))
					{
						A_1.Graphics.FillPath(linearGradientBrush, graphicsPath);
					}
					A_1.Graphics.DrawPath(new Pen(color3, this.\u00A0 ? 2f : 1f), graphicsPath);
				}
				using (Font font = new Font("Segoe UI", 9.5f, FontStyle.Bold))
				{
					StringFormat format = new StringFormat
					{
						Alignment = StringAlignment.Center,
						LineAlignment = StringAlignment.Center
					};
					A_1.Graphics.DrawString(this.Text, font, Brushes.White, rectangle, format);
				}
			}

			// Token: 0x040002FA RID: 762
			private bool \u00A0;
		}

		// Token: 0x020000DB RID: 219
		[CompilerGenerated]
		private sealed class Attr_3
		{
			// Token: 0x060003C5 RID: 965 RVA: 0x00023504 File Offset: 0x00021704
			internal void \u00A0(object A_1, MouseEventArgs A_2)
			{
				if (A_2.Button == MouseButtons.Left)
				{
					\u205B.\u00A0();
					\u205B.\u00A0(this.Attr_2.Handle, 161, 2, 0);
				}
			}

			// Token: 0x060003C6 RID: 966 RVA: 0x00023534 File Offset: 0x00021734
			internal void \u00A0(object A_1, PaintEventArgs A_2)
			{
				A_2.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
				using (GraphicsPath graphicsPath = \u205B.\u00A0(new Rectangle(12, 8, 26, 28), 6))
				{
					A_2.Graphics.FillPath(Brushes.White, graphicsPath);
				}
				using (Font font = new Font("Arial", 14f, FontStyle.Bold))
				{
					A_2.Graphics.DrawString("M", font, new SolidBrush(this.Attr_2.\u1680), 13f, 11f);
				}
				using (Font font2 = new Font("Segoe UI", 10.5f, FontStyle.Bold))
				{
					A_2.Graphics.DrawString("ปลดการจ่ายน้ำมัน TPS 0 % (MZATUNER EDGE)", font2, Brushes.White, 48f, 12f);
				}
				using (Pen pen = new Pen(Color.FromArgb(80, 255, 255, 255), 2f))
				{
					A_2.Graphics.DrawLine(pen, this.Attr_2.Width - 120, 18, this.Attr_2.Width - 60, 18);
					A_2.Graphics.DrawLine(pen, this.Attr_2.Width - 140, 24, this.Attr_2.Width - 70, 24);
					A_2.Graphics.FillRectangle(Brushes.White, this.Attr_2.Width - 55, 16, 4, 10);
				}
			}

			// Token: 0x060003C7 RID: 967 RVA: 0x000236E4 File Offset: 0x000218E4
			internal void \u00A0(object A_1, EventArgs A_2)
			{
				this.Attr_2.Close();
			}

			// Token: 0x060003C8 RID: 968 RVA: 0x000236F1 File Offset: 0x000218F1
			internal void \u1680(object A_1, EventArgs A_2)
			{
				if (this.Attr_2.Attr_2.Checked)
				{
					this.Attr_2.\u2000();
				}
			}

			// Token: 0x060003C9 RID: 969 RVA: 0x00023710 File Offset: 0x00021910
			internal void \u2000(object A_1, EventArgs A_2)
			{
				if (this.Attr_2.Attr_3.Checked)
				{
					this.Attr_2.\u2000();
				}
			}

			// Token: 0x060003CA RID: 970 RVA: 0x00023730 File Offset: 0x00021930
			internal void \u2001(object A_1, EventArgs A_2)
			{
				using (OpenFileDialog openFileDialog = new OpenFileDialog
				{
					Filter = "BIN Files (*.bin)|*.bin|All files (*.*)|*.*"
				})
				{
					if (openFileDialog.ShowDialog() == DialogResult.OK)
					{
						try
						{
							this.Attr_2.\u00A0 = File.ReadAllBytes(openFileDialog.FileName);
							this.Attr_2.\u00A0 = Path.GetFileName(openFileDialog.FileName);
							this.Attr_2.Text = "📁 " + this.Attr_2.\u00A0;
							if (this.Attr_2.Attr_2.Length <= 49152)
							{
								this.Attr_2.Attr_2.Checked = true;
							}
							else if (this.Attr_2.Attr_2.Length >= 65536)
							{
								this.Attr_2.Attr_3.Checked = true;
							}
							this.Attr_2.\u2000();
							MessageBox.Show(string.Format("โหลดไฟล์สำเร็จ: {0}\n(ขนาด: {1} KB)", this.Attr_2.\u00A0, this.Attr_2.Attr_2.Length / 1024), "MZATUNER", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
						}
						catch (Exception ex)
						{
							MessageBox.Show("พบข้อผิดพลาดในการอ่านไฟล์: " + ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Hand);
						}
					}
				}
			}

			// Token: 0x060003CB RID: 971 RVA: 0x0002389C File Offset: 0x00021A9C
			internal void \u2002(object A_1, EventArgs A_2)
			{
				if (this.Attr_2.\u00A0 == null)
				{
					MessageBox.Show("กรุณาเลือกไฟล์ต้นฉบับก่อนครับ!", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					return;
				}
				if (this.Attr_2.Attr_2.Checked)
				{
					if (this.Attr_2.Attr_2.Length < 36630)
					{
						MessageBox.Show("ไฟล์ต้นฉบับมีขนาดเล็กเกินไป ไม่ใช่สมอง 48KB ที่สมบูรณ์!", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Hand);
						return;
					}
					try
					{
						double num = double.Parse(this.Attr_2.Attr_2.Text);
						double num2 = double.Parse(this.Attr_2.Attr_3.Text);
						ushort num3 = (ushort)Math.Round(num / 0.274653);
						ushort num4 = (ushort)Math.Round(num2 / 0.274653);
						this.Attr_2.\u00A0[36626] = (byte)(num3 & 255);
						this.Attr_2.\u00A0[36627] = (byte)(num3 >> 8 & 255);
						this.Attr_2.\u00A0[36628] = (byte)(num4 & 255);
						this.Attr_2.\u00A0[36629] = (byte)(num4 >> 8 & 255);
						MessageBox.Show(string.Format("แพทช์ปลดน้ำมัน (48KB) ที่ตำแหน่ง 0x8F12 เรียบร้อยแล้ว!\nค่าที่ถูกเขียน: แถว 1 = {0:X4}, แถว 2 = {1:X4}\n\n(อย่าลืมกดปุ่มบันทึกไฟล์ออกด้วยนะครับ)", num3, num4), "MZATUNER", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
						return;
					}
					catch
					{
						MessageBox.Show("กรุณากรอกตัวเลขในช่องให้ถูกต้อง!", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Hand);
						return;
					}
				}
				if (this.Attr_2.Attr_3.Checked)
				{
					if (this.Attr_2.Attr_2.Length < 39337)
					{
						MessageBox.Show("ไฟล์ต้นฉบับมีขนาดเล็กเกินไป ไม่ใช่สมอง 64KB ที่สมบูรณ์!", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Hand);
						return;
					}
					try
					{
						double num5 = double.Parse(this.Attr_2.Attr_2.Text);
						double num6 = double.Parse(this.Attr_2.Attr_3.Text);
						ushort num7 = (ushort)Math.Round(num5 / 0.274653);
						ushort num8 = (ushort)Math.Round(num6 / 0.274653);
						this.Attr_2.\u00A0[39333] = (byte)(num7 & 255);
						this.Attr_2.\u00A0[39334] = (byte)(num7 >> 8 & 255);
						this.Attr_2.\u00A0[39335] = (byte)(num8 & 255);
						this.Attr_2.\u00A0[39336] = (byte)(num8 >> 8 & 255);
						MessageBox.Show(string.Format("แพทช์ปลดน้ำมัน (64KB) ที่ตำแหน่ง 0x99A5 (Offset ของ 0x19A5) เรียบร้อยแล้ว!\nค่าที่ถูกเขียน: แถว 1 = {0:X4}, แถว 2 = {1:X4}\n\n(อย่าลืมกดปุ่มบันทึกไฟล์ออกด้วยนะครับ)", num7, num8), "MZATUNER", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
					}
					catch
					{
						MessageBox.Show("กรุณากรอกตัวเลขในช่องให้ถูกต้อง!", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Hand);
					}
				}
			}

			// Token: 0x060003CC RID: 972 RVA: 0x00023B54 File Offset: 0x00021D54
			internal void \u2003(object A_1, EventArgs A_2)
			{
				if (this.Attr_2.\u00A0 == null)
				{
					MessageBox.Show("ไม่มีข้อมูลไฟล์ให้บันทึก กรุณาโหลดไฟล์ก่อนครับ!", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					return;
				}
				using (SaveFileDialog saveFileDialog = new SaveFileDialog
				{
					Filter = "BIN Files (*.bin)|*.bin",
					FileName = "MOD_TPS_" + this.Attr_2.\u00A0
				})
				{
					if (saveFileDialog.ShowDialog() == DialogResult.OK)
					{
						try
						{
							File.WriteAllBytes(saveFileDialog.FileName, this.Attr_2.\u00A0);
							MessageBox.Show("บันทึกไฟล์สำเร็จเรียบร้อย!\n" + saveFileDialog.FileName, "MZATUNER", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
						}
						catch (Exception ex)
						{
							MessageBox.Show("ไม่สามารถบันทึกไฟล์ได้: " + ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Hand);
						}
					}
				}
			}

			// Token: 0x040002FB RID: 763
			public \u205B \u00A0;

			// Token: 0x040002FC RID: 764
			public Panel \u00A0;

			// Token: 0x040002FD RID: 765
			public \u205B.\u00A0 \u00A0;
		}

		// Token: 0x020000DC RID: 220
		[CompilerGenerated]
		private sealed class Form_4
		{
			// Token: 0x060003CE RID: 974 RVA: 0x00023C38 File Offset: 0x00021E38
			internal void \u00A0(object A_1, PaintEventArgs A_2)
			{
				A_2.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
				using (GraphicsPath graphicsPath = \u205B.\u00A0(new Rectangle(0, 0, this.Attr_2.Width - 1, this.Attr_2.Height - 1), 10))
				{
					A_2.Graphics.FillPath(Brushes.White, graphicsPath);
				}
			}

			// Token: 0x040002FE RID: 766
			public Panel \u00A0;
		}
	}
}
