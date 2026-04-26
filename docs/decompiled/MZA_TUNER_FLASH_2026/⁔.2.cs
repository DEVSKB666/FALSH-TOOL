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

namespace Attr_3
{
	// Token: 0x0200009D RID: 157
	public partial class Type_4A : Form
	{
		// Token: 0x06000189 RID: 393
		[DllImport("user32.dll", EntryPoint = "ReleaseCapture")]
		public static extern bool \u00A0();

		// Token: 0x0600018A RID: 394
		[DllImport("user32.dll", EntryPoint = "SendMessage")]
		public static extern int \u00A0(IntPtr, int, int, int);

		// Token: 0x0600018B RID: 395 RVA: 0x0000C860 File Offset: 0x0000AA60
		public Type_4A()
		{
			base.Size = new Size(1000, 650);
			base.FormBorderStyle = FormBorderStyle.None;
			this.BackColor = this.\u2000;
			base.StartPosition = FormStartPosition.CenterParent;
			this.DoubleBuffered = true;
			base.Opacity = 0.0;
			base.Region = Region.FromHrgn(\u2054.\u00A0(0, 0, base.Width, base.Height, 15, 15));
			this.\u2000();
			this.\u00A0 = new Timer
			{
				Interval = 15
			};
			this.Attr_2.Tick += this.\u00A0;
			this.\u1680 = new Timer
			{
				Interval = 30
			};
			this.Attr_3.Tick += this.\u1680;
			this.Attr_3.Start();
		}

		// Token: 0x0600018C RID: 396 RVA: 0x0000C9C7 File Offset: 0x0000ABC7
		protected override void OnLoad(EventArgs A_1)
		{
			base.OnLoad(A_1);
			this.Attr_2.Start();
			this.\u1680();
		}

		// Token: 0x0600018D RID: 397 RVA: 0x0000C9E4 File Offset: 0x0000ABE4
		private void \u1680()
		{
			\u2054.\u2004 u;
			u.\u00A0 = AsyncVoidMethodBuilder.Create();
			u.\u00A0 = this;
			u.\u00A0 = -1;
			u.Attr_2.Start<\u2054.\u2004>(ref u);
		}

		// Token: 0x0600018E RID: 398
		[DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
		private static extern IntPtr \u00A0(int, int, int, int, int, int);

		// Token: 0x0600018F RID: 399 RVA: 0x0000CA1C File Offset: 0x0000AC1C
		private void \u2000()
		{
			\u2054.\u1680 u = new \u2054.\u1680();
			u.\u00A0 = this;
			this.\u00A0 = new Panel
			{
				Dock = DockStyle.Top,
				Height = 45,
				BackColor = Color.Transparent
			};
			this.Attr_2.MouseDown += u.\u00A0;
			this.Attr_2.Paint += u.\u00A0;
			this.\u00A0 = new Label
			{
				Text = "[ MZATUNER ] : คลาวด์แชร์ริ่ง - แบ่งปันไฟล์จูนระดับโปร",
				ForeColor = Color.White,
				Font = new Font("Segoe UI Semibold", 11f),
				AutoSize = true,
				Location = new Point(45, 11),
				BackColor = Color.Transparent
			};
			this.\u00A0 = new Button
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
			this.Attr_2.FlatAppearance.BorderSize = 0;
			this.Attr_2.FlatAppearance.MouseOverBackColor = this.\u00A0;
			this.Attr_2.FlatAppearance.MouseDownBackColor = this.\u1680;
			this.Attr_2.MouseEnter += u.\u00A0;
			this.Attr_2.MouseLeave += u.\u1680;
			this.Attr_2.Click += u.\u2000;
			Panel panel = new Panel
			{
				Size = new Size(30, 30),
				Location = new Point(8, 7),
				BackColor = Color.Transparent
			};
			panel.Paint += u.\u1680;
			this.Attr_2.Controls.Add(this.\u00A0);
			this.Attr_2.Controls.Add(this.\u00A0);
			this.Attr_2.Controls.Add(panel);
			base.Controls.Add(this.\u00A0);
			this.\u1680 = new Panel
			{
				Location = new Point(20, 65),
				Size = new Size(650, 560),
				BackColor = Color.Transparent
			};
			Panel panel2 = this.\u00A0("ขั้นตอน 1: เลือกไฟล์ .BIN ในเครื่อง", new Rectangle(0, 0, 315, 100));
			u.\u00A0 = this.\u00A0(new Point(12, 35), 245, "จิ้มเพื่อเลือกไฟล์จูนของคุณ...");
			\u2051 u2 = new Type_47();
			u2.Text = "";
			u2.\u00A0(\u2050.\u2000);
			u2.Size = new Size(40, 30);
			u2.Location = new Point(262, 35);
			u2.BackColor = this.\u2002;
			u2.ForeColor = this.\u00A0;
			\u2051 u3 = u2;
			u3.Click += u.\u2001;
			panel2.Controls.Add(u.\u00A0);
			panel2.Controls.Add(u3);
			ComboBox comboBox = this.\u00A0("ค้นหาตามรหัสกล่อง ECU", new Point(12, 70), 290);
			string[] array = new string[]
			{
				"38770-K03-H01",
				"รออัพเดท..",
				"รออัพเดท..",
				"รออัพเดท.."
			};
			if (comboBox != null)
			{
				u.\u00A0 = comboBox;
				u.Attr_2.Items.Add("-----------------");
				ComboBox.ObjectCollection items = u.Attr_2.Items;
				object[] items2 = array;
				items.AddRange(items2);
				u.Attr_2.SelectedIndexChanged += u.\u2002;
			}
			panel2.Controls.Add(comboBox);
			Panel panel3 = this.\u00A0("ขั้นตอน 2: ปรับตั้งค่าโหมดการจูน", new Rectangle(330, 0, 315, 100));
			ComboBox comboBox2 = this.\u00A0("โหมดจูนยิง (TPS 0)", new Point(12, 35), 290);
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
			ComboBox comboBox4 = this.\u00A0("ปรับระดับความแรง (Level)", new Point(12, 70), 290);
			if (comboBox4 != null)
			{
				u.\u1680 = comboBox4;
				u.Attr_3.Items.Add("--- ดูไฟล์ทุกเลเวล ---");
				ComboBox.ObjectCollection items4 = u.Attr_3.Items;
				object[] items2 = new string[]
				{
					"เลเวล 1 (เบาๆ)",
					"เลเวล 2 (ปานกลาง)",
					"เลเวล 3 (โหดๆ)"
				};
				items4.AddRange(items2);
				u.Attr_3.SelectedIndexChanged += u.\u2003;
			}
			panel3.Controls.Add(comboBox4);
			u.\u00A0 = this.\u00A0("ข้อมูลไฟล์แชร์จากเซิร์ฟเวอร์ (Live Data)", new Rectangle(0, 115, 645, 380));
			u.Attr_2.Paint += u.\u2000;
			this.\u00A0 = new DataGridView
			{
				Location = new Point(10, 35),
				Size = new Size(625, 335),
				BackgroundColor = this.\u2000,
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
				GridColor = this.\u2002
			};
			this.Attr_2.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(15, 15, 15);
			this.Attr_2.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(200, 200, 200);
			this.Attr_2.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI Semibold", 9f);
			this.Attr_2.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(15, 15, 15);
			this.Attr_2.DefaultCellStyle.BackColor = this.\u2000;
			this.Attr_2.DefaultCellStyle.ForeColor = Color.White;
			this.Attr_2.DefaultCellStyle.SelectionBackColor = Color.FromArgb(40, 10, 10);
			this.Attr_2.DefaultCellStyle.SelectionForeColor = Color.White;
			this.Attr_2.DefaultCellStyle.Font = new Font("Segoe UI", 9f);
			this.Attr_2.Columns.Add("No", "ลำดับ");
			this.Attr_2.Columns.Add("User", "ผู้จูน / สำนัก");
			this.Attr_2.Columns.Add("Ecu", "รหัสกล่อง ECU");
			this.Attr_2.Columns.Add("Details", "รายละเอียดการจูน");
			DataGridViewButtonColumn dataGridViewButtonColumn = new DataGridViewButtonColumn();
			dataGridViewButtonColumn.Name = "Action";
			dataGridViewButtonColumn.HeaderText = "ดาวน์โหลด";
			dataGridViewButtonColumn.FlatStyle = FlatStyle.Flat;
			this.Attr_2.Columns.Add(dataGridViewButtonColumn);
			this.Attr_2.CellMouseMove += u.\u00A0;
			this.Attr_2.CellMouseLeave += u.\u00A0;
			this.Attr_2.CellPainting += u.\u00A0;
			this.Attr_2.RowPostPaint += u.\u00A0;
			this.Attr_2.CellContentClick += u.\u1680;
			this.Attr_2.Columns[0].Width = 40;
			this.Attr_2.Columns[1].Width = 120;
			this.Attr_2.Columns[2].Width = 120;
			this.Attr_2.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
			this.Attr_2.Columns[4].Width = 130;
			u.Attr_2.Controls.Add(this.\u00A0);
			\u2051 u4 = new Type_47();
			u4.Text = "↻ อัพเดทรายการไฟล์ (Refresh)";
			u4.Size = new Size(645, 45);
			u4.Location = new Point(0, 510);
			u4.BackColor = this.\u2000;
			u4.ForeColor = this.\u00A0;
			u4.Font = new Font("Segoe UI", 11f, FontStyle.Bold);
			u4.\u00A0(true);
			\u2051 u5 = u4;
			u5.Click += u.\u2004;
			this.Attr_3.Controls.Add(panel2);
			this.Attr_3.Controls.Add(panel3);
			this.Attr_3.Controls.Add(u.\u00A0);
			this.Attr_3.Controls.Add(u5);
			this.\u2000 = new Panel
			{
				Location = new Point(685, 65),
				Size = new Size(295, 560),
				BackColor = Color.Transparent
			};
			\u2051 u6 = new Type_47();
			u6.Text = "ขั้นตอน 3: โหลดไฟล์ลงเครื่อง";
			u6.Size = new Size(295, 80);
			u6.Location = new Point(0, 0);
			u6.BackColor = this.\u2000;
			u6.ForeColor = Color.FromArgb(255, 40, 40);
			u6.Font = new Font("Segoe UI", 12f, FontStyle.Bold);
			u6.\u00A0(\u2050.\u2002);
			u6.\u00A0(true);
			\u2051 u7 = u6;
			u7.Click += u.\u2005;
			this.Form_4.Controls.Add(u7);
			Panel panel4 = this.\u00A0("อัพโหลดแชร์ไฟล์จูน (Upload)", new Rectangle(0, 95, 295, 465));
			u.\u1680 = this.\u00A0(new Point(12, 40), 271, "ชื่อผู้จูน หรือ ชื่อสำนักของคุณ");
			u.\u2000 = this.\u00A0(new Point(12, 75), 271, "ระบุรายละเอียด (เช่น เวล 1 ยิงโหดๆ)");
			u.\u2001 = this.\u00A0(new Point(12, 110), 271, "รหัสกล่อง ECU (เช่น 38770-K03-H01)");
			panel4.Controls.Add(u.\u1680);
			panel4.Controls.Add(u.\u2000);
			panel4.Controls.Add(u.\u2001);
			u.\u1680 = new Panel
			{
				Size = new Size(271, 150),
				Location = new Point(12, 145),
				BackColor = this.\u2000,
				AllowDrop = true
			};
			u.Attr_3.DragEnter += \u2054.Attr_2.Attr_2.\u00A0;
			u.Attr_3.DragDrop += u.\u00A0;
			u.Attr_3.Click += u.\u2006;
			u.Attr_3.Paint += u.\u2001;
			panel4.Controls.Add(u.\u1680);
			\u2054.\u1680 u8 = u;
			\u2051 u9 = new Type_47();
			u9.Text = "🚀 ส่งไฟล์ขึ้นคลาวด์ (Share)";
			u9.Size = new Size(271, 40);
			u9.Location = new Point(12, 305);
			u9.BackColor = this.\u2000;
			u9.ForeColor = this.\u00A0;
			u9.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
			u9.\u00A0(true);
			u8.\u00A0 = u9;
			u.Attr_2.Click += u.\u2007;
			panel4.Controls.Add(u.\u00A0);
			Panel panel5 = new Panel
			{
				Size = new Size(240, 90),
				Location = new Point(25, 360),
				BackColor = Color.Transparent
			};
			panel5.Paint += u.\u2002;
			panel4.Controls.Add(panel5);
			this.Form_4.Controls.Add(u7);
			this.Form_4.Controls.Add(panel4);
			base.Controls.Add(this.\u1680);
			base.Controls.Add(this.\u2000);
		}

		// Token: 0x06000190 RID: 400 RVA: 0x0000D6B4 File Offset: 0x0000B8B4
		private Task \u00A0(int A_1)
		{
			\u2054.\u2003 u;
			u.\u00A0 = AsyncTaskMethodBuilder.Create();
			u.\u00A0 = this;
			u.\u1680 = A_1;
			u.\u00A0 = -1;
			u.Attr_2.Start<\u2054.\u2003>(ref u);
			return u.Attr_2.Task;
		}

		// Token: 0x06000191 RID: 401 RVA: 0x0000D700 File Offset: 0x0000B900
		private void \u2001()
		{
			\u2054.\u2002 u;
			u.\u00A0 = AsyncVoidMethodBuilder.Create();
			u.\u00A0 = this;
			u.\u00A0 = -1;
			u.Attr_2.Start<\u2054.\u2002>(ref u);
		}

		// Token: 0x06000192 RID: 402 RVA: 0x0000D738 File Offset: 0x0000B938
		private Panel \u00A0(string A_1, Rectangle A_2)
		{
			\u2054.\u2000 u = new \u2054.\u2000();
			u.\u00A0 = this;
			u.\u00A0 = A_1;
			u.\u00A0 = new Panel
			{
				Location = A_2.Location,
				Size = A_2.Size,
				BackColor = Color.Transparent
			};
			u.Attr_2.Paint += u.\u00A0;
			return u.\u00A0;
		}

		// Token: 0x06000193 RID: 403 RVA: 0x0000D7A8 File Offset: 0x0000B9A8
		private TextBox \u00A0(Point A_1, int A_2, string A_3)
		{
			\u2054.\u2001 u = new \u2054.\u2001();
			u.\u00A0 = A_3;
			u.\u00A0 = new TextBox
			{
				Location = A_1,
				Width = A_2,
				BackColor = this.\u2000,
				ForeColor = Color.White,
				BorderStyle = BorderStyle.FixedSingle,
				Font = new Font("Segoe UI", 9f)
			};
			u.Attr_2.Text = u.\u00A0;
			u.Attr_2.ForeColor = Color.DimGray;
			u.Attr_2.Enter += u.\u00A0;
			u.Attr_2.Leave += u.\u1680;
			return u.\u00A0;
		}

		// Token: 0x06000194 RID: 404 RVA: 0x0000D864 File Offset: 0x0000BA64
		private ComboBox \u00A0(string A_1, Point A_2, int A_3)
		{
			return new ComboBox
			{
				Location = A_2,
				Width = A_3,
				BackColor = this.\u2000,
				ForeColor = Color.White,
				DropDownStyle = ComboBoxStyle.DropDownList,
				FlatStyle = FlatStyle.Flat,
				Font = new Font("Segoe UI", 9f),
				Items = 
				{
					A_1
				},
				SelectedIndex = 0
			};
		}

		// Token: 0x06000195 RID: 405 RVA: 0x0000D8D4 File Offset: 0x0000BAD4
		private GraphicsPath \u00A0(Rectangle A_1, int A_2)
		{
			GraphicsPath graphicsPath = new GraphicsPath();
			int num = A_2 * 2;
			graphicsPath.AddArc(A_1.X, A_1.Y, num, num, 180f, 90f);
			graphicsPath.AddArc(A_1.Right - num, A_1.Y, num, num, 270f, 90f);
			graphicsPath.AddArc(A_1.Right - num, A_1.Bottom - num, num, num, 0f, 90f);
			graphicsPath.AddArc(A_1.X, A_1.Bottom - num, num, num, 90f, 90f);
			graphicsPath.CloseFigure();
			return graphicsPath;
		}

		// Token: 0x06000196 RID: 406 RVA: 0x0000D978 File Offset: 0x0000BB78
		[CompilerGenerated]
		private void \u00A0(object A_1, EventArgs A_2)
		{
			if (base.Opacity < 1.0)
			{
				base.Opacity += 0.08;
				return;
			}
			this.Attr_2.Stop();
			this.Attr_2.Dispose();
		}

		// Token: 0x06000197 RID: 407 RVA: 0x0000D9B8 File Offset: 0x0000BBB8
		[CompilerGenerated]
		private void \u1680(object A_1, EventArgs A_2)
		{
			this.\u00A0 += 3;
			if (this.\u00A0 > 380)
			{
				this.\u00A0 = 0;
			}
			if (this.\u1680 != null)
			{
				this.Attr_3.Invalidate(new Rectangle(0, 115, 645, 380));
			}
		}

		// Token: 0x040000F9 RID: 249
		private Color \u00A0 = Color.FromArgb(220, 20, 20);

		// Token: 0x040000FA RID: 250
		private Color \u1680 = Color.FromArgb(180, 0, 0);

		// Token: 0x040000FB RID: 251
		private Color \u2000 = Color.FromArgb(10, 10, 12);

		// Token: 0x040000FC RID: 252
		private Color \u2001 = Color.FromArgb(20, 20, 22);

		// Token: 0x040000FD RID: 253
		private Color \u2002 = Color.FromArgb(40, 40, 45);

		// Token: 0x040000FE RID: 254
		private Panel \u00A0;

		// Token: 0x040000FF RID: 255
		private Label \u00A0;

		// Token: 0x04000100 RID: 256
		private Button \u00A0;

		// Token: 0x04000101 RID: 257
		private DataGridView \u00A0;

		// Token: 0x04000102 RID: 258
		private Panel \u1680;

		// Token: 0x04000103 RID: 259
		private Panel \u2000;

		// Token: 0x04000104 RID: 260
		private Timer \u00A0;

		// Token: 0x04000105 RID: 261
		private Timer \u1680;

		// Token: 0x04000106 RID: 262
		private int \u00A0;

		// Token: 0x04000107 RID: 263
		private string \u00A0 = "";

		// Token: 0x04000108 RID: 264
		private int \u1680 = -1;

		// Token: 0x04000109 RID: 265
		private int \u2000 = -1;

		// Token: 0x0400010A RID: 266
		private string \u1680 = "";

		// Token: 0x0400010B RID: 267
		private string \u2000 = "";

		// Token: 0x0200009E RID: 158
		[CompilerGenerated]
		[Serializable]
		private sealed class Attr_2
		{
			// Token: 0x0600019A RID: 410 RVA: 0x0000DA18 File Offset: 0x0000BC18
			internal bool \u00A0(object A_1, X509Certificate A_2, X509Chain A_3, SslPolicyErrors A_4)
			{
				return true;
			}

			// Token: 0x0600019B RID: 411 RVA: 0x0000DA1B File Offset: 0x0000BC1B
			internal void \u00A0(object A_1, DragEventArgs A_2)
			{
				if (A_2.Data.GetDataPresent(DataFormats.FileDrop))
				{
					A_2.Effect = DragDropEffects.Copy;
				}
			}

			// Token: 0x0600019C RID: 412 RVA: 0x0000DA18 File Offset: 0x0000BC18
			internal bool \u1680(object A_1, X509Certificate A_2, X509Chain A_3, SslPolicyErrors A_4)
			{
				return true;
			}

			// Token: 0x0600019D RID: 413 RVA: 0x0000DA18 File Offset: 0x0000BC18
			internal bool \u2000(object A_1, X509Certificate A_2, X509Chain A_3, SslPolicyErrors A_4)
			{
				return true;
			}

			// Token: 0x0400010C RID: 268
			public static readonly \u2054.\u00A0 \u00A0 = new \u2054.\u00A0();

			// Token: 0x0400010D RID: 269
			public static RemoteCertificateValidationCallback \u00A0;

			// Token: 0x0400010E RID: 270
			public static DragEventHandler \u00A0;

			// Token: 0x0400010F RID: 271
			public static RemoteCertificateValidationCallback \u1680;

			// Token: 0x04000110 RID: 272
			public static RemoteCertificateValidationCallback \u2000;
		}

		// Token: 0x0200009F RID: 159
		[CompilerGenerated]
		private sealed class Attr_3
		{
			// Token: 0x0600019F RID: 415 RVA: 0x0000DA36 File Offset: 0x0000BC36
			internal void \u00A0(object A_1, MouseEventArgs A_2)
			{
				\u2054.\u00A0();
				\u2054.\u00A0(this.Attr_2.Handle, 274, 61458, 0);
			}

			// Token: 0x060001A0 RID: 416 RVA: 0x0000DA5C File Offset: 0x0000BC5C
			internal void \u00A0(object A_1, PaintEventArgs A_2)
			{
				using (LinearGradientBrush linearGradientBrush = new LinearGradientBrush(this.Attr_2.Attr_2.ClientRectangle, Color.FromArgb(15, 15, 15), Color.FromArgb(5, 5, 5), 90f))
				{
					A_2.Graphics.FillRectangle(linearGradientBrush, this.Attr_2.Attr_2.ClientRectangle);
				}
				using (Pen pen = new Pen(this.Attr_2.\u00A0, 2f))
				{
					A_2.Graphics.DrawLine(pen, 0, this.Attr_2.Attr_2.Height - 1, this.Attr_2.Attr_2.Width, this.Attr_2.Attr_2.Height - 1);
				}
				using (Pen pen2 = new Pen(Color.FromArgb(50, this.Attr_2.\u00A0), 4f))
				{
					A_2.Graphics.DrawLine(pen2, 0, this.Attr_2.Attr_2.Height - 2, this.Attr_2.Attr_2.Width, this.Attr_2.Attr_2.Height - 2);
				}
			}

			// Token: 0x060001A1 RID: 417 RVA: 0x0000DBB4 File Offset: 0x0000BDB4
			internal void \u00A0(object A_1, EventArgs A_2)
			{
				this.Attr_2.Attr_2.ForeColor = Color.White;
			}

			// Token: 0x060001A2 RID: 418 RVA: 0x0000DBCB File Offset: 0x0000BDCB
			internal void \u1680(object A_1, EventArgs A_2)
			{
				this.Attr_2.Attr_2.ForeColor = Color.FromArgb(150, 150, 150);
			}

			// Token: 0x060001A3 RID: 419 RVA: 0x0000DBF1 File Offset: 0x0000BDF1
			internal void \u2000(object A_1, EventArgs A_2)
			{
				this.Attr_2.\u2001();
			}

			// Token: 0x060001A4 RID: 420 RVA: 0x0000DC00 File Offset: 0x0000BE00
			internal void \u1680(object A_1, PaintEventArgs A_2)
			{
				A_2.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
				using (GraphicsPath graphicsPath = new GraphicsPath())
				{
					graphicsPath.AddEllipse(1, 1, 28, 28);
					using (PathGradientBrush pathGradientBrush = new PathGradientBrush(graphicsPath))
					{
						pathGradientBrush.CenterColor = Color.FromArgb(180, this.Attr_2.\u00A0);
						pathGradientBrush.SurroundColors = new Color[]
						{
							Color.Transparent
						};
						A_2.Graphics.FillPath(pathGradientBrush, graphicsPath);
					}
				}
				using (SolidBrush solidBrush = new SolidBrush(Color.White))
				{
					A_2.Graphics.FillEllipse(solidBrush, 4, 4, 22, 22);
				}
				using (Font font = new Font("Segoe UI Black", 11f))
				{
					A_2.Graphics.DrawString("M", font, new SolidBrush(this.Attr_2.\u00A0), 6f, 2f);
				}
			}

			// Token: 0x060001A5 RID: 421 RVA: 0x0000DD2C File Offset: 0x0000BF2C
			internal void \u2001(object A_1, EventArgs A_2)
			{
				using (OpenFileDialog openFileDialog = new OpenFileDialog
				{
					Filter = "BIN Files (*.bin)|*.bin",
					Title = "กรุณาเลือกไฟล์ .BIN ที่ต้องการจูน"
				})
				{
					if (openFileDialog.ShowDialog() == DialogResult.OK)
					{
						this.Attr_2.Text = openFileDialog.FileName;
						this.Attr_2.ForeColor = Color.White;
					}
				}
			}

			// Token: 0x060001A6 RID: 422 RVA: 0x0000DD9C File Offset: 0x0000BF9C
			internal void \u2002(object A_1, EventArgs A_2)
			{
				this.Attr_2.\u1680 = ((this.Attr_2.SelectedIndex == 0) ? "" : this.Attr_2.SelectedItem.ToString());
				this.Attr_2.\u1680();
			}

			// Token: 0x060001A7 RID: 423 RVA: 0x0000DDD8 File Offset: 0x0000BFD8
			internal void \u2003(object A_1, EventArgs A_2)
			{
				this.Attr_2.\u2000 = ((this.Attr_3.SelectedIndex == 0) ? "" : this.Attr_3.SelectedItem.ToString());
				this.Attr_2.\u1680();
			}

			// Token: 0x060001A8 RID: 424 RVA: 0x0000DE14 File Offset: 0x0000C014
			internal void \u2000(object A_1, PaintEventArgs A_2)
			{
				if (this.Attr_2.\u00A0 > 0 && this.Attr_2.\u00A0 < this.Attr_2.Height)
				{
					using (LinearGradientBrush linearGradientBrush = new LinearGradientBrush(new Rectangle(0, this.Attr_2.\u00A0, this.Attr_2.Width, 20), Color.FromArgb(80, this.Attr_2.\u00A0), Color.Transparent, 90f))
					{
						A_2.Graphics.FillRectangle(linearGradientBrush, new Rectangle(0, this.Attr_2.\u00A0, this.Attr_2.Width, 20));
					}
					A_2.Graphics.DrawLine(new Pen(this.Attr_2.\u00A0, 1f), 0, this.Attr_2.\u00A0, this.Attr_2.Width, this.Attr_2.\u00A0);
				}
			}

			// Token: 0x060001A9 RID: 425 RVA: 0x0000DF18 File Offset: 0x0000C118
			internal void \u00A0(object A_1, DataGridViewCellMouseEventArgs A_2)
			{
				if (A_2.RowIndex != this.Attr_2.\u1680 || A_2.ColumnIndex != this.Attr_2.\u2000)
				{
					this.Attr_2.\u1680 = A_2.RowIndex;
					this.Attr_2.\u2000 = A_2.ColumnIndex;
					this.Attr_2.Attr_2.Invalidate();
				}
			}

			// Token: 0x060001AA RID: 426 RVA: 0x0000DF7D File Offset: 0x0000C17D
			internal void \u00A0(object A_1, DataGridViewCellEventArgs A_2)
			{
				this.Attr_2.\u1680 = -1;
				this.Attr_2.\u2000 = -1;
				this.Attr_2.Attr_2.Invalidate();
			}

			// Token: 0x060001AB RID: 427 RVA: 0x0000DFA8 File Offset: 0x0000C1A8
			internal void \u00A0(object A_1, DataGridViewCellPaintingEventArgs A_2)
			{
				if (A_2.RowIndex >= 0 && A_2.ColumnIndex >= 0)
				{
					A_2.PaintBackground(A_2.CellBounds, true);
					if (this.Attr_2.Attr_2.Columns[A_2.ColumnIndex].Name == "Action")
					{
						object value = this.Attr_2.Attr_2.Rows[A_2.RowIndex].Cells[0].Value;
						if (value != null && (value.ToString() == "-" || value.ToString() == "ER" || value.ToString() == "00"))
						{
							A_2.Handled = true;
							return;
						}
						Graphics graphics = A_2.Graphics;
						graphics.SmoothingMode = SmoothingMode.AntiAlias;
						Rectangle cellBounds = A_2.CellBounds;
						cellBounds.Inflate(-6, -6);
						bool flag = A_2.RowIndex == this.Attr_2.\u1680 && A_2.ColumnIndex == this.Attr_2.\u2000;
						if (flag)
						{
							using (GraphicsPath graphicsPath = this.Attr_2.\u00A0(cellBounds, 5))
							{
								using (PathGradientBrush pathGradientBrush = new PathGradientBrush(graphicsPath))
								{
									pathGradientBrush.CenterColor = Color.FromArgb(80, this.Attr_2.\u00A0);
									pathGradientBrush.SurroundColors = new Color[]
									{
										Color.Transparent
									};
									graphics.FillPath(pathGradientBrush, graphicsPath);
								}
							}
						}
						using (GraphicsPath graphicsPath2 = this.Attr_2.\u00A0(cellBounds, 5))
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
							using (Pen pen = new Pen(flag ? this.Attr_2.\u00A0 : Color.FromArgb(100, 100, 110), 1.5f))
							{
								graphics.DrawPath(pen, graphicsPath2);
							}
						}
						using (Pen pen2 = new Pen(Color.FromArgb(100, flag ? this.Attr_2.\u00A0 : Color.Gray), 1f))
						{
							graphics.DrawLine(pen2, cellBounds.X + 4, cellBounds.Y + 4, cellBounds.X + 4, cellBounds.Bottom - 4);
							graphics.DrawLine(pen2, cellBounds.Right - 4, cellBounds.Y + 4, cellBounds.Right - 4, cellBounds.Bottom - 4);
						}
						if (flag)
						{
							using (Pen pen3 = new Pen(this.Attr_2.\u00A0, 2f))
							{
								graphics.DrawLine(pen3, cellBounds.X, cellBounds.Y + 6, cellBounds.X, cellBounds.Y);
								graphics.DrawLine(pen3, cellBounds.X, cellBounds.Y, cellBounds.X + 6, cellBounds.Y);
								graphics.DrawLine(pen3, cellBounds.Right - 6, cellBounds.Bottom, cellBounds.Right, cellBounds.Bottom);
								graphics.DrawLine(pen3, cellBounds.Right, cellBounds.Bottom, cellBounds.Right, cellBounds.Bottom - 6);
							}
						}
						string text = "ดาวน์โหลด";
						Font font = new Font("Impact", 8.5f);
						SizeF sizeF = graphics.MeasureString(text, font);
						int num = 14 + 8 + (int)sizeF.Width;
						int num2 = cellBounds.X + (cellBounds.Width - num) / 2;
						int num3 = cellBounds.Y + cellBounds.Height / 2;
						using (Pen pen4 = new Pen(flag ? Color.White : this.Attr_2.\u00A0, 2f))
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
						A_2.Handled = true;
					}
				}
			}

			// Token: 0x060001AC RID: 428 RVA: 0x0000E5D4 File Offset: 0x0000C7D4
			internal void \u00A0(object A_1, DataGridViewRowPostPaintEventArgs A_2)
			{
				object value = this.Attr_2.Attr_2.Rows[A_2.RowIndex].Cells[0].Value;
				string a = (value != null) ? value.ToString() : "";
				if (a == "-" || a == "ER" || a == "00")
				{
					A_2.Graphics.FillRectangle(new SolidBrush(this.Attr_2.\u2000), A_2.RowBounds);
					string s = "";
					if (a == "-")
					{
						s = "--- ไม่พบผู้แชร์ในระบบ (ยังไม่มีไฟล์ถูกอัพโหลด) ---";
					}
					else if (a == "00")
					{
						s = "กำลังเชื่อมต่อกับเซิร์ฟเวอร์คลาวด์ MZATUNER...";
					}
					else if (a == "ER")
					{
						object value2 = this.Attr_2.Attr_2.Rows[A_2.RowIndex].Cells[2].Value;
						s = ((value2 != null) ? value2.ToString() : "เกิดข้อผิดพลาดในการเชื่อมต่อเซิร์ฟเวอร์");
					}
					using (Font font = new Font("Segoe UI", 10.5f, FontStyle.Bold | FontStyle.Italic))
					{
						StringFormat format = new StringFormat
						{
							Alignment = StringAlignment.Center,
							LineAlignment = StringAlignment.Center
						};
						A_2.Graphics.DrawString(s, font, Brushes.Gray, A_2.RowBounds, format);
					}
				}
			}

			// Token: 0x060001AD RID: 429 RVA: 0x0000E748 File Offset: 0x0000C948
			internal void \u1680(object A_1, DataGridViewCellEventArgs A_2)
			{
				if (A_2.RowIndex >= 0 && this.Attr_2.Attr_2.Columns[A_2.ColumnIndex].Name == "Action")
				{
					this.Attr_2.\u00A0(A_2.RowIndex);
				}
			}

			// Token: 0x060001AE RID: 430 RVA: 0x0000E79C File Offset: 0x0000C99C
			internal void \u2004(object A_1, EventArgs A_2)
			{
				this.Attr_2.\u1680();
			}

			// Token: 0x060001AF RID: 431 RVA: 0x0000E7AC File Offset: 0x0000C9AC
			internal void \u2005(object A_1, EventArgs A_2)
			{
				\u2054.Attr_3.\u00A0 u00A;
				u00A.\u00A0 = AsyncVoidMethodBuilder.Create();
				u00A.\u00A0 = this;
				u00A.\u00A0 = -1;
				u00A.Attr_2.Start<\u2054.Attr_3.\u00A0>(ref u00A);
			}

			// Token: 0x060001B0 RID: 432 RVA: 0x0000E7E4 File Offset: 0x0000C9E4
			internal void \u00A0(object A_1, DragEventArgs A_2)
			{
				string[] array = (string[])A_2.Data.GetData(DataFormats.FileDrop);
				if (array.Length != 0 && array[0].EndsWith(".bin", StringComparison.OrdinalIgnoreCase))
				{
					this.Attr_2.\u00A0 = array[0];
					this.Attr_3.Invalidate();
					return;
				}
				MessageBox.Show("กรุณาเลือกไฟล์สกุล .bin เท่านั้น", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}

			// Token: 0x060001B1 RID: 433 RVA: 0x0000E848 File Offset: 0x0000CA48
			internal void \u2006(object A_1, EventArgs A_2)
			{
				using (OpenFileDialog openFileDialog = new OpenFileDialog
				{
					Filter = "BIN files (*.bin)|*.bin|All files (*.*)|*.*"
				})
				{
					if (openFileDialog.ShowDialog() == DialogResult.OK)
					{
						this.Attr_2.\u00A0 = openFileDialog.FileName;
						this.Attr_3.Invalidate();
					}
				}
			}

			// Token: 0x060001B2 RID: 434 RVA: 0x0000E8A8 File Offset: 0x0000CAA8
			internal void \u2001(object A_1, PaintEventArgs A_2)
			{
				A_2.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
				using (Pen pen = new Pen(Color.FromArgb(80, 80, 80), 2f)
				{
					DashStyle = DashStyle.Dash
				})
				{
					using (GraphicsPath graphicsPath = this.Attr_2.\u00A0(new Rectangle(2, 2, this.Attr_3.Width - 4, this.Attr_3.Height - 4), 6))
					{
						A_2.Graphics.DrawPath(pen, graphicsPath);
					}
				}
				int num = this.Attr_3.Width / 2;
				int num2 = this.Attr_3.Height / 2;
				using (Font font = new Font("Segoe UI", 9f))
				{
					StringFormat format = new StringFormat
					{
						Alignment = StringAlignment.Center,
						LineAlignment = StringAlignment.Center
					};
					if (string.IsNullOrEmpty(this.Attr_2.\u00A0))
					{
						A_2.Graphics.DrawLine(new Pen(this.Attr_2.\u00A0, 3f), num, num2 - 20, num, num2);
						A_2.Graphics.DrawLine(new Pen(this.Attr_2.\u00A0, 3f), num - 10, num2 - 10, num, num2);
						A_2.Graphics.DrawLine(new Pen(this.Attr_2.\u00A0, 3f), num + 10, num2 - 10, num, num2);
						A_2.Graphics.DrawLine(new Pen(Color.FromArgb(100, 100, 100), 2f), num - 20, num2 + 8, num + 20, num2 + 8);
						A_2.Graphics.DrawString("ลากไฟล์มาวางตรงนี้\nหรือคลิกเพื่อเลือกไฟล์", font, Brushes.Gray, new Rectangle(0, num2 + 20, this.Attr_3.Width, 40), format);
					}
					else
					{
						A_2.Graphics.DrawRectangle(new Pen(Color.White, 2f), num - 15, num2 - 25, 30, 40);
						A_2.Graphics.FillRectangle(new SolidBrush(this.Attr_2.\u00A0), num - 12, num2 - 5, 24, 15);
						A_2.Graphics.DrawString(Path.GetFileName(this.Attr_2.\u00A0), font, Brushes.White, new Rectangle(0, num2 + 20, this.Attr_3.Width, 40), format);
					}
				}
			}

			// Token: 0x060001B3 RID: 435 RVA: 0x0000EB50 File Offset: 0x0000CD50
			internal void \u2007(object A_1, EventArgs A_2)
			{
				\u2054.Attr_3.\u1680 u;
				u.\u00A0 = AsyncVoidMethodBuilder.Create();
				u.\u00A0 = this;
				u.\u00A0 = -1;
				u.Attr_2.Start<\u2054.Attr_3.\u1680>(ref u);
			}

			// Token: 0x060001B4 RID: 436 RVA: 0x0000EB88 File Offset: 0x0000CD88
			internal void \u2002(object A_1, PaintEventArgs A_2)
			{
				A_2.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
				A_2.Graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
				using (Font font = new Font("Arial Black", 28f, FontStyle.Italic))
				{
					A_2.Graphics.DrawString("MZA", font, new SolidBrush(this.Attr_2.\u00A0), 8f, 0f);
				}
				using (Font font2 = new Font("Showcard Gothic", 16f))
				{
					A_2.Graphics.DrawString("TUNER", font2, Brushes.White, 110f, 16f);
				}
				using (Font font3 = new Font("Segoe UI", 8.5f, FontStyle.Bold))
				{
					A_2.Graphics.DrawString("M Z A T U N E R   E D I T I O N", font3, new SolidBrush(Color.FromArgb(150, 150, 150)), 15f, 55f);
				}
			}

			// Token: 0x04000111 RID: 273
			public \u2054 \u00A0;

			// Token: 0x04000112 RID: 274
			public TextBox \u00A0;

			// Token: 0x04000113 RID: 275
			public ComboBox \u00A0;

			// Token: 0x04000114 RID: 276
			public ComboBox \u1680;

			// Token: 0x04000115 RID: 277
			public Panel \u00A0;

			// Token: 0x04000116 RID: 278
			public Panel \u1680;

			// Token: 0x04000117 RID: 279
			public TextBox \u1680;

			// Token: 0x04000118 RID: 280
			public \u2051 \u00A0;

			// Token: 0x04000119 RID: 281
			public TextBox \u2000;

			// Token: 0x0400011A RID: 282
			public TextBox \u2001;

			// Token: 0x020000A0 RID: 160
			[StructLayout(LayoutKind.Auto)]
			private struct Attr_2 : IAsyncStateMachine
			{
				// Token: 0x060001B5 RID: 437 RVA: 0x0000ECAC File Offset: 0x0000CEAC
				void IAsyncStateMachine.MoveNext()
				{
					int u00A = this.\u00A0;
					\u2054.\u1680 u00A2 = this.\u00A0;
					try
					{
						TaskAwaiter u00A3;
						if (u00A != 0)
						{
							if (u00A2.Attr_2.Attr_2.SelectedRows.Count == 0 || u00A2.Attr_2.Attr_2.SelectedRows[0].Index < 0)
							{
								MessageBox.Show("กรุณาเลือกไฟล์ในตาราง (ด้านซ้าย) ที่ต้องการจะบันทึกก่อนนะครับ!", "MZATUNER", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
								goto IL_F2;
							}
							u00A3 = u00A2.Attr_2.\u00A0(u00A2.Attr_2.Attr_2.SelectedRows[0].Index).GetAwaiter();
							if (!u00A3.IsCompleted)
							{
								this.\u00A0 = 0;
								this.\u00A0 = u00A3;
								this.Attr_2.AwaitUnsafeOnCompleted<TaskAwaiter, \u2054.Attr_3.\u00A0>(ref u00A3, ref this);
								return;
							}
						}
						else
						{
							u00A3 = this.\u00A0;
							this.\u00A0 = default(TaskAwaiter);
							this.\u00A0 = -1;
						}
						u00A3.GetResult();
					}
					catch (Exception exception)
					{
						this.\u00A0 = -2;
						this.Attr_2.SetException(exception);
						return;
					}
					IL_F2:
					this.\u00A0 = -2;
					this.Attr_2.SetResult();
				}

				// Token: 0x060001B6 RID: 438 RVA: 0x0000EDD0 File Offset: 0x0000CFD0
				[DebuggerHidden]
				void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine A_1)
				{
					this.Attr_2.SetStateMachine(A_1);
				}

				// Token: 0x0400011B RID: 283
				public int \u00A0;

				// Token: 0x0400011C RID: 284
				public AsyncVoidMethodBuilder \u00A0;

				// Token: 0x0400011D RID: 285
				public \u2054.\u1680 \u00A0;

				// Token: 0x0400011E RID: 286
				private TaskAwaiter \u00A0;
			}

			// Token: 0x020000A1 RID: 161
			[StructLayout(LayoutKind.Auto)]
			private struct Attr_3 : IAsyncStateMachine
			{
				// Token: 0x060001B7 RID: 439 RVA: 0x0000EDE0 File Offset: 0x0000CFE0
				void IAsyncStateMachine.MoveNext()
				{
					int num = this.\u00A0;
					\u2054.\u1680 u00A = this.\u00A0;
					try
					{
						if (num != 0)
						{
							if (string.IsNullOrEmpty(u00A.Attr_2.\u00A0))
							{
								MessageBox.Show("ลืมแนบไฟล์ครับ! กรุณาเลือกไฟล์ก่อนอัพโหลด", "MZATUNER", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
								goto IL_27F;
							}
							if (u00A.Attr_3.Text.Contains("ชื่อผู้จูน") || string.IsNullOrWhiteSpace(u00A.Attr_3.Text))
							{
								MessageBox.Show("กรุณาใส่ชื่อผู้จูนหรือสำนักก่อนครับ", "MZATUNER", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
								goto IL_27F;
							}
							u00A.Attr_2.Text = "กำลังอัพโหลด...";
							u00A.Attr_2.Enabled = false;
						}
						try
						{
							if (num != 0)
							{
								ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(\u2054.Attr_2.Attr_2.\u1680);
								this.\u00A0 = new WebClient();
							}
							try
							{
								TaskAwaiter<byte[]> u00A2;
								if (num != 0)
								{
									string text = "https://82.26.104.124/Sharefiles/upload.php";
									text = string.Concat(new string[]
									{
										text,
										"?tuner=",
										Uri.EscapeDataString(u00A.Attr_3.Text),
										"&details=",
										Uri.EscapeDataString(u00A.Form_4.Text),
										"&ecu=",
										Uri.EscapeDataString(u00A.Attr_5.Text)
									});
									u00A2 = this.Attr_2.UploadFileTaskAsync(text, "POST", u00A.Attr_2.\u00A0).GetAwaiter();
									if (!u00A2.IsCompleted)
									{
										num = (this.\u00A0 = 0);
										this.\u00A0 = u00A2;
										this.Attr_2.AwaitUnsafeOnCompleted<TaskAwaiter<byte[]>, \u2054.Attr_3.\u1680>(ref u00A2, ref this);
										return;
									}
								}
								else
								{
									u00A2 = this.\u00A0;
									this.\u00A0 = default(TaskAwaiter<byte[]>);
									num = (this.\u00A0 = -1);
								}
								byte[] result = u00A2.GetResult();
								Encoding.UTF8.GetString(result);
								MessageBox.Show("อัพโหลดเรียบร้อย! ขอบคุณที่แบ่งปันสังคมจูนเนอร์ครับ", "MZATUNER", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
								u00A.Attr_2.\u00A0 = "";
								u00A.Attr_3.Invalidate();
								u00A.Attr_2.\u1680();
							}
							finally
							{
								if (num < 0 && this.\u00A0 != null)
								{
									((IDisposable)this.\u00A0).Dispose();
								}
							}
							this.\u00A0 = null;
						}
						catch (Exception ex)
						{
							MessageBox.Show("เกิดข้อผิดพลาดในการส่งไฟล์:\r\n" + ex.Message + "\r\n(ต้องสร้างไฟล์ upload.php บน Server ของคุณด้วยนะครับ)", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Hand);
						}
						finally
						{
							if (num < 0)
							{
								u00A.Attr_2.Text = "🚀 ส่งไฟล์ขึ้นคลาวด์ (Share)";
								u00A.Attr_2.Enabled = true;
							}
						}
					}
					catch (Exception exception)
					{
						this.\u00A0 = -2;
						this.Attr_2.SetException(exception);
						return;
					}
					IL_27F:
					this.\u00A0 = -2;
					this.Attr_2.SetResult();
				}

				// Token: 0x060001B8 RID: 440 RVA: 0x0000F0E4 File Offset: 0x0000D2E4
				[DebuggerHidden]
				void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine A_1)
				{
					this.Attr_2.SetStateMachine(A_1);
				}

				// Token: 0x0400011F RID: 287
				public int \u00A0;

				// Token: 0x04000120 RID: 288
				public AsyncVoidMethodBuilder \u00A0;

				// Token: 0x04000121 RID: 289
				public \u2054.\u1680 \u00A0;

				// Token: 0x04000122 RID: 290
				private WebClient \u00A0;

				// Token: 0x04000123 RID: 291
				private TaskAwaiter<byte[]> \u00A0;
			}
		}

		// Token: 0x020000A2 RID: 162
		[CompilerGenerated]
		private sealed class Form_4
		{
			// Token: 0x060001BA RID: 442 RVA: 0x0000F0F4 File Offset: 0x0000D2F4
			internal void \u00A0(object A_1, PaintEventArgs A_2)
			{
				A_2.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
				Rectangle rectangle = new Rectangle(0, 10, this.Attr_2.Width - 1, this.Attr_2.Height - 11);
				using (GraphicsPath graphicsPath = this.Attr_2.\u00A0(rectangle, 8))
				{
					using (SolidBrush solidBrush = new SolidBrush(this.Attr_2.\u2001))
					{
						A_2.Graphics.FillPath(solidBrush, graphicsPath);
					}
					using (Pen pen = new Pen(this.Attr_2.\u2002, 1f))
					{
						A_2.Graphics.DrawPath(pen, graphicsPath);
					}
				}
				using (Pen pen2 = new Pen(this.Attr_2.\u00A0, 2f))
				{
					A_2.Graphics.DrawLine(pen2, rectangle.X, rectangle.Y + 20, rectangle.X, rectangle.Y + 8);
					A_2.Graphics.DrawArc(pen2, rectangle.X, rectangle.Y, 16, 16, 180, 90);
					A_2.Graphics.DrawLine(pen2, rectangle.X + 8, rectangle.Y, rectangle.X + 30, rectangle.Y);
				}
				using (Font font = new Font("Segoe UI", 8.5f, FontStyle.Bold))
				{
					A_2.Graphics.DrawString(this.\u00A0, font, Brushes.White, 35f, 0f);
				}
			}

			// Token: 0x04000124 RID: 292
			public Panel \u00A0;

			// Token: 0x04000125 RID: 293
			public \u2054 \u00A0;

			// Token: 0x04000126 RID: 294
			public string \u00A0;
		}

		// Token: 0x020000A3 RID: 163
		[CompilerGenerated]
		private sealed class Attr_5
		{
			// Token: 0x060001BC RID: 444 RVA: 0x0000F2CC File Offset: 0x0000D4CC
			internal void \u00A0(object A_1, EventArgs A_2)
			{
				if (this.Attr_2.Text == this.\u00A0)
				{
					this.Attr_2.Text = "";
					this.Attr_2.ForeColor = Color.White;
				}
			}

			// Token: 0x060001BD RID: 445 RVA: 0x0000F306 File Offset: 0x0000D506
			internal void \u1680(object A_1, EventArgs A_2)
			{
				if (string.IsNullOrEmpty(this.Attr_2.Text))
				{
					this.Attr_2.Text = this.\u00A0;
					this.Attr_2.ForeColor = Color.DimGray;
				}
			}

			// Token: 0x04000127 RID: 295
			public TextBox \u00A0;

			// Token: 0x04000128 RID: 296
			public string \u00A0;
		}
	}
}
