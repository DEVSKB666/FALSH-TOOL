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
	// Token: 0x020000AB RID: 171
	public partial class Type_4D : Form
	{
		// Token: 0x060001F1 RID: 497
		[DllImport("user32.dll", EntryPoint = "ReleaseCapture")]
		public static extern bool \u00A0();

		// Token: 0x060001F2 RID: 498
		[DllImport("user32.dll", EntryPoint = "SendMessage")]
		public static extern int \u00A0(IntPtr, int, int, int);

		// Token: 0x060001F3 RID: 499 RVA: 0x0001191C File Offset: 0x0000FB1C
		public Type_4D()
		{
			base.Size = new Size(820, 560);
			base.FormBorderStyle = FormBorderStyle.None;
			this.BackColor = this.\u00A0;
			base.StartPosition = FormStartPosition.CenterParent;
			this.DoubleBuffered = true;
			this.\u1680();
			this.\u00A0 = new Timer
			{
				Interval = 25
			};
			this.Attr_2.Tick += this.\u00A0;
			this.Attr_2.Start();
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x00011A19 File Offset: 0x0000FC19
		protected override void OnFormClosed(FormClosedEventArgs A_1)
		{
			Timer u00A = this.\u00A0;
			if (u00A != null)
			{
				u00A.Stop();
			}
			Timer u00A2 = this.\u00A0;
			if (u00A2 != null)
			{
				u00A2.Dispose();
			}
			base.OnFormClosed(A_1);
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x00011A44 File Offset: 0x0000FC44
		private void \u1680()
		{
			\u2057.\u2000 u = new \u2057.\u2000();
			u.\u00A0 = this;
			Panel panel = new Panel
			{
				Dock = DockStyle.Top,
				Height = 40,
				BackColor = Color.Transparent
			};
			panel.MouseDown += u.\u00A0;
			Label value = new Label
			{
				Text = "MZA",
				ForeColor = Color.White,
				BackColor = this.\u2000,
				Font = new Font("Segoe UI Black", 12f, FontStyle.Italic),
				AutoSize = false,
				Size = new Size(50, 25),
				Location = new Point(15, 7),
				TextAlign = ContentAlignment.MiddleCenter
			};
			Label value2 = new Label
			{
				Text = "ระบบเซนซิทีฟคุมการยิงหอบ (GHOST CAM SYSTEM)",
				ForeColor = Color.LightGray,
				Font = new Font("Segoe UI", 11.5f, FontStyle.Bold),
				AutoSize = true,
				Location = new Point(75, 9),
				BackColor = Color.Transparent
			};
			u.\u00A0 = new Button
			{
				Text = "✖",
				Size = new Size(40, 40),
				Dock = DockStyle.Right,
				FlatStyle = FlatStyle.Flat,
				ForeColor = this.\u2002,
				Font = new Font("Segoe UI", 12f, FontStyle.Bold),
				Cursor = Cursors.Hand
			};
			u.Attr_2.FlatAppearance.BorderSize = 0;
			u.Attr_2.FlatAppearance.MouseOverBackColor = this.\u2000;
			u.Attr_2.MouseEnter += u.\u00A0;
			u.Attr_2.MouseLeave += u.\u1680;
			u.Attr_2.Click += u.\u2000;
			panel.Controls.Add(value);
			panel.Controls.Add(value2);
			panel.Controls.Add(u.\u00A0);
			base.Controls.Add(panel);
			u.\u00A0 = new Panel
			{
				Location = new Point(20, 60),
				Size = new Size(240, 70),
				BackColor = this.\u1680
			};
			u.Attr_2.Paint += u.\u00A0;
			Label value3 = new Label
			{
				Text = "🤖 MZABot_Ai // CORE",
				ForeColor = this.\u2000,
				Font = new Font("Consolas", 8.5f, FontStyle.Bold),
				AutoSize = true,
				Location = new Point(15, 8)
			};
			Label value4 = new Label
			{
				Name = "lblBotMsg",
				Text = "กำลังรอรับคำสั่งจูน...",
				ForeColor = this.\u2003,
				Font = new Font("Segoe UI", 11.5f, FontStyle.Bold),
				AutoSize = true,
				Location = new Point(15, 30)
			};
			u.Attr_2.Controls.Add(value3);
			u.Attr_2.Controls.Add(value4);
			base.Controls.Add(u.\u00A0);
			u.\u1680 = new Panel
			{
				Location = new Point(280, 55),
				Size = new Size(250, 40),
				BackColor = this.\u1680
			};
			u.Attr_3.Paint += u.\u1680;
			u.\u00A0 = new TextBox
			{
				Location = new Point(10, 10),
				Width = 155,
				BackColor = this.\u1680,
				ForeColor = this.\u2002,
				Font = new Font("Segoe UI", 9.5f),
				BorderStyle = BorderStyle.None,
				ReadOnly = true,
				Text = "กรุณาเลือกไฟล์ .BIN"
			};
			\u2057.\u1680 u2 = new \u2057.\u1680
			{
				Text = "ค้นหาไฟล์",
				Location = new Point(175, 4),
				Size = new Size(70, 32),
				Font = new Font("Segoe UI", 8.5f, FontStyle.Bold)
			};
			u2.Click += u.\u2001;
			u.Attr_3.Controls.Add(u.\u00A0);
			u.Attr_3.Controls.Add(u2);
			base.Controls.Add(u.\u1680);
			u.\u2000 = new Panel
			{
				Location = new Point(280, 105),
				Size = new Size(250, 40),
				BackColor = this.\u1680
			};
			u.Form_4.Paint += u.\u2000;
			ComboBox comboBox = new ComboBox
			{
				Location = new Point(5, 6),
				Width = 230,
				Font = new Font("Segoe UI", 12f, FontStyle.Bold),
				DropDownStyle = ComboBoxStyle.DropDownList,
				FlatStyle = FlatStyle.Flat,
				BackColor = this.\u1680,
				ForeColor = this.\u2003
			};
			ComboBox.ObjectCollection items = comboBox.Items;
			object[] items2 = new string[]
			{
				"รออัพเดท",
				"รออัพเดท",
				"รออัพเดท",
				"รออัพเดท"
			};
			items.AddRange(items2);
			if (comboBox.Items.Count > 0)
			{
				comboBox.SelectedIndex = 0;
			}
			u.Form_4.Controls.Add(comboBox);
			base.Controls.Add(u.\u2000);
			u.\u00A0 = new \u2057.\u1680
			{
				Text = "EXECUTE / บันทึกยิงหอบ",
				Location = new Point(550, 55),
				Size = new Size(250, 90),
				BackColor = this.\u00A0
			};
			this.\u1680 = new Timer
			{
				Interval = 45
			};
			this.Attr_3.Tick += u.\u2002;
			u.Attr_2.Click += u.\u2003;
			base.Controls.Add(u.\u00A0);
			Label value5 = new Label
			{
				Text = "[ กำหนดองศาลิ้นปีกผีเสื้อ / TPS ]",
				ForeColor = this.\u2002,
				Font = new Font("Segoe UI", 10.5f, FontStyle.Bold),
				AutoSize = true,
				Location = new Point(20, 155)
			};
			base.Controls.Add(value5);
			string[] array = new string[]
			{
				"0%",
				"3%",
				"5%",
				"8%",
				"12%"
			};
			for (int i = 0; i < array.Length; i++)
			{
				\u2057.\u00A0 u00A = new \u2057.\u00A0
				{
					Text = "TPS " + array[i],
					Location = new Point(20 + i * 155, 185),
					Size = new Size(130, 45)
				};
				if (i == 1)
				{
					u00A.Checked = true;
				}
				base.Controls.Add(u00A);
			}
			Label value6 = new Label
			{
				Text = "[ ล็อครอบเดินเบายิงหอบ / RPM ]",
				ForeColor = this.\u2002,
				Font = new Font("Segoe UI", 10.5f, FontStyle.Bold),
				AutoSize = true,
				Location = new Point(20, 255)
			};
			base.Controls.Add(value6);
			string[] array2 = new string[]
			{
				"1800",
				"2000",
				"2500",
				"3000",
				"3500"
			};
			for (int j = 0; j < array2.Length; j++)
			{
				\u2057.\u00A0 u00A2 = new \u2057.\u00A0
				{
					Text = array2[j] + " RPM",
					Location = new Point(20, 285 + j * 50),
					Size = new Size(250, 40)
				};
				if (j == 4)
				{
					u00A2.Checked = true;
				}
				base.Controls.Add(u00A2);
			}
			Label value7 = new Label
			{
				Text = "[ เลือกระดับความดุดัน / GHOST CAM ]",
				ForeColor = this.\u2002,
				Font = new Font("Segoe UI", 10.5f, FontStyle.Bold),
				AutoSize = true,
				Location = new Point(300, 255)
			};
			base.Controls.Add(value7);
			string[] array3 = new string[]
			{
				"หอบระดับ 1",
				"หอบระดับ 2",
				"หอบระดับ 3",
				"หอบระดับ 4",
				"หอบระดับ 5"
			};
			for (int k = 0; k < array3.Length; k++)
			{
				\u2057.\u00A0 u00A3 = new \u2057.\u00A0
				{
					Text = array3[k],
					Location = new Point(300 + k * 90, 285 + k * 50),
					Size = new Size(130, 40)
				};
				if (k == 2)
				{
					u00A3.Checked = true;
				}
				base.Controls.Add(u00A3);
			}
			Label value8 = new Label
			{
				Text = "MZA TUNER CORE // V1.0.0.X",
				ForeColor = this.\u2000,
				Font = new Font("Consolas", 8f),
				AutoSize = true,
				Location = new Point(15, 535)
			};
			base.Controls.Add(value8);
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x000123CC File Offset: 0x000105CC
		protected override void OnPaint(PaintEventArgs A_1)
		{
			base.OnPaint(A_1);
			A_1.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
			using (Pen pen = new Pen(this.\u2000, 2f))
			{
				A_1.Graphics.DrawRectangle(pen, 1, 1, base.Width - 3, base.Height - 3);
			}
			using (Pen pen2 = new Pen(this.\u2001, 1f))
			{
				A_1.Graphics.DrawRectangle(pen2, 6, 6, base.Width - 13, base.Height - 13);
				A_1.Graphics.FillRectangle(new SolidBrush(this.\u2003), 6, 6, 15, 3);
				A_1.Graphics.FillRectangle(new SolidBrush(this.\u2003), 6, 6, 3, 15);
				A_1.Graphics.FillRectangle(new SolidBrush(this.\u2003), base.Width - 21, 6, 15, 3);
				A_1.Graphics.FillRectangle(new SolidBrush(this.\u2003), base.Width - 9, 6, 3, 15);
				A_1.Graphics.FillRectangle(new SolidBrush(this.\u2003), 6, base.Height - 9, 15, 3);
				A_1.Graphics.FillRectangle(new SolidBrush(this.\u2003), 6, base.Height - 21, 3, 15);
			}
			using (Pen pen3 = new Pen(Color.FromArgb(15, 255, 255, 255), 1f))
			{
				for (int i = 0; i < base.Width; i += 40)
				{
					A_1.Graphics.DrawLine(pen3, i, 45, i, base.Height);
				}
				for (int j = 45; j < base.Height; j += 40)
				{
					A_1.Graphics.DrawLine(pen3, 0, j, base.Width, j);
				}
			}
			using (Font font = new Font("Consolas", 7f))
			{
				using (SolidBrush solidBrush = new SolidBrush(Color.FromArgb(20, this.\u2000)))
				{
					A_1.Graphics.DrawString(string.Format("0x{0:X4}", this.Attr_2.Next(8000, 9999)), font, solidBrush, (float)(base.Width - 80), (float)(base.Height - 40));
					A_1.Graphics.DrawString(string.Format("MEM_{0}", this.Attr_2.Next(10, 99)), font, solidBrush, 300f, 200f);
					A_1.Graphics.DrawString("[TARGET : LOCKED]", font, solidBrush, 280f, 520f);
				}
			}
			if (this.\u00A0 >= 0 && this.\u00A0 <= base.Height)
			{
				using (LinearGradientBrush linearGradientBrush = new LinearGradientBrush(new Rectangle(0, this.\u00A0 - 20, base.Width, 20), Color.Transparent, Color.FromArgb(70, this.\u2000), 90f))
				{
					A_1.Graphics.FillRectangle(linearGradientBrush, new Rectangle(0, this.\u00A0 - 20, base.Width, 20));
				}
				A_1.Graphics.DrawLine(new Pen(Color.FromArgb(150, this.\u2000), 1f), 0, this.\u00A0, base.Width, this.\u00A0);
			}
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x0001278C File Offset: 0x0001098C
		[CompilerGenerated]
		private void \u00A0(object A_1, EventArgs A_2)
		{
			this.\u00A0 += 4;
			if (this.\u00A0 > base.Height + 50)
			{
				this.\u00A0 = -50;
			}
			base.Invalidate(new Rectangle(0, this.\u00A0 - 30, base.Width, 30));
		}

		// Token: 0x0400015D RID: 349
		private Color \u00A0 = Color.FromArgb(12, 12, 15);

		// Token: 0x0400015E RID: 350
		private Color \u1680 = Color.FromArgb(20, 20, 24);

		// Token: 0x0400015F RID: 351
		private Color \u2000 = Color.FromArgb(255, 20, 40);

		// Token: 0x04000160 RID: 352
		private Color \u2001 = Color.FromArgb(180, 10, 20);

		// Token: 0x04000161 RID: 353
		private Color \u2002 = Color.FromArgb(100, 100, 110);

		// Token: 0x04000162 RID: 354
		private Color \u2003 = Color.White;

		// Token: 0x04000163 RID: 355
		private Timer \u00A0;

		// Token: 0x04000164 RID: 356
		private Timer \u1680;

		// Token: 0x04000165 RID: 357
		private int \u00A0 = -50;

		// Token: 0x04000166 RID: 358
		private int \u1680;

		// Token: 0x04000167 RID: 359
		private Random \u00A0 = new Random();

		// Token: 0x020000AC RID: 172
		private class Attr_2 : RadioButton
		{
			// Token: 0x060001F8 RID: 504 RVA: 0x000127DD File Offset: 0x000109DD
			public \u00A0()
			{
				this.ForeColor = Color.LightGray;
				this.Font = new Font("Segoe UI", 10.5f, FontStyle.Bold);
				this.Cursor = Cursors.Hand;
				this.DoubleBuffered = true;
			}

			// Token: 0x060001F9 RID: 505 RVA: 0x00012818 File Offset: 0x00010A18
			protected override void OnMouseEnter(EventArgs A_1)
			{
				this.\u00A0 = true;
				base.Invalidate();
				base.OnMouseEnter(A_1);
			}

			// Token: 0x060001FA RID: 506 RVA: 0x0001282E File Offset: 0x00010A2E
			protected override void OnMouseLeave(EventArgs A_1)
			{
				this.\u00A0 = false;
				base.Invalidate();
				base.OnMouseLeave(A_1);
			}

			// Token: 0x060001FB RID: 507 RVA: 0x00012844 File Offset: 0x00010A44
			protected override void OnPaint(PaintEventArgs A_1)
			{
				A_1.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
				A_1.Graphics.Clear(base.Parent.BackColor);
				new Rectangle(0, 0, base.Width - 1, base.Height - 1);
				GraphicsPath graphicsPath = new GraphicsPath();
				graphicsPath.AddLine(0, 0, base.Width - 10, 0);
				graphicsPath.AddLine(base.Width - 10, 0, base.Width, 10);
				graphicsPath.AddLine(base.Width, 10, base.Width, base.Height);
				graphicsPath.AddLine(base.Width, base.Height, 10, base.Height);
				graphicsPath.AddLine(10, base.Height, 0, base.Height - 10);
				graphicsPath.CloseFigure();
				Color color = base.Checked ? Color.FromArgb(220, 20, 30) : (this.\u00A0 ? Color.FromArgb(50, 50, 50) : Color.FromArgb(20, 20, 22));
				Color color2 = base.Checked ? Color.White : (this.\u00A0 ? Color.FromArgb(255, 60, 70) : Color.FromArgb(100, 100, 110));
				Color foreColor = base.Checked ? Color.White : Color.LightGray;
				using (SolidBrush solidBrush = new SolidBrush(color))
				{
					A_1.Graphics.FillPath(solidBrush, graphicsPath);
				}
				using (Pen pen = new Pen(color2, base.Checked ? 2f : 1f))
				{
					A_1.Graphics.DrawPath(pen, graphicsPath);
				}
				Rectangle rect = new Rectangle(12, (base.Height - 10) / 2, 10, 10);
				if (base.Checked)
				{
					A_1.Graphics.FillRectangle(Brushes.White, rect);
				}
				else
				{
					A_1.Graphics.DrawRectangle(new Pen(Color.Gray, 1f), rect);
				}
				TextRenderer.DrawText(A_1.Graphics, this.Text, this.Font, new Point(32, (base.Height - this.Font.Height) / 2), foreColor);
			}

			// Token: 0x04000168 RID: 360
			private bool \u00A0;
		}

		// Token: 0x020000AD RID: 173
		private class Attr_3 : Button
		{
			// Token: 0x060001FC RID: 508 RVA: 0x00012A88 File Offset: 0x00010C88
			public \u1680()
			{
				this.DoubleBuffered = true;
				this.Cursor = Cursors.Hand;
				this.Font = new Font("Segoe UI Black", 12f);
			}

			// Token: 0x060001FD RID: 509 RVA: 0x00012AB7 File Offset: 0x00010CB7
			protected override void OnMouseEnter(EventArgs A_1)
			{
				this.\u00A0 = true;
				base.Invalidate();
				base.OnMouseEnter(A_1);
			}

			// Token: 0x060001FE RID: 510 RVA: 0x00012ACD File Offset: 0x00010CCD
			protected override void OnMouseLeave(EventArgs A_1)
			{
				this.\u00A0 = false;
				base.Invalidate();
				base.OnMouseLeave(A_1);
			}

			// Token: 0x060001FF RID: 511 RVA: 0x00012AE4 File Offset: 0x00010CE4
			protected override void OnPaint(PaintEventArgs A_1)
			{
				A_1.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
				A_1.Graphics.Clear(base.Parent.BackColor);
				Rectangle rectangle = new Rectangle(0, 0, base.Width - 1, base.Height - 1);
				GraphicsPath graphicsPath = new GraphicsPath();
				graphicsPath.AddLine(15, 0, base.Width, 0);
				graphicsPath.AddLine(base.Width, 0, base.Width, base.Height - 15);
				graphicsPath.AddLine(base.Width - 15, base.Height, 0, base.Height);
				graphicsPath.AddLine(0, base.Height, 0, 15);
				graphicsPath.CloseFigure();
				if (this.\u00A0)
				{
					using (HatchBrush hatchBrush = new HatchBrush(HatchStyle.ForwardDiagonal, Color.FromArgb(180, 20, 30), Color.FromArgb(255, 20, 40)))
					{
						A_1.Graphics.FillPath(hatchBrush, graphicsPath);
						goto IL_127;
					}
				}
				using (LinearGradientBrush linearGradientBrush = new LinearGradientBrush(rectangle, Color.FromArgb(255, 20, 40), Color.FromArgb(180, 5, 10), 45f))
				{
					A_1.Graphics.FillPath(linearGradientBrush, graphicsPath);
				}
				IL_127:
				A_1.Graphics.DrawPath(new Pen(Color.White, 2f), graphicsPath);
				StringFormat format = new StringFormat
				{
					Alignment = StringAlignment.Center,
					LineAlignment = StringAlignment.Center
				};
				A_1.Graphics.DrawString(this.Text, this.Font, Brushes.White, rectangle, format);
			}

			// Token: 0x04000169 RID: 361
			private bool \u00A0;
		}

		// Token: 0x020000AE RID: 174
		[CompilerGenerated]
		private sealed class Form_4
		{
			// Token: 0x06000201 RID: 513 RVA: 0x00012C88 File Offset: 0x00010E88
			internal void \u00A0(object A_1, MouseEventArgs A_2)
			{
				\u2057.\u00A0();
				\u2057.\u00A0(this.Attr_2.Handle, 274, 61458, 0);
			}

			// Token: 0x06000202 RID: 514 RVA: 0x00012CAC File Offset: 0x00010EAC
			internal void \u00A0(object A_1, EventArgs A_2)
			{
				this.Attr_2.ForeColor = Color.White;
			}

			// Token: 0x06000203 RID: 515 RVA: 0x00012CBE File Offset: 0x00010EBE
			internal void \u1680(object A_1, EventArgs A_2)
			{
				this.Attr_2.ForeColor = this.Attr_2.\u2002;
			}

			// Token: 0x06000204 RID: 516 RVA: 0x00012CD6 File Offset: 0x00010ED6
			internal void \u2000(object A_1, EventArgs A_2)
			{
				this.Attr_2.Close();
			}

			// Token: 0x06000205 RID: 517 RVA: 0x00012CE4 File Offset: 0x00010EE4
			internal void \u00A0(object A_1, PaintEventArgs A_2)
			{
				A_2.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
				A_2.Graphics.DrawRectangle(new Pen(this.Attr_2.\u2001, 1.5f), 0, 0, this.Attr_2.Width - 1, this.Attr_2.Height - 1);
				A_2.Graphics.FillRectangle(new SolidBrush(this.Attr_2.\u2000), 0, 0, 5, this.Attr_2.Height);
			}

			// Token: 0x06000206 RID: 518 RVA: 0x00012D62 File Offset: 0x00010F62
			internal void \u1680(object A_1, PaintEventArgs A_2)
			{
				A_2.Graphics.DrawRectangle(new Pen(this.Attr_2.\u2002, 1f), 0, 0, this.Attr_3.Width - 1, this.Attr_3.Height - 1);
			}

			// Token: 0x06000207 RID: 519 RVA: 0x00012DA0 File Offset: 0x00010FA0
			internal void \u2001(object A_1, EventArgs A_2)
			{
				using (OpenFileDialog openFileDialog = new OpenFileDialog
				{
					Filter = "BIN Files (*.bin)|*.bin|All Files (*.*)|*.*",
					Title = "โหลดไฟล์ .bin"
				})
				{
					if (openFileDialog.ShowDialog() == DialogResult.OK)
					{
						this.Attr_2.Text = Path.GetFileName(openFileDialog.FileName);
						this.Attr_2.Tag = openFileDialog.FileName;
						this.Attr_2.ForeColor = this.Attr_2.\u2003;
					}
				}
			}

			// Token: 0x06000208 RID: 520 RVA: 0x00012E2C File Offset: 0x0001102C
			internal void \u2000(object A_1, PaintEventArgs A_2)
			{
				A_2.Graphics.DrawRectangle(new Pen(this.Attr_2.\u2002, 1f), 0, 0, this.Form_4.Width - 1, this.Form_4.Height - 1);
				A_2.Graphics.FillRectangle(new SolidBrush(this.Attr_2.\u2000), this.Form_4.Width - 15, 0, 15, this.Form_4.Height);
			}

			// Token: 0x06000209 RID: 521 RVA: 0x00012EAC File Offset: 0x000110AC
			internal void \u2002(object A_1, EventArgs A_2)
			{
				this.Attr_2.\u1680 = this.Attr_2.\u1680 + 1;
				Label label = (Label)this.Attr_2.Controls["lblBotMsg"];
				if (this.Attr_2.\u1680 < 20)
				{
					label.ForeColor = Color.Yellow;
					label.Text = string.Format("เจาะระบบแทรกโค้ด... {0}%", this.Attr_2.\u1680 * 5);
					this.Attr_2.Text = "กำลังคำนวณค่า...";
					return;
				}
				if (this.Attr_2.\u1680 < 30)
				{
					label.ForeColor = this.Attr_2.\u2000;
					label.Text = "กำลังเขียนทับข้อมูล ROM";
					return;
				}
				if (this.Attr_2.\u1680 < 50)
				{
					label.ForeColor = Color.Cyan;
					label.Text = string.Format("ซิงค์ตารางยิงหอบตารางที่: {0}", this.Attr_2.Attr_2.Next(1000, 9999));
					return;
				}
				if (this.Attr_2.\u1680 < 65)
				{
					label.ForeColor = Color.Lime;
					label.Text = "เคลียร์แคชระบบเตรียมใช้งาน...";
					return;
				}
				this.Attr_2.Attr_3.Stop();
				label.Text = "การจูนหอบสำเร็จสมบูรณ์!";
				label.ForeColor = this.Attr_2.\u2003;
				this.Attr_2.Text = "เริ่มกระบวนการ / บันทึกยิงหอบ";
				this.Attr_2.Enabled = true;
				MessageBox.Show("คำสั่งจูนหอบสำเร็จ! ระดับ GHOST CAM ถูกฝังลงใน ROM รหัสนี้พร้อมใช้งานแล้ว", "MZATUNER EDGE", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}

			// Token: 0x0600020A RID: 522 RVA: 0x00013030 File Offset: 0x00011230
			internal void \u2003(object A_1, EventArgs A_2)
			{
				if (this.Attr_2.Text == "กรุณาเลือกไฟล์ .BIN" || string.IsNullOrEmpty(this.Attr_2.Text) || !this.Attr_2.Text.EndsWith(".bin", StringComparison.OrdinalIgnoreCase))
				{
					MessageBox.Show("กรุณาเลือกไฟล์ .BIN เป้าหมายให้ถูกต้องก่อนครับ!", "ALERT", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					return;
				}
				this.Attr_2.\u1680 = 0;
				this.Attr_2.Enabled = false;
				this.Attr_2.Attr_3.Start();
			}

			// Token: 0x0400016A RID: 362
			public \u2057 \u00A0;

			// Token: 0x0400016B RID: 363
			public Button \u00A0;

			// Token: 0x0400016C RID: 364
			public Panel \u00A0;

			// Token: 0x0400016D RID: 365
			public Panel \u1680;

			// Token: 0x0400016E RID: 366
			public TextBox \u00A0;

			// Token: 0x0400016F RID: 367
			public Panel \u2000;

			// Token: 0x04000170 RID: 368
			public \u2057.\u1680 \u00A0;
		}
	}
}
