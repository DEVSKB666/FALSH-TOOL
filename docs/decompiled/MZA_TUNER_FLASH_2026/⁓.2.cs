using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using <PrivateImplementationDetails>{68F2EF73-9355-4257-ADA6-397CF8BB8E72};

namespace Attr_3
{
	// Token: 0x0200009A RID: 154
	public partial class Type_49 : Form
	{
		// Token: 0x06000171 RID: 369 RVA: 0x0000BA72 File Offset: 0x00009C72
		[CompilerGenerated]
		public Color \u00A0()
		{
			return this.\u00A0;
		}

		// Token: 0x06000172 RID: 370 RVA: 0x0000BA7A File Offset: 0x00009C7A
		[CompilerGenerated]
		private void \u00A0(Color A_1)
		{
			this.\u00A0 = A_1;
		}

		// Token: 0x06000173 RID: 371
		[DllImport("user32.dll", EntryPoint = "ReleaseCapture")]
		public static extern bool \u1680();

		// Token: 0x06000174 RID: 372
		[DllImport("user32.dll", EntryPoint = "SendMessage")]
		public static extern int \u00A0(IntPtr, int, int, int);

		// Token: 0x06000175 RID: 373
		[DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
		private static extern IntPtr \u00A0(int, int, int, int, int, int);

		// Token: 0x06000176 RID: 374 RVA: 0x0000BA84 File Offset: 0x00009C84
		public Type_49(Color A_1)
		{
			this.\u00A0(A_1);
			this.\u00A0 = (int)A_1.R;
			this.\u1680 = (int)A_1.G;
			this.\u2000 = (int)A_1.B;
			base.Size = new Size(480, 520);
			base.FormBorderStyle = FormBorderStyle.None;
			this.BackColor = this.\u2000;
			base.StartPosition = FormStartPosition.CenterParent;
			this.DoubleBuffered = true;
			base.Region = Region.FromHrgn(\u2053.\u00A0(0, 0, base.Width, base.Height, 25, 25));
			this.\u2000();
			this.\u2002();
			this.\u2001();
		}

		// Token: 0x06000177 RID: 375 RVA: 0x0000BB68 File Offset: 0x00009D68
		private void \u2000()
		{
			this.\u00A0 = new Panel
			{
				Dock = DockStyle.Top,
				Height = 50,
				BackColor = Color.FromArgb(12, 12, 15)
			};
			this.Attr_2.MouseDown += this.\u00A0;
			this.Attr_2.Paint += this.\u00A0;
			this.\u00A0 = new Label
			{
				Text = "◢ ระบบสแกนเฉดสี : CHROMA_HUB ◣",
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
			this.Attr_2.Click += this.\u00A0;
			this.Attr_2.Controls.Add(this.\u00A0);
			this.Attr_2.Controls.Add(this.\u00A0);
			base.Controls.Add(this.\u00A0);
			this.\u1680 = new Panel
			{
				Location = new Point(25, 70),
				Size = new Size(150, 150),
				BackColor = Color.Transparent
			};
			this.Attr_3.Paint += this.\u1680;
			base.Controls.Add(this.\u1680);
			int num = 200;
			this.\u00A0 = this.\u00A0(num, 85, Color.Red, this.\u00A0, new Action<int>(this.\u00A0));
			this.\u1680 = this.\u00A0(num, 135, Color.Green, this.\u1680, new Action<int>(this.\u1680));
			this.\u2000 = this.\u00A0(num, 185, Color.Blue, this.\u2000, new Action<int>(this.\u2000));
			this.\u1680 = new Label
			{
				Text = "R: " + this.Attr_2.ToString(),
				ForeColor = Color.White,
				Font = new Font("Consolas", 9f, FontStyle.Bold),
				Location = new Point(num, 70)
			};
			this.\u2000 = new Label
			{
				Text = "G: " + this.Attr_3.ToString(),
				ForeColor = Color.White,
				Font = new Font("Consolas", 9f, FontStyle.Bold),
				Location = new Point(num, 120)
			};
			this.\u2001 = new Label
			{
				Text = "B: " + this.Form_4.ToString(),
				ForeColor = Color.White,
				Font = new Font("Consolas", 9f, FontStyle.Bold),
				Location = new Point(num, 170)
			};
			base.Controls.Add(this.\u1680);
			base.Controls.Add(this.\u2000);
			base.Controls.Add(this.\u2001);
			Label value = new Label
			{
				Text = "◤ ตารางสีพรีเซ็ตนีออน : NEON_MATRIX ◢",
				ForeColor = Color.Gray,
				Font = new Font("Impact", 8f),
				Location = new Point(25, 235)
			};
			base.Controls.Add(value);
			this.\u00A0 = new FlowLayoutPanel
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
				\u2053.\u00A0 u00A = new \u2053.\u00A0();
				u00A.\u00A0 = this;
				u00A.\u00A0 = array[i];
				Button button = new Button
				{
					Size = new Size(45, 45),
					BackColor = u00A.\u00A0,
					FlatStyle = FlatStyle.Flat,
					Cursor = Cursors.Hand
				};
				button.FlatAppearance.BorderSize = 1;
				button.FlatAppearance.BorderColor = Color.FromArgb(40, 40, 45);
				button.Click += u00A.\u00A0;
				this.Attr_2.Controls.Add(button);
			}
			base.Controls.Add(this.\u00A0);
			this.\u1680 = new Button
			{
				Text = "◢ บันทึกสีที่เลือกเข้าสู่ระบบหลัก ◣",
				Location = new Point(25, 445),
				Size = new Size(430, 50),
				BackColor = Color.FromArgb(15, 15, 20),
				ForeColor = this.\u1680,
				FlatStyle = FlatStyle.Flat,
				Font = new Font("Impact", 11f)
			};
			this.Attr_3.FlatAppearance.BorderSize = 1;
			this.Attr_3.FlatAppearance.BorderColor = this.\u1680;
			this.Attr_3.Click += this.\u1680;
			base.Controls.Add(this.\u1680);
		}

		// Token: 0x06000178 RID: 376 RVA: 0x0000C270 File Offset: 0x0000A470
		private TrackBar \u00A0(int A_1, int A_2, Color A_3, int A_4, Action<int> A_5)
		{
			\u2053.\u1680 u = new \u2053.\u1680();
			u.\u00A0 = A_5;
			u.\u00A0 = new TrackBar
			{
				Location = new Point(A_1, A_2),
				Width = 250,
				Maximum = 255,
				Value = A_4,
				BackColor = this.\u2000,
				TickStyle = TickStyle.None
			};
			u.Attr_2.Scroll += u.\u00A0;
			base.Controls.Add(u.\u00A0);
			return u.\u00A0;
		}

		// Token: 0x06000179 RID: 377 RVA: 0x0000C304 File Offset: 0x0000A504
		private void \u2001()
		{
			this.\u00A0(Color.FromArgb(this.\u00A0, this.\u1680, this.\u2000));
			this.Attr_3.Text = "R: " + this.Attr_2.ToString();
			this.Form_4.Text = "G: " + this.Attr_3.ToString();
			this.Attr_5.Text = "B: " + this.Form_4.ToString();
			this.Attr_3.Invalidate();
			this.Attr_3.ForeColor = this.\u00A0();
			this.Attr_3.FlatAppearance.BorderColor = this.\u00A0();
		}

		// Token: 0x0600017A RID: 378 RVA: 0x0000C3C0 File Offset: 0x0000A5C0
		private void \u2002()
		{
			this.\u00A0 = new Timer
			{
				Interval = 40
			};
			this.Attr_2.Tick += this.\u2000;
			this.Attr_2.Start();
		}

		// Token: 0x0600017B RID: 379 RVA: 0x0000C3F8 File Offset: 0x0000A5F8
		protected override void OnPaint(PaintEventArgs A_1)
		{
			base.OnPaint(A_1);
			using (Pen pen = new Pen(Color.FromArgb(40, 40, 45), 1f))
			{
				A_1.Graphics.DrawRectangle(pen, 0, 0, base.Width - 1, base.Height - 1);
			}
			using (Pen pen2 = new Pen(this.\u1680, 2f))
			{
				int num = 20;
				A_1.Graphics.DrawLine(pen2, 5, 5, 5 + num, 5);
				A_1.Graphics.DrawLine(pen2, 5, 5, 5, 5 + num);
				A_1.Graphics.DrawLine(pen2, base.Width - 5, base.Height - 5, base.Width - 5 - num, base.Height - 5);
				A_1.Graphics.DrawLine(pen2, base.Width - 5, base.Height - 5, base.Width - 5, base.Height - 5 - num);
			}
		}

		// Token: 0x0600017C RID: 380 RVA: 0x0000C508 File Offset: 0x0000A708
		[CompilerGenerated]
		private void \u00A0(object A_1, MouseEventArgs A_2)
		{
			\u2053.\u1680();
			\u2053.\u00A0(base.Handle, 274, 61458, 0);
		}

		// Token: 0x0600017D RID: 381 RVA: 0x0000C528 File Offset: 0x0000A728
		[CompilerGenerated]
		private void \u00A0(object A_1, PaintEventArgs A_2)
		{
			A_2.Graphics.DrawLine(new Pen(this.\u1680, 2f), 0, 49, base.Width, 49);
			using (LinearGradientBrush linearGradientBrush = new LinearGradientBrush(new Rectangle(0, 44, base.Width, 6), Color.Transparent, Color.FromArgb(60, this.\u1680), 90f))
			{
				A_2.Graphics.FillRectangle(linearGradientBrush, 0, 44, base.Width, 6);
			}
		}

		// Token: 0x0600017E RID: 382 RVA: 0x0000C5BC File Offset: 0x0000A7BC
		[CompilerGenerated]
		private void \u00A0(object A_1, EventArgs A_2)
		{
			base.DialogResult = DialogResult.Cancel;
			base.Close();
		}

		// Token: 0x0600017F RID: 383 RVA: 0x0000C5CC File Offset: 0x0000A7CC
		[CompilerGenerated]
		private void \u1680(object A_1, PaintEventArgs A_2)
		{
			A_2.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
			Rectangle rect = new Rectangle(10, 10, 130, 130);
			using (SolidBrush solidBrush = new SolidBrush(this.\u00A0()))
			{
				A_2.Graphics.FillEllipse(solidBrush, rect);
			}
			using (Pen pen = new Pen(Color.FromArgb((int)(this.\u00A0 * 255f), this.\u1680), 3f))
			{
				A_2.Graphics.DrawEllipse(pen, rect);
			}
			using (Pen pen2 = new Pen(Color.FromArgb(100, Color.White), 1f))
			{
				A_2.Graphics.DrawArc(pen2, rect, 0f, 90f);
				A_2.Graphics.DrawArc(pen2, rect, 180f, 90f);
			}
		}

		// Token: 0x06000180 RID: 384 RVA: 0x0000C6D4 File Offset: 0x0000A8D4
		[CompilerGenerated]
		private void \u00A0(int A_1)
		{
			this.\u00A0 = A_1;
			this.\u2001();
		}

		// Token: 0x06000181 RID: 385 RVA: 0x0000C6E3 File Offset: 0x0000A8E3
		[CompilerGenerated]
		private void \u1680(int A_1)
		{
			this.\u1680 = A_1;
			this.\u2001();
		}

		// Token: 0x06000182 RID: 386 RVA: 0x0000C6F2 File Offset: 0x0000A8F2
		[CompilerGenerated]
		private void \u2000(int A_1)
		{
			this.\u2000 = A_1;
			this.\u2001();
		}

		// Token: 0x06000183 RID: 387 RVA: 0x0000C701 File Offset: 0x0000A901
		[CompilerGenerated]
		private void \u1680(object A_1, EventArgs A_2)
		{
			base.DialogResult = DialogResult.OK;
			base.Close();
		}

		// Token: 0x06000184 RID: 388 RVA: 0x0000C710 File Offset: 0x0000A910
		[CompilerGenerated]
		private void \u2000(object A_1, EventArgs A_2)
		{
			if (this.\u00A0)
			{
				this.\u00A0 += 0.05f;
				if (this.\u00A0 >= 1f)
				{
					this.\u00A0 = false;
				}
			}
			else
			{
				this.\u00A0 -= 0.05f;
				if (this.\u00A0 <= 0.3f)
				{
					this.\u00A0 = true;
				}
			}
			this.Attr_3.Invalidate();
		}

		// Token: 0x040000E0 RID: 224
		[CompilerGenerated]
		private Color \u00A0;

		// Token: 0x040000E1 RID: 225
		private Color \u1680 = Color.FromArgb(220, 20, 20);

		// Token: 0x040000E2 RID: 226
		private Color \u2000 = Color.FromArgb(8, 8, 10);

		// Token: 0x040000E3 RID: 227
		private int \u00A0 = 255;

		// Token: 0x040000E4 RID: 228
		private int \u1680;

		// Token: 0x040000E5 RID: 229
		private int \u2000;

		// Token: 0x040000E6 RID: 230
		private Panel \u00A0;

		// Token: 0x040000E7 RID: 231
		private Label \u00A0;

		// Token: 0x040000E8 RID: 232
		private Button \u00A0;

		// Token: 0x040000E9 RID: 233
		private Panel \u1680;

		// Token: 0x040000EA RID: 234
		private FlowLayoutPanel \u00A0;

		// Token: 0x040000EB RID: 235
		private TrackBar \u00A0;

		// Token: 0x040000EC RID: 236
		private TrackBar \u1680;

		// Token: 0x040000ED RID: 237
		private TrackBar \u2000;

		// Token: 0x040000EE RID: 238
		private Label \u1680;

		// Token: 0x040000EF RID: 239
		private Label \u2000;

		// Token: 0x040000F0 RID: 240
		private Label \u2001;

		// Token: 0x040000F1 RID: 241
		private Button \u1680;

		// Token: 0x040000F2 RID: 242
		private Timer \u00A0;

		// Token: 0x040000F3 RID: 243
		private float \u00A0 = 1f;

		// Token: 0x040000F4 RID: 244
		private bool \u00A0;

		// Token: 0x0200009B RID: 155
		[CompilerGenerated]
		private sealed class Attr_2
		{
			// Token: 0x06000186 RID: 390 RVA: 0x0000C780 File Offset: 0x0000A980
			internal void \u00A0(object A_1, EventArgs A_2)
			{
				this.Attr_2.\u00A0(this.\u00A0);
				this.Attr_2.\u00A0 = (int)this.Attr_2.R;
				this.Attr_2.\u1680 = (int)this.Attr_2.G;
				this.Attr_2.\u2000 = (int)this.Attr_2.B;
				this.Attr_2.Attr_2.Value = this.Attr_2.\u00A0;
				this.Attr_2.Attr_3.Value = this.Attr_2.\u1680;
				this.Attr_2.Form_4.Value = this.Attr_2.\u2000;
				this.Attr_2.\u2001();
				Console.Beep(1200, 50);
			}

			// Token: 0x040000F5 RID: 245
			public Color \u00A0;

			// Token: 0x040000F6 RID: 246
			public \u2053 \u00A0;
		}

		// Token: 0x0200009C RID: 156
		[CompilerGenerated]
		private sealed class Attr_3
		{
			// Token: 0x06000188 RID: 392 RVA: 0x0000C848 File Offset: 0x0000AA48
			internal void \u00A0(object A_1, EventArgs A_2)
			{
				this.\u00A0(this.Attr_2.Value);
			}

			// Token: 0x040000F7 RID: 247
			public Action<int> \u00A0;

			// Token: 0x040000F8 RID: 248
			public TrackBar \u00A0;
		}
	}
}
