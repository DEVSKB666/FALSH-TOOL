using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using <PrivateImplementationDetails>{68F2EF73-9355-4257-ADA6-397CF8BB8E72};

namespace ns1
{
	// Token: 0x020000AB RID: 171
	public partial class GForm9 : Form
	{
		// Token: 0x060001F1 RID: 497
		[DllImport("user32.dll")]
		public static extern bool ReleaseCapture();

		// Token: 0x060001F2 RID: 498
		[DllImport("user32.dll")]
		public static extern int SendMessage(IntPtr intptr_0, int int_2, int int_3, int int_4);

		// Token: 0x060001F3 RID: 499 RVA: 0x00024D84 File Offset: 0x00022F84
		public GForm9()
		{
			base.Size = new Size(820, 560);
			base.FormBorderStyle = FormBorderStyle.None;
			this.BackColor = this.color_0;
			base.StartPosition = FormStartPosition.CenterParent;
			this.DoubleBuffered = true;
			this.method_0();
			this.timer_0 = new Timer
			{
				Interval = 25
			};
			this.timer_0.Tick += this.timer_0_Tick;
			this.timer_0.Start();
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x0000CDDE File Offset: 0x0000AFDE
		protected override void OnFormClosed(FormClosedEventArgs e)
		{
			Timer timer = this.timer_0;
			if (timer != null)
			{
				timer.Stop();
			}
			Timer timer2 = this.timer_0;
			if (timer2 != null)
			{
				timer2.Dispose();
			}
			base.OnFormClosed(e);
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x00024E84 File Offset: 0x00023084
		private void method_0()
		{
			GForm9.Class137 @class = new GForm9.Class137();
			@class.gform9_0 = this;
			Panel panel = new Panel
			{
				Dock = DockStyle.Top,
				Height = 40,
				BackColor = Color.Transparent
			};
			panel.MouseDown += @class.method_0;
			Label value = new Label
			{
				Text = "MZA",
				ForeColor = Color.White,
				BackColor = this.color_2,
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
			@class.button_0 = new Button
			{
				Text = "✖",
				Size = new Size(40, 40),
				Dock = DockStyle.Right,
				FlatStyle = FlatStyle.Flat,
				ForeColor = this.color_4,
				Font = new Font("Segoe UI", 12f, FontStyle.Bold),
				Cursor = Cursors.Hand
			};
			@class.button_0.FlatAppearance.BorderSize = 0;
			@class.button_0.FlatAppearance.MouseOverBackColor = this.color_2;
			@class.button_0.MouseEnter += @class.method_1;
			@class.button_0.MouseLeave += @class.method_2;
			@class.button_0.Click += @class.method_3;
			panel.Controls.Add(value);
			panel.Controls.Add(value2);
			panel.Controls.Add(@class.button_0);
			base.Controls.Add(panel);
			@class.panel_0 = new Panel
			{
				Location = new Point(20, 60),
				Size = new Size(240, 70),
				BackColor = this.color_1
			};
			@class.panel_0.Paint += @class.method_4;
			Label value3 = new Label
			{
				Text = "\ud83e\udd16 MZABot_Ai // CORE",
				ForeColor = this.color_2,
				Font = new Font("Consolas", 8.5f, FontStyle.Bold),
				AutoSize = true,
				Location = new Point(15, 8)
			};
			Label value4 = new Label
			{
				Name = "lblBotMsg",
				Text = "กำลังรอรับคำสั่งจูน...",
				ForeColor = this.color_5,
				Font = new Font("Segoe UI", 11.5f, FontStyle.Bold),
				AutoSize = true,
				Location = new Point(15, 30)
			};
			@class.panel_0.Controls.Add(value3);
			@class.panel_0.Controls.Add(value4);
			base.Controls.Add(@class.panel_0);
			@class.panel_1 = new Panel
			{
				Location = new Point(280, 55),
				Size = new Size(250, 40),
				BackColor = this.color_1
			};
			@class.panel_1.Paint += @class.method_5;
			@class.textBox_0 = new TextBox
			{
				Location = new Point(10, 10),
				Width = 155,
				BackColor = this.color_1,
				ForeColor = this.color_4,
				Font = new Font("Segoe UI", 9.5f),
				BorderStyle = BorderStyle.None,
				ReadOnly = true,
				Text = "กรุณาเลือกไฟล์ .BIN"
			};
			GForm9.Class136 class2 = new GForm9.Class136
			{
				Text = "ค้นหาไฟล์",
				Location = new Point(175, 4),
				Size = new Size(70, 32),
				Font = new Font("Segoe UI", 8.5f, FontStyle.Bold)
			};
			class2.Click += @class.method_6;
			@class.panel_1.Controls.Add(@class.textBox_0);
			@class.panel_1.Controls.Add(class2);
			base.Controls.Add(@class.panel_1);
			@class.panel_2 = new Panel
			{
				Location = new Point(280, 105),
				Size = new Size(250, 40),
				BackColor = this.color_1
			};
			@class.panel_2.Paint += @class.method_7;
			ComboBox comboBox = new ComboBox
			{
				Location = new Point(5, 6),
				Width = 230,
				Font = new Font("Segoe UI", 12f, FontStyle.Bold),
				DropDownStyle = ComboBoxStyle.DropDownList,
				FlatStyle = FlatStyle.Flat,
				BackColor = this.color_1,
				ForeColor = this.color_5
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
			@class.panel_2.Controls.Add(comboBox);
			base.Controls.Add(@class.panel_2);
			@class.class136_0 = new GForm9.Class136
			{
				Text = "EXECUTE / บันทึกยิงหอบ",
				Location = new Point(550, 55),
				Size = new Size(250, 90),
				BackColor = this.color_0
			};
			this.timer_1 = new Timer
			{
				Interval = 45
			};
			this.timer_1.Tick += @class.method_8;
			@class.class136_0.Click += @class.method_9;
			base.Controls.Add(@class.class136_0);
			Label value5 = new Label
			{
				Text = "[ กำหนดองศาลิ้นปีกผีเสื้อ / TPS ]",
				ForeColor = this.color_4,
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
				GForm9.Class135 class3 = new GForm9.Class135
				{
					Text = "TPS " + array[i],
					Location = new Point(20 + i * 155, 185),
					Size = new Size(130, 45)
				};
				if (i == 1)
				{
					class3.Checked = true;
				}
				base.Controls.Add(class3);
			}
			Label value6 = new Label
			{
				Text = "[ ล็อครอบเดินเบายิงหอบ / RPM ]",
				ForeColor = this.color_4,
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
				GForm9.Class135 class4 = new GForm9.Class135
				{
					Text = array2[j] + " RPM",
					Location = new Point(20, 285 + j * 50),
					Size = new Size(250, 40)
				};
				if (j == 4)
				{
					class4.Checked = true;
				}
				base.Controls.Add(class4);
			}
			Label value7 = new Label
			{
				Text = "[ เลือกระดับความดุดัน / GHOST CAM ]",
				ForeColor = this.color_4,
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
				GForm9.Class135 class5 = new GForm9.Class135
				{
					Text = array3[k],
					Location = new Point(300 + k * 90, 285 + k * 50),
					Size = new Size(130, 40)
				};
				if (k == 2)
				{
					class5.Checked = true;
				}
				base.Controls.Add(class5);
			}
			Label value8 = new Label
			{
				Text = "MZA TUNER CORE // V1.0.0.X",
				ForeColor = this.color_2,
				Font = new Font("Consolas", 8f),
				AutoSize = true,
				Location = new Point(15, 535)
			};
			base.Controls.Add(value8);
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x0002580C File Offset: 0x00023A0C
		protected override void OnPaint(PaintEventArgs pevent)
		{
			base.OnPaint(pevent);
			pevent.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
			using (Pen pen = new Pen(this.color_2, 2f))
			{
				pevent.Graphics.DrawRectangle(pen, 1, 1, base.Width - 3, base.Height - 3);
			}
			using (Pen pen2 = new Pen(this.color_3, 1f))
			{
				pevent.Graphics.DrawRectangle(pen2, 6, 6, base.Width - 13, base.Height - 13);
				pevent.Graphics.FillRectangle(new SolidBrush(this.color_5), 6, 6, 15, 3);
				pevent.Graphics.FillRectangle(new SolidBrush(this.color_5), 6, 6, 3, 15);
				pevent.Graphics.FillRectangle(new SolidBrush(this.color_5), base.Width - 21, 6, 15, 3);
				pevent.Graphics.FillRectangle(new SolidBrush(this.color_5), base.Width - 9, 6, 3, 15);
				pevent.Graphics.FillRectangle(new SolidBrush(this.color_5), 6, base.Height - 9, 15, 3);
				pevent.Graphics.FillRectangle(new SolidBrush(this.color_5), 6, base.Height - 21, 3, 15);
			}
			using (Pen pen3 = new Pen(Color.FromArgb(15, 255, 255, 255), 1f))
			{
				for (int i = 0; i < base.Width; i += 40)
				{
					pevent.Graphics.DrawLine(pen3, i, 45, i, base.Height);
				}
				for (int j = 45; j < base.Height; j += 40)
				{
					pevent.Graphics.DrawLine(pen3, 0, j, base.Width, j);
				}
			}
			using (Font font = new Font("Consolas", 7f))
			{
				using (SolidBrush solidBrush = new SolidBrush(Color.FromArgb(20, this.color_2)))
				{
					pevent.Graphics.DrawString(string.Format("0x{0:X4}", this.random_0.Next(8000, 9999)), font, solidBrush, (float)(base.Width - 80), (float)(base.Height - 40));
					pevent.Graphics.DrawString(string.Format("MEM_{0}", this.random_0.Next(10, 99)), font, solidBrush, 300f, 200f);
					pevent.Graphics.DrawString("[TARGET : LOCKED]", font, solidBrush, 280f, 520f);
				}
			}
			if (this.int_0 >= 0 && this.int_0 <= base.Height)
			{
				using (LinearGradientBrush linearGradientBrush = new LinearGradientBrush(new Rectangle(0, this.int_0 - 20, base.Width, 20), Color.Transparent, Color.FromArgb(70, this.color_2), 90f))
				{
					pevent.Graphics.FillRectangle(linearGradientBrush, new Rectangle(0, this.int_0 - 20, base.Width, 20));
				}
				pevent.Graphics.DrawLine(new Pen(Color.FromArgb(150, this.color_2), 1f), 0, this.int_0, base.Width, this.int_0);
			}
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x00025BCC File Offset: 0x00023DCC
		[CompilerGenerated]
		private void timer_0_Tick(object sender, EventArgs e)
		{
			this.int_0 += 4;
			if (this.int_0 > base.Height + 50)
			{
				this.int_0 = -50;
			}
			base.Invalidate(new Rectangle(0, this.int_0 - 30, base.Width, 30));
		}

		// Token: 0x0400015D RID: 349
		private Color color_0 = Color.FromArgb(12, 12, 15);

		// Token: 0x0400015E RID: 350
		private Color color_1 = Color.FromArgb(20, 20, 24);

		// Token: 0x0400015F RID: 351
		private Color color_2 = Color.FromArgb(255, 20, 40);

		// Token: 0x04000160 RID: 352
		private Color color_3 = Color.FromArgb(180, 10, 20);

		// Token: 0x04000161 RID: 353
		private Color color_4 = Color.FromArgb(100, 100, 110);

		// Token: 0x04000162 RID: 354
		private Color color_5 = Color.White;

		// Token: 0x04000163 RID: 355
		private Timer timer_0;

		// Token: 0x04000164 RID: 356
		private Timer timer_1;

		// Token: 0x04000165 RID: 357
		private int int_0 = -50;

		// Token: 0x04000166 RID: 358
		private int int_1;

		// Token: 0x04000167 RID: 359
		private Random random_0 = new Random();

		// Token: 0x020000AC RID: 172
		private class Class135 : RadioButton
		{
			// Token: 0x060001F8 RID: 504 RVA: 0x0000CE09 File Offset: 0x0000B009
			public Class135()
			{
				this.ForeColor = Color.LightGray;
				this.Font = new Font(EC2D41B1-A2F9-4664-90D8-86645EE2E753.smethod_200(), 10.5f, FontStyle.Bold);
				this.Cursor = Cursors.Hand;
				this.DoubleBuffered = true;
			}

			// Token: 0x060001F9 RID: 505 RVA: 0x0000CE44 File Offset: 0x0000B044
			protected override void OnMouseEnter(EventArgs e)
			{
				this.bool_0 = true;
				base.Invalidate();
				base.OnMouseEnter(e);
			}

			// Token: 0x060001FA RID: 506 RVA: 0x0000CE5A File Offset: 0x0000B05A
			protected override void OnMouseLeave(EventArgs e)
			{
				this.bool_0 = false;
				base.Invalidate();
				base.OnMouseLeave(e);
			}

			// Token: 0x060001FB RID: 507 RVA: 0x00025C20 File Offset: 0x00023E20
			protected override void OnPaint(PaintEventArgs pevent)
			{
				pevent.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
				pevent.Graphics.Clear(base.Parent.BackColor);
				new Rectangle(0, 0, base.Width - 1, base.Height - 1);
				GraphicsPath graphicsPath = new GraphicsPath();
				graphicsPath.AddLine(0, 0, base.Width - 10, 0);
				graphicsPath.AddLine(base.Width - 10, 0, base.Width, 10);
				graphicsPath.AddLine(base.Width, 10, base.Width, base.Height);
				graphicsPath.AddLine(base.Width, base.Height, 10, base.Height);
				graphicsPath.AddLine(10, base.Height, 0, base.Height - 10);
				graphicsPath.CloseFigure();
				Color color = base.Checked ? Color.FromArgb(220, 20, 30) : (this.bool_0 ? Color.FromArgb(50, 50, 50) : Color.FromArgb(20, 20, 22));
				Color color2 = base.Checked ? Color.White : (this.bool_0 ? Color.FromArgb(255, 60, 70) : Color.FromArgb(100, 100, 110));
				Color foreColor = base.Checked ? Color.White : Color.LightGray;
				using (SolidBrush solidBrush = new SolidBrush(color))
				{
					pevent.Graphics.FillPath(solidBrush, graphicsPath);
				}
				using (Pen pen = new Pen(color2, base.Checked ? 2f : 1f))
				{
					pevent.Graphics.DrawPath(pen, graphicsPath);
				}
				Rectangle rect = new Rectangle(12, (base.Height - 10) / 2, 10, 10);
				if (base.Checked)
				{
					pevent.Graphics.FillRectangle(Brushes.White, rect);
				}
				else
				{
					pevent.Graphics.DrawRectangle(new Pen(Color.Gray, 1f), rect);
				}
				TextRenderer.DrawText(pevent.Graphics, this.Text, this.Font, new Point(32, (base.Height - this.Font.Height) / 2), foreColor);
			}

			// Token: 0x04000168 RID: 360
			private bool bool_0;
		}

		// Token: 0x020000AD RID: 173
		private class Class136 : Button
		{
			// Token: 0x060001FC RID: 508 RVA: 0x0000CE70 File Offset: 0x0000B070
			public Class136()
			{
				this.DoubleBuffered = true;
				this.Cursor = Cursors.Hand;
				this.Font = new Font(EC2D41B1-A2F9-4664-90D8-86645EE2E753.smethod_181(), 12f);
			}

			// Token: 0x060001FD RID: 509 RVA: 0x0000CE9F File Offset: 0x0000B09F
			protected override void OnMouseEnter(EventArgs e)
			{
				this.bool_0 = true;
				base.Invalidate();
				base.OnMouseEnter(e);
			}

			// Token: 0x060001FE RID: 510 RVA: 0x0000CEB5 File Offset: 0x0000B0B5
			protected override void OnMouseLeave(EventArgs e)
			{
				this.bool_0 = false;
				base.Invalidate();
				base.OnMouseLeave(e);
			}

			// Token: 0x060001FF RID: 511 RVA: 0x00025E64 File Offset: 0x00024064
			protected override void OnPaint(PaintEventArgs pevent)
			{
				pevent.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
				pevent.Graphics.Clear(base.Parent.BackColor);
				Rectangle rectangle = new Rectangle(0, 0, base.Width - 1, base.Height - 1);
				GraphicsPath graphicsPath = new GraphicsPath();
				graphicsPath.AddLine(15, 0, base.Width, 0);
				graphicsPath.AddLine(base.Width, 0, base.Width, base.Height - 15);
				graphicsPath.AddLine(base.Width - 15, base.Height, 0, base.Height);
				graphicsPath.AddLine(0, base.Height, 0, 15);
				graphicsPath.CloseFigure();
				if (this.bool_0)
				{
					using (HatchBrush hatchBrush = new HatchBrush(HatchStyle.ForwardDiagonal, Color.FromArgb(180, 20, 30), Color.FromArgb(255, 20, 40)))
					{
						pevent.Graphics.FillPath(hatchBrush, graphicsPath);
						goto IL_127;
					}
				}
				using (LinearGradientBrush linearGradientBrush = new LinearGradientBrush(rectangle, Color.FromArgb(255, 20, 40), Color.FromArgb(180, 5, 10), 45f))
				{
					pevent.Graphics.FillPath(linearGradientBrush, graphicsPath);
				}
				IL_127:
				pevent.Graphics.DrawPath(new Pen(Color.White, 2f), graphicsPath);
				StringFormat format = new StringFormat
				{
					Alignment = StringAlignment.Center,
					LineAlignment = StringAlignment.Center
				};
				pevent.Graphics.DrawString(this.Text, this.Font, Brushes.White, rectangle, format);
			}

			// Token: 0x04000169 RID: 361
			private bool bool_0;
		}

		// Token: 0x020000AE RID: 174
		[CompilerGenerated]
		private sealed class Class137
		{
			// Token: 0x06000201 RID: 513 RVA: 0x0000CECB File Offset: 0x0000B0CB
			internal void method_0(object sender, MouseEventArgs e)
			{
				GForm9.ReleaseCapture();
				GForm9.SendMessage(this.gform9_0.Handle, 274, 61458, 0);
			}

			// Token: 0x06000202 RID: 514 RVA: 0x0000CEEF File Offset: 0x0000B0EF
			internal void method_1(object sender, EventArgs e)
			{
				this.button_0.ForeColor = Color.White;
			}

			// Token: 0x06000203 RID: 515 RVA: 0x0000CF01 File Offset: 0x0000B101
			internal void method_2(object sender, EventArgs e)
			{
				this.button_0.ForeColor = this.gform9_0.color_4;
			}

			// Token: 0x06000204 RID: 516 RVA: 0x0000CF19 File Offset: 0x0000B119
			internal void method_3(object sender, EventArgs e)
			{
				this.gform9_0.Close();
			}

			// Token: 0x06000205 RID: 517 RVA: 0x00026008 File Offset: 0x00024208
			internal void method_4(object sender, PaintEventArgs e)
			{
				e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
				e.Graphics.DrawRectangle(new Pen(this.gform9_0.color_3, 1.5f), 0, 0, this.panel_0.Width - 1, this.panel_0.Height - 1);
				e.Graphics.FillRectangle(new SolidBrush(this.gform9_0.color_2), 0, 0, 5, this.panel_0.Height);
			}

			// Token: 0x06000206 RID: 518 RVA: 0x0000CF26 File Offset: 0x0000B126
			internal void method_5(object sender, PaintEventArgs e)
			{
				e.Graphics.DrawRectangle(new Pen(this.gform9_0.color_4, 1f), 0, 0, this.panel_1.Width - 1, this.panel_1.Height - 1);
			}

			// Token: 0x06000207 RID: 519 RVA: 0x00026088 File Offset: 0x00024288
			internal void method_6(object sender, EventArgs e)
			{
				using (OpenFileDialog openFileDialog = new OpenFileDialog
				{
					Filter = EC2D41B1-A2F9-4664-90D8-86645EE2E753.smethod_902(),
					Title = EC2D41B1-A2F9-4664-90D8-86645EE2E753.smethod_926()
				})
				{
					if (openFileDialog.ShowDialog() == DialogResult.OK)
					{
						this.textBox_0.Text = Path.GetFileName(openFileDialog.FileName);
						this.textBox_0.Tag = openFileDialog.FileName;
						this.textBox_0.ForeColor = this.gform9_0.color_5;
					}
				}
			}

			// Token: 0x06000208 RID: 520 RVA: 0x00026114 File Offset: 0x00024314
			internal void method_7(object sender, PaintEventArgs e)
			{
				e.Graphics.DrawRectangle(new Pen(this.gform9_0.color_4, 1f), 0, 0, this.panel_2.Width - 1, this.panel_2.Height - 1);
				e.Graphics.FillRectangle(new SolidBrush(this.gform9_0.color_2), this.panel_2.Width - 15, 0, 15, this.panel_2.Height);
			}

			// Token: 0x06000209 RID: 521 RVA: 0x00026194 File Offset: 0x00024394
			internal void method_8(object sender, EventArgs e)
			{
				this.gform9_0.int_1 = this.gform9_0.int_1 + 1;
				Label label = (Label)this.panel_0.Controls[EC2D41B1-A2F9-4664-90D8-86645EE2E753.smethod_282()];
				if (this.gform9_0.int_1 < 20)
				{
					label.ForeColor = Color.Yellow;
					label.Text = string.Format(EC2D41B1-A2F9-4664-90D8-86645EE2E753.smethod_927(), this.gform9_0.int_1 * 5);
					this.class136_0.Text = EC2D41B1-A2F9-4664-90D8-86645EE2E753.smethod_928();
					return;
				}
				if (this.gform9_0.int_1 < 30)
				{
					label.ForeColor = this.gform9_0.color_2;
					label.Text = EC2D41B1-A2F9-4664-90D8-86645EE2E753.smethod_929();
					return;
				}
				if (this.gform9_0.int_1 < 50)
				{
					label.ForeColor = Color.Cyan;
					label.Text = string.Format(EC2D41B1-A2F9-4664-90D8-86645EE2E753.smethod_930(), this.gform9_0.random_0.Next(1000, 9999));
					return;
				}
				if (this.gform9_0.int_1 < 65)
				{
					label.ForeColor = Color.Lime;
					label.Text = EC2D41B1-A2F9-4664-90D8-86645EE2E753.smethod_931();
					return;
				}
				this.gform9_0.timer_1.Stop();
				label.Text = EC2D41B1-A2F9-4664-90D8-86645EE2E753.smethod_932();
				label.ForeColor = this.gform9_0.color_5;
				this.class136_0.Text = EC2D41B1-A2F9-4664-90D8-86645EE2E753.smethod_933();
				this.class136_0.Enabled = true;
				MessageBox.Show(EC2D41B1-A2F9-4664-90D8-86645EE2E753.smethod_934(), EC2D41B1-A2F9-4664-90D8-86645EE2E753.smethod_935(), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}

			// Token: 0x0600020A RID: 522 RVA: 0x00026318 File Offset: 0x00024518
			internal void method_9(object sender, EventArgs e)
			{
				if (!(this.textBox_0.Text == EC2D41B1-A2F9-4664-90D8-86645EE2E753.smethod_284()) && !string.IsNullOrEmpty(this.textBox_0.Text) && this.textBox_0.Text.EndsWith(EC2D41B1-A2F9-4664-90D8-86645EE2E753.smethod_422(), StringComparison.OrdinalIgnoreCase))
				{
					this.gform9_0.int_1 = 0;
					this.class136_0.Enabled = false;
					this.gform9_0.timer_1.Start();
					return;
				}
				MessageBox.Show(EC2D41B1-A2F9-4664-90D8-86645EE2E753.smethod_936(), EC2D41B1-A2F9-4664-90D8-86645EE2E753.smethod_937(), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}

			// Token: 0x0400016A RID: 362
			public GForm9 gform9_0;

			// Token: 0x0400016B RID: 363
			public Button button_0;

			// Token: 0x0400016C RID: 364
			public Panel panel_0;

			// Token: 0x0400016D RID: 365
			public Panel panel_1;

			// Token: 0x0400016E RID: 366
			public TextBox textBox_0;

			// Token: 0x0400016F RID: 367
			public Panel panel_2;

			// Token: 0x04000170 RID: 368
			public GForm9.Class136 class136_0;
		}
	}
}
