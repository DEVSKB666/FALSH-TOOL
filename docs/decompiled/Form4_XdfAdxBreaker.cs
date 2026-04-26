using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using <PrivateImplementationDetails>{68F2EF73-9355-4257-ADA6-397CF8BB8E72};

namespace Attr_2
{
	// Token: 0x02000008 RID: 8
	public partial class Form_8 : Form
	{
		// Token: 0x0600001B RID: 27
		[DllImport("user32.dll", EntryPoint = "ReleaseCapture")]
		public static extern bool M_1B();

		// Token: 0x0600001C RID: 28
		[DllImport("user32.dll", EntryPoint = "SendMessage")]
		public static extern int M_1B(IntPtr, int, int, int);

		// Token: 0x0600001D RID: 29
		[DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
		private static extern IntPtr M_1B(int, int, int, int, int, int);

		// Token: 0x0600001E RID: 30 RVA: 0x00002AC8 File Offset: 0x00000CC8
		public Form_8()
		{
			this.M_28();
			base.FormBorderStyle = FormBorderStyle.None;
			this.BackColor = this.M_1F;
			base.Region = Region.FromHrgn(global::Attr_2.Form_8.\u00A0(0, 0, base.Width, base.Height, 20, 20));
			this.DoubleBuffered = true;
			this.M_1F();
			this.M_23();
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002B7C File Offset: 0x00000D7C
		private void M_1F()
		{
			base.Paint += this.M_1B;
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
			button.Click += this.M_30;
			base.Controls.Add(button);
			base.MouseDown += this.M_1B;
			this.M_1B(this.M_1B, "FILE_PATH_INPUT");
			this.M_1B(this.M_23, "KEY_OPEN");
			this.M_1B(this.M_28, "KEY_MODIFY");
			this.M_1B(this.M_1B, "◢ เลือกไฟล์ ◣");
			this.M_1B(this.M_23, "◢ ปลดรหัส ◣");
			this.M_1B(this.M_1F, "◢ เซฟไฟล์ ◣");
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002C9C File Offset: 0x00000E9C
		private void M_1B(Graphics A_1, Rectangle A_2, string A_3)
		{
			using (Font font = new Font("Consolas", 7f, FontStyle.Bold))
			{
				A_1.DrawString("[" + A_3 + "]", font, new SolidBrush(Color.FromArgb(100, this.M_1B)), (float)(A_2.X + 2), (float)(A_2.Y - 12));
			}
			using (Pen pen = new Pen(Color.FromArgb(50, this.M_1B), 1f))
			{
				A_1.DrawRectangle(pen, A_2);
			}
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002D4C File Offset: 0x00000F4C
		private void M_1B(Control A_1, string A_2)
		{
			A_1.BackColor = Color.Black;
			A_1.ForeColor = Color.White;
			TextBox textBox = A_1 as TextBox;
			if (textBox != null)
			{
				textBox.BorderStyle = BorderStyle.FixedSingle;
			}
			A_1.Font = new Font("Consolas", 10f);
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002D98 File Offset: 0x00000F98
		private void M_1B(Button A_1, string A_2)
		{
			A_1.Text = A_2;
			A_1.FlatStyle = FlatStyle.Flat;
			A_1.BackColor = Color.FromArgb(20, 20, 25);
			A_1.ForeColor = this.M_1B;
			A_1.FlatAppearance.BorderColor = this.M_1B;
			A_1.Font = new Font("Impact", 10f);
			A_1.Cursor = Cursors.Hand;
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002E04 File Offset: 0x00001004
		private void M_23()
		{
			this.M_1B = new Timer
			{
				Interval = 30
			};
			this.M_1B.Tick += this.M_32;
			this.M_1B.Start();
			this.M_1F = new Timer
			{
				Interval = 50
			};
			this.M_1F.Tick += this.M_33;
			this.M_1F.Start();
			base.Paint += this.M_1F;
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002E8D File Offset: 0x0000108D
		private void M_1B(object A_1, EventArgs A_2)
		{
			this.M_1B.Text = string.Empty;
			this.M_1F.Text = string.Empty;
			this.M_23.Enabled = false;
			this.M_1F.Enabled = false;
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002EC8 File Offset: 0x000010C8
		public int M_1B(string A_1, ref string A_2, ref string A_3, ref string A_4)
		{
			this.M_1B = true;
			Application.DoEvents();
			UIntPtr zero = UIntPtr.Zero;
			UIntPtr zero2 = UIntPtr.Zero;
			UIntPtr zero3 = UIntPtr.Zero;
			string s = "$9Ad0$!#*)1QqPow^";
			byte[] bytes = Encoding.ASCII.GetBytes(s);
			int result;
			try
			{
				byte[] array = File.ReadAllBytes(A_1);
				if (array[0] == 5 && array[1] == 34 && array[2] == 151)
				{
					string[] array3;
					for (;;)
					{
						if (array[131] == 0)
						{
							\u2005 u = new Form_9();
							u.ShowDialog();
							if (string.IsNullOrWhiteSpace(u.Attr_2.Text))
							{
								break;
							}
							s = u.Attr_2.Text;
							bytes = Encoding.ASCII.GetBytes(s);
						}
						byte[] array2 = new byte[array.Length - 264];
						Array.Copy(array, 264, array2, 0, array.Length - 264);
						uint num = (uint)(array.Length - 264);
						if (!global::Attr_2.Type_7.\u00A0(ref zero, "TunerPro", "Microsoft Enhanced Cryptographic Provider v1.0", 1U, 0U))
						{
							goto IL_1D3;
						}
						if (!global::Attr_2.Type_7.\u00A0(zero, 32771U, UIntPtr.Zero, 0U, ref zero2) || !global::Attr_2.Type_7.\u00A0(zero2, bytes, (uint)bytes.Length, 0U) || !global::Attr_2.Type_7.\u1680(zero, 26625U, zero2, 1U, ref zero3))
						{
							goto IL_1E2;
						}
						if (!global::Attr_2.Type_7.\u00A0(zero3, UIntPtr.Zero, 1U, 0U, array2, ref num))
						{
							goto IL_1F1;
						}
						string @string = Encoding.ASCII.GetString(array2);
						array3 = @string.Replace("\r", "").Split(new char[]
						{
							'\n'
						});
						if (@string.Contains("ADXHEADER") || @string.Contains("XDFHEADER") || array[131] > 0)
						{
							goto IL_200;
						}
						MessageBox.Show("รหัสพาสเวิร์ดไม่ถูกต้อง โปรดลองใหม่", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
					}
					this.M_1B = false;
					return 4;
					IL_1D3:
					this.M_1B = false;
					return 1;
					IL_1E2:
					this.M_1B = false;
					return 1;
					IL_1F1:
					this.M_1B = false;
					return 1;
					IL_200:
					foreach (string text in array3)
					{
						if (!string.IsNullOrWhiteSpace(text))
						{
							if (text.Contains("openpassword"))
							{
								A_2 = text.Replace("    <openpassword>", "").Replace("</openpassword>", "");
							}
							else if (text.Contains("modifypassword"))
							{
								A_3 = text.Replace("    <modifypassword>", "").Replace("</modifypassword>", "");
							}
							else if (text.Contains("<flags>0x"))
							{
								A_4 += (A_1.Contains(".adx") ? "    <flags>0x10000</flags>\r\n" : "    <flags>0x1</flags>\r\n");
							}
							else
							{
								A_4 = A_4 + text + "\r\n";
							}
						}
					}
					result = 3;
				}
				else
				{
					result = 2;
				}
			}
			catch (SystemException)
			{
				this.M_1B = false;
				return 1;
			}
			this.M_1B = false;
			return result;
		}

		// Token: 0x06000026 RID: 38 RVA: 0x000031F0 File Offset: 0x000013F0
		private void M_1F(object A_1, EventArgs A_2)
		{
			this.M_1B.Text = string.Empty;
			this.M_23.Text = string.Empty;
			this.M_28.Text = string.Empty;
			this.M_1F.Text = string.Empty;
			this.M_23.Enabled = false;
			this.M_1F.Enabled = false;
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
						this.M_1B.Text = openFileDialog.FileName;
						this.M_23.Enabled = true;
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

		// Token: 0x06000027 RID: 39 RVA: 0x00003308 File Offset: 0x00001508
		private void M_23(object A_1, EventArgs A_2)
		{
			string empty = string.Empty;
			string empty2 = string.Empty;
			string empty3 = string.Empty;
			this.M_23.Text = empty;
			this.M_28.Text = empty2;
			this.M_1F.Text = empty3;
			this.M_1F.Enabled = false;
			if (!this.M_1B.Text.Equals(string.Empty))
			{
				switch (this.M_1B(this.M_1B.Text, ref empty, ref empty2, ref empty3))
				{
				case 1:
					MessageBox.Show("วิเคราะห์ไฟล์ล้มเหลว");
					return;
				case 2:
					MessageBox.Show("ไม่พบรหัสพาสเวิร์ด");
					return;
				case 3:
					this.M_23.Text = empty;
					this.M_28.Text = empty2;
					this.M_1F.Text = empty3;
					this.M_1F.Enabled = true;
					this.M_1F.SelectionStart = 0;
					this.M_1F.SelectionLength = 100;
					break;
				default:
					return;
				}
			}
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00003400 File Offset: 0x00001600
		private void M_28(object A_1, EventArgs A_2)
		{
			if (!string.IsNullOrEmpty(this.M_1F.Text))
			{
				SaveFileDialog saveFileDialog = new SaveFileDialog
				{
					Title = "บันทึกไฟล์",
					Filter = (this.M_1B.Text.Contains(".adx") ? "ADX file|*.adx" : "XDF file|*.xdf")
				};
				if (saveFileDialog.ShowDialog() == DialogResult.OK)
				{
					try
					{
						File.WriteAllText(saveFileDialog.FileName, this.M_1F.Text, Encoding.ASCII);
						MessageBox.Show("บันทึกไฟล์เรียบร้อยแล้ว", "สำเร็จ", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
					}
					catch (SystemException ex)
					{
						MessageBox.Show("บันทึกล้มเหลว: " + ex.Message);
					}
				}
			}
		}

		// Token: 0x06000029 RID: 41 RVA: 0x0000208B File Offset: 0x0000028B
		private void M_23(object A_1, EventArgs A_2)
		{
		}

		// Token: 0x0600002A RID: 42 RVA: 0x0000208B File Offset: 0x0000028B
		private void M_28(object A_1, EventArgs A_2)
		{
		}

		// Token: 0x0600002B RID: 43 RVA: 0x0000208B File Offset: 0x0000028B
		private void \u2004(object A_1, EventArgs A_2)
		{
		}

		// Token: 0x0600002C RID: 44 RVA: 0x0000208B File Offset: 0x0000028B
		private void M_2C(object A_1, EventArgs A_2)
		{
		}

		// Token: 0x0600002E RID: 46 RVA: 0x000034E8 File Offset: 0x000016E8
		private void M_28()
		{
			this.M_1B = new TextBox();
			this.M_1B = new Button();
			this.M_1F = new TextBox();
			this.M_23 = new Button();
			this.M_1B = new Label();
			this.M_1F = new Label();
			this.M_23 = new TextBox();
			this.M_28 = new TextBox();
			this.M_23 = new Label();
			this.M_1F = new Button();
			this.M_28 = new Label();
			base.SuspendLayout();
			this.M_1B.BackColor = Color.Black;
			this.M_1B.BorderStyle = BorderStyle.FixedSingle;
			this.M_1B.ForeColor = Color.White;
			this.M_1B.Location = new Point(116, 52);
			this.M_1B.Name = "TbFilePath";
			this.M_1B.Size = new Size(400, 20);
			this.M_1B.TabIndex = 0;
			this.M_1B.FlatAppearance.BorderColor = Color.Red;
			this.M_1B.FlatStyle = FlatStyle.Flat;
			this.M_1B.ForeColor = Color.Red;
			this.M_1B.Location = new Point(525, 50);
			this.M_1B.Name = "BtnOpenFile";
			this.M_1B.Size = new Size(95, 25);
			this.M_1B.TabIndex = 1;
			this.M_1B.Text = "เปิดไฟล์";
			this.M_1B.UseVisualStyleBackColor = true;
			this.M_1B.Click += this.M_1F;
			this.M_1F.BackColor = Color.Black;
			this.M_1F.BorderStyle = BorderStyle.FixedSingle;
			this.M_1F.Font = new Font("Consolas", 9f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.M_1F.ForeColor = Color.DimGray;
			this.M_1F.Location = new Point(12, 130);
			this.M_1F.Multiline = true;
			this.M_1F.Name = "TbCleanedFile";
			this.M_1F.ReadOnly = true;
			this.M_1F.ScrollBars = ScrollBars.Both;
			this.M_1F.Size = new Size(608, 200);
			this.M_1F.TabIndex = 2;
			this.M_1F.WordWrap = false;
			this.M_1F.TextChanged += this.M_2C;
			this.M_23.FlatAppearance.BorderColor = Color.Red;
			this.M_23.FlatStyle = FlatStyle.Flat;
			this.M_23.ForeColor = Color.Red;
			this.M_23.Location = new Point(525, 80);
			this.M_23.Name = "BtnAnalyzeFile";
			this.M_23.Size = new Size(95, 25);
			this.M_23.TabIndex = 3;
			this.M_23.Text = "ปลดรหัส";
			this.M_23.UseVisualStyleBackColor = true;
			this.M_23.Click += this.M_23;
			this.M_1B.AutoSize = true;
			this.M_1B.Font = new Font("Leelawadee UI", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.M_1B.ForeColor = Color.Gray;
			this.M_1B.Location = new Point(15, 86);
			this.M_1B.Name = "LblOpenPassword";
			this.M_1B.Size = new Size(67, 13);
			this.M_1B.TabIndex = 4;
			this.M_1B.Text = "PASSWORD";
			this.M_1F.AutoSize = true;
			this.M_1F.Font = new Font("Leelawadee UI", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.M_1F.ForeColor = Color.Gray;
			this.M_1F.Location = new Point(235, 86);
			this.M_1F.Name = "LblModifyPassword";
			this.M_1F.Size = new Size(51, 13);
			this.M_1F.TabIndex = 5;
			this.M_1F.Text = "MODIFY ";
			this.M_1F.Click += this.M_28;
			this.M_23.BackColor = Color.Black;
			this.M_23.BorderStyle = BorderStyle.FixedSingle;
			this.M_23.ForeColor = Color.Red;
			this.M_23.Location = new Point(92, 83);
			this.M_23.Name = "TbOpenPassword";
			this.M_23.ReadOnly = true;
			this.M_23.Size = new Size(130, 20);
			this.M_23.TabIndex = 6;
			this.M_23.TextAlign = HorizontalAlignment.Center;
			this.M_23.TextChanged += this.M_2B;
			this.M_28.BackColor = Color.Black;
			this.M_28.BorderStyle = BorderStyle.FixedSingle;
			this.M_28.ForeColor = Color.Red;
			this.M_28.Location = new Point(292, 83);
			this.M_28.Name = "TbModifyPassword";
			this.M_28.ReadOnly = true;
			this.M_28.Size = new Size(224, 20);
			this.M_28.TabIndex = 7;
			this.M_28.TextAlign = HorizontalAlignment.Center;
			this.M_28.TextChanged += this.M_23;
			this.M_23.AutoSize = true;
			this.M_23.Font = new Font("Leelawadee UI", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.M_23.ForeColor = Color.DimGray;
			this.M_23.Location = new Point(12, 114);
			this.M_23.Name = "LblUnlockedFile";
			this.M_23.Size = new Size(95, 13);
			this.M_23.TabIndex = 8;
			this.M_23.Text = "FILE_STREAM_IO";
			this.M_1F.FlatAppearance.BorderColor = Color.Red;
			this.M_1F.FlatStyle = FlatStyle.Flat;
			this.M_1F.ForeColor = Color.Red;
			this.M_1F.Location = new Point(12, 340);
			this.M_1F.Name = "BtnSaveCleanedFile";
			this.M_1F.Size = new Size(608, 40);
			this.M_1F.TabIndex = 9;
			this.M_1F.Text = "บันทึกไฟล์ที่ปลดรหัสแล้ว";
			this.M_1F.UseVisualStyleBackColor = true;
			this.M_1F.Click += this.M_28;
			this.M_28.AutoSize = true;
			this.M_28.Font = new Font("Leelawadee UI", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.M_28.ForeColor = Color.Gray;
			this.M_28.Location = new Point(15, 55);
			this.M_28.Name = "LblXdfFile";
			this.M_28.Size = new Size(95, 13);
			this.M_28.TabIndex = 10;
			this.M_28.Text = "TARGET_SOURCE";
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			this.BackColor = Color.Black;
			base.ClientSize = new Size(635, 400);
			base.Controls.Add(this.M_28);
			base.Controls.Add(this.M_1F);
			base.Controls.Add(this.M_23);
			base.Controls.Add(this.M_28);
			base.Controls.Add(this.M_23);
			base.Controls.Add(this.M_1F);
			base.Controls.Add(this.M_1B);
			base.Controls.Add(this.M_23);
			base.Controls.Add(this.M_1B);
			base.Controls.Add(this.M_1B);
			base.Controls.Add(this.M_1F);
			base.FormBorderStyle = FormBorderStyle.None;
			base.MaximizeBox = false;
			base.Name = "Form4";
			base.ShowIcon = false;
			base.StartPosition = FormStartPosition.CenterScreen;
			this.Text = " ADX & XDF ปลดรหัส";
			base.Load += this.M_1B;
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00003DA8 File Offset: 0x00001FA8
		[CompilerGenerated]
		private void M_1B(object A_1, PaintEventArgs A_2)
		{
			A_2.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
			using (Pen pen = new Pen(this.M_28, 1f))
			{
				A_2.Graphics.DrawRectangle(pen, 0, 0, base.Width - 1, base.Height - 1);
			}
			A_2.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(12, 12, 15)), 0, 0, base.Width, 40);
			A_2.Graphics.DrawLine(new Pen(this.M_1B, 2f), 0, 40, base.Width, 40);
			using (Pen pen2 = new Pen(this.M_1B, 2f))
			{
				int num = 15;
				A_2.Graphics.DrawLine(pen2, 5, 5, 5 + num, 5);
				A_2.Graphics.DrawLine(pen2, 5, 5, 5, 5 + num);
				A_2.Graphics.DrawLine(pen2, base.Width - 5, base.Height - 5, base.Width - 5 - num, base.Height - 5);
				A_2.Graphics.DrawLine(pen2, base.Width - 5, base.Height - 5, base.Width - 5, base.Height - 5 - num);
			}
			using (Font font = new Font("Impact", 11f))
			{
				A_2.Graphics.DrawString("◢ XDF/ADX PASSWORD BREAKER : RED_MATRIX ◣", font, Brushes.White, 15f, 10f);
			}
			this.M_1B(A_2.Graphics, new Rectangle(12, 99, 599, 164), "DECRYPTED_STREAM_VIEW");
		}

		// Token: 0x06000030 RID: 48 RVA: 0x000021C8 File Offset: 0x000003C8
		[CompilerGenerated]
		private void M_30(object A_1, EventArgs A_2)
		{
			base.Close();
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00003F70 File Offset: 0x00002170
		[CompilerGenerated]
		private void M_1B(object A_1, MouseEventArgs A_2)
		{
			if (A_2.Y < 40)
			{
				global::Attr_2.Form_8.\u00A0();
				global::Attr_2.Form_8.\u00A0(base.Handle, 274, 61458, 0);
			}
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00003F9C File Offset: 0x0000219C
		[CompilerGenerated]
		private void M_32(object A_1, EventArgs A_2)
		{
			if (this.M_1B)
			{
				this.M_1B += 8;
				if (this.M_1B > this.M_1F.Height)
				{
					this.M_1B = 0;
				}
				base.Invalidate(new Rectangle(this.M_1F.Location, this.M_1F.Size));
			}
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00003FFC File Offset: 0x000021FC
		[CompilerGenerated]
		private void M_33(object A_1, EventArgs A_2)
		{
			if (this.M_1F)
			{
				this.M_1B += 0.05f;
				if (this.M_1B >= 1f)
				{
					this.M_1F = false;
				}
			}
			else
			{
				this.M_1B -= 0.05f;
				if (this.M_1B <= 0.4f)
				{
					this.M_1F = true;
				}
			}
			if (this.M_1B)
			{
				base.Invalidate();
			}
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00004070 File Offset: 0x00002270
		[CompilerGenerated]
		private void M_1F(object A_1, PaintEventArgs A_2)
		{
			if (this.M_1B)
			{
				int num = this.M_1F.Top + this.M_1B;
				using (Pen pen = new Pen(Color.FromArgb((int)(this.M_1B * 255f), this.M_1B), 2f))
				{
					A_2.Graphics.DrawLine(pen, this.M_1F.Left, num, this.M_1F.Right, num);
				}
			}
		}

		// Token: 0x04000012 RID: 18
		private Color M_1B = Color.FromArgb(220, 20, 20);

		// Token: 0x04000013 RID: 19
		private Color M_1F = Color.FromArgb(8, 8, 10);

		// Token: 0x04000014 RID: 20
		private Color M_23 = Color.FromArgb(15, 15, 18);

		// Token: 0x04000015 RID: 21
		private Color M_28 = Color.FromArgb(40, 40, 45);

		// Token: 0x04000016 RID: 22
		private Timer M_1B;

		// Token: 0x04000017 RID: 23
		private int M_1B;

		// Token: 0x04000018 RID: 24
		private bool M_1B;

		// Token: 0x04000019 RID: 25
		private Timer M_1F;

		// Token: 0x0400001A RID: 26
		private float M_1B = 1f;

		// Token: 0x0400001B RID: 27
		private bool M_1F;

		// Token: 0x0400001D RID: 29
		private TextBox M_1B;

		// Token: 0x0400001E RID: 30
		private Button M_1B;

		// Token: 0x0400001F RID: 31
		private TextBox M_1F;

		// Token: 0x04000020 RID: 32
		private Label M_1B;

		// Token: 0x04000021 RID: 33
		private Label M_1F;

		// Token: 0x04000022 RID: 34
		private TextBox M_23;

		// Token: 0x04000023 RID: 35
		private TextBox M_28;

		// Token: 0x04000024 RID: 36
		private Label M_23;

		// Token: 0x04000025 RID: 37
		private Button M_1F;

		// Token: 0x04000026 RID: 38
		private Label M_28;

		// Token: 0x04000027 RID: 39
		private Button M_23;
	}
}
