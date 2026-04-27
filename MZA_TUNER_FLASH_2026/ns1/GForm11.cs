using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace ns1
{
	// Token: 0x020000B2 RID: 178
	public partial class GForm11 : Form
	{
		// Token: 0x0600021D RID: 541 RVA: 0x00026C58 File Offset: 0x00024E58
		public GForm11()
		{
			base.Size = new Size(620, 460);
			base.FormBorderStyle = FormBorderStyle.None;
			this.BackColor = this.color_0;
			base.StartPosition = FormStartPosition.CenterParent;
			this.DoubleBuffered = true;
			this.AllowDrop = true;
			base.Region = Region.FromHrgn(GForm11.CreateRoundRectRgn(0, 0, base.Width, base.Height, 30, 30));
			this.timer_0 = new Timer
			{
				Interval = 40
			};
			this.timer_0.Tick += this.timer_0_Tick;
			this.timer_0.Start();
			this.method_0();
		}

		// Token: 0x0600021E RID: 542
		[DllImport("Gdi32.dll")]
		private static extern IntPtr CreateRoundRectRgn(int int_0, int int_1, int int_2, int int_3, int int_4, int int_5);

		// Token: 0x0600021F RID: 543 RVA: 0x00026D8C File Offset: 0x00024F8C
		private void method_0()
		{
			GForm11.Class140 @class = new GForm11.Class140();
			@class.gform11_0 = this;
			Button button = new Button
			{
				Text = "✕",
				Size = new Size(40, 40),
				Location = new Point(base.Width - 50, 10),
				FlatStyle = FlatStyle.Flat,
				ForeColor = Color.White,
				Font = new Font("Bahnschrift", 14f, FontStyle.Bold),
				Cursor = Cursors.Hand
			};
			button.FlatAppearance.BorderSize = 0;
			button.FlatAppearance.MouseOverBackColor = Color.FromArgb(180, 255, 30, 30);
			button.Click += @class.method_0;
			base.Controls.Add(button);
			Label value = new Label
			{
				Text = "MZATUNER BINARY SYSTEM // JOINER MODULE",
				Font = new Font("Bahnschrift", 8f),
				ForeColor = Color.FromArgb(120, 120, 130),
				Location = new Point(25, 20),
				AutoSize = true
			};
			base.Controls.Add(value);
			@class.panel_0 = new Panel
			{
				Size = new Size(570, 100),
				Location = new Point(25, 50),
				BackColor = this.color_1
			};
			@class.panel_0.Paint += @class.method_1;
			Label value2 = new Label
			{
				Text = "เลือกไฟล์ครบ 2 ตัว",
				Font = new Font("Bahnschrift SemiBold", 26f),
				ForeColor = this.color_4,
				TextAlign = ContentAlignment.MiddleCenter,
				Dock = DockStyle.Fill,
				BackColor = Color.Transparent
			};
			@class.panel_0.Controls.Add(value2);
			base.Controls.Add(@class.panel_0);
			Panel value3 = this.method_2(new Point(25, 180), out this.textBox_0, "คลิกเลือกไฟล์จูน (32KB)...");
			base.Controls.Add(value3);
			Button button2 = this.method_3("เลือก Bin", new Point(415, 180), this.color_3);
			button2.Click += @class.method_2;
			base.Controls.Add(button2);
			Panel value4 = this.method_2(new Point(25, 250), out this.textBox_1, "คลิกเลือกไฟล์เต็ม (64KB)...");
			base.Controls.Add(value4);
			Button button3 = this.method_3("เลือกไฟล์เต็ม", new Point(415, 250), this.color_2);
			button3.Click += @class.method_3;
			base.Controls.Add(button3);
			Button button4 = new Button
			{
				Text = "สร้างไฟล์ BIN",
				Size = new Size(320, 65),
				Location = new Point(base.Width / 2 - 160, 345),
				FlatStyle = FlatStyle.Flat,
				Font = new Font("Bahnschrift", 20f, FontStyle.Bold),
				ForeColor = this.color_4,
				BackColor = Color.FromArgb(20, 20, 25),
				Cursor = Cursors.Hand
			};
			button4.FlatAppearance.BorderSize = 1;
			button4.FlatAppearance.BorderColor = Color.FromArgb(100, this.color_2);
			button4.FlatAppearance.MouseOverBackColor = Color.FromArgb(40, this.color_2);
			button4.Click += this.method_7;
			base.Controls.Add(button4);
			@class.panel_0.MouseDown += @class.method_4;
			base.MouseDown += @class.method_5;
		}

		// Token: 0x06000220 RID: 544 RVA: 0x0000CFFD File Offset: 0x0000B1FD
		private void method_1(MouseEventArgs mouseEventArgs_0)
		{
			if (mouseEventArgs_0.Button == MouseButtons.Left)
			{
				GForm11.ReleaseCapture();
				GForm11.SendMessage(base.Handle, 161, 2, 0);
			}
		}

		// Token: 0x06000221 RID: 545 RVA: 0x0002715C File Offset: 0x0002535C
		private Panel method_2(Point point_0, out TextBox textBox_2, string string_2)
		{
			GForm11.Class141 @class = new GForm11.Class141();
			@class.gform11_0 = this;
			@class.panel_0 = new Panel
			{
				Size = new Size(380, 52),
				Location = point_0,
				BackColor = Color.FromArgb(15, 15, 20)
			};
			@class.panel_0.Region = Region.FromHrgn(GForm11.CreateRoundRectRgn(0, 0, @class.panel_0.Width, @class.panel_0.Height, 26, 26));
			@class.panel_0.Paint += @class.method_0;
			textBox_2 = new TextBox
			{
				Text = string_2,
				Location = new Point(25, 16),
				Size = new Size(330, 25),
				BorderStyle = BorderStyle.None,
				BackColor = Color.FromArgb(15, 15, 20),
				ForeColor = this.color_5,
				Font = new Font("Bahnschrift", 10f),
				ReadOnly = true
			};
			@class.panel_0.Controls.Add(textBox_2);
			return @class.panel_0;
		}

		// Token: 0x06000222 RID: 546 RVA: 0x0002727C File Offset: 0x0002547C
		private Button method_3(string string_2, Point point_0, Color color_6)
		{
			Button button = new Button
			{
				Text = string_2,
				Location = point_0,
				Size = new Size(180, 52),
				FlatStyle = FlatStyle.Flat,
				BackColor = Color.Transparent,
				ForeColor = color_6,
				Font = new Font("Bahnschrift", 13f, FontStyle.Bold),
				Cursor = Cursors.Hand
			};
			button.FlatAppearance.BorderSize = 1;
			button.FlatAppearance.BorderColor = Color.FromArgb(150, color_6);
			button.FlatAppearance.MouseOverBackColor = Color.FromArgb(50, color_6);
			button.Region = Region.FromHrgn(GForm11.CreateRoundRectRgn(0, 0, button.Width, button.Height, 26, 26));
			return button;
		}

		// Token: 0x06000223 RID: 547 RVA: 0x00027344 File Offset: 0x00025544
		protected override void OnPaint(PaintEventArgs pevent)
		{
			Graphics graphics = pevent.Graphics;
			graphics.SmoothingMode = SmoothingMode.AntiAlias;
			using (Pen pen = new Pen(Color.FromArgb(10, this.color_2), 1f))
			{
				for (int i = 0; i < base.Width; i += 25)
				{
					graphics.DrawLine(pen, i, 0, i, base.Height);
				}
				for (int j = 0; j < base.Height; j += 25)
				{
					graphics.DrawLine(pen, 0, j, base.Width, j);
				}
			}
			using (Pen pen2 = new Pen(Color.FromArgb((int)(this.float_0 * 80f) + 40, this.color_2), 2f))
			{
				graphics.DrawPath(pen2, this.method_5(new Rectangle(1, 1, base.Width - 3, base.Height - 3), 30));
			}
			using (Pen pen3 = new Pen(this.color_2, 4f))
			{
				graphics.DrawLines(pen3, new Point[]
				{
					new Point(40, base.Height - 12),
					new Point(12, base.Height - 12),
					new Point(12, base.Height - 40)
				});
				graphics.DrawLines(pen3, new Point[]
				{
					new Point(base.Width - 40, base.Height - 12),
					new Point(base.Width - 12, base.Height - 12),
					new Point(base.Width - 12, base.Height - 40)
				});
			}
			graphics.DrawString("MZATUNER_OS_LOADED", new Font("Consolas", 7f), new SolidBrush(Color.FromArgb(60, this.color_2)), (float)(base.Width - 120), (float)(base.Height - 25));
		}

		// Token: 0x06000224 RID: 548 RVA: 0x00027568 File Offset: 0x00025768
		private GraphicsPath method_4(Rectangle rectangle_0)
		{
			GraphicsPath graphicsPath = new GraphicsPath();
			int height = rectangle_0.Height;
			graphicsPath.AddArc(rectangle_0.X, rectangle_0.Y, height, height, 90f, 180f);
			graphicsPath.AddArc(rectangle_0.Right - height, rectangle_0.Y, height, height, 270f, 180f);
			graphicsPath.CloseFigure();
			return graphicsPath;
		}

		// Token: 0x06000225 RID: 549 RVA: 0x00026B88 File Offset: 0x00024D88
		private GraphicsPath method_5(Rectangle rectangle_0, int int_0)
		{
			GraphicsPath graphicsPath = new GraphicsPath();
			graphicsPath.AddArc(rectangle_0.X, rectangle_0.Y, int_0, int_0, 180f, 90f);
			graphicsPath.AddArc(rectangle_0.Right - int_0, rectangle_0.Y, int_0, int_0, 270f, 90f);
			graphicsPath.AddArc(rectangle_0.Right - int_0, rectangle_0.Bottom - int_0, int_0, int_0, 0f, 90f);
			graphicsPath.AddArc(rectangle_0.X, rectangle_0.Bottom - int_0, int_0, int_0, 90f, 90f);
			graphicsPath.CloseFigure();
			return graphicsPath;
		}

		// Token: 0x06000226 RID: 550 RVA: 0x000275CC File Offset: 0x000257CC
		private void method_6(int int_0)
		{
			using (OpenFileDialog openFileDialog = new OpenFileDialog
			{
				Filter = "BIN Files|*.bin"
			})
			{
				if (openFileDialog.ShowDialog() == DialogResult.OK)
				{
					if (int_0 == 1)
					{
						this.string_0 = openFileDialog.FileName;
						this.textBox_0.Text = Path.GetFileName(this.string_0);
						this.textBox_0.ForeColor = this.color_4;
					}
					else
					{
						this.string_1 = openFileDialog.FileName;
						this.textBox_1.Text = Path.GetFileName(this.string_1);
						this.textBox_1.ForeColor = this.color_4;
					}
				}
			}
		}

		// Token: 0x06000227 RID: 551 RVA: 0x0002767C File Offset: 0x0002587C
		private void method_7(object sender, EventArgs e)
		{
			if (!string.IsNullOrEmpty(this.string_0) && !string.IsNullOrEmpty(this.string_1))
			{
				try
				{
					byte[] array = File.ReadAllBytes(this.string_0);
					byte[] array2 = File.ReadAllBytes(this.string_1);
					if (array2.Length != 65536)
					{
						MessageBox.Show("ไฟล์ตัวเต็มต้องมีขนาด 64KB!");
					}
					else
					{
						Array.Copy(array, 0, array2, 32768, Math.Min(array.Length, 32768));
						byte b = 0;
						for (int i = 1; i < array2.Length; i++)
						{
							b += array2[i];
						}
						array2[0] = -b;
						using (SaveFileDialog saveFileDialog = new SaveFileDialog
						{
							Filter = "BIN|*.bin",
							FileName = Path.GetFileNameWithoutExtension(this.string_0) + "_MZATUNER_FIXED.bin"
						})
						{
							if (saveFileDialog.ShowDialog() == DialogResult.OK)
							{
								File.WriteAllBytes(saveFileDialog.FileName, array2);
								MessageBox.Show("สร้างไฟล์ BIN สำเร็จ!");
							}
						}
					}
				}
				catch (Exception ex)
				{
					MessageBox.Show("เกิดข้อผิดพลาด: " + ex.Message);
				}
				return;
			}
			MessageBox.Show("กรุณาเลือกไฟล์ให้ครบทั้ง 2 ตัว!");
		}

		// Token: 0x06000228 RID: 552
		[DllImport("user32.dll")]
		public static extern bool ReleaseCapture();

		// Token: 0x06000229 RID: 553
		[DllImport("user32.dll")]
		public static extern IntPtr SendMessage(IntPtr intptr_0, int int_0, int int_1, int int_2);

		// Token: 0x0600022A RID: 554 RVA: 0x000277B8 File Offset: 0x000259B8
		[CompilerGenerated]
		private void timer_0_Tick(object sender, EventArgs e)
		{
			this.float_0 = (float)(Math.Sin((double)DateTime.Now.Ticks / 2500000.0) * 0.5 + 0.5);
			base.Invalidate();
		}

		// Token: 0x0400017D RID: 381
		private readonly Color color_0 = Color.FromArgb(10, 10, 15);

		// Token: 0x0400017E RID: 382
		private readonly Color color_1 = Color.FromArgb(20, 20, 28);

		// Token: 0x0400017F RID: 383
		private readonly Color color_2 = Color.FromArgb(255, 30, 30);

		// Token: 0x04000180 RID: 384
		private readonly Color color_3 = Color.FromArgb(0, 255, 120);

		// Token: 0x04000181 RID: 385
		private readonly Color color_4 = Color.White;

		// Token: 0x04000182 RID: 386
		private readonly Color color_5 = Color.FromArgb(150, 150, 160);

		// Token: 0x04000183 RID: 387
		private string string_0 = "";

		// Token: 0x04000184 RID: 388
		private string string_1 = "";

		// Token: 0x04000185 RID: 389
		private TextBox textBox_0;

		// Token: 0x04000186 RID: 390
		private TextBox textBox_1;

		// Token: 0x04000187 RID: 391
		private float float_0;

		// Token: 0x04000188 RID: 392
		private Timer timer_0;

		// Token: 0x020000B3 RID: 179
		[CompilerGenerated]
		private sealed class Class140
		{
			// Token: 0x0600022C RID: 556 RVA: 0x0000D025 File Offset: 0x0000B225
			internal void method_0(object sender, EventArgs e)
			{
				this.gform11_0.Close();
			}

			// Token: 0x0600022D RID: 557 RVA: 0x00027804 File Offset: 0x00025A04
			internal void method_1(object sender, PaintEventArgs e)
			{
				e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
				using (LinearGradientBrush linearGradientBrush = new LinearGradientBrush(this.panel_0.ClientRectangle, Color.FromArgb(30, this.gform11_0.color_2), Color.Transparent, 90f))
				{
					e.Graphics.FillRectangle(linearGradientBrush, this.panel_0.ClientRectangle);
				}
				e.Graphics.DrawRectangle(new Pen(Color.FromArgb(80, this.gform11_0.color_2), 1f), 0, 0, this.panel_0.Width - 1, this.panel_0.Height - 1);
			}

			// Token: 0x0600022E RID: 558 RVA: 0x0000D032 File Offset: 0x0000B232
			internal void method_2(object sender, EventArgs e)
			{
				this.gform11_0.method_6(1);
			}

			// Token: 0x0600022F RID: 559 RVA: 0x0000D040 File Offset: 0x0000B240
			internal void method_3(object sender, EventArgs e)
			{
				this.gform11_0.method_6(2);
			}

			// Token: 0x06000230 RID: 560 RVA: 0x0000D04E File Offset: 0x0000B24E
			internal void method_4(object sender, MouseEventArgs e)
			{
				this.gform11_0.method_1(e);
			}

			// Token: 0x06000231 RID: 561 RVA: 0x0000D04E File Offset: 0x0000B24E
			internal void method_5(object sender, MouseEventArgs e)
			{
				this.gform11_0.method_1(e);
			}

			// Token: 0x04000189 RID: 393
			public GForm11 gform11_0;

			// Token: 0x0400018A RID: 394
			public Panel panel_0;
		}

		// Token: 0x020000B4 RID: 180
		[CompilerGenerated]
		private sealed class Class141
		{
			// Token: 0x06000233 RID: 563 RVA: 0x000278C0 File Offset: 0x00025AC0
			internal void method_0(object sender, PaintEventArgs e)
			{
				e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
				using (Pen pen = new Pen(Color.FromArgb(50, 50, 60), 1f))
				{
					e.Graphics.DrawPath(pen, this.gform11_0.method_4(new Rectangle(1, 1, this.panel_0.Width - 3, this.panel_0.Height - 3)));
				}
			}

			// Token: 0x0400018B RID: 395
			public GForm11 gform11_0;

			// Token: 0x0400018C RID: 396
			public Panel panel_0;
		}
	}
}
