using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace ns1
{
	// Token: 0x020000AF RID: 175
	public partial class GForm10 : Form
	{
		// Token: 0x0600020B RID: 523
		[DllImport("Gdi32.dll")]
		private static extern IntPtr CreateRoundRectRgn(int int_1, int int_2, int int_3, int int_4, int int_5, int int_6);

		// Token: 0x0600020C RID: 524 RVA: 0x000263A4 File Offset: 0x000245A4
		public GForm10()
		{
			base.Size = new Size(500, 450);
			base.FormBorderStyle = FormBorderStyle.None;
			base.StartPosition = FormStartPosition.CenterParent;
			this.BackColor = Color.FromArgb(15, 15, 15);
			this.DoubleBuffered = true;
			base.TopMost = true;
			try
			{
				IntPtr intPtr = GForm10.CreateRoundRectRgn(0, 0, base.Width, base.Height, 30, 30);
				if (intPtr != IntPtr.Zero)
				{
					base.Region = Region.FromHrgn(intPtr);
				}
			}
			catch
			{
			}
			this.font_0 = new Font("Bahnschrift SemiBold", 14f, FontStyle.Bold);
			this.font_1 = new Font("Leelawadee UI", 9f, FontStyle.Bold);
			this.font_2 = new Font("Consolas", 18f, FontStyle.Bold);
			this.method_0();
		}

		// Token: 0x0600020D RID: 525 RVA: 0x000264BC File Offset: 0x000246BC
		private void method_0()
		{
			Button button = new Button
			{
				Text = "×",
				Size = new Size(30, 30),
				Location = new Point(base.Width - 40, 10),
				FlatStyle = FlatStyle.Flat,
				ForeColor = Color.Gray,
				BackColor = Color.Transparent,
				Font = new Font("Arial", 16f, FontStyle.Bold)
			};
			button.FlatAppearance.BorderSize = 0;
			button.Click += this.method_5;
			base.Controls.Add(button);
			this.method_1("TARGET HP", 100, 80, new Action<float>(this.method_6));
			this.method_1("CYLINDERS", 100, 140, new Action<float>(this.method_7));
			this.method_2("FUEL", 300, 80, new string[]
			{
				"Gasoline",
				"E85"
			}, new Action<string>(this.method_8));
			this.method_2("ASPI", 300, 140, new string[]
			{
				"NA",
				"Turbo"
			}, new Action<string>(this.method_9));
		}

		// Token: 0x0600020E RID: 526 RVA: 0x00026600 File Offset: 0x00024800
		private void method_1(string string_2, int int_1, int int_2, Action<float> action_0)
		{
			GForm10.Class138 @class = new GForm10.Class138();
			@class.action_0 = action_0;
			@class.textBox_0 = new TextBox
			{
				Location = new Point(int_1, int_2 + 20),
				Size = new Size(120, 30),
				BackColor = Color.FromArgb(30, 30, 30),
				ForeColor = Color.White,
				BorderStyle = BorderStyle.FixedSingle,
				Font = this.font_2,
				Text = ((string_2 == "CYLINDERS") ? "4" : "300")
			};
			@class.textBox_0.TextChanged += @class.method_0;
			Label value = new Label
			{
				Text = string_2,
				Location = new Point(int_1, int_2),
				ForeColor = Color.Gray,
				Font = this.font_1,
				AutoSize = true
			};
			base.Controls.Add(@class.textBox_0);
			base.Controls.Add(value);
		}

		// Token: 0x0600020F RID: 527 RVA: 0x00026700 File Offset: 0x00024900
		private void method_2(string string_2, int int_1, int int_2, string[] string_3, Action<string> action_0)
		{
			GForm10.Class139 @class = new GForm10.Class139();
			@class.action_0 = action_0;
			@class.comboBox_0 = new ComboBox
			{
				Location = new Point(int_1, int_2 + 20),
				Size = new Size(120, 30),
				BackColor = Color.FromArgb(30, 30, 30),
				ForeColor = Color.White,
				FlatStyle = FlatStyle.Flat,
				Font = this.font_1,
				DropDownStyle = ComboBoxStyle.DropDownList
			};
			@class.comboBox_0.Items.AddRange(string_3);
			@class.comboBox_0.SelectedIndex = 0;
			@class.comboBox_0.SelectedIndexChanged += @class.method_0;
			Label value = new Label
			{
				Text = string_2,
				Location = new Point(int_1, int_2),
				ForeColor = Color.Gray,
				Font = this.font_1,
				AutoSize = true
			};
			base.Controls.Add(@class.comboBox_0);
			base.Controls.Add(value);
		}

		// Token: 0x06000210 RID: 528 RVA: 0x00026808 File Offset: 0x00024A08
		protected override void OnPaint(PaintEventArgs pevent)
		{
			Graphics graphics = pevent.Graphics;
			graphics.SmoothingMode = SmoothingMode.AntiAlias;
			using (Pen pen = new Pen(Color.FromArgb(20, 255, 255, 255), 1f))
			{
				graphics.DrawLine(pen, 20, 50, base.Width - 20, 50);
				graphics.DrawLine(pen, 50, 0, 50, base.Height);
			}
			graphics.DrawString("INJECTOR FLOW CALCULATOR", this.font_0, Brushes.IndianRed, 65f, 15f);
			float num = (this.string_1 == "NA") ? 0.5f : 0.65f;
			if (this.string_0 == "E85")
			{
				num *= 1.35f;
			}
			double num2 = (double)this.float_1 / 100.0;
			double num3 = (double)(this.float_0 * num) / ((double)this.int_0 * num2);
			double num4 = num3 * 10.5;
			this.method_3(graphics, "REQUIRED CC/MIN", string.Format("{0:F0}", num4), Color.FromArgb(0, 255, 120), 65, 230);
			this.method_3(graphics, "REQUIRED LB/HR", string.Format("{0:F1}", num3), Color.FromArgb(255, 200, 0), 285, 230);
			int num5 = 50;
			int num6 = 65;
			int num7 = 330;
			graphics.FillRectangle(new SolidBrush(Color.FromArgb(30, 30, 30)), 65, 330, 370, 50);
			int num8 = (int)(Math.Min(1f, (float)(num4 / 2200.0)) * 370f);
			using (LinearGradientBrush linearGradientBrush = new LinearGradientBrush(new Rectangle(65, 330, num8 + 1, 50), Color.FromArgb(0, 255, 120), Color.FromArgb(255, 30, 30), 0f))
			{
				graphics.FillRectangle(linearGradientBrush, num6, num7, num8, num5);
			}
			graphics.DrawString("FLOW SCALE (0 - 2200cc)", this.font_1, Brushes.DimGray, (float)num6, (float)(num7 + num5 + 5));
			using (Pen pen2 = new Pen(Color.FromArgb(100, 231, 76, 60), 2f))
			{
				graphics.DrawPath(pen2, this.method_4(new Rectangle(1, 1, base.Width - 2, base.Height - 2), 30));
			}
		}

		// Token: 0x06000211 RID: 529 RVA: 0x00026AB4 File Offset: 0x00024CB4
		private void method_3(Graphics graphics_0, string string_2, string string_3, Color color_0, int int_1, int int_2)
		{
			graphics_0.DrawString(string_2, this.font_1, Brushes.Gray, (float)int_1, (float)int_2);
			using (SolidBrush solidBrush = new SolidBrush(Color.FromArgb(100, 255, 0, 0)))
			{
				graphics_0.DrawString(string_3, this.font_2, solidBrush, (float)(int_1 - 1), (float)(int_2 + 20));
			}
			using (SolidBrush solidBrush2 = new SolidBrush(Color.FromArgb(100, 0, 255, 255)))
			{
				graphics_0.DrawString(string_3, this.font_2, solidBrush2, (float)(int_1 + 1), (float)(int_2 + 20));
			}
			graphics_0.DrawString(string_3, this.font_2, new SolidBrush(color_0), (float)int_1, (float)(int_2 + 20));
		}

		// Token: 0x06000212 RID: 530 RVA: 0x00026B88 File Offset: 0x00024D88
		private GraphicsPath method_4(Rectangle rectangle_0, int int_1)
		{
			GraphicsPath graphicsPath = new GraphicsPath();
			graphicsPath.AddArc(rectangle_0.X, rectangle_0.Y, int_1, int_1, 180f, 90f);
			graphicsPath.AddArc(rectangle_0.Right - int_1, rectangle_0.Y, int_1, int_1, 270f, 90f);
			graphicsPath.AddArc(rectangle_0.Right - int_1, rectangle_0.Bottom - int_1, int_1, int_1, 0f, 90f);
			graphicsPath.AddArc(rectangle_0.X, rectangle_0.Bottom - int_1, int_1, int_1, 90f, 90f);
			graphicsPath.CloseFigure();
			return graphicsPath;
		}

		// Token: 0x06000214 RID: 532 RVA: 0x0000C305 File Offset: 0x0000A505
		[CompilerGenerated]
		private void method_5(object sender, EventArgs e)
		{
			base.Close();
		}

		// Token: 0x06000215 RID: 533 RVA: 0x0000CFA3 File Offset: 0x0000B1A3
		[CompilerGenerated]
		private void method_6(float float_2)
		{
			this.float_0 = float_2;
			base.Invalidate();
		}

		// Token: 0x06000216 RID: 534 RVA: 0x0000CFB2 File Offset: 0x0000B1B2
		[CompilerGenerated]
		private void method_7(float float_2)
		{
			this.int_0 = (int)float_2;
			base.Invalidate();
		}

		// Token: 0x06000217 RID: 535 RVA: 0x0000CFC2 File Offset: 0x0000B1C2
		[CompilerGenerated]
		private void method_8(string string_2)
		{
			this.string_0 = string_2;
			base.Invalidate();
		}

		// Token: 0x06000218 RID: 536 RVA: 0x0000CFD1 File Offset: 0x0000B1D1
		[CompilerGenerated]
		private void method_9(string string_2)
		{
			this.string_1 = string_2;
			base.Invalidate();
		}

		// Token: 0x04000171 RID: 369
		private float float_0 = 300f;

		// Token: 0x04000172 RID: 370
		private int int_0 = 4;

		// Token: 0x04000173 RID: 371
		private float float_1 = 80f;

		// Token: 0x04000174 RID: 372
		private string string_0 = "Gasoline";

		// Token: 0x04000175 RID: 373
		private string string_1 = "NA";

		// Token: 0x020000B0 RID: 176
		[CompilerGenerated]
		private sealed class Class138
		{
			// Token: 0x0600021A RID: 538 RVA: 0x00026C28 File Offset: 0x00024E28
			internal void method_0(object sender, EventArgs e)
			{
				float obj;
				if (float.TryParse(this.textBox_0.Text, out obj))
				{
					this.action_0(obj);
				}
			}

			// Token: 0x04000179 RID: 377
			public TextBox textBox_0;

			// Token: 0x0400017A RID: 378
			public Action<float> action_0;
		}

		// Token: 0x020000B1 RID: 177
		[CompilerGenerated]
		private sealed class Class139
		{
			// Token: 0x0600021C RID: 540 RVA: 0x0000CFE0 File Offset: 0x0000B1E0
			internal void method_0(object sender, EventArgs e)
			{
				this.action_0(this.comboBox_0.SelectedItem.ToString());
			}

			// Token: 0x0400017B RID: 379
			public Action<string> action_0;

			// Token: 0x0400017C RID: 380
			public ComboBox comboBox_0;
		}
	}
}
