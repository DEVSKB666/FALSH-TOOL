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
using ns1;

namespace ns0
{
	// Token: 0x0200000A RID: 10
	public partial class GForm3 : Form
	{
		// Token: 0x0600003A RID: 58 RVA: 0x00018128 File Offset: 0x00016328
		public GForm3()
		{
			this.method_29();
			base.FormClosing += this.GForm3_FormClosing;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.StartPosition = FormStartPosition.CenterScreen;
			base.FormBorderStyle = FormBorderStyle.FixedDialog;
			this.comboBox_0.Items.Clear();
			this.timer_0 = new System.Windows.Forms.Timer();
			this.timer_0.Interval = 3000;
			this.comboBox_0.Items.Add("ดูดไฟล์กล่อง 48K");
			this.comboBox_0.Items.Add("ดูดไฟล์กล่อง 64K");
		}

		// Token: 0x0600003B RID: 59 RVA: 0x0000C48D File Offset: 0x0000A68D
		public GForm3(Icon icon_0) : this()
		{
			base.Icon = icon_0;
			base.FormClosing += this.GForm3_FormClosing;
		}

		// Token: 0x0600003C RID: 60 RVA: 0x000181F0 File Offset: 0x000163F0
		private void GForm3_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (this.thread_0 != null && this.thread_0.IsAlive)
			{
				try
				{
					this.thread_0.Join(1000);
				}
				catch
				{
				}
			}
			if (GForm3.intptr_0 != IntPtr.Zero)
			{
				try
				{
					Class122.FT_Close(GForm3.intptr_0);
					GForm3.intptr_0 = IntPtr.Zero;
					Thread.Sleep(100);
				}
				catch
				{
				}
			}
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00018278 File Offset: 0x00016478
		private bool method_0()
		{
			uint num = 0U;
			if (Class122.FT_Open(0U, ref GForm3.intptr_0) > Class122.Enum0.const_0)
			{
				try
				{
					Class122.FT_Close(GForm3.intptr_0);
				}
				catch
				{
				}
				return false;
			}
			if (Class122.FT_Purge(GForm3.intptr_0, 3U) > Class122.Enum0.const_0)
			{
				Class122.FT_Close(GForm3.intptr_0);
				return false;
			}
			if (Class122.FT_SetLatencyTimer(GForm3.intptr_0, 2) > Class122.Enum0.const_0)
			{
				Class122.FT_Close(GForm3.intptr_0);
				return false;
			}
			if (Class122.FT_SetDataCharacteristics(GForm3.intptr_0, 8, 0, 0) > Class122.Enum0.const_0)
			{
				Class122.FT_Close(GForm3.intptr_0);
				return false;
			}
			if (Class122.FT_SetBaudRate(GForm3.intptr_0, 10400U) > Class122.Enum0.const_0)
			{
				Class122.FT_Close(GForm3.intptr_0);
				return false;
			}
			if (Class122.FT_SetTimeouts(GForm3.intptr_0, 200U, 200U) > Class122.Enum0.const_0)
			{
				Class122.FT_Close(GForm3.intptr_0);
				return false;
			}
			if (Class122.FT_SetBitMode(GForm3.intptr_0, 0, 0) > Class122.Enum0.const_0)
			{
				Class122.FT_Close(GForm3.intptr_0);
				return false;
			}
			if (Class122.FT_SetBitMode(GForm3.intptr_0, 1, 1) > Class122.Enum0.const_0)
			{
				Class122.FT_Close(GForm3.intptr_0);
				return false;
			}
			byte[] array = new byte[1];
			byte[] array2 = new byte[]
			{
				1
			};
			if (Class122.FT_Write(GForm3.intptr_0, array, (uint)array.Length, ref num) > Class122.Enum0.const_0)
			{
				Class122.FT_Close(GForm3.intptr_0);
				return false;
			}
			Thread.Sleep(70);
			if (Class122.FT_Write(GForm3.intptr_0, array2, (uint)array2.Length, ref num) > Class122.Enum0.const_0)
			{
				Class122.FT_Close(GForm3.intptr_0);
				return false;
			}
			if (Class122.FT_SetBitMode(GForm3.intptr_0, 0, 0) > Class122.Enum0.const_0)
			{
				Class122.FT_Close(GForm3.intptr_0);
				return false;
			}
			if (Class122.FT_SetBaudRate(GForm3.intptr_0, 10400U) > Class122.Enum0.const_0)
			{
				Class122.FT_Close(GForm3.intptr_0);
				return false;
			}
			if (Class122.FT_Purge(GForm3.intptr_0, 3U) > Class122.Enum0.const_0)
			{
				Class122.FT_Close(GForm3.intptr_0);
				return false;
			}
			Thread.Sleep(130);
			return true;
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00018440 File Offset: 0x00016640
		private bool method_1(byte[] byte_1, int int_1, ref byte[] byte_2, ref uint uint_0, int int_2 = 0)
		{
			Class122.FT_Purge(GForm3.intptr_0, 1U);
			uint num = 0U;
			if (Class122.FT_Write(GForm3.intptr_0, byte_1, (uint)int_1, ref num) <= Class122.Enum0.const_0)
			{
				if (num == (uint)int_1)
				{
					this.method_2("ส่ง >> " + BitConverter.ToString(byte_1, 0, int_1));
					if (int_2 > 0)
					{
						Thread.Sleep(int_2);
					}
					int num2 = Math.Min(180, Math.Max(80, 40 + 6 * int_1));
					byte[] array = new byte[512];
					List<byte> list = new List<byte>(256);
					uint num3 = 0U;
					uint num4 = 0U;
					Stopwatch stopwatch = Stopwatch.StartNew();
					Stopwatch stopwatch2 = Stopwatch.StartNew();
					while (stopwatch.ElapsedMilliseconds < (long)num2)
					{
						if (Class122.FT_GetQueueStatus(GForm3.intptr_0, ref num3) == Class122.Enum0.const_0 && num3 > 0U)
						{
							if ((ulong)num3 > (ulong)((long)array.Length))
							{
								num3 = (uint)array.Length;
							}
							if (Class122.FT_Read(GForm3.intptr_0, array, num3, ref num4) == Class122.Enum0.const_0 && num4 > 0U)
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
						byte_2 = list.ToArray();
						uint_0 = (uint)list.Count;
						this.method_2("รับ << " + BitConverter.ToString(byte_2, 0, (int)uint_0));
						return true;
					}
					this.method_2("รับ << ไม่มีข้อมูลตอบกลับ");
					return false;
				}
			}
			this.method_2("ส่ง >> " + BitConverter.ToString(byte_1, 0, int_1) + " (ส่งล้มเหลว)");
			return false;
		}

		// Token: 0x0600003F RID: 63 RVA: 0x000185B4 File Offset: 0x000167B4
		private void method_2(string string_0)
		{
			GForm3.Class1 @class = new GForm3.Class1();
			@class.gform3_0 = this;
			@class.string_0 = string_0;
			if (this.textBox_0.InvokeRequired)
			{
				this.textBox_0.Invoke(new Action(@class.method_0));
				return;
			}
			this.textBox_0.AppendText(@class.string_0 + Environment.NewLine);
			this.textBox_0.SelectionStart = this.textBox_0.Text.Length;
			this.textBox_0.ScrollToCaret();
		}

		// Token: 0x06000040 RID: 64 RVA: 0x0001863C File Offset: 0x0001683C
		private void method_3(byte[] byte_1, bool bool_2, uint uint_0)
		{
			GForm3.Class2 @class = new GForm3.Class2();
			@class.byte_0 = byte_1;
			string text = this.stopwatch_0.Elapsed.TotalSeconds.ToString("F4");
			string text2 = bool_2 ? "<" : ">";
			IEnumerable<string> values = Enumerable.Range(0, (int)uint_0).Select(new Func<int, string>(@class.method_0));
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

		// Token: 0x06000041 RID: 65 RVA: 0x000186E8 File Offset: 0x000168E8
		private bool method_4(byte[] byte_1)
		{
			byte[] byte_2 = new byte[256];
			uint uint_ = 0U;
			bool flag = this.method_1(byte_1, byte_1.Length, ref byte_2, ref uint_, 0);
			this.method_3(byte_1, false, (uint)byte_1.Length);
			if (flag)
			{
				this.method_3(byte_2, true, uint_);
			}
			return flag;
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00018728 File Offset: 0x00016928
		private void method_5(List<byte> list_1)
		{
			GForm3.Class3 @class = new GForm3.Class3();
			@class.gform3_0 = this;
			@class.list_0 = list_1;
			base.Invoke(new Action(@class.method_0));
		}

		// Token: 0x06000043 RID: 67 RVA: 0x0000C303 File Offset: 0x0000A503
		private void textBox_0_TextChanged(object sender, EventArgs e)
		{
		}

		// Token: 0x06000044 RID: 68 RVA: 0x0000C303 File Offset: 0x0000A503
		private void progressBar_0_Click(object sender, EventArgs e)
		{
		}

		// Token: 0x06000045 RID: 69 RVA: 0x0000C4AE File Offset: 0x0000A6AE
		private byte method_6(string string_0)
		{
			if (string.IsNullOrEmpty(string_0))
			{
				throw new ArgumentException("Input tidak boleh kosong.");
			}
			return Convert.ToByte(string_0, 16);
		}

		// Token: 0x06000046 RID: 70 RVA: 0x0000C303 File Offset: 0x0000A503
		private void comboBox_0_SelectedIndexChanged(object sender, EventArgs e)
		{
		}

		// Token: 0x06000047 RID: 71 RVA: 0x0001875C File Offset: 0x0001695C
		private void button_0_Click(object sender, EventArgs e)
		{
			object selectedItem = this.comboBox_0.SelectedItem;
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
					if (this.thread_0 != null && this.thread_0.IsAlive)
					{
						MessageBox.Show("Process is already running.");
						return;
					}
					this.thread_0 = new Thread(new ThreadStart(this.method_8))
					{
						IsBackground = true
					};
					this.thread_0.Start();
					return;
				}
			}
			else if (text == "ลบจำนวนการอัด Kh")
			{
				if (this.thread_0 != null && this.thread_0.IsAlive)
				{
					MessageBox.Show("Process is already running.");
					return;
				}
				this.thread_0 = new Thread(new ThreadStart(this.method_10))
				{
					IsBackground = true
				};
				this.thread_0.Start();
				return;
			}
			else if (text == "Format EEPROM 0x00")
			{
				if (this.thread_0 != null && this.thread_0.IsAlive)
				{
					MessageBox.Show("Process is already running.");
					return;
				}
				this.thread_0 = new Thread(new ThreadStart(this.method_11))
				{
					IsBackground = true
				};
				this.thread_0.Start();
				return;
			}
			else if (text == "Format EEPROM 0xFF")
			{
				if (this.thread_0 != null && this.thread_0.IsAlive)
				{
					MessageBox.Show("Process is already running.");
					return;
				}
				this.thread_0 = new Thread(new ThreadStart(this.method_12))
				{
					IsBackground = true
				};
				this.thread_0.Start();
				return;
			}
			else if (text == "READ EEPROM Sh")
			{
				if (this.thread_0 != null && this.thread_0.IsAlive)
				{
					MessageBox.Show("Process is already running.");
					return;
				}
				this.thread_0 = new Thread(new ThreadStart(this.method_7))
				{
					IsBackground = true
				};
				this.thread_0.Start();
				return;
			}
			else if (text == "ดูดไฟล์กล่อง 48K")
			{
				if (this.thread_0 != null && this.thread_0.IsAlive)
				{
					MessageBox.Show("Process is already running.");
					return;
				}
				this.thread_0 = new Thread(new ThreadStart(this.method_15))
				{
					IsBackground = true
				};
				this.thread_0.Start();
				return;
			}
			else if (text == "ดูดไฟล์กล่อง 64K")
			{
				if (this.thread_0 != null && this.thread_0.IsAlive)
				{
					MessageBox.Show("Process is already running.");
					return;
				}
				this.thread_0 = new Thread(new ThreadStart(this.method_14))
				{
					IsBackground = true
				};
				this.thread_0.Start();
				return;
			}
			else if (text == "ลบจำนวนการอัด Sh")
			{
				if (MessageBox.Show("OFF สวิตซ์ และ ON\r\nภายใน 3 วินาที\r\nและกด OK", "WRITE EEPROM", MessageBoxButtons.OK) == DialogResult.OK)
				{
					if (this.thread_0 != null && this.thread_0.IsAlive)
					{
						MessageBox.Show("Process is already running.");
						return;
					}
					this.thread_0 = new Thread(new ThreadStart(this.method_9))
					{
						IsBackground = true
					};
					this.thread_0.Start();
					return;
				}
			}
			else
			{
				MessageBox.Show("คำสั่งไม่รู้จัก");
			}
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00018A8C File Offset: 0x00016C8C
		private void method_7()
		{
			GForm3.Class4 @class = new GForm3.Class4();
			@class.gform3_0 = this;
			@class.stopwatch_0 = new Stopwatch();
			@class.stopwatch_0.Start();
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
			if (!this.method_0())
			{
				return;
			}
			base.Invoke(new Action(@class.method_0));
			if (this.method_1(array, array.Length, ref array5, ref num, 0))
			{
				string str = BitConverter.ToString(array5, 0, (int)num);
				Console.WriteLine("Response: " + str);
			}
			Thread.Sleep(150);
			if (this.method_1(array2, array2.Length, ref array5, ref num, 0))
			{
				string str2 = BitConverter.ToString(array5, 0, (int)num);
				Console.WriteLine("Response: " + str2);
			}
			Thread.Sleep(150);
			if (this.method_1(array3, array3.Length, ref array5, ref num, 0))
			{
				string str3 = BitConverter.ToString(array5, 0, (int)num);
				Console.WriteLine("Response: " + str3);
			}
			Thread.Sleep(150);
			if (this.method_1(array4, array4.Length, ref array5, ref num, 0))
			{
				string str4 = BitConverter.ToString(array5, 0, (int)num);
				Console.WriteLine("Response2: " + str4);
			}
			Console.WriteLine("Response Data:");
			GForm3.Class5 class2 = new GForm3.Class5();
			class2.class4_0 = @class;
			class2.int_0 = 0;
			while (class2.int_0 <= 255)
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
				this.method_1(array6, array6.Length, ref array5, ref num, 0);
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
				base.Invoke(new Action(class2.method_0));
				int num3 = class2.int_0;
				class2.int_0 = num3 + 1;
			}
			@class.stopwatch_0.Stop();
			this.byte_0 = list.ToArray();
			this.method_5(list);
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00018D28 File Offset: 0x00016F28
		private void method_8()
		{
			GForm3.Class6 @class = new GForm3.Class6();
			@class.gform3_0 = this;
			@class.stopwatch_0 = new Stopwatch();
			@class.stopwatch_0.Start();
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
			if (!this.method_0())
			{
				return;
			}
			base.Invoke(new Action(@class.method_0));
			if (this.method_1(array, array.Length, ref array7, ref num, 0))
			{
				string str = BitConverter.ToString(array7, 0, (int)num);
				Console.WriteLine("Response: " + str);
			}
			Thread.Sleep(150);
			if (this.method_1(array2, array2.Length, ref array7, ref num, 0))
			{
				string str2 = BitConverter.ToString(array7, 0, (int)num);
				Console.WriteLine("Response: " + str2);
			}
			Thread.Sleep(150);
			if (this.method_1(array3, array3.Length, ref array7, ref num, 0))
			{
				string str3 = BitConverter.ToString(array7, 0, (int)num);
				Console.WriteLine("Response: " + str3);
			}
			Thread.Sleep(150);
			if (this.method_1(array4, array4.Length, ref array7, ref num, 0))
			{
				string str4 = BitConverter.ToString(array7, 0, (int)num);
				Console.WriteLine("Response2: " + str4);
			}
			if (this.method_1(array5, array5.Length, ref array7, ref num, 0))
			{
				string str5 = BitConverter.ToString(array7, 0, (int)num);
				Console.WriteLine("Response7: " + str5);
			}
			if (this.method_1(array6, array6.Length, ref array7, ref num, 0))
			{
				string str6 = BitConverter.ToString(array7, 0, (int)num);
				Console.WriteLine("Response8: " + str6);
			}
			Console.WriteLine("Response Data:");
			GForm3.Class7 class2 = new GForm3.Class7();
			class2.class6_0 = @class;
			class2.int_0 = 0;
			while (class2.int_0 <= 255)
			{
				byte b = (byte)(230 - class2.int_0);
				byte[] array8 = new byte[]
				{
					130,
					130,
					16,
					6,
					0,
					0
				};
				array8[4] = (byte)class2.int_0;
				array8[5] = b;
				this.method_1(array8, array8.Length, ref array7, ref num, 0);
				if (num > 12U)
				{
					Console.Write(string.Format("[{0:F4}] {1} > ", class2.class6_0.stopwatch_0.Elapsed.TotalSeconds, class2.int_0));
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
					Console.WriteLine(string.Format("[{0:F4}] {1} > Data kosong.", class2.class6_0.stopwatch_0.Elapsed.TotalSeconds, class2.int_0));
				}
				base.Invoke(new Action(class2.method_0));
				Thread.Sleep(15);
				int num3 = class2.int_0;
				class2.int_0 = num3 + 1;
			}
			@class.stopwatch_0.Stop();
			this.byte_0 = list.ToArray();
			this.method_5(list);
		}

		// Token: 0x0600004A RID: 74 RVA: 0x000190DC File Offset: 0x000172DC
		private void method_9()
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
			this.method_1(array, array.Length, ref array4, ref num, 0);
			Thread.Sleep(100);
			this.method_1(array2, array2.Length, ref array4, ref num, 0);
			Thread.Sleep(100);
			this.method_1(array3, array3.Length, ref array4, ref num, 0);
			Thread.Sleep(100);
			Thread.Sleep(1000);
			MessageBox.Show("การรีเซ็ตตัวนับแฟลชเสร็จสิ้น\r\nหมุน/กุญแจสตาร์ท ปิด - เปิด\r\nเป็นเวลา 5 วินาที", "ลบตัวนับแฟลช", MessageBoxButtons.OK);
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00019190 File Offset: 0x00017390
		private void method_10()
		{
			this.stopwatch_0.Restart();
			byte[] byte_ = new byte[]
			{
				254,
				4,
				114,
				140
			};
			byte[] byte_2 = new byte[]
			{
				114,
				5,
				0,
				240,
				153
			};
			byte[] byte_3 = new byte[]
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
			byte[] byte_4 = new byte[]
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
			byte[] byte_5 = new byte[]
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
			if (!this.method_0())
			{
				return;
			}
			base.Invoke(new Action(this.method_30));
			this.method_4(byte_);
			Thread.Sleep(150);
			this.method_4(byte_2);
			Thread.Sleep(150);
			this.method_4(byte_3);
			Thread.Sleep(150);
			this.method_4(byte_4);
			Thread.Sleep(150);
			this.method_4(byte_5);
			Thread.Sleep(150);
			GForm3.Class8 @class = new GForm3.Class8();
			@class.gform3_0 = this;
			@class.int_0 = 0;
			while (@class.int_0 <= 127)
			{
				byte[] array = new byte[8];
				array[0] = 130;
				array[1] = 130;
				array[2] = 20;
				array[3] = 8;
				array[4] = (byte)@class.int_0;
				array[5] = 0;
				array[6] = 0;
				array[7] = this.method_19(array, 7);
				byte[] value = new byte[256];
				uint length = 0U;
				if (this.method_1(array, array.Length, ref value, ref length, 0))
				{
					string text = BitConverter.ToString(value, 0, (int)length);
					Console.WriteLine("Response: " + text);
					if (text.Equals("82-82-14-08-7F-00-00-61-92-92-14-05-C3", StringComparison.OrdinalIgnoreCase))
					{
						base.Invoke(new Action(GForm3.Class0.class0_0.method_0));
						return;
					}
				}
				base.Invoke(new Action(@class.method_0));
				Thread.Sleep(15);
				int num = @class.int_0;
				@class.int_0 = num + 1;
			}
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00019394 File Offset: 0x00017594
		private void method_11()
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
			if (!this.method_0())
			{
				return;
			}
			base.Invoke(new Action(this.method_31));
			if (this.method_1(array, array.Length, ref value, ref length, 0))
			{
				string str = BitConverter.ToString(value, 0, (int)length);
				Console.WriteLine("Response: " + str);
			}
			Thread.Sleep(150);
			if (this.method_1(array2, array2.Length, ref value, ref length, 0))
			{
				string str2 = BitConverter.ToString(value, 0, (int)length);
				Console.WriteLine("Response: " + str2);
			}
			Thread.Sleep(150);
			if (this.method_1(array3, array3.Length, ref value, ref length, 0))
			{
				string str3 = BitConverter.ToString(value, 0, (int)length);
				Console.WriteLine("Response: " + str3);
			}
			Thread.Sleep(150);
			if (this.method_1(array4, array4.Length, ref value, ref length, 0))
			{
				string str4 = BitConverter.ToString(value, 0, (int)length);
				Console.WriteLine("Response2: " + str4);
			}
			if (this.method_1(array5, array5.Length, ref value, ref length, 0))
			{
				string str5 = BitConverter.ToString(value, 0, (int)length);
				Console.WriteLine("Response7: " + str5);
			}
			if (this.method_1(array6, array6.Length, ref value, ref length, 0))
			{
				string text = BitConverter.ToString(value, 0, (int)length);
				Console.WriteLine("Response8: " + text);
				if (text.Equals("82-82-19-05-DE-92-92-19-05-BE", StringComparison.OrdinalIgnoreCase))
				{
					base.Invoke(new Action(GForm3.Class0.class0_0.method_1));
				}
				Thread.Sleep(15);
			}
		}

		// Token: 0x0600004D RID: 77 RVA: 0x000195C0 File Offset: 0x000177C0
		private void method_12()
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
			if (!this.method_0())
			{
				return;
			}
			base.Invoke(new Action(this.method_32));
			if (this.method_1(array, array.Length, ref value, ref length, 0))
			{
				string str = BitConverter.ToString(value, 0, (int)length);
				Console.WriteLine("Response: " + str);
			}
			Thread.Sleep(150);
			if (this.method_1(array2, array2.Length, ref value, ref length, 0))
			{
				string str2 = BitConverter.ToString(value, 0, (int)length);
				Console.WriteLine("Response: " + str2);
			}
			Thread.Sleep(150);
			if (this.method_1(array3, array3.Length, ref value, ref length, 0))
			{
				string str3 = BitConverter.ToString(value, 0, (int)length);
				Console.WriteLine("Response: " + str3);
			}
			Thread.Sleep(150);
			if (this.method_1(array4, array4.Length, ref value, ref length, 0))
			{
				string str4 = BitConverter.ToString(value, 0, (int)length);
				Console.WriteLine("Response2: " + str4);
			}
			if (this.method_1(array5, array5.Length, ref value, ref length, 0))
			{
				string str5 = BitConverter.ToString(value, 0, (int)length);
				Console.WriteLine("Response7: " + str5);
			}
			if (this.method_1(array6, array6.Length, ref value, ref length, 0))
			{
				string text = BitConverter.ToString(value, 0, (int)length);
				Console.WriteLine("Response8: " + text);
				if (text.Equals("82-82-18-05-DF-92-92-18-05-BF", StringComparison.OrdinalIgnoreCase))
				{
					base.Invoke(new Action(GForm3.Class0.class0_0.method_2));
				}
				Thread.Sleep(15);
			}
			stopwatch.Stop();
		}

		// Token: 0x0600004E RID: 78 RVA: 0x000197F8 File Offset: 0x000179F8
		private bool method_13(byte[] byte_1, uint uint_0)
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
			return (ulong)uint_0 >= (ulong)((long)array.Length) && byte_1.Take(array.Length).SequenceEqual(array);
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00019834 File Offset: 0x00017A34
		private void method_14()
		{
			string path = Path.Combine(Environment.CurrentDirectory, "read64k.log");
			GForm3.Class9 @class = new GForm3.Class9();
			@class.gform3_0 = this;
			@class.streamWriter_0 = new StreamWriter(path, false, Encoding.UTF8);
			try
			{
				this.stopwatch_0.Restart();
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
				@class.byte_0 = new byte[256];
				@class.uint_0 = 0U;
				List<byte> list = new List<byte>();
				if (this.method_0())
				{
					base.Invoke(new Action(this.method_33));
					Action<byte[], bool> action = new Action<byte[], bool>(@class.method_0);
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
					if (@class.uint_0 == 26U)
					{
						this.method_16(@class.byte_0, @class.uint_0, list);
						this.method_17(list.Count, 32768);
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
							if (@class.uint_0 != 26U)
							{
								Console.WriteLine(string.Format("[!] reply size = {0} (≠ 26) — ทำการบันทึกไฟล์และหยุด", @class.uint_0));
								@class.streamWriter_0.WriteLine(string.Format("[!] reply size = {0} (≠ 26) — ทำการบันทึกไฟล์และหยุด", @class.uint_0));
								this.stopwatch_0.Stop();
								this.byte_0 = list.ToArray();
								this.method_5(list);
								@class.streamWriter_0.Flush();
								return;
							}
							if (@class.uint_0 <= 4U)
							{
								string value = string.Format("[{0:F4}] หยุด: replySize <= {1} at offset={2}", this.stopwatch_0.Elapsed.TotalSeconds, 4, num2);
								Console.WriteLine(value);
								@class.streamWriter_0.WriteLine(value);
								IL_39F:
								this.stopwatch_0.Stop();
								this.byte_0 = list.ToArray();
								this.method_5(list);
								@class.streamWriter_0.Flush();
								return;
							}
							this.method_16(@class.byte_0, @class.uint_0, list);
							this.method_17(list.Count, 32768);
							Thread.Sleep(15);
							num2 += 12;
						}
						goto IL_39F;
					}
					Console.WriteLine(string.Format("[!] reply size = {0} (≠ 26) — ทำการบันทึกไฟล์และหยุด", @class.uint_0));
					@class.streamWriter_0.WriteLine(string.Format("[!] reply size = {0} (≠ 26) — ทำการบันทึกไฟล์และหยุด", @class.uint_0));
					this.stopwatch_0.Stop();
					this.byte_0 = list.ToArray();
					this.method_5(list);
					@class.streamWriter_0.Flush();
				}
			}
			finally
			{
				if (@class.streamWriter_0 != null)
				{
					((IDisposable)@class.streamWriter_0).Dispose();
				}
			}
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00019C40 File Offset: 0x00017E40
		private void method_15()
		{
			string path = Path.Combine(Environment.CurrentDirectory, "read48k.log");
			GForm3.Class10 @class = new GForm3.Class10();
			@class.gform3_0 = this;
			@class.streamWriter_0 = new StreamWriter(path, false, Encoding.UTF8);
			try
			{
				this.stopwatch_0.Restart();
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
				@class.byte_0 = new byte[256];
				@class.uint_0 = 0U;
				List<byte> list = new List<byte>();
				if (this.method_0())
				{
					base.Invoke(new Action(this.method_34));
					Action<byte[], bool> action = new Action<byte[], bool>(@class.method_0);
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
					this.method_16(@class.byte_0, @class.uint_0, list);
					this.method_17(list.Count, 49152);
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
						if (@class.uint_0 <= 4U)
						{
							string value = string.Format("[{0:F4}] หยุด: replySize <= {1} at offset={2}", this.stopwatch_0.Elapsed.TotalSeconds, 4, num2);
							Console.WriteLine(value);
							@class.streamWriter_0.WriteLine(value);
							IL_2A9:
							this.stopwatch_0.Stop();
							this.byte_0 = list.ToArray();
							this.method_5(list);
							@class.streamWriter_0.Flush();
							return;
						}
						this.method_16(@class.byte_0, @class.uint_0, list);
						this.method_17(list.Count, 49152);
						Thread.Sleep(15);
						num2 += 12;
					}
					goto IL_2A9;
				}
			}
			finally
			{
				if (@class.streamWriter_0 != null)
				{
					((IDisposable)@class.streamWriter_0).Dispose();
				}
			}
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00019F54 File Offset: 0x00018154
		private void method_16(byte[] byte_1, uint uint_0, List<byte> list_1)
		{
			if (uint_0 <= 24U)
			{
				return;
			}
			int num = 13;
			int num2 = 24;
			int num3 = num;
			while (num3 <= num2 && (long)num3 < (long)((ulong)uint_0))
			{
				list_1.Add(byte_1[num3]);
				num3++;
			}
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00019F88 File Offset: 0x00018188
		private void method_17(int int_1, int int_2)
		{
			GForm3.Class11 @class = new GForm3.Class11();
			@class.gform3_0 = this;
			@class.int_0 = int_1 * 100 / int_2;
			if (@class.int_0 > 100)
			{
				@class.int_0 = 100;
			}
			base.Invoke(new Action(@class.method_0));
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00019FD4 File Offset: 0x000181D4
		private void method_18(byte[] byte_1)
		{
			byte[] byte_2 = new byte[256];
			uint uint_ = 0U;
			bool flag = this.method_1(byte_1, byte_1.Length, ref byte_2, ref uint_, 0);
			this.method_3(byte_1, false, (uint)byte_1.Length);
			if (flag)
			{
				this.method_3(byte_2, true, uint_);
			}
		}

		// Token: 0x06000054 RID: 84 RVA: 0x0001A014 File Offset: 0x00018214
		private byte method_19(byte[] byte_1, int int_1)
		{
			int num = 0;
			for (int i = 0; i < int_1; i++)
			{
				num += (int)byte_1[i];
			}
			return (byte)(256 - (num & 255) & 255);
		}

		// Token: 0x06000055 RID: 85 RVA: 0x0000C303 File Offset: 0x0000A503
		private void label_0_Click(object sender, EventArgs e)
		{
		}

		// Token: 0x06000056 RID: 86 RVA: 0x0000C303 File Offset: 0x0000A503
		private void label_1_Click(object sender, EventArgs e)
		{
		}

		// Token: 0x06000057 RID: 87 RVA: 0x0000C303 File Offset: 0x0000A503
		private void method_20(object sender, EventArgs e)
		{
		}

		// Token: 0x06000058 RID: 88 RVA: 0x0000C303 File Offset: 0x0000A503
		private void method_21(object sender, EventArgs e)
		{
		}

		// Token: 0x06000059 RID: 89 RVA: 0x0000C303 File Offset: 0x0000A503
		private void method_22(object sender, EventArgs e)
		{
		}

		// Token: 0x0600005A RID: 90 RVA: 0x0000C303 File Offset: 0x0000A503
		private void method_23(object sender, EventArgs e)
		{
		}

		// Token: 0x0600005B RID: 91 RVA: 0x0000C303 File Offset: 0x0000A503
		private void method_24(object sender, EventArgs e)
		{
		}

		// Token: 0x0600005C RID: 92 RVA: 0x0000C303 File Offset: 0x0000A503
		private void GForm3_Load(object sender, EventArgs e)
		{
		}

		// Token: 0x0600005D RID: 93 RVA: 0x0001A04C File Offset: 0x0001824C
		private void method_25(object sender, EventArgs e)
		{
			GForm3.Struct1 @struct;
			@struct.asyncVoidMethodBuilder_0 = AsyncVoidMethodBuilder.Create();
			@struct.int_0 = -1;
			@struct.asyncVoidMethodBuilder_0.Start<GForm3.Struct1>(ref @struct);
		}

		// Token: 0x0600005E RID: 94 RVA: 0x0001A07C File Offset: 0x0001827C
		private Task method_26()
		{
			GForm3.Struct0 @struct;
			@struct.asyncTaskMethodBuilder_0 = AsyncTaskMethodBuilder.Create();
			@struct.int_0 = -1;
			@struct.asyncTaskMethodBuilder_0.Start<GForm3.Struct0>(ref @struct);
			return @struct.asyncTaskMethodBuilder_0.Task;
		}

		// Token: 0x0600005F RID: 95 RVA: 0x0000C303 File Offset: 0x0000A503
		private void method_27(object sender, EventArgs e)
		{
		}

		// Token: 0x06000060 RID: 96 RVA: 0x0000C303 File Offset: 0x0000A503
		private void method_28(object sender, EventArgs e)
		{
		}

		// Token: 0x06000061 RID: 97 RVA: 0x0000C303 File Offset: 0x0000A503
		private void label_3_Click(object sender, EventArgs e)
		{
		}

		// Token: 0x06000063 RID: 99 RVA: 0x0001A0B8 File Offset: 0x000182B8
		private void method_29()
		{
			this.progressBar_0 = new ProgressBar();
			this.comboBox_0 = new ComboBox();
			this.button_0 = new Button();
			this.label_0 = new Label();
			this.label_1 = new Label();
			this.textBox_0 = new TextBox();
			this.label_2 = new Label();
			this.label_3 = new Label();
			base.SuspendLayout();
			this.progressBar_0.ForeColor = Color.Red;
			this.progressBar_0.Location = new Point(16, 94);
			this.progressBar_0.Name = "progressBar1";
			this.progressBar_0.Size = new Size(367, 19);
			this.progressBar_0.TabIndex = 1;
			this.progressBar_0.Click += this.progressBar_0_Click;
			this.comboBox_0.BackColor = Color.White;
			this.comboBox_0.FlatStyle = FlatStyle.Flat;
			this.comboBox_0.Font = new Font("Microsoft Sans Serif", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 222);
			this.comboBox_0.FormattingEnabled = true;
			this.comboBox_0.Location = new Point(16, 16);
			this.comboBox_0.Name = "comboBox1";
			this.comboBox_0.Size = new Size(367, 24);
			this.comboBox_0.TabIndex = 2;
			this.comboBox_0.SelectedIndexChanged += this.comboBox_0_SelectedIndexChanged;
			this.button_0.BackColor = Color.Transparent;
			this.button_0.FlatStyle = FlatStyle.Flat;
			this.button_0.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.button_0.ForeColor = Color.Red;
			this.button_0.Location = new Point(16, 55);
			this.button_0.Name = "button1";
			this.button_0.Size = new Size(367, 33);
			this.button_0.TabIndex = 3;
			this.button_0.Text = "เริ่มการทำงาน";
			this.button_0.UseVisualStyleBackColor = false;
			this.button_0.Click += this.button_0_Click;
			this.label_0.AutoSize = true;
			this.label_0.Font = new Font("Microsoft Sans Serif", 14.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.label_0.ForeColor = Color.Lime;
			this.label_0.Location = new Point(38, 300);
			this.label_0.Name = "PERSEN";
			this.label_0.Size = new Size(17, 24);
			this.label_0.TabIndex = 4;
			this.label_0.Text = "-";
			this.label_0.Click += this.label_0_Click;
			this.label_1.AutoSize = true;
			this.label_1.ForeColor = Color.White;
			this.label_1.Location = new Point(527, 240);
			this.label_1.Name = "label2";
			this.label_1.Size = new Size(10, 13);
			this.label_1.TabIndex = 5;
			this.label_1.Text = "-";
			this.label_1.Click += this.label_1_Click;
			this.textBox_0.Cursor = Cursors.Arrow;
			this.textBox_0.Location = new Point(116, 232);
			this.textBox_0.Multiline = true;
			this.textBox_0.Name = "textBoxLogs";
			this.textBox_0.ReadOnly = true;
			this.textBox_0.ScrollBars = ScrollBars.Vertical;
			this.textBox_0.Size = new Size(367, 178);
			this.textBox_0.TabIndex = 0;
			this.textBox_0.TextChanged += this.textBox_0_TextChanged;
			this.label_2.AutoSize = true;
			this.label_2.Font = new Font("Microsoft Sans Serif", 20.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.label_2.Location = new Point(21, 269);
			this.label_2.Name = "label1";
			this.label_2.Size = new Size(347, 31);
			this.label_2.TabIndex = 6;
			this.label_2.Text = "เลือกหัวข้อที่ต้องการดำเนินการ";
			this.label_3.AutoSize = true;
			this.label_3.FlatStyle = FlatStyle.System;
			this.label_3.Font = new Font("Microsoft Sans Serif", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 222);
			this.label_3.ForeColor = Color.Lime;
			this.label_3.Location = new Point(86, 124);
			this.label_3.Name = "label3";
			this.label_3.Size = new Size(236, 16);
			this.label_3.TabIndex = 7;
			this.label_3.Text = "---------- Facebook : เส’เอ็ม สามย่าน ---------- ";
			this.label_3.Click += this.label_3_Click;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			this.AutoValidate = AutoValidate.Disable;
			this.BackColor = Color.Black;
			this.BackgroundImageLayout = ImageLayout.None;
			base.CausesValidation = false;
			base.ClientSize = new Size(395, 151);
			base.Controls.Add(this.label_3);
			base.Controls.Add(this.label_2);
			base.Controls.Add(this.textBox_0);
			base.Controls.Add(this.label_1);
			base.Controls.Add(this.label_0);
			base.Controls.Add(this.button_0);
			base.Controls.Add(this.comboBox_0);
			base.Controls.Add(this.progressBar_0);
			base.Enabled = false;
			base.FormBorderStyle = FormBorderStyle.SizableToolWindow;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "pass";
			this.RightToLeftLayout = true;
			base.StartPosition = FormStartPosition.CenterScreen;
			base.Load += this.GForm3_Load;
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x06000064 RID: 100 RVA: 0x0000C4EA File Offset: 0x0000A6EA
		[CompilerGenerated]
		private void method_30()
		{
			this.progressBar_0.Maximum = 128;
			this.progressBar_0.Value = 100;
		}

		// Token: 0x06000065 RID: 101 RVA: 0x0000C509 File Offset: 0x0000A709
		[CompilerGenerated]
		private void method_31()
		{
			this.progressBar_0.Maximum = 256;
			this.progressBar_0.Value = 0;
		}

		// Token: 0x06000066 RID: 102 RVA: 0x0000C509 File Offset: 0x0000A709
		[CompilerGenerated]
		private void method_32()
		{
			this.progressBar_0.Maximum = 256;
			this.progressBar_0.Value = 0;
		}

		// Token: 0x06000067 RID: 103 RVA: 0x0000C527 File Offset: 0x0000A727
		[CompilerGenerated]
		private void method_33()
		{
			this.progressBar_0.Maximum = 100;
			this.progressBar_0.Value = 0;
		}

		// Token: 0x06000068 RID: 104 RVA: 0x0000C527 File Offset: 0x0000A727
		[CompilerGenerated]
		private void method_34()
		{
			this.progressBar_0.Maximum = 100;
			this.progressBar_0.Value = 0;
		}

		// Token: 0x0400002C RID: 44
		private static IntPtr intptr_0;

		// Token: 0x0400002D RID: 45
		private System.Windows.Forms.Timer timer_0;

		// Token: 0x0400002E RID: 46
		private bool bool_0;

		// Token: 0x0400002F RID: 47
		private int int_0 = 3;

		// Token: 0x04000030 RID: 48
		private byte[] byte_0;

		// Token: 0x04000031 RID: 49
		private List<byte> list_0 = new List<byte>();

		// Token: 0x04000032 RID: 50
		private Thread thread_0;

		// Token: 0x04000033 RID: 51
		private Stopwatch stopwatch_0 = new Stopwatch();

		// Token: 0x04000034 RID: 52
		private bool bool_1;

		// Token: 0x04000035 RID: 53
		private Stopwatch stopwatch_1 = new Stopwatch();

		// Token: 0x04000037 RID: 55
		private ProgressBar progressBar_0;

		// Token: 0x04000038 RID: 56
		private ComboBox comboBox_0;

		// Token: 0x04000039 RID: 57
		private Button button_0;

		// Token: 0x0400003A RID: 58
		private Label label_0;

		// Token: 0x0400003B RID: 59
		private Label label_1;

		// Token: 0x0400003C RID: 60
		private TextBox textBox_0;

		// Token: 0x0400003D RID: 61
		private TextBox textBox_1;

		// Token: 0x0400003E RID: 62
		private Label label_2;

		// Token: 0x0400003F RID: 63
		private Label label_3;

		// Token: 0x0200000B RID: 11
		[CompilerGenerated]
		[Serializable]
		private sealed class Class0
		{
			// Token: 0x0600006B RID: 107 RVA: 0x0000C54E File Offset: 0x0000A74E
			internal void method_0()
			{
				MessageBox.Show(EC2D41B1-A2F9-4664-90D8-86645EE2E753.smethod_850(), EC2D41B1-A2F9-4664-90D8-86645EE2E753.smethod_629(), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}

			// Token: 0x0600006C RID: 108 RVA: 0x0000C563 File Offset: 0x0000A763
			internal void method_1()
			{
				MessageBox.Show(EC2D41B1-A2F9-4664-90D8-86645EE2E753.smethod_850(), EC2D41B1-A2F9-4664-90D8-86645EE2E753.smethod_851(), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}

			// Token: 0x0600006D RID: 109 RVA: 0x0000C563 File Offset: 0x0000A763
			internal void method_2()
			{
				MessageBox.Show(EC2D41B1-A2F9-4664-90D8-86645EE2E753.smethod_850(), EC2D41B1-A2F9-4664-90D8-86645EE2E753.smethod_851(), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}

			// Token: 0x0600006E RID: 110 RVA: 0x0000C578 File Offset: 0x0000A778
			internal string method_3(byte byte_0)
			{
				return byte_0.ToString(EC2D41B1-A2F9-4664-90D8-86645EE2E753.smethod_142()).ToLower();
			}

			// Token: 0x0600006F RID: 111 RVA: 0x0000C578 File Offset: 0x0000A778
			internal string method_4(byte byte_0)
			{
				return byte_0.ToString(EC2D41B1-A2F9-4664-90D8-86645EE2E753.smethod_142()).ToLower();
			}

			// Token: 0x06000070 RID: 112 RVA: 0x0000C578 File Offset: 0x0000A778
			internal string method_5(byte byte_0)
			{
				return byte_0.ToString(EC2D41B1-A2F9-4664-90D8-86645EE2E753.smethod_142()).ToLower();
			}

			// Token: 0x06000071 RID: 113 RVA: 0x0000C578 File Offset: 0x0000A778
			internal string method_6(byte byte_0)
			{
				return byte_0.ToString(EC2D41B1-A2F9-4664-90D8-86645EE2E753.smethod_142()).ToLower();
			}

			// Token: 0x04000040 RID: 64
			public static readonly GForm3.Class0 class0_0 = new GForm3.Class0();

			// Token: 0x04000041 RID: 65
			public static Action action_0;

			// Token: 0x04000042 RID: 66
			public static Action action_1;

			// Token: 0x04000043 RID: 67
			public static Action action_2;

			// Token: 0x04000044 RID: 68
			public static Func<byte, string> func_0;

			// Token: 0x04000045 RID: 69
			public static Func<byte, string> func_1;

			// Token: 0x04000046 RID: 70
			public static Func<byte, string> func_2;

			// Token: 0x04000047 RID: 71
			public static Func<byte, string> func_3;
		}

		// Token: 0x0200000C RID: 12
		[CompilerGenerated]
		private sealed class Class1
		{
			// Token: 0x06000073 RID: 115 RVA: 0x0001A730 File Offset: 0x00018930
			internal void method_0()
			{
				this.gform3_0.textBox_0.AppendText(this.string_0 + Environment.NewLine);
				this.gform3_0.textBox_0.SelectionStart = this.gform3_0.textBox_0.Text.Length;
				this.gform3_0.textBox_0.ScrollToCaret();
			}

			// Token: 0x04000048 RID: 72
			public GForm3 gform3_0;

			// Token: 0x04000049 RID: 73
			public string string_0;
		}

		// Token: 0x0200000D RID: 13
		[CompilerGenerated]
		private sealed class Class2
		{
			// Token: 0x06000075 RID: 117 RVA: 0x0000C58B File Offset: 0x0000A78B
			internal string method_0(int int_0)
			{
				return this.byte_0[int_0].ToString(EC2D41B1-A2F9-4664-90D8-86645EE2E753.smethod_852());
			}

			// Token: 0x0400004A RID: 74
			public byte[] byte_0;
		}

		// Token: 0x0200000E RID: 14
		[CompilerGenerated]
		private sealed class Class3
		{
			// Token: 0x06000077 RID: 119 RVA: 0x0001A794 File Offset: 0x00018994
			internal void method_0()
			{
				using (SaveFileDialog saveFileDialog = new SaveFileDialog())
				{
					saveFileDialog.Title = EC2D41B1-A2F9-4664-90D8-86645EE2E753.smethod_853();
					saveFileDialog.InitialDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), EC2D41B1-A2F9-4664-90D8-86645EE2E753.smethod_854());
					FileDialog fileDialog = saveFileDialog;
					string format = EC2D41B1-A2F9-4664-90D8-86645EE2E753.smethod_855();
					object selectedItem = this.gform3_0.comboBox_0.SelectedItem;
					string arg;
					if (selectedItem != null)
					{
						if ((arg = selectedItem.ToString()) != null)
						{
							goto IL_52;
						}
					}
					arg = EC2D41B1-A2F9-4664-90D8-86645EE2E753.smethod_856();
					IL_52:
					fileDialog.FileName = string.Format(format, arg, DateTime.Now);
					saveFileDialog.Filter = EC2D41B1-A2F9-4664-90D8-86645EE2E753.smethod_857();
					saveFileDialog.DefaultExt = EC2D41B1-A2F9-4664-90D8-86645EE2E753.smethod_858();
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
							File.WriteAllBytes(saveFileDialog.FileName, this.list_0.ToArray());
							Console.WriteLine(EC2D41B1-A2F9-4664-90D8-86645EE2E753.smethod_859() + saveFileDialog.FileName);
							this.gform3_0.progressBar_0.Value = 0;
							return;
						}
						catch (Exception ex)
						{
							Console.WriteLine(EC2D41B1-A2F9-4664-90D8-86645EE2E753.smethod_860() + ex.Message);
							return;
						}
					}
					Console.WriteLine(EC2D41B1-A2F9-4664-90D8-86645EE2E753.smethod_861());
				}
			}

			// Token: 0x0400004B RID: 75
			public GForm3 gform3_0;

			// Token: 0x0400004C RID: 76
			public List<byte> list_0;
		}

		// Token: 0x0200000F RID: 15
		[CompilerGenerated]
		private sealed class Class4
		{
			// Token: 0x06000079 RID: 121 RVA: 0x0000C5A3 File Offset: 0x0000A7A3
			internal void method_0()
			{
				this.gform3_0.progressBar_0.Maximum = 256;
				this.gform3_0.progressBar_0.Value = 0;
			}

			// Token: 0x0400004D RID: 77
			public GForm3 gform3_0;

			// Token: 0x0400004E RID: 78
			public Stopwatch stopwatch_0;
		}

		// Token: 0x02000010 RID: 16
		[CompilerGenerated]
		private sealed class Class5
		{
			// Token: 0x0600007B RID: 123 RVA: 0x0001A8E8 File Offset: 0x00018AE8
			internal void method_0()
			{
				this.class4_0.gform3_0.progressBar_0.Value = this.int_0 + 1;
				int num = (this.int_0 + 1) * 100 / 256;
				this.class4_0.gform3_0.label_0.Text = string.Format(EC2D41B1-A2F9-4664-90D8-86645EE2E753.smethod_862(), num);
				TimeSpan elapsed = this.class4_0.stopwatch_0.Elapsed;
				this.class4_0.gform3_0.label_1.Text = string.Format(EC2D41B1-A2F9-4664-90D8-86645EE2E753.smethod_863(), elapsed.Seconds);
			}

			// Token: 0x0400004F RID: 79
			public int int_0;

			// Token: 0x04000050 RID: 80
			public GForm3.Class4 class4_0;
		}

		// Token: 0x02000011 RID: 17
		[CompilerGenerated]
		private sealed class Class6
		{
			// Token: 0x0600007D RID: 125 RVA: 0x0000C5CB File Offset: 0x0000A7CB
			internal void method_0()
			{
				this.gform3_0.progressBar_0.Maximum = 256;
				this.gform3_0.progressBar_0.Value = 0;
			}

			// Token: 0x04000051 RID: 81
			public GForm3 gform3_0;

			// Token: 0x04000052 RID: 82
			public Stopwatch stopwatch_0;
		}

		// Token: 0x02000012 RID: 18
		[CompilerGenerated]
		private sealed class Class7
		{
			// Token: 0x0600007F RID: 127 RVA: 0x0001A988 File Offset: 0x00018B88
			internal void method_0()
			{
				this.class6_0.gform3_0.progressBar_0.Value = this.int_0 + 1;
				int num = (this.int_0 + 1) * 100 / 256;
				this.class6_0.gform3_0.label_0.Text = string.Format(EC2D41B1-A2F9-4664-90D8-86645EE2E753.smethod_862(), num);
				this.class6_0.gform3_0.label_1.Text = string.Format(EC2D41B1-A2F9-4664-90D8-86645EE2E753.smethod_864(), this.class6_0.stopwatch_0.Elapsed.TotalSeconds);
			}

			// Token: 0x04000053 RID: 83
			public int int_0;

			// Token: 0x04000054 RID: 84
			public GForm3.Class6 class6_0;
		}

		// Token: 0x02000013 RID: 19
		[CompilerGenerated]
		private sealed class Class8
		{
			// Token: 0x06000081 RID: 129 RVA: 0x0001AA28 File Offset: 0x00018C28
			internal void method_0()
			{
				this.gform3_0.progressBar_0.Value = this.int_0 + 1;
				int num = (this.int_0 + 1) * 100 / 256;
				this.gform3_0.label_0.Text = string.Format(EC2D41B1-A2F9-4664-90D8-86645EE2E753.smethod_862(), num);
				this.gform3_0.label_1.Text = string.Format(EC2D41B1-A2F9-4664-90D8-86645EE2E753.smethod_864(), this.gform3_0.stopwatch_0.Elapsed.TotalSeconds);
			}

			// Token: 0x04000055 RID: 85
			public int int_0;

			// Token: 0x04000056 RID: 86
			public GForm3 gform3_0;
		}

		// Token: 0x02000014 RID: 20
		[CompilerGenerated]
		private sealed class Class9
		{
			// Token: 0x06000083 RID: 131 RVA: 0x0001AAB8 File Offset: 0x00018CB8
			internal void method_0(byte[] byte_1, bool bool_0)
			{
				this.gform3_0.method_1(byte_1, byte_1.Length, ref this.byte_0, ref this.uint_0, 0);
				string arg = string.Join(EC2D41B1-A2F9-4664-90D8-86645EE2E753.smethod_125(), byte_1.Select(new Func<byte, string>(GForm3.Class0.class0_0.method_3)));
				string value = string.Format(EC2D41B1-A2F9-4664-90D8-86645EE2E753.smethod_865(), this.gform3_0.stopwatch_0.Elapsed.TotalSeconds, arg);
				Console.WriteLine(value);
				this.streamWriter_0.WriteLine(value);
				if (this.uint_0 > 0U)
				{
					string arg2 = string.Join(EC2D41B1-A2F9-4664-90D8-86645EE2E753.smethod_125(), this.byte_0.Take((int)this.uint_0).Select(new Func<byte, string>(GForm3.Class0.class0_0.method_4)));
					string value2 = string.Format(EC2D41B1-A2F9-4664-90D8-86645EE2E753.smethod_866(), this.gform3_0.stopwatch_0.Elapsed.TotalSeconds, arg2);
					Console.WriteLine(value2);
					this.streamWriter_0.WriteLine(value2);
				}
				if (bool_0)
				{
					Thread.Sleep(150);
				}
			}

			// Token: 0x04000057 RID: 87
			public StreamWriter streamWriter_0;

			// Token: 0x04000058 RID: 88
			public byte[] byte_0;

			// Token: 0x04000059 RID: 89
			public uint uint_0;

			// Token: 0x0400005A RID: 90
			public GForm3 gform3_0;
		}

		// Token: 0x02000015 RID: 21
		[CompilerGenerated]
		private sealed class Class10
		{
			// Token: 0x06000085 RID: 133 RVA: 0x0001ABE4 File Offset: 0x00018DE4
			internal void method_0(byte[] byte_1, bool bool_0)
			{
				this.gform3_0.method_1(byte_1, byte_1.Length, ref this.byte_0, ref this.uint_0, 0);
				string arg = string.Join(EC2D41B1-A2F9-4664-90D8-86645EE2E753.smethod_125(), byte_1.Select(new Func<byte, string>(GForm3.Class0.class0_0.method_5)));
				string value = string.Format(EC2D41B1-A2F9-4664-90D8-86645EE2E753.smethod_865(), this.gform3_0.stopwatch_0.Elapsed.TotalSeconds, arg);
				Console.WriteLine(value);
				this.streamWriter_0.WriteLine(value);
				if (this.uint_0 > 0U)
				{
					string arg2 = string.Join(EC2D41B1-A2F9-4664-90D8-86645EE2E753.smethod_125(), this.byte_0.Take((int)this.uint_0).Select(new Func<byte, string>(GForm3.Class0.class0_0.method_6)));
					string value2 = string.Format(EC2D41B1-A2F9-4664-90D8-86645EE2E753.smethod_866(), this.gform3_0.stopwatch_0.Elapsed.TotalSeconds, arg2);
					Console.WriteLine(value2);
					this.streamWriter_0.WriteLine(value2);
				}
				if (bool_0)
				{
					Thread.Sleep(150);
				}
			}

			// Token: 0x0400005B RID: 91
			public StreamWriter streamWriter_0;

			// Token: 0x0400005C RID: 92
			public byte[] byte_0;

			// Token: 0x0400005D RID: 93
			public uint uint_0;

			// Token: 0x0400005E RID: 94
			public GForm3 gform3_0;
		}

		// Token: 0x02000016 RID: 22
		[CompilerGenerated]
		private sealed class Class11
		{
			// Token: 0x06000087 RID: 135 RVA: 0x0001AD10 File Offset: 0x00018F10
			internal void method_0()
			{
				this.gform3_0.progressBar_0.Value = this.int_0;
				this.gform3_0.label_1.Text = string.Format(EC2D41B1-A2F9-4664-90D8-86645EE2E753.smethod_867(), this.gform3_0.stopwatch_0.Elapsed.Seconds);
				this.gform3_0.label_0.Text = string.Format(EC2D41B1-A2F9-4664-90D8-86645EE2E753.smethod_862(), this.int_0);
			}

			// Token: 0x0400005F RID: 95
			public GForm3 gform3_0;

			// Token: 0x04000060 RID: 96
			public int int_0;
		}
	}
}
