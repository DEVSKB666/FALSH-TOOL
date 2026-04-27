using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace ns0
{
	// Token: 0x02000008 RID: 8
	public partial class GForm1 : Form
	{
		// Token: 0x0600001B RID: 27
		[DllImport("user32.dll")]
		public static extern bool ReleaseCapture();

		// Token: 0x0600001C RID: 28
		[DllImport("user32.dll")]
		public static extern int SendMessage(IntPtr intptr_0, int int_1, int int_2, int int_3);

		// Token: 0x0600001D RID: 29
		[DllImport("Gdi32.dll")]
		private static extern IntPtr CreateRoundRectRgn(int int_1, int int_2, int int_3, int int_4, int int_5, int int_6);

		// Token: 0x0600001E RID: 30 RVA: 0x00016970 File Offset: 0x00014B70
		public GForm1()
		{
			this.method_6();
			base.FormBorderStyle = FormBorderStyle.None;
			this.BackColor = this.color_1;
			base.Region = Region.FromHrgn(GForm1.CreateRoundRectRgn(0, 0, base.Width, base.Height, 20, 20));
			this.DoubleBuffered = true;
			this.method_0();
			this.method_4();
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00016A24 File Offset: 0x00014C24
		private void method_0()
		{
			base.Paint += this.GForm1_Paint;
			Button button = new Button
			{
				Text = "×",
				Location = new Point(base.Width - 45, 0),
				Size = new Size(45, 40),
				FlatStyle = FlatStyle.Flat,
				ForeColor = Color.Gray,
				Font = new Font("Arial", 16f, FontStyle.Bold)
			};
			button.FlatAppearance.BorderSize = 0;
			button.Click += this.method_7;
			base.Controls.Add(button);
			base.MouseDown += this.GForm1_MouseDown;
			this.method_2(this.textBox_0, "FILE_PATH_INPUT");
			this.method_2(this.textBox_2, "KEY_OPEN");
			this.method_2(this.textBox_3, "KEY_MODIFY");
			this.method_3(this.button_0, "◢ เลือกไฟล์ ◣");
			this.method_3(this.button_2, "◢ ปลดรหัส ◣");
			this.method_3(this.button_1, "◢ เซฟไฟล์ ◣");
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00016B44 File Offset: 0x00014D44
		private void method_1(Graphics graphics_0, Rectangle rectangle_0, string string_0)
		{
			using (Font font = new Font("Consolas", 7f, FontStyle.Bold))
			{
				graphics_0.DrawString("[" + string_0 + "]", font, new SolidBrush(Color.FromArgb(100, this.color_0)), (float)(rectangle_0.X + 2), (float)(rectangle_0.Y - 12));
			}
			using (Pen pen = new Pen(Color.FromArgb(50, this.color_0), 1f))
			{
				graphics_0.DrawRectangle(pen, rectangle_0);
			}
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00016BF4 File Offset: 0x00014DF4
		private void method_2(Control control_0, string string_0)
		{
			control_0.BackColor = Color.Black;
			control_0.ForeColor = Color.White;
			TextBox textBox = control_0 as TextBox;
			if (textBox != null)
			{
				textBox.BorderStyle = BorderStyle.FixedSingle;
			}
			control_0.Font = new Font("Consolas", 10f);
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00016C40 File Offset: 0x00014E40
		private void method_3(Button button_3, string string_0)
		{
			button_3.Text = string_0;
			button_3.FlatStyle = FlatStyle.Flat;
			button_3.BackColor = Color.FromArgb(20, 20, 25);
			button_3.ForeColor = this.color_0;
			button_3.FlatAppearance.BorderColor = this.color_0;
			button_3.Font = new Font("Impact", 10f);
			button_3.Cursor = Cursors.Hand;
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00016CAC File Offset: 0x00014EAC
		private void method_4()
		{
			this.timer_0 = new Timer
			{
				Interval = 30
			};
			this.timer_0.Tick += this.timer_0_Tick;
			this.timer_0.Start();
			this.timer_1 = new Timer
			{
				Interval = 50
			};
			this.timer_1.Tick += this.timer_1_Tick;
			this.timer_1.Start();
			base.Paint += this.GForm1_Paint_1;
		}

		// Token: 0x06000024 RID: 36 RVA: 0x0000C388 File Offset: 0x0000A588
		private void GForm1_Load(object sender, EventArgs e)
		{
			this.textBox_0.Text = string.Empty;
			this.textBox_1.Text = string.Empty;
			this.button_2.Enabled = false;
			this.button_1.Enabled = false;
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00016D38 File Offset: 0x00014F38
		public int method_5(string string_0, ref string string_1, ref string string_2, ref string string_3)
		{
			this.bool_0 = true;
			Application.DoEvents();
			UIntPtr zero = UIntPtr.Zero;
			UIntPtr zero2 = UIntPtr.Zero;
			UIntPtr zero3 = UIntPtr.Zero;
			string s = "$9Ad0$!#*)1QqPow^";
			byte[] bytes = Encoding.ASCII.GetBytes(s);
			int result;
			try
			{
				byte[] array = File.ReadAllBytes(string_0);
				if (array[0] == 5 && array[1] == 34 && array[2] == 151)
				{
					string[] array3;
					for (;;)
					{
						if (array[131] == 0)
						{
							GForm2 gform = new GForm2();
							gform.ShowDialog();
							if (string.IsNullOrWhiteSpace(gform.textBox_0.Text))
							{
								break;
							}
							s = gform.textBox_0.Text;
							bytes = Encoding.ASCII.GetBytes(s);
						}
						byte[] array2 = new byte[array.Length - 264];
						Array.Copy(array, 264, array2, 0, array.Length - 264);
						uint num = (uint)(array.Length - 264);
						if (!GClass1.CryptAcquireContext(ref zero, "TunerPro", "Microsoft Enhanced Cryptographic Provider v1.0", 1U, 0U))
						{
							goto IL_2D3;
						}
						if (!GClass1.CryptCreateHash(zero, 32771U, UIntPtr.Zero, 0U, ref zero2) || !GClass1.CryptHashData(zero2, bytes, (uint)bytes.Length, 0U) || !GClass1.CryptDeriveKey(zero, 26625U, zero2, 1U, ref zero3))
						{
							goto IL_2C7;
						}
						if (!GClass1.CryptDecrypt(zero3, UIntPtr.Zero, 1U, 0U, array2, ref num))
						{
							goto IL_2BB;
						}
						string @string = Encoding.ASCII.GetString(array2);
						array3 = @string.Replace("\r", "").Split(new char[]
						{
							'\n'
						});
						if (@string.Contains("ADXHEADER") || @string.Contains("XDFHEADER") || array[131] > 0)
						{
							goto IL_1DC;
						}
						MessageBox.Show("รหัสพาสเวิร์ดไม่ถูกต้อง โปรดลองใหม่", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
					}
					this.bool_0 = false;
					return 4;
					IL_1DC:
					foreach (string text in array3)
					{
						if (!string.IsNullOrWhiteSpace(text))
						{
							if (text.Contains("openpassword"))
							{
								string_1 = text.Replace("    <openpassword>", "").Replace("</openpassword>", "");
							}
							else if (text.Contains("modifypassword"))
							{
								string_2 = text.Replace("    <modifypassword>", "").Replace("</modifypassword>", "");
							}
							else if (text.Contains("<flags>0x"))
							{
								string_3 += (string_0.Contains(".adx") ? "    <flags>0x10000</flags>\r\n" : "    <flags>0x1</flags>\r\n");
							}
							else
							{
								string_3 = string_3 + text + "\r\n";
							}
						}
					}
					result = 3;
					goto IL_2E2;
					IL_2BB:
					this.bool_0 = false;
					return 1;
					IL_2C7:
					this.bool_0 = false;
					return 1;
					IL_2D3:
					this.bool_0 = false;
					return 1;
				}
				result = 2;
				IL_2E2:;
			}
			catch (SystemException)
			{
				this.bool_0 = false;
				return 1;
			}
			this.bool_0 = false;
			return result;
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00017060 File Offset: 0x00015260
		private void button_0_Click(object sender, EventArgs e)
		{
			this.textBox_0.Text = string.Empty;
			this.textBox_2.Text = string.Empty;
			this.textBox_3.Text = string.Empty;
			this.textBox_1.Text = string.Empty;
			this.button_2.Enabled = false;
			this.button_1.Enabled = false;
			OpenFileDialog openFileDialog = new OpenFileDialog
			{
				Title = "เลือกไฟล์",
				Multiselect = false,
				Filter = "ADX or XDF file|*.adx;*.xdf"
			};
			if (openFileDialog.ShowDialog() == DialogResult.OK)
			{
				try
				{
					byte[] array = File.ReadAllBytes(openFileDialog.FileName);
					if (array[0] == 5 && array[1] == 34 && array[2] == 151)
					{
						this.textBox_0.Text = openFileDialog.FileName;
						this.button_2.Enabled = true;
					}
					else
					{
						MessageBox.Show("ไฟล์นี้ไม่มีรหัสพาสเวิร์ด\nโปรดเลือกไฟล์ที่ติดรหัสผ่าน", "ข้อมูล", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
					}
				}
				catch (SystemException ex)
				{
					MessageBox.Show("เปิดไฟล์ล้มเหลว: " + ex.Message);
				}
			}
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00017178 File Offset: 0x00015378
		private void button_2_Click(object sender, EventArgs e)
		{
			string empty = string.Empty;
			string empty2 = string.Empty;
			string empty3 = string.Empty;
			this.textBox_2.Text = empty;
			this.textBox_3.Text = empty2;
			this.textBox_1.Text = empty3;
			this.button_1.Enabled = false;
			if (!this.textBox_0.Text.Equals(string.Empty))
			{
				switch (this.method_5(this.textBox_0.Text, ref empty, ref empty2, ref empty3))
				{
				case 1:
					MessageBox.Show("วิเคราะห์ไฟล์ล้มเหลว");
					return;
				case 2:
					MessageBox.Show("ไม่พบรหัสพาสเวิร์ด");
					return;
				case 3:
					this.textBox_2.Text = empty;
					this.textBox_3.Text = empty2;
					this.textBox_1.Text = empty3;
					this.button_1.Enabled = true;
					this.textBox_1.SelectionStart = 0;
					this.textBox_1.SelectionLength = 100;
					break;
				default:
					return;
				}
			}
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00017270 File Offset: 0x00015470
		private void button_1_Click(object sender, EventArgs e)
		{
			if (!string.IsNullOrEmpty(this.textBox_1.Text))
			{
				SaveFileDialog saveFileDialog = new SaveFileDialog
				{
					Title = "บันทึกไฟล์",
					Filter = (this.textBox_0.Text.Contains(".adx") ? "ADX file|*.adx" : "XDF file|*.xdf")
				};
				if (saveFileDialog.ShowDialog() == DialogResult.OK)
				{
					try
					{
						File.WriteAllText(saveFileDialog.FileName, this.textBox_1.Text, Encoding.ASCII);
						MessageBox.Show("บันทึกไฟล์เรียบร้อยแล้ว", "สำเร็จ", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
					}
					catch (SystemException ex)
					{
						MessageBox.Show("บันทึกล้มเหลว: " + ex.Message);
					}
				}
			}
		}

		// Token: 0x06000029 RID: 41 RVA: 0x0000C303 File Offset: 0x0000A503
		private void textBox_3_TextChanged(object sender, EventArgs e)
		{
		}

		// Token: 0x0600002A RID: 42 RVA: 0x0000C303 File Offset: 0x0000A503
		private void label_1_Click(object sender, EventArgs e)
		{
		}

		// Token: 0x0600002B RID: 43 RVA: 0x0000C303 File Offset: 0x0000A503
		private void textBox_2_TextChanged(object sender, EventArgs e)
		{
		}

		// Token: 0x0600002C RID: 44 RVA: 0x0000C303 File Offset: 0x0000A503
		private void textBox_1_TextChanged(object sender, EventArgs e)
		{
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00017330 File Offset: 0x00015530
		private void method_6()
		{
			this.textBox_0 = new TextBox();
			this.button_0 = new Button();
			this.textBox_1 = new TextBox();
			this.button_2 = new Button();
			this.label_0 = new Label();
			this.label_1 = new Label();
			this.textBox_2 = new TextBox();
			this.textBox_3 = new TextBox();
			this.label_2 = new Label();
			this.button_1 = new Button();
			this.label_3 = new Label();
			base.SuspendLayout();
			this.textBox_0.BackColor = Color.Black;
			this.textBox_0.BorderStyle = BorderStyle.FixedSingle;
			this.textBox_0.ForeColor = Color.White;
			this.textBox_0.Location = new Point(116, 52);
			this.textBox_0.Name = "TbFilePath";
			this.textBox_0.Size = new Size(400, 20);
			this.textBox_0.TabIndex = 0;
			this.button_0.FlatAppearance.BorderColor = Color.Red;
			this.button_0.FlatStyle = FlatStyle.Flat;
			this.button_0.ForeColor = Color.Red;
			this.button_0.Location = new Point(525, 50);
			this.button_0.Name = "BtnOpenFile";
			this.button_0.Size = new Size(95, 25);
			this.button_0.TabIndex = 1;
			this.button_0.Text = "เปิดไฟล์";
			this.button_0.UseVisualStyleBackColor = true;
			this.button_0.Click += this.button_0_Click;
			this.textBox_1.BackColor = Color.Black;
			this.textBox_1.BorderStyle = BorderStyle.FixedSingle;
			this.textBox_1.Font = new Font("Consolas", 9f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.textBox_1.ForeColor = Color.DimGray;
			this.textBox_1.Location = new Point(12, 130);
			this.textBox_1.Multiline = true;
			this.textBox_1.Name = "TbCleanedFile";
			this.textBox_1.ReadOnly = true;
			this.textBox_1.ScrollBars = ScrollBars.Both;
			this.textBox_1.Size = new Size(608, 200);
			this.textBox_1.TabIndex = 2;
			this.textBox_1.WordWrap = false;
			this.textBox_1.TextChanged += this.textBox_1_TextChanged;
			this.button_2.FlatAppearance.BorderColor = Color.Red;
			this.button_2.FlatStyle = FlatStyle.Flat;
			this.button_2.ForeColor = Color.Red;
			this.button_2.Location = new Point(525, 80);
			this.button_2.Name = "BtnAnalyzeFile";
			this.button_2.Size = new Size(95, 25);
			this.button_2.TabIndex = 3;
			this.button_2.Text = "ปลดรหัส";
			this.button_2.UseVisualStyleBackColor = true;
			this.button_2.Click += this.button_2_Click;
			this.label_0.AutoSize = true;
			this.label_0.Font = new Font("Leelawadee UI", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.label_0.ForeColor = Color.Gray;
			this.label_0.Location = new Point(15, 86);
			this.label_0.Name = "LblOpenPassword";
			this.label_0.Size = new Size(67, 13);
			this.label_0.TabIndex = 4;
			this.label_0.Text = "PASSWORD";
			this.label_1.AutoSize = true;
			this.label_1.Font = new Font("Leelawadee UI", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.label_1.ForeColor = Color.Gray;
			this.label_1.Location = new Point(235, 86);
			this.label_1.Name = "LblModifyPassword";
			this.label_1.Size = new Size(51, 13);
			this.label_1.TabIndex = 5;
			this.label_1.Text = "MODIFY ";
			this.label_1.Click += this.label_1_Click;
			this.textBox_2.BackColor = Color.Black;
			this.textBox_2.BorderStyle = BorderStyle.FixedSingle;
			this.textBox_2.ForeColor = Color.Red;
			this.textBox_2.Location = new Point(92, 83);
			this.textBox_2.Name = "TbOpenPassword";
			this.textBox_2.ReadOnly = true;
			this.textBox_2.Size = new Size(130, 20);
			this.textBox_2.TabIndex = 6;
			this.textBox_2.TextAlign = HorizontalAlignment.Center;
			this.textBox_2.TextChanged += this.textBox_2_TextChanged;
			this.textBox_3.BackColor = Color.Black;
			this.textBox_3.BorderStyle = BorderStyle.FixedSingle;
			this.textBox_3.ForeColor = Color.Red;
			this.textBox_3.Location = new Point(292, 83);
			this.textBox_3.Name = "TbModifyPassword";
			this.textBox_3.ReadOnly = true;
			this.textBox_3.Size = new Size(224, 20);
			this.textBox_3.TabIndex = 7;
			this.textBox_3.TextAlign = HorizontalAlignment.Center;
			this.textBox_3.TextChanged += this.textBox_3_TextChanged;
			this.label_2.AutoSize = true;
			this.label_2.Font = new Font("Leelawadee UI", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.label_2.ForeColor = Color.DimGray;
			this.label_2.Location = new Point(12, 114);
			this.label_2.Name = "LblUnlockedFile";
			this.label_2.Size = new Size(95, 13);
			this.label_2.TabIndex = 8;
			this.label_2.Text = "FILE_STREAM_IO";
			this.button_1.FlatAppearance.BorderColor = Color.Red;
			this.button_1.FlatStyle = FlatStyle.Flat;
			this.button_1.ForeColor = Color.Red;
			this.button_1.Location = new Point(12, 340);
			this.button_1.Name = "BtnSaveCleanedFile";
			this.button_1.Size = new Size(608, 40);
			this.button_1.TabIndex = 9;
			this.button_1.Text = "บันทึกไฟล์ที่ปลดรหัสแล้ว";
			this.button_1.UseVisualStyleBackColor = true;
			this.button_1.Click += this.button_1_Click;
			this.label_3.AutoSize = true;
			this.label_3.Font = new Font("Leelawadee UI", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.label_3.ForeColor = Color.Gray;
			this.label_3.Location = new Point(15, 55);
			this.label_3.Name = "LblXdfFile";
			this.label_3.Size = new Size(95, 13);
			this.label_3.TabIndex = 10;
			this.label_3.Text = "TARGET_SOURCE";
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			this.BackColor = Color.Black;
			base.ClientSize = new Size(635, 400);
			base.Controls.Add(this.label_3);
			base.Controls.Add(this.button_1);
			base.Controls.Add(this.label_2);
			base.Controls.Add(this.textBox_3);
			base.Controls.Add(this.textBox_2);
			base.Controls.Add(this.label_1);
			base.Controls.Add(this.label_0);
			base.Controls.Add(this.button_2);
			base.Controls.Add(this.button_0);
			base.Controls.Add(this.textBox_0);
			base.Controls.Add(this.textBox_1);
			base.FormBorderStyle = FormBorderStyle.None;
			base.MaximizeBox = false;
			base.Name = "Form4";
			base.ShowIcon = false;
			base.StartPosition = FormStartPosition.CenterScreen;
			this.Text = " ADX & XDF ปลดรหัส";
			base.Load += this.GForm1_Load;
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00017BF0 File Offset: 0x00015DF0
		[CompilerGenerated]
		private void GForm1_Paint(object sender, PaintEventArgs e)
		{
			e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
			using (Pen pen = new Pen(this.color_3, 1f))
			{
				e.Graphics.DrawRectangle(pen, 0, 0, base.Width - 1, base.Height - 1);
			}
			e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(12, 12, 15)), 0, 0, base.Width, 40);
			e.Graphics.DrawLine(new Pen(this.color_0, 2f), 0, 40, base.Width, 40);
			using (Pen pen2 = new Pen(this.color_0, 2f))
			{
				e.Graphics.DrawLine(pen2, 5, 5, 20, 5);
				e.Graphics.DrawLine(pen2, 5, 5, 5, 20);
				e.Graphics.DrawLine(pen2, base.Width - 5, base.Height - 5, base.Width - 5 - 15, base.Height - 5);
				e.Graphics.DrawLine(pen2, base.Width - 5, base.Height - 5, base.Width - 5, base.Height - 5 - 15);
			}
			using (Font font = new Font("Impact", 11f))
			{
				e.Graphics.DrawString("◢ XDF/ADX PASSWORD BREAKER : RED_MATRIX ◣", font, Brushes.White, 15f, 10f);
			}
			this.method_1(e.Graphics, new Rectangle(12, 99, 599, 164), "DECRYPTED_STREAM_VIEW");
		}

		// Token: 0x06000030 RID: 48 RVA: 0x0000C305 File Offset: 0x0000A505
		[CompilerGenerated]
		private void method_7(object sender, EventArgs e)
		{
			base.Close();
		}

		// Token: 0x06000031 RID: 49 RVA: 0x0000C3E7 File Offset: 0x0000A5E7
		[CompilerGenerated]
		private void GForm1_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Y < 40)
			{
				GForm1.ReleaseCapture();
				GForm1.SendMessage(base.Handle, 274, 61458, 0);
			}
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00017DB4 File Offset: 0x00015FB4
		[CompilerGenerated]
		private void timer_0_Tick(object sender, EventArgs e)
		{
			if (this.bool_0)
			{
				this.int_0 += 8;
				if (this.int_0 > this.textBox_1.Height)
				{
					this.int_0 = 0;
				}
				base.Invalidate(new Rectangle(this.textBox_1.Location, this.textBox_1.Size));
			}
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00017E14 File Offset: 0x00016014
		[CompilerGenerated]
		private void timer_1_Tick(object sender, EventArgs e)
		{
			if (this.bool_1)
			{
				this.float_0 += 0.05f;
				if (this.float_0 >= 1f)
				{
					this.bool_1 = false;
				}
			}
			else
			{
				this.float_0 -= 0.05f;
				if (this.float_0 <= 0.4f)
				{
					this.bool_1 = true;
				}
			}
			if (this.bool_0)
			{
				base.Invalidate();
			}
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00017E88 File Offset: 0x00016088
		[CompilerGenerated]
		private void GForm1_Paint_1(object sender, PaintEventArgs e)
		{
			if (this.bool_0)
			{
				int num = this.textBox_1.Top + this.int_0;
				using (Pen pen = new Pen(Color.FromArgb((int)(this.float_0 * 255f), this.color_0), 2f))
				{
					e.Graphics.DrawLine(pen, this.textBox_1.Left, num, this.textBox_1.Right, num);
				}
			}
		}

		// Token: 0x04000012 RID: 18
		private Color color_0 = Color.FromArgb(220, 20, 20);

		// Token: 0x04000013 RID: 19
		private Color color_1 = Color.FromArgb(8, 8, 10);

		// Token: 0x04000014 RID: 20
		private Color color_2 = Color.FromArgb(15, 15, 18);

		// Token: 0x04000015 RID: 21
		private Color color_3 = Color.FromArgb(40, 40, 45);

		// Token: 0x04000016 RID: 22
		private Timer timer_0;

		// Token: 0x04000017 RID: 23
		private int int_0;

		// Token: 0x04000018 RID: 24
		private bool bool_0;

		// Token: 0x04000019 RID: 25
		private Timer timer_1;

		// Token: 0x0400001A RID: 26
		private float float_0 = 1f;

		// Token: 0x0400001B RID: 27
		private bool bool_1;

		// Token: 0x0400001D RID: 29
		private TextBox textBox_0;

		// Token: 0x0400001E RID: 30
		private Button button_0;

		// Token: 0x0400001F RID: 31
		private TextBox textBox_1;

		// Token: 0x04000020 RID: 32
		private Label label_0;

		// Token: 0x04000021 RID: 33
		private Label label_1;

		// Token: 0x04000022 RID: 34
		private TextBox textBox_2;

		// Token: 0x04000023 RID: 35
		private TextBox textBox_3;

		// Token: 0x04000024 RID: 36
		private Label label_2;

		// Token: 0x04000025 RID: 37
		private Button button_1;

		// Token: 0x04000026 RID: 38
		private Label label_3;

		// Token: 0x04000027 RID: 39
		private Button button_2;
	}
}
