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

namespace Attr_2
{
	// Token: 0x0200001C RID: 28
	public partial class Type_15 : Form
	{
		// Token: 0x0600009A RID: 154 RVA: 0x00007946 File Offset: 0x00005B46
		[CompilerGenerated]
		public string M_85()
		{
			return this.M_85;
		}

		// Token: 0x0600009B RID: 155 RVA: 0x0000794E File Offset: 0x00005B4E
		[CompilerGenerated]
		private void M_85(string A_1)
		{
			this.M_85 = A_1;
		}

		// Token: 0x0600009C RID: 156
		[DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
		private static extern IntPtr M_85(int, int, int, int, int, int);

		// Token: 0x0600009D RID: 157
		[DllImport("user32.dll", EntryPoint = "SendMessage")]
		public static extern int M_85(IntPtr, int, int, int);

		// Token: 0x0600009E RID: 158
		[DllImport("user32.dll", EntryPoint = "ReleaseCapture")]
		public static extern bool \u1680();

		// Token: 0x0600009F RID: 159 RVA: 0x00007958 File Offset: 0x00005B58
		private GraphicsPath M_85(Rectangle A_1, int A_2)
		{
			GraphicsPath graphicsPath = new GraphicsPath();
			graphicsPath.AddArc(A_1.X, A_1.Y, A_2, A_2, 180f, 90f);
			graphicsPath.AddArc(A_1.X + A_1.Width - A_2, A_1.Y, A_2, A_2, 270f, 90f);
			graphicsPath.AddArc(A_1.X + A_1.Width - A_2, A_1.Y + A_1.Height - A_2, A_2, A_2, 0f, 90f);
			graphicsPath.AddArc(A_1.X, A_1.Y + A_1.Height - A_2, A_2, A_2, 90f, 90f);
			graphicsPath.CloseFigure();
			return graphicsPath;
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00007A18 File Offset: 0x00005C18
		private void M_85(int A_1)
		{
			\u2008.\u1680 u = new \u2008.\u1680();
			u.\u00A0 = A_1;
			Task.Run(new Action(u.\u00A0));
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00007A38 File Offset: 0x00005C38
		public Type_15(string A_1)
		{
			\u2008.\u2000 u = new \u2008.\u2000();
			u.\u00A0 = this;
			this.\u1680 = A_1;
			base.Size = new Size(950, 580);
			base.FormBorderStyle = FormBorderStyle.None;
			this.BackColor = Color.FromArgb(12, 12, 12);
			base.StartPosition = FormStartPosition.CenterScreen;
			this.DoubleBuffered = true;
			base.TopMost = true;
			this.M_85 = new Font("Segoe UI Black", 95f, FontStyle.Bold);
			base.Region = Region.FromHrgn(\u2008.\u00A0(0, 0, base.Width, base.Height, 35, 35));
			this.M_85 = new Bitmap(base.Width, base.Height);
			using (Graphics graphics = Graphics.FromImage(this.M_85))
			{
				graphics.SmoothingMode = SmoothingMode.AntiAlias;
				int num = 25;
				float num2 = (float)(Math.Sin(0.5235987755982988) * (double)num);
				float num3 = (float)(Math.Cos(0.5235987755982988) * (double)num);
				float num4 = (float)num + num2;
				float num5 = 2f * num3;
				using (Pen pen = new Pen(Color.FromArgb(4, 255, 255, 255), 1f))
				{
					for (float num6 = (float)(-(float)num); num6 < (float)(this.M_85.Height + num); num6 += num4)
					{
						for (float num7 = -((Math.Abs(num6 / num4) % 2f < 1f) ? 0f : num3) - num3; num7 < (float)this.M_85.Width + num3; num7 += num5)
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
			u.\u00A0 = new Panel
			{
				Dock = DockStyle.Bottom,
				Height = 35,
				BackColor = Color.FromArgb(8, 8, 8)
			};
			this.\u1680 = new Label
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
			this.M_85 = new Label
			{
				ForeColor = Color.WhiteSmoke,
				Font = new Font("Leelawadee UI", 9f),
				AutoSize = false,
				Width = 350,
				TextAlign = ContentAlignment.MiddleRight,
				Dock = DockStyle.Right,
				Padding = new Padding(0, 0, 25, 0)
			};
			u.Attr_2.Controls.AddRange(new Control[]
			{
				this.\u1680,
				this.M_85
			});
			u.Attr_2.Paint += u.\u00A0;
			base.Controls.Add(u.\u00A0);
			u.\u1680 = new Panel
			{
				Dock = DockStyle.Left,
				Width = 400,
				BackColor = Color.FromArgb(9, 9, 9),
				Cursor = Cursors.SizeAll
			};
			u.Attr_3.Paint += u.\u1680;
			Label label = new Label
			{
				Text = "MZA-TUNER",
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
			u.Attr_3.Controls.AddRange(new Control[]
			{
				label,
				label2,
				label3
			});
			this.M_85 = new Panel
			{
				Dock = DockStyle.Fill,
				BackColor = Color.FromArgb(14, 14, 14),
				Cursor = Cursors.SizeAll
			};
			this.M_85.Paint += u.\u2000;
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
			u.\u2000 = new Panel
			{
				Top = 190,
				Left = 60,
				Width = 310,
				Height = 45,
				BackColor = Color.FromArgb(22, 22, 22),
				Cursor = Cursors.Default
			};
			u.Form_4.Region = Region.FromHrgn(\u2008.\u00A0(0, 0, u.Form_4.Width, u.Form_4.Height, 20, 20));
			u.Form_4.Paint += u.\u2001;
			TextBox value = new TextBox
			{
				Text = A_1,
				Top = 12,
				Left = 15,
				Width = 280,
				BorderStyle = BorderStyle.None,
				BackColor = Color.FromArgb(22, 22, 22),
				ForeColor = Color.LightGray,
				Font = new Font("Consolas", 11f, FontStyle.Bold),
				ReadOnly = true
			};
			u.Form_4.Controls.Add(value);
			u.\u00A0 = new Button
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
			u.Attr_2.Region = Region.FromHrgn(\u2008.\u00A0(0, 0, u.Attr_2.Width, u.Attr_2.Height, 20, 20));
			u.Attr_2.FlatAppearance.BorderSize = 0;
			u.Attr_2.MouseEnter += u.\u00A0;
			u.Attr_2.MouseLeave += u.\u1680;
			u.Attr_2.Click += u.\u2000;
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
			u.\u2001 = new Panel
			{
				Top = 280,
				Left = 60,
				Width = 410,
				Height = 45,
				BackColor = Color.FromArgb(22, 22, 22),
				Cursor = Cursors.Default
			};
			u.Attr_5.Region = Region.FromHrgn(\u2008.\u00A0(0, 0, u.Attr_5.Width, u.Attr_5.Height, 20, 20));
			u.Attr_5.Paint += u.\u2002;
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
			u.Attr_5.Controls.Add(value2);
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
			u.\u2002 = new Panel
			{
				Top = 370,
				Left = 60,
				Width = 410,
				Height = 50,
				BackColor = Color.FromArgb(28, 28, 28),
				Cursor = Cursors.IBeam
			};
			u.Type_6.Region = Region.FromHrgn(\u2008.\u00A0(0, 0, u.Type_6.Width, u.Type_6.Height, 20, 20));
			u.Type_6.Paint += u.\u2003;
			u.\u00A0 = new TextBox
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
			u.Type_6.Controls.Add(u.\u00A0);
			u.Type_6.Click += u.\u2001;
			u.Attr_2.Enter += u.\u2002;
			u.Attr_2.Leave += u.\u2003;
			this.M_85 = new Button
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
			this.M_85.Region = Region.FromHrgn(\u2008.\u00A0(0, 0, this.M_85.Width, this.M_85.Height, 25, 25));
			this.M_85.FlatAppearance.BorderSize = 0;
			this.M_85.Paint += this.M_85;
			this.M_85.MouseEnter += this.M_85;
			this.M_85.MouseLeave += this.\u1680;
			this.M_85.Click += u.\u2004;
			Panel panel = new Panel
			{
				Top = 15,
				Left = 500,
				Width = 35,
				Height = 35,
				BackColor = Color.Transparent
			};
			u.\u1680 = new Button
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
			u.Attr_3.Region = Region.FromHrgn(\u2008.\u00A0(0, 0, 35, 35, 35, 35));
			u.Attr_3.FlatAppearance.BorderSize = 0;
			u.Attr_3.FlatAppearance.MouseOverBackColor = Color.FromArgb(231, 76, 60);
			u.Attr_3.FlatAppearance.MouseDownBackColor = Color.FromArgb(192, 57, 43);
			u.Attr_3.MouseEnter += u.\u2005;
			u.Attr_3.MouseLeave += u.\u2006;
			u.Attr_3.Click += this.\u2000;
			panel.Controls.Add(u.\u1680);
			this.M_85.Controls.AddRange(new Control[]
			{
				label4,
				label5,
				label6,
				u.\u2000,
				u.\u00A0,
				label7,
				u.\u2001,
				label8,
				u.\u2002,
				this.M_85,
				panel
			});
			base.Controls.Add(this.M_85);
			base.Controls.Add(u.\u1680);
			for (int i = 0; i < 70; i++)
			{
				this.M_85.Add(new \u2008.\u00A0((float)this.M_85.Next(950), (float)this.M_85.Next(580), (float)(this.M_85.NextDouble() * 1.5 - 0.75), (float)(this.M_85.NextDouble() * 1.5 - 0.75)));
			}
			this.M_85 = new Timer
			{
				Interval = 30
			};
			this.M_85.Tick += u.\u2007;
			this.M_85.Start();
			this.\u1680 = new Timer
			{
				Interval = 1000
			};
			this.Attr_3.Tick += this.\u2001;
			this.Attr_3.Start();
			this.\u2000();
			base.Shown += this.\u2002;
			base.MouseDown += this.M_85;
			u.Attr_3.MouseDown += this.M_85;
			this.M_85.MouseDown += this.M_85;
			label4.MouseDown += this.M_85;
			label5.MouseDown += this.M_85;
			label.MouseDown += this.M_85;
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00008B1C File Offset: 0x00006D1C
		private void M_85(object A_1, MouseEventArgs A_2)
		{
			if (A_2.Button == MouseButtons.Left)
			{
				\u2008.\u1680();
				\u2008.\u00A0(base.Handle, 161, 2, 0);
			}
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00008B44 File Offset: 0x00006D44
		private void \u2000()
		{
			this.M_85.Text = string.Format("วันที่: {0}   |   เวลาระบบ: {1:HH:mm:ss}", DateTime.Now.ToString("dd MMMM yyyy", new CultureInfo("th-TH")), DateTime.Now);
			PowerStatus powerStatus = SystemInformation.PowerStatus;
			this.\u1680 = (int)(powerStatus.BatteryLifePercent * 100f);
			this.\u2002 = (powerStatus.PowerLineStatus == PowerLineStatus.Online);
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00008BB4 File Offset: 0x00006DB4
		private Task \u2001()
		{
			\u2008.\u2002 u;
			u.\u00A0 = AsyncTaskMethodBuilder.Create();
			u.\u00A0 = this;
			u.\u00A0 = -1;
			u.Attr_2.Start<\u2008.\u2002>(ref u);
			return u.Attr_2.Task;
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00008BF8 File Offset: 0x00006DF8
		private void M_85(Graphics A_1, Control A_2)
		{
			Rectangle clientRectangle = A_2.ClientRectangle;
			using (SolidBrush solidBrush = new SolidBrush(Color.FromArgb(170, 231, 76, 60)))
			{
				using (SolidBrush solidBrush2 = new SolidBrush(Color.FromArgb(25, 231, 76, 60)))
				{
					for (int i = 0; i < this.M_85.Count; i++)
					{
						\u2008.\u00A0 u00A = this.M_85[i];
						Point point = A_2.PointToClient(base.PointToScreen(new Point((int)u00A.\u00A0, (int)u00A.\u1680)));
						if (clientRectangle.Contains(point))
						{
							A_1.FillEllipse(solidBrush2, point.X - 5, point.Y - 5, 10, 10);
							A_1.FillEllipse(solidBrush, (float)point.X - 1.5f, (float)point.Y - 1.5f, 3f, 3f);
						}
						for (int j = i + 1; j < this.M_85.Count; j++)
						{
							\u2008.\u00A0 u00A2 = this.M_85[j];
							float num = (u00A.\u00A0 - u00A2.\u00A0) * (u00A.\u00A0 - u00A2.\u00A0) + (u00A.\u1680 - u00A2.\u1680) * (u00A.\u1680 - u00A2.\u1680);
							if (num < 7000f)
							{
								Point point2 = A_2.PointToClient(base.PointToScreen(new Point((int)u00A2.\u00A0, (int)u00A2.\u1680)));
								if (clientRectangle.Contains(point) || clientRectangle.Contains(point2))
								{
									int num2 = (int)(110f * (1f - num / 7000f));
									if (num2 > 0)
									{
										using (Pen pen = new Pen(Color.FromArgb(num2, 255, 80, 60), 1f))
										{
											A_1.DrawLine(pen, point, point2);
										}
									}
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00008E48 File Offset: 0x00007048
		[CompilerGenerated]
		private void M_85(object A_1, PaintEventArgs A_2)
		{
			A_2.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
			Rectangle rectangle = new Rectangle(0, 0, this.M_85.Width, this.M_85.Height);
			using (GraphicsPath graphicsPath = this.M_85(new Rectangle(0, 0, this.M_85.Width - 1, this.M_85.Height - 1), 25))
			{
				Color color = this.\u1680 ? Color.FromArgb(255, 90, 70) : Color.FromArgb(210, 50, 40);
				Color color2 = this.\u1680 ? Color.FromArgb(200, 30, 20) : Color.FromArgb(140, 20, 10);
				if (this.M_85 >= 3)
				{
					color = Color.Purple;
					color2 = Color.DarkMagenta;
				}
				else if (this.M_85.Text == "ตรวจสอบผ่านแล้ว")
				{
					color = Color.MediumSeaGreen;
					color2 = Color.DarkGreen;
				}
				using (LinearGradientBrush linearGradientBrush = new LinearGradientBrush(rectangle, color, color2, 90f))
				{
					A_2.Graphics.FillPath(linearGradientBrush, graphicsPath);
				}
				A_2.Graphics.DrawPath(new Pen(Color.FromArgb(60, 255, 255, 255), 1.5f), graphicsPath);
			}
			TextRenderer.DrawText(A_2.Graphics, this.M_85.Text, this.M_85.Font, rectangle, this.M_85.ForeColor, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00008FE0 File Offset: 0x000071E0
		[CompilerGenerated]
		private void M_85(object A_1, EventArgs A_2)
		{
			this.\u1680 = true;
			if (this.M_85 >= 3)
			{
				this.M_85.Text = "ปฏิเสธการเข้าถึง!";
				this.M_85.Font = new Font("Leelawadee UI", 10f, FontStyle.Bold);
				if (this.M_85.Width > 70)
				{
					this.M_85.Width -= 30;
					this.M_85.Height -= 5;
					this.M_85.Region = Region.FromHrgn(\u2008.\u00A0(0, 0, this.M_85.Width, this.M_85.Height, 25, 25));
				}
				this.M_85.Left = this.M_85.Next(10, this.M_85.Width - this.M_85.Width - 10);
				this.M_85.Top = this.M_85.Next(50, this.M_85.Height - this.M_85.Height - 10);
				this.M_85(1);
			}
			else
			{
				this.M_85.Top -= 2;
				this.M_85(1);
			}
			this.M_85.Invalidate();
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00009124 File Offset: 0x00007324
		[CompilerGenerated]
		private void \u1680(object A_1, EventArgs A_2)
		{
			this.\u1680 = false;
			if (this.M_85 < 3)
			{
				this.M_85.Top += 2;
			}
			this.M_85.Invalidate();
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00009154 File Offset: 0x00007354
		[CompilerGenerated]
		private void \u2000(object A_1, EventArgs A_2)
		{
			this.M_85(3);
			Application.Exit();
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00009162 File Offset: 0x00007362
		[CompilerGenerated]
		private void \u2001(object A_1, EventArgs A_2)
		{
			this.\u2000();
		}

		// Token: 0x060000AB RID: 171 RVA: 0x0000916A File Offset: 0x0000736A
		[CompilerGenerated]
		private void \u2002(object A_1, EventArgs A_2)
		{
			this.M_85(4);
		}

		// Token: 0x04000073 RID: 115
		[CompilerGenerated]
		private string M_85;

		// Token: 0x04000074 RID: 116
		private Timer M_85;

		// Token: 0x04000075 RID: 117
		private Timer \u1680;

		// Token: 0x04000076 RID: 118
		private List<\u2008.\u00A0> \u00A0 = new List<\u2008.\u00A0>();

		// Token: 0x04000077 RID: 119
		private Random M_85 = new Random();

		// Token: 0x04000078 RID: 120
		private int M_85;

		// Token: 0x04000079 RID: 121
		private string \u1680;

		// Token: 0x0400007A RID: 122
		private Button M_85;

		// Token: 0x0400007B RID: 123
		private Panel M_85;

		// Token: 0x0400007C RID: 124
		private Label M_85;

		// Token: 0x0400007D RID: 125
		private Label \u1680;

		// Token: 0x0400007E RID: 126
		private float M_85 = -50f;

		// Token: 0x0400007F RID: 127
		private bool M_85 = true;

		// Token: 0x04000080 RID: 128
		private bool \u1680;

		// Token: 0x04000081 RID: 129
		private bool \u2000;

		// Token: 0x04000082 RID: 130
		private Bitmap M_85;

		// Token: 0x04000083 RID: 131
		private float \u1680 = 10f;

		// Token: 0x04000084 RID: 132
		private bool \u2001 = true;

		// Token: 0x04000085 RID: 133
		private int \u1680 = 100;

		// Token: 0x04000086 RID: 134
		private bool \u2002;

		// Token: 0x04000087 RID: 135
		private Font M_85;

		// Token: 0x04000088 RID: 136
		public const int \u2000 = 161;

		// Token: 0x04000089 RID: 137
		public const int \u2001 = 2;

		// Token: 0x0200001D RID: 29
		private class Attr_2
		{
			// Token: 0x060000AC RID: 172 RVA: 0x00009173 File Offset: 0x00007373
			public \u00A0(float A_1, float A_2, float A_3, float A_4)
			{
				this.M_85 = A_1;
				this.\u1680 = A_2;
				this.\u2000 = A_3;
				this.\u2001 = A_4;
			}

			// Token: 0x0400008A RID: 138
			public float M_85;

			// Token: 0x0400008B RID: 139
			public float \u1680;

			// Token: 0x0400008C RID: 140
			public float \u2000;

			// Token: 0x0400008D RID: 141
			public float \u2001;
		}

		// Token: 0x0200001E RID: 30
		[CompilerGenerated]
		private sealed class Attr_3
		{
			// Token: 0x060000AE RID: 174 RVA: 0x00009198 File Offset: 0x00007398
			internal void M_85()
			{
				try
				{
					switch (this.M_85)
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
			public int M_85;
		}

		// Token: 0x0200001F RID: 31
		[CompilerGenerated]
		private sealed class Form_4
		{
			// Token: 0x060000B0 RID: 176 RVA: 0x00009234 File Offset: 0x00007434
			internal void M_85(object A_1, PaintEventArgs A_2)
			{
				A_2.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
				A_2.Graphics.DrawLine(new Pen(Color.FromArgb(40, 40, 40), 1f), 0, 0, this.M_85.Width, 0);
				int num = 34;
				int num2 = 14;
				Rectangle rect = new Rectangle(25, 10, num, num2);
				A_2.Graphics.DrawRectangle(new Pen(Color.DimGray, 1.2f), rect);
				A_2.Graphics.FillRectangle(new SolidBrush(Color.DimGray), rect.Right, rect.Top + 3, 3, 8);
				Color color = (this.M_85.\u1680 > 20) ? (this.M_85.\u2002 ? Color.MediumSeaGreen : Color.White) : Color.Tomato;
				int width = (int)((float)this.M_85.\u1680 / 100f * (float)(num - 4));
				A_2.Graphics.FillRectangle(new SolidBrush(color), rect.Left + 2, rect.Top + 2, width, num2 - 3);
			}

			// Token: 0x060000B1 RID: 177 RVA: 0x00009344 File Offset: 0x00007544
			internal void \u1680(object A_1, PaintEventArgs A_2)
			{
				A_2.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
				using (LinearGradientBrush linearGradientBrush = new LinearGradientBrush(this.Attr_3.ClientRectangle, Color.FromArgb(4, 4, 4), Color.FromArgb(16, 16, 16), 45f))
				{
					A_2.Graphics.FillRectangle(linearGradientBrush, this.Attr_3.ClientRectangle);
				}
				A_2.Graphics.DrawImage(this.M_85.\u00A0, new Rectangle(0, 0, this.Attr_3.Width, this.Attr_3.Height), new Rectangle(0, 0, this.Attr_3.Width, this.Attr_3.Height), GraphicsUnit.Pixel);
				this.M_85.\u00A0(A_2.Graphics, this.\u1680);
				string text = "M";
				SizeF sizeF = A_2.Graphics.MeasureString(text, this.M_85.\u00A0);
				float num = ((float)this.Attr_3.Width - sizeF.Width) / 2f;
				float num2 = 60f;
				using (SolidBrush solidBrush = new SolidBrush(Color.FromArgb((int)this.M_85.\u1680 / 2, 231, 76, 60)))
				{
					for (int i = -4; i <= 4; i += 2)
					{
						for (int j = -4; j <= 4; j += 2)
						{
							if (i != 0 || j != 0)
							{
								A_2.Graphics.DrawString(text, this.M_85.\u00A0, solidBrush, num + (float)i, num2 + (float)j);
							}
						}
					}
				}
				A_2.Graphics.DrawString(text, this.M_85.\u00A0, new SolidBrush(Color.FromArgb(231, 76, 60)), num, num2);
				using (LinearGradientBrush linearGradientBrush2 = new LinearGradientBrush(new Rectangle(0, Math.Max(0, (int)this.M_85.\u00A0 - 30), this.Attr_3.Width, 60), Color.Transparent, Color.FromArgb(40, 231, 76, 60), 90f))
				{
					A_2.Graphics.FillRectangle(linearGradientBrush2, 0, (int)this.M_85.\u00A0 - 30, this.Attr_3.Width, 60);
				}
				A_2.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(180, 231, 76, 60)), 0f, this.M_85.\u00A0, (float)this.Attr_3.Width, 2f);
				A_2.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(255, 255, 255, 255)), 0f, this.M_85.\u00A0 + 0.5f, (float)this.Attr_3.Width, 1f);
				A_2.Graphics.DrawLine(new Pen(Color.FromArgb(40, 40, 40), 1f), this.Attr_3.Width - 1, 0, this.Attr_3.Width - 1, this.Attr_3.Height);
			}

			// Token: 0x060000B2 RID: 178 RVA: 0x00009684 File Offset: 0x00007884
			internal void \u2000(object A_1, PaintEventArgs A_2)
			{
				A_2.Graphics.DrawImage(this.M_85.\u00A0, new Rectangle(0, 0, this.M_85.Attr_2.Width, this.M_85.Attr_2.Height), new Rectangle(this.Attr_3.Width, 0, this.M_85.Attr_2.Width, this.M_85.Attr_2.Height), GraphicsUnit.Pixel);
				this.M_85.\u00A0(A_2.Graphics, this.M_85.\u00A0);
			}

			// Token: 0x060000B3 RID: 179 RVA: 0x0000971C File Offset: 0x0000791C
			internal void \u2001(object A_1, PaintEventArgs A_2)
			{
				A_2.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
				using (GraphicsPath graphicsPath = this.M_85.\u00A0(new Rectangle(0, 0, this.Form_4.Width - 1, this.Form_4.Height - 1), 20))
				{
					A_2.Graphics.DrawPath(new Pen(Color.FromArgb(70, 70, 70), 1.5f), graphicsPath);
				}
			}

			// Token: 0x060000B4 RID: 180 RVA: 0x000097A4 File Offset: 0x000079A4
			internal void M_85(object A_1, EventArgs A_2)
			{
				this.M_85.BackColor = Color.FromArgb(60, 60, 60);
				this.M_85.ForeColor = Color.White;
			}

			// Token: 0x060000B5 RID: 181 RVA: 0x000097CC File Offset: 0x000079CC
			internal void \u1680(object A_1, EventArgs A_2)
			{
				this.M_85.BackColor = Color.FromArgb(30, 30, 30);
				this.M_85.ForeColor = Color.Tomato;
			}

			// Token: 0x060000B6 RID: 182 RVA: 0x000097F4 File Offset: 0x000079F4
			internal void \u2000(object A_1, EventArgs A_2)
			{
				try
				{
					if (!string.IsNullOrEmpty(this.M_85.\u1680))
					{
						\u2008.\u2001 u = new \u2008.\u2001();
						u.\u00A0 = this;
						Clipboard.SetDataObject(this.M_85.\u1680, true);
						this.M_85.\u00A0(2);
						this.M_85.Text = "คัดลอกสำเร็จ";
						this.M_85.ForeColor = Color.SpringGreen;
						this.M_85.BackColor = Color.FromArgb(45, 45, 45);
						u.\u00A0 = new Timer
						{
							Interval = 1500
						};
						u.Attr_2.Tick += u.\u00A0;
						u.Attr_2.Start();
					}
				}
				catch
				{
					MessageBox.Show("ไม่สามารถคัดลอกได้ในขณะนี้ กรุณาลองใหม่อีกครั้ง", "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				}
			}

			// Token: 0x060000B7 RID: 183 RVA: 0x000098D8 File Offset: 0x00007AD8
			internal void \u2002(object A_1, PaintEventArgs A_2)
			{
				A_2.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
				using (GraphicsPath graphicsPath = this.M_85.\u00A0(new Rectangle(0, 0, this.Attr_5.Width - 1, this.Attr_5.Height - 1), 20))
				{
					A_2.Graphics.DrawPath(new Pen(Color.FromArgb(50, 50, 50), 1.5f), graphicsPath);
				}
			}

			// Token: 0x060000B8 RID: 184 RVA: 0x00009960 File Offset: 0x00007B60
			internal void \u2003(object A_1, PaintEventArgs A_2)
			{
				A_2.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
				using (GraphicsPath graphicsPath = this.M_85.\u00A0(new Rectangle(0, 0, this.Type_6.Width - 1, this.Type_6.Height - 1), 20))
				{
					if (this.M_85.\u2000)
					{
						A_2.Graphics.DrawPath(new Pen(Color.FromArgb(231, 76, 60), 2.5f), graphicsPath);
					}
					else
					{
						using (LinearGradientBrush linearGradientBrush = new LinearGradientBrush(this.Type_6.ClientRectangle, Color.FromArgb(120, 40, 40), Color.FromArgb(30, 30, 30), 45f))
						{
							A_2.Graphics.DrawPath(new Pen(linearGradientBrush, 1.5f), graphicsPath);
						}
					}
				}
			}

			// Token: 0x060000B9 RID: 185 RVA: 0x00009A50 File Offset: 0x00007C50
			internal void \u2001(object A_1, EventArgs A_2)
			{
				this.M_85.Focus();
			}

			// Token: 0x060000BA RID: 186 RVA: 0x00009A60 File Offset: 0x00007C60
			internal void \u2002(object A_1, EventArgs A_2)
			{
				this.M_85.\u2000 = true;
				this.Type_6.BackColor = Color.FromArgb(38, 38, 38);
				this.M_85.BackColor = Color.FromArgb(38, 38, 38);
				this.Type_6.Invalidate();
			}

			// Token: 0x060000BB RID: 187 RVA: 0x00009AB0 File Offset: 0x00007CB0
			internal void \u2003(object A_1, EventArgs A_2)
			{
				this.M_85.\u2000 = false;
				this.Type_6.BackColor = Color.FromArgb(28, 28, 28);
				this.M_85.BackColor = Color.FromArgb(28, 28, 28);
				this.Type_6.Invalidate();
			}

			// Token: 0x060000BC RID: 188 RVA: 0x00009B00 File Offset: 0x00007D00
			internal void \u2004(object A_1, EventArgs A_2)
			{
				\u2008.Form_4.\u00A0 u00A;
				u00A.\u00A0 = AsyncVoidMethodBuilder.Create();
				u00A.\u00A0 = this;
				u00A.\u00A0 = -1;
				u00A.Attr_2.Start<\u2008.Form_4.\u00A0>(ref u00A);
			}

			// Token: 0x060000BD RID: 189 RVA: 0x00009B37 File Offset: 0x00007D37
			internal void \u2005(object A_1, EventArgs A_2)
			{
				this.Attr_3.ForeColor = Color.White;
			}

			// Token: 0x060000BE RID: 190 RVA: 0x00009B49 File Offset: 0x00007D49
			internal void \u2006(object A_1, EventArgs A_2)
			{
				this.Attr_3.ForeColor = Color.Gray;
			}

			// Token: 0x060000BF RID: 191 RVA: 0x00009B5C File Offset: 0x00007D5C
			internal void \u2007(object A_1, EventArgs A_2)
			{
				foreach (\u2008.\u00A0 u00A in this.M_85.\u00A0)
				{
					u00A.\u00A0 += u00A.\u2000;
					u00A.\u1680 += u00A.\u2001;
					if (u00A.\u00A0 < -10f)
					{
						u00A.\u00A0 = 960f;
					}
					else if (u00A.\u00A0 > 960f)
					{
						u00A.\u00A0 = -10f;
					}
					if (u00A.\u1680 < -10f)
					{
						u00A.\u1680 = 590f;
					}
					else if (u00A.\u1680 > 590f)
					{
						u00A.\u1680 = -10f;
					}
				}
				if (this.M_85.\u00A0)
				{
					this.M_85.\u00A0 = this.M_85.\u00A0 + 4f;
					if (this.M_85.\u00A0 > (float)this.Attr_3.Height)
					{
						this.M_85.\u00A0 = false;
					}
				}
				else
				{
					this.M_85.\u00A0 = this.M_85.\u00A0 - 4f;
					if (this.M_85.\u00A0 < 0f)
					{
						this.M_85.\u00A0 = true;
					}
				}
				if (this.M_85.\u2001)
				{
					this.M_85.\u1680 = this.M_85.\u1680 + 3f;
					if (this.M_85.\u1680 > 110f)
					{
						this.M_85.\u2001 = false;
					}
				}
				else
				{
					this.M_85.\u1680 = this.M_85.\u1680 - 3f;
					if (this.M_85.\u1680 < 15f)
					{
						this.M_85.\u2001 = true;
					}
				}
				this.Attr_3.Invalidate();
				this.M_85.Attr_2.Invalidate();
			}

			// Token: 0x0400008F RID: 143
			public Panel M_85;

			// Token: 0x04000090 RID: 144
			public Panel \u1680;

			// Token: 0x04000091 RID: 145
			public Panel \u2000;

			// Token: 0x04000092 RID: 146
			public Button M_85;

			// Token: 0x04000093 RID: 147
			public Panel \u2001;

			// Token: 0x04000094 RID: 148
			public Panel \u2002;

			// Token: 0x04000095 RID: 149
			public TextBox M_85;

			// Token: 0x04000096 RID: 150
			public Button \u1680;

			// Token: 0x04000097 RID: 151
			public \u2008 \u00A0;

			// Token: 0x02000020 RID: 32
			[StructLayout(LayoutKind.Auto)]
			private struct Attr_2 : IAsyncStateMachine
			{
				// Token: 0x060000C0 RID: 192 RVA: 0x00009D68 File Offset: 0x00007F68
				void IAsyncStateMachine.MoveNext()
				{
					int u00A = this.M_85;
					\u2008.\u2000 u00A2 = this.M_85;
					try
					{
						TaskAwaiter u00A3;
						if (u00A != 0)
						{
							if (u00A != 1)
							{
								this.M_85 = u00A2.Attr_2.Text.Trim();
								if (string.Equals(this.M_85, global::Attr_2.Type_14.\u00A0(u00A2.Attr_2.\u1680), StringComparison.OrdinalIgnoreCase))
								{
									u00A2.Attr_2.\u00A0(2);
									u00A2.Attr_2.Attr_2.Text = "ตรวจสอบผ่านแล้ว";
									u00A2.Attr_2.Attr_2.Invalidate();
									u00A3 = Task.Delay(600).GetAwaiter();
									if (!u00A3.IsCompleted)
									{
										this.M_85 = 0;
										this.M_85 = u00A3;
										this.M_85.AwaitUnsafeOnCompleted<TaskAwaiter, \u2008.Form_4.\u00A0>(ref u00A3, ref this);
										return;
									}
									goto IL_DB;
								}
								else
								{
									u00A2.Attr_2.\u00A0 = u00A2.Attr_2.\u00A0 + 1;
									u00A3 = u00A2.Attr_2.\u2001().GetAwaiter();
									if (!u00A3.IsCompleted)
									{
										this.M_85 = 1;
										this.M_85 = u00A3;
										this.M_85.AwaitUnsafeOnCompleted<TaskAwaiter, \u2008.Form_4.\u00A0>(ref u00A3, ref this);
										return;
									}
								}
							}
							else
							{
								u00A3 = this.M_85;
								this.M_85 = default(TaskAwaiter);
								this.M_85 = -1;
							}
							u00A3.GetResult();
							u00A2.Attr_2.Attr_2.Invalidate();
							goto IL_189;
						}
						u00A3 = this.M_85;
						this.M_85 = default(TaskAwaiter);
						this.M_85 = -1;
						IL_DB:
						u00A3.GetResult();
						u00A2.Attr_2.\u00A0(this.M_85);
						u00A2.Attr_2.DialogResult = DialogResult.OK;
						IL_189:;
					}
					catch (Exception exception)
					{
						this.M_85 = -2;
						this.M_85 = null;
						this.M_85.SetException(exception);
						return;
					}
					this.M_85 = -2;
					this.M_85 = null;
					this.M_85.SetResult();
				}

				// Token: 0x060000C1 RID: 193 RVA: 0x00009F54 File Offset: 0x00008154
				[DebuggerHidden]
				void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine A_1)
				{
					this.M_85.SetStateMachine(A_1);
				}

				// Token: 0x04000098 RID: 152
				public int M_85;

				// Token: 0x04000099 RID: 153
				public AsyncVoidMethodBuilder M_85;

				// Token: 0x0400009A RID: 154
				public \u2008.\u2000 \u00A0;

				// Token: 0x0400009B RID: 155
				private string M_85;

				// Token: 0x0400009C RID: 156
				private TaskAwaiter M_85;
			}
		}

		// Token: 0x02000021 RID: 33
		[CompilerGenerated]
		private sealed class Attr_5
		{
			// Token: 0x060000C3 RID: 195 RVA: 0x00009F64 File Offset: 0x00008164
			internal void M_85(object A_1, EventArgs A_2)
			{
				try
				{
					this.M_85.Attr_2.Text = "คัดลอกรหัส";
					this.M_85.Attr_2.ForeColor = Color.Tomato;
					this.M_85.Attr_2.BackColor = Color.FromArgb(30, 30, 30);
				}
				catch
				{
				}
				this.M_85.Stop();
				this.M_85.Dispose();
			}

			// Token: 0x0400009D RID: 157
			public Timer M_85;

			// Token: 0x0400009E RID: 158
			public \u2008.\u2000 \u00A0;
		}
	}
}
