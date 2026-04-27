using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using <PrivateImplementationDetails>{68F2EF73-9355-4257-ADA6-397CF8BB8E72};

namespace ns1
{
	// Token: 0x020000A8 RID: 168
	public partial class GForm8 : Form
	{
		// Token: 0x060001C9 RID: 457
		[DllImport("user32.dll")]
		public static extern bool ReleaseCapture();

		// Token: 0x060001CA RID: 458
		[DllImport("user32.dll")]
		public static extern int SendMessage(IntPtr intptr_0, int int_2, int int_3, int int_4);

		// Token: 0x060001CB RID: 459
		[DllImport("Gdi32.dll")]
		private static extern IntPtr CreateRoundRectRgn(int int_2, int int_3, int int_4, int int_5, int int_6, int int_7);

		// Token: 0x060001CC RID: 460 RVA: 0x0002361C File Offset: 0x0002181C
		public GForm8()
		{
			base.Size = new Size(1000, 650);
			base.FormBorderStyle = FormBorderStyle.None;
			this.BackColor = this.color_1;
			base.StartPosition = FormStartPosition.CenterParent;
			this.DoubleBuffered = true;
			base.Region = Region.FromHrgn(GForm8.CreateRoundRectRgn(0, 0, base.Width, base.Height, 25, 25));
			this.method_0();
			this.method_8();
			this.timer_0 = new Timer
			{
				Interval = 30
			};
			this.timer_0.Tick += this.timer_0_Tick;
			this.timer_0.Start();
		}

		// Token: 0x060001CD RID: 461 RVA: 0x00023744 File Offset: 0x00021944
		private void method_0()
		{
			this.panel_0 = new Panel
			{
				Dock = DockStyle.Top,
				Height = 50,
				BackColor = Color.FromArgb(10, 10, 12)
			};
			this.panel_0.MouseDown += this.panel_0_MouseDown;
			this.panel_0.Paint += this.panel_0_Paint;
			this.label_0 = new Label
			{
				Text = "◢ ระบบจัดการรหัสกล่อง ECU : คอนโซลควบคุมหลัก ◣",
				ForeColor = Color.White,
				Font = new Font("Impact", 12f),
				Location = new Point(15, 15),
				AutoSize = true,
				BackColor = Color.Transparent
			};
			this.button_0 = new Button
			{
				Text = "×",
				Dock = DockStyle.Right,
				Width = 50,
				FlatStyle = FlatStyle.Flat,
				ForeColor = Color.Gray,
				Font = new Font("Arial", 18f, FontStyle.Bold)
			};
			this.button_0.FlatAppearance.BorderSize = 0;
			this.button_0.Click += this.button_0_Click;
			this.panel_0.Controls.Add(this.label_0);
			this.panel_0.Controls.Add(this.button_0);
			base.Controls.Add(this.panel_0);
			this.button_1 = this.method_1("◢ หมวดหมู่ : รถเกียร์ [ MANUAL ]", new Point(25, 65), true);
			this.button_2 = this.method_1("◢ หมวดหมู่ : รถออโต้ [ AUTO ]", new Point(310, 65), false);
			this.button_1.Click += this.button_1_Click;
			this.button_2.Click += this.button_2_Click;
			base.Controls.Add(this.button_1);
			base.Controls.Add(this.button_2);
			this.textBox_0 = new TextBox
			{
				Location = new Point(620, 65),
				Width = 355,
				BackColor = Color.Black,
				ForeColor = this.color_0,
				BorderStyle = BorderStyle.FixedSingle,
				Font = new Font("Consolas", 12f)
			};
			this.textBox_0.TextChanged += this.textBox_0_TextChanged;
			base.Controls.Add(this.textBox_0);
			base.Controls.Add(new Label
			{
				Text = "ค้นหาสายธารข้อมูลรหัสพาร์ท",
				ForeColor = Color.FromArgb(120, this.color_0),
				Font = new Font("Leelawadee UI", 7f, FontStyle.Bold),
				Location = new Point(620, 50)
			});
			this.dataGridView_0 = new DataGridView
			{
				Location = new Point(25, 115),
				Size = new Size(610, 430),
				BackgroundColor = Color.Black,
				BorderStyle = BorderStyle.None,
				EnableHeadersVisualStyles = false,
				AllowUserToAddRows = false,
				ReadOnly = true,
				SelectionMode = DataGridViewSelectionMode.FullRowSelect,
				RowHeadersVisible = false,
				GridColor = Color.FromArgb(25, 25, 30),
				ForeColor = Color.White,
				RowTemplate = 
				{
					Height = 35
				}
			};
			this.dataGridView_0.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(15, 15, 20);
			this.dataGridView_0.ColumnHeadersDefaultCellStyle.ForeColor = Color.Gray;
			this.dataGridView_0.ColumnHeadersDefaultCellStyle.Font = new Font("Consolas", 9f, FontStyle.Bold);
			this.dataGridView_0.DefaultCellStyle.BackColor = Color.Black;
			this.dataGridView_0.DefaultCellStyle.SelectionBackColor = Color.FromArgb(100, 20, 20);
			this.dataGridView_0.DefaultCellStyle.SelectionForeColor = Color.White;
			this.dataGridView_0.Columns.Add("PartCode", "รหัสพาร์ท / รุ่นรถ");
			this.dataGridView_0.Columns.Add("EcmId", "หมายเลข ECM ID");
			this.dataGridView_0.Columns.Add("StartOffset", "START");
			this.dataGridView_0.Columns.Add("CksumOffset", "CHKSUM");
			this.dataGridView_0.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
			this.dataGridView_0.SelectionChanged += this.dataGridView_0_SelectionChanged;
			this.dataGridView_0.CellPainting += this.dataGridView_0_CellPainting;
			base.Controls.Add(this.dataGridView_0);
			this.panel_1 = new Panel
			{
				Location = new Point(650, 115),
				Size = new Size(325, 430),
				BackColor = this.color_2
			};
			this.panel_1.Paint += this.panel_1_Paint;
			this.method_4();
			base.Controls.Add(this.panel_1);
			this.button_6 = new Button
			{
				Text = "◢ บันทึกข้อมูลทั้งหมดลงฐานข้อมูลหลัก ◣",
				Location = new Point(25, 560),
				Size = new Size(950, 60),
				BackColor = Color.FromArgb(10, 10, 15),
				ForeColor = this.color_0,
				FlatStyle = FlatStyle.Flat,
				Font = new Font("Impact", 13f)
			};
			this.button_6.FlatAppearance.BorderSize = 0;
			this.button_6.Paint += this.button_6_Paint;
			this.button_6.Click += this.button_6_Click;
			base.Controls.Add(this.button_6);
			base.Paint += this.GForm8_Paint;
		}

		// Token: 0x060001CE RID: 462 RVA: 0x00023D3C File Offset: 0x00021F3C
		private Button method_1(string string_1, Point point_0, bool bool_1)
		{
			return new Button
			{
				Text = string_1,
				Location = point_0,
				Size = new Size(270, 40),
				BackColor = (bool_1 ? Color.FromArgb(60, 20, 20) : Color.FromArgb(20, 20, 25)),
				ForeColor = (bool_1 ? Color.White : Color.Gray),
				FlatStyle = FlatStyle.Flat,
				Font = new Font("Impact", 10f),
				TextAlign = ContentAlignment.MiddleLeft,
				FlatAppearance = 
				{
					BorderSize = 1,
					BorderColor = (bool_1 ? this.color_0 : this.color_3)
				}
			};
		}

		// Token: 0x060001CF RID: 463 RVA: 0x00023DF0 File Offset: 0x00021FF0
		private void method_2(Graphics graphics_0, Rectangle rectangle_0)
		{
			using (new Pen(Color.FromArgb(150, 20, 20), 1f))
			{
				for (int i = 0; i < rectangle_0.Width; i += 40)
				{
					graphics_0.DrawLine(new Pen(Color.FromArgb(10, 20, 20), 1f), i, 0, i, rectangle_0.Height);
				}
				for (int j = 0; j < rectangle_0.Height; j += 40)
				{
					graphics_0.DrawLine(new Pen(Color.FromArgb(10, 20, 20), 1f), 0, j, rectangle_0.Width, j);
				}
			}
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x00023EA4 File Offset: 0x000220A4
		private void method_3(Graphics graphics_0, Rectangle rectangle_0, string string_1)
		{
			graphics_0.SmoothingMode = SmoothingMode.AntiAlias;
			using (Pen pen = new Pen(this.color_3, 1f))
			{
				graphics_0.DrawRectangle(pen, rectangle_0.X, rectangle_0.Y, rectangle_0.Width - 1, rectangle_0.Height - 1);
			}
			int num = 60;
			using (Pen pen2 = new Pen(this.color_0, 3f))
			{
				graphics_0.DrawLine(pen2, rectangle_0.X + this.int_1, rectangle_0.Y, rectangle_0.X + this.int_1 + num, rectangle_0.Y);
				graphics_0.DrawLine(pen2, rectangle_0.Right - 1, rectangle_0.Y + this.int_1, rectangle_0.Right - 1, rectangle_0.Y + this.int_1 + num);
			}
			using (Pen pen3 = new Pen(this.color_0, 2f))
			{
				graphics_0.DrawLine(pen3, rectangle_0.X, rectangle_0.Y, rectangle_0.X + 20, rectangle_0.Y);
				graphics_0.DrawLine(pen3, rectangle_0.X, rectangle_0.Y, rectangle_0.X, rectangle_0.Y + 20);
				graphics_0.DrawLine(pen3, rectangle_0.Right - 1, rectangle_0.Bottom - 1, rectangle_0.Right - 1 - 20, rectangle_0.Bottom - 1);
				graphics_0.DrawLine(pen3, rectangle_0.Right - 1, rectangle_0.Bottom - 1, rectangle_0.Right - 1, rectangle_0.Bottom - 1 - 20);
			}
			using (Font font = new Font("Consolas", 7f, FontStyle.Bold))
			{
				graphics_0.DrawString("◢ " + string_1 + " ◣", font, new SolidBrush(Color.FromArgb(150, this.color_0)), (float)(rectangle_0.X + 5), (float)(rectangle_0.Y - 12));
			}
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x000240E4 File Offset: 0x000222E4
		private void dataGridView_0_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
		{
			if (e.RowIndex >= 0 && e.State.HasFlag(DataGridViewElementStates.Selected))
			{
				e.PaintBackground(e.CellBounds, true);
				using (LinearGradientBrush linearGradientBrush = new LinearGradientBrush(e.CellBounds, Color.FromArgb(60, this.color_0), Color.Transparent, 0f))
				{
					e.Graphics.FillRectangle(linearGradientBrush, e.CellBounds);
				}
				e.PaintContent(e.CellBounds);
				e.Handled = true;
			}
			if (e.RowIndex == this.dataGridView_0.Rows.Count - 1 && e.ColumnIndex == this.dataGridView_0.Columns.Count - 1)
			{
				int num = this.int_0 - 120;
				if (num > 0 && num < this.dataGridView_0.Height)
				{
					using (Pen pen = new Pen(Color.FromArgb(180, this.color_0), 1f))
					{
						e.Graphics.DrawLine(pen, 0, num, this.dataGridView_0.Width, num);
					}
					using (LinearGradientBrush linearGradientBrush2 = new LinearGradientBrush(new Rectangle(0, num - 15, this.dataGridView_0.Width, 15), Color.Transparent, Color.FromArgb(50, this.color_0), 90f))
					{
						e.Graphics.FillRectangle(linearGradientBrush2, 0, num - 15, this.dataGridView_0.Width, 15);
					}
				}
			}
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x00024298 File Offset: 0x00022498
		private void method_4()
		{
			int num = 25;
			this.textBox_1 = this.method_5("◤ รหัสพาร์ท / รุ่นรถ ◢", ref num);
			this.textBox_2 = this.method_5("◤ หมายเลขลายเซ็น ECM_ID ◢", ref num);
			this.textBox_3 = this.method_5("◤ ตำแหน่งเริ่มหน่วยความจำ (HEX) ◢", ref num);
			this.textBox_4 = this.method_5("◤ ตำแหน่งเช็คซัม (CHECKSUM) ◢", ref num);
			num += 5;
			this.button_3 = this.method_6("◢ เพิ่มข้อมูลใหม่ ◣", num, Color.FromArgb(0, 100, 200));
			this.button_3.Click += this.button_3_Click;
			this.button_4 = this.method_6("◢ บันทึกการแก้ไข ◣", num + 45, Color.FromArgb(35, 35, 40));
			this.button_4.Click += this.button_4_Click;
			this.button_5 = this.method_6("◢ ลบข้อมูลนี้ออก ◣", num + 90, Color.FromArgb(80, 0, 0));
			this.button_5.Click += this.button_5_Click;
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x0002439C File Offset: 0x0002259C
		private TextBox method_5(string string_1, ref int int_2)
		{
			this.panel_1.Controls.Add(new Label
			{
				Text = string_1,
				ForeColor = Color.Gray,
				Location = new Point(20, int_2),
				Font = new Font("Impact", 8f),
				AutoSize = true
			});
			TextBox textBox = new TextBox
			{
				Location = new Point(20, int_2 + 18),
				Width = 285,
				BackColor = Color.Black,
				ForeColor = Color.White,
				BorderStyle = BorderStyle.FixedSingle,
				Font = new Font("Consolas", 11f)
			};
			this.panel_1.Controls.Add(textBox);
			int_2 += 58;
			return textBox;
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x0002446C File Offset: 0x0002266C
		private Button method_6(string string_1, int int_2, Color color_5)
		{
			Button button = new Button
			{
				Text = string_1,
				Location = new Point(20, int_2),
				Size = new Size(285, 38),
				BackColor = color_5,
				ForeColor = Color.White,
				FlatStyle = FlatStyle.Flat,
				Font = new Font("Impact", 10f)
			};
			button.FlatAppearance.BorderSize = 0;
			this.panel_1.Controls.Add(button);
			return button;
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x000244F4 File Offset: 0x000226F4
		private void method_7(string string_1)
		{
			this.string_0 = string_1;
			this.button_1.BackColor = ((this.string_0 == "รถเกียร์") ? Color.FromArgb(60, 20, 20) : Color.FromArgb(20, 20, 25));
			this.button_1.ForeColor = ((this.string_0 == "รถเกียร์") ? Color.White : Color.Gray);
			this.button_1.FlatAppearance.BorderColor = ((this.string_0 == "รถเกียร์") ? this.color_0 : this.color_3);
			this.button_2.BackColor = ((this.string_0 == "รถออโต้") ? Color.FromArgb(60, 20, 20) : Color.FromArgb(20, 20, 25));
			this.button_2.ForeColor = ((this.string_0 == "รถออโต้") ? Color.White : Color.Gray);
			this.button_2.FlatAppearance.BorderColor = ((this.string_0 == "รถออโต้") ? this.color_0 : this.color_3);
			this.method_8();
			Console.Beep(1500, 50);
			base.Invalidate();
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x0002463C File Offset: 0x0002283C
		private void method_8()
		{
			this.dataGridView_0.Rows.Clear();
			foreach (GClass16 gclass in GClass17.smethod_0().Where(new Func<GClass16, bool>(this.method_13)).ToList<GClass16>())
			{
				this.dataGridView_0.Rows.Add(new object[]
				{
					gclass.method_0(),
					gclass.method_2(),
					gclass.method_4(),
					gclass.method_6()
				});
			}
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x000246EC File Offset: 0x000228EC
		private void method_9(string string_1)
		{
			GForm8.Class133 @class = new GForm8.Class133();
			@class.gform8_0 = this;
			@class.string_0 = string_1;
			this.dataGridView_0.Rows.Clear();
			foreach (GClass16 gclass in GClass17.smethod_0().Where(new Func<GClass16, bool>(@class.method_0)).ToList<GClass16>())
			{
				this.dataGridView_0.Rows.Add(new object[]
				{
					gclass.method_0(),
					gclass.method_2(),
					gclass.method_4(),
					gclass.method_6()
				});
			}
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x000247B0 File Offset: 0x000229B0
		private void dataGridView_0_SelectionChanged(object sender, EventArgs e)
		{
			if (this.dataGridView_0.SelectedRows.Count > 0)
			{
				GForm8.Class134 @class = new GForm8.Class134();
				@class.string_0 = this.dataGridView_0.SelectedRows[0].Cells[0].Value.ToString();
				GClass16 gclass = GClass17.smethod_0().FirstOrDefault(new Func<GClass16, bool>(@class.method_0));
				if (gclass != null)
				{
					this.textBox_1.Text = gclass.method_0();
					this.textBox_2.Text = gclass.method_2();
					this.textBox_3.Text = gclass.method_4();
					this.textBox_4.Text = gclass.method_6();
				}
			}
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x00024864 File Offset: 0x00022A64
		private void method_10()
		{
			if (string.IsNullOrWhiteSpace(this.textBox_1.Text))
			{
				return;
			}
			List<GClass16> list = GClass17.smethod_0();
			GClass16 gclass = new GClass16();
			gclass.method_1(this.textBox_1.Text);
			gclass.method_3(this.textBox_2.Text);
			gclass.method_5(this.textBox_3.Text);
			gclass.method_7(this.textBox_4.Text);
			gclass.method_9((this.string_0 == "รถเกียร์") ? GEnum2.const_0 : GEnum2.const_1);
			list.Add(gclass);
			this.method_8();
			Console.Beep(1200, 100);
		}

		// Token: 0x060001DA RID: 474 RVA: 0x00024908 File Offset: 0x00022B08
		private void method_11()
		{
			GClass16 gclass = GClass17.smethod_0().FirstOrDefault(new Func<GClass16, bool>(this.method_14));
			if (gclass != null)
			{
				gclass.method_3(this.textBox_2.Text);
				gclass.method_5(this.textBox_3.Text);
				gclass.method_7(this.textBox_4.Text);
				this.method_8();
				Console.Beep(1500, 100);
			}
		}

		// Token: 0x060001DB RID: 475 RVA: 0x00024974 File Offset: 0x00022B74
		private void method_12()
		{
			if (this.dataGridView_0.SelectedRows.Count == 0)
			{
				return;
			}
			if (MessageBox.Show("◢ ยืนยันการลบไฟล์ชุดนี้ออกจากสารบบหรือไม่? ◣", "MASTER_CORE_SECURITY", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
			{
				GClass17.smethod_0().RemoveAll(new Predicate<GClass16>(this.method_15));
				this.method_8();
				Console.Beep(800, 200);
			}
		}

		// Token: 0x060001DC RID: 476 RVA: 0x000249D8 File Offset: 0x00022BD8
		private void button_6_Click(object sender, EventArgs e)
		{
			try
			{
				GClass17.smethod_5();
				GForm15.smethod_1("บันทึกรหัสกล่องลงโปรแกรม", "บันทึกรหัสกล่องลงโปรแกรมสำเร็จ", GEnum1.const_0);
				Console.Beep(2000, 150);
			}
			catch (Exception ex)
			{
				MessageBox.Show("ล้มเหลว: " + ex.Message);
			}
		}

		// Token: 0x060001DD RID: 477 RVA: 0x00024A34 File Offset: 0x00022C34
		[CompilerGenerated]
		private void timer_0_Tick(object sender, EventArgs e)
		{
			this.int_0 += 5;
			if (this.int_0 > 550)
			{
				this.int_0 = 0;
			}
			this.int_1 = (this.int_1 + 3) % 200;
			if (this.bool_0)
			{
				this.float_0 += 0.04f;
				if (this.float_0 >= 1f)
				{
					this.bool_0 = false;
				}
			}
			else
			{
				this.float_0 -= 0.04f;
				if (this.float_0 <= 0.4f)
				{
					this.bool_0 = true;
				}
			}
			base.Invalidate(false);
			this.button_6.Invalidate();
			this.dataGridView_0.Invalidate();
		}

		// Token: 0x060001DE RID: 478 RVA: 0x0000CCC3 File Offset: 0x0000AEC3
		[CompilerGenerated]
		private void panel_0_MouseDown(object sender, MouseEventArgs e)
		{
			GForm8.ReleaseCapture();
			GForm8.SendMessage(base.Handle, 274, 61458, 0);
		}

		// Token: 0x060001DF RID: 479 RVA: 0x00024AEC File Offset: 0x00022CEC
		[CompilerGenerated]
		private void panel_0_Paint(object sender, PaintEventArgs e)
		{
			e.Graphics.DrawLine(new Pen(this.color_0, 2f), 0, this.panel_0.Height - 1, this.panel_0.Width, this.panel_0.Height - 1);
			using (LinearGradientBrush linearGradientBrush = new LinearGradientBrush(new Rectangle(0, this.panel_0.Height - 6, this.panel_0.Width, 6), Color.Transparent, Color.FromArgb(60, this.color_0), 90f))
			{
				e.Graphics.FillRectangle(linearGradientBrush, 0, this.panel_0.Height - 6, this.panel_0.Width, 6);
			}
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x0000C305 File Offset: 0x0000A505
		[CompilerGenerated]
		private void button_0_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x0000CCE2 File Offset: 0x0000AEE2
		[CompilerGenerated]
		private void button_1_Click(object sender, EventArgs e)
		{
			this.method_7("รถเกียร์");
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x0000CCEF File Offset: 0x0000AEEF
		[CompilerGenerated]
		private void button_2_Click(object sender, EventArgs e)
		{
			this.method_7("รถออโต้");
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x0000CCFC File Offset: 0x0000AEFC
		[CompilerGenerated]
		private void textBox_0_TextChanged(object sender, EventArgs e)
		{
			this.method_9(this.textBox_0.Text);
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x0000CD0F File Offset: 0x0000AF0F
		[CompilerGenerated]
		private void panel_1_Paint(object sender, PaintEventArgs e)
		{
			this.method_3(e.Graphics, this.panel_1.ClientRectangle, "ระบบแก้ไขข้อมูลหลัก");
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x00024BB8 File Offset: 0x00022DB8
		[CompilerGenerated]
		private void button_6_Paint(object sender, PaintEventArgs e)
		{
			using (Pen pen = new Pen(Color.FromArgb((int)(this.float_0 * 255f), this.color_0), 2f))
			{
				e.Graphics.DrawRectangle(pen, 0, 0, this.button_6.Width - 1, this.button_6.Height - 1);
			}
			using (Pen pen2 = new Pen(Color.FromArgb(100, this.color_0), 1f))
			{
				e.Graphics.DrawLine(pen2, 10, 10, 30, 10);
				e.Graphics.DrawLine(pen2, this.button_6.Width - 30, 10, this.button_6.Width - 10, 10);
			}
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x00024C9C File Offset: 0x00022E9C
		[CompilerGenerated]
		private void GForm8_Paint(object sender, PaintEventArgs e)
		{
			e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
			this.method_2(e.Graphics, base.ClientRectangle);
			this.method_3(e.Graphics, new Rectangle(20, 110, 620, 440), (this.string_0 == "รถเกียร์") ? "ระบบย่อยรถเกียร์" : "ระบบย่อยรถออโต้");
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x0000CD2D File Offset: 0x0000AF2D
		[CompilerGenerated]
		private void button_3_Click(object sender, EventArgs e)
		{
			this.method_10();
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x0000CD35 File Offset: 0x0000AF35
		[CompilerGenerated]
		private void button_4_Click(object sender, EventArgs e)
		{
			this.method_11();
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x0000CD3D File Offset: 0x0000AF3D
		[CompilerGenerated]
		private void button_5_Click(object sender, EventArgs e)
		{
			this.method_12();
		}

		// Token: 0x060001EA RID: 490 RVA: 0x0000CD45 File Offset: 0x0000AF45
		[CompilerGenerated]
		private bool method_13(GClass16 gclass16_0)
		{
			return (this.string_0 == "รถเกียร์" && gclass16_0.method_8() == GEnum2.const_0) || (this.string_0 == "รถออโต้" && gclass16_0.method_8() == GEnum2.const_1);
		}

		// Token: 0x060001EB RID: 491 RVA: 0x0000CD80 File Offset: 0x0000AF80
		[CompilerGenerated]
		private bool method_14(GClass16 gclass16_0)
		{
			return gclass16_0.method_0() == this.textBox_1.Text;
		}

		// Token: 0x060001EC RID: 492 RVA: 0x0000CD98 File Offset: 0x0000AF98
		[CompilerGenerated]
		private bool method_15(GClass16 gclass16_0)
		{
			return gclass16_0.method_0() == this.dataGridView_0.SelectedRows[0].Cells[0].Value.ToString();
		}

		// Token: 0x0400013E RID: 318
		private Color color_0 = Color.FromArgb(220, 20, 20);

		// Token: 0x0400013F RID: 319
		private Color color_1 = Color.FromArgb(8, 8, 10);

		// Token: 0x04000140 RID: 320
		private Color color_2 = Color.FromArgb(15, 15, 18);

		// Token: 0x04000141 RID: 321
		private Color color_3 = Color.FromArgb(40, 40, 45);

		// Token: 0x04000142 RID: 322
		private Color color_4 = Color.FromArgb(255, 50, 50);

		// Token: 0x04000143 RID: 323
		private Panel panel_0;

		// Token: 0x04000144 RID: 324
		private Label label_0;

		// Token: 0x04000145 RID: 325
		private Button button_0;

		// Token: 0x04000146 RID: 326
		private DataGridView dataGridView_0;

		// Token: 0x04000147 RID: 327
		private Panel panel_1;

		// Token: 0x04000148 RID: 328
		private TextBox textBox_0;

		// Token: 0x04000149 RID: 329
		private Button button_1;

		// Token: 0x0400014A RID: 330
		private Button button_2;

		// Token: 0x0400014B RID: 331
		private string string_0 = "รถเกียร์";

		// Token: 0x0400014C RID: 332
		private TextBox textBox_1;

		// Token: 0x0400014D RID: 333
		private TextBox textBox_2;

		// Token: 0x0400014E RID: 334
		private TextBox textBox_3;

		// Token: 0x0400014F RID: 335
		private TextBox textBox_4;

		// Token: 0x04000150 RID: 336
		private Button button_3;

		// Token: 0x04000151 RID: 337
		private Button button_4;

		// Token: 0x04000152 RID: 338
		private Button button_5;

		// Token: 0x04000153 RID: 339
		private Button button_6;

		// Token: 0x04000154 RID: 340
		private Timer timer_0;

		// Token: 0x04000155 RID: 341
		private int int_0;

		// Token: 0x04000156 RID: 342
		private float float_0 = 1f;

		// Token: 0x04000157 RID: 343
		private bool bool_0;

		// Token: 0x04000158 RID: 344
		private int int_1;

		// Token: 0x04000159 RID: 345
		private float float_1 = 0.1f;

		// Token: 0x020000A9 RID: 169
		[CompilerGenerated]
		private sealed class Class133
		{
			// Token: 0x060001EE RID: 494 RVA: 0x00024D04 File Offset: 0x00022F04
			internal bool method_0(GClass16 gclass16_0)
			{
				return ((this.gform8_0.string_0 == EC2D41B1-A2F9-4664-90D8-86645EE2E753.smethod_247() && gclass16_0.method_8() == GEnum2.const_0) || (this.gform8_0.string_0 == EC2D41B1-A2F9-4664-90D8-86645EE2E753.smethod_270() && gclass16_0.method_8() == GEnum2.const_1)) && (gclass16_0.method_0().IndexOf(this.string_0, StringComparison.OrdinalIgnoreCase) >= 0 || gclass16_0.method_2().IndexOf(this.string_0, StringComparison.OrdinalIgnoreCase) >= 0);
			}

			// Token: 0x0400015A RID: 346
			public GForm8 gform8_0;

			// Token: 0x0400015B RID: 347
			public string string_0;
		}

		// Token: 0x020000AA RID: 170
		[CompilerGenerated]
		private sealed class Class134
		{
			// Token: 0x060001F0 RID: 496 RVA: 0x0000CDCB File Offset: 0x0000AFCB
			internal bool method_0(GClass16 gclass16_0)
			{
				return gclass16_0.method_0() == this.string_0;
			}

			// Token: 0x0400015C RID: 348
			public string string_0;
		}
	}
}
