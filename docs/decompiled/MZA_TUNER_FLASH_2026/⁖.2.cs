using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using <PrivateImplementationDetails>{68F2EF73-9355-4257-ADA6-397CF8BB8E72};

namespace Attr_3
{
	// Token: 0x020000A8 RID: 168
	public partial class Type_4C : Form
	{
		// Token: 0x060001C9 RID: 457
		[DllImport("user32.dll", EntryPoint = "ReleaseCapture")]
		public static extern bool \u00A0();

		// Token: 0x060001CA RID: 458
		[DllImport("user32.dll", EntryPoint = "SendMessage")]
		public static extern int \u00A0(IntPtr, int, int, int);

		// Token: 0x060001CB RID: 459
		[DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
		private static extern IntPtr \u00A0(int, int, int, int, int, int);

		// Token: 0x060001CC RID: 460 RVA: 0x0001009C File Offset: 0x0000E29C
		public Type_4C()
		{
			base.Size = new Size(1000, 650);
			base.FormBorderStyle = FormBorderStyle.None;
			this.BackColor = this.\u1680;
			base.StartPosition = FormStartPosition.CenterParent;
			this.DoubleBuffered = true;
			base.Region = Region.FromHrgn(\u2056.\u00A0(0, 0, base.Width, base.Height, 25, 25));
			this.\u1680();
			this.\u2001();
			this.\u00A0 = new Timer
			{
				Interval = 30
			};
			this.Attr_2.Tick += this.\u2000;
			this.Attr_2.Start();
		}

		// Token: 0x060001CD RID: 461 RVA: 0x000101C4 File Offset: 0x0000E3C4
		private void \u1680()
		{
			this.\u00A0 = new Panel
			{
				Dock = DockStyle.Top,
				Height = 50,
				BackColor = Color.FromArgb(10, 10, 12)
			};
			this.Attr_2.MouseDown += this.\u00A0;
			this.Attr_2.Paint += this.\u00A0;
			this.\u00A0 = new Label
			{
				Text = "◢ ระบบจัดการรหัสกล่อง ECU : คอนโซลควบคุมหลัก ◣",
				ForeColor = Color.White,
				Font = new Font("Impact", 12f),
				Location = new Point(15, 15),
				AutoSize = true,
				BackColor = Color.Transparent
			};
			this.\u00A0 = new Button
			{
				Text = "×",
				Dock = DockStyle.Right,
				Width = 50,
				FlatStyle = FlatStyle.Flat,
				ForeColor = Color.Gray,
				Font = new Font("Arial", 18f, FontStyle.Bold)
			};
			this.Attr_2.FlatAppearance.BorderSize = 0;
			this.Attr_2.Click += this.\u2001;
			this.Attr_2.Controls.Add(this.\u00A0);
			this.Attr_2.Controls.Add(this.\u00A0);
			base.Controls.Add(this.\u00A0);
			this.\u1680 = this.\u00A0("◢ หมวดหมู่ : รถเกียร์ [ MANUAL ]", new Point(25, 65), true);
			this.\u2000 = this.\u00A0("◢ หมวดหมู่ : รถออโต้ [ AUTO ]", new Point(310, 65), false);
			this.Attr_3.Click += this.\u2002;
			this.Form_4.Click += this.\u2003;
			base.Controls.Add(this.\u1680);
			base.Controls.Add(this.\u2000);
			this.\u00A0 = new TextBox
			{
				Location = new Point(620, 65),
				Width = 355,
				BackColor = Color.Black,
				ForeColor = this.\u00A0,
				BorderStyle = BorderStyle.FixedSingle,
				Font = new Font("Consolas", 12f)
			};
			this.Attr_2.TextChanged += this.\u2004;
			base.Controls.Add(this.\u00A0);
			base.Controls.Add(new Label
			{
				Text = "ค้นหาสายธารข้อมูลรหัสพาร์ท",
				ForeColor = Color.FromArgb(120, this.\u00A0),
				Font = new Font("Leelawadee UI", 7f, FontStyle.Bold),
				Location = new Point(620, 50)
			});
			this.\u00A0 = new DataGridView
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
			this.Attr_2.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(15, 15, 20);
			this.Attr_2.ColumnHeadersDefaultCellStyle.ForeColor = Color.Gray;
			this.Attr_2.ColumnHeadersDefaultCellStyle.Font = new Font("Consolas", 9f, FontStyle.Bold);
			this.Attr_2.DefaultCellStyle.BackColor = Color.Black;
			this.Attr_2.DefaultCellStyle.SelectionBackColor = Color.FromArgb(100, 20, 20);
			this.Attr_2.DefaultCellStyle.SelectionForeColor = Color.White;
			this.Attr_2.Columns.Add("PartCode", "รหัสพาร์ท / รุ่นรถ");
			this.Attr_2.Columns.Add("EcmId", "หมายเลข ECM ID");
			this.Attr_2.Columns.Add("StartOffset", "START");
			this.Attr_2.Columns.Add("CksumOffset", "CHKSUM");
			this.Attr_2.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
			this.Attr_2.SelectionChanged += this.\u00A0;
			this.Attr_2.CellPainting += this.\u00A0;
			base.Controls.Add(this.\u00A0);
			this.\u1680 = new Panel
			{
				Location = new Point(650, 115),
				Size = new Size(325, 430),
				BackColor = this.\u2000
			};
			this.Attr_3.Paint += this.\u1680;
			this.\u2000();
			base.Controls.Add(this.\u1680);
			this.\u2004 = new Button
			{
				Text = "◢ บันทึกข้อมูลทั้งหมดลงฐานข้อมูลหลัก ◣",
				Location = new Point(25, 560),
				Size = new Size(950, 60),
				BackColor = Color.FromArgb(10, 10, 15),
				ForeColor = this.\u00A0,
				FlatStyle = FlatStyle.Flat,
				Font = new Font("Impact", 13f)
			};
			this.Form_8.FlatAppearance.BorderSize = 0;
			this.Form_8.Paint += this.\u2000;
			this.Form_8.Click += this.\u1680;
			base.Controls.Add(this.\u2004);
			base.Paint += this.\u2001;
		}

		// Token: 0x060001CE RID: 462 RVA: 0x000107BC File Offset: 0x0000E9BC
		private Button \u00A0(string A_1, Point A_2, bool A_3)
		{
			return new Button
			{
				Text = A_1,
				Location = A_2,
				Size = new Size(270, 40),
				BackColor = (A_3 ? Color.FromArgb(60, 20, 20) : Color.FromArgb(20, 20, 25)),
				ForeColor = (A_3 ? Color.White : Color.Gray),
				FlatStyle = FlatStyle.Flat,
				Font = new Font("Impact", 10f),
				TextAlign = ContentAlignment.MiddleLeft,
				FlatAppearance = 
				{
					BorderSize = 1,
					BorderColor = (A_3 ? this.\u00A0 : this.\u2001)
				}
			};
		}

		// Token: 0x060001CF RID: 463 RVA: 0x00010870 File Offset: 0x0000EA70
		private void \u00A0(Graphics A_1, Rectangle A_2)
		{
			using (new Pen(Color.FromArgb(150, 20, 20), 1f))
			{
				for (int i = 0; i < A_2.Width; i += 40)
				{
					A_1.DrawLine(new Pen(Color.FromArgb(10, 20, 20), 1f), i, 0, i, A_2.Height);
				}
				for (int j = 0; j < A_2.Height; j += 40)
				{
					A_1.DrawLine(new Pen(Color.FromArgb(10, 20, 20), 1f), 0, j, A_2.Width, j);
				}
			}
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x00010924 File Offset: 0x0000EB24
		private void \u00A0(Graphics A_1, Rectangle A_2, string A_3)
		{
			A_1.SmoothingMode = SmoothingMode.AntiAlias;
			using (Pen pen = new Pen(this.\u2001, 1f))
			{
				A_1.DrawRectangle(pen, A_2.X, A_2.Y, A_2.Width - 1, A_2.Height - 1);
			}
			int num = 60;
			using (Pen pen2 = new Pen(this.\u00A0, 3f))
			{
				A_1.DrawLine(pen2, A_2.X + this.\u1680, A_2.Y, A_2.X + this.\u1680 + num, A_2.Y);
				A_1.DrawLine(pen2, A_2.Right - 1, A_2.Y + this.\u1680, A_2.Right - 1, A_2.Y + this.\u1680 + num);
			}
			using (Pen pen3 = new Pen(this.\u00A0, 2f))
			{
				int num2 = 20;
				A_1.DrawLine(pen3, A_2.X, A_2.Y, A_2.X + num2, A_2.Y);
				A_1.DrawLine(pen3, A_2.X, A_2.Y, A_2.X, A_2.Y + num2);
				A_1.DrawLine(pen3, A_2.Right - 1, A_2.Bottom - 1, A_2.Right - 1 - num2, A_2.Bottom - 1);
				A_1.DrawLine(pen3, A_2.Right - 1, A_2.Bottom - 1, A_2.Right - 1, A_2.Bottom - 1 - num2);
			}
			using (Font font = new Font("Consolas", 7f, FontStyle.Bold))
			{
				A_1.DrawString("◢ " + A_3 + " ◣", font, new SolidBrush(Color.FromArgb(150, this.\u00A0)), (float)(A_2.X + 5), (float)(A_2.Y - 12));
			}
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x00010B68 File Offset: 0x0000ED68
		private void \u00A0(object A_1, DataGridViewCellPaintingEventArgs A_2)
		{
			if (A_2.RowIndex >= 0 && A_2.State.HasFlag(DataGridViewElementStates.Selected))
			{
				A_2.PaintBackground(A_2.CellBounds, true);
				using (LinearGradientBrush linearGradientBrush = new LinearGradientBrush(A_2.CellBounds, Color.FromArgb(60, this.\u00A0), Color.Transparent, 0f))
				{
					A_2.Graphics.FillRectangle(linearGradientBrush, A_2.CellBounds);
				}
				A_2.PaintContent(A_2.CellBounds);
				A_2.Handled = true;
			}
			if (A_2.RowIndex == this.Attr_2.Rows.Count - 1 && A_2.ColumnIndex == this.Attr_2.Columns.Count - 1)
			{
				int num = this.\u00A0 - 120;
				if (num > 0 && num < this.Attr_2.Height)
				{
					using (Pen pen = new Pen(Color.FromArgb(180, this.\u00A0), 1f))
					{
						A_2.Graphics.DrawLine(pen, 0, num, this.Attr_2.Width, num);
					}
					using (LinearGradientBrush linearGradientBrush2 = new LinearGradientBrush(new Rectangle(0, num - 15, this.Attr_2.Width, 15), Color.Transparent, Color.FromArgb(50, this.\u00A0), 90f))
					{
						A_2.Graphics.FillRectangle(linearGradientBrush2, 0, num - 15, this.Attr_2.Width, 15);
					}
				}
			}
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x00010D1C File Offset: 0x0000EF1C
		private void \u2000()
		{
			int num = 25;
			this.\u1680 = this.\u00A0("◤ รหัสพาร์ท / รุ่นรถ ◢", ref num);
			this.\u2000 = this.\u00A0("◤ หมายเลขลายเซ็น ECM_ID ◢", ref num);
			this.\u2001 = this.\u00A0("◤ ตำแหน่งเริ่มหน่วยความจำ (HEX) ◢", ref num);
			this.\u2002 = this.\u00A0("◤ ตำแหน่งเช็คซัม (CHECKSUM) ◢", ref num);
			num += 5;
			this.\u2001 = this.\u00A0("◢ เพิ่มข้อมูลใหม่ ◣", num, Color.FromArgb(0, 100, 200));
			this.Attr_5.Click += this.\u2005;
			this.\u2002 = this.\u00A0("◢ บันทึกการแก้ไข ◣", num + 45, Color.FromArgb(35, 35, 40));
			this.Type_6.Click += this.\u2006;
			this.\u2003 = this.\u00A0("◢ ลบข้อมูลนี้ออก ◣", num + 90, Color.FromArgb(80, 0, 0));
			this.Type_7.Click += this.\u2007;
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x00010E20 File Offset: 0x0000F020
		private TextBox \u00A0(string A_1, ref int A_2)
		{
			this.Attr_3.Controls.Add(new Label
			{
				Text = A_1,
				ForeColor = Color.Gray,
				Location = new Point(20, A_2),
				Font = new Font("Impact", 8f),
				AutoSize = true
			});
			TextBox textBox = new TextBox
			{
				Location = new Point(20, A_2 + 18),
				Width = 285,
				BackColor = Color.Black,
				ForeColor = Color.White,
				BorderStyle = BorderStyle.FixedSingle,
				Font = new Font("Consolas", 11f)
			};
			this.Attr_3.Controls.Add(textBox);
			A_2 += 58;
			return textBox;
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x00010EF0 File Offset: 0x0000F0F0
		private Button \u00A0(string A_1, int A_2, Color A_3)
		{
			Button button = new Button
			{
				Text = A_1,
				Location = new Point(20, A_2),
				Size = new Size(285, 38),
				BackColor = A_3,
				ForeColor = Color.White,
				FlatStyle = FlatStyle.Flat,
				Font = new Font("Impact", 10f)
			};
			button.FlatAppearance.BorderSize = 0;
			this.Attr_3.Controls.Add(button);
			return button;
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x00010F78 File Offset: 0x0000F178
		private void \u00A0(string A_1)
		{
			this.\u00A0 = A_1;
			this.Attr_3.BackColor = ((this.\u00A0 == "รถเกียร์") ? Color.FromArgb(60, 20, 20) : Color.FromArgb(20, 20, 25));
			this.Attr_3.ForeColor = ((this.\u00A0 == "รถเกียร์") ? Color.White : Color.Gray);
			this.Attr_3.FlatAppearance.BorderColor = ((this.\u00A0 == "รถเกียร์") ? this.\u00A0 : this.\u2001);
			this.Form_4.BackColor = ((this.\u00A0 == "รถออโต้") ? Color.FromArgb(60, 20, 20) : Color.FromArgb(20, 20, 25));
			this.Form_4.ForeColor = ((this.\u00A0 == "รถออโต้") ? Color.White : Color.Gray);
			this.Form_4.FlatAppearance.BorderColor = ((this.\u00A0 == "รถออโต้") ? this.\u00A0 : this.\u2001);
			this.\u2001();
			Console.Beep(1500, 50);
			base.Invalidate();
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x000110C0 File Offset: 0x0000F2C0
		private void \u2001()
		{
			this.Attr_2.Rows.Clear();
			foreach (\u206D u206D in \u206E.\u00A0().Where(new Func<Type_5E, bool>(this.\u00A0)).ToList<Type_5E>())
			{
				this.Attr_2.Rows.Add(new object[]
				{
					u206D.\u00A0(),
					u206D.\u1680(),
					u206D.\u2000(),
					u206D.\u2001()
				});
			}
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x0001116C File Offset: 0x0000F36C
		private void \u1680(string A_1)
		{
			\u2056.\u00A0 u00A = new \u2056.\u00A0();
			u00A.\u00A0 = this;
			u00A.\u00A0 = A_1;
			this.Attr_2.Rows.Clear();
			foreach (\u206D u206D in \u206E.\u00A0().Where(new Func<Type_5E, bool>(u00A.\u00A0)).ToList<Type_5E>())
			{
				this.Attr_2.Rows.Add(new object[]
				{
					u206D.\u00A0(),
					u206D.\u1680(),
					u206D.\u2000(),
					u206D.\u2001()
				});
			}
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x0001122C File Offset: 0x0000F42C
		private void \u00A0(object A_1, EventArgs A_2)
		{
			if (this.Attr_2.SelectedRows.Count > 0)
			{
				\u2056.\u1680 u = new \u2056.\u1680();
				u.\u00A0 = this.Attr_2.SelectedRows[0].Cells[0].Value.ToString();
				\u206D u206D = \u206E.\u00A0().FirstOrDefault(new Func<Type_5E, bool>(u.\u00A0));
				if (u206D != null)
				{
					this.Attr_3.Text = u206D.\u00A0();
					this.Form_4.Text = u206D.\u1680();
					this.Attr_5.Text = u206D.\u2000();
					this.Type_6.Text = u206D.\u2001();
				}
			}
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x000112E0 File Offset: 0x0000F4E0
		private void \u2002()
		{
			if (string.IsNullOrWhiteSpace(this.Attr_3.Text))
			{
				return;
			}
			List<Type_5E> list = \u206E.\u00A0();
			\u206D u206D = new Type_5E();
			u206D.\u00A0(this.Attr_3.Text);
			u206D.\u1680(this.Form_4.Text);
			u206D.\u2000(this.Attr_5.Text);
			u206D.\u2001(this.Type_6.Text);
			u206D.\u00A0((this.\u00A0 == "รถเกียร์") ? \u206C.\u00A0 : \u206C.\u1680);
			list.Add(u206D);
			this.\u2001();
			Console.Beep(1200, 100);
		}

		// Token: 0x060001DA RID: 474 RVA: 0x00011384 File Offset: 0x0000F584
		private void \u2003()
		{
			\u206D u206D = \u206E.\u00A0().FirstOrDefault(new Func<Type_5E, bool>(this.\u1680));
			if (u206D != null)
			{
				u206D.\u1680(this.Form_4.Text);
				u206D.\u2000(this.Attr_5.Text);
				u206D.\u2001(this.Type_6.Text);
				this.\u2001();
				Console.Beep(1500, 100);
			}
		}

		// Token: 0x060001DB RID: 475 RVA: 0x000113F0 File Offset: 0x0000F5F0
		private void \u2004()
		{
			if (this.Attr_2.SelectedRows.Count == 0)
			{
				return;
			}
			if (MessageBox.Show("◢ ยืนยันการลบไฟล์ชุดนี้ออกจากสารบบหรือไม่? ◣", "MASTER_CORE_SECURITY", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
			{
				\u206E.\u00A0().RemoveAll(new Predicate<Type_5E>(this.\u2000));
				this.\u2001();
				Console.Beep(800, 200);
			}
		}

		// Token: 0x060001DC RID: 476 RVA: 0x00011454 File Offset: 0x0000F654
		private void \u1680(object A_1, EventArgs A_2)
		{
			try
			{
				\u206E.\u2000();
				\u2062.\u00A0("บันทึกรหัสกล่องลงโปรแกรม", "บันทึกรหัสกล่องลงโปรแกรมสำเร็จ", \u2061.\u00A0);
				Console.Beep(2000, 150);
			}
			catch (Exception ex)
			{
				MessageBox.Show("ล้มเหลว: " + ex.Message);
			}
		}

		// Token: 0x060001DD RID: 477 RVA: 0x000114B0 File Offset: 0x0000F6B0
		[CompilerGenerated]
		private void \u2000(object A_1, EventArgs A_2)
		{
			this.\u00A0 += 5;
			if (this.\u00A0 > 550)
			{
				this.\u00A0 = 0;
			}
			this.\u1680 = (this.\u1680 + 3) % 200;
			if (this.\u00A0)
			{
				this.\u00A0 += 0.04f;
				if (this.\u00A0 >= 1f)
				{
					this.\u00A0 = false;
				}
			}
			else
			{
				this.\u00A0 -= 0.04f;
				if (this.\u00A0 <= 0.4f)
				{
					this.\u00A0 = true;
				}
			}
			base.Invalidate(false);
			this.Form_8.Invalidate();
			this.Attr_2.Invalidate();
		}

		// Token: 0x060001DE RID: 478 RVA: 0x00011566 File Offset: 0x0000F766
		[CompilerGenerated]
		private void \u00A0(object A_1, MouseEventArgs A_2)
		{
			\u2056.\u00A0();
			\u2056.\u00A0(base.Handle, 274, 61458, 0);
		}

		// Token: 0x060001DF RID: 479 RVA: 0x00011588 File Offset: 0x0000F788
		[CompilerGenerated]
		private void \u00A0(object A_1, PaintEventArgs A_2)
		{
			A_2.Graphics.DrawLine(new Pen(this.\u00A0, 2f), 0, this.Attr_2.Height - 1, this.Attr_2.Width, this.Attr_2.Height - 1);
			using (LinearGradientBrush linearGradientBrush = new LinearGradientBrush(new Rectangle(0, this.Attr_2.Height - 6, this.Attr_2.Width, 6), Color.Transparent, Color.FromArgb(60, this.\u00A0), 90f))
			{
				A_2.Graphics.FillRectangle(linearGradientBrush, 0, this.Attr_2.Height - 6, this.Attr_2.Width, 6);
			}
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x000021C8 File Offset: 0x000003C8
		[CompilerGenerated]
		private void \u2001(object A_1, EventArgs A_2)
		{
			base.Close();
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x00011654 File Offset: 0x0000F854
		[CompilerGenerated]
		private void \u2002(object A_1, EventArgs A_2)
		{
			this.\u00A0("รถเกียร์");
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x00011661 File Offset: 0x0000F861
		[CompilerGenerated]
		private void \u2003(object A_1, EventArgs A_2)
		{
			this.\u00A0("รถออโต้");
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x0001166E File Offset: 0x0000F86E
		[CompilerGenerated]
		private void \u2004(object A_1, EventArgs A_2)
		{
			this.\u1680(this.Attr_2.Text);
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x00011681 File Offset: 0x0000F881
		[CompilerGenerated]
		private void \u1680(object A_1, PaintEventArgs A_2)
		{
			this.\u00A0(A_2.Graphics, this.Attr_3.ClientRectangle, "ระบบแก้ไขข้อมูลหลัก");
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x000116A0 File Offset: 0x0000F8A0
		[CompilerGenerated]
		private void \u2000(object A_1, PaintEventArgs A_2)
		{
			using (Pen pen = new Pen(Color.FromArgb((int)(this.\u00A0 * 255f), this.\u00A0), 2f))
			{
				A_2.Graphics.DrawRectangle(pen, 0, 0, this.Form_8.Width - 1, this.Form_8.Height - 1);
			}
			using (Pen pen2 = new Pen(Color.FromArgb(100, this.\u00A0), 1f))
			{
				A_2.Graphics.DrawLine(pen2, 10, 10, 30, 10);
				A_2.Graphics.DrawLine(pen2, this.Form_8.Width - 30, 10, this.Form_8.Width - 10, 10);
			}
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x00011784 File Offset: 0x0000F984
		[CompilerGenerated]
		private void \u2001(object A_1, PaintEventArgs A_2)
		{
			A_2.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
			this.\u00A0(A_2.Graphics, base.ClientRectangle);
			this.\u00A0(A_2.Graphics, new Rectangle(20, 110, 620, 440), (this.\u00A0 == "รถเกียร์") ? "ระบบย่อยรถเกียร์" : "ระบบย่อยรถออโต้");
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x000117EC File Offset: 0x0000F9EC
		[CompilerGenerated]
		private void \u2005(object A_1, EventArgs A_2)
		{
			this.\u2002();
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x000117F4 File Offset: 0x0000F9F4
		[CompilerGenerated]
		private void \u2006(object A_1, EventArgs A_2)
		{
			this.\u2003();
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x000117FC File Offset: 0x0000F9FC
		[CompilerGenerated]
		private void \u2007(object A_1, EventArgs A_2)
		{
			this.\u2004();
		}

		// Token: 0x060001EA RID: 490 RVA: 0x00011804 File Offset: 0x0000FA04
		[CompilerGenerated]
		private bool \u00A0(\u206D A_1)
		{
			return (this.\u00A0 == "รถเกียร์" && A_1.\u2002() == \u206C.\u00A0) || (this.\u00A0 == "รถออโต้" && A_1.\u2002() == \u206C.\u1680);
		}

		// Token: 0x060001EB RID: 491 RVA: 0x0001183F File Offset: 0x0000FA3F
		[CompilerGenerated]
		private bool \u1680(\u206D A_1)
		{
			return A_1.\u00A0() == this.Attr_3.Text;
		}

		// Token: 0x060001EC RID: 492 RVA: 0x00011857 File Offset: 0x0000FA57
		[CompilerGenerated]
		private bool \u2000(\u206D A_1)
		{
			return A_1.\u00A0() == this.Attr_2.SelectedRows[0].Cells[0].Value.ToString();
		}

		// Token: 0x0400013E RID: 318
		private Color \u00A0 = Color.FromArgb(220, 20, 20);

		// Token: 0x0400013F RID: 319
		private Color \u1680 = Color.FromArgb(8, 8, 10);

		// Token: 0x04000140 RID: 320
		private Color \u2000 = Color.FromArgb(15, 15, 18);

		// Token: 0x04000141 RID: 321
		private Color \u2001 = Color.FromArgb(40, 40, 45);

		// Token: 0x04000142 RID: 322
		private Color \u2002 = Color.FromArgb(255, 50, 50);

		// Token: 0x04000143 RID: 323
		private Panel \u00A0;

		// Token: 0x04000144 RID: 324
		private Label \u00A0;

		// Token: 0x04000145 RID: 325
		private Button \u00A0;

		// Token: 0x04000146 RID: 326
		private DataGridView \u00A0;

		// Token: 0x04000147 RID: 327
		private Panel \u1680;

		// Token: 0x04000148 RID: 328
		private TextBox \u00A0;

		// Token: 0x04000149 RID: 329
		private Button \u1680;

		// Token: 0x0400014A RID: 330
		private Button \u2000;

		// Token: 0x0400014B RID: 331
		private string \u00A0 = "รถเกียร์";

		// Token: 0x0400014C RID: 332
		private TextBox \u1680;

		// Token: 0x0400014D RID: 333
		private TextBox \u2000;

		// Token: 0x0400014E RID: 334
		private TextBox \u2001;

		// Token: 0x0400014F RID: 335
		private TextBox \u2002;

		// Token: 0x04000150 RID: 336
		private Button \u2001;

		// Token: 0x04000151 RID: 337
		private Button \u2002;

		// Token: 0x04000152 RID: 338
		private Button \u2003;

		// Token: 0x04000153 RID: 339
		private Button \u2004;

		// Token: 0x04000154 RID: 340
		private Timer \u00A0;

		// Token: 0x04000155 RID: 341
		private int \u00A0;

		// Token: 0x04000156 RID: 342
		private float \u00A0 = 1f;

		// Token: 0x04000157 RID: 343
		private bool \u00A0;

		// Token: 0x04000158 RID: 344
		private int \u1680;

		// Token: 0x04000159 RID: 345
		private float \u1680 = 0.1f;

		// Token: 0x020000A9 RID: 169
		[CompilerGenerated]
		private sealed class Attr_2
		{
			// Token: 0x060001EE RID: 494 RVA: 0x0001188C File Offset: 0x0000FA8C
			internal bool \u00A0(\u206D A_1)
			{
				return ((this.Attr_2.\u00A0 == "รถเกียร์" && A_1.\u2002() == \u206C.\u00A0) || (this.Attr_2.\u00A0 == "รถออโต้" && A_1.\u2002() == \u206C.\u1680)) && (A_1.\u00A0().IndexOf(this.\u00A0, StringComparison.OrdinalIgnoreCase) >= 0 || A_1.\u1680().IndexOf(this.\u00A0, StringComparison.OrdinalIgnoreCase) >= 0);
			}

			// Token: 0x0400015A RID: 346
			public \u2056 \u00A0;

			// Token: 0x0400015B RID: 347
			public string \u00A0;
		}

		// Token: 0x020000AA RID: 170
		[CompilerGenerated]
		private sealed class Attr_3
		{
			// Token: 0x060001F0 RID: 496 RVA: 0x00011909 File Offset: 0x0000FB09
			internal bool \u00A0(\u206D A_1)
			{
				return A_1.\u00A0() == this.\u00A0;
			}

			// Token: 0x0400015C RID: 348
			public string \u00A0;
		}
	}
}
