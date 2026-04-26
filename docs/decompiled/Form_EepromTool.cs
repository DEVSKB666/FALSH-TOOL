using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using <PrivateImplementationDetails>{68F2EF73-9355-4257-ADA6-397CF8BB8E72};
using Attr_3;

namespace Attr_2
{
	// Token: 0x0200000A RID: 10
	public partial class Form_A : Form
	{
		// Token: 0x0600003A RID: 58 RVA: 0x00004390 File Offset: 0x00002590
		public Form_A()
		{
			this.M_5B();
			base.FormClosing += this.M_3C;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.StartPosition = FormStartPosition.CenterScreen;
			base.FormBorderStyle = FormBorderStyle.FixedDialog;
			this.M_3C.Items.Clear();
			this.M_3C = new System.Windows.Forms.Timer();
			this.M_3C.Interval = 3000;
			this.M_3C.Items.Add("ดูดไฟล์กล่อง 48K");
			this.M_3C.Items.Add("ดูดไฟล์กล่อง 64K");
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00004456 File Offset: 0x00002656
		public Form_A(Icon A_1) : this()
		{
			base.Icon = A_1;
			base.FormClosing += this.M_3C;
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00004478 File Offset: 0x00002678
		private void M_3C(object A_1, FormClosingEventArgs A_2)
		{
			if (this.M_3C != null && this.M_3C.IsAlive)
			{
				try
				{
					this.M_3C.Join(1000);
				}
				catch
				{
				}
			}
			if (global::Attr_2.Form_A.\u00A0 != IntPtr.Zero)
			{
				try
				{
					\u204E.\u00A0(global::Attr_2.Form_A.\u00A0);
					global::Attr_2.Form_A.\u00A0 = IntPtr.Zero;
					Thread.Sleep(100);
				}
				catch
				{
				}
			}
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00004500 File Offset: 0x00002700
		private bool M_3C()
		{
			uint num = 0U;
			if (\u204E.\u00A0(0U, ref global::Attr_2.Form_A.\u00A0) > \u204E.Attr_2.\u00A0)
			{
				try
				{
					\u204E.\u00A0(global::Attr_2.Form_A.\u00A0);
				}
				catch
				{
				}
				return false;
			}
			if (\u204E.\u1680(global::Attr_2.Form_A.\u00A0, 3U) > \u204E.Attr_2.\u00A0)
			{
				\u204E.\u00A0(global::Attr_2.Form_A.\u00A0);
				return false;
			}
			if (\u204E.\u00A0(global::Attr_2.Form_A.\u00A0, 2) > \u204E.Attr_2.\u00A0)
			{
				\u204E.\u00A0(global::Attr_2.Form_A.\u00A0);
				return false;
			}
			if (\u204E.\u00A0(global::Attr_2.Form_A.\u00A0, 8, 0, 0) > \u204E.Attr_2.\u00A0)
			{
				\u204E.\u00A0(global::Attr_2.Form_A.\u00A0);
				return false;
			}
			if (\u204E.\u00A0(global::Attr_2.Form_A.\u00A0, 10400U) > \u204E.Attr_2.\u00A0)
			{
				\u204E.\u00A0(global::Attr_2.Form_A.\u00A0);
				return false;
			}
			if (\u204E.\u00A0(global::Attr_2.Form_A.\u00A0, 200U, 200U) > \u204E.Attr_2.\u00A0)
			{
				\u204E.\u00A0(global::Attr_2.Form_A.\u00A0);
				return false;
			}
			if (\u204E.\u00A0(global::Attr_2.Form_A.\u00A0, 0, 0) > \u204E.Attr_2.\u00A0)
			{
				\u204E.\u00A0(global::Attr_2.Form_A.\u00A0);
				return false;
			}
			if (\u204E.\u00A0(global::Attr_2.Form_A.\u00A0, 1, 1) > \u204E.Attr_2.\u00A0)
			{
				\u204E.\u00A0(global::Attr_2.Form_A.\u00A0);
				return false;
			}
			byte[] array = new byte[1];
			byte[] array2 = new byte[]
			{
				1
			};
			if (\u204E.\u1680(global::Attr_2.Form_A.\u00A0, array, (uint)array.Length, ref num) > \u204E.Attr_2.\u00A0)
			{
				\u204E.\u00A0(global::Attr_2.Form_A.\u00A0);
				return false;
			}
			Thread.Sleep(70);
			if (\u204E.\u1680(global::Attr_2.Form_A.\u00A0, array2, (uint)array2.Length, ref num) > \u204E.Attr_2.\u00A0)
			{
				\u204E.\u00A0(global::Attr_2.Form_A.\u00A0);
				return false;
			}
			if (\u204E.\u00A0(global::Attr_2.Form_A.\u00A0, 0, 0) > \u204E.Attr_2.\u00A0)
			{
				\u204E.\u00A0(global::Attr_2.Form_A.\u00A0);
				return false;
			}
			if (\u204E.\u00A0(global::Attr_2.Form_A.\u00A0, 10400U) > \u204E.Attr_2.\u00A0)
			{
				\u204E.\u00A0(global::Attr_2.Form_A.\u00A0);
				return false;
			}
			if (\u204E.\u1680(global::Attr_2.Form_A.\u00A0, 3U) > \u204E.Attr_2.\u00A0)
			{
				\u204E.\u00A0(global::Attr_2.Form_A.\u00A0);
				return false;
			}
			Thread.Sleep(130);
			return true;
		}

		// Token: 0x0600003E RID: 62 RVA: 0x000046C8 File Offset: 0x000028C8
		private bool M_3C(byte[] A_1, int A_2, ref byte[] A_3, ref uint A_4, int A_5 = 0)
		{
			\u204E.\u1680(global::Attr_2.Form_A.\u00A0, 1U);
			uint num = 0U;
			if (\u204E.\u1680(global::Attr_2.Form_A.\u00A0, A_1, (uint)A_2, ref num) > \u204E.Attr_2.\u00A0 || num != (uint)A_2)
			{
				this.M_3C("ส่ง >> " + BitConverter.ToString(A_1, 0, A_2) + " (ส่งล้มเหลว)");
				return false;
			}
			this.M_3C("ส่ง >> " + BitConverter.ToString(A_1, 0, A_2));
			if (A_5 > 0)
			{
				Thread.Sleep(A_5);
			}
			int num2 = Math.Min(180, Math.Max(80, 40 + 6 * A_2));
			byte[] array = new byte[512];
			List<byte> list = new List<byte>(256);
			uint num3 = 0U;
			uint num4 = 0U;
			Stopwatch stopwatch = Stopwatch.StartNew();
			Stopwatch stopwatch2 = Stopwatch.StartNew();
			while (stopwatch.ElapsedMilliseconds < (long)num2)
			{
				if (\u204E.\u00A0(global::Attr_2.Form_A.\u00A0, ref num3) == \u204E.Attr_2.\u00A0 && num3 > 0U)
				{
					if ((ulong)num3 > (ulong)((long)array.Length))
					{
						num3 = (uint)array.Length;
					}
					if (\u204E.\u00A0(global::Attr_2.Form_A.\u00A0, array, num3, ref num4) == \u204E.Attr_2.\u00A0 && num4 > 0U)
					{
						list.AddRange(array.Take((int)num4));
						stopwatch2.Restart();
						continue;
					}
				}
				if (stopwatch2.ElapsedMilliseconds >= 7L)
				{
					break;
				}
				Thread.Sleep(1);
			}
			if (list.Count > 0)
			{
				A_3 = list.ToArray();
				A_4 = (uint)list.Count;
				this.M_3C("รับ << " + BitConverter.ToString(A_3, 0, (int)A_4));
				return true;
			}
			this.M_3C("รับ << ไม่มีข้อมูลตอบกลับ");
			return false;
		}

		// Token: 0x0600003F RID: 63 RVA: 0x0000482C File Offset: 0x00002A2C
		private void M_3C(string A_1)
		{
			global::Attr_2.Form_A.\u1680 u = new global::Attr_2.Form_A.\u1680();
			u.\u00A0 = this;
			u.\u00A0 = A_1;
			if (this.M_3C.InvokeRequired)
			{
				this.M_3C.Invoke(new Action(u.\u00A0));
				return;
			}
			this.M_3C.AppendText(u.\u00A0 + Environment.NewLine);
			this.M_3C.SelectionStart = this.M_3C.Text.Length;
			this.M_3C.ScrollToCaret();
		}

		// Token: 0x06000040 RID: 64 RVA: 0x000048B4 File Offset: 0x00002AB4
		private void M_3C(byte[] A_1, bool A_2, uint A_3)
		{
			global::Attr_2.Form_A.\u2000 u = new global::Attr_2.Form_A.\u2000();
			u.\u00A0 = A_1;
			string text = this.M_3C.Elapsed.TotalSeconds.ToString("F4");
			string text2 = A_2 ? "<" : ">";
			IEnumerable<string> values = Enumerable.Range(0, (int)A_3).Select(new Func<int, string>(u.\u00A0));
			Console.WriteLine(string.Concat(new string[]
			{
				"[",
				text,
				"] 0 ",
				text2,
				" [",
				string.Join(", ", values),
				"]"
			}));
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00004960 File Offset: 0x00002B60
		private bool M_3C(byte[] A_1)
		{
			byte[] array = new byte[256];
			uint num = 0U;
			bool flag = this.M_3C(A_1, A_1.Length, ref array, ref num, 0);
			this.M_3C(A_1, false, (uint)A_1.Length);
			if (flag)
			{
				this.M_3C(array, true, num);
			}
			return flag;
		}

		// Token: 0x06000042 RID: 66 RVA: 0x000049A0 File Offset: 0x00002BA0
		private void M_3C(List<byte> A_1)
		{
			global::Attr_2.Form_A.\u2001 u = new global::Attr_2.Form_A.\u2001();
			u.\u00A0 = this;
			u.\u00A0 = A_1;
			base.Invoke(new Action(u.\u00A0));
		}

		// Token: 0x06000043 RID: 67 RVA: 0x0000208B File Offset: 0x0000028B
		private void M_3C(object A_1, EventArgs A_2)
		{
		}

		// Token: 0x06000044 RID: 68 RVA: 0x0000208B File Offset: 0x0000028B
		private void M_44(object A_1, EventArgs A_2)
		{
		}

		// Token: 0x06000045 RID: 69 RVA: 0x000049D4 File Offset: 0x00002BD4
		private byte M_44(string A_1)
		{
			if (string.IsNullOrEmpty(A_1))
			{
				throw new ArgumentException("Input tidak boleh kosong.");
			}
			return Convert.ToByte(A_1, 16);
		}

		// Token: 0x06000046 RID: 70 RVA: 0x0000208B File Offset: 0x0000028B
		private void M_46(object A_1, EventArgs A_2)
		{
		}

		// Token: 0x06000047 RID: 71 RVA: 0x000049F4 File Offset: 0x00002BF4
		private void M_47(object A_1, EventArgs A_2)
		{
			object selectedItem = this.M_3C.SelectedItem;
			string text = (selectedItem != null) ? selectedItem.ToString() : null;
			if (string.IsNullOrEmpty(text))
			{
				MessageBox.Show("กรุณาเลือกคำสั่งจากรายการ");
				return;
			}
			if (text == "READ EEPROM KH")
			{
				if (MessageBox.Show("OFF สวิตซ์ และ ON\r\nภายใน 3 วินาที\r\nและกด OK", "READ EEPROM", MessageBoxButtons.OK) == DialogResult.OK)
				{
					if (this.M_3C == null || !this.M_3C.IsAlive)
					{
						this.M_3C = new Thread(new ThreadStart(this.M_46))
						{
							IsBackground = true
						};
						this.M_3C.Start();
						return;
					}
					MessageBox.Show("Process is already running.");
					return;
				}
			}
			else if (text == "ลบจำนวนการอัด Kh")
			{
				if (this.M_3C == null || !this.M_3C.IsAlive)
				{
					this.M_3C = new Thread(new ThreadStart(this.M_46))
					{
						IsBackground = true
					};
					this.M_3C.Start();
					return;
				}
				MessageBox.Show("Process is already running.");
				return;
			}
			else if (text == "Format EEPROM 0x00")
			{
				if (this.M_3C == null || !this.M_3C.IsAlive)
				{
					this.M_3C = new Thread(new ThreadStart(this.M_47))
					{
						IsBackground = true
					};
					this.M_3C.Start();
					return;
				}
				MessageBox.Show("Process is already running.");
				return;
			}
			else if (text == "Format EEPROM 0xFF")
			{
				if (this.M_3C == null || !this.M_3C.IsAlive)
				{
					this.M_3C = new Thread(new ThreadStart(this.M_4D))
					{
						IsBackground = true
					};
					this.M_3C.Start();
					return;
				}
				MessageBox.Show("Process is already running.");
				return;
			}
			else if (text == "READ EEPROM Sh")
			{
				if (this.M_3C == null || !this.M_3C.IsAlive)
				{
					this.M_3C = new Thread(new ThreadStart(this.M_44))
					{
						IsBackground = true
					};
					this.M_3C.Start();
					return;
				}
				MessageBox.Show("Process is already running.");
				return;
			}
			else if (text == "ดูดไฟล์กล่อง 48K")
			{
				if (this.M_3C == null || !this.M_3C.IsAlive)
				{
					this.M_3C = new Thread(new ThreadStart(this.M_50))
					{
						IsBackground = true
					};
					this.M_3C.Start();
					return;
				}
				MessageBox.Show("Process is already running.");
				return;
			}
			else if (text == "ดูดไฟล์กล่อง 64K")
			{
				if (this.M_3C == null || !this.M_3C.IsAlive)
				{
					this.M_3C = new Thread(new ThreadStart(this.M_4F))
					{
						IsBackground = true
					};
					this.M_3C.Start();
					return;
				}
				MessageBox.Show("Process is already running.");
				return;
			}
			else if (text == "ลบจำนวนการอัด Sh")
			{
				if (MessageBox.Show("OFF สวิตซ์ และ ON\r\nภายใน 3 วินาที\r\nและกด OK", "WRITE EEPROM", MessageBoxButtons.OK) == DialogResult.OK)
				{
					if (this.M_3C == null || !this.M_3C.IsAlive)
					{
						this.M_3C = new Thread(new ThreadStart(this.M_47))
						{
							IsBackground = true
						};
						this.M_3C.Start();
						return;
					}
					MessageBox.Show("Process is already running.");
					return;
				}
			}
			else
			{
				MessageBox.Show("คำสั่งไม่รู้จัก");
			}
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00004D24 File Offset: 0x00002F24
		private void M_44()
		{
			global::Attr_2.Form_A.\u2002 u = new global::Attr_2.Form_A.\u2002();
			u.\u00A0 = this;
			u.\u00A0 = new Stopwatch();
			u.Attr_2.Start();
			byte[] array = new byte[]
			{
				254,
				4,
				114,
				140
			};
			byte[] array2 = new byte[]
			{
				114,
				5,
				0,
				240,
				153
			};
			byte[] array3 = new byte[]
			{
				145,
				145,
				13,
				223,
				158,
				141,
				154,
				134,
				144,
				138,
				140,
				155,
				136
			};
			byte[] array4 = new byte[]
			{
				145,
				145,
				13,
				223,
				146,
				158,
				134,
				150,
				139,
				141,
				134,
				192,
				106
			};
			byte[] array5 = new byte[20];
			uint num = 0U;
			List<byte> list = new List<byte>();
			if (!this.M_3C())
			{
				return;
			}
			base.Invoke(new Action(u.\u00A0));
			if (this.M_3C(array, array.Length, ref array5, ref num, 0))
			{
				string str = BitConverter.ToString(array5, 0, (int)num);
				Console.WriteLine("Response: " + str);
			}
			Thread.Sleep(150);
			if (this.M_3C(array2, array2.Length, ref array5, ref num, 0))
			{
				string str2 = BitConverter.ToString(array5, 0, (int)num);
				Console.WriteLine("Response: " + str2);
			}
			Thread.Sleep(150);
			if (this.M_3C(array3, array3.Length, ref array5, ref num, 0))
			{
				string str3 = BitConverter.ToString(array5, 0, (int)num);
				Console.WriteLine("Response: " + str3);
			}
			Thread.Sleep(150);
			if (this.M_3C(array4, array4.Length, ref array5, ref num, 0))
			{
				string str4 = BitConverter.ToString(array5, 0, (int)num);
				Console.WriteLine("Response2: " + str4);
			}
			Console.WriteLine("Response Data:");
			global::Attr_2.Form_A.\u2003 u2 = new global::Attr_2.Form_A.\u2003();
			u2.\u00A0 = u;
			u2.\u00A0 = 0;
			while (u2.\u00A0 <= 255)
			{
				byte[] array6 = new byte[]
				{
					145,
					145,
					7,
					64,
					0,
					0,
					0
				};
				this.M_3C(array6, array6.Length, ref array5, ref num, 0);
				Thread.Sleep(100);
				if (num > 12U)
				{
					int num2 = 11;
					while ((long)num2 < (long)((ulong)Math.Min(num, 13U)))
					{
						Console.Write(array5[num2].ToString("X2") + " ");
						if (num2 == 11 || num2 == 12)
						{
							list.Add(array5[num2]);
						}
						num2++;
					}
					Console.WriteLine();
				}
				else
				{
					Console.WriteLine("Data tidak cukup untuk menampilkan byte 11 dan 12.");
				}
				base.Invoke(new Action(u2.\u00A0));
				int u00A = u2.\u00A0;
				u2.\u00A0 = u00A + 1;
			}
			u.Attr_2.Stop();
			this.M_3C = list.ToArray();
			this.M_3C(list);
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00004FC0 File Offset: 0x000031C0
		private void M_46()
		{
			global::Attr_2.Form_A.\u2004 u = new global::Attr_2.Form_A.\u2004();
			u.\u00A0 = this;
			u.\u00A0 = new Stopwatch();
			u.Attr_2.Start();
			byte[] array = new byte[]
			{
				254,
				4,
				114,
				140
			};
			byte[] array2 = new byte[]
			{
				114,
				5,
				0,
				240,
				153
			};
			byte[] array3 = new byte[]
			{
				39,
				11,
				224,
				72,
				101,
				108,
				108,
				111,
				72,
				111,
				67
			};
			byte[] array4 = new byte[]
			{
				39,
				11,
				224,
				119,
				65,
				114,
				101,
				89,
				111,
				117,
				34
			};
			byte[] array5 = new byte[]
			{
				126,
				6,
				1,
				1,
				0,
				122
			};
			byte[] array6 = new byte[]
			{
				130,
				130,
				16,
				6,
				0,
				230
			};
			byte[] array7 = new byte[256];
			uint num = 0U;
			List<byte> list = new List<byte>();
			if (!this.M_3C())
			{
				return;
			}
			base.Invoke(new Action(u.\u00A0));
			if (this.M_3C(array, array.Length, ref array7, ref num, 0))
			{
				string str = BitConverter.ToString(array7, 0, (int)num);
				Console.WriteLine("Response: " + str);
			}
			Thread.Sleep(150);
			if (this.M_3C(array2, array2.Length, ref array7, ref num, 0))
			{
				string str2 = BitConverter.ToString(array7, 0, (int)num);
				Console.WriteLine("Response: " + str2);
			}
			Thread.Sleep(150);
			if (this.M_3C(array3, array3.Length, ref array7, ref num, 0))
			{
				string str3 = BitConverter.ToString(array7, 0, (int)num);
				Console.WriteLine("Response: " + str3);
			}
			Thread.Sleep(150);
			if (this.M_3C(array4, array4.Length, ref array7, ref num, 0))
			{
				string str4 = BitConverter.ToString(array7, 0, (int)num);
				Console.WriteLine("Response2: " + str4);
			}
			if (this.M_3C(array5, array5.Length, ref array7, ref num, 0))
			{
				string str5 = BitConverter.ToString(array7, 0, (int)num);
				Console.WriteLine("Response7: " + str5);
			}
			if (this.M_3C(array6, array6.Length, ref array7, ref num, 0))
			{
				string str6 = BitConverter.ToString(array7, 0, (int)num);
				Console.WriteLine("Response8: " + str6);
			}
			Console.WriteLine("Response Data:");
			global::Attr_2.Form_A.\u2005 u2 = new global::Attr_2.Form_A.\u2005();
			u2.\u00A0 = u;
			u2.\u00A0 = 0;
			while (u2.\u00A0 <= 255)
			{
				byte b = (byte)(230 - u2.\u00A0);
				byte[] array8 = new byte[]
				{
					130,
					130,
					16,
					6,
					0,
					0
				};
				array8[4] = (byte)u2.\u00A0;
				array8[5] = b;
				this.M_3C(array8, array8.Length, ref array7, ref num, 0);
				if (num > 12U)
				{
					Console.Write(string.Format("[{0:F4}] {1} > ", u2.Attr_2.Attr_2.Elapsed.TotalSeconds, u2.\u00A0));
					int num2 = 10;
					while ((long)num2 < (long)((ulong)Math.Min(num, 12U)))
					{
						Console.Write(array7[num2].ToString("X2") + " ");
						if (num2 == 10 || num2 == 11)
						{
							list.Add(array7[num2]);
						}
						num2++;
					}
					Console.WriteLine();
				}
				else
				{
					Console.WriteLine(string.Format("[{0:F4}] {1} > Data kosong.", u2.Attr_2.Attr_2.Elapsed.TotalSeconds, u2.\u00A0));
				}
				base.Invoke(new Action(u2.\u00A0));
				Thread.Sleep(15);
				int u00A = u2.\u00A0;
				u2.\u00A0 = u00A + 1;
			}
			u.Attr_2.Stop();
			this.M_3C = list.ToArray();
			this.M_3C(list);
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00005374 File Offset: 0x00003574
		private void M_47()
		{
			byte[] array = new byte[]
			{
				145,
				145,
				13,
				223,
				158,
				141,
				154,
				134,
				144,
				138,
				140,
				155,
				136
			};
			byte[] array2 = new byte[]
			{
				145,
				145,
				13,
				223,
				146,
				158,
				134,
				150,
				139,
				141,
				134,
				192,
				106
			};
			byte[] array3 = new byte[]
			{
				145,
				145,
				9,
				65,
				14,
				0,
				0,
				0,
				168
			};
			byte[] array4 = new byte[256];
			uint num = 0U;
			this.M_3C(array, array.Length, ref array4, ref num, 0);
			Thread.Sleep(100);
			this.M_3C(array2, array2.Length, ref array4, ref num, 0);
			Thread.Sleep(100);
			this.M_3C(array3, array3.Length, ref array4, ref num, 0);
			Thread.Sleep(100);
			Thread.Sleep(1000);
			MessageBox.Show("การรีเซ็ตตัวนับแฟลชเสร็จสิ้น\r\nหมุน/กุญแจสตาร์ท ปิด - เปิด\r\nเป็นเวลา 5 วินาที", "ลบตัวนับแฟลช", MessageBoxButtons.OK);
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00005428 File Offset: 0x00003628
		private void M_46()
		{
			this.M_3C.Restart();
			byte[] array = new byte[]
			{
				254,
				4,
				114,
				140
			};
			byte[] array2 = new byte[]
			{
				114,
				5,
				0,
				240,
				153
			};
			byte[] array3 = new byte[]
			{
				39,
				11,
				224,
				72,
				101,
				108,
				108,
				111,
				72,
				111,
				67
			};
			byte[] array4 = new byte[]
			{
				39,
				11,
				224,
				119,
				65,
				114,
				101,
				89,
				111,
				117,
				34
			};
			byte[] array5 = new byte[]
			{
				130,
				130,
				20,
				8,
				0,
				1,
				0,
				223
			};
			if (!this.M_3C())
			{
				return;
			}
			base.Invoke(new Action(this.M_5C));
			this.M_3C(array);
			Thread.Sleep(150);
			this.M_3C(array2);
			Thread.Sleep(150);
			this.M_3C(array3);
			Thread.Sleep(150);
			this.M_3C(array4);
			Thread.Sleep(150);
			this.M_3C(array5);
			Thread.Sleep(150);
			global::Attr_2.Form_A.\u2006 u = new global::Attr_2.Form_A.\u2006();
			u.\u00A0 = this;
			u.\u00A0 = 0;
			while (u.\u00A0 <= 127)
			{
				byte[] array6 = new byte[8];
				array6[0] = 130;
				array6[1] = 130;
				array6[2] = 20;
				array6[3] = 8;
				array6[4] = (byte)u.\u00A0;
				array6[5] = 0;
				array6[6] = 0;
				array6[7] = this.M_3C(array6, 7);
				byte[] value = new byte[256];
				uint length = 0U;
				if (this.M_3C(array6, array6.Length, ref value, ref length, 0))
				{
					string text = BitConverter.ToString(value, 0, (int)length);
					Console.WriteLine("Response: " + text);
					if (text.Equals("82-82-14-08-7F-00-00-61-92-92-14-05-C3", StringComparison.OrdinalIgnoreCase))
					{
						base.Invoke(new Action(global::Attr_2.Form_A.Attr_2.Attr_2.\u00A0));
						return;
					}
				}
				base.Invoke(new Action(u.\u00A0));
				Thread.Sleep(15);
				int u00A = u.\u00A0;
				u.\u00A0 = u00A + 1;
			}
		}

		// Token: 0x0600004C RID: 76 RVA: 0x0000562C File Offset: 0x0000382C
		private void M_47()
		{
			new Stopwatch().Start();
			byte[] array = new byte[]
			{
				254,
				4,
				114,
				140
			};
			byte[] array2 = new byte[]
			{
				114,
				5,
				0,
				240,
				153
			};
			byte[] array3 = new byte[]
			{
				39,
				11,
				224,
				72,
				101,
				108,
				108,
				111,
				72,
				111,
				67
			};
			byte[] array4 = new byte[]
			{
				39,
				11,
				224,
				119,
				65,
				114,
				101,
				89,
				111,
				117,
				34
			};
			byte[] array5 = new byte[]
			{
				130,
				130,
				25,
				5,
				222
			};
			byte[] array6 = new byte[]
			{
				130,
				130,
				25,
				5,
				222
			};
			byte[] value = new byte[256];
			uint length = 0U;
			new List<byte>();
			if (!this.M_3C())
			{
				return;
			}
			base.Invoke(new Action(this.M_5D));
			if (this.M_3C(array, array.Length, ref value, ref length, 0))
			{
				string str = BitConverter.ToString(value, 0, (int)length);
				Console.WriteLine("Response: " + str);
			}
			Thread.Sleep(150);
			if (this.M_3C(array2, array2.Length, ref value, ref length, 0))
			{
				string str2 = BitConverter.ToString(value, 0, (int)length);
				Console.WriteLine("Response: " + str2);
			}
			Thread.Sleep(150);
			if (this.M_3C(array3, array3.Length, ref value, ref length, 0))
			{
				string str3 = BitConverter.ToString(value, 0, (int)length);
				Console.WriteLine("Response: " + str3);
			}
			Thread.Sleep(150);
			if (this.M_3C(array4, array4.Length, ref value, ref length, 0))
			{
				string str4 = BitConverter.ToString(value, 0, (int)length);
				Console.WriteLine("Response2: " + str4);
			}
			if (this.M_3C(array5, array5.Length, ref value, ref length, 0))
			{
				string str5 = BitConverter.ToString(value, 0, (int)length);
				Console.WriteLine("Response7: " + str5);
			}
			if (this.M_3C(array6, array6.Length, ref value, ref length, 0))
			{
				string text = BitConverter.ToString(value, 0, (int)length);
				Console.WriteLine("Response8: " + text);
				if (text.Equals("82-82-19-05-DE-92-92-19-05-BE", StringComparison.OrdinalIgnoreCase))
				{
					base.Invoke(new Action(global::Attr_2.Form_A.Attr_2.Attr_2.\u1680));
				}
				Thread.Sleep(15);
			}
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00005858 File Offset: 0x00003A58
		private void M_4D()
		{
			Stopwatch stopwatch = new Stopwatch();
			stopwatch.Start();
			byte[] array = new byte[]
			{
				254,
				4,
				114,
				140
			};
			byte[] array2 = new byte[]
			{
				114,
				5,
				0,
				240,
				153
			};
			byte[] array3 = new byte[]
			{
				39,
				11,
				224,
				72,
				101,
				108,
				108,
				111,
				72,
				111,
				67
			};
			byte[] array4 = new byte[]
			{
				39,
				11,
				224,
				119,
				65,
				114,
				101,
				89,
				111,
				117,
				34
			};
			byte[] array5 = new byte[]
			{
				130,
				130,
				24,
				5,
				223
			};
			byte[] array6 = new byte[]
			{
				130,
				130,
				24,
				5,
				223
			};
			byte[] value = new byte[256];
			uint length = 0U;
			new List<byte>();
			if (!this.M_3C())
			{
				return;
			}
			base.Invoke(new Action(this.M_5F));
			if (this.M_3C(array, array.Length, ref value, ref length, 0))
			{
				string str = BitConverter.ToString(value, 0, (int)length);
				Console.WriteLine("Response: " + str);
			}
			Thread.Sleep(150);
			if (this.M_3C(array2, array2.Length, ref value, ref length, 0))
			{
				string str2 = BitConverter.ToString(value, 0, (int)length);
				Console.WriteLine("Response: " + str2);
			}
			Thread.Sleep(150);
			if (this.M_3C(array3, array3.Length, ref value, ref length, 0))
			{
				string str3 = BitConverter.ToString(value, 0, (int)length);
				Console.WriteLine("Response: " + str3);
			}
			Thread.Sleep(150);
			if (this.M_3C(array4, array4.Length, ref value, ref length, 0))
			{
				string str4 = BitConverter.ToString(value, 0, (int)length);
				Console.WriteLine("Response2: " + str4);
			}
			if (this.M_3C(array5, array5.Length, ref value, ref length, 0))
			{
				string str5 = BitConverter.ToString(value, 0, (int)length);
				Console.WriteLine("Response7: " + str5);
			}
			if (this.M_3C(array6, array6.Length, ref value, ref length, 0))
			{
				string text = BitConverter.ToString(value, 0, (int)length);
				Console.WriteLine("Response8: " + text);
				if (text.Equals("82-82-18-05-DF-92-92-18-05-BF", StringComparison.OrdinalIgnoreCase))
				{
					base.Invoke(new Action(global::Attr_2.Form_A.Attr_2.Attr_2.\u2000));
				}
				Thread.Sleep(15);
			}
			stopwatch.Stop();
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00005A90 File Offset: 0x00003C90
		private bool M_3C(byte[] A_1, uint A_2)
		{
			byte[] array = new byte[]
			{
				130,
				130,
				0,
				9,
				0,
				146,
				146,
				0,
				6,
				0,
				214
			};
			return (ulong)A_2 >= (ulong)((long)array.Length) && A_1.Take(array.Length).SequenceEqual(array);
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00005ACC File Offset: 0x00003CCC
		private void M_4F()
		{
			string path = Path.Combine(Environment.CurrentDirectory, "read64k.log");
			global::Attr_2.Form_A.\u2007 u = new global::Attr_2.Form_A.\u2007();
			u.\u00A0 = this;
			u.\u00A0 = new StreamWriter(path, false, Encoding.UTF8);
			try
			{
				this.M_3C.Restart();
				byte[][] array = new byte[][]
				{
					new byte[]
					{
						254,
						4,
						114,
						140
					},
					new byte[]
					{
						114,
						5,
						0,
						240,
						153
					},
					new byte[]
					{
						39,
						11,
						224,
						72,
						101,
						108,
						108,
						111,
						72,
						111,
						67
					},
					new byte[]
					{
						39,
						11,
						224,
						119,
						65,
						114,
						101,
						89,
						111,
						34
					},
					new byte[]
					{
						130,
						130,
						16,
						6,
						0,
						230
					}
				};
				byte[] array2 = new byte[]
				{
					130,
					130,
					0,
					9,
					0,
					0,
					0,
					0,
					0
				};
				u.\u00A0 = new byte[256];
				u.\u00A0 = 0U;
				List<byte> list = new List<byte>();
				if (this.M_3C())
				{
					base.Invoke(new Action(this.M_60));
					Action<byte[], bool> action = new Action<byte[], bool>(u.\u00A0);
					foreach (byte[] arg in array)
					{
						action(arg, true);
					}
					array2[4] = 0;
					array2[5] = 0;
					array2[6] = 128;
					array2[7] = 12;
					int num = 0;
					for (int j = 0; j < 8; j++)
					{
						num += (int)array2[j];
					}
					array2[8] = (byte)(256 - (num & 255) & 255);
					action(array2, false);
					if (u.\u00A0 != 26U)
					{
						Console.WriteLine(string.Format("[!] reply size = {0} (≠ 26) — ทำการบันทึกไฟล์และหยุด", u.\u00A0));
						u.Attr_2.WriteLine(string.Format("[!] reply size = {0} (≠ 26) — ทำการบันทึกไฟล์และหยุด", u.\u00A0));
						this.M_3C.Stop();
						this.M_3C = list.ToArray();
						this.M_3C(list);
						u.Attr_2.Flush();
					}
					else
					{
						this.M_3C(u.\u00A0, u.\u00A0, list);
						this.M_3C(list.Count, 32768);
						Thread.Sleep(15);
						int num2 = 12;
						while (list.Count < 32768)
						{
							array2[4] = 0;
							array2[5] = (byte)(num2 & 255);
							array2[6] = (byte)(128 + num2 / 256);
							array2[7] = 12;
							int num3 = 0;
							for (int k = 0; k < 8; k++)
							{
								num3 += (int)array2[k];
							}
							array2[8] = (byte)(256 - (num3 & 255) & 255);
							action(array2, false);
							if (u.\u00A0 != 26U)
							{
								Console.WriteLine(string.Format("[!] reply size = {0} (≠ 26) — ทำการบันทึกไฟล์และหยุด", u.\u00A0));
								u.Attr_2.WriteLine(string.Format("[!] reply size = {0} (≠ 26) — ทำการบันทึกไฟล์และหยุด", u.\u00A0));
								this.M_3C.Stop();
								this.M_3C = list.ToArray();
								this.M_3C(list);
								u.Attr_2.Flush();
								return;
							}
							if (u.\u00A0 <= 4U)
							{
								string value = string.Format("[{0:F4}] หยุด: replySize <= {1} at offset={2}", this.M_3C.Elapsed.TotalSeconds, 4, num2);
								Console.WriteLine(value);
								u.Attr_2.WriteLine(value);
								break;
							}
							this.M_3C(u.\u00A0, u.\u00A0, list);
							this.M_3C(list.Count, 32768);
							Thread.Sleep(15);
							num2 += 12;
						}
						this.M_3C.Stop();
						this.M_3C = list.ToArray();
						this.M_3C(list);
						u.Attr_2.Flush();
					}
				}
			}
			finally
			{
				if (u.\u00A0 != null)
				{
					((IDisposable)u.\u00A0).Dispose();
				}
			}
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00005ED0 File Offset: 0x000040D0
		private void \u2006()
		{
			string path = Path.Combine(Environment.CurrentDirectory, "read48k.log");
			global::Attr_2.Form_A.\u2008 u = new global::Attr_2.Form_A.\u2008();
			u.\u00A0 = this;
			u.\u00A0 = new StreamWriter(path, false, Encoding.UTF8);
			try
			{
				this.M_3C.Restart();
				byte[][] array = new byte[][]
				{
					new byte[]
					{
						254,
						4,
						114,
						140
					},
					new byte[]
					{
						114,
						5,
						0,
						240,
						153
					},
					new byte[]
					{
						39,
						11,
						224,
						72,
						101,
						108,
						108,
						111,
						72,
						111,
						67
					},
					new byte[]
					{
						39,
						11,
						224,
						119,
						65,
						114,
						101,
						89,
						111,
						34
					},
					new byte[]
					{
						130,
						130,
						16,
						6,
						0,
						230
					}
				};
				byte[] array2 = new byte[]
				{
					130,
					130,
					0,
					9,
					0,
					0,
					0,
					0,
					0
				};
				u.\u00A0 = new byte[256];
				u.\u00A0 = 0U;
				List<byte> list = new List<byte>();
				if (this.M_3C())
				{
					base.Invoke(new Action(this.M_61));
					Action<byte[], bool> action = new Action<byte[], bool>(u.\u00A0);
					foreach (byte[] arg in array)
					{
						action(arg, true);
					}
					array2[4] = 0;
					array2[5] = 0;
					array2[6] = 64;
					array2[7] = 12;
					int num = 0;
					for (int j = 0; j < 8; j++)
					{
						num += (int)array2[j];
					}
					array2[8] = (byte)(256 - (num & 255) & 255);
					action(array2, false);
					this.M_3C(u.\u00A0, u.\u00A0, list);
					this.M_3C(list.Count, 49152);
					Thread.Sleep(15);
					int num2 = 12;
					while (list.Count < 49152)
					{
						array2[4] = 0;
						array2[5] = (byte)(num2 & 255);
						array2[6] = (byte)(64 + num2 / 256);
						array2[7] = 12;
						int num3 = 0;
						for (int k = 0; k < 8; k++)
						{
							num3 += (int)array2[k];
						}
						array2[8] = (byte)(256 - (num3 & 255) & 255);
						action(array2, false);
						if (u.\u00A0 <= 4U)
						{
							string value = string.Format("[{0:F4}] หยุด: replySize <= {1} at offset={2}", this.M_3C.Elapsed.TotalSeconds, 4, num2);
							Console.WriteLine(value);
							u.Attr_2.WriteLine(value);
							break;
						}
						this.M_3C(u.\u00A0, u.\u00A0, list);
						this.M_3C(list.Count, 49152);
						Thread.Sleep(15);
						num2 += 12;
					}
					this.M_3C.Stop();
					this.M_3C = list.ToArray();
					this.M_3C(list);
					u.Attr_2.Flush();
				}
			}
			finally
			{
				if (u.\u00A0 != null)
				{
					((IDisposable)u.\u00A0).Dispose();
				}
			}
		}

		// Token: 0x06000051 RID: 81 RVA: 0x000061E4 File Offset: 0x000043E4
		private void M_3C(byte[] A_1, uint A_2, List<byte> A_3)
		{
			if (A_2 <= 24U)
			{
				return;
			}
			int num = 13;
			int num2 = 24;
			int num3 = num;
			while (num3 <= num2 && (long)num3 < (long)((ulong)A_2))
			{
				A_3.Add(A_1[num3]);
				num3++;
			}
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00006218 File Offset: 0x00004418
		private void M_3C(int A_1, int A_2)
		{
			global::Attr_2.Form_A.\u2009 u = new global::Attr_2.Form_A.\u2009();
			u.\u00A0 = this;
			u.\u00A0 = A_1 * 100 / A_2;
			if (u.\u00A0 > 100)
			{
				u.\u00A0 = 100;
			}
			base.Invoke(new Action(u.\u00A0));
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00006264 File Offset: 0x00004464
		private void M_44(byte[] A_1)
		{
			byte[] array = new byte[256];
			uint num = 0U;
			bool flag = this.M_3C(A_1, A_1.Length, ref array, ref num, 0);
			this.M_3C(A_1, false, (uint)A_1.Length);
			if (flag)
			{
				this.M_3C(array, true, num);
			}
		}

		// Token: 0x06000054 RID: 84 RVA: 0x000062A4 File Offset: 0x000044A4
		private byte M_3C(byte[] A_1, int A_2)
		{
			int num = 0;
			for (int i = 0; i < A_2; i++)
			{
				num += (int)A_1[i];
			}
			return (byte)(256 - (num & 255) & 255);
		}

		// Token: 0x06000055 RID: 85 RVA: 0x0000208B File Offset: 0x0000028B
		private void M_46(object A_1, EventArgs A_2)
		{
		}

		// Token: 0x06000056 RID: 86 RVA: 0x0000208B File Offset: 0x0000028B
		private void M_47(object A_1, EventArgs A_2)
		{
		}

		// Token: 0x06000057 RID: 87 RVA: 0x0000208B File Offset: 0x0000028B
		private void M_4D(object A_1, EventArgs A_2)
		{
		}

		// Token: 0x06000058 RID: 88 RVA: 0x0000208B File Offset: 0x0000028B
		private void M_4F(object A_1, EventArgs A_2)
		{
		}

		// Token: 0x06000059 RID: 89 RVA: 0x0000208B File Offset: 0x0000028B
		private void \u2006(object A_1, EventArgs A_2)
		{
		}

		// Token: 0x0600005A RID: 90 RVA: 0x0000208B File Offset: 0x0000028B
		private void M_5A(object A_1, EventArgs A_2)
		{
		}

		// Token: 0x0600005B RID: 91 RVA: 0x0000208B File Offset: 0x0000028B
		private void M_5B(object A_1, EventArgs A_2)
		{
		}

		// Token: 0x0600005C RID: 92 RVA: 0x0000208B File Offset: 0x0000028B
		private void M_5C(object A_1, EventArgs A_2)
		{
		}

		// Token: 0x0600005D RID: 93 RVA: 0x000062DC File Offset: 0x000044DC
		private void M_5D(object A_1, EventArgs A_2)
		{
			global::Attr_2.Form_A.\u200B u200B;
			u200B.\u00A0 = AsyncVoidMethodBuilder.Create();
			u200B.\u00A0 = -1;
			u200B.Attr_2.Start<global::Attr_2.Form_A.\u200B>(ref u200B);
		}

		// Token: 0x0600005E RID: 94 RVA: 0x0000630C File Offset: 0x0000450C
		private Task M_5A()
		{
			global::Attr_2.Form_A.\u200A u200A;
			u200A.\u00A0 = AsyncTaskMethodBuilder.Create();
			u200A.\u00A0 = -1;
			u200A.Attr_2.Start<global::Attr_2.Form_A.\u200A>(ref u200A);
			return u200A.Attr_2.Task;
		}

		// Token: 0x0600005F RID: 95 RVA: 0x0000208B File Offset: 0x0000028B
		private void M_5F(object A_1, EventArgs A_2)
		{
		}

		// Token: 0x06000060 RID: 96 RVA: 0x0000208B File Offset: 0x0000028B
		private void M_60(object A_1, EventArgs A_2)
		{
		}

		// Token: 0x06000061 RID: 97 RVA: 0x0000208B File Offset: 0x0000028B
		private void M_61(object A_1, EventArgs A_2)
		{
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00006368 File Offset: 0x00004568
		private void M_5B()
		{
			this.M_3C = new ProgressBar();
			this.M_3C = new ComboBox();
			this.M_3C = new Button();
			this.M_3C = new Label();
			this.M_44 = new Label();
			this.M_3C = new TextBox();
			this.M_46 = new Label();
			this.M_47 = new Label();
			base.SuspendLayout();
			this.M_3C.ForeColor = Color.Red;
			this.M_3C.Location = new Point(16, 94);
			this.M_3C.Name = "progressBar1";
			this.M_3C.Size = new Size(367, 19);
			this.M_3C.TabIndex = 1;
			this.M_3C.Click += this.M_44;
			this.M_3C.BackColor = Color.White;
			this.M_3C.FlatStyle = FlatStyle.Flat;
			this.M_3C.Font = new Font("Microsoft Sans Serif", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 222);
			this.M_3C.FormattingEnabled = true;
			this.M_3C.Location = new Point(16, 16);
			this.M_3C.Name = "comboBox1";
			this.M_3C.Size = new Size(367, 24);
			this.M_3C.TabIndex = 2;
			this.M_3C.SelectedIndexChanged += this.M_46;
			this.M_3C.BackColor = Color.Transparent;
			this.M_3C.FlatStyle = FlatStyle.Flat;
			this.M_3C.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.M_3C.ForeColor = Color.Red;
			this.M_3C.Location = new Point(16, 55);
			this.M_3C.Name = "button1";
			this.M_3C.Size = new Size(367, 33);
			this.M_3C.TabIndex = 3;
			this.M_3C.Text = "เริ่มการทำงาน";
			this.M_3C.UseVisualStyleBackColor = false;
			this.M_3C.Click += this.M_47;
			this.M_3C.AutoSize = true;
			this.M_3C.Font = new Font("Microsoft Sans Serif", 14.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.M_3C.ForeColor = Color.Lime;
			this.M_3C.Location = new Point(38, 300);
			this.M_3C.Name = "PERSEN";
			this.M_3C.Size = new Size(17, 24);
			this.M_3C.TabIndex = 4;
			this.M_3C.Text = "-";
			this.M_3C.Click += this.M_46;
			this.M_44.AutoSize = true;
			this.M_44.ForeColor = Color.White;
			this.M_44.Location = new Point(527, 240);
			this.M_44.Name = "label2";
			this.M_44.Size = new Size(10, 13);
			this.M_44.TabIndex = 5;
			this.M_44.Text = "-";
			this.M_44.Click += this.M_47;
			this.M_3C.Cursor = Cursors.Arrow;
			this.M_3C.Location = new Point(116, 232);
			this.M_3C.Multiline = true;
			this.M_3C.Name = "textBoxLogs";
			this.M_3C.ReadOnly = true;
			this.M_3C.ScrollBars = ScrollBars.Vertical;
			this.M_3C.Size = new Size(367, 178);
			this.M_3C.TabIndex = 0;
			this.M_3C.TextChanged += this.M_3C;
			this.M_46.AutoSize = true;
			this.M_46.Font = new Font("Microsoft Sans Serif", 20.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.M_46.Location = new Point(21, 269);
			this.M_46.Name = "label1";
			this.M_46.Size = new Size(347, 31);
			this.M_46.TabIndex = 6;
			this.M_46.Text = "เลือกหัวข้อที่ต้องการดำเนินการ";
			this.M_47.AutoSize = true;
			this.M_47.FlatStyle = FlatStyle.System;
			this.M_47.Font = new Font("Microsoft Sans Serif", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 222);
			this.M_47.ForeColor = Color.Lime;
			this.M_47.Location = new Point(86, 124);
			this.M_47.Name = "label3";
			this.M_47.Size = new Size(236, 16);
			this.M_47.TabIndex = 7;
			this.M_47.Text = "---------- Facebook : เส’เอ็ม สามย่าน ---------- ";
			this.M_47.Click += this.M_61;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			this.AutoValidate = AutoValidate.Disable;
			this.BackColor = Color.Black;
			this.BackgroundImageLayout = ImageLayout.None;
			base.CausesValidation = false;
			base.ClientSize = new Size(395, 151);
			base.Controls.Add(this.M_47);
			base.Controls.Add(this.M_46);
			base.Controls.Add(this.M_3C);
			base.Controls.Add(this.M_44);
			base.Controls.Add(this.M_3C);
			base.Controls.Add(this.M_3C);
			base.Controls.Add(this.M_3C);
			base.Controls.Add(this.M_3C);
			base.Enabled = false;
			base.FormBorderStyle = FormBorderStyle.SizableToolWindow;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "pass";
			this.RightToLeftLayout = true;
			base.StartPosition = FormStartPosition.CenterScreen;
			base.Load += this.M_5C;
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x06000064 RID: 100 RVA: 0x000069DE File Offset: 0x00004BDE
		[CompilerGenerated]
		private void M_5C()
		{
			this.M_3C.Maximum = 128;
			this.M_3C.Value = 100;
		}

		// Token: 0x06000065 RID: 101 RVA: 0x000069FD File Offset: 0x00004BFD
		[CompilerGenerated]
		private void M_5D()
		{
			this.M_3C.Maximum = 256;
			this.M_3C.Value = 0;
		}

		// Token: 0x06000066 RID: 102 RVA: 0x000069FD File Offset: 0x00004BFD
		[CompilerGenerated]
		private void M_5F()
		{
			this.M_3C.Maximum = 256;
			this.M_3C.Value = 0;
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00006A1B File Offset: 0x00004C1B
		[CompilerGenerated]
		private void M_60()
		{
			this.M_3C.Maximum = 100;
			this.M_3C.Value = 0;
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00006A1B File Offset: 0x00004C1B
		[CompilerGenerated]
		private void M_61()
		{
			this.M_3C.Maximum = 100;
			this.M_3C.Value = 0;
		}

		// Token: 0x0400002C RID: 44
		private static IntPtr M_3C;

		// Token: 0x0400002D RID: 45
		private System.Windows.Forms.Timer M_3C;

		// Token: 0x0400002E RID: 46
		private bool M_3C;

		// Token: 0x0400002F RID: 47
		private int M_3C = 3;

		// Token: 0x04000030 RID: 48
		private byte[] M_3C;

		// Token: 0x04000031 RID: 49
		private List<byte> M_3C = new List<byte>();

		// Token: 0x04000032 RID: 50
		private Thread M_3C;

		// Token: 0x04000033 RID: 51
		private Stopwatch M_3C = new Stopwatch();

		// Token: 0x04000034 RID: 52
		private bool M_44;

		// Token: 0x04000035 RID: 53
		private Stopwatch M_44 = new Stopwatch();

		// Token: 0x04000037 RID: 55
		private ProgressBar M_3C;

		// Token: 0x04000038 RID: 56
		private ComboBox M_3C;

		// Token: 0x04000039 RID: 57
		private Button M_3C;

		// Token: 0x0400003A RID: 58
		private Label M_3C;

		// Token: 0x0400003B RID: 59
		private Label M_44;

		// Token: 0x0400003C RID: 60
		private TextBox M_3C;

		// Token: 0x0400003D RID: 61
		private TextBox M_44;

		// Token: 0x0400003E RID: 62
		private Label M_46;

		// Token: 0x0400003F RID: 63
		private Label M_47;

		// Token: 0x0200000B RID: 11
		[CompilerGenerated]
		[Serializable]
		private sealed class Attr_2
		{
			// Token: 0x0600006B RID: 107 RVA: 0x00006A42 File Offset: 0x00004C42
			internal void M_3C()
			{
				MessageBox.Show("รีเซ็ตสำเร็จ", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}

			// Token: 0x0600006C RID: 108 RVA: 0x00006A57 File Offset: 0x00004C57
			internal void M_44()
			{
				MessageBox.Show("รีเซ็ตสำเร็จ", "ยืนยันผลลัพธ์", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}

			// Token: 0x0600006D RID: 109 RVA: 0x00006A57 File Offset: 0x00004C57
			internal void M_46()
			{
				MessageBox.Show("รีเซ็ตสำเร็จ", "ยืนยันผลลัพธ์", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}

			// Token: 0x0600006E RID: 110 RVA: 0x00006A6C File Offset: 0x00004C6C
			internal string M_3C(byte A_1)
			{
				return A_1.ToString("X2").ToLower();
			}

			// Token: 0x0600006F RID: 111 RVA: 0x00006A6C File Offset: 0x00004C6C
			internal string M_44(byte A_1)
			{
				return A_1.ToString("X2").ToLower();
			}

			// Token: 0x06000070 RID: 112 RVA: 0x00006A6C File Offset: 0x00004C6C
			internal string M_46(byte A_1)
			{
				return A_1.ToString("X2").ToLower();
			}

			// Token: 0x06000071 RID: 113 RVA: 0x00006A6C File Offset: 0x00004C6C
			internal string M_47(byte A_1)
			{
				return A_1.ToString("X2").ToLower();
			}

			// Token: 0x04000040 RID: 64
			public static readonly global::Attr_2.Form_A.\u00A0 \u00A0 = new global::Attr_2.Form_A.\u00A0();

			// Token: 0x04000041 RID: 65
			public static Action M_3C;

			// Token: 0x04000042 RID: 66
			public static Action M_44;

			// Token: 0x04000043 RID: 67
			public static Action M_46;

			// Token: 0x04000044 RID: 68
			public static Func<byte, string> M_3C;

			// Token: 0x04000045 RID: 69
			public static Func<byte, string> M_44;

			// Token: 0x04000046 RID: 70
			public static Func<byte, string> M_46;

			// Token: 0x04000047 RID: 71
			public static Func<byte, string> M_47;
		}

		// Token: 0x0200000C RID: 12
		[CompilerGenerated]
		private sealed class Attr_3
		{
			// Token: 0x06000073 RID: 115 RVA: 0x00006A80 File Offset: 0x00004C80
			internal void M_3C()
			{
				this.M_3C.Attr_2.AppendText(this.M_3C + Environment.NewLine);
				this.M_3C.Attr_2.SelectionStart = this.M_3C.Attr_2.Text.Length;
				this.M_3C.Attr_2.ScrollToCaret();
			}

			// Token: 0x04000048 RID: 72
			public global::Attr_2.\u2006 \u00A0;

			// Token: 0x04000049 RID: 73
			public string M_3C;
		}

		// Token: 0x0200000D RID: 13
		[CompilerGenerated]
		private sealed class Form_4
		{
			// Token: 0x06000075 RID: 117 RVA: 0x00006AE2 File Offset: 0x00004CE2
			internal string M_3C(int A_1)
			{
				return this.M_3C[A_1].ToString("x2");
			}

			// Token: 0x0400004A RID: 74
			public byte[] M_3C;
		}

		// Token: 0x0200000E RID: 14
		[CompilerGenerated]
		private sealed class Attr_5
		{
			// Token: 0x06000077 RID: 119 RVA: 0x00006AFC File Offset: 0x00004CFC
			internal void M_3C()
			{
				using (SaveFileDialog saveFileDialog = new SaveFileDialog())
				{
					saveFileDialog.Title = "เลือกตำแหน่งและชื่อไฟล์สำหรับบันทึก EEPROM";
					saveFileDialog.InitialDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "File EEPROM READ ID");
					FileDialog fileDialog = saveFileDialog;
					string format = "{0} {1:dd-MM-yyyy}.bin";
					object selectedItem = this.M_3C.Attr_2.SelectedItem;
					fileDialog.FileName = string.Format(format, ((selectedItem != null) ? selectedItem.ToString() : null) ?? "Default", DateTime.Now);
					saveFileDialog.Filter = "Binary files (*.bin)|*.bin|All files (*.*)|*.*";
					saveFileDialog.DefaultExt = "bin";
					saveFileDialog.AddExtension = true;
					if (saveFileDialog.ShowDialog() == DialogResult.OK)
					{
						try
						{
							string directoryName = Path.GetDirectoryName(saveFileDialog.FileName);
							if (!Directory.Exists(directoryName))
							{
								Directory.CreateDirectory(directoryName);
							}
							File.WriteAllBytes(saveFileDialog.FileName, this.M_3C.ToArray());
							Console.WriteLine("Data berhasil disimpan ke: " + saveFileDialog.FileName);
							this.M_3C.Attr_2.Value = 0;
							return;
						}
						catch (Exception ex)
						{
							Console.WriteLine("Gagal menyimpan file: " + ex.Message);
							return;
						}
					}
					Console.WriteLine("ผู้ใช้ยกเลิกการบันทึกไฟล์");
				}
			}

			// Token: 0x0400004B RID: 75
			public global::Attr_2.\u2006 \u00A0;

			// Token: 0x0400004C RID: 76
			public List<byte> M_3C;
		}

		// Token: 0x0200000F RID: 15
		[CompilerGenerated]
		private sealed class Type_6
		{
			// Token: 0x06000079 RID: 121 RVA: 0x00006C50 File Offset: 0x00004E50
			internal void M_3C()
			{
				this.M_3C.Attr_2.Maximum = 256;
				this.M_3C.Attr_2.Value = 0;
			}

			// Token: 0x0400004D RID: 77
			public global::Attr_2.\u2006 \u00A0;

			// Token: 0x0400004E RID: 78
			public Stopwatch M_3C;
		}

		// Token: 0x02000010 RID: 16
		[CompilerGenerated]
		private sealed class Type_7
		{
			// Token: 0x0600007B RID: 123 RVA: 0x00006C78 File Offset: 0x00004E78
			internal void M_3C()
			{
				this.M_3C.Attr_2.Attr_2.Value = this.M_3C + 1;
				int num = (this.M_3C + 1) * 100 / 256;
				this.M_3C.Attr_2.Attr_2.Text = string.Format("{0} %", num);
				TimeSpan elapsed = this.M_3C.Attr_2.Elapsed;
				this.M_3C.Attr_2.Attr_3.Text = string.Format("{0} Detik", elapsed.Seconds);
			}

			// Token: 0x0400004F RID: 79
			public int M_3C;

			// Token: 0x04000050 RID: 80
			public global::Attr_2.Form_A.\u2002 \u00A0;
		}

		// Token: 0x02000011 RID: 17
		[CompilerGenerated]
		private sealed class Form_8
		{
			// Token: 0x0600007D RID: 125 RVA: 0x00006D15 File Offset: 0x00004F15
			internal void M_3C()
			{
				this.M_3C.Attr_2.Maximum = 256;
				this.M_3C.Attr_2.Value = 0;
			}

			// Token: 0x04000051 RID: 81
			public global::Attr_2.\u2006 \u00A0;

			// Token: 0x04000052 RID: 82
			public Stopwatch M_3C;
		}

		// Token: 0x02000012 RID: 18
		[CompilerGenerated]
		private sealed class Form_9
		{
			// Token: 0x0600007F RID: 127 RVA: 0x00006D40 File Offset: 0x00004F40
			internal void M_3C()
			{
				this.M_3C.Attr_2.Attr_2.Value = this.M_3C + 1;
				int num = (this.M_3C + 1) * 100 / 256;
				this.M_3C.Attr_2.Attr_2.Text = string.Format("{0} %", num);
				this.M_3C.Attr_2.Attr_3.Text = string.Format("{0:F1} s", this.M_3C.Attr_2.Elapsed.TotalSeconds);
			}

			// Token: 0x04000053 RID: 83
			public int M_3C;

			// Token: 0x04000054 RID: 84
			public global::Attr_2.Form_A.\u2004 \u00A0;
		}

		// Token: 0x02000013 RID: 19
		[CompilerGenerated]
		private sealed class Form_A
		{
			// Token: 0x06000081 RID: 129 RVA: 0x00006DE0 File Offset: 0x00004FE0
			internal void M_3C()
			{
				this.M_3C.Attr_2.Value = this.M_3C + 1;
				int num = (this.M_3C + 1) * 100 / 256;
				this.M_3C.Attr_2.Text = string.Format("{0} %", num);
				this.M_3C.Attr_3.Text = string.Format("{0:F1} s", this.M_3C.Attr_2.Elapsed.TotalSeconds);
			}

			// Token: 0x04000055 RID: 85
			public int M_3C;

			// Token: 0x04000056 RID: 86
			public global::Attr_2.\u2006 \u00A0;
		}

		// Token: 0x02000014 RID: 20
		[CompilerGenerated]
		private sealed class Type_14
		{
			// Token: 0x06000083 RID: 131 RVA: 0x00006E70 File Offset: 0x00005070
			internal void M_3C(byte[] A_1, bool A_2)
			{
				this.M_3C.\u00A0(A_1, A_1.Length, ref this.M_3C, ref this.M_3C, 0);
				string arg = string.Join(", ", A_1.Select(new Func<byte, string>(global::Attr_2.Form_A.Attr_2.Attr_2.\u00A0)));
				string value = string.Format("[{0:F4}] 0 > [{1}]", this.M_3C.Attr_2.Elapsed.TotalSeconds, arg);
				Console.WriteLine(value);
				this.M_3C.WriteLine(value);
				if (this.M_3C > 0U)
				{
					string arg2 = string.Join(", ", this.M_3C.Take((int)this.M_3C).Select(new Func<byte, string>(global::Attr_2.Form_A.Attr_2.Attr_2.\u1680)));
					string value2 = string.Format("[{0:F4}] 0 < [{1}]", this.M_3C.Attr_2.Elapsed.TotalSeconds, arg2);
					Console.WriteLine(value2);
					this.M_3C.WriteLine(value2);
				}
				if (A_2)
				{
					Thread.Sleep(150);
				}
			}

			// Token: 0x04000057 RID: 87
			public StreamWriter M_3C;

			// Token: 0x04000058 RID: 88
			public byte[] M_3C;

			// Token: 0x04000059 RID: 89
			public uint M_3C;

			// Token: 0x0400005A RID: 90
			public global::Attr_2.\u2006 \u00A0;
		}

		// Token: 0x02000015 RID: 21
		[CompilerGenerated]
		private sealed class Type_15
		{
			// Token: 0x06000085 RID: 133 RVA: 0x00006F9C File Offset: 0x0000519C
			internal void M_3C(byte[] A_1, bool A_2)
			{
				this.M_3C.\u00A0(A_1, A_1.Length, ref this.M_3C, ref this.M_3C, 0);
				string arg = string.Join(", ", A_1.Select(new Func<byte, string>(global::Attr_2.Form_A.Attr_2.Attr_2.\u2000)));
				string value = string.Format("[{0:F4}] 0 > [{1}]", this.M_3C.Attr_2.Elapsed.TotalSeconds, arg);
				Console.WriteLine(value);
				this.M_3C.WriteLine(value);
				if (this.M_3C > 0U)
				{
					string arg2 = string.Join(", ", this.M_3C.Take((int)this.M_3C).Select(new Func<byte, string>(global::Attr_2.Form_A.Attr_2.Attr_2.\u2001)));
					string value2 = string.Format("[{0:F4}] 0 < [{1}]", this.M_3C.Attr_2.Elapsed.TotalSeconds, arg2);
					Console.WriteLine(value2);
					this.M_3C.WriteLine(value2);
				}
				if (A_2)
				{
					Thread.Sleep(150);
				}
			}

			// Token: 0x0400005B RID: 91
			public StreamWriter M_3C;

			// Token: 0x0400005C RID: 92
			public byte[] M_3C;

			// Token: 0x0400005D RID: 93
			public uint M_3C;

			// Token: 0x0400005E RID: 94
			public global::Attr_2.\u2006 \u00A0;
		}

		// Token: 0x02000016 RID: 22
		[CompilerGenerated]
		private sealed class Type_16
		{
			// Token: 0x06000087 RID: 135 RVA: 0x000070C8 File Offset: 0x000052C8
			internal void M_3C()
			{
				this.M_3C.Attr_2.Value = this.M_3C;
				this.M_3C.Attr_3.Text = string.Format("{0} s", this.M_3C.Attr_2.Elapsed.Seconds);
				this.M_3C.Attr_2.Text = string.Format("{0} %", this.M_3C);
			}

			// Token: 0x0400005F RID: 95
			public global::Attr_2.\u2006 \u00A0;

			// Token: 0x04000060 RID: 96
			public int M_3C;
		}
	}
}
