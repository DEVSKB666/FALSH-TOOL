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
	// Token: 0x020000D9 RID: 217
	public partial class GForm13 : Form
	{
		// Token: 0x060003B7 RID: 951
		[DllImport("user32.dll")]
		public static extern bool ReleaseCapture();

		// Token: 0x060003B8 RID: 952
		[DllImport("user32.dll")]
		public static extern int SendMessage(IntPtr intptr_0, int int_0, int int_1, int int_2);

		// Token: 0x060003B9 RID: 953 RVA: 0x000351BC File Offset: 0x000333BC
		public GForm13()
		{
			base.Size = new Size(450, 300);
			base.FormBorderStyle = FormBorderStyle.None;
			this.BackColor = this.color_0;
			base.StartPosition = FormStartPosition.CenterParent;
			this.DoubleBuffered = true;
			base.Region = Region.FromHrgn(GForm13.CreateRoundRectRgn(0, 0, base.Width, base.Height, 20, 20));
			this.method_0();
		}

		// Token: 0x060003BA RID: 954 RVA: 0x00035260 File Offset: 0x00033460
		private void method_0()
		{
			GForm13.Class153 @class = new GForm13.Class153();
			@class.gform13_0 = this;
			@class.panel_0 = new Panel
			{
				Size = new Size(base.Width, 45),
				Location = new Point(0, 0),
				BackColor = this.color_1
			};
			@class.panel_0.MouseDown += @class.method_0;
			@class.panel_0.Paint += @class.method_1;
			Label label = new Label
			{
				Text = "✕",
				Font = new Font("Segoe UI", 13f, FontStyle.Bold),
				ForeColor = Color.White,
				BackColor = Color.Transparent,
				AutoSize = true,
				Cursor = Cursors.Hand,
				Location = new Point(base.Width - 30, 10)
			};
			label.Click += @class.method_2;
			@class.panel_0.Controls.Add(label);
			base.Controls.Add(@class.panel_0);
			this.radioButton_0 = new RadioButton
			{
				Text = "48KB ROM",
				ForeColor = Color.LightGray,
				Font = new Font("Segoe UI", 10f, FontStyle.Bold),
				Location = new Point(110, 65),
				AutoSize = true,
				Cursor = Cursors.Hand
			};
			this.radioButton_0.CheckedChanged += @class.method_3;
			this.radioButton_1 = new RadioButton
			{
				Text = "64KB ROM",
				ForeColor = Color.LightGray,
				Font = new Font("Segoe UI", 10f, FontStyle.Bold),
				Location = new Point(250, 65),
				AutoSize = true,
				Checked = true,
				Cursor = Cursors.Hand
			};
			this.radioButton_1.CheckedChanged += @class.method_4;
			base.Controls.Add(this.radioButton_0);
			base.Controls.Add(this.radioButton_1);
			Panel value = this.method_1(new Point(35, 110), "2000", out this.textBox_0);
			base.Controls.Add(value);
			Panel value2 = this.method_1(new Point(245, 110), "2000", out this.textBox_1);
			base.Controls.Add(value2);
			@class.control0_0 = new GForm13.Control0
			{
				Text = "\ud83d\udcc2 เลือกไฟล์ .bin (ต้นฉบับ)",
				Location = new Point(35, 157),
				Size = new Size(380, 36)
			};
			@class.control0_0.Click += @class.method_5;
			base.Controls.Add(@class.control0_0);
			GForm13.Control0 control = new GForm13.Control0
			{
				Text = "[-] ใช้งาน (แพทช์ข้อมูล)",
				Location = new Point(35, 203),
				Size = new Size(380, 36)
			};
			control.Click += @class.method_6;
			base.Controls.Add(control);
			GForm13.Control0 control2 = new GForm13.Control0
			{
				Text = "[+] บันทึกไฟล์ออก",
				Location = new Point(35, 249),
				Size = new Size(380, 36)
			};
			control2.Click += @class.method_7;
			base.Controls.Add(control2);
		}

		// Token: 0x060003BB RID: 955 RVA: 0x000355E4 File Offset: 0x000337E4
		protected override void OnPaint(PaintEventArgs pevent)
		{
			base.OnPaint(pevent);
			pevent.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
			using (Pen pen = new Pen(Color.FromArgb(22, 22, 25), 1f))
			{
				for (int i = 0; i < base.Width; i += 25)
				{
					pevent.Graphics.DrawLine(pen, i, 45, i, base.Height);
				}
				for (int j = 45; j < base.Height; j += 25)
				{
					pevent.Graphics.DrawLine(pen, 0, j, base.Width, j);
				}
			}
			using (GraphicsPath graphicsPath = GForm13.smethod_0(new Rectangle(0, 0, base.Width - 1, base.Height - 1), 20))
			{
				using (Pen pen2 = new Pen(Color.FromArgb(120, 220, 25, 35), 2f))
				{
					pevent.Graphics.DrawPath(pen2, graphicsPath);
				}
			}
		}

		// Token: 0x060003BC RID: 956 RVA: 0x00035700 File Offset: 0x00033900
		private Panel method_1(Point point_0, string string_1, out TextBox textBox_2)
		{
			GForm13.Class154 @class = new GForm13.Class154();
			@class.panel_0 = new Panel
			{
				Size = new Size(170, 35),
				Location = point_0,
				BackColor = this.color_0
			};
			@class.panel_0.Paint += @class.method_0;
			textBox_2 = new TextBox
			{
				Text = string_1,
				BorderStyle = BorderStyle.None,
				BackColor = Color.White,
				ForeColor = Color.Black,
				Font = new Font("Segoe UI", 12f, FontStyle.Bold),
				TextAlign = HorizontalAlignment.Center,
				Size = new Size(160, 25),
				Location = new Point(5, 7)
			};
			@class.panel_0.Controls.Add(textBox_2);
			return @class.panel_0;
		}

		// Token: 0x060003BD RID: 957 RVA: 0x000357DC File Offset: 0x000339DC
		private void method_2()
		{
			if (this.byte_0 == null)
			{
				return;
			}
			try
			{
				if (this.radioButton_0.Checked && this.byte_0.Length >= 36628)
				{
					ushort num = (ushort)((int)this.byte_0[36626] | (int)this.byte_0[36627] << 8);
					ushort num2 = (ushort)((int)this.byte_0[36628] | (int)this.byte_0[36629] << 8);
					this.textBox_0.Text = Math.Round((double)num * 0.274653, 0).ToString();
					this.textBox_1.Text = Math.Round((double)num2 * 0.274653, 0).ToString();
				}
				else if (this.radioButton_1.Checked && this.byte_0.Length >= 39337)
				{
					ushort num3 = (ushort)((int)this.byte_0[39333] | (int)this.byte_0[39334] << 8);
					ushort num4 = (ushort)((int)this.byte_0[39335] | (int)this.byte_0[39336] << 8);
					this.textBox_0.Text = Math.Round((double)num3 * 0.274653, 0).ToString();
					this.textBox_1.Text = Math.Round((double)num4 * 0.274653, 0).ToString();
				}
			}
			catch
			{
			}
		}

		// Token: 0x060003BE RID: 958 RVA: 0x00035968 File Offset: 0x00033B68
		private static GraphicsPath smethod_0(Rectangle rectangle_0, int int_0)
		{
			int num = int_0 * 2;
			GraphicsPath graphicsPath = new GraphicsPath();
			graphicsPath.AddArc(rectangle_0.X, rectangle_0.Y, num, num, 180f, 90f);
			graphicsPath.AddArc(rectangle_0.X + rectangle_0.Width - num, rectangle_0.Y, num, num, 270f, 90f);
			graphicsPath.AddArc(rectangle_0.X + rectangle_0.Width - num, rectangle_0.Y + rectangle_0.Height - num, num, num, 0f, 90f);
			graphicsPath.AddArc(rectangle_0.X, rectangle_0.Y + rectangle_0.Height - num, num, num, 90f, 90f);
			graphicsPath.CloseFigure();
			return graphicsPath;
		}

		// Token: 0x060003BF RID: 959
		[DllImport("Gdi32.dll")]
		private static extern IntPtr CreateRoundRectRgn(int int_0, int int_1, int int_2, int int_3, int int_4, int int_5);

		// Token: 0x040002F2 RID: 754
		private Color color_0 = Color.FromArgb(17, 17, 19);

		// Token: 0x040002F3 RID: 755
		private Color color_1 = Color.FromArgb(220, 25, 35);

		// Token: 0x040002F4 RID: 756
		public TextBox textBox_0;

		// Token: 0x040002F5 RID: 757
		public TextBox textBox_1;

		// Token: 0x040002F6 RID: 758
		private RadioButton radioButton_0;

		// Token: 0x040002F7 RID: 759
		private RadioButton radioButton_1;

		// Token: 0x040002F8 RID: 760
		private byte[] byte_0;

		// Token: 0x040002F9 RID: 761
		private string string_0 = "";

		// Token: 0x020000DA RID: 218
		private class Control0 : Control
		{
			// Token: 0x060003C0 RID: 960 RVA: 0x0000D8E3 File Offset: 0x0000BAE3
			public Control0()
			{
				this.DoubleBuffered = true;
				this.Cursor = Cursors.Hand;
			}

			// Token: 0x060003C1 RID: 961 RVA: 0x0000D8FD File Offset: 0x0000BAFD
			protected override void OnMouseEnter(EventArgs e)
			{
				this.bool_0 = true;
				base.Invalidate();
				base.OnMouseEnter(e);
			}

			// Token: 0x060003C2 RID: 962 RVA: 0x0000D913 File Offset: 0x0000BB13
			protected override void OnMouseLeave(EventArgs e)
			{
				this.bool_0 = false;
				base.Invalidate();
				base.OnMouseLeave(e);
			}

			// Token: 0x060003C3 RID: 963 RVA: 0x00035A2C File Offset: 0x00033C2C
			protected override void OnPaint(PaintEventArgs pevent)
			{
				base.OnPaint(pevent);
				pevent.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
				Rectangle rectangle = new Rectangle(0, 0, base.Width - 1, base.Height - 1);
				Color color = this.bool_0 ? Color.FromArgb(255, 45, 55) : Color.FromArgb(235, 30, 40);
				Color color2 = this.bool_0 ? Color.FromArgb(210, 20, 30) : Color.FromArgb(200, 20, 30);
				Color color3 = this.bool_0 ? Color.FromArgb(255, 100, 100) : Color.FromArgb(255, 60, 70);
				using (GraphicsPath graphicsPath = GForm13.smethod_0(rectangle, 8))
				{
					using (LinearGradientBrush linearGradientBrush = new LinearGradientBrush(rectangle, color, color2, 90f))
					{
						pevent.Graphics.FillPath(linearGradientBrush, graphicsPath);
					}
					pevent.Graphics.DrawPath(new Pen(color3, this.bool_0 ? 2f : 1f), graphicsPath);
				}
				using (Font font = new Font(EC2D41B1-A2F9-4664-90D8-86645EE2E753.smethod_200(), 9.5f, FontStyle.Bold))
				{
					StringFormat format = new StringFormat
					{
						Alignment = StringAlignment.Center,
						LineAlignment = StringAlignment.Center
					};
					pevent.Graphics.DrawString(this.Text, font, Brushes.White, rectangle, format);
				}
			}

			// Token: 0x040002FA RID: 762
			private bool bool_0;
		}

		// Token: 0x020000DB RID: 219
		[CompilerGenerated]
		private sealed class Class153
		{
			// Token: 0x060003C5 RID: 965 RVA: 0x0000D929 File Offset: 0x0000BB29
			internal void method_0(object sender, MouseEventArgs e)
			{
				if (e.Button == MouseButtons.Left)
				{
					GForm13.ReleaseCapture();
					GForm13.SendMessage(this.gform13_0.Handle, 161, 2, 0);
				}
			}

			// Token: 0x060003C6 RID: 966 RVA: 0x00035BC4 File Offset: 0x00033DC4
			internal void method_1(object sender, PaintEventArgs e)
			{
				e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
				using (GraphicsPath graphicsPath = GForm13.smethod_0(new Rectangle(12, 8, 26, 28), 6))
				{
					e.Graphics.FillPath(Brushes.White, graphicsPath);
				}
				using (Font font = new Font(EC2D41B1-A2F9-4664-90D8-86645EE2E753.smethod_40(), 14f, FontStyle.Bold))
				{
					e.Graphics.DrawString(EC2D41B1-A2F9-4664-90D8-86645EE2E753.smethod_765(), font, new SolidBrush(this.gform13_0.color_1), 13f, 11f);
				}
				using (Font font2 = new Font(EC2D41B1-A2F9-4664-90D8-86645EE2E753.smethod_200(), 10.5f, FontStyle.Bold))
				{
					e.Graphics.DrawString(EC2D41B1-A2F9-4664-90D8-86645EE2E753.smethod_971(), font2, Brushes.White, 48f, 12f);
				}
				using (Pen pen = new Pen(Color.FromArgb(80, 255, 255, 255), 2f))
				{
					e.Graphics.DrawLine(pen, this.panel_0.Width - 120, 18, this.panel_0.Width - 60, 18);
					e.Graphics.DrawLine(pen, this.panel_0.Width - 140, 24, this.panel_0.Width - 70, 24);
					e.Graphics.FillRectangle(Brushes.White, this.panel_0.Width - 55, 16, 4, 10);
				}
			}

			// Token: 0x060003C7 RID: 967 RVA: 0x0000D956 File Offset: 0x0000BB56
			internal void method_2(object sender, EventArgs e)
			{
				this.gform13_0.Close();
			}

			// Token: 0x060003C8 RID: 968 RVA: 0x0000D963 File Offset: 0x0000BB63
			internal void method_3(object sender, EventArgs e)
			{
				if (this.gform13_0.radioButton_0.Checked)
				{
					this.gform13_0.method_2();
				}
			}

			// Token: 0x060003C9 RID: 969 RVA: 0x0000D982 File Offset: 0x0000BB82
			internal void method_4(object sender, EventArgs e)
			{
				if (this.gform13_0.radioButton_1.Checked)
				{
					this.gform13_0.method_2();
				}
			}

			// Token: 0x060003CA RID: 970 RVA: 0x00035D74 File Offset: 0x00033F74
			internal void method_5(object sender, EventArgs e)
			{
				using (OpenFileDialog openFileDialog = new OpenFileDialog
				{
					Filter = EC2D41B1-A2F9-4664-90D8-86645EE2E753.smethod_972()
				})
				{
					if (openFileDialog.ShowDialog() == DialogResult.OK)
					{
						try
						{
							this.gform13_0.byte_0 = File.ReadAllBytes(openFileDialog.FileName);
							this.gform13_0.string_0 = Path.GetFileName(openFileDialog.FileName);
							this.control0_0.Text = EC2D41B1-A2F9-4664-90D8-86645EE2E753.smethod_973() + this.gform13_0.string_0;
							if (this.gform13_0.byte_0.Length <= 49152)
							{
								this.gform13_0.radioButton_0.Checked = true;
							}
							else if (this.gform13_0.byte_0.Length >= 65536)
							{
								this.gform13_0.radioButton_1.Checked = true;
							}
							this.gform13_0.method_2();
							MessageBox.Show(string.Format(EC2D41B1-A2F9-4664-90D8-86645EE2E753.smethod_974(), this.gform13_0.string_0, this.gform13_0.byte_0.Length / 1024), EC2D41B1-A2F9-4664-90D8-86645EE2E753.smethod_906(), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
						}
						catch (Exception ex)
						{
							MessageBox.Show(EC2D41B1-A2F9-4664-90D8-86645EE2E753.smethod_975() + ex.Message, EC2D41B1-A2F9-4664-90D8-86645EE2E753.smethod_976(), MessageBoxButtons.OK, MessageBoxIcon.Hand);
						}
					}
				}
			}

			// Token: 0x060003CB RID: 971 RVA: 0x00035EE0 File Offset: 0x000340E0
			internal void method_6(object sender, EventArgs e)
			{
				if (this.gform13_0.byte_0 == null)
				{
					MessageBox.Show(EC2D41B1-A2F9-4664-90D8-86645EE2E753.smethod_977(), EC2D41B1-A2F9-4664-90D8-86645EE2E753.smethod_629(), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					return;
				}
				if (this.gform13_0.radioButton_0.Checked)
				{
					if (this.gform13_0.byte_0.Length < 36630)
					{
						MessageBox.Show(EC2D41B1-A2F9-4664-90D8-86645EE2E753.smethod_978(), EC2D41B1-A2F9-4664-90D8-86645EE2E753.smethod_976(), MessageBoxButtons.OK, MessageBoxIcon.Hand);
						return;
					}
					try
					{
						double num = double.Parse(this.gform13_0.textBox_0.Text);
						double num2 = double.Parse(this.gform13_0.textBox_1.Text);
						ushort num3 = (ushort)Math.Round(num / 0.274653);
						ushort num4 = (ushort)Math.Round(num2 / 0.274653);
						this.gform13_0.byte_0[36626] = (byte)(num3 & 255);
						this.gform13_0.byte_0[36627] = (byte)(num3 >> 8 & 255);
						this.gform13_0.byte_0[36628] = (byte)(num4 & 255);
						this.gform13_0.byte_0[36629] = (byte)(num4 >> 8 & 255);
						MessageBox.Show(string.Format(EC2D41B1-A2F9-4664-90D8-86645EE2E753.smethod_979(), num3, num4), EC2D41B1-A2F9-4664-90D8-86645EE2E753.smethod_906(), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
						return;
					}
					catch
					{
						MessageBox.Show(EC2D41B1-A2F9-4664-90D8-86645EE2E753.smethod_980(), EC2D41B1-A2F9-4664-90D8-86645EE2E753.smethod_976(), MessageBoxButtons.OK, MessageBoxIcon.Hand);
						return;
					}
				}
				if (this.gform13_0.radioButton_1.Checked)
				{
					if (this.gform13_0.byte_0.Length < 39337)
					{
						MessageBox.Show(EC2D41B1-A2F9-4664-90D8-86645EE2E753.smethod_981(), EC2D41B1-A2F9-4664-90D8-86645EE2E753.smethod_976(), MessageBoxButtons.OK, MessageBoxIcon.Hand);
						return;
					}
					try
					{
						double num5 = double.Parse(this.gform13_0.textBox_0.Text);
						double num6 = double.Parse(this.gform13_0.textBox_1.Text);
						ushort num7 = (ushort)Math.Round(num5 / 0.274653);
						ushort num8 = (ushort)Math.Round(num6 / 0.274653);
						this.gform13_0.byte_0[39333] = (byte)(num7 & 255);
						this.gform13_0.byte_0[39334] = (byte)(num7 >> 8 & 255);
						this.gform13_0.byte_0[39335] = (byte)(num8 & 255);
						this.gform13_0.byte_0[39336] = (byte)(num8 >> 8 & 255);
						MessageBox.Show(string.Format(EC2D41B1-A2F9-4664-90D8-86645EE2E753.smethod_982(), num7, num8), EC2D41B1-A2F9-4664-90D8-86645EE2E753.smethod_906(), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
					}
					catch
					{
						MessageBox.Show(EC2D41B1-A2F9-4664-90D8-86645EE2E753.smethod_980(), EC2D41B1-A2F9-4664-90D8-86645EE2E753.smethod_976(), MessageBoxButtons.OK, MessageBoxIcon.Hand);
					}
				}
			}

			// Token: 0x060003CC RID: 972 RVA: 0x00036198 File Offset: 0x00034398
			internal void method_7(object sender, EventArgs e)
			{
				if (this.gform13_0.byte_0 == null)
				{
					MessageBox.Show(EC2D41B1-A2F9-4664-90D8-86645EE2E753.smethod_983(), EC2D41B1-A2F9-4664-90D8-86645EE2E753.smethod_629(), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					return;
				}
				using (SaveFileDialog saveFileDialog = new SaveFileDialog
				{
					Filter = EC2D41B1-A2F9-4664-90D8-86645EE2E753.smethod_886(),
					FileName = EC2D41B1-A2F9-4664-90D8-86645EE2E753.smethod_984() + this.gform13_0.string_0
				})
				{
					if (saveFileDialog.ShowDialog() == DialogResult.OK)
					{
						try
						{
							File.WriteAllBytes(saveFileDialog.FileName, this.gform13_0.byte_0);
							MessageBox.Show(EC2D41B1-A2F9-4664-90D8-86645EE2E753.smethod_985() + saveFileDialog.FileName, EC2D41B1-A2F9-4664-90D8-86645EE2E753.smethod_906(), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
						}
						catch (Exception ex)
						{
							MessageBox.Show(EC2D41B1-A2F9-4664-90D8-86645EE2E753.smethod_986() + ex.Message, EC2D41B1-A2F9-4664-90D8-86645EE2E753.smethod_976(), MessageBoxButtons.OK, MessageBoxIcon.Hand);
						}
					}
				}
			}

			// Token: 0x040002FB RID: 763
			public GForm13 gform13_0;

			// Token: 0x040002FC RID: 764
			public Panel panel_0;

			// Token: 0x040002FD RID: 765
			public GForm13.Control0 control0_0;
		}

		// Token: 0x020000DC RID: 220
		[CompilerGenerated]
		private sealed class Class154
		{
			// Token: 0x060003CE RID: 974 RVA: 0x0003627C File Offset: 0x0003447C
			internal void method_0(object sender, PaintEventArgs e)
			{
				e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
				using (GraphicsPath graphicsPath = GForm13.smethod_0(new Rectangle(0, 0, this.panel_0.Width - 1, this.panel_0.Height - 1), 10))
				{
					e.Graphics.FillPath(Brushes.White, graphicsPath);
				}
			}

			// Token: 0x040002FE RID: 766
			public Panel panel_0;
		}
	}
}
