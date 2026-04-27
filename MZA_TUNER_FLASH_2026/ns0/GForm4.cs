using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;
using <PrivateImplementationDetails>{68F2EF73-9355-4257-ADA6-397CF8BB8E72};

namespace ns0
{
	// Token: 0x0200001C RID: 28
	public partial class GForm4 : Form
	{
		// Token: 0x0600009A RID: 154 RVA: 0x0000C65D File Offset: 0x0000A85D
		[CompilerGenerated]
		public string method_0()
		{
			return this.string_0;
		}

		// Token: 0x0600009B RID: 155 RVA: 0x0000C665 File Offset: 0x0000A865
		[CompilerGenerated]
		private void method_1(string string_2)
		{
			this.string_0 = string_2;
		}

		// Token: 0x0600009C RID: 156
		[DllImport("Gdi32.dll")]
		private static extern IntPtr CreateRoundRectRgn(int int_4, int int_5, int int_6, int int_7, int int_8, int int_9);

		// Token: 0x0600009D RID: 157
		[DllImport("user32.dll")]
		public static extern int SendMessage(IntPtr intptr_0, int int_4, int int_5, int int_6);

		// Token: 0x0600009E RID: 158
		[DllImport("user32.dll")]
		public static extern bool ReleaseCapture();

		// Token: 0x0600009F RID: 159 RVA: 0x0001B50C File Offset: 0x0001970C
		private GraphicsPath method_2(Rectangle rectangle_0, int int_4)
		{
			GraphicsPath graphicsPath = new GraphicsPath();
			graphicsPath.AddArc(rectangle_0.X, rectangle_0.Y, int_4, int_4, 180f, 90f);
			graphicsPath.AddArc(rectangle_0.X + rectangle_0.Width - int_4, rectangle_0.Y, int_4, int_4, 270f, 90f);
			graphicsPath.AddArc(rectangle_0.X + rectangle_0.Width - int_4, rectangle_0.Y + rectangle_0.Height - int_4, int_4, int_4, 0f, 90f);
			graphicsPath.AddArc(rectangle_0.X, rectangle_0.Y + rectangle_0.Height - int_4, int_4, int_4, 90f, 90f);
			graphicsPath.CloseFigure();
			return graphicsPath;
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x0000C66E File Offset: 0x0000A86E
		private void method_3(int int_4)
		{
			GForm4.Class13 @class = new GForm4.Class13();
			@class.int_0 = int_4;
			Task.Run(new Action(@class.method_0));
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x0001B5CC File Offset: 0x000197CC
		public GForm4(string string_2)
		{
			GForm4.Class14 @class = new GForm4.Class14();
			@class.gform4_0 = this;
			this.string_1 = string_2;
			base.Size = new Size(950, 580);
			base.FormBorderStyle = FormBorderStyle.None;
			this.BackColor = Color.FromArgb(12, 12, 12);
			base.StartPosition = FormStartPosition.CenterScreen;
			this.DoubleBuffered = true;
			base.TopMost = true;
			this.font_0 = new Font("Segoe UI Black", 95f, FontStyle.Bold);
			base.Region = Region.FromHrgn(GForm4.CreateRoundRectRgn(0, 0, base.Width, base.Height, 35, 35));
			this.bitmap_0 = new Bitmap(base.Width, base.Height);
			using (Graphics graphics = Graphics.FromImage(this.bitmap_0))
			{
				graphics.SmoothingMode = SmoothingMode.AntiAlias;
				int num = 25;
				float num2 = (float)(Math.Sin(0.5235987755982988) * (double)25f);
				float num3 = (float)(Math.Cos(0.5235987755982988) * (double)25f);
				float num4 = 25f + num2;
				float num5 = 2f * num3;
				using (Pen pen = new Pen(Color.FromArgb(4, 255, 255, 255), 1f))
				{
					for (float num6 = (float)(-(float)num); num6 < (float)(this.bitmap_0.Height + num); num6 += num4)
					{
						for (float num7 = -((Math.Abs(num6 / num4) % 2f < 1f) ? 0f : num3) - num3; num7 < (float)this.bitmap_0.Width + num3; num7 += num5)
						{
							PointF[] points = new PointF[]
							{
								new PointF(num7, num6 + num2),
								new PointF(num7 + num3, num6),
								new PointF(num7 + 2f * num3, num6 + num2),
								new PointF(num7 + 2f * num3, num6 + (float)num + num2),
								new PointF(num7 + num3, num6 + (float)(2 * num)),
								new PointF(num7, num6 + (float)num + num2)
							};
							graphics.DrawPolygon(pen, points);
						}
					}
				}
			}
			@class.panel_0 = new Panel
			{
				Dock = DockStyle.Bottom,
				Height = 35,
				BackColor = Color.FromArgb(8, 8, 8)
			};
			this.label_1 = new Label
			{
				ForeColor = Color.LightGray,
				Font = new Font("Leelawadee UI", 9f, FontStyle.Bold),
				AutoSize = false,
				Width = 280,
				TextAlign = ContentAlignment.MiddleLeft,
				Dock = DockStyle.Left,
				Padding = new Padding(70, 0, 0, 0),
				BackColor = Color.Transparent
			};
			this.label_0 = new Label
			{
				ForeColor = Color.WhiteSmoke,
				Font = new Font("Leelawadee UI", 9f),
				AutoSize = false,
				Width = 350,
				TextAlign = ContentAlignment.MiddleRight,
				Dock = DockStyle.Right,
				Padding = new Padding(0, 0, 25, 0)
			};
			@class.panel_0.Controls.AddRange(new Control[]
			{
				this.label_1,
				this.label_0
			});
			@class.panel_0.Paint += @class.method_0;
			base.Controls.Add(@class.panel_0);
			@class.panel_1 = new Panel
			{
				Dock = DockStyle.Left,
				Width = 400,
				BackColor = Color.FromArgb(9, 9, 9),
				Cursor = Cursors.SizeAll
			};
			@class.panel_1.Paint += @class.method_1;
			Label label = new Label
			{
				Text = "LOY-TUNER",
				ForeColor = Color.White,
				Font = new Font("Leelawadee UI", 28f, FontStyle.Bold),
				TextAlign = ContentAlignment.MiddleCenter,
				Width = 400,
				Height = 55,
				Top = 220,
				BackColor = Color.Transparent
			};
			Label label2 = new Label
			{
				Text = "พรีเมียมจูนเนอร์ซิสเต็ม (PREMIUM ECU)",
				ForeColor = Color.IndianRed,
				Font = new Font("Leelawadee UI", 10f, FontStyle.Bold),
				TextAlign = ContentAlignment.MiddleCenter,
				Width = 400,
				Height = 30,
				Top = label.Bottom,
				BackColor = Color.Transparent
			};
			Label label3 = new Label
			{
				Text = "เวอร์ชัน 2.0.8 โปร (ไซเบอร์-ไทย)",
				ForeColor = Color.DimGray,
				Font = new Font("Leelawadee UI", 9f, FontStyle.Italic),
				TextAlign = ContentAlignment.MiddleCenter,
				Width = 400,
				Height = 30,
				Top = label2.Bottom + 90,
				BackColor = Color.Transparent
			};
			@class.panel_1.Controls.AddRange(new Control[]
			{
				label,
				label2,
				label3
			});
			this.panel_0 = new Panel
			{
				Dock = DockStyle.Fill,
				BackColor = Color.FromArgb(14, 14, 14),
				Cursor = Cursors.SizeAll
			};
			this.panel_0.Paint += @class.method_2;
			Label label4 = new Label
			{
				Text = "ลงทะเบียนระบบจูนเนอร์",
				ForeColor = Color.White,
				Font = new Font("Leelawadee UI", 24f, FontStyle.Bold),
				Top = 50,
				Left = 60,
				AutoSize = true,
				BackColor = Color.Transparent
			};
			Label label5 = new Label
			{
				Text = "กรุณายืนยันรหัสฮาร์ดแวร์เพื่อปลดล็อคการใช้งานฉบับเต็ม",
				ForeColor = Color.DarkGray,
				Font = new Font("Leelawadee UI", 10f),
				Top = 100,
				Left = 64,
				AutoSize = true,
				BackColor = Color.Transparent
			};
			Label label6 = new Label
			{
				Text = "รหัสเครื่องฮาร์ดแวร์ (HWID)",
				ForeColor = Color.FromArgb(231, 76, 60),
				Top = 165,
				Left = 60,
				AutoSize = true,
				Font = new Font("Leelawadee UI", 9f, FontStyle.Bold),
				BackColor = Color.Transparent
			};
			@class.panel_2 = new Panel
			{
				Top = 190,
				Left = 60,
				Width = 310,
				Height = 45,
				BackColor = Color.FromArgb(22, 22, 22),
				Cursor = Cursors.Default
			};
			@class.panel_2.Region = Region.FromHrgn(GForm4.CreateRoundRectRgn(0, 0, @class.panel_2.Width, @class.panel_2.Height, 20, 20));
			@class.panel_2.Paint += @class.method_3;
			TextBox value = new TextBox
			{
				Text = string_2,
				Top = 12,
				Left = 15,
				Width = 280,
				BorderStyle = BorderStyle.None,
				BackColor = Color.FromArgb(22, 22, 22),
				ForeColor = Color.LightGray,
				Font = new Font("Consolas", 11f, FontStyle.Bold),
				ReadOnly = true
			};
			@class.panel_2.Controls.Add(value);
			@class.button_0 = new Button
			{
				Text = "คัดลอกรหัส",
				Top = 190,
				Left = 380,
				Width = 90,
				Height = 45,
				BackColor = Color.FromArgb(30, 30, 30),
				FlatStyle = FlatStyle.Flat,
				ForeColor = Color.Tomato,
				Font = new Font("Leelawadee UI", 9f, FontStyle.Bold),
				Cursor = Cursors.Hand
			};
			@class.button_0.Region = Region.FromHrgn(GForm4.CreateRoundRectRgn(0, 0, @class.button_0.Width, @class.button_0.Height, 20, 20));
			@class.button_0.FlatAppearance.BorderSize = 0;
			@class.button_0.MouseEnter += @class.method_4;
			@class.button_0.MouseLeave += @class.method_5;
			@class.button_0.Click += @class.method_6;
			Label label7 = new Label
			{
				Text = "ช่องทางติดต่อจูนเนอร์ผู้ดูแล",
				ForeColor = Color.FromArgb(231, 76, 60),
				Top = 255,
				Left = 60,
				AutoSize = true,
				Font = new Font("Leelawadee UI", 9f, FontStyle.Bold),
				BackColor = Color.Transparent
			};
			@class.panel_3 = new Panel
			{
				Top = 280,
				Left = 60,
				Width = 410,
				Height = 45,
				BackColor = Color.FromArgb(22, 22, 22),
				Cursor = Cursors.Default
			};
			@class.panel_3.Region = Region.FromHrgn(GForm4.CreateRoundRectRgn(0, 0, @class.panel_3.Width, @class.panel_3.Height, 20, 20));
			@class.panel_3.Paint += @class.method_7;
			TextBox value2 = new TextBox
			{
				Text = "Facebook : เส’เอ็ม สามย่าน & Man-Turbo Remap",
				Top = 12,
				Left = 15,
				Width = 380,
				BorderStyle = BorderStyle.None,
				ReadOnly = true,
				BackColor = Color.FromArgb(22, 22, 22),
				ForeColor = Color.DimGray,
				Font = new Font("Leelawadee UI", 10f),
				Cursor = Cursors.No
			};
			@class.panel_3.Controls.Add(value2);
			Label label8 = new Label
			{
				Text = "รหัสปลดล็อค (LICENSE KEY)",
				ForeColor = Color.FromArgb(231, 76, 60),
				Top = 345,
				Left = 60,
				AutoSize = true,
				Font = new Font("Leelawadee UI", 9f, FontStyle.Bold),
				BackColor = Color.Transparent
			};
			@class.panel_4 = new Panel
			{
				Top = 370,
				Left = 60,
				Width = 410,
				Height = 50,
				BackColor = Color.FromArgb(28, 28, 28),
				Cursor = Cursors.IBeam
			};
			@class.panel_4.Region = Region.FromHrgn(GForm4.CreateRoundRectRgn(0, 0, @class.panel_4.Width, @class.panel_4.Height, 20, 20));
			@class.panel_4.Paint += @class.method_8;
			@class.textBox_0 = new TextBox
			{
				Top = 14,
				Left = 15,
				Width = 380,
				BorderStyle = BorderStyle.None,
				BackColor = Color.FromArgb(28, 28, 28),
				ForeColor = Color.White,
				Font = new Font("Consolas", 15f, FontStyle.Bold),
				UseSystemPasswordChar = true,
				TextAlign = HorizontalAlignment.Center,
				Cursor = Cursors.IBeam
			};
			@class.panel_4.Controls.Add(@class.textBox_0);
			@class.panel_4.Click += @class.method_9;
			@class.textBox_0.Enter += @class.method_10;
			@class.textBox_0.Leave += @class.method_11;
			this.button_0 = new Button
			{
				Text = "ยืนยันข้อมูล",
				Top = 450,
				Left = 60,
				Width = 410,
				Height = 55,
				FlatStyle = FlatStyle.Flat,
				ForeColor = Color.White,
				Font = new Font("Leelawadee UI", 12f, FontStyle.Bold),
				Cursor = Cursors.Hand
			};
			this.button_0.Region = Region.FromHrgn(GForm4.CreateRoundRectRgn(0, 0, this.button_0.Width, this.button_0.Height, 25, 25));
			this.button_0.FlatAppearance.BorderSize = 0;
			this.button_0.Paint += this.button_0_Paint;
			this.button_0.MouseEnter += this.button_0_MouseEnter;
			this.button_0.MouseLeave += this.button_0_MouseLeave;
			this.button_0.Click += @class.method_12;
			Panel panel = new Panel
			{
				Top = 15,
				Left = 500,
				Width = 35,
				Height = 35,
				BackColor = Color.Transparent
			};
			@class.button_1 = new Button
			{
				Text = "✕",
				Top = 0,
				Left = 0,
				Width = 35,
				Height = 35,
				FlatStyle = FlatStyle.Flat,
				BackColor = Color.FromArgb(20, 20, 20),
				ForeColor = Color.Gray,
				Font = new Font("Arial", 11f, FontStyle.Bold),
				Cursor = Cursors.Hand
			};
			@class.button_1.Region = Region.FromHrgn(GForm4.CreateRoundRectRgn(0, 0, 35, 35, 35, 35));
			@class.button_1.FlatAppearance.BorderSize = 0;
			@class.button_1.FlatAppearance.MouseOverBackColor = Color.FromArgb(231, 76, 60);
			@class.button_1.FlatAppearance.MouseDownBackColor = Color.FromArgb(192, 57, 43);
			@class.button_1.MouseEnter += @class.method_13;
			@class.button_1.MouseLeave += @class.method_14;
			@class.button_1.Click += this.method_7;
			panel.Controls.Add(@class.button_1);
			this.panel_0.Controls.AddRange(new Control[]
			{
				label4,
				label5,
				label6,
				@class.panel_2,
				@class.button_0,
				label7,
				@class.panel_3,
				label8,
				@class.panel_4,
				this.button_0,
				panel
			});
			base.Controls.Add(this.panel_0);
			base.Controls.Add(@class.panel_1);
			for (int i = 0; i < 70; i++)
			{
				this.list_0.Add(new GForm4.Class12((float)this.random_0.Next(950), (float)this.random_0.Next(580), (float)(this.random_0.NextDouble() * 1.5 - 0.75), (float)(this.random_0.NextDouble() * 1.5 - 0.75)));
			}
			this.timer_0 = new Timer
			{
				Interval = 30
			};
			this.timer_0.Tick += @class.method_15;
			this.timer_0.Start();
			this.timer_1 = new Timer
			{
				Interval = 1000
			};
			this.timer_1.Tick += this.timer_1_Tick;
			this.timer_1.Start();
			this.method_4();
			base.Shown += this.GForm4_Shown;
			base.MouseDown += this.GForm4_MouseDown;
			@class.panel_1.MouseDown += this.GForm4_MouseDown;
			this.panel_0.MouseDown += this.GForm4_MouseDown;
			label4.MouseDown += this.GForm4_MouseDown;
			label5.MouseDown += this.GForm4_MouseDown;
			label.MouseDown += this.GForm4_MouseDown;
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x0000C68D File Offset: 0x0000A88D
		private void GForm4_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				GForm4.ReleaseCapture();
				GForm4.SendMessage(base.Handle, 161, 2, 0);
			}
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x0001C6B4 File Offset: 0x0001A8B4
		private void method_4()
		{
			this.label_0.Text = string.Format("วันที่: {0}   |   เวลาระบบ: {1:HH:mm:ss}", DateTime.Now.ToString("dd MMMM yyyy", new CultureInfo("th-TH")), DateTime.Now);
			PowerStatus powerStatus = SystemInformation.PowerStatus;
			this.int_1 = (int)(powerStatus.BatteryLifePercent * 100f);
			this.bool_4 = (powerStatus.PowerLineStatus == PowerLineStatus.Online);
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x0001C724 File Offset: 0x0001A924
		private Task method_5()
		{
			GForm4.Struct5 @struct;
			@struct.asyncTaskMethodBuilder_0 = AsyncTaskMethodBuilder.Create();
			@struct.gform4_0 = this;
			@struct.int_0 = -1;
			@struct.asyncTaskMethodBuilder_0.Start<GForm4.Struct5>(ref @struct);
			return @struct.asyncTaskMethodBuilder_0.Task;
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x0001C768 File Offset: 0x0001A968
		private void method_6(Graphics graphics_0, Control control_0)
		{
			Rectangle clientRectangle = control_0.ClientRectangle;
			using (SolidBrush solidBrush = new SolidBrush(Color.FromArgb(170, 231, 76, 60)))
			{
				using (SolidBrush solidBrush2 = new SolidBrush(Color.FromArgb(25, 231, 76, 60)))
				{
					for (int i = 0; i < this.list_0.Count; i++)
					{
						GForm4.Class12 @class = this.list_0[i];
						Point point = control_0.PointToClient(base.PointToScreen(new Point((int)@class.float_0, (int)@class.float_1)));
						if (clientRectangle.Contains(point))
						{
							graphics_0.FillEllipse(solidBrush2, point.X - 5, point.Y - 5, 10, 10);
							graphics_0.FillEllipse(solidBrush, (float)point.X - 1.5f, (float)point.Y - 1.5f, 3f, 3f);
						}
						for (int j = i + 1; j < this.list_0.Count; j++)
						{
							GForm4.Class12 class2 = this.list_0[j];
							float num = (@class.float_0 - class2.float_0) * (@class.float_0 - class2.float_0) + (@class.float_1 - class2.float_1) * (@class.float_1 - class2.float_1);
							if (num < 7000f)
							{
								Point point2 = control_0.PointToClient(base.PointToScreen(new Point((int)class2.float_0, (int)class2.float_1)));
								if (clientRectangle.Contains(point) || clientRectangle.Contains(point2))
								{
									int num2 = (int)(110f * (1f - num / 7000f));
									if (num2 > 0)
									{
										using (Pen pen = new Pen(Color.FromArgb(num2, 255, 80, 60), 1f))
										{
											graphics_0.DrawLine(pen, point, point2);
										}
									}
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x0001C9B8 File Offset: 0x0001ABB8
		[CompilerGenerated]
		private void button_0_Paint(object sender, PaintEventArgs e)
		{
			e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
			Rectangle rectangle = new Rectangle(0, 0, this.button_0.Width, this.button_0.Height);
			using (GraphicsPath graphicsPath = this.method_2(new Rectangle(0, 0, this.button_0.Width - 1, this.button_0.Height - 1), 25))
			{
				Color color = this.bool_1 ? Color.FromArgb(255, 90, 70) : Color.FromArgb(210, 50, 40);
				Color color2 = this.bool_1 ? Color.FromArgb(200, 30, 20) : Color.FromArgb(140, 20, 10);
				if (this.int_0 >= 3)
				{
					color = Color.Purple;
					color2 = Color.DarkMagenta;
				}
				else if (this.button_0.Text == "ตรวจสอบผ่านแล้ว")
				{
					color = Color.MediumSeaGreen;
					color2 = Color.DarkGreen;
				}
				using (LinearGradientBrush linearGradientBrush = new LinearGradientBrush(rectangle, color, color2, 90f))
				{
					e.Graphics.FillPath(linearGradientBrush, graphicsPath);
				}
				e.Graphics.DrawPath(new Pen(Color.FromArgb(60, 255, 255, 255), 1.5f), graphicsPath);
			}
			TextRenderer.DrawText(e.Graphics, this.button_0.Text, this.button_0.Font, rectangle, this.button_0.ForeColor, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x0001CB50 File Offset: 0x0001AD50
		[CompilerGenerated]
		private void button_0_MouseEnter(object sender, EventArgs e)
		{
			this.bool_1 = true;
			if (this.int_0 >= 3)
			{
				this.button_0.Text = "ปฏิเสธการเข้าถึง!";
				this.button_0.Font = new Font("Leelawadee UI", 10f, FontStyle.Bold);
				if (this.button_0.Width > 70)
				{
					this.button_0.Width -= 30;
					this.button_0.Height -= 5;
					this.button_0.Region = Region.FromHrgn(GForm4.CreateRoundRectRgn(0, 0, this.button_0.Width, this.button_0.Height, 25, 25));
				}
				this.button_0.Left = this.random_0.Next(10, this.panel_0.Width - this.button_0.Width - 10);
				this.button_0.Top = this.random_0.Next(50, this.panel_0.Height - this.button_0.Height - 10);
				this.method_3(1);
			}
			else
			{
				this.button_0.Top -= 2;
				this.method_3(1);
			}
			this.button_0.Invalidate();
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x0000C6B5 File Offset: 0x0000A8B5
		[CompilerGenerated]
		private void button_0_MouseLeave(object sender, EventArgs e)
		{
			this.bool_1 = false;
			if (this.int_0 < 3)
			{
				this.button_0.Top += 2;
			}
			this.button_0.Invalidate();
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x0000C6E5 File Offset: 0x0000A8E5
		[CompilerGenerated]
		private void method_7(object sender, EventArgs e)
		{
			this.method_3(3);
			Application.Exit();
		}

		// Token: 0x060000AA RID: 170 RVA: 0x0000C6F3 File Offset: 0x0000A8F3
		[CompilerGenerated]
		private void timer_1_Tick(object sender, EventArgs e)
		{
			this.method_4();
		}

		// Token: 0x060000AB RID: 171 RVA: 0x0000C6FB File Offset: 0x0000A8FB
		[CompilerGenerated]
		private void GForm4_Shown(object sender, EventArgs e)
		{
			this.method_3(4);
		}

		// Token: 0x04000073 RID: 115
		[CompilerGenerated]
		private string string_0;

		// Token: 0x04000074 RID: 116
		private Timer timer_0;

		// Token: 0x04000075 RID: 117
		private Timer timer_1;

		// Token: 0x04000076 RID: 118
		private List<GForm4.Class12> list_0 = new List<GForm4.Class12>();

		// Token: 0x04000077 RID: 119
		private Random random_0 = new Random();

		// Token: 0x04000078 RID: 120
		private int int_0;

		// Token: 0x04000079 RID: 121
		private string string_1;

		// Token: 0x0400007A RID: 122
		private Button button_0;

		// Token: 0x0400007B RID: 123
		private Panel panel_0;

		// Token: 0x0400007C RID: 124
		private Label label_0;

		// Token: 0x0400007D RID: 125
		private Label label_1;

		// Token: 0x0400007E RID: 126
		private float float_0 = -50f;

		// Token: 0x0400007F RID: 127
		private bool bool_0 = true;

		// Token: 0x04000080 RID: 128
		private bool bool_1;

		// Token: 0x04000081 RID: 129
		private bool bool_2;

		// Token: 0x04000082 RID: 130
		private Bitmap bitmap_0;

		// Token: 0x04000083 RID: 131
		private float float_1 = 10f;

		// Token: 0x04000084 RID: 132
		private bool bool_3 = true;

		// Token: 0x04000085 RID: 133
		private int int_1 = 100;

		// Token: 0x04000086 RID: 134
		private bool bool_4;

		// Token: 0x04000087 RID: 135
		private Font font_0;

		// Token: 0x04000088 RID: 136
		public const int int_2 = 161;

		// Token: 0x04000089 RID: 137
		public const int int_3 = 2;

		// Token: 0x0200001D RID: 29
		private class Class12
		{
			// Token: 0x060000AC RID: 172 RVA: 0x0000C704 File Offset: 0x0000A904
			public Class12(float float_4, float float_5, float float_6, float float_7)
			{
				this.float_0 = float_4;
				this.float_1 = float_5;
				this.float_2 = float_6;
				this.float_3 = float_7;
			}

			// Token: 0x0400008A RID: 138
			public float float_0;

			// Token: 0x0400008B RID: 139
			public float float_1;

			// Token: 0x0400008C RID: 140
			public float float_2;

			// Token: 0x0400008D RID: 141
			public float float_3;
		}

		// Token: 0x0200001E RID: 30
		[CompilerGenerated]
		private sealed class Class13
		{
			// Token: 0x060000AE RID: 174 RVA: 0x0001CC94 File Offset: 0x0001AE94
			internal void method_0()
			{
				try
				{
					switch (this.int_0)
					{
					case 1:
						Console.Beep(2000, 15);
						break;
					case 2:
						Console.Beep(1300, 70);
						break;
					case 3:
						Console.Beep(400, 100);
						break;
					case 4:
						Console.Beep(900, 50);
						Console.Beep(1100, 50);
						break;
					case 5:
						Console.Beep(200, 500);
						break;
					}
				}
				catch
				{
				}
			}

			// Token: 0x0400008E RID: 142
			public int int_0;
		}

		// Token: 0x0200001F RID: 31
		[CompilerGenerated]
		private sealed class Class14
		{
			// Token: 0x060000B0 RID: 176 RVA: 0x0001CD30 File Offset: 0x0001AF30
			internal void method_0(object sender, PaintEventArgs e)
			{
				e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
				e.Graphics.DrawLine(new Pen(Color.FromArgb(40, 40, 40), 1f), 0, 0, this.panel_0.Width, 0);
				int num = 34;
				int num2 = 14;
				Rectangle rect = new Rectangle(25, 10, 34, 14);
				e.Graphics.DrawRectangle(new Pen(Color.DimGray, 1.2f), rect);
				e.Graphics.FillRectangle(new SolidBrush(Color.DimGray), rect.Right, rect.Top + 3, 3, 8);
				Color color = (this.gform4_0.int_1 > 20) ? (this.gform4_0.bool_4 ? Color.MediumSeaGreen : Color.White) : Color.Tomato;
				int width = (int)((float)this.gform4_0.int_1 / 100f * (float)(num - 4));
				e.Graphics.FillRectangle(new SolidBrush(color), rect.Left + 2, rect.Top + 2, width, num2 - 3);
			}

			// Token: 0x060000B1 RID: 177 RVA: 0x0001CE40 File Offset: 0x0001B040
			internal void method_1(object sender, PaintEventArgs e)
			{
				e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
				using (LinearGradientBrush linearGradientBrush = new LinearGradientBrush(this.panel_1.ClientRectangle, Color.FromArgb(4, 4, 4), Color.FromArgb(16, 16, 16), 45f))
				{
					e.Graphics.FillRectangle(linearGradientBrush, this.panel_1.ClientRectangle);
				}
				e.Graphics.DrawImage(this.gform4_0.bitmap_0, new Rectangle(0, 0, this.panel_1.Width, this.panel_1.Height), new Rectangle(0, 0, this.panel_1.Width, this.panel_1.Height), GraphicsUnit.Pixel);
				this.gform4_0.method_6(e.Graphics, this.panel_1);
				string text = EC2D41B1-A2F9-4664-90D8-86645EE2E753.smethod_765();
				SizeF sizeF = e.Graphics.MeasureString(text, this.gform4_0.font_0);
				float num = ((float)this.panel_1.Width - sizeF.Width) / 2f;
				float num2 = 60f;
				using (SolidBrush solidBrush = new SolidBrush(Color.FromArgb((int)this.gform4_0.float_1 / 2, 231, 76, 60)))
				{
					for (int i = -4; i <= 4; i += 2)
					{
						for (int j = -4; j <= 4; j += 2)
						{
							if (i != 0 || j != 0)
							{
								e.Graphics.DrawString(text, this.gform4_0.font_0, solidBrush, num + (float)i, num2 + (float)j);
							}
						}
					}
				}
				e.Graphics.DrawString(text, this.gform4_0.font_0, new SolidBrush(Color.FromArgb(231, 76, 60)), num, num2);
				using (LinearGradientBrush linearGradientBrush2 = new LinearGradientBrush(new Rectangle(0, Math.Max(0, (int)this.gform4_0.float_0 - 30), this.panel_1.Width, 60), Color.Transparent, Color.FromArgb(40, 231, 76, 60), 90f))
				{
					e.Graphics.FillRectangle(linearGradientBrush2, 0, (int)this.gform4_0.float_0 - 30, this.panel_1.Width, 60);
				}
				e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(180, 231, 76, 60)), 0f, this.gform4_0.float_0, (float)this.panel_1.Width, 2f);
				e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(255, 255, 255, 255)), 0f, this.gform4_0.float_0 + 0.5f, (float)this.panel_1.Width, 1f);
				e.Graphics.DrawLine(new Pen(Color.FromArgb(40, 40, 40), 1f), this.panel_1.Width - 1, 0, this.panel_1.Width - 1, this.panel_1.Height);
			}

			// Token: 0x060000B2 RID: 178 RVA: 0x0001D180 File Offset: 0x0001B380
			internal void method_2(object sender, PaintEventArgs e)
			{
				e.Graphics.DrawImage(this.gform4_0.bitmap_0, new Rectangle(0, 0, this.gform4_0.panel_0.Width, this.gform4_0.panel_0.Height), new Rectangle(this.panel_1.Width, 0, this.gform4_0.panel_0.Width, this.gform4_0.panel_0.Height), GraphicsUnit.Pixel);
				this.gform4_0.method_6(e.Graphics, this.gform4_0.panel_0);
			}

			// Token: 0x060000B3 RID: 179 RVA: 0x0001D218 File Offset: 0x0001B418
			internal void method_3(object sender, PaintEventArgs e)
			{
				e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
				using (GraphicsPath graphicsPath = this.gform4_0.method_2(new Rectangle(0, 0, this.panel_2.Width - 1, this.panel_2.Height - 1), 20))
				{
					e.Graphics.DrawPath(new Pen(Color.FromArgb(70, 70, 70), 1.5f), graphicsPath);
				}
			}

			// Token: 0x060000B4 RID: 180 RVA: 0x0000C729 File Offset: 0x0000A929
			internal void method_4(object sender, EventArgs e)
			{
				this.button_0.BackColor = Color.FromArgb(60, 60, 60);
				this.button_0.ForeColor = Color.White;
			}

			// Token: 0x060000B5 RID: 181 RVA: 0x0000C751 File Offset: 0x0000A951
			internal void method_5(object sender, EventArgs e)
			{
				this.button_0.BackColor = Color.FromArgb(30, 30, 30);
				this.button_0.ForeColor = Color.Tomato;
			}

			// Token: 0x060000B6 RID: 182 RVA: 0x0001D2A0 File Offset: 0x0001B4A0
			internal void method_6(object sender, EventArgs e)
			{
				try
				{
					if (!string.IsNullOrEmpty(this.gform4_0.string_1))
					{
						GForm4.Class15 @class = new GForm4.Class15();
						@class.class14_0 = this;
						Clipboard.SetDataObject(this.gform4_0.string_1, true);
						this.gform4_0.method_3(2);
						this.button_0.Text = EC2D41B1-A2F9-4664-90D8-86645EE2E753.smethod_872();
						this.button_0.ForeColor = Color.SpringGreen;
						this.button_0.BackColor = Color.FromArgb(45, 45, 45);
						@class.timer_0 = new Timer
						{
							Interval = 1500
						};
						@class.timer_0.Tick += @class.method_0;
						@class.timer_0.Start();
					}
				}
				catch
				{
					MessageBox.Show(EC2D41B1-A2F9-4664-90D8-86645EE2E753.smethod_873(), EC2D41B1-A2F9-4664-90D8-86645EE2E753.smethod_691(), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				}
			}

			// Token: 0x060000B7 RID: 183 RVA: 0x0001D384 File Offset: 0x0001B584
			internal void method_7(object sender, PaintEventArgs e)
			{
				e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
				using (GraphicsPath graphicsPath = this.gform4_0.method_2(new Rectangle(0, 0, this.panel_3.Width - 1, this.panel_3.Height - 1), 20))
				{
					e.Graphics.DrawPath(new Pen(Color.FromArgb(50, 50, 50), 1.5f), graphicsPath);
				}
			}

			// Token: 0x060000B8 RID: 184 RVA: 0x0001D40C File Offset: 0x0001B60C
			internal void method_8(object sender, PaintEventArgs e)
			{
				e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
				using (GraphicsPath graphicsPath = this.gform4_0.method_2(new Rectangle(0, 0, this.panel_4.Width - 1, this.panel_4.Height - 1), 20))
				{
					if (!this.gform4_0.bool_2)
					{
						using (LinearGradientBrush linearGradientBrush = new LinearGradientBrush(this.panel_4.ClientRectangle, Color.FromArgb(120, 40, 40), Color.FromArgb(30, 30, 30), 45f))
						{
							e.Graphics.DrawPath(new Pen(linearGradientBrush, 1.5f), graphicsPath);
							return;
						}
					}
					e.Graphics.DrawPath(new Pen(Color.FromArgb(231, 76, 60), 2.5f), graphicsPath);
				}
			}

			// Token: 0x060000B9 RID: 185 RVA: 0x0000C779 File Offset: 0x0000A979
			internal void method_9(object sender, EventArgs e)
			{
				this.textBox_0.Focus();
			}

			// Token: 0x060000BA RID: 186 RVA: 0x0001D4FC File Offset: 0x0001B6FC
			internal void method_10(object sender, EventArgs e)
			{
				this.gform4_0.bool_2 = true;
				this.panel_4.BackColor = Color.FromArgb(38, 38, 38);
				this.textBox_0.BackColor = Color.FromArgb(38, 38, 38);
				this.panel_4.Invalidate();
			}

			// Token: 0x060000BB RID: 187 RVA: 0x0001D54C File Offset: 0x0001B74C
			internal void method_11(object sender, EventArgs e)
			{
				this.gform4_0.bool_2 = false;
				this.panel_4.BackColor = Color.FromArgb(28, 28, 28);
				this.textBox_0.BackColor = Color.FromArgb(28, 28, 28);
				this.panel_4.Invalidate();
			}

			// Token: 0x060000BC RID: 188 RVA: 0x0001D59C File Offset: 0x0001B79C
			internal void method_12(object sender, EventArgs e)
			{
				GForm4.Class14.Struct4 @struct;
				@struct.asyncVoidMethodBuilder_0 = AsyncVoidMethodBuilder.Create();
				@struct.class14_0 = this;
				@struct.int_0 = -1;
				@struct.asyncVoidMethodBuilder_0.Start<GForm4.Class14.Struct4>(ref @struct);
			}

			// Token: 0x060000BD RID: 189 RVA: 0x0000C787 File Offset: 0x0000A987
			internal void method_13(object sender, EventArgs e)
			{
				this.button_1.ForeColor = Color.White;
			}

			// Token: 0x060000BE RID: 190 RVA: 0x0000C799 File Offset: 0x0000A999
			internal void method_14(object sender, EventArgs e)
			{
				this.button_1.ForeColor = Color.Gray;
			}

			// Token: 0x060000BF RID: 191 RVA: 0x0001D5D4 File Offset: 0x0001B7D4
			internal void method_15(object sender, EventArgs e)
			{
				foreach (GForm4.Class12 @class in this.gform4_0.list_0)
				{
					@class.float_0 += @class.float_2;
					@class.float_1 += @class.float_3;
					if (@class.float_0 < -10f)
					{
						@class.float_0 = 960f;
					}
					else if (@class.float_0 > 960f)
					{
						@class.float_0 = -10f;
					}
					if (@class.float_1 < -10f)
					{
						@class.float_1 = 590f;
					}
					else if (@class.float_1 > 590f)
					{
						@class.float_1 = -10f;
					}
				}
				if (this.gform4_0.bool_0)
				{
					this.gform4_0.float_0 = this.gform4_0.float_0 + 4f;
					if (this.gform4_0.float_0 > (float)this.panel_1.Height)
					{
						this.gform4_0.bool_0 = false;
					}
				}
				else
				{
					this.gform4_0.float_0 = this.gform4_0.float_0 - 4f;
					if (this.gform4_0.float_0 < 0f)
					{
						this.gform4_0.bool_0 = true;
					}
				}
				if (this.gform4_0.bool_3)
				{
					this.gform4_0.float_1 = this.gform4_0.float_1 + 3f;
					if (this.gform4_0.float_1 > 110f)
					{
						this.gform4_0.bool_3 = false;
					}
				}
				else
				{
					this.gform4_0.float_1 = this.gform4_0.float_1 - 3f;
					if (this.gform4_0.float_1 < 15f)
					{
						this.gform4_0.bool_3 = true;
					}
				}
				this.panel_1.Invalidate();
				this.gform4_0.panel_0.Invalidate();
			}

			// Token: 0x0400008F RID: 143
			public Panel panel_0;

			// Token: 0x04000090 RID: 144
			public Panel panel_1;

			// Token: 0x04000091 RID: 145
			public Panel panel_2;

			// Token: 0x04000092 RID: 146
			public Button button_0;

			// Token: 0x04000093 RID: 147
			public Panel panel_3;

			// Token: 0x04000094 RID: 148
			public Panel panel_4;

			// Token: 0x04000095 RID: 149
			public TextBox textBox_0;

			// Token: 0x04000096 RID: 150
			public Button button_1;

			// Token: 0x04000097 RID: 151
			public GForm4 gform4_0;

			// Token: 0x02000020 RID: 32
			[StructLayout(LayoutKind.Auto)]
			private struct Struct4 : IAsyncStateMachine
			{
				// Token: 0x060000C0 RID: 192 RVA: 0x0001D7E0 File Offset: 0x0001B9E0
				void IAsyncStateMachine.MoveNext()
				{
					int num = this.int_0;
					GForm4.Class14 @class = this.class14_0;
					try
					{
						TaskAwaiter awaiter;
						if (num != 0)
						{
							if (num != 1)
							{
								this.string_0 = @class.textBox_0.Text.Trim();
								if (string.Equals(this.string_0, GClass2.smethod_5(@class.gform4_0.string_1), StringComparison.OrdinalIgnoreCase))
								{
									@class.gform4_0.method_3(2);
									@class.gform4_0.button_0.Text = EC2D41B1-A2F9-4664-90D8-86645EE2E753.smethod_197();
									@class.gform4_0.button_0.Invalidate();
									awaiter = Task.Delay(600).GetAwaiter();
									if (!awaiter.IsCompleted)
									{
										this.int_0 = 0;
										this.taskAwaiter_0 = awaiter;
										this.asyncVoidMethodBuilder_0.AwaitUnsafeOnCompleted<TaskAwaiter, GForm4.Class14.Struct4>(ref awaiter, ref this);
										return;
									}
									goto IL_165;
								}
								else
								{
									@class.gform4_0.int_0 = @class.gform4_0.int_0 + 1;
									awaiter = @class.gform4_0.method_5().GetAwaiter();
									if (!awaiter.IsCompleted)
									{
										this.int_0 = 1;
										this.taskAwaiter_0 = awaiter;
										this.asyncVoidMethodBuilder_0.AwaitUnsafeOnCompleted<TaskAwaiter, GForm4.Class14.Struct4>(ref awaiter, ref this);
										return;
									}
								}
							}
							else
							{
								awaiter = this.taskAwaiter_0;
								this.taskAwaiter_0 = default(TaskAwaiter);
								this.int_0 = -1;
							}
							awaiter.GetResult();
							@class.gform4_0.button_0.Invalidate();
							goto IL_189;
						}
						awaiter = this.taskAwaiter_0;
						this.taskAwaiter_0 = default(TaskAwaiter);
						this.int_0 = -1;
						IL_165:
						awaiter.GetResult();
						@class.gform4_0.method_1(this.string_0);
						@class.gform4_0.DialogResult = DialogResult.OK;
						IL_189:;
					}
					catch (Exception exception)
					{
						this.int_0 = -2;
						this.string_0 = null;
						this.asyncVoidMethodBuilder_0.SetException(exception);
						return;
					}
					this.int_0 = -2;
					this.string_0 = null;
					this.asyncVoidMethodBuilder_0.SetResult();
				}

				// Token: 0x060000C1 RID: 193 RVA: 0x0000C7AB File Offset: 0x0000A9AB
				[DebuggerHidden]
				void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
				{
					this.asyncVoidMethodBuilder_0.SetStateMachine(stateMachine);
				}

				// Token: 0x04000098 RID: 152
				public int int_0;

				// Token: 0x04000099 RID: 153
				public AsyncVoidMethodBuilder asyncVoidMethodBuilder_0;

				// Token: 0x0400009A RID: 154
				public GForm4.Class14 class14_0;

				// Token: 0x0400009B RID: 155
				private string string_0;

				// Token: 0x0400009C RID: 156
				private TaskAwaiter taskAwaiter_0;
			}
		}

		// Token: 0x02000021 RID: 33
		[CompilerGenerated]
		private sealed class Class15
		{
			// Token: 0x060000C3 RID: 195 RVA: 0x0001D9CC File Offset: 0x0001BBCC
			internal void method_0(object sender, EventArgs e)
			{
				try
				{
					this.class14_0.button_0.Text = EC2D41B1-A2F9-4664-90D8-86645EE2E753.smethod_188();
					this.class14_0.button_0.ForeColor = Color.Tomato;
					this.class14_0.button_0.BackColor = Color.FromArgb(30, 30, 30);
				}
				catch
				{
				}
				this.timer_0.Stop();
				this.timer_0.Dispose();
			}

			// Token: 0x0400009D RID: 157
			public Timer timer_0;

			// Token: 0x0400009E RID: 158
			public GForm4.Class14 class14_0;
		}
	}
}
