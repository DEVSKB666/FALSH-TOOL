using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using <PrivateImplementationDetails>{68F2EF73-9355-4257-ADA6-397CF8BB8E72};

namespace ns1
{
	// Token: 0x0200009D RID: 157
	public partial class GForm6 : Form
	{
		// Token: 0x06000189 RID: 393
		[DllImport("user32.dll")]
		public static extern bool ReleaseCapture();

		// Token: 0x0600018A RID: 394
		[DllImport("user32.dll")]
		public static extern int SendMessage(IntPtr intptr_0, int int_3, int int_4, int int_5);

		// Token: 0x0600018B RID: 395 RVA: 0x0002006C File Offset: 0x0001E26C
		public GForm6()
		{
			base.Size = new Size(1000, 650);
			base.FormBorderStyle = FormBorderStyle.None;
			this.BackColor = this.color_2;
			base.StartPosition = FormStartPosition.CenterParent;
			this.DoubleBuffered = true;
			base.Opacity = 0.0;
			base.Region = Region.FromHrgn(GForm6.CreateRoundRectRgn(0, 0, base.Width, base.Height, 15, 15));
			this.method_1();
			this.timer_0 = new Timer
			{
				Interval = 15
			};
			this.timer_0.Tick += this.timer_0_Tick;
			this.timer_1 = new Timer
			{
				Interval = 30
			};
			this.timer_1.Tick += this.timer_1_Tick;
			this.timer_1.Start();
		}

		// Token: 0x0600018C RID: 396 RVA: 0x0000CA47 File Offset: 0x0000AC47
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			this.timer_0.Start();
			this.method_0();
		}

		// Token: 0x0600018D RID: 397 RVA: 0x000201D4 File Offset: 0x0001E3D4
		private void method_0()
		{
			GForm6.Struct11 @struct;
			@struct.asyncVoidMethodBuilder_0 = AsyncVoidMethodBuilder.Create();
			@struct.gform6_0 = this;
			@struct.int_0 = -1;
			@struct.asyncVoidMethodBuilder_0.Start<GForm6.Struct11>(ref @struct);
		}

		// Token: 0x0600018E RID: 398
		[DllImport("Gdi32.dll")]
		private static extern IntPtr CreateRoundRectRgn(int int_3, int int_4, int int_5, int int_6, int int_7, int int_8);

		// Token: 0x0600018F RID: 399 RVA: 0x0002020C File Offset: 0x0001E40C
		private void method_1()
		{
			GForm6.Class130 @class = new GForm6.Class130();
			@class.gform6_0 = this;
			this.panel_0 = new Panel
			{
				Dock = DockStyle.Top,
				Height = 45,
				BackColor = Color.Transparent
			};
			this.panel_0.MouseDown += @class.method_0;
			this.panel_0.Paint += @class.method_1;
			this.label_0 = new Label
			{
				Text = "[ MZATUNER ] : คลาวด์แชร์ริ่ง - แบ่งปันไฟล์จูนระดับโปร",
				ForeColor = Color.White,
				Font = new Font("Segoe UI Semibold", 11f),
				AutoSize = true,
				Location = new Point(45, 11),
				BackColor = Color.Transparent
			};
			this.button_0 = new Button
			{
				Text = "✖",
				Size = new Size(45, 45),
				Dock = DockStyle.Right,
				FlatStyle = FlatStyle.Flat,
				ForeColor = Color.FromArgb(150, 150, 150),
				Font = new Font("Segoe UI", 12f, FontStyle.Bold),
				Cursor = Cursors.Hand,
				BackColor = Color.Transparent
			};
			this.button_0.FlatAppearance.BorderSize = 0;
			this.button_0.FlatAppearance.MouseOverBackColor = this.color_0;
			this.button_0.FlatAppearance.MouseDownBackColor = this.color_1;
			this.button_0.MouseEnter += @class.method_2;
			this.button_0.MouseLeave += @class.method_3;
			this.button_0.Click += @class.method_4;
			Panel panel = new Panel
			{
				Size = new Size(30, 30),
				Location = new Point(8, 7),
				BackColor = Color.Transparent
			};
			panel.Paint += @class.method_5;
			this.panel_0.Controls.Add(this.label_0);
			this.panel_0.Controls.Add(this.button_0);
			this.panel_0.Controls.Add(panel);
			base.Controls.Add(this.panel_0);
			this.panel_1 = new Panel
			{
				Location = new Point(20, 65),
				Size = new Size(650, 560),
				BackColor = Color.Transparent
			};
			Panel panel2 = this.method_4("ขั้นตอน 1: เลือกไฟล์ .BIN ในเครื่อง", new Rectangle(0, 0, 315, 100));
			@class.textBox_0 = this.method_5(new Point(12, 35), 245, "จิ้มเพื่อเลือกไฟล์จูนของคุณ...");
			GClass4 gclass = new GClass4();
			gclass.Text = "";
			gclass.method_1(GEnum0.const_2);
			gclass.Size = new Size(40, 30);
			gclass.Location = new Point(262, 35);
			gclass.BackColor = this.color_4;
			gclass.ForeColor = this.color_0;
			GClass4 gclass2 = gclass;
			gclass2.Click += @class.method_6;
			panel2.Controls.Add(@class.textBox_0);
			panel2.Controls.Add(gclass2);
			ComboBox comboBox = this.method_6("ค้นหาตามรหัสกล่อง ECU", new Point(12, 70), 290);
			string[] array = new string[]
			{
				"38770-K03-H01",
				"รออัพเดท..",
				"รออัพเดท..",
				"รออัพเดท.."
			};
			if (comboBox != null)
			{
				@class.comboBox_0 = comboBox;
				@class.comboBox_0.Items.Add("-----------------");
				ComboBox.ObjectCollection items = @class.comboBox_0.Items;
				object[] items2 = array;
				items.AddRange(items2);
				@class.comboBox_0.SelectedIndexChanged += @class.method_7;
			}
			panel2.Controls.Add(comboBox);
			Panel panel3 = this.method_4("ขั้นตอน 2: ปรับตั้งค่าโหมดการจูน", new Rectangle(330, 0, 315, 100));
			ComboBox comboBox2 = this.method_6("โหมดจูนยิง (TPS 0)", new Point(12, 35), 290);
			if (comboBox2 != null)
			{
				ComboBox comboBox3 = comboBox2;
				ComboBox.ObjectCollection items3 = comboBox3.Items;
				object[] items2 = new string[]
				{
					"เปิดฟังก์ชั่น (ON)",
					"ปิดฟังก์ชั่น (OFF)"
				};
				items3.AddRange(items2);
			}
			panel3.Controls.Add(comboBox2);
			ComboBox comboBox4 = this.method_6("ปรับระดับความแรง (Level)", new Point(12, 70), 290);
			if (comboBox4 != null)
			{
				@class.comboBox_1 = comboBox4;
				@class.comboBox_1.Items.Add("--- ดูไฟล์ทุกเลเวล ---");
				ComboBox.ObjectCollection items4 = @class.comboBox_1.Items;
				object[] items2 = new string[]
				{
					"เลเวล 1 (เบาๆ)",
					"เลเวล 2 (ปานกลาง)",
					"เลเวล 3 (โหดๆ)"
				};
				items4.AddRange(items2);
				@class.comboBox_1.SelectedIndexChanged += @class.method_8;
			}
			panel3.Controls.Add(comboBox4);
			@class.panel_0 = this.method_4("ข้อมูลไฟล์แชร์จากเซิร์ฟเวอร์ (Live Data)", new Rectangle(0, 115, 645, 380));
			@class.panel_0.Paint += @class.method_9;
			this.dataGridView_0 = new DataGridView
			{
				Location = new Point(10, 35),
				Size = new Size(625, 335),
				BackgroundColor = this.color_2,
				BorderStyle = BorderStyle.None,
				ColumnHeadersHeight = 35,
				RowTemplate = 
				{
					Height = 40
				},
				EnableHeadersVisualStyles = false,
				AllowUserToAddRows = false,
				ReadOnly = true,
				SelectionMode = DataGridViewSelectionMode.FullRowSelect,
				RowHeadersVisible = false,
				CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal,
				GridColor = this.color_4
			};
			this.dataGridView_0.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(15, 15, 15);
			this.dataGridView_0.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(200, 200, 200);
			this.dataGridView_0.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI Semibold", 9f);
			this.dataGridView_0.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(15, 15, 15);
			this.dataGridView_0.DefaultCellStyle.BackColor = this.color_2;
			this.dataGridView_0.DefaultCellStyle.ForeColor = Color.White;
			this.dataGridView_0.DefaultCellStyle.SelectionBackColor = Color.FromArgb(40, 10, 10);
			this.dataGridView_0.DefaultCellStyle.SelectionForeColor = Color.White;
			this.dataGridView_0.DefaultCellStyle.Font = new Font("Segoe UI", 9f);
			this.dataGridView_0.Columns.Add("No", "ลำดับ");
			this.dataGridView_0.Columns.Add("User", "ผู้จูน / สำนัก");
			this.dataGridView_0.Columns.Add("Ecu", "รหัสกล่อง ECU");
			this.dataGridView_0.Columns.Add("Details", "รายละเอียดการจูน");
			DataGridViewButtonColumn dataGridViewButtonColumn = new DataGridViewButtonColumn();
			dataGridViewButtonColumn.Name = "Action";
			dataGridViewButtonColumn.HeaderText = "ดาวน์โหลด";
			dataGridViewButtonColumn.FlatStyle = FlatStyle.Flat;
			this.dataGridView_0.Columns.Add(dataGridViewButtonColumn);
			this.dataGridView_0.CellMouseMove += @class.method_10;
			this.dataGridView_0.CellMouseLeave += @class.method_11;
			this.dataGridView_0.CellPainting += @class.method_12;
			this.dataGridView_0.RowPostPaint += @class.method_13;
			this.dataGridView_0.CellContentClick += @class.method_14;
			this.dataGridView_0.Columns[0].Width = 40;
			this.dataGridView_0.Columns[1].Width = 120;
			this.dataGridView_0.Columns[2].Width = 120;
			this.dataGridView_0.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
			this.dataGridView_0.Columns[4].Width = 130;
			@class.panel_0.Controls.Add(this.dataGridView_0);
			GClass4 gclass3 = new GClass4();
			gclass3.Text = "↻ อัพเดทรายการไฟล์ (Refresh)";
			gclass3.Size = new Size(645, 45);
			gclass3.Location = new Point(0, 510);
			gclass3.BackColor = this.color_2;
			gclass3.ForeColor = this.color_0;
			gclass3.Font = new Font("Segoe UI", 11f, FontStyle.Bold);
			gclass3.method_5(true);
			GClass4 gclass4 = gclass3;
			gclass4.Click += @class.method_15;
			this.panel_1.Controls.Add(panel2);
			this.panel_1.Controls.Add(panel3);
			this.panel_1.Controls.Add(@class.panel_0);
			this.panel_1.Controls.Add(gclass4);
			this.panel_2 = new Panel
			{
				Location = new Point(685, 65),
				Size = new Size(295, 560),
				BackColor = Color.Transparent
			};
			GClass4 gclass5 = new GClass4();
			gclass5.Text = "ขั้นตอน 3: โหลดไฟล์ลงเครื่อง";
			gclass5.Size = new Size(295, 80);
			gclass5.Location = new Point(0, 0);
			gclass5.BackColor = this.color_2;
			gclass5.ForeColor = Color.FromArgb(255, 40, 40);
			gclass5.Font = new Font("Segoe UI", 12f, FontStyle.Bold);
			gclass5.method_1(GEnum0.const_4);
			gclass5.method_5(true);
			GClass4 gclass6 = gclass5;
			gclass6.Click += @class.method_16;
			this.panel_2.Controls.Add(gclass6);
			Panel panel4 = this.method_4("อัพโหลดแชร์ไฟล์จูน (Upload)", new Rectangle(0, 95, 295, 465));
			@class.textBox_1 = this.method_5(new Point(12, 40), 271, "ชื่อผู้จูน หรือ ชื่อสำนักของคุณ");
			@class.textBox_2 = this.method_5(new Point(12, 75), 271, "ระบุรายละเอียด (เช่น เวล 1 ยิงโหดๆ)");
			@class.textBox_3 = this.method_5(new Point(12, 110), 271, "รหัสกล่อง ECU (เช่น 38770-K03-H01)");
			panel4.Controls.Add(@class.textBox_1);
			panel4.Controls.Add(@class.textBox_2);
			panel4.Controls.Add(@class.textBox_3);
			@class.panel_1 = new Panel
			{
				Size = new Size(271, 150),
				Location = new Point(12, 145),
				BackColor = this.color_2,
				AllowDrop = true
			};
			@class.panel_1.DragEnter += GForm6.Class129.class129_0.method_1;
			@class.panel_1.DragDrop += @class.method_17;
			@class.panel_1.Click += @class.method_18;
			@class.panel_1.Paint += @class.method_19;
			panel4.Controls.Add(@class.panel_1);
			GForm6.Class130 class2 = @class;
			GClass4 gclass7 = new GClass4();
			gclass7.Text = "\ud83d\ude80 ส่งไฟล์ขึ้นคลาวด์ (Share)";
			gclass7.Size = new Size(271, 40);
			gclass7.Location = new Point(12, 305);
			gclass7.BackColor = this.color_2;
			gclass7.ForeColor = this.color_0;
			gclass7.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
			gclass7.method_5(true);
			class2.gclass4_0 = gclass7;
			@class.gclass4_0.Click += @class.method_20;
			panel4.Controls.Add(@class.gclass4_0);
			Panel panel5 = new Panel
			{
				Size = new Size(240, 90),
				Location = new Point(25, 360),
				BackColor = Color.Transparent
			};
			panel5.Paint += @class.method_21;
			panel4.Controls.Add(panel5);
			this.panel_2.Controls.Add(gclass6);
			this.panel_2.Controls.Add(panel4);
			base.Controls.Add(this.panel_1);
			base.Controls.Add(this.panel_2);
		}

		// Token: 0x06000190 RID: 400 RVA: 0x00020EA4 File Offset: 0x0001F0A4
		private Task method_2(int int_3)
		{
			GForm6.Struct10 @struct;
			@struct.asyncTaskMethodBuilder_0 = AsyncTaskMethodBuilder.Create();
			@struct.gform6_0 = this;
			@struct.int_1 = int_3;
			@struct.int_0 = -1;
			@struct.asyncTaskMethodBuilder_0.Start<GForm6.Struct10>(ref @struct);
			return @struct.asyncTaskMethodBuilder_0.Task;
		}

		// Token: 0x06000191 RID: 401 RVA: 0x00020EF0 File Offset: 0x0001F0F0
		private void method_3()
		{
			GForm6.Struct9 @struct;
			@struct.asyncVoidMethodBuilder_0 = AsyncVoidMethodBuilder.Create();
			@struct.gform6_0 = this;
			@struct.int_0 = -1;
			@struct.asyncVoidMethodBuilder_0.Start<GForm6.Struct9>(ref @struct);
		}

		// Token: 0x06000192 RID: 402 RVA: 0x00020F28 File Offset: 0x0001F128
		private Panel method_4(string string_3, Rectangle rectangle_0)
		{
			GForm6.Class131 @class = new GForm6.Class131();
			@class.gform6_0 = this;
			@class.string_0 = string_3;
			@class.panel_0 = new Panel
			{
				Location = rectangle_0.Location,
				Size = rectangle_0.Size,
				BackColor = Color.Transparent
			};
			@class.panel_0.Paint += @class.method_0;
			return @class.panel_0;
		}

		// Token: 0x06000193 RID: 403 RVA: 0x00020F98 File Offset: 0x0001F198
		private TextBox method_5(Point point_0, int int_3, string string_3)
		{
			GForm6.Class132 @class = new GForm6.Class132();
			@class.string_0 = string_3;
			@class.textBox_0 = new TextBox
			{
				Location = point_0,
				Width = int_3,
				BackColor = this.color_2,
				ForeColor = Color.White,
				BorderStyle = BorderStyle.FixedSingle,
				Font = new Font("Segoe UI", 9f)
			};
			@class.textBox_0.Text = @class.string_0;
			@class.textBox_0.ForeColor = Color.DimGray;
			@class.textBox_0.Enter += @class.method_0;
			@class.textBox_0.Leave += @class.method_1;
			return @class.textBox_0;
		}

		// Token: 0x06000194 RID: 404 RVA: 0x00021054 File Offset: 0x0001F254
		private ComboBox method_6(string string_3, Point point_0, int int_3)
		{
			return new ComboBox
			{
				Location = point_0,
				Width = int_3,
				BackColor = this.color_2,
				ForeColor = Color.White,
				DropDownStyle = ComboBoxStyle.DropDownList,
				FlatStyle = FlatStyle.Flat,
				Font = new Font("Segoe UI", 9f),
				Items = 
				{
					string_3
				},
				SelectedIndex = 0
			};
		}

		// Token: 0x06000195 RID: 405 RVA: 0x000210C4 File Offset: 0x0001F2C4
		private GraphicsPath method_7(Rectangle rectangle_0, int int_3)
		{
			GraphicsPath graphicsPath = new GraphicsPath();
			int num = int_3 * 2;
			graphicsPath.AddArc(rectangle_0.X, rectangle_0.Y, num, num, 180f, 90f);
			graphicsPath.AddArc(rectangle_0.Right - num, rectangle_0.Y, num, num, 270f, 90f);
			graphicsPath.AddArc(rectangle_0.Right - num, rectangle_0.Bottom - num, num, num, 0f, 90f);
			graphicsPath.AddArc(rectangle_0.X, rectangle_0.Bottom - num, num, num, 90f, 90f);
			graphicsPath.CloseFigure();
			return graphicsPath;
		}

		// Token: 0x06000196 RID: 406 RVA: 0x0000CA61 File Offset: 0x0000AC61
		[CompilerGenerated]
		private void timer_0_Tick(object sender, EventArgs e)
		{
			if (base.Opacity < 1.0)
			{
				base.Opacity += 0.08;
				return;
			}
			this.timer_0.Stop();
			this.timer_0.Dispose();
		}

		// Token: 0x06000197 RID: 407 RVA: 0x00021168 File Offset: 0x0001F368
		[CompilerGenerated]
		private void timer_1_Tick(object sender, EventArgs e)
		{
			this.int_0 += 3;
			if (this.int_0 > 380)
			{
				this.int_0 = 0;
			}
			if (this.panel_1 != null)
			{
				this.panel_1.Invalidate(new Rectangle(0, 115, 645, 380));
			}
		}

		// Token: 0x040000F9 RID: 249
		private Color color_0 = Color.FromArgb(220, 20, 20);

		// Token: 0x040000FA RID: 250
		private Color color_1 = Color.FromArgb(180, 0, 0);

		// Token: 0x040000FB RID: 251
		private Color color_2 = Color.FromArgb(10, 10, 12);

		// Token: 0x040000FC RID: 252
		private Color color_3 = Color.FromArgb(20, 20, 22);

		// Token: 0x040000FD RID: 253
		private Color color_4 = Color.FromArgb(40, 40, 45);

		// Token: 0x040000FE RID: 254
		private Panel panel_0;

		// Token: 0x040000FF RID: 255
		private Label label_0;

		// Token: 0x04000100 RID: 256
		private Button button_0;

		// Token: 0x04000101 RID: 257
		private DataGridView dataGridView_0;

		// Token: 0x04000102 RID: 258
		private Panel panel_1;

		// Token: 0x04000103 RID: 259
		private Panel panel_2;

		// Token: 0x04000104 RID: 260
		private Timer timer_0;

		// Token: 0x04000105 RID: 261
		private Timer timer_1;

		// Token: 0x04000106 RID: 262
		private int int_0;

		// Token: 0x04000107 RID: 263
		private string string_0 = "";

		// Token: 0x04000108 RID: 264
		private int int_1 = -1;

		// Token: 0x04000109 RID: 265
		private int int_2 = -1;

		// Token: 0x0400010A RID: 266
		private string string_1 = "";

		// Token: 0x0400010B RID: 267
		private string string_2 = "";

		// Token: 0x0200009E RID: 158
		[CompilerGenerated]
		[Serializable]
		private sealed class Class129
		{
			// Token: 0x0600019A RID: 410 RVA: 0x0000CAAD File Offset: 0x0000ACAD
			internal bool method_0(object object_0, X509Certificate x509Certificate_0, X509Chain x509Chain_0, SslPolicyErrors sslPolicyErrors_0)
			{
				return true;
			}

			// Token: 0x0600019B RID: 411 RVA: 0x0000CAB0 File Offset: 0x0000ACB0
			internal void method_1(object sender, DragEventArgs e)
			{
				if (e.Data.GetDataPresent(DataFormats.FileDrop))
				{
					e.Effect = DragDropEffects.Copy;
				}
			}

			// Token: 0x0600019C RID: 412 RVA: 0x0000CAAD File Offset: 0x0000ACAD
			internal bool method_2(object object_0, X509Certificate x509Certificate_0, X509Chain x509Chain_0, SslPolicyErrors sslPolicyErrors_0)
			{
				return true;
			}

			// Token: 0x0600019D RID: 413 RVA: 0x0000CAAD File Offset: 0x0000ACAD
			internal bool method_3(object object_0, X509Certificate x509Certificate_0, X509Chain x509Chain_0, SslPolicyErrors sslPolicyErrors_0)
			{
				return true;
			}

			// Token: 0x0400010C RID: 268
			public static readonly GForm6.Class129 class129_0 = new GForm6.Class129();

			// Token: 0x0400010D RID: 269
			public static RemoteCertificateValidationCallback remoteCertificateValidationCallback_0;

			// Token: 0x0400010E RID: 270
			public static DragEventHandler dragEventHandler_0;

			// Token: 0x0400010F RID: 271
			public static RemoteCertificateValidationCallback remoteCertificateValidationCallback_1;

			// Token: 0x04000110 RID: 272
			public static RemoteCertificateValidationCallback remoteCertificateValidationCallback_2;
		}

		// Token: 0x0200009F RID: 159
		[CompilerGenerated]
		private sealed class Class130
		{
			// Token: 0x0600019F RID: 415 RVA: 0x0000CACB File Offset: 0x0000ACCB
			internal void method_0(object sender, MouseEventArgs e)
			{
				GForm6.ReleaseCapture();
				GForm6.SendMessage(this.gform6_0.Handle, 274, 61458, 0);
			}

			// Token: 0x060001A0 RID: 416 RVA: 0x000211BC File Offset: 0x0001F3BC
			internal void method_1(object sender, PaintEventArgs e)
			{
				using (LinearGradientBrush linearGradientBrush = new LinearGradientBrush(this.gform6_0.panel_0.ClientRectangle, Color.FromArgb(15, 15, 15), Color.FromArgb(5, 5, 5), 90f))
				{
					e.Graphics.FillRectangle(linearGradientBrush, this.gform6_0.panel_0.ClientRectangle);
				}
				using (Pen pen = new Pen(this.gform6_0.color_0, 2f))
				{
					e.Graphics.DrawLine(pen, 0, this.gform6_0.panel_0.Height - 1, this.gform6_0.panel_0.Width, this.gform6_0.panel_0.Height - 1);
				}
				using (Pen pen2 = new Pen(Color.FromArgb(50, this.gform6_0.color_0), 4f))
				{
					e.Graphics.DrawLine(pen2, 0, this.gform6_0.panel_0.Height - 2, this.gform6_0.panel_0.Width, this.gform6_0.panel_0.Height - 2);
				}
			}

			// Token: 0x060001A1 RID: 417 RVA: 0x0000CAEF File Offset: 0x0000ACEF
			internal void method_2(object sender, EventArgs e)
			{
				this.gform6_0.button_0.ForeColor = Color.White;
			}

			// Token: 0x060001A2 RID: 418 RVA: 0x0000CB06 File Offset: 0x0000AD06
			internal void method_3(object sender, EventArgs e)
			{
				this.gform6_0.button_0.ForeColor = Color.FromArgb(150, 150, 150);
			}

			// Token: 0x060001A3 RID: 419 RVA: 0x0000CB2C File Offset: 0x0000AD2C
			internal void method_4(object sender, EventArgs e)
			{
				this.gform6_0.method_3();
			}

			// Token: 0x060001A4 RID: 420 RVA: 0x00021314 File Offset: 0x0001F514
			internal void method_5(object sender, PaintEventArgs e)
			{
				e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
				using (GraphicsPath graphicsPath = new GraphicsPath())
				{
					graphicsPath.AddEllipse(1, 1, 28, 28);
					using (PathGradientBrush pathGradientBrush = new PathGradientBrush(graphicsPath))
					{
						pathGradientBrush.CenterColor = Color.FromArgb(180, this.gform6_0.color_0);
						pathGradientBrush.SurroundColors = new Color[]
						{
							Color.Transparent
						};
						e.Graphics.FillPath(pathGradientBrush, graphicsPath);
					}
				}
				using (SolidBrush solidBrush = new SolidBrush(Color.White))
				{
					e.Graphics.FillEllipse(solidBrush, 4, 4, 22, 22);
				}
				using (Font font = new Font(EC2D41B1-A2F9-4664-90D8-86645EE2E753.smethod_181(), 11f))
				{
					e.Graphics.DrawString(EC2D41B1-A2F9-4664-90D8-86645EE2E753.smethod_765(), font, new SolidBrush(this.gform6_0.color_0), 6f, 2f);
				}
			}

			// Token: 0x060001A5 RID: 421 RVA: 0x00021440 File Offset: 0x0001F640
			internal void method_6(object sender, EventArgs e)
			{
				using (OpenFileDialog openFileDialog = new OpenFileDialog
				{
					Filter = EC2D41B1-A2F9-4664-90D8-86645EE2E753.smethod_886(),
					Title = EC2D41B1-A2F9-4664-90D8-86645EE2E753.smethod_887()
				})
				{
					if (openFileDialog.ShowDialog() == DialogResult.OK)
					{
						this.textBox_0.Text = openFileDialog.FileName;
						this.textBox_0.ForeColor = Color.White;
					}
				}
			}

			// Token: 0x060001A6 RID: 422 RVA: 0x0000CB39 File Offset: 0x0000AD39
			internal void method_7(object sender, EventArgs e)
			{
				this.gform6_0.string_1 = ((this.comboBox_0.SelectedIndex == 0) ? EC2D41B1-A2F9-4664-90D8-86645EE2E753.smethod_3() : this.comboBox_0.SelectedItem.ToString());
				this.gform6_0.method_0();
			}

			// Token: 0x060001A7 RID: 423 RVA: 0x0000CB75 File Offset: 0x0000AD75
			internal void method_8(object sender, EventArgs e)
			{
				this.gform6_0.string_2 = ((this.comboBox_1.SelectedIndex == 0) ? EC2D41B1-A2F9-4664-90D8-86645EE2E753.smethod_3() : this.comboBox_1.SelectedItem.ToString());
				this.gform6_0.method_0();
			}

			// Token: 0x060001A8 RID: 424 RVA: 0x000214B0 File Offset: 0x0001F6B0
			internal void method_9(object sender, PaintEventArgs e)
			{
				if (this.gform6_0.int_0 > 0 && this.gform6_0.int_0 < this.panel_0.Height)
				{
					using (LinearGradientBrush linearGradientBrush = new LinearGradientBrush(new Rectangle(0, this.gform6_0.int_0, this.panel_0.Width, 20), Color.FromArgb(80, this.gform6_0.color_0), Color.Transparent, 90f))
					{
						e.Graphics.FillRectangle(linearGradientBrush, new Rectangle(0, this.gform6_0.int_0, this.panel_0.Width, 20));
					}
					e.Graphics.DrawLine(new Pen(this.gform6_0.color_0, 1f), 0, this.gform6_0.int_0, this.panel_0.Width, this.gform6_0.int_0);
				}
			}

			// Token: 0x060001A9 RID: 425 RVA: 0x000215B4 File Offset: 0x0001F7B4
			internal void method_10(object sender, DataGridViewCellMouseEventArgs e)
			{
				if (e.RowIndex != this.gform6_0.int_1 || e.ColumnIndex != this.gform6_0.int_2)
				{
					this.gform6_0.int_1 = e.RowIndex;
					this.gform6_0.int_2 = e.ColumnIndex;
					this.gform6_0.dataGridView_0.Invalidate();
				}
			}

			// Token: 0x060001AA RID: 426 RVA: 0x0000CBB1 File Offset: 0x0000ADB1
			internal void method_11(object sender, DataGridViewCellEventArgs e)
			{
				this.gform6_0.int_1 = -1;
				this.gform6_0.int_2 = -1;
				this.gform6_0.dataGridView_0.Invalidate();
			}

			// Token: 0x060001AB RID: 427 RVA: 0x0002161C File Offset: 0x0001F81C
			internal void method_12(object sender, DataGridViewCellPaintingEventArgs e)
			{
				if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
				{
					e.PaintBackground(e.CellBounds, true);
					if (this.gform6_0.dataGridView_0.Columns[e.ColumnIndex].Name == EC2D41B1-A2F9-4664-90D8-86645EE2E753.smethod_234())
					{
						object value = this.gform6_0.dataGridView_0.Rows[e.RowIndex].Cells[0].Value;
						if (value != null && (value.ToString() == EC2D41B1-A2F9-4664-90D8-86645EE2E753.smethod_162() || value.ToString() == EC2D41B1-A2F9-4664-90D8-86645EE2E753.smethod_888() || value.ToString() == EC2D41B1-A2F9-4664-90D8-86645EE2E753.smethod_889()))
						{
							e.Handled = true;
							return;
						}
						Graphics graphics = e.Graphics;
						graphics.SmoothingMode = SmoothingMode.AntiAlias;
						Rectangle cellBounds = e.CellBounds;
						cellBounds.Inflate(-6, -6);
						bool flag;
						if (flag = (e.RowIndex == this.gform6_0.int_1 && e.ColumnIndex == this.gform6_0.int_2))
						{
							using (GraphicsPath graphicsPath = this.gform6_0.method_7(cellBounds, 5))
							{
								using (PathGradientBrush pathGradientBrush = new PathGradientBrush(graphicsPath))
								{
									pathGradientBrush.CenterColor = Color.FromArgb(80, this.gform6_0.color_0);
									pathGradientBrush.SurroundColors = new Color[]
									{
										Color.Transparent
									};
									graphics.FillPath(pathGradientBrush, graphicsPath);
								}
							}
						}
						using (GraphicsPath graphicsPath2 = this.gform6_0.method_7(cellBounds, 5))
						{
							Color color = flag ? Color.FromArgb(70, 20, 20) : Color.FromArgb(30, 30, 35);
							Color color2 = flag ? Color.FromArgb(50, 0, 0) : Color.FromArgb(15, 15, 18);
							using (LinearGradientBrush linearGradientBrush = new LinearGradientBrush(cellBounds, color, color2, 90f))
							{
								graphics.FillPath(linearGradientBrush, graphicsPath2);
							}
							RectangleF rect = new RectangleF((float)cellBounds.X, (float)cellBounds.Y, (float)cellBounds.Width, (float)(cellBounds.Height / 2));
							using (LinearGradientBrush linearGradientBrush2 = new LinearGradientBrush(rect, Color.FromArgb(40, Color.White), Color.Transparent, 90f))
							{
								graphics.FillRectangle(linearGradientBrush2, rect);
							}
							using (Pen pen = new Pen(flag ? this.gform6_0.color_0 : Color.FromArgb(100, 100, 110), 1.5f))
							{
								graphics.DrawPath(pen, graphicsPath2);
							}
						}
						using (Pen pen2 = new Pen(Color.FromArgb(100, flag ? this.gform6_0.color_0 : Color.Gray), 1f))
						{
							graphics.DrawLine(pen2, cellBounds.X + 4, cellBounds.Y + 4, cellBounds.X + 4, cellBounds.Bottom - 4);
							graphics.DrawLine(pen2, cellBounds.Right - 4, cellBounds.Y + 4, cellBounds.Right - 4, cellBounds.Bottom - 4);
						}
						if (flag)
						{
							using (Pen pen3 = new Pen(this.gform6_0.color_0, 2f))
							{
								graphics.DrawLine(pen3, cellBounds.X, cellBounds.Y + 6, cellBounds.X, cellBounds.Y);
								graphics.DrawLine(pen3, cellBounds.X, cellBounds.Y, cellBounds.X + 6, cellBounds.Y);
								graphics.DrawLine(pen3, cellBounds.Right - 6, cellBounds.Bottom, cellBounds.Right, cellBounds.Bottom);
								graphics.DrawLine(pen3, cellBounds.Right, cellBounds.Bottom, cellBounds.Right, cellBounds.Bottom - 6);
							}
						}
						string text = EC2D41B1-A2F9-4664-90D8-86645EE2E753.smethod_235();
						Font font = new Font(EC2D41B1-A2F9-4664-90D8-86645EE2E753.smethod_50(), 8.5f);
						SizeF sizeF = graphics.MeasureString(text, font);
						int num = 22 + (int)sizeF.Width;
						int num2 = cellBounds.X + (cellBounds.Width - num) / 2;
						int num3 = cellBounds.Y + cellBounds.Height / 2;
						using (Pen pen4 = new Pen(flag ? Color.White : this.gform6_0.color_0, 2f))
						{
							pen4.LineJoin = LineJoin.Round;
							int num4 = num2;
							int num5 = num3 - 5;
							graphics.DrawLine(pen4, num4, num5 + 8, num4, num5 + 11);
							graphics.DrawLine(pen4, num4, num5 + 11, num4 + 12, num5 + 11);
							graphics.DrawLine(pen4, num4 + 12, num5 + 11, num4 + 12, num5 + 8);
							graphics.DrawLine(pen4, num4 + 6, num5, num4 + 6, num5 + 8);
							graphics.DrawLine(pen4, num4 + 6, num5 + 8, num4 + 2, num5 + 4);
							graphics.DrawLine(pen4, num4 + 6, num5 + 8, num4 + 10, num5 + 4);
						}
						graphics.DrawString(text, font, Brushes.White, (float)(num2 + 20), (float)num3 - sizeF.Height / 2f);
						e.Handled = true;
					}
				}
			}

			// Token: 0x060001AC RID: 428 RVA: 0x00021C48 File Offset: 0x0001FE48
			internal void method_13(object sender, DataGridViewRowPostPaintEventArgs e)
			{
				object value = this.gform6_0.dataGridView_0.Rows[e.RowIndex].Cells[0].Value;
				string a = (value != null) ? value.ToString() : EC2D41B1-A2F9-4664-90D8-86645EE2E753.smethod_3();
				if (a == EC2D41B1-A2F9-4664-90D8-86645EE2E753.smethod_162() || a == EC2D41B1-A2F9-4664-90D8-86645EE2E753.smethod_888() || a == EC2D41B1-A2F9-4664-90D8-86645EE2E753.smethod_889())
				{
					e.Graphics.FillRectangle(new SolidBrush(this.gform6_0.color_2), e.RowBounds);
					string s = EC2D41B1-A2F9-4664-90D8-86645EE2E753.smethod_3();
					if (a == EC2D41B1-A2F9-4664-90D8-86645EE2E753.smethod_162())
					{
						s = EC2D41B1-A2F9-4664-90D8-86645EE2E753.smethod_890();
					}
					else if (a == EC2D41B1-A2F9-4664-90D8-86645EE2E753.smethod_889())
					{
						s = EC2D41B1-A2F9-4664-90D8-86645EE2E753.smethod_891();
					}
					else if (a == EC2D41B1-A2F9-4664-90D8-86645EE2E753.smethod_888())
					{
						object value2 = this.gform6_0.dataGridView_0.Rows[e.RowIndex].Cells[2].Value;
						s = ((value2 != null) ? value2.ToString() : EC2D41B1-A2F9-4664-90D8-86645EE2E753.smethod_892());
					}
					using (Font font = new Font(EC2D41B1-A2F9-4664-90D8-86645EE2E753.smethod_200(), 10.5f, FontStyle.Bold | FontStyle.Italic))
					{
						StringFormat format = new StringFormat
						{
							Alignment = StringAlignment.Center,
							LineAlignment = StringAlignment.Center
						};
						e.Graphics.DrawString(s, font, Brushes.Gray, e.RowBounds, format);
					}
				}
			}

			// Token: 0x060001AD RID: 429 RVA: 0x00021DBC File Offset: 0x0001FFBC
			internal void method_14(object sender, DataGridViewCellEventArgs e)
			{
				if (e.RowIndex >= 0 && this.gform6_0.dataGridView_0.Columns[e.ColumnIndex].Name == EC2D41B1-A2F9-4664-90D8-86645EE2E753.smethod_234())
				{
					this.gform6_0.method_2(e.RowIndex);
				}
			}

			// Token: 0x060001AE RID: 430 RVA: 0x0000CBDB File Offset: 0x0000ADDB
			internal void method_15(object sender, EventArgs e)
			{
				this.gform6_0.method_0();
			}

			// Token: 0x060001AF RID: 431 RVA: 0x00021E10 File Offset: 0x00020010
			internal void method_16(object sender, EventArgs e)
			{
				GForm6.Class130.Struct7 @struct;
				@struct.asyncVoidMethodBuilder_0 = AsyncVoidMethodBuilder.Create();
				@struct.class130_0 = this;
				@struct.int_0 = -1;
				@struct.asyncVoidMethodBuilder_0.Start<GForm6.Class130.Struct7>(ref @struct);
			}

			// Token: 0x060001B0 RID: 432 RVA: 0x00021E48 File Offset: 0x00020048
			internal void method_17(object sender, DragEventArgs e)
			{
				string[] array = (string[])e.Data.GetData(DataFormats.FileDrop);
				if (array.Length != 0 && array[0].EndsWith(EC2D41B1-A2F9-4664-90D8-86645EE2E753.smethod_422(), StringComparison.OrdinalIgnoreCase))
				{
					this.gform6_0.string_0 = array[0];
					this.panel_1.Invalidate();
					return;
				}
				MessageBox.Show(EC2D41B1-A2F9-4664-90D8-86645EE2E753.smethod_893(), EC2D41B1-A2F9-4664-90D8-86645EE2E753.smethod_629(), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}

			// Token: 0x060001B1 RID: 433 RVA: 0x00021EAC File Offset: 0x000200AC
			internal void method_18(object sender, EventArgs e)
			{
				using (OpenFileDialog openFileDialog = new OpenFileDialog
				{
					Filter = EC2D41B1-A2F9-4664-90D8-86645EE2E753.smethod_894()
				})
				{
					if (openFileDialog.ShowDialog() == DialogResult.OK)
					{
						this.gform6_0.string_0 = openFileDialog.FileName;
						this.panel_1.Invalidate();
					}
				}
			}

			// Token: 0x060001B2 RID: 434 RVA: 0x00021F0C File Offset: 0x0002010C
			internal void method_19(object sender, PaintEventArgs e)
			{
				e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
				using (Pen pen = new Pen(Color.FromArgb(80, 80, 80), 2f)
				{
					DashStyle = DashStyle.Dash
				})
				{
					using (GraphicsPath graphicsPath = this.gform6_0.method_7(new Rectangle(2, 2, this.panel_1.Width - 4, this.panel_1.Height - 4), 6))
					{
						e.Graphics.DrawPath(pen, graphicsPath);
					}
				}
				int num = this.panel_1.Width / 2;
				int num2 = this.panel_1.Height / 2;
				using (Font font = new Font(EC2D41B1-A2F9-4664-90D8-86645EE2E753.smethod_200(), 9f))
				{
					StringFormat format = new StringFormat
					{
						Alignment = StringAlignment.Center,
						LineAlignment = StringAlignment.Center
					};
					if (string.IsNullOrEmpty(this.gform6_0.string_0))
					{
						e.Graphics.DrawLine(new Pen(this.gform6_0.color_0, 3f), num, num2 - 20, num, num2);
						e.Graphics.DrawLine(new Pen(this.gform6_0.color_0, 3f), num - 10, num2 - 10, num, num2);
						e.Graphics.DrawLine(new Pen(this.gform6_0.color_0, 3f), num + 10, num2 - 10, num, num2);
						e.Graphics.DrawLine(new Pen(Color.FromArgb(100, 100, 100), 2f), num - 20, num2 + 8, num + 20, num2 + 8);
						e.Graphics.DrawString(EC2D41B1-A2F9-4664-90D8-86645EE2E753.smethod_895(), font, Brushes.Gray, new Rectangle(0, num2 + 20, this.panel_1.Width, 40), format);
					}
					else
					{
						e.Graphics.DrawRectangle(new Pen(Color.White, 2f), num - 15, num2 - 25, 30, 40);
						e.Graphics.FillRectangle(new SolidBrush(this.gform6_0.color_0), num - 12, num2 - 5, 24, 15);
						e.Graphics.DrawString(Path.GetFileName(this.gform6_0.string_0), font, Brushes.White, new Rectangle(0, num2 + 20, this.panel_1.Width, 40), format);
					}
				}
			}

			// Token: 0x060001B3 RID: 435 RVA: 0x000221B4 File Offset: 0x000203B4
			internal void method_20(object sender, EventArgs e)
			{
				GForm6.Class130.Struct8 @struct;
				@struct.asyncVoidMethodBuilder_0 = AsyncVoidMethodBuilder.Create();
				@struct.class130_0 = this;
				@struct.int_0 = -1;
				@struct.asyncVoidMethodBuilder_0.Start<GForm6.Class130.Struct8>(ref @struct);
			}

			// Token: 0x060001B4 RID: 436 RVA: 0x000221EC File Offset: 0x000203EC
			internal void method_21(object sender, PaintEventArgs e)
			{
				e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
				e.Graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
				using (Font font = new Font(EC2D41B1-A2F9-4664-90D8-86645EE2E753.smethod_896(), 28f, FontStyle.Italic))
				{
					e.Graphics.DrawString(EC2D41B1-A2F9-4664-90D8-86645EE2E753.smethod_279(), font, new SolidBrush(this.gform6_0.color_0), 8f, 0f);
				}
				using (Font font2 = new Font(EC2D41B1-A2F9-4664-90D8-86645EE2E753.smethod_897(), 16f))
				{
					e.Graphics.DrawString(EC2D41B1-A2F9-4664-90D8-86645EE2E753.smethod_898(), font2, Brushes.White, 110f, 16f);
				}
				using (Font font3 = new Font(EC2D41B1-A2F9-4664-90D8-86645EE2E753.smethod_200(), 8.5f, FontStyle.Bold))
				{
					e.Graphics.DrawString(EC2D41B1-A2F9-4664-90D8-86645EE2E753.smethod_899(), font3, new SolidBrush(Color.FromArgb(150, 150, 150)), 15f, 55f);
				}
			}

			// Token: 0x04000111 RID: 273
			public GForm6 gform6_0;

			// Token: 0x04000112 RID: 274
			public TextBox textBox_0;

			// Token: 0x04000113 RID: 275
			public ComboBox comboBox_0;

			// Token: 0x04000114 RID: 276
			public ComboBox comboBox_1;

			// Token: 0x04000115 RID: 277
			public Panel panel_0;

			// Token: 0x04000116 RID: 278
			public Panel panel_1;

			// Token: 0x04000117 RID: 279
			public TextBox textBox_1;

			// Token: 0x04000118 RID: 280
			public GClass4 gclass4_0;

			// Token: 0x04000119 RID: 281
			public TextBox textBox_2;

			// Token: 0x0400011A RID: 282
			public TextBox textBox_3;

			// Token: 0x020000A0 RID: 160
			[StructLayout(LayoutKind.Auto)]
			private struct Struct7 : IAsyncStateMachine
			{
				// Token: 0x060001B5 RID: 437 RVA: 0x00022310 File Offset: 0x00020510
				void IAsyncStateMachine.MoveNext()
				{
					int num = this.int_0;
					GForm6.Class130 @class = this.class130_0;
					try
					{
						TaskAwaiter awaiter;
						if (num != 0)
						{
							if (@class.gform6_0.dataGridView_0.SelectedRows.Count == 0 || @class.gform6_0.dataGridView_0.SelectedRows[0].Index < 0)
							{
								MessageBox.Show(EC2D41B1-A2F9-4664-90D8-86645EE2E753.smethod_987(), EC2D41B1-A2F9-4664-90D8-86645EE2E753.smethod_906(), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
								goto IL_EF;
							}
							awaiter = @class.gform6_0.method_2(@class.gform6_0.dataGridView_0.SelectedRows[0].Index).GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								this.int_0 = 0;
								this.taskAwaiter_0 = awaiter;
								this.asyncVoidMethodBuilder_0.AwaitUnsafeOnCompleted<TaskAwaiter, GForm6.Class130.Struct7>(ref awaiter, ref this);
								return;
							}
						}
						else
						{
							awaiter = this.taskAwaiter_0;
							this.taskAwaiter_0 = default(TaskAwaiter);
							this.int_0 = -1;
						}
						awaiter.GetResult();
					}
					catch (Exception exception)
					{
						this.int_0 = -2;
						this.asyncVoidMethodBuilder_0.SetException(exception);
						return;
					}
					IL_EF:
					this.int_0 = -2;
					this.asyncVoidMethodBuilder_0.SetResult();
				}

				// Token: 0x060001B6 RID: 438 RVA: 0x0000CBE8 File Offset: 0x0000ADE8
				[DebuggerHidden]
				void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
				{
					this.asyncVoidMethodBuilder_0.SetStateMachine(stateMachine);
				}

				// Token: 0x0400011B RID: 283
				public int int_0;

				// Token: 0x0400011C RID: 284
				public AsyncVoidMethodBuilder asyncVoidMethodBuilder_0;

				// Token: 0x0400011D RID: 285
				public GForm6.Class130 class130_0;

				// Token: 0x0400011E RID: 286
				private TaskAwaiter taskAwaiter_0;
			}

			// Token: 0x020000A1 RID: 161
			[StructLayout(LayoutKind.Auto)]
			private struct Struct8 : IAsyncStateMachine
			{
				// Token: 0x060001B7 RID: 439 RVA: 0x00022430 File Offset: 0x00020630
				void IAsyncStateMachine.MoveNext()
				{
					int num = this.int_0;
					GForm6.Class130 @class = this.class130_0;
					try
					{
						if (num != 0)
						{
							if (string.IsNullOrEmpty(@class.gform6_0.string_0))
							{
								MessageBox.Show(EC2D41B1-A2F9-4664-90D8-86645EE2E753.smethod_988(), EC2D41B1-A2F9-4664-90D8-86645EE2E753.smethod_906(), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
								goto IL_281;
							}
							if (@class.textBox_1.Text.Contains(EC2D41B1-A2F9-4664-90D8-86645EE2E753.smethod_989()) || string.IsNullOrWhiteSpace(@class.textBox_1.Text))
							{
								MessageBox.Show(EC2D41B1-A2F9-4664-90D8-86645EE2E753.smethod_990(), EC2D41B1-A2F9-4664-90D8-86645EE2E753.smethod_906(), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
								goto IL_281;
							}
							@class.gclass4_0.Text = EC2D41B1-A2F9-4664-90D8-86645EE2E753.smethod_991();
							@class.gclass4_0.Enabled = false;
						}
						try
						{
							if (num != 0)
							{
								ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(GForm6.Class129.class129_0.method_2);
								this.webClient_0 = new WebClient();
							}
							try
							{
								TaskAwaiter<byte[]> awaiter;
								if (num != 0)
								{
									string text = EC2D41B1-A2F9-4664-90D8-86645EE2E753.smethod_992();
									text = string.Concat(new string[]
									{
										text,
										EC2D41B1-A2F9-4664-90D8-86645EE2E753.smethod_993(),
										Uri.EscapeDataString(@class.textBox_1.Text),
										EC2D41B1-A2F9-4664-90D8-86645EE2E753.smethod_994(),
										Uri.EscapeDataString(@class.textBox_2.Text),
										EC2D41B1-A2F9-4664-90D8-86645EE2E753.smethod_995(),
										Uri.EscapeDataString(@class.textBox_3.Text)
									});
									awaiter = this.webClient_0.UploadFileTaskAsync(text, EC2D41B1-A2F9-4664-90D8-86645EE2E753.smethod_996(), @class.gform6_0.string_0).GetAwaiter();
									if (!awaiter.IsCompleted)
									{
										int num2 = 0;
										num = 0;
										this.int_0 = num2;
										this.taskAwaiter_0 = awaiter;
										this.asyncVoidMethodBuilder_0.AwaitUnsafeOnCompleted<TaskAwaiter<byte[]>, GForm6.Class130.Struct8>(ref awaiter, ref this);
										return;
									}
								}
								else
								{
									awaiter = this.taskAwaiter_0;
									this.taskAwaiter_0 = default(TaskAwaiter<byte[]>);
									int num3 = -1;
									num = -1;
									this.int_0 = num3;
								}
								byte[] result = awaiter.GetResult();
								Encoding.UTF8.GetString(result);
								MessageBox.Show(EC2D41B1-A2F9-4664-90D8-86645EE2E753.smethod_997(), EC2D41B1-A2F9-4664-90D8-86645EE2E753.smethod_906(), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
								@class.gform6_0.string_0 = EC2D41B1-A2F9-4664-90D8-86645EE2E753.smethod_3();
								@class.panel_1.Invalidate();
								@class.gform6_0.method_0();
							}
							finally
							{
								if (num < 0 && this.webClient_0 != null)
								{
									((IDisposable)this.webClient_0).Dispose();
								}
							}
							this.webClient_0 = null;
						}
						catch (Exception ex)
						{
							MessageBox.Show(EC2D41B1-A2F9-4664-90D8-86645EE2E753.smethod_998() + ex.Message + EC2D41B1-A2F9-4664-90D8-86645EE2E753.smethod_999(), EC2D41B1-A2F9-4664-90D8-86645EE2E753.smethod_976(), MessageBoxButtons.OK, MessageBoxIcon.Hand);
						}
						finally
						{
							if (num < 0)
							{
								@class.gclass4_0.Text = EC2D41B1-A2F9-4664-90D8-86645EE2E753.smethod_242();
								@class.gclass4_0.Enabled = true;
							}
						}
					}
					catch (Exception exception)
					{
						this.int_0 = -2;
						this.asyncVoidMethodBuilder_0.SetException(exception);
						return;
					}
					IL_281:
					this.int_0 = -2;
					this.asyncVoidMethodBuilder_0.SetResult();
				}

				// Token: 0x060001B8 RID: 440 RVA: 0x0000CBF6 File Offset: 0x0000ADF6
				[DebuggerHidden]
				void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
				{
					this.asyncVoidMethodBuilder_0.SetStateMachine(stateMachine);
				}

				// Token: 0x0400011F RID: 287
				public int int_0;

				// Token: 0x04000120 RID: 288
				public AsyncVoidMethodBuilder asyncVoidMethodBuilder_0;

				// Token: 0x04000121 RID: 289
				public GForm6.Class130 class130_0;

				// Token: 0x04000122 RID: 290
				private WebClient webClient_0;

				// Token: 0x04000123 RID: 291
				private TaskAwaiter<byte[]> taskAwaiter_0;
			}
		}

		// Token: 0x020000A2 RID: 162
		[CompilerGenerated]
		private sealed class Class131
		{
			// Token: 0x060001BA RID: 442 RVA: 0x00022738 File Offset: 0x00020938
			internal void method_0(object sender, PaintEventArgs e)
			{
				e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
				Rectangle rectangle_ = new Rectangle(0, 10, this.panel_0.Width - 1, this.panel_0.Height - 11);
				using (GraphicsPath graphicsPath = this.gform6_0.method_7(rectangle_, 8))
				{
					using (SolidBrush solidBrush = new SolidBrush(this.gform6_0.color_3))
					{
						e.Graphics.FillPath(solidBrush, graphicsPath);
					}
					using (Pen pen = new Pen(this.gform6_0.color_4, 1f))
					{
						e.Graphics.DrawPath(pen, graphicsPath);
					}
				}
				using (Pen pen2 = new Pen(this.gform6_0.color_0, 2f))
				{
					e.Graphics.DrawLine(pen2, rectangle_.X, rectangle_.Y + 20, rectangle_.X, rectangle_.Y + 8);
					e.Graphics.DrawArc(pen2, rectangle_.X, rectangle_.Y, 16, 16, 180, 90);
					e.Graphics.DrawLine(pen2, rectangle_.X + 8, rectangle_.Y, rectangle_.X + 30, rectangle_.Y);
				}
				using (Font font = new Font(EC2D41B1-A2F9-4664-90D8-86645EE2E753.smethod_200(), 8.5f, FontStyle.Bold))
				{
					e.Graphics.DrawString(this.string_0, font, Brushes.White, 35f, 0f);
				}
			}

			// Token: 0x04000124 RID: 292
			public Panel panel_0;

			// Token: 0x04000125 RID: 293
			public GForm6 gform6_0;

			// Token: 0x04000126 RID: 294
			public string string_0;
		}

		// Token: 0x020000A3 RID: 163
		[CompilerGenerated]
		private sealed class Class132
		{
			// Token: 0x060001BC RID: 444 RVA: 0x0000CC04 File Offset: 0x0000AE04
			internal void method_0(object sender, EventArgs e)
			{
				if (this.textBox_0.Text == this.string_0)
				{
					this.textBox_0.Text = EC2D41B1-A2F9-4664-90D8-86645EE2E753.smethod_3();
					this.textBox_0.ForeColor = Color.White;
				}
			}

			// Token: 0x060001BD RID: 445 RVA: 0x0000CC3E File Offset: 0x0000AE3E
			internal void method_1(object sender, EventArgs e)
			{
				if (string.IsNullOrEmpty(this.textBox_0.Text))
				{
					this.textBox_0.Text = this.string_0;
					this.textBox_0.ForeColor = Color.DimGray;
				}
			}

			// Token: 0x04000127 RID: 295
			public TextBox textBox_0;

			// Token: 0x04000128 RID: 296
			public string string_0;
		}
	}
}
