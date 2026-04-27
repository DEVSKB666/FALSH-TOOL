using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace ns1
{
	// Token: 0x0200009A RID: 154
	public partial class GForm5 : Form
	{
		// Token: 0x06000171 RID: 369 RVA: 0x0000C97D File Offset: 0x0000AB7D
		[CompilerGenerated]
		public Color method_0()
		{
			return this.color_0;
		}

		// Token: 0x06000172 RID: 370 RVA: 0x0000C985 File Offset: 0x0000AB85
		[CompilerGenerated]
		private void method_1(Color color_3)
		{
			this.color_0 = color_3;
		}

		// Token: 0x06000173 RID: 371
		[DllImport("user32.dll")]
		public static extern bool ReleaseCapture();

		// Token: 0x06000174 RID: 372
		[DllImport("user32.dll")]
		public static extern int SendMessage(IntPtr intptr_0, int int_3, int int_4, int int_5);

		// Token: 0x06000175 RID: 373
		[DllImport("Gdi32.dll")]
		private static extern IntPtr CreateRoundRectRgn(int int_3, int int_4, int int_5, int int_6, int int_7, int int_8);

		// Token: 0x06000176 RID: 374 RVA: 0x0001F338 File Offset: 0x0001D538
		public GForm5(Color color_3)
		{
			this.method_1(color_3);
			this.int_0 = (int)color_3.R;
			this.int_1 = (int)color_3.G;
			this.int_2 = (int)color_3.B;
			base.Size = new Size(480, 520);
			base.FormBorderStyle = FormBorderStyle.None;
			this.BackColor = this.color_2;
			base.StartPosition = FormStartPosition.CenterParent;
			this.DoubleBuffered = true;
			base.Region = Region.FromHrgn(GForm5.CreateRoundRectRgn(0, 0, base.Width, base.Height, 25, 25));
			this.method_2();
			this.method_5();
			this.method_4();
		}

		// Token: 0x06000177 RID: 375 RVA: 0x0001F41C File Offset: 0x0001D61C
		private void method_2()
		{
			this.panel_0 = new Panel
			{
				Dock = DockStyle.Top,
				Height = 50,
				BackColor = Color.FromArgb(12, 12, 15)
			};
			this.panel_0.MouseDown += this.panel_0_MouseDown;
			this.panel_0.Paint += this.panel_0_Paint;
			this.label_0 = new Label
			{
				Text = "◢ ระบบสแกนเฉดสี : CHROMA_HUB ◣",
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
			this.panel_1 = new Panel
			{
				Location = new Point(25, 70),
				Size = new Size(150, 150),
				BackColor = Color.Transparent
			};
			this.panel_1.Paint += this.panel_1_Paint;
			base.Controls.Add(this.panel_1);
			this.trackBar_0 = this.method_3(200, 85, Color.Red, this.int_0, new Action<int>(this.method_6));
			this.trackBar_1 = this.method_3(200, 135, Color.Green, this.int_1, new Action<int>(this.method_7));
			this.trackBar_2 = this.method_3(200, 185, Color.Blue, this.int_2, new Action<int>(this.method_8));
			this.label_1 = new Label
			{
				Text = "R: " + this.int_0.ToString(),
				ForeColor = Color.White,
				Font = new Font("Consolas", 9f, FontStyle.Bold),
				Location = new Point(200, 70)
			};
			this.label_2 = new Label
			{
				Text = "G: " + this.int_1.ToString(),
				ForeColor = Color.White,
				Font = new Font("Consolas", 9f, FontStyle.Bold),
				Location = new Point(200, 120)
			};
			this.label_3 = new Label
			{
				Text = "B: " + this.int_2.ToString(),
				ForeColor = Color.White,
				Font = new Font("Consolas", 9f, FontStyle.Bold),
				Location = new Point(200, 170)
			};
			base.Controls.Add(this.label_1);
			base.Controls.Add(this.label_2);
			base.Controls.Add(this.label_3);
			Label value = new Label
			{
				Text = "◤ ตารางสีพรีเซ็ตนีออน : NEON_MATRIX ◢",
				ForeColor = Color.Gray,
				Font = new Font("Impact", 8f),
				Location = new Point(25, 235)
			};
			base.Controls.Add(value);
			this.flowLayoutPanel_0 = new FlowLayoutPanel
			{
				Location = new Point(25, 255),
				Size = new Size(430, 180),
				AutoScroll = false
			};
			Color[] array = new Color[]
			{
				Color.Red,
				Color.OrangeRed,
				Color.Gold,
				Color.Lime,
				Color.Cyan,
				Color.DeepSkyBlue,
				Color.MediumSlateBlue,
				Color.Magenta,
				Color.FromArgb(255, 30, 30),
				Color.FromArgb(30, 255, 30),
				Color.FromArgb(30, 30, 255),
				Color.FromArgb(255, 255, 30),
				Color.FromArgb(215, 15, 15),
				Color.FromArgb(15, 215, 15),
				Color.FromArgb(15, 15, 215),
				Color.White,
				Color.DeepPink,
				Color.SpringGreen,
				Color.Yellow,
				Color.Aqua,
				Color.Purple,
				Color.Silver,
				Color.Teal,
				Color.Navy
			};
			for (int i = 0; i < array.Length; i++)
			{
				GForm5.Class127 @class = new GForm5.Class127();
				@class.gform5_0 = this;
				@class.color_0 = array[i];
				Button button = new Button
				{
					Size = new Size(45, 45),
					BackColor = @class.color_0,
					FlatStyle = FlatStyle.Flat,
					Cursor = Cursors.Hand
				};
				button.FlatAppearance.BorderSize = 1;
				button.FlatAppearance.BorderColor = Color.FromArgb(40, 40, 45);
				button.Click += @class.method_0;
				this.flowLayoutPanel_0.Controls.Add(button);
			}
			base.Controls.Add(this.flowLayoutPanel_0);
			this.button_1 = new Button
			{
				Text = "◢ บันทึกสีที่เลือกเข้าสู่ระบบหลัก ◣",
				Location = new Point(25, 445),
				Size = new Size(430, 50),
				BackColor = Color.FromArgb(15, 15, 20),
				ForeColor = this.color_1,
				FlatStyle = FlatStyle.Flat,
				Font = new Font("Impact", 11f)
			};
			this.button_1.FlatAppearance.BorderSize = 1;
			this.button_1.FlatAppearance.BorderColor = this.color_1;
			this.button_1.Click += this.button_1_Click;
			base.Controls.Add(this.button_1);
		}

		// Token: 0x06000178 RID: 376 RVA: 0x0001FB38 File Offset: 0x0001DD38
		private TrackBar method_3(int int_3, int int_4, Color color_3, int int_5, Action<int> action_0)
		{
			GForm5.Class128 @class = new GForm5.Class128();
			@class.action_0 = action_0;
			@class.trackBar_0 = new TrackBar
			{
				Location = new Point(int_3, int_4),
				Width = 250,
				Maximum = 255,
				Value = int_5,
				BackColor = this.color_2,
				TickStyle = TickStyle.None
			};
			@class.trackBar_0.Scroll += @class.method_0;
			base.Controls.Add(@class.trackBar_0);
			return @class.trackBar_0;
		}

		// Token: 0x06000179 RID: 377 RVA: 0x0001FBCC File Offset: 0x0001DDCC
		private void method_4()
		{
			this.method_1(Color.FromArgb(this.int_0, this.int_1, this.int_2));
			this.label_1.Text = "R: " + this.int_0.ToString();
			this.label_2.Text = "G: " + this.int_1.ToString();
			this.label_3.Text = "B: " + this.int_2.ToString();
			this.panel_1.Invalidate();
			this.button_1.ForeColor = this.method_0();
			this.button_1.FlatAppearance.BorderColor = this.method_0();
		}

		// Token: 0x0600017A RID: 378 RVA: 0x0000C98E File Offset: 0x0000AB8E
		private void method_5()
		{
			this.timer_0 = new Timer
			{
				Interval = 40
			};
			this.timer_0.Tick += this.timer_0_Tick;
			this.timer_0.Start();
		}

		// Token: 0x0600017B RID: 379 RVA: 0x0001FC88 File Offset: 0x0001DE88
		protected override void OnPaint(PaintEventArgs pevent)
		{
			base.OnPaint(pevent);
			using (Pen pen = new Pen(Color.FromArgb(40, 40, 45), 1f))
			{
				pevent.Graphics.DrawRectangle(pen, 0, 0, base.Width - 1, base.Height - 1);
			}
			using (Pen pen2 = new Pen(this.color_1, 2f))
			{
				pevent.Graphics.DrawLine(pen2, 5, 5, 25, 5);
				pevent.Graphics.DrawLine(pen2, 5, 5, 5, 25);
				pevent.Graphics.DrawLine(pen2, base.Width - 5, base.Height - 5, base.Width - 5 - 20, base.Height - 5);
				pevent.Graphics.DrawLine(pen2, base.Width - 5, base.Height - 5, base.Width - 5, base.Height - 5 - 20);
			}
		}

		// Token: 0x0600017C RID: 380 RVA: 0x0000C9C5 File Offset: 0x0000ABC5
		[CompilerGenerated]
		private void panel_0_MouseDown(object sender, MouseEventArgs e)
		{
			GForm5.ReleaseCapture();
			GForm5.SendMessage(base.Handle, 274, 61458, 0);
		}

		// Token: 0x0600017D RID: 381 RVA: 0x0001FD98 File Offset: 0x0001DF98
		[CompilerGenerated]
		private void panel_0_Paint(object sender, PaintEventArgs e)
		{
			e.Graphics.DrawLine(new Pen(this.color_1, 2f), 0, 49, base.Width, 49);
			using (LinearGradientBrush linearGradientBrush = new LinearGradientBrush(new Rectangle(0, 44, base.Width, 6), Color.Transparent, Color.FromArgb(60, this.color_1), 90f))
			{
				e.Graphics.FillRectangle(linearGradientBrush, 0, 44, base.Width, 6);
			}
		}

		// Token: 0x0600017E RID: 382 RVA: 0x0000C9E4 File Offset: 0x0000ABE4
		[CompilerGenerated]
		private void button_0_Click(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.Cancel;
			base.Close();
		}

		// Token: 0x0600017F RID: 383 RVA: 0x0001FE2C File Offset: 0x0001E02C
		[CompilerGenerated]
		private void panel_1_Paint(object sender, PaintEventArgs e)
		{
			e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
			Rectangle rect = new Rectangle(10, 10, 130, 130);
			using (SolidBrush solidBrush = new SolidBrush(this.method_0()))
			{
				e.Graphics.FillEllipse(solidBrush, rect);
			}
			using (Pen pen = new Pen(Color.FromArgb((int)(this.float_0 * 255f), this.color_1), 3f))
			{
				e.Graphics.DrawEllipse(pen, rect);
			}
			using (Pen pen2 = new Pen(Color.FromArgb(100, Color.White), 1f))
			{
				e.Graphics.DrawArc(pen2, rect, 0f, 90f);
				e.Graphics.DrawArc(pen2, rect, 180f, 90f);
			}
		}

		// Token: 0x06000180 RID: 384 RVA: 0x0000C9F3 File Offset: 0x0000ABF3
		[CompilerGenerated]
		private void method_6(int int_3)
		{
			this.int_0 = int_3;
			this.method_4();
		}

		// Token: 0x06000181 RID: 385 RVA: 0x0000CA02 File Offset: 0x0000AC02
		[CompilerGenerated]
		private void method_7(int int_3)
		{
			this.int_1 = int_3;
			this.method_4();
		}

		// Token: 0x06000182 RID: 386 RVA: 0x0000CA11 File Offset: 0x0000AC11
		[CompilerGenerated]
		private void method_8(int int_3)
		{
			this.int_2 = int_3;
			this.method_4();
		}

		// Token: 0x06000183 RID: 387 RVA: 0x0000CA20 File Offset: 0x0000AC20
		[CompilerGenerated]
		private void button_1_Click(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.OK;
			base.Close();
		}

		// Token: 0x06000184 RID: 388 RVA: 0x0001FF34 File Offset: 0x0001E134
		[CompilerGenerated]
		private void timer_0_Tick(object sender, EventArgs e)
		{
			if (this.bool_0)
			{
				this.float_0 += 0.05f;
				if (this.float_0 >= 1f)
				{
					this.bool_0 = false;
				}
			}
			else
			{
				this.float_0 -= 0.05f;
				if (this.float_0 <= 0.3f)
				{
					this.bool_0 = true;
				}
			}
			this.panel_1.Invalidate();
		}

		// Token: 0x040000E0 RID: 224
		[CompilerGenerated]
		private Color color_0;

		// Token: 0x040000E1 RID: 225
		private Color color_1 = Color.FromArgb(220, 20, 20);

		// Token: 0x040000E2 RID: 226
		private Color color_2 = Color.FromArgb(8, 8, 10);

		// Token: 0x040000E3 RID: 227
		private int int_0 = 255;

		// Token: 0x040000E4 RID: 228
		private int int_1;

		// Token: 0x040000E5 RID: 229
		private int int_2;

		// Token: 0x040000E6 RID: 230
		private Panel panel_0;

		// Token: 0x040000E7 RID: 231
		private Label label_0;

		// Token: 0x040000E8 RID: 232
		private Button button_0;

		// Token: 0x040000E9 RID: 233
		private Panel panel_1;

		// Token: 0x040000EA RID: 234
		private FlowLayoutPanel flowLayoutPanel_0;

		// Token: 0x040000EB RID: 235
		private TrackBar trackBar_0;

		// Token: 0x040000EC RID: 236
		private TrackBar trackBar_1;

		// Token: 0x040000ED RID: 237
		private TrackBar trackBar_2;

		// Token: 0x040000EE RID: 238
		private Label label_1;

		// Token: 0x040000EF RID: 239
		private Label label_2;

		// Token: 0x040000F0 RID: 240
		private Label label_3;

		// Token: 0x040000F1 RID: 241
		private Button button_1;

		// Token: 0x040000F2 RID: 242
		private Timer timer_0;

		// Token: 0x040000F3 RID: 243
		private float float_0 = 1f;

		// Token: 0x040000F4 RID: 244
		private bool bool_0;

		// Token: 0x0200009B RID: 155
		[CompilerGenerated]
		private sealed class Class127
		{
			// Token: 0x06000186 RID: 390 RVA: 0x0001FFA4 File Offset: 0x0001E1A4
			internal void method_0(object sender, EventArgs e)
			{
				this.gform5_0.method_1(this.color_0);
				this.gform5_0.int_0 = (int)this.color_0.R;
				this.gform5_0.int_1 = (int)this.color_0.G;
				this.gform5_0.int_2 = (int)this.color_0.B;
				this.gform5_0.trackBar_0.Value = this.gform5_0.int_0;
				this.gform5_0.trackBar_1.Value = this.gform5_0.int_1;
				this.gform5_0.trackBar_2.Value = this.gform5_0.int_2;
				this.gform5_0.method_4();
				Console.Beep(1200, 50);
			}

			// Token: 0x040000F5 RID: 245
			public Color color_0;

			// Token: 0x040000F6 RID: 246
			public GForm5 gform5_0;
		}

		// Token: 0x0200009C RID: 156
		[CompilerGenerated]
		private sealed class Class128
		{
			// Token: 0x06000188 RID: 392 RVA: 0x0000CA2F File Offset: 0x0000AC2F
			internal void method_0(object sender, EventArgs e)
			{
				this.action_0(this.trackBar_0.Value);
			}

			// Token: 0x040000F7 RID: 247
			public Action<int> action_0;

			// Token: 0x040000F8 RID: 248
			public TrackBar trackBar_0;
		}
	}
}
