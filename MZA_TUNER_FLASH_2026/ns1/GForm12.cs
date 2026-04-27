using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Globalization;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using <PrivateImplementationDetails>{68F2EF73-9355-4257-ADA6-397CF8BB8E72};
using FTD2XX_NET;
using ns0;
using ns2;

namespace ns1
{
	// Token: 0x020000B5 RID: 181
	public partial class GForm12 : Form
	{
		// Token: 0x06000234 RID: 564
		[DllImport("Gdi32.dll")]
		private static extern IntPtr CreateRoundRectRgn(int int_8, int int_9, int int_10, int int_11, int int_12, int int_13);

		// Token: 0x06000235 RID: 565 RVA: 0x0000D05C File Offset: 0x0000B25C
		private string method_0()
		{
			return "ANTI";
		}

		// Token: 0x06000236 RID: 566 RVA: 0x0000D063 File Offset: 0x0000B263
		private int method_1(int int_8)
		{
			return int_8 << 2 ^ 90;
		}

		// Token: 0x06000237 RID: 567 RVA: 0x0000D06B File Offset: 0x0000B26B
		private bool method_2()
		{
			return new Random().Next(0, 100) > -1;
		}

		// Token: 0x06000238 RID: 568
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		private static extern IntPtr SendMessage(IntPtr intptr_2, uint uint_1, IntPtr intptr_3, IntPtr intptr_4);

		// Token: 0x06000239 RID: 569
		[DllImport("user32.dll")]
		public static extern bool ReleaseCapture();

		// Token: 0x0600023A RID: 570
		[DllImport("user32.dll", EntryPoint = "SendMessage")]
		public static extern IntPtr SendMessage_1(IntPtr intptr_2, int int_8, int int_9, int int_10);

		// Token: 0x0600023B RID: 571
		[DllImport("user32.dll")]
		internal static extern int SetWindowCompositionAttribute(IntPtr intptr_2, ref GForm12.Struct12 struct12_0);

		// Token: 0x0600023C RID: 572 RVA: 0x0000D07D File Offset: 0x0000B27D
		[CompilerGenerated]
		public object method_3()
		{
			return this.object_4;
		}

		// Token: 0x0600023D RID: 573 RVA: 0x0000D085 File Offset: 0x0000B285
		[CompilerGenerated]
		private void method_4(object object_6)
		{
			this.object_4 = object_6;
		}

		// Token: 0x0600023E RID: 574 RVA: 0x0000D08E File Offset: 0x0000B28E
		[CompilerGenerated]
		public object method_5()
		{
			return this.object_5;
		}

		// Token: 0x0600023F RID: 575 RVA: 0x0000D096 File Offset: 0x0000B296
		[CompilerGenerated]
		private void method_6(object object_6)
		{
			this.object_5 = object_6;
		}

		// Token: 0x06000240 RID: 576 RVA: 0x00027944 File Offset: 0x00025B44
		public GForm12()
		{
			this.method_44();
			this.method_103();
			this.method_16();
			try
			{
				GClass17.smethod_4();
			}
			catch
			{
			}
			GForm12.bool_8 = true;
			this.method_83(this);
			this.method_91();
			if (this.toolStripMenuItem_4 != null)
			{
				this.toolStripMenuItem_4.Click -= this.toolStripMenuItem_4_Click;
				this.toolStripMenuItem_4.Click += this.toolStripMenuItem_4_Click;
			}
			base.Shown += this.GForm12_Shown;
			this.method_14(null);
		}

		// Token: 0x06000241 RID: 577 RVA: 0x00027A64 File Offset: 0x00025C64
		private void method_7()
		{
			if (LicenseManager.UsageMode != LicenseUsageMode.Designtime && !Process.GetCurrentProcess().ProcessName.ToLower().Contains("devenv"))
			{
				this.DoubleBuffered = true;
				this.color_0 = ((this.panel_0 != null) ? this.panel_0.BackColor : Color.Red);
				for (int i = 0; i < 60; i++)
				{
					this.list_0.Add(this.method_8(true));
				}
				this.bool_1 = true;
				Task.Run(new Func<Task>(this.method_105));
				if (this.pictureBox_4 != null)
				{
					this.pictureBox_4.Paint += this.pictureBox_4_Paint_1;
				}
				return;
			}
		}

		// Token: 0x06000242 RID: 578 RVA: 0x0000D09F File Offset: 0x0000B29F
		protected override void OnFormClosing(FormClosingEventArgs e)
		{
			GClass19.smethod_0("Goodbye Master. Shutting down.");
			this.bool_1 = false;
			base.OnFormClosing(e);
		}

		// Token: 0x06000243 RID: 579 RVA: 0x00027B18 File Offset: 0x00025D18
		private GForm12.GClass6 method_8(bool bool_15 = false)
		{
			return new GForm12.GClass6
			{
				float_0 = (float)this.random_0.Next(0, base.Width),
				float_1 = (float)(bool_15 ? this.random_0.Next(0, base.Height) : -10),
				float_2 = (float)(this.random_0.NextDouble() * 1.5 + 0.5),
				float_3 = (float)(this.random_0.NextDouble() * 3.0 + 1.5),
				float_4 = (float)(this.random_0.NextDouble() * 1.5 - 0.75),
				int_0 = this.random_0.Next(150, 255)
			};
		}

		// Token: 0x06000244 RID: 580 RVA: 0x00027BF0 File Offset: 0x00025DF0
		private void method_9()
		{
			object obj = this.object_1;
			lock (obj)
			{
				for (int i = 0; i < this.list_0.Count; i++)
				{
					GForm12.GClass6 gclass = this.list_0[i];
					gclass.float_1 += gclass.float_2;
					gclass.float_0 += gclass.float_4;
					if (gclass.float_1 > (float)base.Height)
					{
						this.list_0[i] = this.method_8(false);
					}
				}
			}
		}

		// Token: 0x06000245 RID: 581 RVA: 0x00027C98 File Offset: 0x00025E98
		private void method_10()
		{
			GForm12.Class143 @class = new GForm12.Class143();
			@class.gform12_0 = this;
			this.bool_2 = true;
			this.float_0 = 230f;
			@class.string_0 = new string[]
			{
				"> INITIALIZING MZA-TUNER 2026...",
				"> SEARCHING FTDI INTERFACE...",
				"> FTDI D2XX DRIVER DETECTED [OK]",
				"> AUTHENTICATING LICENSE KEY...",
				"> MEMORY OFFSET MAPPING: 0x8000-0xFFFF",
				"> K-LINE PROTOCOL STACK LOADED",
				"> WARMING UP ISO-9141 BUS... [OK]",
				"> SYNCING ECU DATABASE...",
				"> SYSTEM STABLE. ACCESS GRANTED."
			};
			Task.Run(new Func<Task>(@class.method_0));
		}

		// Token: 0x06000246 RID: 582 RVA: 0x0000D0B9 File Offset: 0x0000B2B9
		protected override void OnPaint(PaintEventArgs pevent)
		{
			base.OnPaint(pevent);
			this.method_13(pevent.Graphics, this);
			this.method_11(pevent.Graphics);
		}

		// Token: 0x06000247 RID: 583 RVA: 0x00027D2C File Offset: 0x00025F2C
		private void method_11(Graphics graphics_0)
		{
			if (!this.bool_2)
			{
				return;
			}
			graphics_0.SmoothingMode = SmoothingMode.AntiAlias;
			Rectangle rect = new Rectangle(0, 0, base.Width, base.Height);
			using (SolidBrush solidBrush = new SolidBrush(Color.FromArgb((int)this.float_0, 5, 5, 5)))
			{
				graphics_0.FillRectangle(solidBrush, rect);
			}
			using (Pen pen = new Pen(Color.FromArgb(20, 0, 255, 0), 1f))
			{
				for (int i = 0; i < base.Height; i += 3)
				{
					graphics_0.DrawLine(pen, 0, i, base.Width, i);
				}
			}
			Rectangle rectangle = new Rectangle((base.Width - 500) / 2, (base.Height - 300) / 2, 500, 300);
			using (GraphicsPath graphicsPath = this.method_12(rectangle, 10))
			{
				using (PathGradientBrush pathGradientBrush = new PathGradientBrush(graphicsPath))
				{
					pathGradientBrush.CenterColor = Color.FromArgb(40, this.color_0);
					pathGradientBrush.SurroundColors = new Color[]
					{
						Color.Transparent
					};
					graphics_0.FillPath(pathGradientBrush, graphicsPath);
				}
			}
			Color color = Color.FromArgb((int)this.float_0, this.color_0);
			using (Font font = new Font("Consolas", 10f, FontStyle.Bold))
			{
				float num = (float)(rectangle.Y + 20);
				object obj = this.object_2;
				lock (obj)
				{
					foreach (string s in this.list_1)
					{
						graphics_0.DrawString(s, font, new SolidBrush(Color.FromArgb((int)((double)this.float_0 * 0.7), color)), (float)(rectangle.X + 20), num);
						num += 20f;
					}
					if (!string.IsNullOrEmpty(this.string_2))
					{
						graphics_0.DrawString(this.string_2, font, new SolidBrush(color), (float)(rectangle.X + 20), num);
					}
				}
			}
			using (Pen pen2 = new Pen(Color.FromArgb((int)this.float_0, this.color_0), 1f))
			{
				graphics_0.DrawRectangle(pen2, rectangle);
				int num2 = 15;
				using (Pen pen3 = new Pen(color, 3f))
				{
					graphics_0.DrawLine(pen3, rectangle.X, rectangle.Y, rectangle.X + num2, rectangle.Y);
					graphics_0.DrawLine(pen3, rectangle.X, rectangle.Y, rectangle.X, rectangle.Y + num2);
					graphics_0.DrawLine(pen3, rectangle.Right - num2, rectangle.Bottom, rectangle.Right, rectangle.Bottom);
					graphics_0.DrawLine(pen3, rectangle.Right, rectangle.Bottom - num2, rectangle.Right, rectangle.Bottom);
				}
			}
		}

		// Token: 0x06000248 RID: 584 RVA: 0x000280D0 File Offset: 0x000262D0
		private GraphicsPath method_12(Rectangle rectangle_0, int int_8)
		{
			GraphicsPath graphicsPath = new GraphicsPath();
			float num = (float)(int_8 * 2);
			graphicsPath.AddArc((float)rectangle_0.X, (float)rectangle_0.Y, num, num, 180f, 90f);
			graphicsPath.AddArc((float)rectangle_0.Right - num, (float)rectangle_0.Y, num, num, 270f, 90f);
			graphicsPath.AddArc((float)rectangle_0.Right - num, (float)rectangle_0.Bottom - num, num, num, 0f, 90f);
			graphicsPath.AddArc((float)rectangle_0.X, (float)rectangle_0.Bottom - num, num, num, 90f, 90f);
			graphicsPath.CloseFigure();
			return graphicsPath;
		}

		// Token: 0x06000249 RID: 585 RVA: 0x00028180 File Offset: 0x00026380
		private void method_13(Graphics graphics_0, Control control_0)
		{
			try
			{
				if (control_0 != null && graphics_0 != null && this.bool_1)
				{
					if (control_0.IsHandleCreated && !control_0.IsDisposed)
					{
						graphics_0.SmoothingMode = SmoothingMode.AntiAlias;
						object obj = this.object_1;
						GForm12.GClass6[] array;
						lock (obj)
						{
							array = this.list_0.ToArray();
						}
						foreach (GForm12.GClass6 gclass in array)
						{
							Point p = new Point((int)gclass.float_0, (int)gclass.float_1);
							Point p2 = base.PointToScreen(p);
							Point point = control_0.PointToClient(p2);
							float num = (float)point.X;
							float num2 = (float)point.Y;
							if (num >= -10f && num <= (float)(control_0.Width + 10) && num2 >= -10f && num2 <= (float)(control_0.Height + 10))
							{
								using (SolidBrush solidBrush = new SolidBrush(Color.FromArgb(gclass.int_0 / 5, this.color_0)))
								{
									graphics_0.FillEllipse(solidBrush, num - gclass.float_3, num2 - gclass.float_3, gclass.float_3 * 3f, gclass.float_3 * 3f);
								}
								using (SolidBrush solidBrush2 = new SolidBrush(Color.FromArgb(gclass.int_0, this.color_0)))
								{
									graphics_0.FillEllipse(solidBrush2, num, num2, gclass.float_3, gclass.float_3);
								}
							}
						}
					}
				}
			}
			catch
			{
			}
		}

		// Token: 0x0600024A RID: 586 RVA: 0x00028390 File Offset: 0x00026590
		private void method_14(Color? nullable_0 = null)
		{
			try
			{
				Color backColor = Color.FromArgb(25, 25, 25);
				Color white = Color.White;
				Color color = nullable_0 ?? ((this.panel_0 != null) ? this.panel_0.BackColor : Color.FromArgb(200, 20, 20));
				this.color_0 = color;
				Font font = new Font("Segoe UI", 10f, FontStyle.Bold);
				foreach (Control control in new Control[]
				{
					this.textBox_2,
					this.textBox_1,
					this.textBox_0
				})
				{
					if (control != null)
					{
						control.BackColor = backColor;
						control.ForeColor = white;
						TextBox textBox = control as TextBox;
						if (textBox != null)
						{
							textBox.BorderStyle = BorderStyle.None;
						}
						control.Font = font;
						if (control.Parent != null)
						{
							string text = control.Name + "_Line";
							Panel panel = control.Parent.Controls.Find(text, false).FirstOrDefault<Control>() as Panel;
							if (panel != null)
							{
								panel.BackColor = color;
								panel.Width = control.Width;
								panel.Location = new Point(control.Location.X, control.Location.Y + control.Height - 1);
							}
							else
							{
								Panel panel2 = new Panel();
								panel2.BackColor = color;
								panel2.Height = 2;
								panel2.Width = control.Width;
								panel2.Location = new Point(control.Location.X, control.Location.Y + control.Height - 1);
								panel2.Name = text;
								control.Parent.Controls.Add(panel2);
								panel2.BringToFront();
							}
						}
					}
				}
				if (this.gclass15_1 != null)
				{
					this.gclass15_1.method_1(color);
				}
			}
			catch
			{
			}
		}

		// Token: 0x0600024B RID: 587 RVA: 0x000285BC File Offset: 0x000267BC
		private void method_15()
		{
			try
			{
				GForm12.Struct13 structure = new GForm12.Struct13
				{
					enum2_0 = GForm12.Enum2.const_4,
					int_1 = -1123415542
				};
				int cb = Marshal.SizeOf<GForm12.Struct13>(structure);
				IntPtr intPtr = Marshal.AllocHGlobal(cb);
				Marshal.StructureToPtr<GForm12.Struct13>(structure, intPtr, false);
				GForm12.Struct12 @struct = default(GForm12.Struct12);
				@struct.enum1_0 = GForm12.Enum1.const_0;
				@struct.int_0 = cb;
				@struct.intptr_0 = intPtr;
				GForm12.SetWindowCompositionAttribute(base.Handle, ref @struct);
				Marshal.FreeHGlobal(intPtr);
				this.BackColor = Color.FromArgb(10, 10, 10);
				base.Opacity = 1.0;
			}
			catch
			{
				base.Opacity = 0.9;
			}
		}

		// Token: 0x0600024C RID: 588 RVA: 0x00028674 File Offset: 0x00026874
		private void method_16()
		{
			try
			{
				Control control = base.Controls.Find("label_batrei", true).FirstOrDefault<Control>() ?? base.Controls.Find("TxtBatteryVolt", true).FirstOrDefault<Control>();
				if (control != null)
				{
					GControl2 gcontrol = new GControl2();
					gcontrol.Name = "modernBatteryGauge";
					gcontrol.Size = new Size(180, 80);
					gcontrol.Location = new Point(control.Location.X - 10, control.Location.Y - 15);
					gcontrol.Parent = control.Parent;
					control.Visible = false;
					gcontrol.BringToFront();
				}
			}
			catch
			{
			}
		}

		// Token: 0x0600024D RID: 589 RVA: 0x00028734 File Offset: 0x00026934
		private byte method_17(byte[] byte_15, int int_8, int int_9 = 0)
		{
			int num = int_9 + int_8;
			byte b = 0;
			while (int_9 < num)
			{
				b += byte_15[int_9];
				int_9++;
			}
			return b;
		}

		// Token: 0x0600024E RID: 590 RVA: 0x0002875C File Offset: 0x0002695C
		private uint method_18(byte[] byte_15, int int_8, int int_9 = 0)
		{
			int num = int_9 + int_8;
			uint num2 = 0U;
			while (int_9 + 4 <= num)
			{
				num2 += BitConverter.ToUInt32(byte_15, int_9);
				int_9 += 4;
			}
			return num2;
		}

		// Token: 0x0600024F RID: 591 RVA: 0x0002878C File Offset: 0x0002698C
		private byte method_19(byte[] byte_15, int int_8, int int_9 = 0)
		{
			int num = int_9 + int_8;
			int num2 = 0;
			while (int_9 < num)
			{
				num2 += (int)byte_15[int_9];
				int_9++;
			}
			return (byte)(255 - (num2 - 1 >> 8));
		}

		// Token: 0x06000250 RID: 592 RVA: 0x000287C0 File Offset: 0x000269C0
		private byte method_20(byte[] byte_15, int int_8, int int_9 = 0)
		{
			int num = int_9 + int_8;
			int num2 = 0;
			while (int_9 < num)
			{
				num2 += (int)byte_15[int_9];
				int_9++;
			}
			return (byte)((num2 ^ 255) + 1 & 255);
		}

		// Token: 0x06000251 RID: 593 RVA: 0x0000D0DB File Offset: 0x0000B2DB
		private bool method_21(byte[] byte_15, int int_8)
		{
			return this.method_20(byte_15, int_8 - 1, 0) == byte_15[int_8 - 1];
		}

		// Token: 0x06000252 RID: 594 RVA: 0x000287F8 File Offset: 0x000269F8
		private bool method_22(byte[] byte_15, int int_8, ref byte[] byte_16, ref uint uint_1, int int_9 = 0)
		{
			byte[] array = new byte[256];
			byte[] array2 = new byte[256];
			uint num = 0U;
			uint num2 = 0U;
			uint num3 = 0U;
			uint num4 = 0U;
			uint num5 = 0U;
			long num6 = (long)(50 + 2 * int_8);
			bool result;
			if (FTDI.FT_Write(GForm12.intptr_0, byte_15, (uint)int_8, ref num) > FTDI.FT_STATUS.FT_OK)
			{
				result = false;
			}
			else if (FTDI.FT_SetLatencyTimer(GForm12.intptr_0, 8) > FTDI.FT_STATUS.FT_OK)
			{
				result = false;
			}
			else
			{
				if (int_9 > 0)
				{
					Thread.Sleep(int_9);
				}
				Stopwatch stopwatch = new Stopwatch();
				stopwatch.Start();
				do
				{
					if (FTDI.FT_GetQueueStatus(GForm12.intptr_0, ref num2) == FTDI.FT_STATUS.FT_OK && num2 != 0U && (ulong)num2 < (ulong)((long)array.Length) && FTDI.FT_Read(GForm12.intptr_0, array, num2, ref num3) == FTDI.FT_STATUS.FT_OK && num3 != 0U)
					{
						Array.Copy(array, 0L, array2, (long)((ulong)num4), (long)((ulong)num3));
						num4 += num3;
						if ((ulong)num4 >= (ulong)((long)(int_8 + 2)))
						{
							if (num5 == 0U)
							{
								num5 = (uint)array2[int_8 + 1];
							}
							if ((ulong)num4 - (ulong)((long)int_8) == (ulong)num5)
							{
								uint_1 = num5;
								Array.Copy(array2, (long)((ulong)(num4 - num5)), byte_16, 0L, (long)((ulong)num5));
								if (this.method_21(byte_16, (int)num5))
								{
									goto IL_12E;
								}
							}
						}
					}
				}
				while (stopwatch.ElapsedMilliseconds < num6);
				stopwatch.Stop();
				FTDI.FT_Purge(GForm12.intptr_0, 3U);
				return false;
				IL_12E:
				stopwatch.Stop();
				FTDI.FT_Purge(GForm12.intptr_0, 3U);
				result = true;
			}
			return result;
		}

		// Token: 0x06000253 RID: 595 RVA: 0x00028960 File Offset: 0x00026B60
		private void method_23(int int_8, GForm12.Enum3 enum3_0, bool bool_15 = false)
		{
			byte[] array = new byte[]
			{
				byte.MaxValue,
				37,
				0,
				32,
				124,
				0
			};
			byte[] array2 = new byte[]
			{
				126,
				6,
				1,
				1,
				0,
				122
			};
			byte[] array3 = new byte[]
			{
				126,
				5,
				1,
				7,
				117
			};
			byte[] array4 = new byte[]
			{
				126,
				5,
				1,
				8,
				116
			};
			byte[] array5 = new byte[]
			{
				126,
				5,
				1,
				9,
				115
			};
			byte[] array6 = new byte[]
			{
				126,
				5,
				1,
				10,
				114
			};
			byte[] array7 = new byte[]
			{
				126,
				5,
				1,
				12,
				112
			};
			byte[] array8 = new byte[]
			{
				126,
				5,
				1,
				13,
				111
			};
			byte[] array9 = new byte[]
			{
				124,
				5,
				2,
				8,
				117
			};
			byte[] array10 = new byte[]
			{
				124,
				5,
				2,
				9,
				116
			};
			byte[] array11 = new byte[]
			{
				124,
				5,
				2,
				10,
				115
			};
			byte[] array12 = new byte[]
			{
				124,
				5,
				2,
				12,
				113
			};
			byte[] array13 = new byte[]
			{
				124,
				5,
				2,
				13,
				112
			};
			byte[] array14 = new byte[256];
			byte[] array15 = new byte[256];
			byte[] array16 = new byte[128];
			uint num = 0U;
			int int_9 = 0;
			int num2 = 0;
			int num3 = 128;
			int num4 = 8;
			int num5 = int_8 / 16;
			int num6 = 0;
			int i = 0;
			int num7 = GForm12.int_2 / 128;
			Stopwatch stopwatch = new Stopwatch();
			bool flag = true;
			array15[0] = 126;
			array15[1] = 139;
			array15[2] = 1;
			array15[3] = 6;
			for (int j = 0; j < array16.Length; j++)
			{
				array16[j] = byte.MaxValue;
			}
			if (enum3_0 == GForm12.Enum3.const_0 && bool_15)
			{
				if (this.method_22(array, array.Length, ref array14, ref num, 0) && array14[1] == 5 && array14[3] == 0)
				{
					FTDI.FT_SetBaudRate(GForm12.intptr_0, 921600U);
				}
			}
			else
			{
				switch (enum3_0)
				{
				case GForm12.Enum3.const_1:
					num3 = 64;
					num4 = 4;
					num7 = GForm12.int_2 / 64;
					array15[0] = 126;
					array15[1] = 75;
					array15[2] = 1;
					array15[3] = 6;
					flag = false;
					break;
				case GForm12.Enum3.const_2:
					flag = false;
					break;
				case GForm12.Enum3.const_3:
					array15[0] = 124;
					array15[1] = 139;
					array15[2] = 2;
					array15[3] = 6;
					i = int_8 / num3;
					if (!bool_15)
					{
						int_9 = 150;
					}
					break;
				case GForm12.Enum3.const_4:
					array15[0] = 124;
					array15[1] = 139;
					array15[2] = 2;
					array15[3] = 6;
					i = int_8 / num3;
					flag = false;
					if (!bool_15)
					{
						int_9 = 150;
					}
					break;
				case GForm12.Enum3.const_5:
					array15[0] = 124;
					array15[1] = 139;
					array15[2] = 2;
					array15[3] = 6;
					i = int_8 / num3;
					flag = false;
					if (!bool_15)
					{
						int_9 = 150;
					}
					break;
				}
			}
			stopwatch.Start();
			while (i < num7)
			{
				array15[4] = (byte)(num5 >> 8 & 255);
				array15[5] = (byte)(num5 & 255);
				Array.Copy(GForm12.byte_12, i * num3, array15, 6, num3);
				if (flag)
				{
					for (int k = 1; k < num7 - i; k++)
					{
						if (!this.method_34(GForm12.byte_12, (i + k) * num3, array16, num3))
						{
							num6 = num5 + k * num4;
							i += k - 1;
							break;
						}
						if (i + k + 1 == num7)
						{
							i += k;
						}
					}
				}
				else
				{
					num6 = num5 + num4;
				}
				if (i + 1 == num7)
				{
					num6 = 0;
				}
				array15[6 + num3] = (byte)(num6 >> 8 & 255);
				array15[6 + num3 + 1] = (byte)(num6 & 255);
				array15[6 + num3 + 2] = this.method_19(array15, num3 + 4, 4);
				array15[6 + num3 + 3] = this.method_20(array15, num3 + 4, 4);
				array15[6 + num3 + 4] = this.method_20(array15, (int)(array15[1] - 1), 0);
				while (this.method_22(array15, (int)array15[1], ref array14, ref num, int_9))
				{
					double num8 = (double)(i + 1) * (double)num3;
					double num9 = num8 / (double)GForm12.int_2;
					GForm12.Enum4 @enum;
					if (array14[1] == 7)
					{
						if (array14[4] == 0 && array14[5] == 0)
						{
							if (num2 < 2)
							{
								num2++;
								continue;
							}
							@enum = GForm12.Enum4.const_1;
						}
						else
						{
							@enum = GForm12.Enum4.const_1;
						}
					}
					else if (array14[1] == 5)
					{
						TimeSpan elapsed = stopwatch.Elapsed;
						if (num3 == 64 && (i + 1) % 2 == 0)
						{
							this.method_22(array3, (int)array3[1], ref array14, ref num, 0);
							Thread.Sleep(100);
						}
						@enum = GForm12.Enum4.const_0;
						this.method_40(this.label_17, string.Concat(new string[]
						{
							"กำลังเขียน : ",
							(num8 / 1024.0).ToString("F2"),
							" KB / ",
							((double)GForm12.int_2 / 1024.0).ToString("F2"),
							" KB (",
							(num9 * 100.0).ToString("F2"),
							"%)"
						}));
					}
					else
					{
						@enum = GForm12.Enum4.const_1;
					}
					this.method_42(this.gclass13_0, (int)(num9 * 10000.0));
					if (@enum != GForm12.Enum4.const_1)
					{
						num5 = num6;
						i++;
						break;
					}
					stopwatch.Stop();
					base.BeginInvoke(new Action(GForm12.Class142.class142_0.method_0));
					this.method_42(this.gclass13_0, 0);
					this.method_40(this.label_17, "");
					return;
				}
			}
			stopwatch.Stop();
			if (enum3_0 != GForm12.Enum3.const_3 && enum3_0 != GForm12.Enum3.const_4)
			{
				if (enum3_0 != GForm12.Enum3.const_5)
				{
					if (num3 == 64)
					{
						this.method_22(array3, (int)array3[1], ref array14, ref num, 0);
						Thread.Sleep(100);
					}
					this.method_22(array4, array4.Length, ref array14, ref num, 0);
					Thread.Sleep(1000);
					this.method_22(array2, array2.Length, ref array14, ref num, 0);
					this.method_22(array5, array5.Length, ref array14, ref num, 0);
					Thread.Sleep(1000);
					this.method_22(array2, array2.Length, ref array14, ref num, 0);
					this.method_22(array6, array6.Length, ref array14, ref num, 0);
					Thread.Sleep(1000);
					this.method_22(array2, array2.Length, ref array14, ref num, 0);
					this.method_22(array7, array7.Length, ref array14, ref num, 0);
					Thread.Sleep(1000);
					if (this.method_22(array2, array2.Length, ref array14, ref num, 0) && array14[3] == 15 && this.method_22(array8, array8.Length, ref array14, ref num, 0) && array14[3] == 15)
					{
						base.BeginInvoke(new Action(GForm12.Class142.class142_0.method_2));
						this.method_40(this.label_12, "อัดไฟล์สำเร็จ!! ปิดเปิดกุญแจ ใหม่อีกครั้ง");
						this.label_12.ForeColor = Color.Lime;
						this.method_40(this.label_17, "");
						this.method_42(this.gclass13_0, 0);
						return;
					}
					goto IL_75C;
				}
			}
			this.method_22(array9, array9.Length, ref array14, ref num, int_9);
			Thread.Sleep(1000);
			this.method_22(array10, array10.Length, ref array14, ref num, int_9);
			Thread.Sleep(1000);
			this.method_22(array11, array11.Length, ref array14, ref num, int_9);
			Thread.Sleep(1000);
			this.method_22(array12, array12.Length, ref array14, ref num, int_9);
			Thread.Sleep(3000);
			if (this.method_22(array13, array13.Length, ref array14, ref num, int_9) && array14[3] == 15)
			{
				base.BeginInvoke(new Action(this.method_107));
				this.method_42(this.gclass13_0, 0);
				this.method_40(this.label_17, "");
				return;
			}
			IL_75C:
			base.BeginInvoke(new Action(this.method_108));
			this.method_40(this.label_17, "");
			this.method_42(this.gclass13_0, 0);
		}

		// Token: 0x06000254 RID: 596 RVA: 0x00029148 File Offset: 0x00027348
		private bool method_24(byte[] byte_15, ref byte[] byte_16)
		{
			for (int i = 0; i < GForm12.int_2 - GForm12.byte_2.Length; i++)
			{
				if (this.method_34(GForm12.byte_12, i, GForm12.byte_2, GForm12.byte_2.Length))
				{
					for (int j = i + GForm12.byte_2.Length; j < GForm12.int_2 - 10; j++)
					{
						if (GForm12.byte_12[j + 8] == 238)
						{
							if (GForm12.byte_12[j + 9] != 0)
							{
								if (GForm12.byte_12[j + 9] != 255)
								{
									goto IL_59;
								}
							}
							ushort num = (ushort)((int)byte_15[0] * 256 + (int)byte_15[1]);
							ushort num2 = (ushort)((int)GForm12.byte_12[j + 1] * 256 + (int)GForm12.byte_12[j]);
							ushort num3 = (ushort)((int)GForm12.byte_12[j + 3] * 256 + (int)GForm12.byte_12[j + 2]);
							ushort num4 = (ushort)((int)GForm12.byte_12[j + 5] * 256 + (int)GForm12.byte_12[j + 4]);
							ushort num5 = num3 * (num + num2);
							if (num4 > 0)
							{
								byte_16[0] = (byte)(num5 + num % num4 >> 8 & 255);
								byte_16[1] = (byte)(num5 + num % num4 & 255);
								return true;
							}
							byte_16[0] = (byte)(num5 + num >> 8 & 255);
							byte_16[1] = (byte)(num5 + num & 255);
							return true;
						}
						IL_59:;
					}
				}
			}
			return false;
		}

		// Token: 0x06000255 RID: 597 RVA: 0x00029288 File Offset: 0x00027488
		private bool method_25(byte[] byte_15, ref byte[] byte_16)
		{
			for (int i = 0; i < GForm12.int_2 - GForm12.byte_3.Length; i++)
			{
				if (this.method_34(GForm12.byte_12, i + 6, GForm12.byte_3, GForm12.byte_3.Length))
				{
					ushort num = (ushort)((int)byte_15[0] * 256 + (int)byte_15[1]);
					ushort num2 = (ushort)((int)GForm12.byte_12[i + 1] * 256 + (int)GForm12.byte_12[i]);
					ushort num3 = (ushort)((int)GForm12.byte_12[i + 3] * 256 + (int)GForm12.byte_12[i + 2]);
					ushort num4 = (ushort)((int)GForm12.byte_12[i + 5] * 256 + (int)GForm12.byte_12[i + 4]);
					ushort num5 = num3 * (num + num2);
					bool result;
					if (num4 > 0)
					{
						byte_16[0] = (byte)(num5 + num % num4 >> 8 & 255);
						byte_16[1] = (byte)(num5 + num % num4 & 255);
						result = true;
					}
					else
					{
						byte_16[0] = (byte)(num5 + num >> 8 & 255);
						byte_16[1] = (byte)(num5 + num & 255);
						result = true;
					}
					return result;
				}
			}
			return false;
		}

		// Token: 0x06000256 RID: 598 RVA: 0x00029384 File Offset: 0x00027584
		private void method_26(GForm12.Enum3 enum3_0)
		{
			for (int i = 0; i < GForm12.int_2 - GForm12.byte_2.Length; i++)
			{
				if (this.method_34(GForm12.byte_12, i, GForm12.byte_2, GForm12.byte_2.Length))
				{
					for (int j = i + GForm12.byte_2.Length; j < GForm12.int_2 - 6; j++)
					{
						if (GForm12.byte_12[j] == 208 && GForm12.byte_12[j + 1] == 7)
						{
							int num = j;
							if ((long)j - ((long)j + (long)((ulong)((uint)(j - 4 >> 31) >> 30)) - 4L & 4294967292L) == 6L)
							{
								num = j - 6;
							}
							this.method_40(this.textBox_0, (num - 4).ToString("X"));
							uint num2 = this.method_18(GForm12.byte_12, num - 4, 0) + this.method_18(GForm12.byte_12, GForm12.int_2 - num, num);
							byte[] bytes = BitConverter.GetBytes(0U - num2);
							GForm12.byte_12[num - 4] = bytes[0];
							GForm12.byte_12[num - 3] = bytes[1];
							GForm12.byte_12[num - 2] = bytes[2];
							GForm12.byte_12[num - 1] = bytes[3];
							Debug.WriteLine(string.Concat(new string[]
							{
								"Address ",
								(num - 4).ToString("X"),
								" bytes ",
								bytes[0].ToString("X2"),
								bytes[1].ToString("X2"),
								bytes[2].ToString("X2"),
								bytes[3].ToString("X2")
							}));
							return;
						}
					}
				}
			}
		}

		// Token: 0x06000257 RID: 599 RVA: 0x00029538 File Offset: 0x00027738
		private bool method_27(bool bool_15 = false, bool bool_16 = false)
		{
			byte[] array = new byte[]
			{
				123,
				8,
				2,
				2,
				80,
				25,
				118,
				154
			};
			byte[] array2 = new byte[]
			{
				123,
				8,
				2,
				3,
				37,
				55,
				134,
				150
			};
			byte[] array3 = new byte[]
			{
				123,
				5,
				2,
				4,
				122
			};
			byte[] array4 = new byte[]
			{
				123,
				7,
				2,
				5,
				0,
				0,
				0
			};
			byte[] array5 = new byte[]
			{
				124,
				6,
				2,
				1,
				0,
				123
			};
			byte[] array6 = new byte[]
			{
				124,
				11,
				2,
				11,
				0,
				0,
				0,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				111
			};
			byte[] array7 = new byte[]
			{
				124,
				7,
				2,
				14,
				1,
				0,
				108
			};
			byte[] array8 = new byte[]
			{
				124,
				6,
				2,
				4,
				byte.MaxValue,
				121
			};
			byte[] array9 = new byte[]
			{
				124,
				5,
				2,
				5,
				120
			};
			byte[] array10 = new byte[256];
			uint num = 0U;
			int int_ = 0;
			if (!bool_15)
			{
				int_ = 150;
			}
			this.method_40(this.label_17, "กำลังส่งสัญญาณปลุกกล่อง Shindengen...");
			this.method_22(array, array.Length, ref array10, ref num, int_);
			this.method_22(array2, array2.Length, ref array10, ref num, int_);
			base.Invoke(new Action(this.method_109));
			this.method_40(this.label_17, "กำลังตรวจสอบ Seed/Key...");
			this.method_42(this.gclass13_0, 0);
			this.method_22(array3, array3.Length, ref array10, ref num, int_);
			bool result;
			if (array10[1] == 7 && array10[3] == 0)
			{
				byte[] array11 = new byte[2];
				byte[] array12 = new byte[2];
				array11[0] = array10[4];
				array11[1] = array10[5];
				if ((!bool_16) ? this.method_24(array11, ref array12) : this.method_25(array11, ref array12))
				{
					array4[4] = array12[0];
					array4[5] = array12[1];
					array4[6] = this.method_20(array4, (int)(array10[1] - 1), 0);
				}
				this.method_22(array4, array4.Length, ref array10, ref num, int_);
				if (array10[1] == 5 && array10[3] == 0)
				{
					this.method_40(this.label_17, "กำลังเริ่ม ประมวลผล");
					this.method_22(array5, array5.Length, ref array10, ref num, int_);
					this.method_22(array6, array6.Length, ref array10, ref num, int_);
					this.method_22(array7, array7.Length, ref array10, ref num, int_);
					if (this.method_22(array8, array8.Length, ref array10, ref num, int_) && array10[3] == 0)
					{
						Thread.Sleep(2000);
						if (this.method_22(array9, array9.Length, ref array10, ref num, int_) && array10[3] == 0)
						{
							this.method_40(this.label_17, "เสร็จสิ้นการประมวลผล เริ่มเขียนไฟล์");
							return true;
						}
						if (array10[3] == 250)
						{
							MessageBox.Show("การเขียนไฟล์ล้มเหลว, ECM บล็อค", "ฮอนด้า แฟลช", MessageBoxButtons.OK, MessageBoxIcon.Hand);
							return false;
						}
					}
					MessageBox.Show("การเขียนไฟล์ ล้มเหลว ", "ฮอนด้า แฟลช", MessageBoxButtons.OK, MessageBoxIcon.Hand);
					result = false;
				}
				else
				{
					MessageBox.Show("การเขียนไฟล์ ล้มเหลว ", "ฮอนด้า แฟลช", MessageBoxButtons.OK, MessageBoxIcon.Hand);
					result = false;
				}
			}
			else
			{
				MessageBox.Show("การตรวจสอบ ล้มเหลว Seed/Key", "ฮอนด้า แฟลช", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				result = false;
			}
			return result;
		}

		// Token: 0x06000258 RID: 600 RVA: 0x00029814 File Offset: 0x00027A14
		private void method_28(int int_8 = 0)
		{
			byte b = (int_8 != 0) ? (this.method_17(GForm12.byte_12, int_8, 0) + this.method_17(GForm12.byte_12, GForm12.int_2 - (int_8 + 1), int_8 + 1)) : this.method_17(GForm12.byte_12, GForm12.int_2 - 1, 1);
			GForm12.byte_12[int_8] = -b;
		}

		// Token: 0x06000259 RID: 601 RVA: 0x0002986C File Offset: 0x00027A6C
		private bool method_29()
		{
			byte[] array = new byte[]
			{
				125,
				8,
				1,
				2,
				80,
				71,
				77,
				148
			};
			byte[] array2 = new byte[]
			{
				125,
				8,
				1,
				3,
				45,
				70,
				73,
				187
			};
			byte[] array3 = new byte[]
			{
				126,
				5,
				1,
				2,
				122
			};
			byte[] array4 = new byte[]
			{
				126,
				7,
				1,
				3,
				0,
				0,
				119
			};
			byte[] array5 = new byte[]
			{
				126,
				11,
				1,
				11,
				0,
				0,
				0,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				110
			};
			byte[] array6 = new byte[]
			{
				126,
				7,
				1,
				14,
				1,
				144,
				219
			};
			byte[] array7 = new byte[]
			{
				126,
				6,
				1,
				4,
				byte.MaxValue,
				120
			};
			byte[] array8 = new byte[]
			{
				126,
				5,
				1,
				5,
				119
			};
			byte[] array9 = new byte[]
			{
				126,
				6,
				1,
				1,
				0,
				122
			};
			byte[] array10 = new byte[256];
			uint num = 0U;
			int num2 = 0;
			this.method_40(this.label_17, "กำลังส่งสัญญาณปลุกกล่อง ECU...");
			while (!this.method_22(array, array.Length, ref array10, ref num, 0))
			{
				Thread.Sleep(500);
				if (num2 >= 1)
				{
					IL_103:
					Thread.Sleep(200);
					this.method_22(array9, array9.Length, ref array10, ref num, 0);
					base.Invoke(new Action(this.method_110));
					this.method_40(this.label_17, "กำลังประมวลผลเตรียมอัดไฟล์...");
					this.method_42(this.gclass13_0, 0);
					Thread.Sleep(500);
					this.method_22(array3, array3.Length, ref array10, ref num, 250);
					this.method_22(array4, array4.Length, ref array10, ref num, 0);
					this.method_22(array9, array9.Length, ref array10, ref num, 0);
					this.method_22(array5, array5.Length, ref array10, ref num, 0);
					this.method_22(array9, array9.Length, ref array10, ref num, 0);
					this.method_22(array6, array6.Length, ref array10, ref num, 0);
					if (this.method_22(array7, array7.Length, ref array10, ref num, 0) && array10[3] == 0)
					{
						Thread.Sleep(2000);
						if (this.method_22(array8, array8.Length, ref array10, ref num, 250) && array10[3] == 0)
						{
							this.method_40(this.label_17, "ประมวลผลสำเร็จ กำลังเริ่มเขียนไฟล์...");
							this.method_22(array9, array9.Length, ref array10, ref num, 0);
							return true;
						}
						if (array10[3] == 250)
						{
							base.BeginInvoke(new Action(GForm12.Class142.class142_0.method_4));
							this.method_40(this.label_17, "ล้มเหลว: ECM บล็อค");
							return false;
						}
					}
					base.BeginInvoke(new Action(GForm12.Class142.class142_0.method_5));
					this.method_40(this.label_17, "เชื่อมต่อเตรียมอัดไฟล์ล้มเหลว");
					return false;
				}
				num2++;
			}
			this.method_22(array2, array2.Length, ref array10, ref num, 0);
			goto IL_103;
		}

		// Token: 0x0600025A RID: 602 RVA: 0x00029B28 File Offset: 0x00027D28
		private bool method_30()
		{
			byte[] array = new byte[1];
			byte[] array2 = new byte[]
			{
				1
			};
			uint num = 0U;
			if (FTDI.FT_Open(0U, ref GForm12.intptr_0) > FTDI.FT_STATUS.FT_OK)
			{
				FTDI.FT_Close(GForm12.intptr_0);
				return false;
			}
			if (FTDI.FT_Purge(GForm12.intptr_0, 3U) > FTDI.FT_STATUS.FT_OK)
			{
				FTDI.FT_Close(GForm12.intptr_0);
				return false;
			}
			if (FTDI.FT_SetBitMode(GForm12.intptr_0, 0, 0) > FTDI.FT_STATUS.FT_OK)
			{
				FTDI.FT_Close(GForm12.intptr_0);
				return false;
			}
			if (FTDI.FT_SetDataCharacteristics(GForm12.intptr_0, 8, 0, 0) > FTDI.FT_STATUS.FT_OK)
			{
				FTDI.FT_Close(GForm12.intptr_0);
				return false;
			}
			if (FTDI.FT_SetBaudRate(GForm12.intptr_0, 921600U) > FTDI.FT_STATUS.FT_OK)
			{
				FTDI.FT_Close(GForm12.intptr_0);
				return false;
			}
			if (FTDI.FT_SetTimeouts(GForm12.intptr_0, 50U, 0U) > FTDI.FT_STATUS.FT_OK)
			{
				FTDI.FT_Close(GForm12.intptr_0);
				return false;
			}
			if (FTDI.FT_SetLatencyTimer(GForm12.intptr_0, 8) > FTDI.FT_STATUS.FT_OK)
			{
				FTDI.FT_Close(GForm12.intptr_0);
				return false;
			}
			if (FTDI.FT_SetBitMode(GForm12.intptr_0, 1, 1) > FTDI.FT_STATUS.FT_OK)
			{
				FTDI.FT_Close(GForm12.intptr_0);
				return false;
			}
			if (FTDI.FT_Write(GForm12.intptr_0, array, (uint)array.Length, ref num) > FTDI.FT_STATUS.FT_OK)
			{
				FTDI.FT_Close(GForm12.intptr_0);
				return false;
			}
			Thread.Sleep(70);
			if (FTDI.FT_Write(GForm12.intptr_0, array2, (uint)array2.Length, ref num) > FTDI.FT_STATUS.FT_OK)
			{
				FTDI.FT_Close(GForm12.intptr_0);
				return false;
			}
			if (FTDI.FT_SetBitMode(GForm12.intptr_0, 0, 0) > FTDI.FT_STATUS.FT_OK)
			{
				FTDI.FT_Close(GForm12.intptr_0);
				return false;
			}
			if (FTDI.FT_SetBaudRate(GForm12.intptr_0, 10400U) > FTDI.FT_STATUS.FT_OK)
			{
				FTDI.FT_Close(GForm12.intptr_0);
				return false;
			}
			if (FTDI.FT_Purge(GForm12.intptr_0, 3U) > FTDI.FT_STATUS.FT_OK)
			{
				FTDI.FT_Close(GForm12.intptr_0);
				return false;
			}
			Thread.Sleep(130);
			return true;
		}

		// Token: 0x0600025B RID: 603 RVA: 0x00029CD4 File Offset: 0x00027ED4
		private void method_31()
		{
			try
			{
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
					114,
					7,
					114,
					0,
					0,
					5,
					16
				};
				byte[] array4 = new byte[]
				{
					125,
					6,
					1,
					1,
					3,
					120
				};
				byte[] array5 = new byte[]
				{
					123,
					6,
					2,
					1,
					3,
					121
				};
				byte[] array6 = new byte[]
				{
					126,
					6,
					1,
					1,
					0,
					122
				};
				byte[] array7 = new byte[]
				{
					124,
					6,
					2,
					1,
					0,
					123
				};
				byte[] array8 = new byte[]
				{
					114,
					5,
					0,
					240,
					153
				};
				byte[] array9 = new byte[]
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
				byte[] byte_ = new byte[]
				{
					7,
					8,
					70,
					105,
					110,
					46,
					127,
					39
				};
				byte[] array10 = new byte[256];
				uint num = 0U;
				int num2 = 0;
				IL_6B6:
				while (!GForm12.bool_3)
				{
					if (this.method_30())
					{
						if (GForm12.bool_5)
						{
							if (!GForm12.bool_4)
							{
								GClass19.smethod_0("ECU Connected.");
							}
							GForm12.bool_4 = true;
							GForm12.bool_5 = false;
						}
						this.method_22(array, array.Length, ref array10, ref num, 0);
						if (this.method_22(array2, array2.Length, ref array10, ref num, 0))
						{
							array8 = new byte[]
							{
								114,
								5,
								0,
								240,
								153
							};
						}
						else if (this.method_22(array6, array6.Length, ref array10, ref num, 0))
						{
							array8 = new byte[]
							{
								126,
								6,
								1,
								1,
								0,
								122
							};
						}
						else
						{
							array8 = new byte[]
							{
								114,
								5,
								0,
								240,
								153
							};
						}
						while (!GForm12.bool_3)
						{
							if (!this.bool_14)
							{
								if (this.method_22(array8, array8.Length, ref array10, ref num, 0))
								{
									if (++num2 >= 4)
									{
										num2 = 0;
										double num3 = this.method_112();
										if (!double.IsNaN(num3))
										{
											this.method_113(num3);
										}
									}
									if (!string.Equals(this.method_35(this.label_11), "ON"))
									{
										string string_ = "-";
										string string_2 = "-";
										GForm12.bool_4 = true;
										if (this.method_22(array3, array3.Length, ref array10, ref num, 0) && num >= 10U)
										{
											string_ = BitConverter.ToString(array10, 5, 5).Replace("-", "");
										}
										if (this.method_22(array4, array4.Length, ref array10, ref num, 0))
										{
											if (num >= 8U)
											{
												string_2 = string.Format("{0}/{1}", array10[6], array10[7]);
											}
										}
										else if (this.method_22(array5, array5.Length, ref array10, ref num, 0) && num >= 8U)
										{
											string_2 = string.Format("{0}/{1}", array10[6], array10[7]);
										}
										this.method_40(this.label_11, "ON");
										this.label_11.ForeColor = Color.Green;
										this.method_40(this.label_10, string_);
										this.method_40(this.label_9, string_2);
										if (this.method_36(string_) == 0)
										{
											GForm12.bool_7 = true;
										}
									}
									if (this.bool_9)
									{
										bool flag = this.method_22(array, array.Length, ref array10, ref num, 0);
										bool flag2 = this.method_22(array2, array2.Length, ref array10, ref num, 0);
										bool flag3 = this.method_22(array9, array9.Length, ref array10, ref num, 0);
										object obj = this.object_0;
										byte byte_2;
										byte byte_3;
										lock (obj)
										{
											byte_2 = this.byte_13;
											byte_3 = this.byte_14;
										}
										byte[] array11 = GForm12.smethod_4(byte_2, byte_3);
										bool flag5 = this.method_22(array11, array11.Length, ref array10, ref num, 0);
										if (!flag || !flag2 || !flag3 || !flag5 || num < 8U)
										{
											GForm12.bool_4 = false;
											this.bool_9 = false;
											this.enum5_0 = GForm12.Enum5.const_0;
											this.method_40(this.label_11, "เชื่อมต่อไม่สำเร็จ");
											this.method_40(this.label_12, "ตรวจสอบสาย/สวิตช์ แล้วลองใหม่");
											goto IL_633;
										}
										if (GForm12.smethod_6(array10, num, byte_))
										{
											this.bool_14 = true;
											this.bool_9 = false;
											this.enum5_0 = GForm12.Enum5.const_0;
											this.method_33(this.label_12, "เข้าสู่โหมด Security แล้ว");
											continue;
										}
										if (this.enum5_0 == GForm12.Enum5.const_1)
										{
											if (GForm12.smethod_5(array10, num))
											{
												int num4 = this.int_5 + 1;
												this.int_5 = num4;
												if (num4 >= 2)
												{
													this.method_40(this.label_12, "ปิดแล้ว ✅ กรุณาเปิดสวิตช์กุญแจ (ON)");
													this.enum5_0 = GForm12.Enum5.const_2;
												}
											}
											else
											{
												this.int_5 = 0;
											}
											Thread.Sleep(200);
											continue;
										}
										if (this.enum5_0 == GForm12.Enum5.const_2)
										{
											if (!GForm12.smethod_5(array10, num))
											{
												this.method_40(this.label_12, "ส่งคำสั่งพิเศษเรียบร้อย ✅");
												this.bool_9 = false;
												this.enum5_0 = GForm12.Enum5.const_0;
												this.int_5 = 0;
											}
											Thread.Sleep(200);
											continue;
										}
									}
									if (this.bool_14 || !GForm12.bool_10)
									{
										Thread.Sleep(300);
										continue;
									}
									GForm12.bool_10 = false;
									GForm12.Enum3 @enum = this.method_38();
									if (@enum != GForm12.Enum3.const_3 && @enum != GForm12.Enum3.const_4)
									{
										if (@enum != GForm12.Enum3.const_5)
										{
											this.method_28(GForm12.int_4);
											if (this.method_29())
											{
												this.method_23(GForm12.int_3, @enum, GForm12.bool_8);
											}
											while (this.method_22(array6, array6.Length, ref array10, ref num, 0))
											{
												if (GForm12.bool_3)
												{
													return;
												}
												Thread.Sleep(1000);
											}
											goto IL_633;
										}
									}
									this.method_26(@enum);
									if (this.method_27(GForm12.bool_8, GForm12.bool_7))
									{
										this.method_23(GForm12.int_3, @enum, GForm12.bool_8);
									}
									while (this.method_22(array7, array7.Length, ref array10, ref num, 0))
									{
										if (GForm12.bool_3)
										{
											return;
										}
										Thread.Sleep(1000);
									}
								}
								else if (GForm12.bool_4)
								{
									GForm12.bool_4 = false;
									this.method_40(this.label_11, "OFF");
									this.label_11.ForeColor = Color.Red;
									this.method_40(this.label_10, "-");
									this.method_40(this.label_12, "กรุณาเชื่อมต่อรถ/เปิดกุญแจรถ !!");
									this.method_40(this.label_9, "-");
									this.method_40(this.textBox_1, "");
									this.method_40(this.textBox_0, "");
								}
								IL_633:
								try
								{
									FTDI.FT_Close(GForm12.intptr_0);
									goto IL_6B6;
								}
								catch
								{
									goto IL_6B6;
								}
								goto IL_644;
							}
							Thread.Sleep(1000);
						}
						break;
					}
					IL_644:
					if (!GForm12.bool_5)
					{
						GForm12.bool_5 = true;
						this.label_11.ForeColor = Color.Red;
						this.method_40(this.label_10, "-");
						this.method_40(this.label_12, "LOY-TUNER | FLASHERNEW 2026");
						this.method_40(this.label_9, "-");
						this.method_40(this.textBox_1, "");
						this.method_40(this.textBox_0, "");
					}
				}
			}
			catch (Exception ex)
			{
				GForm12.Class145 @class = new GForm12.Class145();
				@class.gform12_0 = this;
				Exception exception_ = ex;
				@class.exception_0 = exception_;
				base.BeginInvoke(new Action(@class.method_0));
			}
			finally
			{
				try
				{
					FTDI.FT_Close(GForm12.intptr_0);
				}
				catch
				{
				}
			}
		}

		// Token: 0x0600025C RID: 604 RVA: 0x0002A468 File Offset: 0x00028668
		private void method_32(GForm12.Enum3 enum3_0)
		{
			try
			{
				if (GForm12.byte_12 != null && GForm12.byte_12.Length >= 1024)
				{
					byte[] bytes = Encoding.ASCII.GetBytes("MZA_SHIELD_2026_ULTRA_PRO");
					int destinationIndex = GForm12.byte_12.Length / 2;
					Array.Copy(bytes, 0, GForm12.byte_12, destinationIndex, bytes.Length);
					if (enum3_0 != GForm12.Enum3.const_1 && enum3_0 != GForm12.Enum3.const_2 && enum3_0 != GForm12.Enum3.const_0)
					{
						for (int i = 0; i < 64; i++)
						{
							if (GForm12.byte_12.Length > 512 + i)
							{
								GForm12.byte_12[512 + i] = (GForm12.byte_12[512 + i] ^ 51);
							}
						}
						byte[] array = new byte[]
						{
							222,
							173,
							190,
							239,
							77,
							90,
							65
						};
						Array.Copy(array, 0, GForm12.byte_12, GForm12.byte_12.Length - 20, array.Length);
					}
					else if (GForm12.byte_12.Length >= 32768)
					{
						GForm12.byte_12[32764] = 85;
						GForm12.byte_12[32765] = 170;
						GForm12.byte_12[32766] = byte.MaxValue;
						GForm12.byte_12[32767] = 0;
						for (int j = 0; j < 4; j++)
						{
							byte[] array2 = GForm12.byte_12;
							int num = GForm12.byte_12.Length - 10 - j;
							array2[num] ^= 90;
						}
					}
					GForm15.smethod_1("MZA SHIELD", "ระบบล็อคข้อมูล (Deep Lock) ทำงานเรียบร้อย", GEnum1.const_0);
				}
			}
			catch
			{
			}
		}

		// Token: 0x0600025D RID: 605 RVA: 0x0002A5D8 File Offset: 0x000287D8
		private void method_33(Control control_0, string string_9)
		{
			GForm12.Class146 @class = new GForm12.Class146();
			@class.control_0 = control_0;
			@class.string_0 = string_9;
			if (@class.control_0.InvokeRequired)
			{
				@class.control_0.BeginInvoke(new Action(@class.method_0));
				return;
			}
			@class.control_0.Text = @class.string_0;
		}

		// Token: 0x0600025E RID: 606 RVA: 0x0002A630 File Offset: 0x00028830
		private bool method_34(byte[] byte_15, int int_8, byte[] byte_16, int int_9)
		{
			bool result;
			if (int_8 + int_9 > byte_15.Length)
			{
				result = false;
			}
			else
			{
				for (int i = 0; i < int_9; i++)
				{
					if (byte_15[int_8 + i] != byte_16[i])
					{
						return false;
					}
				}
				result = true;
			}
			return result;
		}

		// Token: 0x0600025F RID: 607 RVA: 0x0002A668 File Offset: 0x00028868
		private string method_35(Control control_0)
		{
			GForm12.Class147 @class = new GForm12.Class147();
			@class.control_0 = control_0;
			if (@class.control_0 == null)
			{
				return string.Empty;
			}
			if (@class.control_0.InvokeRequired)
			{
				@class.string_0 = string.Empty;
				@class.control_0.Invoke(new Action(@class.method_0));
				return @class.string_0;
			}
			return @class.control_0.Text;
		}

		// Token: 0x06000260 RID: 608 RVA: 0x0002A6D4 File Offset: 0x000288D4
		private int method_36(string string_9)
		{
			int result;
			if (string_9.EndsWith("0000"))
			{
				for (int i = 0; i < GForm12.string_4.Length; i++)
				{
					if (GForm12.string_4[i].Substring(0, 6).Equals(string_9.Substring(0, 6)))
					{
						this.method_40(this.label_12, "!!! ว้ายๆ กล่องไปดิ้ !!!");
						this.method_40(this.textBox_1, GForm12.string_5[i]);
						this.method_40(this.textBox_0, GForm12.string_6[i]);
						GForm12.bool_6 = false;
						return 0;
					}
				}
				for (int j = 0; j < GForm12.string_8.Length; j++)
				{
					if (GForm12.string_8[j].Substring(0, 6).Equals(string_9.Substring(0, 6)))
					{
						this.method_40(this.label_12, "!!! ว้ายๆ กล่องไปดิ้ !!!");
						this.method_40(this.textBox_1, "auto");
						this.method_40(this.textBox_0, "auto");
						GForm12.bool_6 = false;
						return 0;
					}
				}
				this.method_40(this.label_12, "!!! ว้ายๆ กล่องไปดิ้ !!!");
				this.method_40(this.textBox_1, "");
				this.method_40(this.textBox_0, "");
				GForm12.bool_6 = true;
				result = 0;
			}
			else if (string_9.Contains("-"))
			{
				this.method_40(this.label_12, "!!! ว้ายๆ กล่องไปดิ้ !!!");
				GForm12.bool_6 = true;
				result = 0;
			}
			else
			{
				for (int k = 0; k < GForm12.string_4.Length; k++)
				{
					if (GForm12.string_4[k].Equals(string_9))
					{
						this.method_40(this.label_12, GForm12.string_3[k]);
						this.method_40(this.textBox_1, GForm12.string_5[k]);
						this.method_40(this.textBox_0, GForm12.string_6[k]);
						GForm12.bool_6 = false;
						return 1;
					}
				}
				for (int l = 0; l < GForm12.string_8.Length; l++)
				{
					if (GForm12.string_8[l].Equals(string_9))
					{
						this.method_40(this.label_12, GForm12.string_7[l]);
						this.method_40(this.textBox_1, "auto");
						this.method_40(this.textBox_0, "auto");
						GForm12.bool_6 = false;
						return 1;
					}
				}
				this.method_40(this.label_12, "!!! อัพเดทรหัสกล่องบ้างนะ !!!");
				this.method_40(this.textBox_1, "");
				this.method_40(this.textBox_0, "");
				GForm12.bool_6 = true;
				result = 1;
			}
			return result;
		}

		// Token: 0x06000261 RID: 609 RVA: 0x0002A93C File Offset: 0x00028B3C
		private void method_37()
		{
			if (GForm12.int_2 == 49152)
			{
				this.textBox_1.Text = "4000";
				this.textBox_0.Text = "7600";
				return;
			}
			if (GForm12.int_2 == 57344)
			{
				this.textBox_1.Text = "0";
				this.textBox_0.Text = "DFEF";
				return;
			}
			if (GForm12.int_2 != 65536)
			{
				if (GForm12.int_2 != 98304)
				{
					if (GForm12.int_2 == 131072)
					{
						this.textBox_1.Text = "E0000";
						this.textBox_0.Text = "B000";
						return;
					}
					if (GForm12.int_2 < 262144)
					{
						return;
					}
					if (this.method_34(GForm12.byte_12, GForm12.int_2 - GForm12.byte_0.Length, GForm12.byte_0, GForm12.byte_0.Length))
					{
						for (int i = 0; i < GForm12.int_2 - 5; i++)
						{
							if (this.method_34(GForm12.byte_12, i, GForm12.byte_4, GForm12.byte_4.Length) || this.method_34(GForm12.byte_12, i, GForm12.byte_5, GForm12.byte_5.Length))
							{
								this.method_40(this.textBox_1, "auto");
								this.method_40(this.textBox_0, "auto");
								return;
							}
						}
						return;
					}
					if (GForm12.int_2 == 262144 && (this.method_34(GForm12.byte_12, GForm12.int_2 - GForm12.byte_8.Length, GForm12.byte_8, GForm12.byte_8.Length) || this.method_34(GForm12.byte_12, GForm12.int_2 - GForm12.byte_7.Length, GForm12.byte_7, GForm12.byte_7.Length) || this.method_34(GForm12.byte_12, GForm12.int_2 - GForm12.byte_9.Length, GForm12.byte_9, GForm12.byte_9.Length)))
					{
						this.textBox_1.Text = "0";
						this.textBox_0.Text = "3FFF8";
						return;
					}
					if (GForm12.int_2 == 524288)
					{
						if (this.method_34(GForm12.byte_12, GForm12.int_2 - GForm12.byte_9.Length, GForm12.byte_9, GForm12.byte_9.Length))
						{
							this.textBox_1.Text = "0";
							this.textBox_0.Text = "7FFF8";
							return;
						}
					}
					else if (GForm12.int_2 == 1048576)
					{
						if (this.method_34(GForm12.byte_12, GForm12.int_2 - GForm12.byte_11.Length, GForm12.byte_11, GForm12.byte_11.Length))
						{
							this.textBox_1.Text = "0";
							this.textBox_0.Text = "FFFF8";
							return;
						}
						this.textBox_1.Text = "0";
						this.textBox_0.Text = "7FFF8";
					}
					return;
				}
			}
			this.textBox_1.Text = "8000";
			this.textBox_0.Text = "0";
		}

		// Token: 0x06000262 RID: 610 RVA: 0x0002AC1C File Offset: 0x00028E1C
		private GForm12.Enum3 method_38()
		{
			if (this.method_34(GForm12.byte_12, GForm12.int_2 - GForm12.byte_0.Length, GForm12.byte_0, GForm12.byte_0.Length))
			{
				bool flag = false;
				int i = 0;
				while (i < GForm12.int_2 - GForm12.byte_2.Length)
				{
					if (this.method_34(GForm12.byte_12, i, GForm12.byte_2, GForm12.byte_2.Length))
					{
						flag = true;
					}
					if (this.method_34(GForm12.byte_12, i, GForm12.byte_4, GForm12.byte_4.Length))
					{
						this.method_40(this.textBox_1, "10000");
						return GForm12.Enum3.const_3;
					}
					if (!this.method_34(GForm12.byte_12, i, GForm12.byte_5, GForm12.byte_5.Length))
					{
						i++;
					}
					else
					{
						if (this.method_34(GForm12.byte_12, 40960, GForm12.byte_1, GForm12.byte_1.Length))
						{
							this.method_40(this.textBox_1, "A000");
							return GForm12.Enum3.const_5;
						}
						this.method_40(this.textBox_1, "10000");
						return GForm12.Enum3.const_4;
					}
				}
				if (flag)
				{
					this.method_40(this.textBox_1, "10000");
					return GForm12.Enum3.const_3;
				}
			}
			bool flag2;
			if (GForm12.int_2 != 57344)
			{
				if (GForm12.int_2 != 32768)
				{
					flag2 = false;
					goto IL_144;
				}
			}
			flag2 = this.method_34(GForm12.byte_12, GForm12.int_2 - GForm12.byte_6.Length, GForm12.byte_6, GForm12.byte_6.Length);
			IL_144:
			if (flag2)
			{
				return GForm12.Enum3.const_1;
			}
			if (GForm12.int_2 == 262144 && this.method_34(GForm12.byte_12, GForm12.int_2 - GForm12.byte_7.Length, GForm12.byte_7, GForm12.byte_7.Length))
			{
				return GForm12.Enum3.const_2;
			}
			return GForm12.Enum3.const_0;
		}

		// Token: 0x06000263 RID: 611 RVA: 0x0002ADA8 File Offset: 0x00028FA8
		private void GForm12_Load(object sender, EventArgs e)
		{
			GForm12.Class148 @class = new GForm12.Class148();
			@class.gform12_0 = this;
			base.Region = Region.FromHrgn(GForm12.CreateRoundRectRgn(0, 0, base.Width, base.Height, 15, 15));
			this.menuStrip_0.MouseDown += @class.method_0;
			this.menuStrip_0.MouseMove += @class.method_1;
			this.menuStrip_0.MouseUp += @class.method_2;
			this.menuStrip_0.Renderer = new GForm12.GClass10();
			this.menuStrip_0.BackColor = Color.Black;
			this.menuStrip_0.Padding = new Padding(3, 3, 0, 3);
			string text = this.label_3.Text;
			this.label_3.Text = "";
			@class.label_0 = new Label();
			@class.label_0.Text = text;
			@class.label_0.Font = this.label_3.Font;
			@class.label_0.ForeColor = this.label_3.ForeColor;
			@class.label_0.BackColor = Color.Transparent;
			@class.label_0.AutoSize = true;
			@class.label_0.Cursor = Cursors.Hand;
			@class.label_0.Click += @class.method_3;
			this.label_3.Controls.Add(@class.label_0);
			@class.label_0.Location = new Point(this.label_3.Width, (this.label_3.Height - 15) / 2);
			@class.int_0 = 100;
			@class.bool_0 = true;
			System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
			timer.Interval = 15;
			timer.Tick += @class.method_4;
			timer.Start();
			if (File.Exists("programName.dat"))
			{
				this.Text = File.ReadAllText("programName.dat");
			}
			else
			{
				this.Text = "Default Program Name";
			}
			try
			{
				Image image = Image.FromFile(Path.Combine(Application.StartupPath, "Background.dat"));
				this.pictureBox_4.Image = image;
			}
			catch (Exception)
			{
			}
			this.gclass4_2.Enabled = false;
			this.button_5.Enabled = false;
			GForm12.bool_4 = true;
			GForm12.bool_5 = false;
			this.label_17.Text = "";
			this.gclass15_0.Checked = GForm12.bool_8;
			this.gclass15_0.Checked = true;
			new Thread(new ThreadStart(this.method_31))
			{
				IsBackground = true
			}.Start();
		}

		// Token: 0x06000264 RID: 612 RVA: 0x0000D0EF File Offset: 0x0000B2EF
		private void GForm12_FormClosing(object sender, FormClosingEventArgs e)
		{
			GForm12.bool_3 = true;
		}

		// Token: 0x06000265 RID: 613 RVA: 0x0000C303 File Offset: 0x0000A503
		private void method_39(object sender, EventArgs e)
		{
		}

		// Token: 0x06000266 RID: 614 RVA: 0x0000D0F7 File Offset: 0x0000B2F7
		private void button_5_Click(object sender, EventArgs e)
		{
			this.button_5.Enabled = false;
			this.gclass4_2.Enabled = false;
			GForm15.smethod_1("เริ่มทำงาน - MZATUNER", "กรุณาเปิดสวิตช์กุญแจรถ (ON) แล้วรอระบบเชื่อมต่อ", GEnum1.const_1);
			GForm12.bool_10 = true;
			this.Refresh();
		}

		// Token: 0x06000267 RID: 615 RVA: 0x0002B040 File Offset: 0x00029240
		private void method_40(Control control_0, string string_9)
		{
			if (control_0.InvokeRequired)
			{
				GForm12.Delegate0 method = new GForm12.Delegate0(this.method_40);
				control_0.Invoke(method, new object[]
				{
					control_0,
					string_9
				});
				return;
			}
			control_0.Text = string_9;
		}

		// Token: 0x06000268 RID: 616 RVA: 0x0002B080 File Offset: 0x00029280
		private void method_41(ComboBox comboBox_0, bool bool_15)
		{
			if (comboBox_0.InvokeRequired)
			{
				GForm12.Delegate2 method = new GForm12.Delegate2(this.method_41);
				comboBox_0.Invoke(method, new object[]
				{
					comboBox_0,
					bool_15
				});
				return;
			}
			comboBox_0.Enabled = bool_15;
		}

		// Token: 0x06000269 RID: 617 RVA: 0x0002B0C8 File Offset: 0x000292C8
		private void method_42(ProgressBar progressBar_0, int int_8)
		{
			if (progressBar_0.InvokeRequired)
			{
				GForm12.Delegate3 method = new GForm12.Delegate3(this.method_42);
				progressBar_0.Invoke(method, new object[]
				{
					progressBar_0,
					int_8
				});
				return;
			}
			progressBar_0.Value = int_8;
		}

		// Token: 0x0600026A RID: 618 RVA: 0x0002B110 File Offset: 0x00029310
		private void textBox_2_TextChanged(object sender, EventArgs e)
		{
			if (this.textBox_2.TextLength > 0)
			{
				this.button_5.Enabled = true;
				return;
			}
			if (GForm12.bool_6)
			{
				this.textBox_1.Text = "";
				this.textBox_0.Text = "";
			}
			this.button_5.Enabled = false;
		}

		// Token: 0x0600026B RID: 619 RVA: 0x0000D12D File Offset: 0x0000B32D
		private void gclass15_0_CheckedChanged(object sender, EventArgs e)
		{
			if (this.gclass15_0.CheckState == CheckState.Checked)
			{
				GForm12.bool_8 = true;
				return;
			}
			GForm12.bool_8 = false;
		}

		// Token: 0x0600026C RID: 620 RVA: 0x0002B16C File Offset: 0x0002936C
		private void button_8_Click(object sender, EventArgs e)
		{
			OpenFileDialog openFileDialog = new OpenFileDialog
			{
				Title = "Open bin file",
				Filter = "Bin file (*.bin)|*.bin"
			};
			if (openFileDialog.ShowDialog() == DialogResult.OK)
			{
				string fileName = openFileDialog.FileName;
				string fileName2 = Path.GetFileName(fileName);
				GForm12.byte_12 = File.ReadAllBytes(fileName);
				GForm12.int_2 = GForm12.byte_12.Length;
				double num = (double)GForm12.int_2 / 1024.0;
				this.textBox_2.Text = string.Format("{0} ({1:F2} KB)", fileName2, num);
				if (Path.GetExtension(fileName).ToLower() != ".bin")
				{
					MessageBox.Show("ไฟล์ที่เลือกไม่ใช่ .bin", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
					this.textBox_2.Text = "(กรุณาเลือกไฟล์ BIN) ..";
					GForm12.byte_12 = null;
					GForm12.int_2 = 0;
					return;
				}
				MessageBox.Show(string.Concat(new string[]
				{
					"ชื่อไฟล์ : ",
					fileName2,
					"\n",
					string.Format("ขนาดไฟล์ : {0:F2} KB\n\n", num),
					"คำเตือน !! โปรดตรวจสอบ ก่อนอัดไฟล์ อีกที เพื่อกันกล่องหลับหรือเสียหาย "
				}), "คำเตือนเกี่ยวกับไฟล์", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				if (GForm12.bool_6)
				{
					this.method_37();
					return;
				}
			}
			else
			{
				this.textBox_2.Text = "(กรุณาเลือกไฟล์ BIN) ..";
				GForm12.byte_12 = null;
				GForm12.int_2 = 0;
			}
		}

		// Token: 0x0600026D RID: 621 RVA: 0x0002B2AC File Offset: 0x000294AC
		private void textBox_1_TextChanged(object sender, EventArgs e)
		{
			if (this.textBox_1.TextLength > 0 && !this.textBox_1.Text.Contains("auto"))
			{
				int num;
				if (int.TryParse(this.textBox_1.Text, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out num))
				{
					GForm12.int_3 = num;
					return;
				}
			}
			else
			{
				GForm12.int_3 = 0;
			}
		}

		// Token: 0x0600026E RID: 622 RVA: 0x0002B30C File Offset: 0x0002950C
		private void textBox_0_TextChanged(object sender, EventArgs e)
		{
			if (this.textBox_0.TextLength > 0 && !this.textBox_0.Text.Contains("auto"))
			{
				int num;
				if (int.TryParse(this.textBox_0.Text, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out num))
				{
					GForm12.int_4 = num;
					return;
				}
			}
			else
			{
				GForm12.int_4 = 0;
			}
		}

		// Token: 0x0600026F RID: 623 RVA: 0x0000C305 File Offset: 0x0000A505
		private void pictureBox_1_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		// Token: 0x06000270 RID: 624 RVA: 0x0000D14A File Offset: 0x0000B34A
		private void button_7_Click(object sender, EventArgs e)
		{
			new GForm0().Show();
		}

		// Token: 0x06000271 RID: 625 RVA: 0x0000C305 File Offset: 0x0000A505
		private void method_43(object sender, EventArgs e)
		{
			base.Close();
		}

		// Token: 0x06000272 RID: 626 RVA: 0x0000D156 File Offset: 0x0000B356
		private void pictureBox_3_Click(object sender, EventArgs e)
		{
			Process.Start("https://www.facebook.com/profile.php?id=100086932872601");
		}

		// Token: 0x06000273 RID: 627 RVA: 0x0000D163 File Offset: 0x0000B363
		private void pictureBox_5_Click(object sender, EventArgs e)
		{
			Process.Start("https://www.facebook.com/juniel.pontongan/");
		}

		// Token: 0x06000275 RID: 629 RVA: 0x0002B36C File Offset: 0x0002956C
		private void method_44()
		{
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(GForm12));
			this.label_10 = new Label();
			this.label_9 = new Label();
			this.label_11 = new Label();
			this.label_6 = new Label();
			this.label_7 = new Label();
			this.label_8 = new Label();
			this.textBox_0 = new TextBox();
			this.textBox_1 = new TextBox();
			this.textBox_2 = new TextBox();
			this.pictureBox_1 = new PictureBox();
			this.label_12 = new Label();
			this.button_5 = new Button();
			this.pictureBox_2 = new PictureBox();
			this.label_13 = new Label();
			this.pictureBox_3 = new PictureBox();
			this.pictureBox_5 = new PictureBox();
			this.label_17 = new Label();
			this.label_14 = new Label();
			this.label_15 = new Label();
			this.label_16 = new Label();
			this.toolStripMenuItem_39 = new ToolStripMenuItem();
			this.toolStripMenuItem_41 = new ToolStripMenuItem();
			this.toolStripMenuItem_42 = new ToolStripMenuItem();
			this.toolStripMenuItem_43 = new ToolStripMenuItem();
			this.toolStripMenuItem_44 = new ToolStripMenuItem();
			this.toolStripMenuItem_40 = new ToolStripMenuItem();
			this.toolStripMenuItem_49 = new ToolStripMenuItem();
			this.toolStripMenuItem_53 = new ToolStripMenuItem();
			this.toolStripMenuItem_54 = new ToolStripMenuItem();
			this.toolStripMenuItem_56 = new ToolStripMenuItem();
			this.toolStripMenuItem_55 = new ToolStripMenuItem();
			this.toolStripMenuItem_48 = new ToolStripMenuItem();
			this.toolStripMenuItem_32 = new ToolStripMenuItem();
			this.toolStripMenuItem_33 = new ToolStripMenuItem();
			this.toolStripMenuItem_34 = new ToolStripMenuItem();
			this.toolStripMenuItem_35 = new ToolStripMenuItem();
			this.toolStripMenuItem_36 = new ToolStripMenuItem();
			this.toolStripMenuItem_38 = new ToolStripMenuItem();
			this.toolStripMenuItem_37 = new ToolStripMenuItem();
			this.toolStripMenuItem_45 = new ToolStripMenuItem();
			this.toolStripMenuItem_46 = new ToolStripMenuItem();
			this.toolStripMenuItem_47 = new ToolStripMenuItem();
			this.toolStripMenuItem_51 = new ToolStripMenuItem();
			this.toolStripMenuItem_52 = new ToolStripMenuItem();
			this.toolStripMenuItem_50 = new ToolStripMenuItem();
			this.button_7 = new Button();
			this.button_8 = new Button();
			this.pictureBox_4 = new PictureBox();
			this.groupBox_1 = new GroupBox();
			this.button_9 = new Button();
			this.button_1 = new Button();
			this.button_10 = new Button();
			this.button_11 = new Button();
			this.groupBox_2 = new GroupBox();
			this.label_2 = new Label();
			this.label_1 = new Label();
			this.groupBox_0 = new GroupBox();
			this.gclass15_1 = new GClass15();
			this.gclass15_0 = new GClass15();
			this.gclass4_0 = new GClass4();
			this.gclass4_2 = new GClass4();
			this.gclass4_1 = new GClass4();
			this.button_12 = new Button();
			this.gclass13_0 = new GClass13();
			this.button_2 = new Button();
			this.button_0 = new Button();
			this.toolStripMenuItem_0 = new ToolStripMenuItem();
			this.toolStripMenuItem_1 = new ToolStripMenuItem();
			this.toolStripMenuItem_2 = new ToolStripMenuItem();
			this.toolStripMenuItem_3 = new ToolStripMenuItem();
			this.toolStripMenuItem_4 = new ToolStripMenuItem();
			this.toolStripMenuItem_5 = new ToolStripMenuItem();
			this.toolStripMenuItem_6 = new ToolStripMenuItem();
			this.toolStripMenuItem_7 = new ToolStripMenuItem();
			this.toolStripMenuItem_8 = new ToolStripMenuItem();
			this.toolStripMenuItem_9 = new ToolStripMenuItem();
			this.toolStripMenuItem_10 = new ToolStripMenuItem();
			this.toolStripMenuItem_11 = new ToolStripMenuItem();
			this.toolStripMenuItem_12 = new ToolStripMenuItem();
			this.toolStripMenuItem_13 = new ToolStripMenuItem();
			this.toolStripMenuItem_14 = new ToolStripMenuItem();
			this.toolStripMenuItem_15 = new ToolStripMenuItem();
			this.toolStripMenuItem_16 = new ToolStripMenuItem();
			this.toolStripMenuItem_17 = new ToolStripMenuItem();
			this.toolStripMenuItem_18 = new ToolStripMenuItem();
			this.toolStripMenuItem_19 = new ToolStripMenuItem();
			this.toolStripMenuItem_20 = new ToolStripMenuItem();
			this.toolStripMenuItem_21 = new ToolStripMenuItem();
			this.toolStripMenuItem_22 = new ToolStripMenuItem();
			this.toolStripMenuItem_23 = new ToolStripMenuItem();
			this.toolStripMenuItem_24 = new ToolStripMenuItem();
			this.toolStripMenuItem_25 = new ToolStripMenuItem();
			this.toolStripMenuItem_26 = new ToolStripMenuItem();
			this.toolStripMenuItem_27 = new ToolStripMenuItem();
			this.toolStripMenuItem_28 = new ToolStripMenuItem();
			this.toolStripMenuItem_29 = new ToolStripMenuItem();
			this.toolStripMenuItem_30 = new ToolStripMenuItem();
			this.toolStripMenuItem_31 = new ToolStripMenuItem();
			this.menuStrip_0 = new MenuStrip();
			this.label_3 = new Label();
			this.panel_0 = new Panel();
			this.pictureBox_0 = new PictureBox();
			this.label_5 = new Label();
			this.button_3 = new Button();
			this.button_4 = new Button();
			this.label_4 = new Label();
			((ISupportInitialize)this.pictureBox_1).BeginInit();
			((ISupportInitialize)this.pictureBox_2).BeginInit();
			((ISupportInitialize)this.pictureBox_3).BeginInit();
			((ISupportInitialize)this.pictureBox_5).BeginInit();
			((ISupportInitialize)this.pictureBox_4).BeginInit();
			this.groupBox_1.SuspendLayout();
			this.groupBox_2.SuspendLayout();
			this.groupBox_0.SuspendLayout();
			this.menuStrip_0.SuspendLayout();
			this.panel_0.SuspendLayout();
			((ISupportInitialize)this.pictureBox_0).BeginInit();
			base.SuspendLayout();
			this.label_10.AutoSize = true;
			this.label_10.BackColor = Color.Transparent;
			this.label_10.Font = new Font("Microsoft New Tai Lue", 9.75f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.label_10.ForeColor = Color.Red;
			this.label_10.Location = new Point(134, 46);
			this.label_10.Name = "TxtEcmId";
			this.label_10.Size = new Size(13, 17);
			this.label_10.TabIndex = 6;
			this.label_10.Text = "-";
			this.label_10.Click += this.label_10_Click;
			this.label_9.BackColor = Color.Transparent;
			this.label_9.Font = new Font("Microsoft New Tai Lue", 9.75f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.label_9.ForeColor = Color.Red;
			this.label_9.Location = new Point(105, 153);
			this.label_9.Name = "TxtFlashCount";
			this.label_9.Size = new Size(105, 20);
			this.label_9.TabIndex = 8;
			this.label_9.Text = "-";
			this.label_9.TextAlign = ContentAlignment.MiddleLeft;
			this.label_9.Click += this.label_9_Click;
			this.label_11.AutoSize = true;
			this.label_11.BackColor = Color.Transparent;
			this.label_11.FlatStyle = FlatStyle.Flat;
			this.label_11.Font = new Font("Times New Roman", 12f, FontStyle.Bold);
			this.label_11.ForeColor = Color.Red;
			this.label_11.Location = new Point(149, 20);
			this.label_11.Name = "TxtConnStat";
			this.label_11.Size = new Size(14, 19);
			this.label_11.TabIndex = 32;
			this.label_11.Text = "-";
			this.label_11.Click += this.label_11_Click;
			this.label_6.AutoSize = true;
			this.label_6.Location = new Point(132, 418);
			this.label_6.Name = "LblConnection";
			this.label_6.Size = new Size(64, 13);
			this.label_6.TabIndex = 0;
			this.label_6.Text = "Connection:";
			this.label_7.BackColor = Color.Black;
			this.label_7.Font = new Font("Times New Roman", 12f, FontStyle.Bold);
			this.label_7.ForeColor = Color.White;
			this.label_7.Location = new Point(119, -3);
			this.label_7.Name = "LblChecksumOffset";
			this.label_7.Size = new Size(100, 15);
			this.label_7.TabIndex = 9;
			this.label_7.Text = "เช็คซั่มออฟเซ็ค";
			this.label_7.TextAlign = ContentAlignment.MiddleCenter;
			this.label_7.Click += this.label_7_Click;
			this.label_8.BackColor = Color.Black;
			this.label_8.Font = new Font("Times New Roman", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.label_8.ForeColor = Color.White;
			this.label_8.Location = new Point(15, -3);
			this.label_8.Name = "LblStartOffset";
			this.label_8.Size = new Size(88, 19);
			this.label_8.TabIndex = 8;
			this.label_8.Text = "สตาร์ทออฟเซ็ต";
			this.textBox_0.Font = new Font("Microsoft Sans Serif", 11.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.textBox_0.Location = new Point(117, 19);
			this.textBox_0.Name = "TbChecksumOffset";
			this.textBox_0.ReadOnly = true;
			this.textBox_0.Size = new Size(107, 24);
			this.textBox_0.TabIndex = 5;
			this.textBox_0.Text = "-";
			this.textBox_0.TextAlign = HorizontalAlignment.Center;
			this.textBox_0.TextChanged += this.textBox_0_TextChanged;
			this.textBox_1.Font = new Font("Microsoft Sans Serif", 11.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.textBox_1.Location = new Point(5, 19);
			this.textBox_1.Name = "TbStartOffset";
			this.textBox_1.ReadOnly = true;
			this.textBox_1.Size = new Size(107, 24);
			this.textBox_1.TabIndex = 4;
			this.textBox_1.Text = "-";
			this.textBox_1.TextAlign = HorizontalAlignment.Center;
			this.textBox_1.TextChanged += this.textBox_1_TextChanged;
			this.textBox_2.BackColor = Color.DimGray;
			this.textBox_2.BorderStyle = BorderStyle.None;
			this.textBox_2.Font = new Font("Microsoft Tai Le", 9f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.textBox_2.ForeColor = Color.White;
			this.textBox_2.Location = new Point(6, 52);
			this.textBox_2.Multiline = true;
			this.textBox_2.Name = "TbFileName";
			this.textBox_2.ReadOnly = true;
			this.textBox_2.Size = new Size(453, 19);
			this.textBox_2.TabIndex = 0;
			this.textBox_2.Text = "กรุณาเลือกไฟล์ BIN ..";
			this.textBox_2.TextChanged += this.textBox_2_TextChanged;
			this.pictureBox_1.Location = new Point(800, 800);
			this.pictureBox_1.Margin = new Padding(2);
			this.pictureBox_1.Name = "pictureBox2";
			this.pictureBox_1.Size = new Size(98, 90);
			this.pictureBox_1.SizeMode = PictureBoxSizeMode.StretchImage;
			this.pictureBox_1.TabIndex = 30;
			this.pictureBox_1.TabStop = false;
			this.pictureBox_1.Click += this.pictureBox_1_Click;
			this.label_12.AccessibleRole = AccessibleRole.Text;
			this.label_12.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this.label_12.BackColor = Color.Black;
			this.label_12.FlatStyle = FlatStyle.System;
			this.label_12.Font = new Font("Segoe UI Black", 18f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.label_12.ForeColor = Color.White;
			this.label_12.ImageAlign = ContentAlignment.MiddleLeft;
			this.label_12.Location = new Point(5, 14);
			this.label_12.Margin = new Padding(2, 0, 2, 2);
			this.label_12.Name = "TxtPartCode";
			this.label_12.Size = new Size(591, 34);
			this.label_12.TabIndex = 8;
			this.label_12.Text = "-";
			this.label_12.TextAlign = ContentAlignment.BottomCenter;
			this.label_12.Click += this.label_12_Click;
			this.button_5.BackColor = Color.Transparent;
			this.button_5.BackgroundImageLayout = ImageLayout.Stretch;
			this.button_5.FlatStyle = FlatStyle.Flat;
			this.button_5.Font = new Font("Microsoft YaHei", 15.75f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.button_5.ForeColor = Color.Fuchsia;
			this.button_5.Location = new Point(19, 673);
			this.button_5.Name = "BtnWrite";
			this.button_5.Size = new Size(447, 44);
			this.button_5.TabIndex = 2;
			this.button_5.Text = "อัดไฟล์ลงกล่อง ECU";
			this.button_5.UseVisualStyleBackColor = false;
			this.button_5.Click += this.button_5_Click;
			this.pictureBox_2.Location = new Point(800, 800);
			this.pictureBox_2.Margin = new Padding(2);
			this.pictureBox_2.Name = "pictureBox5";
			this.pictureBox_2.Size = new Size(100, 61);
			this.pictureBox_2.SizeMode = PictureBoxSizeMode.StretchImage;
			this.pictureBox_2.TabIndex = 39;
			this.pictureBox_2.TabStop = false;
			this.label_13.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.label_13.ForeColor = Color.Red;
			this.label_13.Location = new Point(800, 800);
			this.label_13.Margin = new Padding(2, 0, 2, 0);
			this.label_13.Name = "label1";
			this.label_13.Size = new Size(178, 18);
			this.label_13.TabIndex = 40;
			this.label_13.Text = "HONDA PART CODE";
			this.pictureBox_3.BackColor = Color.Transparent;
			this.pictureBox_3.Location = new Point(800, 800);
			this.pictureBox_3.Margin = new Padding(2);
			this.pictureBox_3.Name = "pictureBox6";
			this.pictureBox_3.Size = new Size(101, 41);
			this.pictureBox_3.SizeMode = PictureBoxSizeMode.StretchImage;
			this.pictureBox_3.TabIndex = 41;
			this.pictureBox_3.TabStop = false;
			this.pictureBox_3.Click += this.pictureBox_3_Click;
			this.pictureBox_5.Location = new Point(800, 800);
			this.pictureBox_5.Margin = new Padding(2);
			this.pictureBox_5.Name = "pictureBox7";
			this.pictureBox_5.Size = new Size(47, 41);
			this.pictureBox_5.SizeMode = PictureBoxSizeMode.StretchImage;
			this.pictureBox_5.TabIndex = 42;
			this.pictureBox_5.TabStop = false;
			this.pictureBox_5.Click += this.pictureBox_5_Click;
			this.label_17.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this.label_17.BackColor = Color.Transparent;
			this.label_17.Font = new Font("Segoe UI", 9.75f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.label_17.ForeColor = Color.Red;
			this.label_17.Location = new Point(629, 282);
			this.label_17.Name = "TxtPb";
			this.label_17.Size = new Size(602, 25);
			this.label_17.TabIndex = 8;
			this.label_17.Text = "-";
			this.label_17.TextAlign = ContentAlignment.MiddleCenter;
			this.label_17.Click += this.label_17_Click;
			this.label_14.AutoSize = true;
			this.label_14.Font = new Font("Times New Roman", 12f, FontStyle.Bold);
			this.label_14.ForeColor = Color.White;
			this.label_14.Location = new Point(35, 20);
			this.label_14.Name = "label2";
			this.label_14.Size = new Size(115, 19);
			this.label_14.TabIndex = 45;
			this.label_14.Text = "สถานะการชื่อกุญแจ :";
			this.label_14.Click += this.label_14_Click;
			this.label_15.AutoSize = true;
			this.label_15.BackColor = Color.Transparent;
			this.label_15.Font = new Font("Times New Roman", 12f, FontStyle.Bold);
			this.label_15.ForeColor = Color.White;
			this.label_15.Location = new Point(6, 153);
			this.label_15.Name = "label3";
			this.label_15.Size = new Size(104, 19);
			this.label_15.TabIndex = 46;
			this.label_15.Text = "\ud83d\udee1จำนวนการอัด : ";
			this.label_15.Click += this.label_15_Click;
			this.label_16.AutoSize = true;
			this.label_16.Font = new Font("Times New Roman", 12f, FontStyle.Bold);
			this.label_16.ForeColor = Color.White;
			this.label_16.Location = new Point(18, 43);
			this.label_16.Name = "label4";
			this.label_16.Size = new Size(118, 19);
			this.label_16.TabIndex = 47;
			this.label_16.Text = "\ud83d\udcc8 : ไอดีกล่องอีซียู : ";
			this.label_16.Click += this.label_16_Click;
			this.toolStripMenuItem_39.BackColor = Color.Black;
			this.toolStripMenuItem_39.DropDownItems.AddRange(new ToolStripItem[]
			{
				this.toolStripMenuItem_41,
				this.toolStripMenuItem_42,
				this.toolStripMenuItem_43,
				this.toolStripMenuItem_44,
				this.toolStripMenuItem_40,
				this.toolStripMenuItem_49,
				this.toolStripMenuItem_53,
				this.toolStripMenuItem_54,
				this.toolStripMenuItem_56,
				this.toolStripMenuItem_55,
				this.toolStripMenuItem_48
			});
			this.toolStripMenuItem_39.ForeColor = Color.White;
			this.toolStripMenuItem_39.Name = "ไฟลToolStripMenuItem";
			this.toolStripMenuItem_39.Size = new Size(58, 24);
			this.toolStripMenuItem_39.Text = "ไฟล์";
			this.toolStripMenuItem_39.Click += this.toolStripMenuItem_39_Click;
			this.toolStripMenuItem_41.Name = "เปลยนชอโปรแกรมToolStripMenuItem";
			this.toolStripMenuItem_41.Size = new Size(171, 22);
			this.toolStripMenuItem_42.Name = "เปลยนรปToolStripMenuItem1";
			this.toolStripMenuItem_42.Size = new Size(171, 22);
			this.toolStripMenuItem_43.Name = "ปลดรหสXDFADXToolStripMenuItem";
			this.toolStripMenuItem_43.Size = new Size(171, 22);
			this.toolStripMenuItem_44.Name = "แปลงไฟลECUACGToolStripMenuItem";
			this.toolStripMenuItem_44.Size = new Size(171, 22);
			this.toolStripMenuItem_40.BackColor = SystemColors.ActiveCaptionText;
			this.toolStripMenuItem_40.ForeColor = Color.White;
			this.toolStripMenuItem_40.Name = "เลอกไฟลแฟชรToolStripMenuItem";
			this.toolStripMenuItem_40.Size = new Size(171, 22);
			this.toolStripMenuItem_40.Text = "เลือกไฟล์ Bin";
			this.toolStripMenuItem_40.Click += this.toolStripMenuItem_40_Click;
			this.toolStripMenuItem_49.Name = "ตดตงไดรเวอรToolStripMenuItem1";
			this.toolStripMenuItem_49.Size = new Size(171, 22);
			this.toolStripMenuItem_53.Name = "รสตารทโปรแกรมToolStripMenuItem";
			this.toolStripMenuItem_53.Size = new Size(171, 22);
			this.toolStripMenuItem_54.BackColor = SystemColors.ActiveCaptionText;
			this.toolStripMenuItem_54.ForeColor = Color.White;
			this.toolStripMenuItem_54.Name = "อพเดทรหสกลองลาสดToolStripMenuItem";
			this.toolStripMenuItem_54.Size = new Size(171, 22);
			this.toolStripMenuItem_54.Text = "อัพเดทรหัสกล่องล่าสุด";
			this.toolStripMenuItem_54.Click += this.toolStripMenuItem_54_Click;
			this.toolStripMenuItem_56.Name = "ไฟลเดมโรงงานToolStripMenuItem";
			this.toolStripMenuItem_56.Size = new Size(171, 22);
			this.toolStripMenuItem_55.Name = "ไฟลโมสำหรบมอใหมToolStripMenuItem";
			this.toolStripMenuItem_55.Size = new Size(171, 22);
			this.toolStripMenuItem_48.Name = "toolStripMenuItem1";
			this.toolStripMenuItem_48.Size = new Size(171, 22);
			this.toolStripMenuItem_32.Name = "ลบออโตToolStripMenuItem";
			this.toolStripMenuItem_32.Size = new Size(32, 19);
			this.toolStripMenuItem_33.Name = "ลบออโตToolStripMenuItem1";
			this.toolStripMenuItem_33.Size = new Size(32, 19);
			this.toolStripMenuItem_34.BackColor = SystemColors.ActiveCaptionText;
			this.toolStripMenuItem_34.ForeColor = Color.White;
			this.toolStripMenuItem_34.Name = "รเซตกลองToolStripMenuItem";
			this.toolStripMenuItem_34.Size = new Size(199, 22);
			this.toolStripMenuItem_34.Text = "รีเช็ตกล่อง";
			this.toolStripMenuItem_34.Click += this.toolStripMenuItem_34_Click;
			this.toolStripMenuItem_35.BackColor = SystemColors.ActiveCaptionText;
			this.toolStripMenuItem_35.ForeColor = Color.White;
			this.toolStripMenuItem_35.Name = "ลบโคตToolStripMenuItem";
			this.toolStripMenuItem_35.Size = new Size(199, 22);
			this.toolStripMenuItem_35.Text = "ลบโค็ต";
			this.toolStripMenuItem_35.Click += this.toolStripMenuItem_35_Click;
			this.toolStripMenuItem_36.BackColor = Color.Black;
			this.toolStripMenuItem_36.ForeColor = Color.White;
			this.toolStripMenuItem_36.Name = "ลบแฟลชเคาทShindengenToolStripMenuItem";
			this.toolStripMenuItem_36.Size = new Size(199, 22);
			this.toolStripMenuItem_36.Text = "ลบแฟลชเคาท์Shindengen";
			this.toolStripMenuItem_36.Click += this.toolStripMenuItem_36_Click;
			this.toolStripMenuItem_38.Name = "ดดดาตาToolStripMenuItem";
			this.toolStripMenuItem_38.Size = new Size(32, 19);
			this.toolStripMenuItem_37.Name = "จนโปรToolStripMenuItem";
			this.toolStripMenuItem_37.Size = new Size(32, 19);
			this.toolStripMenuItem_45.Name = "เปดจนโปรToolStripMenuItem";
			this.toolStripMenuItem_45.Size = new Size(32, 19);
			this.toolStripMenuItem_46.Name = "ลงทะเบยนToolStripMenuItem";
			this.toolStripMenuItem_46.Size = new Size(32, 19);
			this.toolStripMenuItem_47.Name = "อพเดทจนโปรToolStripMenuItem";
			this.toolStripMenuItem_47.Size = new Size(32, 19);
			this.toolStripMenuItem_51.Name = "ลอคฝงชนToolStripMenuItem";
			this.toolStripMenuItem_51.Size = new Size(32, 19);
			this.toolStripMenuItem_52.Name = "เพมรหสกลองToolStripMenuItem";
			this.toolStripMenuItem_52.Size = new Size(32, 19);
			this.toolStripMenuItem_50.Name = "อพเดทโปรแกรมToolStripMenuItem";
			this.toolStripMenuItem_50.Size = new Size(32, 19);
			this.button_7.BackColor = Color.White;
			this.button_7.FlatAppearance.BorderColor = Color.White;
			this.button_7.Font = new Font("Microsoft Sans Serif", 7.8f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.button_7.ForeColor = Color.Black;
			this.button_7.Location = new Point(615, 673);
			this.button_7.Margin = new Padding(2);
			this.button_7.Name = "button2";
			this.button_7.Size = new Size(137, 36);
			this.button_7.TabIndex = 35;
			this.button_7.Text = "ACG/ECU TO BIN";
			this.button_7.UseVisualStyleBackColor = false;
			this.button_7.Click += this.button_7_Click;
			this.button_8.BackColor = Color.Transparent;
			this.button_8.BackgroundImageLayout = ImageLayout.Stretch;
			this.button_8.FlatStyle = FlatStyle.Flat;
			this.button_8.Font = new Font("Microsoft YaHei", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.button_8.ForeColor = Color.Fuchsia;
			this.button_8.Location = new Point(771, 669);
			this.button_8.Name = "button1";
			this.button_8.Size = new Size(103, 57);
			this.button_8.TabIndex = 10;
			this.button_8.Text = "เลือกไฟล์";
			this.button_8.UseVisualStyleBackColor = false;
			this.button_8.Click += this.button_8_Click;
			this.pictureBox_4.Cursor = Cursors.No;
			this.pictureBox_4.Location = new Point(7, 60);
			this.pictureBox_4.Name = "pictureBox1";
			this.pictureBox_4.Size = new Size(602, 261);
			this.pictureBox_4.SizeMode = PictureBoxSizeMode.StretchImage;
			this.pictureBox_4.TabIndex = 53;
			this.pictureBox_4.TabStop = false;
			this.pictureBox_4.Click += this.pictureBox_4_Click;
			this.groupBox_1.Controls.Add(this.label_12);
			this.groupBox_1.Location = new Point(7, 321);
			this.groupBox_1.Name = "groupBox3";
			this.groupBox_1.Size = new Size(602, 60);
			this.groupBox_1.TabIndex = 59;
			this.groupBox_1.TabStop = false;
			this.button_9.BackColor = Color.Transparent;
			this.button_9.BackgroundImageLayout = ImageLayout.Stretch;
			this.button_9.FlatStyle = FlatStyle.Flat;
			this.button_9.Font = new Font("Microsoft YaHei", 9f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.button_9.ForeColor = Color.Fuchsia;
			this.button_9.Location = new Point(1110, 62);
			this.button_9.Name = "button11";
			this.button_9.Size = new Size(198, 26);
			this.button_9.TabIndex = 63;
			this.button_9.Text = "➕ เพิ่มรหัสกล่องโปรแกรม";
			this.button_9.UseVisualStyleBackColor = false;
			this.button_9.Click += this.button_9_Click;
			this.button_1.BackColor = Color.Transparent;
			this.button_1.BackgroundImageLayout = ImageLayout.Stretch;
			this.button_1.FlatStyle = FlatStyle.Flat;
			this.button_1.Font = new Font("Microsoft YaHei", 9f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.button_1.ForeColor = Color.Fuchsia;
			this.button_1.Location = new Point(685, 592);
			this.button_1.Name = "button28";
			this.button_1.Size = new Size(199, 26);
			this.button_1.TabIndex = 69;
			this.button_1.Text = "⚙️ ดูดไฟล์รถเกียร์ 48 - 64";
			this.button_1.UseVisualStyleBackColor = false;
			this.button_1.Click += this.button_1_Click;
			this.button_10.BackColor = Color.Transparent;
			this.button_10.BackgroundImageLayout = ImageLayout.Stretch;
			this.button_10.FlatStyle = FlatStyle.Flat;
			this.button_10.Font = new Font("Microsoft YaHei", 9f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.button_10.ForeColor = Color.Fuchsia;
			this.button_10.Location = new Point(686, 560);
			this.button_10.Name = "button15";
			this.button_10.Size = new Size(198, 26);
			this.button_10.TabIndex = 67;
			this.button_10.Text = "⚙️ ลบจำนวนการอัดรถเกียร์";
			this.button_10.UseVisualStyleBackColor = false;
			this.button_10.Click += this.button_10_Click;
			this.button_11.BackColor = Color.Transparent;
			this.button_11.BackgroundImageLayout = ImageLayout.Stretch;
			this.button_11.FlatStyle = FlatStyle.Flat;
			this.button_11.Font = new Font("Microsoft YaHei", 9f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.button_11.ForeColor = Color.Fuchsia;
			this.button_11.Location = new Point(868, 673);
			this.button_11.Name = "button21";
			this.button_11.Size = new Size(180, 26);
			this.button_11.TabIndex = 68;
			this.button_11.Text = "น้ำมัน TPS 0";
			this.button_11.UseVisualStyleBackColor = false;
			this.groupBox_2.Controls.Add(this.label_9);
			this.groupBox_2.Controls.Add(this.label_2);
			this.groupBox_2.Controls.Add(this.label_1);
			this.groupBox_2.Controls.Add(this.groupBox_0);
			this.groupBox_2.Controls.Add(this.gclass4_0);
			this.groupBox_2.Controls.Add(this.label_15);
			this.groupBox_2.Controls.Add(this.gclass4_2);
			this.groupBox_2.Controls.Add(this.gclass4_1);
			this.groupBox_2.Controls.Add(this.button_12);
			this.groupBox_2.Controls.Add(this.textBox_2);
			this.groupBox_2.Controls.Add(this.gclass13_0);
			this.groupBox_2.Controls.Add(this.label_14);
			this.groupBox_2.Controls.Add(this.label_11);
			this.groupBox_2.Location = new Point(7, 380);
			this.groupBox_2.Name = "groupBox9";
			this.groupBox_2.Size = new Size(602, 216);
			this.groupBox_2.TabIndex = 62;
			this.groupBox_2.TabStop = false;
			this.groupBox_2.Enter += this.groupBox_2_Enter;
			this.label_2.BackColor = Color.Transparent;
			this.label_2.Font = new Font("Microsoft New Tai Lue", 9.75f, FontStyle.Bold);
			this.label_2.ForeColor = Color.Yellow;
			this.label_2.Location = new Point(351, 150);
			this.label_2.Name = "TxtBatteryVolt";
			this.label_2.Size = new Size(52, 23);
			this.label_2.TabIndex = 22;
			this.label_2.Text = "0.0 V";
			this.label_2.TextAlign = ContentAlignment.MiddleRight;
			this.label_2.Click += this.label_2_Click;
			this.label_1.AutoSize = true;
			this.label_1.Font = new Font("Times New Roman", 12f, FontStyle.Bold);
			this.label_1.ForeColor = Color.White;
			this.label_1.Location = new Point(273, 153);
			this.label_1.Name = "label_batTitle";
			this.label_1.Size = new Size(82, 19);
			this.label_1.TabIndex = 70;
			this.label_1.Text = "\ud83d\udd0bแบตเตอรรี่:";
			this.groupBox_0.BackColor = Color.Transparent;
			this.groupBox_0.Controls.Add(this.label_16);
			this.groupBox_0.Controls.Add(this.gclass15_1);
			this.groupBox_0.Controls.Add(this.label_10);
			this.groupBox_0.Controls.Add(this.gclass15_0);
			this.groupBox_0.Controls.Add(this.label_8);
			this.groupBox_0.Controls.Add(this.textBox_1);
			this.groupBox_0.Controls.Add(this.label_7);
			this.groupBox_0.Controls.Add(this.textBox_0);
			this.groupBox_0.ForeColor = Color.Transparent;
			this.groupBox_0.Location = new Point(6, 77);
			this.groupBox_0.Name = "groupBox1";
			this.groupBox_0.Size = new Size(395, 67);
			this.groupBox_0.TabIndex = 54;
			this.groupBox_0.TabStop = false;
			this.groupBox_0.Enter += this.groupBox_0_Enter;
			this.gclass15_1.Cursor = Cursors.Hand;
			this.gclass15_1.Font = new Font("Times New Roman", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.gclass15_1.ForeColor = Color.White;
			this.gclass15_1.Location = new Point(230, 36);
			this.gclass15_1.Name = "checkBox1";
			this.gclass15_1.method_3(Color.FromArgb(100, 100, 100));
			this.gclass15_1.method_1(Color.FromArgb(0, 192, 0));
			this.gclass15_1.Size = new Size(160, 25);
			this.gclass15_1.TabIndex = 54;
			this.gclass15_1.Text = "ออฟเซ็ตแบบแมนนวล";
			this.gclass15_1.UseVisualStyleBackColor = true;
			this.gclass15_1.CheckedChanged += this.gclass15_1_CheckedChanged;
			this.gclass15_0.Cursor = Cursors.Hand;
			this.gclass15_0.Font = new Font("Times New Roman", 8.25f, FontStyle.Bold);
			this.gclass15_0.ForeColor = Color.White;
			this.gclass15_0.Location = new Point(230, 13);
			this.gclass15_0.Name = "CbFastWrite";
			this.gclass15_0.method_3(Color.FromArgb(100, 100, 100));
			this.gclass15_0.method_1(Color.FromArgb(0, 192, 0));
			this.gclass15_0.Size = new Size(160, 25);
			this.gclass15_0.TabIndex = 28;
			this.gclass15_0.Text = "เขียนไฟล์เร็ว";
			this.gclass15_0.UseVisualStyleBackColor = true;
			this.gclass15_0.CheckedChanged += this.gclass15_0_CheckedChanged;
			this.gclass4_0.BackColor = Color.Black;
			this.gclass4_0.BackgroundImageLayout = ImageLayout.Stretch;
			this.gclass4_0.method_3(8);
			this.gclass4_0.Cursor = Cursors.Hand;
			this.gclass4_0.FlatStyle = FlatStyle.Flat;
			this.gclass4_0.Font = new Font("Microsoft YaHei", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.gclass4_0.ForeColor = Color.Red;
			this.gclass4_0.method_5(false);
			this.gclass4_0.Location = new Point(326, 12);
			this.gclass4_0.method_1(GEnum0.const_1);
			this.gclass4_0.Name = "button12";
			this.gclass4_0.Size = new Size(133, 37);
			this.gclass4_0.TabIndex = 64;
			this.gclass4_0.Text = "รีสตาร์ทโปรแกรม";
			this.gclass4_0.UseVisualStyleBackColor = false;
			this.gclass4_0.Click += this.gclass4_0_Click;
			this.gclass4_2.BackColor = Color.Black;
			this.gclass4_2.BackgroundImageLayout = ImageLayout.Stretch;
			this.gclass4_2.method_3(8);
			this.gclass4_2.Cursor = Cursors.Hand;
			this.gclass4_2.FlatStyle = FlatStyle.Flat;
			this.gclass4_2.Font = new Font("Microsoft YaHei", 20.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.gclass4_2.ForeColor = Color.Red;
			this.gclass4_2.method_5(false);
			this.gclass4_2.Location = new Point(407, 77);
			this.gclass4_2.method_1(GEnum0.const_3);
			this.gclass4_2.Name = "button20";
			this.gclass4_2.Size = new Size(189, 97);
			this.gclass4_2.TabIndex = 68;
			this.gclass4_2.Text = "อัดไฟล์";
			this.gclass4_2.UseVisualStyleBackColor = false;
			this.gclass4_2.Click += this.gclass4_2_Click;
			this.gclass4_1.BackColor = Color.Black;
			this.gclass4_1.BackgroundImageLayout = ImageLayout.Stretch;
			this.gclass4_1.method_3(8);
			this.gclass4_1.Cursor = Cursors.Hand;
			this.gclass4_1.FlatStyle = FlatStyle.Flat;
			this.gclass4_1.Font = new Font("Microsoft YaHei", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.gclass4_1.ForeColor = Color.Red;
			this.gclass4_1.method_5(false);
			this.gclass4_1.Location = new Point(465, 12);
			this.gclass4_1.method_1(GEnum0.const_2);
			this.gclass4_1.Name = "button19";
			this.gclass4_1.Size = new Size(131, 60);
			this.gclass4_1.TabIndex = 68;
			this.gclass4_1.Text = "เลือกไฟล์";
			this.gclass4_1.UseVisualStyleBackColor = false;
			this.gclass4_1.Click += this.gclass4_1_Click;
			this.button_12.BackColor = Color.Transparent;
			this.button_12.BackgroundImageLayout = ImageLayout.Stretch;
			this.button_12.FlatStyle = FlatStyle.Flat;
			this.button_12.Font = new Font("Microsoft YaHei", 9.75f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.button_12.ForeColor = Color.Red;
			this.button_12.Location = new Point(800, 800);
			this.button_12.Name = "button26";
			this.button_12.Size = new Size(103, 31);
			this.button_12.TabIndex = 69;
			this.button_12.Text = "\ud83d\udd04 Resset";
			this.button_12.UseVisualStyleBackColor = false;
			this.button_12.Visible = false;
			this.button_12.Click += this.button_12_Click;
			this.gclass13_0.AccessibleRole = AccessibleRole.ProgressBar;
			this.gclass13_0.BackColor = Color.Black;
			this.gclass13_0.Cursor = Cursors.Default;
			this.gclass13_0.ForeColor = Color.Red;
			this.gclass13_0.Location = new Point(6, 180);
			this.gclass13_0.Maximum = 10000;
			this.gclass13_0.Name = "PbProgress";
			this.gclass13_0.Size = new Size(590, 27);
			this.gclass13_0.Step = 1;
			this.gclass13_0.Style = ProgressBarStyle.Continuous;
			this.gclass13_0.TabIndex = 10;
			this.gclass13_0.Click += this.gclass13_0_Click;
			this.button_2.BackColor = Color.Red;
			this.button_2.Cursor = Cursors.Default;
			this.button_2.FlatAppearance.BorderSize = 0;
			this.button_2.FlatStyle = FlatStyle.Flat;
			this.button_2.Location = new Point(593, 40);
			this.button_2.Name = "sx1";
			this.button_2.Size = new Size(14, 12);
			this.button_2.TabIndex = 20;
			this.button_2.UseVisualStyleBackColor = false;
			this.button_2.Click += this.button_2_Click;
			this.button_2.Paint += this.button_2_Paint;
			this.button_0.BackColor = Color.Transparent;
			this.button_0.BackgroundImageLayout = ImageLayout.Stretch;
			this.button_0.FlatStyle = FlatStyle.Flat;
			this.button_0.Font = new Font("Microsoft YaHei", 9f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.button_0.ForeColor = Color.Fuchsia;
			this.button_0.Location = new Point(14, 752);
			this.button_0.Name = "button22";
			this.button_0.Size = new Size(180, 26);
			this.button_0.TabIndex = 69;
			this.button_0.Text = "ดูดไฟล์ 48-64 KB";
			this.button_0.UseVisualStyleBackColor = false;
			this.toolStripMenuItem_0.DropDownItems.AddRange(new ToolStripItem[]
			{
				this.toolStripMenuItem_1,
				this.toolStripMenuItem_2,
				this.toolStripMenuItem_3,
				this.toolStripMenuItem_4,
				this.toolStripMenuItem_5,
				this.toolStripMenuItem_6,
				this.toolStripMenuItem_7,
				this.toolStripMenuItem_8,
				this.toolStripMenuItem_9,
				this.toolStripMenuItem_10,
				this.toolStripMenuItem_11,
				this.toolStripMenuItem_12,
				this.toolStripMenuItem_13,
				this.toolStripMenuItem_14
			});
			this.toolStripMenuItem_0.Font = new Font("Segoe UI", 9f);
			this.toolStripMenuItem_0.ForeColor = Color.White;
			this.toolStripMenuItem_0.Name = "ไฟลToolStripMenuItem1";
			this.toolStripMenuItem_0.Size = new Size(53, 20);
			this.toolStripMenuItem_0.Text = "\ud83d\uddc2️ ไฟล์";
			this.toolStripMenuItem_1.Name = "เกยวกบโปรแกรมToolStripMenuItem";
			this.toolStripMenuItem_1.Size = new Size(214, 22);
			this.toolStripMenuItem_1.Text = "ℹ️ เกี่ยวกับโปรแกรม";
			this.toolStripMenuItem_1.Click += this.toolStripMenuItem_1_Click;
			this.toolStripMenuItem_2.Name = "ตดตอสอบถามปญหาToolStripMenuItem";
			this.toolStripMenuItem_2.Size = new Size(214, 22);
			this.toolStripMenuItem_2.Text = "\ud83d\udcde ติดต่อสอบถาม/ปัญหา";
			this.toolStripMenuItem_2.Click += this.toolStripMenuItem_2_Click;
			this.toolStripMenuItem_3.Name = "อพเดทตรวจสอบเวอรชนToolStripMenuItem";
			this.toolStripMenuItem_3.Size = new Size(214, 22);
			this.toolStripMenuItem_3.Text = "\ud83d\udd04 อัพเดท/ตรวจสอบเวอร์ชั่น";
			this.toolStripMenuItem_3.Click += this.toolStripMenuItem_3_Click;
			this.toolStripMenuItem_4.Name = "เปลยนสโปรแกรมToolStripMenuItem";
			this.toolStripMenuItem_4.Size = new Size(214, 22);
			this.toolStripMenuItem_4.Text = "\ud83c\udfa8 เปลี่ยนสีโปรแกรม";
			this.toolStripMenuItem_4.Click += this.toolStripMenuItem_4_Click;
			this.toolStripMenuItem_5.Name = "ปรบเปลยนแตงโลโกToolStripMenuItem";
			this.toolStripMenuItem_5.Size = new Size(214, 22);
			this.toolStripMenuItem_5.Text = "\ud83d\uddbc️ ปรับเปลี่ยนแต่งโลโก้";
			this.toolStripMenuItem_5.Click += this.toolStripMenuItem_5_Click;
			this.toolStripMenuItem_6.Name = "ตงคาชอโปรแกรมToolStripMenuItem";
			this.toolStripMenuItem_6.Size = new Size(214, 22);
			this.toolStripMenuItem_6.Text = "⚙️ ตั้งค่าชื่อโปรแกรม";
			this.toolStripMenuItem_6.Click += this.toolStripMenuItem_6_Click;
			this.toolStripMenuItem_7.Name = "ตดตงไดรเวอรFTDIToolStripMenuItem";
			this.toolStripMenuItem_7.Size = new Size(214, 22);
			this.toolStripMenuItem_7.Text = "\ud83d\udd0c ติดตั้งไดรเวอร์ (FTDI)";
			this.toolStripMenuItem_7.Click += this.toolStripMenuItem_7_Click;
			this.toolStripMenuItem_8.Name = "เปดโปรแกรมTunerProRTToolStripMenuItem";
			this.toolStripMenuItem_8.Size = new Size(214, 22);
			this.toolStripMenuItem_8.Text = "\ud83d\ude80 เปิดโปรแกรม TunerPro RT";
			this.toolStripMenuItem_8.Click += this.toolStripMenuItem_8_Click;
			this.toolStripMenuItem_9.Name = "ลงทะเบยนTunerProToolStripMenuItem";
			this.toolStripMenuItem_9.Size = new Size(214, 22);
			this.toolStripMenuItem_9.Text = "\ud83d\udd11 ลงทะเบียน TunerPro";
			this.toolStripMenuItem_9.Click += this.toolStripMenuItem_9_Click;
			this.toolStripMenuItem_10.Name = "ตดตงTunerProRTToolStripMenuItem";
			this.toolStripMenuItem_10.Size = new Size(214, 22);
			this.toolStripMenuItem_10.Text = "\ud83d\udce5 ติดตั้ง TunerPro RT";
			this.toolStripMenuItem_10.Click += this.toolStripMenuItem_10_Click;
			this.toolStripMenuItem_11.Name = "ตดตงTunerProRTTHToolStripMenuItem";
			this.toolStripMenuItem_11.Size = new Size(214, 22);
			this.toolStripMenuItem_11.Text = "\ud83d\udce5 ติดตั้ง  TunerPro RT TH";
			this.toolStripMenuItem_11.Click += this.toolStripMenuItem_11_Click;
			this.toolStripMenuItem_12.Name = "แปลงไฟลECUACGToolStripMenuItem1";
			this.toolStripMenuItem_12.Size = new Size(214, 22);
			this.toolStripMenuItem_12.Text = "\ud83e\uddec แปลงไฟล์ ECU - ACG";
			this.toolStripMenuItem_12.Click += this.toolStripMenuItem_12_Click;
			this.toolStripMenuItem_13.Name = "ปลดรหสXDFADXToolStripMenuItem1";
			this.toolStripMenuItem_13.Size = new Size(214, 22);
			this.toolStripMenuItem_13.Text = "\ud83d\udd13 ปลดรหัส XDF - ADX";
			this.toolStripMenuItem_13.Click += this.toolStripMenuItem_13_Click;
			this.toolStripMenuItem_14.Name = "เพมรหสกลองโปรแกรมToolStripMenuItem";
			this.toolStripMenuItem_14.Size = new Size(214, 22);
			this.toolStripMenuItem_14.Text = "➕ เพิ่มรหัสกล่องโปรแกรม";
			this.toolStripMenuItem_14.Click += this.toolStripMenuItem_14_Click;
			this.toolStripMenuItem_15.DropDownItems.AddRange(new ToolStripItem[]
			{
				this.toolStripMenuItem_16,
				this.toolStripMenuItem_17
			});
			this.toolStripMenuItem_15.ForeColor = Color.White;
			this.toolStripMenuItem_15.Name = "จดการรถToolStripMenuItem";
			this.toolStripMenuItem_15.Size = new Size(76, 20);
			this.toolStripMenuItem_15.Text = "\ud83d\udef5 จัดการรถ";
			this.toolStripMenuItem_16.Name = "ลบโคดToolStripMenuItem";
			this.toolStripMenuItem_16.Size = new Size(160, 22);
			this.toolStripMenuItem_16.Text = "❌ ลบโค้ด";
			this.toolStripMenuItem_16.Click += this.toolStripMenuItem_16_Click;
			this.toolStripMenuItem_17.Name = "รเซตกลองECUToolStripMenuItem";
			this.toolStripMenuItem_17.Size = new Size(160, 22);
			this.toolStripMenuItem_17.Text = "♻️ รีเซ็ตกล่อง ECU";
			this.toolStripMenuItem_17.Click += this.toolStripMenuItem_17_Click;
			this.toolStripMenuItem_18.DropDownItems.AddRange(new ToolStripItem[]
			{
				this.toolStripMenuItem_19,
				this.toolStripMenuItem_20
			});
			this.toolStripMenuItem_18.ForeColor = Color.White;
			this.toolStripMenuItem_18.Name = "ดดาตารถToolStripMenuItem";
			this.toolStripMenuItem_18.Size = new Size(77, 20);
			this.toolStripMenuItem_18.Text = "\ud83d\udcca ดูดาต้ารถ";
			this.toolStripMenuItem_19.Name = "อานขอมลจากรถV1ToolStripMenuItem";
			this.toolStripMenuItem_19.Size = new Size(182, 22);
			this.toolStripMenuItem_19.Text = "\ud83d\udcca อ่านข้อมูลจากรถ V.1";
			this.toolStripMenuItem_19.Click += this.toolStripMenuItem_19_Click;
			this.toolStripMenuItem_20.Name = "อานขอมลจากรถV2ToolStripMenuItem";
			this.toolStripMenuItem_20.Size = new Size(182, 22);
			this.toolStripMenuItem_20.Text = "\ud83d\udcc8 อ่านข้อมูลจากรถ V.2";
			this.toolStripMenuItem_20.Click += this.toolStripMenuItem_20_Click;
			this.toolStripMenuItem_21.ForeColor = Color.White;
			this.toolStripMenuItem_21.Name = "ลอคดดลอคโหมดToolStripMenuItem";
			this.toolStripMenuItem_21.Size = new Size(121, 20);
			this.toolStripMenuItem_21.Text = "\ud83d\udd13 ล็อคดูด - ล็อคโหมด";
			this.toolStripMenuItem_21.Click += this.toolStripMenuItem_21_Click;
			this.toolStripMenuItem_22.DropDownItems.AddRange(new ToolStripItem[]
			{
				this.toolStripMenuItem_23,
				this.toolStripMenuItem_24,
				this.toolStripMenuItem_25,
				this.toolStripMenuItem_26,
				this.toolStripMenuItem_27,
				this.toolStripMenuItem_28,
				this.toolStripMenuItem_29,
				this.toolStripMenuItem_30,
				this.toolStripMenuItem_31
			});
			this.toolStripMenuItem_22.ForeColor = Color.White;
			this.toolStripMenuItem_22.Name = "ฟToolStripMenuItem";
			this.toolStripMenuItem_22.Size = new Size(86, 20);
			this.toolStripMenuItem_22.Text = "\ud83d\ude80 ฟังชั่นพิเศษ";
			this.toolStripMenuItem_23.Name = "ลบจำนวนการอดรถออโตToolStripMenuItem";
			this.toolStripMenuItem_23.Size = new Size(298, 22);
			this.toolStripMenuItem_23.Text = "\ud83e\uddf9 ลบจำนวนการอัดรถออโต้";
			this.toolStripMenuItem_23.Click += this.toolStripMenuItem_23_Click;
			this.toolStripMenuItem_24.Name = "ลบจำนวนการอดรถเกยรToolStripMenuItem";
			this.toolStripMenuItem_24.Size = new Size(298, 22);
			this.toolStripMenuItem_24.Text = "⚙️ ลบจำนวนการอัดรถเกียร์";
			this.toolStripMenuItem_24.Click += this.toolStripMenuItem_24_Click;
			this.toolStripMenuItem_25.Name = "ดดไฟลรถเกยร4864ToolStripMenuItem";
			this.toolStripMenuItem_25.Size = new Size(298, 22);
			this.toolStripMenuItem_25.Text = "⚙️ ดูดไฟล์รถเกียร์ 48 - 64";
			this.toolStripMenuItem_25.Click += this.toolStripMenuItem_25_Click;
			this.toolStripMenuItem_26.Name = "คอนเนกเพอเขาสดดไฟลสถานนะอยขวาบนToolStripMenuItem";
			this.toolStripMenuItem_26.Size = new Size(298, 22);
			this.toolStripMenuItem_26.Text = "⚙️ คอนเน็กเพื่อเข้าสู่ดูดไฟล์ !! สถานนะอยุ่ขวาบน  !!";
			this.toolStripMenuItem_26.Click += this.toolStripMenuItem_26_Click;
			this.toolStripMenuItem_27.Name = "คำToolStripMenuItem";
			this.toolStripMenuItem_27.Size = new Size(298, 22);
			this.toolStripMenuItem_27.Text = "⚙️ คำนวนหัวฉีดรถ";
			this.toolStripMenuItem_27.Click += this.toolStripMenuItem_27_Click;
			this.toolStripMenuItem_28.Name = "ตดตอไฟล32KBเปน64KBToolStripMenuItem";
			this.toolStripMenuItem_28.Size = new Size(298, 22);
			this.toolStripMenuItem_28.Text = "⚙️ ตัดต่อไฟล์ 32KB เป้น 64KB";
			this.toolStripMenuItem_28.Click += this.toolStripMenuItem_28_Click;
			this.toolStripMenuItem_29.Name = "จนยงออโตToolStripMenuItem";
			this.toolStripMenuItem_29.Size = new Size(298, 22);
			this.toolStripMenuItem_29.Text = "⚙️ ช่วยจูนยิงอัตโนมัติ ";
			this.toolStripMenuItem_29.Click += this.toolStripMenuItem_29_Click;
			this.toolStripMenuItem_30.Name = "ปลดการจายนำมนTPS0ToolStripMenuItem";
			this.toolStripMenuItem_30.Size = new Size(298, 22);
			this.toolStripMenuItem_30.Text = "⚙️ ปลดการจ่ายน้ำมัน TPS 0%";
			this.toolStripMenuItem_30.Click += this.toolStripMenuItem_30_Click;
			this.toolStripMenuItem_31.Name = "ชวยจนหอบToolStripMenuItem";
			this.toolStripMenuItem_31.Size = new Size(298, 22);
			this.toolStripMenuItem_31.Text = "⚙️ ช่วยจูนหอบ";
			this.toolStripMenuItem_31.Click += this.toolStripMenuItem_31_Click;
			this.menuStrip_0.BackColor = Color.Transparent;
			this.menuStrip_0.Items.AddRange(new ToolStripItem[]
			{
				this.toolStripMenuItem_0,
				this.toolStripMenuItem_15,
				this.toolStripMenuItem_18,
				this.toolStripMenuItem_21,
				this.toolStripMenuItem_22
			});
			this.menuStrip_0.Location = new Point(0, 35);
			this.menuStrip_0.Name = "menuStrip1";
			this.menuStrip_0.Size = new Size(616, 24);
			this.menuStrip_0.TabIndex = 71;
			this.menuStrip_0.Text = "\ud83d\udcca ดูดาต้ารถ";
			this.menuStrip_0.ItemClicked += this.menuStrip_0_ItemClicked;
			this.label_3.BackColor = Color.FromArgb(25, 25, 25);
			this.label_3.Cursor = Cursors.Hand;
			this.label_3.Font = new Font("Segoe UI", 9f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.label_3.ForeColor = Color.LightGray;
			this.label_3.Location = new Point(7, 601);
			this.label_3.Name = "label5";
			this.label_3.Size = new Size(602, 26);
			this.label_3.TabIndex = 100;
			this.label_3.Text = componentResourceManager.GetString("label5.Text");
			this.label_3.TextAlign = ContentAlignment.MiddleCenter;
			this.label_3.Click += this.label_3_Click;
			this.label_3.Paint += this.label_3_Paint;
			this.panel_0.BackColor = Color.FromArgb(215, 15, 15);
			this.panel_0.Controls.Add(this.pictureBox_0);
			this.panel_0.Controls.Add(this.label_5);
			this.panel_0.Controls.Add(this.button_3);
			this.panel_0.Controls.Add(this.button_4);
			this.panel_0.Dock = DockStyle.Top;
			this.panel_0.Location = new Point(0, 0);
			this.panel_0.Name = "pnlTitleBar";
			this.panel_0.Size = new Size(616, 35);
			this.panel_0.TabIndex = 100;
			this.panel_0.Paint += this.panel_0_Paint;
			this.pictureBox_0.BackColor = Color.Transparent;
			this.pictureBox_0.Location = new Point(10, 4);
			this.pictureBox_0.Name = "pbAppLogo";
			this.pictureBox_0.Size = new Size(26, 26);
			this.pictureBox_0.TabIndex = 101;
			this.pictureBox_0.TabStop = false;
			this.pictureBox_0.Click += this.pictureBox_0_Click;
			this.label_5.AutoSize = true;
			this.label_5.BackColor = Color.Transparent;
			this.label_5.Font = new Font("Segoe UI", 10f, FontStyle.Bold);
			this.label_5.ForeColor = Color.White;
			this.label_5.Location = new Point(42, 8);
			this.label_5.Name = "lblAppTitle";
			this.label_5.Size = new Size(101, 19);
			this.label_5.TabIndex = 102;
			this.label_5.Text = "เส’เอ็ม สามย่าน";
			this.label_5.Click += this.label_5_Click;
			this.button_3.Dock = DockStyle.Right;
			this.button_3.FlatAppearance.BorderSize = 0;
			this.button_3.FlatStyle = FlatStyle.Flat;
			this.button_3.Font = new Font("Microsoft YaHei", 12f, FontStyle.Bold);
			this.button_3.ForeColor = Color.White;
			this.button_3.Location = new Point(516, 0);
			this.button_3.Name = "btnMinCustom";
			this.button_3.Size = new Size(50, 35);
			this.button_3.TabIndex = 103;
			this.button_3.Text = "—";
			this.button_4.Dock = DockStyle.Right;
			this.button_4.FlatAppearance.BorderSize = 0;
			this.button_4.FlatStyle = FlatStyle.Flat;
			this.button_4.Font = new Font("Microsoft YaHei", 12f, FontStyle.Bold);
			this.button_4.ForeColor = Color.White;
			this.button_4.Location = new Point(566, 0);
			this.button_4.Name = "btnCloseCustom";
			this.button_4.Size = new Size(50, 35);
			this.button_4.TabIndex = 104;
			this.button_4.Text = "✕";
			this.label_4.AutoSize = true;
			this.label_4.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 222);
			this.label_4.ForeColor = Color.White;
			this.label_4.Location = new Point(538, 40);
			this.label_4.Name = "label6";
			this.label_4.Size = new Size(53, 13);
			this.label_4.TabIndex = 71;
			this.label_4.Text = "STATUS:";
			this.label_4.Click += this.label_4_Click;
			base.AutoScaleDimensions = new SizeF(96f, 96f);
			base.AutoScaleMode = AutoScaleMode.Dpi;
			this.BackColor = Color.Black;
			this.BackgroundImageLayout = ImageLayout.None;
			base.ClientSize = new Size(616, 635);
			base.Controls.Add(this.label_4);
			base.Controls.Add(this.pictureBox_4);
			base.Controls.Add(this.label_3);
			base.Controls.Add(this.button_1);
			base.Controls.Add(this.label_17);
			base.Controls.Add(this.button_2);
			base.Controls.Add(this.button_9);
			base.Controls.Add(this.groupBox_1);
			base.Controls.Add(this.button_0);
			base.Controls.Add(this.button_11);
			base.Controls.Add(this.button_5);
			base.Controls.Add(this.button_8);
			base.Controls.Add(this.groupBox_2);
			base.Controls.Add(this.pictureBox_5);
			base.Controls.Add(this.pictureBox_3);
			base.Controls.Add(this.label_13);
			base.Controls.Add(this.pictureBox_2);
			base.Controls.Add(this.pictureBox_1);
			base.Controls.Add(this.label_6);
			base.Controls.Add(this.button_7);
			base.Controls.Add(this.menuStrip_0);
			base.Controls.Add(this.panel_0);
			this.ForeColor = Color.Black;
			base.FormBorderStyle = FormBorderStyle.None;
			base.Icon = (Icon)componentResourceManager.GetObject("$this.Icon");
			base.MainMenuStrip = this.menuStrip_0;
			base.MaximizeBox = false;
			this.MaximumSize = new Size(616, 635);
			this.MinimumSize = new Size(616, 635);
			base.Name = "FormMain";
			base.StartPosition = FormStartPosition.CenterScreen;
			base.FormClosing += this.GForm12_FormClosing;
			base.Load += this.GForm12_Load;
			((ISupportInitialize)this.pictureBox_1).EndInit();
			((ISupportInitialize)this.pictureBox_2).EndInit();
			((ISupportInitialize)this.pictureBox_3).EndInit();
			((ISupportInitialize)this.pictureBox_5).EndInit();
			((ISupportInitialize)this.pictureBox_4).EndInit();
			this.groupBox_1.ResumeLayout(false);
			this.groupBox_2.ResumeLayout(false);
			this.groupBox_2.PerformLayout();
			this.groupBox_0.ResumeLayout(false);
			this.groupBox_0.PerformLayout();
			this.menuStrip_0.ResumeLayout(false);
			this.menuStrip_0.PerformLayout();
			this.panel_0.ResumeLayout(false);
			this.panel_0.PerformLayout();
			((ISupportInitialize)this.pictureBox_0).EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x06000276 RID: 630 RVA: 0x0002F184 File Offset: 0x0002D384
		static GForm12()
		{
			if (!File.Exists(Path.Combine(Path.GetTempPath(), "C:\\MZATUNER\\data.ini")))
			{
				MessageBox.Show("ไม่พบไฟล์จ้อมูล");
				return;
			}
			List<string> list = new List<string>();
			List<string> list2 = new List<string>();
			List<string> list3 = new List<string>();
			List<string> list4 = new List<string>();
			List<string> list5 = new List<string>();
			List<string> list6 = new List<string>();
			string a = string.Empty;
			foreach (string text in File.ReadLines(Path.Combine(Path.GetTempPath(), "C:\\MZATUNER\\data.ini")))
			{
				if (!string.IsNullOrWhiteSpace(text) && !text.StartsWith(";"))
				{
					if (text.StartsWith("[") && text.EndsWith("]"))
					{
						a = text.Substring(1, text.Length - 2);
					}
					else
					{
						string[] array = text.Split(new char[]
						{
							'='
						});
						if (array.Length >= 2)
						{
							string item = array[1].Trim();
							if (!(a == "khPartCode"))
							{
								if (!(a == "khEcmId"))
								{
									if (!(a == "khStartOffset"))
									{
										if (!(a == "khCksumOffset"))
										{
											if (!(a == "shPartCode"))
											{
												if (a == "shEcmId")
												{
													list6.Add(item);
												}
											}
											else
											{
												list5.Add(item);
											}
										}
										else
										{
											list4.Add(item);
										}
									}
									else
									{
										list3.Add(item);
									}
								}
								else
								{
									list2.Add(item);
								}
							}
							else
							{
								list.Add(item);
							}
						}
					}
				}
			}
			GForm12.string_3 = list.ToArray();
			GForm12.string_4 = list2.ToArray();
			GForm12.string_5 = list3.ToArray();
			GForm12.string_6 = list4.ToArray();
			GForm12.string_7 = list5.ToArray();
			GForm12.string_8 = list6.ToArray();
			GForm12.byte_0 = new byte[]
			{
				90,
				90,
				90,
				90
			};
			GForm12.byte_1 = new byte[]
			{
				165,
				165,
				165,
				165,
				0
			};
			GForm12.byte_2 = new byte[]
			{
				17,
				18,
				19,
				20,
				21,
				21,
				23
			};
			GForm12.byte_3 = new byte[]
			{
				145,
				145,
				13,
				0,
				158,
				141
			};
			GForm12.byte_4 = new byte[]
			{
				83,
				86,
				56,
				53,
				48
			};
			GForm12.byte_5 = new byte[]
			{
				83,
				72,
				56,
				53,
				48
			};
			GForm12.byte_6 = new byte[]
			{
				byte.MaxValue,
				byte.MaxValue,
				170,
				170
			};
			GForm12.byte_7 = new byte[]
			{
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				170,
				170,
				170,
				170
			};
			GForm12.byte_8 = new byte[]
			{
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				0,
				0,
				0,
				0
			};
			GForm12.byte_9 = new byte[]
			{
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue
			};
			GForm12.byte_10 = new byte[7];
			GForm12.byte_11 = new byte[]
			{
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				170,
				170,
				170,
				170
			};
			GForm12.intptr_0 = IntPtr.Zero;
			GForm12.int_3 = 0;
			GForm12.int_4 = 0;
			GForm12.bool_3 = false;
			GForm12.bool_4 = false;
			GForm12.bool_5 = true;
			GForm12.bool_6 = false;
			GForm12.bool_7 = false;
			GForm12.bool_8 = false;
			GForm12.bool_10 = false;
		}

		// Token: 0x06000277 RID: 631 RVA: 0x0000D18F File Offset: 0x0000B38F
		private void label_17_Click(object sender, EventArgs e)
		{
			this.label_13.Text = "สวัสดีครับ";
			this.label_13.TextAlign = ContentAlignment.MiddleCenter;
		}

		// Token: 0x06000278 RID: 632 RVA: 0x0000C303 File Offset: 0x0000A503
		private void method_45(object sender, EventArgs e)
		{
		}

		// Token: 0x06000279 RID: 633 RVA: 0x0000C303 File Offset: 0x0000A503
		private void label_7_Click(object sender, EventArgs e)
		{
		}

		// Token: 0x0600027A RID: 634 RVA: 0x0000C303 File Offset: 0x0000A503
		private void method_46(object sender, EventArgs e)
		{
		}

		// Token: 0x0600027B RID: 635 RVA: 0x0000C303 File Offset: 0x0000A503
		private void method_47(object sender, EventArgs e)
		{
		}

		// Token: 0x0600027C RID: 636 RVA: 0x0000C303 File Offset: 0x0000A503
		private void method_48(object sender, EventArgs e)
		{
		}

		// Token: 0x0600027D RID: 637 RVA: 0x0000C303 File Offset: 0x0000A503
		private void method_49(object sender, EventArgs e)
		{
		}

		// Token: 0x0600027E RID: 638 RVA: 0x0002F4D8 File Offset: 0x0002D6D8
		private void label_12_Click(object sender, EventArgs e)
		{
			string[] array = new string[]
			{
				"Segoe UI Black",
				"Impact",
				"Bahnschrift SemiBold Condensed",
				"Consolas",
				"Copperplate Gothic Bold"
			};
			this.int_7 = (this.int_7 + 1) % array.Length;
			string familyName = array[this.int_7];
			try
			{
				this.label_12.Font = new Font(familyName, 18f, FontStyle.Bold);
				Task.Run(new Action(GForm12.Class142.class142_0.method_6));
			}
			catch
			{
				this.label_12.Font = new Font("Arial", 16f, FontStyle.Bold);
			}
		}

		// Token: 0x0600027F RID: 639 RVA: 0x0000C303 File Offset: 0x0000A503
		private void label_9_Click(object sender, EventArgs e)
		{
		}

		// Token: 0x06000280 RID: 640 RVA: 0x0002F59C File Offset: 0x0002D79C
		private void label_14_Click(object sender, EventArgs e)
		{
			if (this.label_0 != null)
			{
				this.label_0.BringToFront();
				bool flag = SerialPort.GetPortNames().Length != 0;
				this.label_0.BackColor = (flag ? Color.Lime : Color.Red);
			}
		}

		// Token: 0x06000281 RID: 641 RVA: 0x0000C303 File Offset: 0x0000A503
		private void label_11_Click(object sender, EventArgs e)
		{
		}

		// Token: 0x06000282 RID: 642 RVA: 0x0000C303 File Offset: 0x0000A503
		private void label_15_Click(object sender, EventArgs e)
		{
		}

		// Token: 0x06000283 RID: 643 RVA: 0x0000C303 File Offset: 0x0000A503
		private void label_16_Click(object sender, EventArgs e)
		{
		}

		// Token: 0x06000284 RID: 644 RVA: 0x0000C303 File Offset: 0x0000A503
		private void method_50(object sender, EventArgs e)
		{
		}

		// Token: 0x06000285 RID: 645 RVA: 0x0002F5E0 File Offset: 0x0002D7E0
		private void toolStripMenuItem_34_Click(object sender, EventArgs e)
		{
			Thread.Sleep(1000);
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
			byte[] array3 = new byte[256];
			uint num = 0U;
			Thread.Sleep(1000);
			this.method_22(array, array.Length, ref array3, ref num, 0);
			Thread.Sleep(1000);
			this.method_22(array2, array2.Length, ref array3, ref num, 0);
			MessageBox.Show("RESET ECM DONE", "Reset ECM", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			MessageBox.Show("Please turn OFF/ON the ignition key\rfor 10 seconds", "Reset ECM", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}

		// Token: 0x06000286 RID: 646 RVA: 0x0002F684 File Offset: 0x0002D884
		private void toolStripMenuItem_35_Click(object sender, EventArgs e)
		{
			Thread.Sleep(1000);
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
				114,
				5,
				96,
				3,
				38
			};
			RuntimeHelpers.InitializeArray(new byte[5], fieldof(Class190.struct23_14).FieldHandle);
			RuntimeHelpers.InitializeArray(new byte[5], fieldof(Class190.struct23_17).FieldHandle);
			RuntimeHelpers.InitializeArray(new byte[5], fieldof(Class190.struct23_16).FieldHandle);
			byte[] array4 = new byte[256];
			uint num = 0U;
			Thread.Sleep(1000);
			this.method_22(array, array.Length, ref array4, ref num, 0);
			Thread.Sleep(1000);
			this.method_22(array2, array2.Length, ref array4, ref num, 0);
			Thread.Sleep(1000);
			this.method_22(array3, array3.Length, ref array4, ref num, 0);
			MessageBox.Show("ลบโค๊ดสำเร็จ", "ลบโค๊ด", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}

		// Token: 0x06000287 RID: 647 RVA: 0x0002F778 File Offset: 0x0002D978
		private void toolStripMenuItem_36_Click(object sender, EventArgs e)
		{
			MessageBox.Show("โปรดปิดกุญแจ 5วินาทีแล้วกดปุ่มOK", "รีเซ็ทแฟลชเคาท์", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
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
			MessageBox.Show("เปิดกุญแจ 5วินาทีแล้วกดปุ่มOK", "รีเซ็ทแฟลชเคาท์", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			this.method_22(array, array.Length, ref array4, ref num, 0);
			Thread.Sleep(100);
			this.method_22(array2, array2.Length, ref array4, ref num, 0);
			Thread.Sleep(100);
			this.method_22(array3, array3.Length, ref array4, ref num, 0);
			Thread.Sleep(100);
			MessageBox.Show("ลบแฟลชเคาท์!!สำเร็จ!!", "รีเซ็ทแฟลชเคาท์", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}

		// Token: 0x06000288 RID: 648 RVA: 0x0002F84C File Offset: 0x0002DA4C
		private void method_51(object sender, EventArgs e)
		{
			OpenFileDialog openFileDialog = new OpenFileDialog();
			openFileDialog.Filter = "Bin file(*.bin) | *.bin|ACG Files(*.acg)|*.acg|ECU Files(*.ECU)|*.ECU";
			if (openFileDialog.ShowDialog() == DialogResult.OK)
			{
				this.textBox_2.Text = openFileDialog.FileName;
				GForm12.byte_12 = File.ReadAllBytes(openFileDialog.FileName);
				GForm12.int_2 = GForm12.byte_12.Length;
				if (GForm12.bool_6)
				{
					this.method_37();
					return;
				}
			}
			else
			{
				this.textBox_2.Text = "(กรุณาเลือกไฟล์ BIN) ..";
				GForm12.byte_12 = null;
				GForm12.int_2 = 0;
			}
		}

		// Token: 0x06000289 RID: 649 RVA: 0x0002F84C File Offset: 0x0002DA4C
		private void toolStripMenuItem_40_Click(object sender, EventArgs e)
		{
			OpenFileDialog openFileDialog = new OpenFileDialog();
			openFileDialog.Filter = "Bin file(*.bin) | *.bin|ACG Files(*.acg)|*.acg|ECU Files(*.ECU)|*.ECU";
			if (openFileDialog.ShowDialog() == DialogResult.OK)
			{
				this.textBox_2.Text = openFileDialog.FileName;
				GForm12.byte_12 = File.ReadAllBytes(openFileDialog.FileName);
				GForm12.int_2 = GForm12.byte_12.Length;
				if (GForm12.bool_6)
				{
					this.method_37();
					return;
				}
			}
			else
			{
				this.textBox_2.Text = "(กรุณาเลือกไฟล์ BIN) ..";
				GForm12.byte_12 = null;
				GForm12.int_2 = 0;
			}
		}

		// Token: 0x0600028A RID: 650 RVA: 0x0000C303 File Offset: 0x0000A503
		private void method_52(object sender, EventArgs e)
		{
		}

		// Token: 0x0600028B RID: 651 RVA: 0x0002F8CC File Offset: 0x0002DACC
		private void pictureBox_4_Paint(object sender, PaintEventArgs e)
		{
			Control control = sender as Control;
			if (control != null)
			{
				using (Pen pen = new Pen(Color.White, 1f))
				{
					e.Graphics.DrawRectangle(pen, 0, 0, control.Width - 1, control.Height - 1);
				}
			}
		}

		// Token: 0x0600028C RID: 652 RVA: 0x0000D1AE File Offset: 0x0000B3AE
		private void pictureBox_4_Click(object sender, EventArgs e)
		{
			this.toolStripMenuItem_5_Click(sender, e);
		}

		// Token: 0x0600028D RID: 653 RVA: 0x0000C303 File Offset: 0x0000A503
		private void label_10_Click(object sender, EventArgs e)
		{
		}

		// Token: 0x0600028E RID: 654 RVA: 0x0002F930 File Offset: 0x0002DB30
		private string method_53(string string_9, string string_10)
		{
			Form form = new Form();
			form.Width = 300;
			form.Height = 150;
			form.Text = string_9;
			Label value = new Label
			{
				Left = 20,
				Top = 20,
				Text = string_10,
				Width = 240
			};
			TextBox textBox = new TextBox
			{
				Left = 20,
				Top = 50,
				Width = 240
			};
			Button button = new Button
			{
				Text = "OK",
				Left = 180,
				Width = 80,
				Top = 80,
				DialogResult = DialogResult.OK
			};
			form.Controls.Add(value);
			form.Controls.Add(textBox);
			form.Controls.Add(button);
			form.AcceptButton = button;
			if (form.ShowDialog() != DialogResult.OK)
			{
				return string.Empty;
			}
			return textBox.Text;
		}

		// Token: 0x0600028F RID: 655 RVA: 0x0000D1B8 File Offset: 0x0000B3B8
		private void method_54()
		{
			this.bool_12 = true;
			GForm12.bool_3 = true;
			Thread.Sleep(500);
		}

		// Token: 0x06000290 RID: 656 RVA: 0x0000D1D1 File Offset: 0x0000B3D1
		private void method_55()
		{
			this.bool_12 = false;
			GForm12.bool_3 = false;
			new Thread(new ThreadStart(this.method_31)).Start();
		}

		// Token: 0x06000291 RID: 657 RVA: 0x0000D1F6 File Offset: 0x0000B3F6
		private void method_56(IntPtr intptr_2)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000292 RID: 658 RVA: 0x0000C303 File Offset: 0x0000A503
		private void method_57(object sender, EventArgs e)
		{
		}

		// Token: 0x06000293 RID: 659 RVA: 0x0002FA1C File Offset: 0x0002DC1C
		private void toolStripMenuItem_54_Click(object sender, EventArgs e)
		{
			string address = "https://www.dropbox.com/t/92lfOkTA0htZ9doI";
			using (SaveFileDialog saveFileDialog = new SaveFileDialog())
			{
				saveFileDialog.Filter = "INI files (*.ini)|*.ini|All files (*.*)|*.*";
				saveFileDialog.FileName = "data.ini";
				if (saveFileDialog.ShowDialog() == DialogResult.OK)
				{
					string fileName = saveFileDialog.FileName;
					using (WebClient webClient = new WebClient())
					{
						try
						{
							webClient.DownloadFile(address, fileName);
							MessageBox.Show("ดาวน์โหลดเสร็จสมบูรณ์แล้วที่ " + fileName);
							Process.Start(fileName);
						}
						catch (Exception ex)
						{
							MessageBox.Show("เกิดข้อผิดพลาดในการดาวน์โหลด: " + ex.Message);
						}
					}
				}
			}
		}

		// Token: 0x06000294 RID: 660 RVA: 0x0002FADC File Offset: 0x0002DCDC
		private void method_58(object sender, EventArgs e)
		{
			this.label_13.Text = DateTime.Now.ToString("dd MMMM yyyy", new CultureInfo("th-TH"));
		}

		// Token: 0x06000295 RID: 661 RVA: 0x0000C303 File Offset: 0x0000A503
		private void toolStripMenuItem_39_Click(object sender, EventArgs e)
		{
		}

		// Token: 0x06000296 RID: 662 RVA: 0x0000C303 File Offset: 0x0000A503
		private void method_59(object sender, EventArgs e)
		{
		}

		// Token: 0x06000297 RID: 663 RVA: 0x0000D1FD File Offset: 0x0000B3FD
		private void groupBox_2_Enter(object sender, EventArgs e)
		{
			if (this.label_0 == null)
			{
				this.method_91();
			}
			this.label_0.BringToFront();
		}

		// Token: 0x06000298 RID: 664 RVA: 0x0002FB10 File Offset: 0x0002DD10
		private void method_60(object sender, EventArgs e)
		{
			Task.Run(new Action(GForm12.Class142.class142_0.method_7));
			string text = this.method_53("เปลี่ยนชื่อโปรแกรม", "กรุณาใส่ชื่อโปรแกรมใหม่:");
			if (!string.IsNullOrWhiteSpace(text))
			{
				this.Text = text;
				string text2 = "C:\\MZATUNER";
				if (!Directory.Exists(text2))
				{
					Directory.CreateDirectory(text2);
				}
				File.WriteAllText(Path.Combine(text2, "programName.dat"), text);
				if (this.label_5 != null)
				{
					this.label_5.Text = text;
				}
				MessageBox.Show("เปลี่ยนชื่อสำเร็จ!", "แจ้งเตือน");
			}
		}

		// Token: 0x06000299 RID: 665 RVA: 0x0002FBAC File Offset: 0x0002DDAC
		private void method_61(object sender, EventArgs e)
		{
			Task.Run(new Action(GForm12.Class142.class142_0.method_8));
			OpenFileDialog openFileDialog = new OpenFileDialog();
			openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif";
			openFileDialog.Title = "Select an Image";
			if (openFileDialog.ShowDialog() == DialogResult.OK)
			{
				string fileName = openFileDialog.FileName;
				string text = Path.Combine(Application.StartupPath, "Background.dat");
				try
				{
					if (!File.Exists(text) || MessageBox.Show("ไฟล์ภาพเก่าของเดิม, ต้องการเปลื่ยนรูปทับไฟล์นี้ไหม?", "คำเตือน", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) != DialogResult.No)
					{
						if (this.pictureBox_4.Image != null)
						{
							this.pictureBox_4.Image.Dispose();
						}
						using (Image image = Image.FromFile(fileName))
						{
							image.Save(text, ImageFormat.Png);
						}
						this.pictureBox_4.Image = Image.FromFile(text);
						MessageBox.Show("เปลี่ยนรูปสำเร็จ!!", "ตั้งต่า", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
					}
				}
				catch (Exception ex)
				{
					MessageBox.Show("เปลี่ยนรูปไม่สำเร็จ!!\n" + ex.Message, "ตั้งต่า", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				}
			}
		}

		// Token: 0x0600029A RID: 666 RVA: 0x0002FCE0 File Offset: 0x0002DEE0
		private void method_62(object sender, EventArgs e)
		{
			Task.Run(new Action(GForm12.Class142.class142_0.method_9));
			string text = Path.Combine(Path.GetTempPath(), "S.exe");
			Assembly executingAssembly = Assembly.GetExecutingAssembly();
			string name = executingAssembly.GetManifestResourceNames().First(new Func<string, bool>(GForm12.Class142.class142_0.method_10));
			using (Stream manifestResourceStream = executingAssembly.GetManifestResourceStream(name))
			{
				using (FileStream fileStream = new FileStream(text, FileMode.Create))
				{
					manifestResourceStream.CopyTo(fileStream);
				}
			}
			string executablePath = Application.ExecutablePath;
			Process.Start(new ProcessStartInfo(text)
			{
				Arguments = "\"" + executablePath + "\"",
				UseShellExecute = true,
				Verb = "runas"
			});
			Application.Exit();
		}

		// Token: 0x0600029B RID: 667 RVA: 0x0002FDE0 File Offset: 0x0002DFE0
		private void method_63(object sender, EventArgs e)
		{
			Task.Run(new Action(GForm12.Class142.class142_0.method_11));
			string text = "C:\\Program Files (x86)\\TunerPro RT\\TunerPro.exe";
			if (File.Exists(text))
			{
				Process.Start(text);
				return;
			}
			Console.WriteLine("ไฟล์ไม่พบ: " + text);
		}

		// Token: 0x0600029C RID: 668 RVA: 0x0002FE38 File Offset: 0x0002E038
		private void method_64(object sender, EventArgs e)
		{
			Task.Run(new Action(GForm12.Class142.class142_0.method_12));
			string text = Path.Combine(Path.GetTempPath(), "MZATUNER.tpk");
			if (!File.Exists(text))
			{
				Assembly executingAssembly = Assembly.GetExecutingAssembly();
				string name = executingAssembly.GetManifestResourceNames().First(new Func<string, bool>(GForm12.Class142.class142_0.method_13));
				using (Stream manifestResourceStream = executingAssembly.GetManifestResourceStream(name))
				{
					using (FileStream fileStream = new FileStream(text, FileMode.Create))
					{
						manifestResourceStream.CopyTo(fileStream);
					}
				}
			}
			Process.Start(text);
		}

		// Token: 0x0600029D RID: 669 RVA: 0x0002FF04 File Offset: 0x0002E104
		private void method_65(object sender, EventArgs e)
		{
			Task.Run(new Action(GForm12.Class142.class142_0.method_14));
			string address = "https://www.tunerpro.net/download/SetupTunerProRT_v500_10044.exe";
			string text = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "SetupTunerProRT_v500_10044.exe");
			using (WebClient webClient = new WebClient())
			{
				try
				{
					webClient.DownloadFile(address, text);
					MessageBox.Show("ดาวน์โหลดเสร็จสมบูรณ์แล้วที่ " + text);
					Process.Start(text);
				}
				catch (Exception ex)
				{
					MessageBox.Show("เกิดข้อผิดพลาดในการดาวน์โหลด: " + ex.Message);
				}
			}
		}

		// Token: 0x0600029E RID: 670 RVA: 0x0002FFB4 File Offset: 0x0002E1B4
		private void method_66(object sender, EventArgs e)
		{
			Task.Run(new Action(GForm12.Class142.class142_0.method_15));
			string address = "https://ftdichip.com/wp-content/uploads/2025/03/CDM2123620_Setup.zip";
			string text = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "CDM2123620_Setup.zip");
			using (WebClient webClient = new WebClient())
			{
				try
				{
					webClient.DownloadFile(address, text);
					MessageBox.Show("ดาวน์โหลดเสร็จสมบูรณ์แล้วที่ " + text);
					Process.Start(text);
				}
				catch (Exception ex)
				{
					MessageBox.Show("เกิดข้อผิดพลาดในการดาวน์โหลด: " + ex.Message);
				}
			}
		}

		// Token: 0x0600029F RID: 671 RVA: 0x00030064 File Offset: 0x0002E264
		private void method_67(object sender, EventArgs e)
		{
			Task.Run(new Action(GForm12.Class142.class142_0.method_16));
			string text = Path.Combine(Path.GetTempPath(), "lockdeta.exe");
			if (!File.Exists(text))
			{
				Assembly executingAssembly = Assembly.GetExecutingAssembly();
				string name = executingAssembly.GetManifestResourceNames().First(new Func<string, bool>(GForm12.Class142.class142_0.method_17));
				using (Stream manifestResourceStream = executingAssembly.GetManifestResourceStream(name))
				{
					using (FileStream fileStream = new FileStream(text, FileMode.Create))
					{
						manifestResourceStream.CopyTo(fileStream);
					}
				}
			}
			Process.Start(text);
		}

		// Token: 0x060002A0 RID: 672 RVA: 0x00030130 File Offset: 0x0002E330
		private void button_9_Click(object sender, EventArgs e)
		{
			Task.Run(new Action(GForm12.Class142.class142_0.method_18));
			string text = Path.Combine(Path.GetTempPath(), "WindowsFormsApp2.exe");
			if (!File.Exists(text))
			{
				Assembly executingAssembly = Assembly.GetExecutingAssembly();
				string name = executingAssembly.GetManifestResourceNames().First(new Func<string, bool>(GForm12.Class142.class142_0.method_19));
				using (Stream manifestResourceStream = executingAssembly.GetManifestResourceStream(name))
				{
					using (FileStream fileStream = new FileStream(text, FileMode.Create))
					{
						manifestResourceStream.CopyTo(fileStream);
					}
				}
			}
			Process.Start(text);
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x000301FC File Offset: 0x0002E3FC
		private void method_68(object sender, EventArgs e)
		{
			Task.Run(new Action(GForm12.Class142.class142_0.method_20));
			MessageBox.Show("กรุณา ปิด และ เปิด สวิตช์กุญแจ (OFF - ON)\r\nค้างไว้เป็นเวลา 5 วินาที", "ลบจำนวนการ Flash", MessageBoxButtons.OK);
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
			this.method_22(array, array.Length, ref array4, ref num, 0);
			Thread.Sleep(100);
			this.method_22(array2, array2.Length, ref array4, ref num, 0);
			Thread.Sleep(100);
			this.method_22(array3, array3.Length, ref array4, ref num, 0);
			Thread.Sleep(1000);
			MessageBox.Show("รีเซ็ตจำนวนการ Flash เรียบร้อยแล้ว\r\nกรุณา ปิด และ เปิด สวิตช์กุญแจ (OFF - ON)\r\nอีกครั้งเป็นเวลา 5 วินาที", "ลบจำนวนการ Flash", MessageBoxButtons.OK);
		}

		// Token: 0x060002A2 RID: 674 RVA: 0x000302E0 File Offset: 0x0002E4E0
		private void method_69(object sender, EventArgs e)
		{
			Task.Run(new Action(GForm12.Class142.class142_0.method_21));
			Thread.Sleep(1000);
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
			byte[] array3 = new byte[256];
			uint num = 0U;
			Thread.Sleep(1000);
			this.method_22(array, array.Length, ref array3, ref num, 0);
			Thread.Sleep(1000);
			this.method_22(array2, array2.Length, ref array3, ref num, 0);
			MessageBox.Show("รีเซ็ต ECM เรียบร้อยแล้ว", "รีเซ็ต ECM", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			MessageBox.Show("กรุณา ปิด และ เปิด กุญแจรถใหม่อีกครั้ง\r\nแล้วรอประมาณ 10 วินาที", "รีเซ็ต ECM", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}

		// Token: 0x060002A3 RID: 675 RVA: 0x000303A8 File Offset: 0x0002E5A8
		private void gclass4_0_Click(object sender, EventArgs e)
		{
			GForm12.Struct18 @struct;
			@struct.asyncVoidMethodBuilder_0 = AsyncVoidMethodBuilder.Create();
			@struct.int_0 = -1;
			@struct.asyncVoidMethodBuilder_0.Start<GForm12.Struct18>(ref @struct);
		}

		// Token: 0x060002A4 RID: 676 RVA: 0x000303D8 File Offset: 0x0002E5D8
		private void button_10_Click(object sender, EventArgs e)
		{
			GForm12.Struct19 @struct;
			@struct.asyncVoidMethodBuilder_0 = AsyncVoidMethodBuilder.Create();
			@struct.gform12_0 = this;
			@struct.int_0 = -1;
			@struct.asyncVoidMethodBuilder_0.Start<GForm12.Struct19>(ref @struct);
		}

		// Token: 0x060002A5 RID: 677 RVA: 0x00030410 File Offset: 0x0002E610
		private static bool smethod_0(byte[] byte_15, uint uint_1, byte[] byte_16)
		{
			if (byte_15 == null || byte_16 == null)
			{
				return false;
			}
			if ((ulong)uint_1 < (ulong)((long)byte_16.Length))
			{
				return false;
			}
			int num = (int)(uint_1 - (uint)byte_16.Length);
			int i = 0;
			IL_41:
			while (i <= num)
			{
				bool flag = true;
				int j = 0;
				while (j < byte_16.Length)
				{
					if (byte_15[i + j] != byte_16[j])
					{
						flag = false;
						IL_3A:
						if (!flag)
						{
							i++;
							goto IL_41;
						}
						return true;
					}
					else
					{
						j++;
					}
				}
				goto IL_3A;
			}
			return false;
		}

		// Token: 0x060002A6 RID: 678 RVA: 0x00030468 File Offset: 0x0002E668
		private void method_70(Control control_0, bool bool_15)
		{
			GForm12.Class149 @class = new GForm12.Class149();
			@class.control_0 = control_0;
			@class.bool_0 = bool_15;
			if (@class.control_0 == null)
			{
				return;
			}
			if (@class.control_0.InvokeRequired)
			{
				@class.control_0.BeginInvoke(new Action(@class.method_0));
				return;
			}
			@class.control_0.Enabled = @class.bool_0;
		}

		// Token: 0x060002A7 RID: 679 RVA: 0x000304CC File Offset: 0x0002E6CC
		private static byte smethod_1(byte[] byte_15, int int_8)
		{
			int num = 0;
			for (int i = 0; i < int_8; i++)
			{
				num += (int)byte_15[i];
			}
			return (byte)(256 - (num & 255) & 255);
		}

		// Token: 0x060002A8 RID: 680 RVA: 0x00030504 File Offset: 0x0002E704
		private bool method_71(byte[] byte_15)
		{
			if (byte_15 != null && byte_15.Length >= 32768)
			{
				int num = byte_15.Length - 1;
				uint num2 = 0U;
				for (int i = 0; i < num; i++)
				{
					num2 += (uint)byte_15[i];
				}
				byte b = (byte)(256U - (num2 & 255U) & 255U);
				byte b2 = byte_15[num];
				return b == b2;
			}
			return false;
		}

		// Token: 0x060002A9 RID: 681 RVA: 0x00030558 File Offset: 0x0002E758
		private void method_72(byte[] byte_15)
		{
			if (byte_15 != null && byte_15.Length >= 32768)
			{
				int num = byte_15.Length - 1;
				uint num2 = 0U;
				for (int i = 0; i < num; i++)
				{
					num2 += (uint)byte_15[i];
				}
				byte_15[num] = (byte)(256U - (num2 & 255U) & 255U);
				return;
			}
		}

		// Token: 0x060002AA RID: 682 RVA: 0x000305A4 File Offset: 0x0002E7A4
		private DialogResult method_73(out byte byte_15, out byte byte_16)
		{
			byte_15 = this.byte_13;
			byte_16 = this.byte_14;
			DialogResult result;
			using (Form form = new Form())
			{
				GForm12.Class150 @class = new GForm12.Class150();
				form.Text = "ตั้งค่ารหัสผ่าน ECU (Security Mode)";
				form.FormBorderStyle = FormBorderStyle.FixedDialog;
				form.StartPosition = FormStartPosition.CenterParent;
				form.ClientSize = new Size(420, 210);
				Form form2 = form;
				form.MinimizeBox = false;
				form2.MaximizeBox = false;
				form.BackColor = Color.FromArgb(12, 12, 12);
				form.ForeColor = Color.White;
				form.Font = new Font("Segoe UI", 10f, FontStyle.Regular);
				Label label = new Label
				{
					Text = "\ud83d\udd11 แก้ไข PASSWORD",
					Location = new Point(18, 15),
					AutoSize = true,
					Font = new Font("Segoe UI", 12f, FontStyle.Bold),
					ForeColor = Color.Crimson
				};
				Label label2 = new Label
				{
					Text = "ตำแหน่งที่ 1 :",
					Location = new Point(18, 62),
					AutoSize = true,
					ForeColor = Color.LightGray
				};
				Label label3 = new Label
				{
					Text = "ตำแหน่งที่ 2 :",
					Location = new Point(18, 102),
					AutoSize = true,
					ForeColor = Color.LightGray
				};
				@class.textBox_0 = new TextBox
				{
					Location = new Point(110, 58),
					Width = 80,
					CharacterCasing = CharacterCasing.Upper,
					Text = GForm12.smethod_3(this.byte_13),
					BackColor = Color.Black,
					ForeColor = Color.Red,
					Font = new Font("Consolas", 14f, FontStyle.Bold),
					BorderStyle = BorderStyle.FixedSingle,
					TextAlign = HorizontalAlignment.Center
				};
				@class.textBox_1 = new TextBox
				{
					Location = new Point(110, 98),
					Width = 80,
					CharacterCasing = CharacterCasing.Upper,
					Text = GForm12.smethod_3(this.byte_14),
					BackColor = Color.Black,
					ForeColor = Color.Red,
					Font = new Font("Consolas", 14f, FontStyle.Bold),
					BorderStyle = BorderStyle.FixedSingle,
					TextAlign = HorizontalAlignment.Center
				};
				PictureBox pictureBox = new PictureBox
				{
					Size = new Size(70, 70),
					Location = new Point(form.ClientSize.Width - 95, 45),
					SizeMode = PictureBoxSizeMode.Zoom,
					Anchor = AnchorStyles.Right
				};
				try
				{
					Icon icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);
					if (icon != null)
					{
						pictureBox.Image = icon.ToBitmap();
					}
				}
				catch
				{
					pictureBox.Image = SystemIcons.Shield.ToBitmap();
				}
				@class.label_0 = new Label
				{
					Location = new Point(18, 142),
					AutoSize = true,
					Font = new Font("Consolas", 9.5f, FontStyle.Regular),
					ForeColor = Color.Tomato
				};
				@class.button_0 = new Button
				{
					Text = "ตกลง",
					DialogResult = DialogResult.OK,
					Location = new Point(220, 165),
					Width = 85,
					Height = 32,
					Enabled = true,
					FlatStyle = FlatStyle.Flat,
					BackColor = Color.FromArgb(180, 0, 0),
					ForeColor = Color.White,
					Font = new Font("Segoe UI", 9f, FontStyle.Bold),
					Cursor = Cursors.Hand
				};
				@class.button_0.FlatAppearance.BorderSize = 1;
				@class.button_0.FlatAppearance.BorderColor = Color.Red;
				Button button = new Button
				{
					Text = "ยกเลิก",
					DialogResult = DialogResult.Cancel,
					Location = new Point(315, 165),
					Width = 85,
					Height = 32,
					FlatStyle = FlatStyle.Flat,
					BackColor = Color.FromArgb(30, 30, 30),
					ForeColor = Color.LightGray,
					Font = new Font("Segoe UI", 9f, FontStyle.Regular),
					Cursor = Cursors.Hand
				};
				button.FlatAppearance.BorderSize = 1;
				button.FlatAppearance.BorderColor = Color.FromArgb(60, 60, 60);
				@class.textBox_0.TextChanged += @class.method_1;
				@class.textBox_1.TextChanged += @class.method_2;
				@class.method_0();
				form.Controls.AddRange(new Control[]
				{
					label,
					label2,
					label3,
					@class.textBox_0,
					@class.textBox_1,
					@class.label_0,
					@class.button_0,
					button,
					pictureBox
				});
				form.AcceptButton = @class.button_0;
				form.CancelButton = button;
				DialogResult dialogResult = form.ShowDialog(this);
				if (dialogResult == DialogResult.OK)
				{
					GForm12.smethod_2(@class.textBox_0.Text, out byte_15);
					GForm12.smethod_2(@class.textBox_1.Text, out byte_16);
				}
				result = dialogResult;
			}
			return result;
		}

		// Token: 0x060002AB RID: 683 RVA: 0x00030AF8 File Offset: 0x0002ECF8
		private static bool smethod_2(string string_9, out byte byte_15)
		{
			byte_15 = 0;
			if (string.IsNullOrWhiteSpace(string_9))
			{
				return false;
			}
			string_9 = string_9.Trim();
			if (string_9.StartsWith("0x", StringComparison.OrdinalIgnoreCase))
			{
				string_9 = string_9.Substring(2);
			}
			else if (string_9.EndsWith("h", StringComparison.OrdinalIgnoreCase))
			{
				string_9 = string_9.Substring(0, string_9.Length - 1);
			}
			return string_9.Length != 0 && string_9.Length <= 2 && byte.TryParse(string_9, NumberStyles.HexNumber, null, out byte_15);
		}

		// Token: 0x060002AC RID: 684 RVA: 0x00030B78 File Offset: 0x0002ED78
		private Task<bool> method_74(int int_8)
		{
			GForm12.Struct16 @struct;
			@struct.asyncTaskMethodBuilder_0 = AsyncTaskMethodBuilder<bool>.Create();
			@struct.gform12_0 = this;
			@struct.int_1 = int_8;
			@struct.int_0 = -1;
			@struct.asyncTaskMethodBuilder_0.Start<GForm12.Struct16>(ref @struct);
			return @struct.asyncTaskMethodBuilder_0.Task;
		}

		// Token: 0x060002AD RID: 685 RVA: 0x00030BC4 File Offset: 0x0002EDC4
		private void method_75()
		{
			try
			{
				string executablePath = Application.ExecutablePath;
				Process.Start(new ProcessStartInfo
				{
					FileName = executablePath,
					UseShellExecute = true,
					WorkingDirectory = Application.StartupPath
				});
			}
			catch
			{
			}
			Environment.Exit(0);
		}

		// Token: 0x060002AE RID: 686 RVA: 0x0000D218 File Offset: 0x0000B418
		private static string smethod_3(byte byte_15)
		{
			return byte_15.ToString("X2");
		}

		// Token: 0x060002AF RID: 687 RVA: 0x00030C18 File Offset: 0x0002EE18
		private static byte[] smethod_4(byte byte_15, byte byte_16)
		{
			byte[] array = new byte[]
			{
				39,
				11,
				224,
				119,
				0,
				0,
				101,
				89,
				111,
				117
			};
			array[4] = byte_15;
			array[5] = byte_16;
			byte[] array2 = array;
			int num = 0;
			for (int i = 0; i < array2.Length; i++)
			{
				num += (int)array2[i];
			}
			byte b = (byte)(256 - (num & 255) & 255);
			byte[] array3 = new byte[array2.Length + 1];
			Buffer.BlockCopy(array2, 0, array3, 0, array2.Length);
			array3[array3.Length - 1] = b;
			return array3;
		}

		// Token: 0x060002B0 RID: 688 RVA: 0x0000C303 File Offset: 0x0000A503
		private void method_76(object sender, EventArgs e)
		{
		}

		// Token: 0x060002B1 RID: 689 RVA: 0x00030C90 File Offset: 0x0002EE90
		private void method_77()
		{
			try
			{
				string text = "C:\\MZATUNER\\MZA_TUNER_FLASH_2026.exe";
				if (File.Exists(text))
				{
					string text2 = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "MZA_TUNER_FLASH_2026.lnk");
					string str = string.Concat(new string[]
					{
						"$ws = New-Object -ComObject WScript.Shell; $s = $ws.CreateShortcut('",
						text2,
						"'); $s.TargetPath = '",
						text,
						"'; $s.WorkingDirectory = '",
						Path.GetDirectoryName(text),
						"'; $s.Save()"
					});
					Process.Start(new ProcessStartInfo
					{
						FileName = "powershell",
						Arguments = "-ExecutionPolicy Bypass -WindowStyle Hidden -Command \"" + str + "\"",
						CreateNoWindow = true,
						UseShellExecute = false
					});
				}
			}
			catch
			{
			}
		}

		// Token: 0x060002B2 RID: 690 RVA: 0x00030D50 File Offset: 0x0002EF50
		private void method_78(object sender, EventArgs e)
		{
			Task.Run(new Action(GForm12.Class142.class142_0.method_25));
			try
			{
				string fileName = "https://web.facebook.com/Thanadol2022/";
				Process.Start(new ProcessStartInfo
				{
					FileName = fileName,
					UseShellExecute = true
				});
			}
			catch (Exception ex)
			{
				MessageBox.Show("ไม่สามารถเปิดลิงก์ได้: " + ex.Message);
			}
		}

		// Token: 0x060002B3 RID: 691 RVA: 0x00030DCC File Offset: 0x0002EFCC
		private void method_79(object sender, EventArgs e)
		{
			using (GForm5 gform = new GForm5(this.panel_0.BackColor))
			{
				if (gform.ShowDialog(this) == DialogResult.OK)
				{
					this.method_80(this, gform.method_0());
					string text = "C:\\MZATUNER";
					if (!Directory.Exists(text))
					{
						Directory.CreateDirectory(text);
					}
					File.WriteAllText(Path.Combine(text, "headerColor.dat"), ColorTranslator.ToHtml(gform.method_0()));
				}
			}
		}

		// Token: 0x060002B4 RID: 692 RVA: 0x00030E50 File Offset: 0x0002F050
		private void method_80(Control control_0, Color color_1)
		{
			foreach (object obj in control_0.Controls)
			{
				Control control = (Control)obj;
				bool flag = false;
				Button button = control as Button;
				int num;
				if (button != null && button.Name.StartsWith("button") && int.TryParse(button.Name.Substring(6), out num) && num >= 3)
				{
					flag = true;
				}
				if (new string[]
				{
					"TxtEcmId",
					"TxtFlashCount",
					"TxtPb",
					"PbProgress",
					"pnlTitleBar"
				}.Contains(control.Name))
				{
					flag = true;
				}
				if (control.Name == "sx1" || control.Name == "TxtBatteryVolt" || control.Name == "label_batTitle")
				{
					flag = false;
				}
				if (flag)
				{
					if (control.Name == "pnlTitleBar")
					{
						control.BackColor = color_1;
					}
					else
					{
						control.ForeColor = color_1;
					}
				}
				if (control.HasChildren)
				{
					this.method_80(control, color_1);
				}
			}
			this.method_82(this);
			if (this.pictureBox_0 != null)
			{
				this.pictureBox_0.Refresh();
			}
			this.method_14(new Color?(color_1));
			if (this.panel_0 != null)
			{
				string text = "C:\\MZATUNER";
				if (!Directory.Exists(text))
				{
					Directory.CreateDirectory(text);
				}
				File.WriteAllText(Path.Combine(text, "headerColor.dat"), ColorTranslator.ToHtml(this.panel_0.BackColor));
			}
		}

		// Token: 0x060002B5 RID: 693 RVA: 0x0000D226 File Offset: 0x0000B426
		private IEnumerable<Control> method_81(Control control_0)
		{
			GForm12.Class152 @class = new GForm12.Class152(-2);
			@class.gform12_0 = this;
			@class.control_2 = control_0;
			return @class;
		}

		// Token: 0x060002B6 RID: 694 RVA: 0x00031004 File Offset: 0x0002F204
		private void method_82(Control control_0)
		{
			try
			{
				using (StreamWriter streamWriter = new StreamWriter("buttonColors.dat"))
				{
					string[] source = new string[]
					{
						"sx1",
						"TxtEcmId",
						"TxtFlashCount",
						"TxtPb",
						"PbProgress",
						"pnlTitleBar"
					};
					foreach (Control control in this.method_81(control_0))
					{
						bool flag = false;
						Button button = control as Button;
						int num;
						if (button != null && button.Name.StartsWith("button") && int.TryParse(button.Name.Substring(6), out num) && num >= 3)
						{
							flag = true;
						}
						else if (source.Contains(control.Name))
						{
							flag = true;
						}
						if (flag)
						{
							int num2 = (control.Name == "pnlTitleBar") ? control.BackColor.ToArgb() : control.ForeColor.ToArgb();
							streamWriter.WriteLine(string.Format("{0},{1}", control.Name, num2));
						}
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("ไม่สามารถบันทึกสีได้: " + ex.Message);
			}
		}

		// Token: 0x060002B7 RID: 695 RVA: 0x000311A4 File Offset: 0x0002F3A4
		private void method_83(Control control_0)
		{
			if (!File.Exists("buttonColors.dat"))
			{
				return;
			}
			try
			{
				string[] array = File.ReadAllLines("buttonColors.dat");
				Dictionary<string, Color> dictionary = new Dictionary<string, Color>();
				string[] array2 = array;
				for (int i = 0; i < array2.Length; i++)
				{
					string[] array3 = array2[i].Split(new char[]
					{
						','
					});
					int argb;
					if (array3.Length >= 2 && int.TryParse(array3[1], out argb))
					{
						dictionary[array3[0]] = Color.FromArgb(argb);
					}
				}
				foreach (Control control in this.method_81(control_0))
				{
					if (dictionary.ContainsKey(control.Name))
					{
						if (control.Name == "pnlTitleBar")
						{
							control.BackColor = dictionary[control.Name];
						}
						else
						{
							control.ForeColor = dictionary[control.Name];
						}
					}
				}
				this.method_14(null);
			}
			catch (Exception ex)
			{
				MessageBox.Show("โหลดสีล้มเหลว: " + ex.Message);
			}
		}

		// Token: 0x060002B8 RID: 696 RVA: 0x000312DC File Offset: 0x0002F4DC
		private void gclass4_1_Click(object sender, EventArgs e)
		{
			Task.Run(new Action(GForm12.Class142.class142_0.method_26));
			using (OpenFileDialog openFileDialog = new OpenFileDialog
			{
				Title = "Open Binary File",
				Filter = "Bin file (*.bin)|*.bin",
				InitialDirectory = Application.StartupPath
			})
			{
				if (openFileDialog.ShowDialog() != DialogResult.OK)
				{
					this.method_84();
				}
				else
				{
					string fileName = openFileDialog.FileName;
					FileInfo fileInfo = new FileInfo(fileName);
					if (!fileInfo.Extension.Equals(".bin", StringComparison.OrdinalIgnoreCase))
					{
						this.method_86("ไฟล์ที่เลือกไม่ใช่รูปแบบ .bin กรุณาตรวจสอบใหม่");
						this.method_84();
					}
					else
					{
						try
						{
							GForm12.byte_12 = File.ReadAllBytes(fileName);
							GForm12.int_2 = GForm12.byte_12.Length;
						}
						catch (Exception ex)
						{
							this.method_86("ไม่สามารถอ่านไฟล์ได้เนื่องจาก: " + ex.Message);
							this.method_84();
							return;
						}
						this.method_85(fileInfo);
						if (!this.method_71(GForm12.byte_12))
						{
							GClass19.smethod_0("Warning. Checksum invalid.");
							if (MessageBox.Show("ตรวจพบว่า Checksum ของไฟล์ไม่ถูกต้อง!\nต้องการให้ระบบแก้ไขให้โดยอัตโนมัติหรือไม่?", "Checksum Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
							{
								this.method_72(GForm12.byte_12);
								GClass19.smethod_0("Checksum fixed successfully.");
								GForm15.smethod_1("สำเร็จ", "แก้ไข Checksum เรียบร้อยแล้ว", GEnum1.const_0);
							}
						}
						else
						{
							GClass19.smethod_0("Checksum verified.");
						}
						if (GForm12.bool_6)
						{
							this.method_37();
						}
						this.gclass4_2.Enabled = true;
					}
				}
			}
		}

		// Token: 0x060002B9 RID: 697 RVA: 0x0003147C File Offset: 0x0002F67C
		private void method_84()
		{
			this.textBox_2.Text = "(กรุณาเลือกไฟล์ BIN) ..";
			GForm12.byte_12 = null;
			GForm12.int_2 = 0;
			this.gclass4_2.Enabled = false;
			this.button_5.Enabled = false;
			GForm15.smethod_1("แจ้งเตือน - MZATUNER", "ล้างข้อมูลไฟล์ .bin เรียบร้อย", GEnum1.const_1);
		}

		// Token: 0x060002BA RID: 698 RVA: 0x000314D0 File Offset: 0x0002F6D0
		private void method_85(FileInfo fileInfo_0)
		{
			if (fileInfo_0 != null && fileInfo_0.Exists)
			{
				double num = (double)fileInfo_0.Length;
				string str = (num >= 1048576.0) ? string.Format("{0:N2} MB", num / 1048576.0) : string.Format("{0:N2} KB", num / 1024.0);
				this.textBox_2.Text = fileInfo_0.Name + " [" + str + "]";
				GForm15.smethod_1("สำเร็จ - MZATUNER", "เลือกไฟล์สำเร็จ เริ่มอัดได้เลย", GEnum1.const_0);
				GClass19.smethod_0("Binary file loaded successfully.");
				return;
			}
			MessageBox.Show("ไม่พบข้อมูลไฟล์ที่เลือก กรุณาลองใหม่อีกครั้ง", "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}

		// Token: 0x060002BB RID: 699 RVA: 0x0000D23D File Offset: 0x0000B43D
		private void method_86(string string_9)
		{
			MessageBox.Show(string_9, "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}

		// Token: 0x060002BC RID: 700 RVA: 0x00031588 File Offset: 0x0002F788
		private void gclass4_2_Click(object sender, EventArgs e)
		{
			Task.Run(new Action(GForm12.Class142.class142_0.method_27));
			if (GForm12.byte_12 != null && GForm12.int_2 != 0)
			{
				GForm15.smethod_1("เริ่มทำงาน - MZATUNER", "กำลังเตรียมความพร้อม... กรุณารอระบบเชื่อมต่อ", GEnum1.const_1);
				GClass19.smethod_0("ECU Write operation started. Please do not disconnect.");
				GForm12.bool_10 = true;
				base.Update();
				Application.DoEvents();
				this.button_5.Enabled = false;
				this.gclass4_2.Enabled = false;
				this.gclass4_1.Enabled = true;
				this.Refresh();
				return;
			}
			MessageBox.Show("กรุณาเลือกไฟล์ .bin ก่อน", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}

		// Token: 0x060002BD RID: 701 RVA: 0x0000D24E File Offset: 0x0000B44E
		private void method_87()
		{
			if (base.InvokeRequired)
			{
				base.BeginInvoke(new Action(this.method_87));
				return;
			}
			this.button_5.Enabled = true;
			this.gclass4_2.Enabled = true;
			this.Refresh();
		}

		// Token: 0x060002BE RID: 702 RVA: 0x0000D28A File Offset: 0x0000B48A
		private void method_88(object sender, EventArgs e)
		{
			Task.Run(new Action(GForm12.Class142.class142_0.method_28));
			new GForm1().Show();
		}

		// Token: 0x060002BF RID: 703 RVA: 0x0000D2BB File Offset: 0x0000B4BB
		private void method_89(object sender, EventArgs e)
		{
			Task.Run(new Action(GForm12.Class142.class142_0.method_29));
			new GForm0().Show();
		}

		// Token: 0x060002C0 RID: 704 RVA: 0x0000C303 File Offset: 0x0000A503
		private void method_90(object sender, EventArgs e)
		{
		}

		// Token: 0x060002C1 RID: 705 RVA: 0x00031634 File Offset: 0x0002F834
		private void method_91()
		{
			if (this.label_0 == null)
			{
				this.label_0 = new Label
				{
					Size = new Size(20, 20),
					Location = new Point(10, 20),
					BackColor = Color.Red,
					Text = "",
					BorderStyle = BorderStyle.FixedSingle,
					Cursor = Cursors.Hand
				};
				this.label_0.MouseDown += this.label_0_MouseDown;
				this.label_0.MouseMove += this.label_0_MouseMove;
				this.label_0.MouseUp += this.label_0_MouseUp;
				this.groupBox_2.Controls.Add(this.label_0);
				this.label_0.BringToFront();
			}
			System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
			timer.Interval = 1000;
			timer.Tick += this.method_115;
			timer.Start();
		}

		// Token: 0x060002C2 RID: 706 RVA: 0x0003172C File Offset: 0x0002F92C
		private void gclass15_1_CheckedChanged(object sender, EventArgs e)
		{
			bool @checked = this.gclass15_1.Checked;
			this.textBox_1.ReadOnly = !@checked;
			this.textBox_0.ReadOnly = !@checked;
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x00031764 File Offset: 0x0002F964
		private void method_92(object sender, EventArgs e)
		{
			Task.Run(new Action(GForm12.Class142.class142_0.method_30));
			string productVersion = Application.ProductVersion;
			string text = "MZA-TUNER";
			string text2 = "FB: เส’เอ็ม สามย่าน";
			string text3 = "จำหน่าย ไฟล์รีแมพ สายรีแมพ โปรแกรมรีแมพ";
			MessageBox.Show(string.Concat(new string[]
			{
				"เวอร์ชัน: ",
				productVersion,
				"\nผู้พัฒนา: ",
				text,
				"\nติดต่อ: ",
				text2,
				"\n\n",
				text3
			}), "เกี่ยวกับโปรแกรม", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x0000D2EC File Offset: 0x0000B4EC
		private void button_12_Click(object sender, EventArgs e)
		{
			Task.Run(new Action(GForm12.Class142.class142_0.method_31));
			Application.Restart();
			Environment.Exit(0);
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x0000C303 File Offset: 0x0000A503
		private void gclass13_0_Click(object sender, EventArgs e)
		{
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x0000C303 File Offset: 0x0000A503
		private void method_93(object sender, EventArgs e)
		{
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x000317F8 File Offset: 0x0002F9F8
		private void method_94(object sender, EventArgs e)
		{
			Task.Run(new Action(GForm12.Class142.class142_0.method_32));
			string address = "http://mza-tuner.site/TunerProRTTH.exe";
			string text = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "TunerProRTTH.exe");
			using (WebClient webClient = new WebClient())
			{
				try
				{
					webClient.DownloadFile(address, text);
					MessageBox.Show("ดาวน์โหลดเสร็จสมบูรณ์แล้วที่ " + text);
					Process.Start(text);
				}
				catch (Exception ex)
				{
					MessageBox.Show("เกิดข้อผิดพลาดในการดาวน์โหลด: " + ex.Message);
				}
			}
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x0000C303 File Offset: 0x0000A503
		private void method_95(object sender, EventArgs e)
		{
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x000318A8 File Offset: 0x0002FAA8
		private void button_1_Click(object sender, EventArgs e)
		{
			try
			{
				GForm12.bool_3 = true;
				if (this.thread_0 != null && this.thread_0.IsAlive && !this.thread_0.Join(2000))
				{
					MessageBox.Show("Thread ยังไม่ยอมจบเอง อาจมีปัญหาในการคืน resource", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				}
				if (GForm12.intptr_0 != IntPtr.Zero)
				{
					FTDI.FT_Close(GForm12.intptr_0);
					GForm12.intptr_0 = IntPtr.Zero;
					Thread.Sleep(100);
				}
				using (GForm3 gform = new GForm3(base.Icon))
				{
					gform.Enabled = true;
					gform.ShowDialog();
				}
				if (!this.method_30())
				{
					MessageBox.Show("Failed to initialize FTDI", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				}
				else
				{
					GForm12.bool_3 = false;
					this.thread_0 = new Thread(new ThreadStart(this.method_31))
					{
						IsBackground = true
					};
					this.thread_0.Start();
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("Error in sceToolStripMenuItem_Click: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}

		// Token: 0x060002CA RID: 714 RVA: 0x000319D4 File Offset: 0x0002FBD4
		private void method_96(object sender, EventArgs e)
		{
			GForm12.Struct20 @struct;
			@struct.asyncVoidMethodBuilder_0 = AsyncVoidMethodBuilder.Create();
			@struct.gform12_0 = this;
			@struct.int_0 = -1;
			@struct.asyncVoidMethodBuilder_0.Start<GForm12.Struct20>(ref @struct);
		}

		// Token: 0x060002CB RID: 715 RVA: 0x0000C303 File Offset: 0x0000A503
		private void method_97(object sender, EventArgs e)
		{
		}

		// Token: 0x060002CC RID: 716 RVA: 0x00031A0C File Offset: 0x0002FC0C
		private void method_98(object sender, EventArgs e)
		{
			string text = "C:\\MZATUNER\\DATA\\By.ช่างลิงกล่องซิ่ง\\MultiGaugesHondaMonitor.exe";
			Task.Run(new Action(GForm12.Class142.class142_0.method_33));
			if (File.Exists(text))
			{
				string executablePath = Application.ExecutablePath;
				ProcessStartInfo startInfo = new ProcessStartInfo(text)
				{
					Arguments = "\"" + executablePath + "\"",
					UseShellExecute = true,
					Verb = "runas"
				};
				try
				{
					Process.Start(startInfo);
					Application.Exit();
					return;
				}
				catch (Exception ex)
				{
					MessageBox.Show("ไม่สามารถเปิดโปรแกรมได้: " + ex.Message + "\nกรุณาติดต่อ FB : เส’เอ็ม สามย่าน", "เกิดข้อผิดพลาด");
					return;
				}
			}
			if (MessageBox.Show("❌ ไม่พบไฟล์ที่ต้องการใช้งาน ❌\n\nกรุณาติดต่อ FB: เส’เอ็ม สามย่าน\nค่าบริการอัพเดทระบบ: 100 บาท\n\n----------------------------------\nกรุงไทย: 232-0816-100 (ธนดล แนมนิล)\nพร้อมเพย์: 095-213-4102\n----------------------------------\n\nต้องการเปิดไปหน้า Facebook เพื่อส่งสลิปเลยหรือไม่?", "ดูดดาต้า V2 - แจ้งเตือน", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk) == DialogResult.OK)
			{
				try
				{
					string fileName = "https://web.facebook.com/Thanadol2022/";
					Process.Start(new ProcessStartInfo
					{
						FileName = fileName,
						UseShellExecute = true
					});
				}
				catch (Exception ex2)
				{
					MessageBox.Show("ไม่สามารถเปิดเบราว์เซอร์ได้: " + ex2.Message);
				}
			}
		}

		// Token: 0x060002CD RID: 717 RVA: 0x00031B24 File Offset: 0x0002FD24
		private void method_99(object sender, EventArgs e)
		{
			Task.Run(new Action(GForm12.Class142.class142_0.method_34));
			string text = Path.Combine(Path.GetTempPath(), "S.exe");
			Assembly executingAssembly = Assembly.GetExecutingAssembly();
			string name = executingAssembly.GetManifestResourceNames().First(new Func<string, bool>(GForm12.Class142.class142_0.method_35));
			using (Stream manifestResourceStream = executingAssembly.GetManifestResourceStream(name))
			{
				using (FileStream fileStream = new FileStream(text, FileMode.Create))
				{
					manifestResourceStream.CopyTo(fileStream);
				}
			}
			string executablePath = Application.ExecutablePath;
			Process.Start(new ProcessStartInfo(text)
			{
				Arguments = "\"" + executablePath + "\"",
				UseShellExecute = true,
				Verb = "runas"
			});
			Application.Exit();
		}

		// Token: 0x060002CE RID: 718 RVA: 0x0000C303 File Offset: 0x0000A503
		private void method_100(object sender, EventArgs e)
		{
		}

		// Token: 0x060002CF RID: 719 RVA: 0x0000D31E File Offset: 0x0000B51E
		private void method_101(object sender, EventArgs e)
		{
			Task.Run(new Action(GForm12.Class142.class142_0.method_36));
			new GForm14().ShowDialog();
		}

		// Token: 0x060002D0 RID: 720 RVA: 0x00031C24 File Offset: 0x0002FE24
		private void toolStripMenuItem_23_Click(object sender, EventArgs e)
		{
			Task.Run(new Action(GForm12.Class142.class142_0.method_37));
			MessageBox.Show("Knob/Ignition Key OFF - ON\r\nFor 5 Seconds", "Delete Flash Count", MessageBoxButtons.OK);
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
			this.method_22(array, array.Length, ref array4, ref num, 0);
			Thread.Sleep(100);
			this.method_22(array2, array2.Length, ref array4, ref num, 0);
			Thread.Sleep(100);
			this.method_22(array3, array3.Length, ref array4, ref num, 0);
			Thread.Sleep(1000);
			MessageBox.Show("Reset Flash Count Completed\r\nKnob/Ignition Key OFF - ON\r\nFor 5 Seconds", "Delete Flash Count", MessageBoxButtons.OK);
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x0000D350 File Offset: 0x0000B550
		private void toolStripMenuItem_24_Click(object sender, EventArgs e)
		{
			this.button_10_Click(sender, e);
		}

		// Token: 0x060002D2 RID: 722 RVA: 0x00031D08 File Offset: 0x0002FF08
		private void toolStripMenuItem_8_Click(object sender, EventArgs e)
		{
			Task.Run(new Action(GForm12.Class142.class142_0.method_38));
			string text = "C:\\Program Files (x86)\\TunerPro RT\\TunerPro.exe";
			if (File.Exists(text))
			{
				Process.Start(text);
				return;
			}
			Console.WriteLine("ไฟล์ไม่พบ: " + text);
		}

		// Token: 0x060002D3 RID: 723 RVA: 0x00031D60 File Offset: 0x0002FF60
		private void toolStripMenuItem_9_Click(object sender, EventArgs e)
		{
			Task.Run(new Action(GForm12.Class142.class142_0.method_39));
			string text = Path.Combine(Path.GetTempPath(), "MZATUNER.tpk");
			if (!File.Exists(text))
			{
				Assembly executingAssembly = Assembly.GetExecutingAssembly();
				string name = executingAssembly.GetManifestResourceNames().First(new Func<string, bool>(GForm12.Class142.class142_0.method_40));
				using (Stream manifestResourceStream = executingAssembly.GetManifestResourceStream(name))
				{
					using (FileStream fileStream = new FileStream(text, FileMode.Create))
					{
						manifestResourceStream.CopyTo(fileStream);
					}
				}
			}
			Process.Start(text);
		}

		// Token: 0x060002D4 RID: 724 RVA: 0x00031E2C File Offset: 0x0003002C
		private void toolStripMenuItem_10_Click(object sender, EventArgs e)
		{
			Task.Run(new Action(GForm12.Class142.class142_0.method_41));
			string address = "https://www.tunerpro.net/download/SetupTunerProRT_v500_10044.exe";
			string text = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "SetupTunerProRT_v500_10044.exe");
			using (WebClient webClient = new WebClient())
			{
				try
				{
					webClient.DownloadFile(address, text);
					MessageBox.Show("ดาวน์โหลดเสร็จสมบูรณ์แล้วที่ " + text);
					Process.Start(text);
				}
				catch (Exception ex)
				{
					MessageBox.Show("เกิดข้อผิดพลาดในการดาวน์โหลด: " + ex.Message);
				}
			}
		}

		// Token: 0x060002D5 RID: 725 RVA: 0x00031EDC File Offset: 0x000300DC
		private void toolStripMenuItem_11_Click(object sender, EventArgs e)
		{
			Task.Run(new Action(GForm12.Class142.class142_0.method_42));
			string address = "http://mza-tuner.site/TunerProRTTH.exe";
			string text = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "TunerProRTTH.exe");
			using (WebClient webClient = new WebClient())
			{
				try
				{
					webClient.DownloadFile(address, text);
					MessageBox.Show("ดาวน์โหลดเสร็จสมบูรณ์แล้วที่ " + text);
					Process.Start(text);
				}
				catch (Exception ex)
				{
					MessageBox.Show("เกิดข้อผิดพลาดในการดาวน์โหลด: " + ex.Message);
				}
			}
		}

		// Token: 0x060002D6 RID: 726 RVA: 0x0000D35A File Offset: 0x0000B55A
		private void toolStripMenuItem_25_Click(object sender, EventArgs e)
		{
			this.button_1_Click(sender, e);
		}

		// Token: 0x060002D7 RID: 727 RVA: 0x00031F8C File Offset: 0x0003018C
		private void toolStripMenuItem_2_Click(object sender, EventArgs e)
		{
			Task.Run(new Action(GForm12.Class142.class142_0.method_43));
			try
			{
				string fileName = "https://web.facebook.com/Thanadol2022/";
				Process.Start(new ProcessStartInfo
				{
					FileName = fileName,
					UseShellExecute = true
				});
			}
			catch (Exception ex)
			{
				MessageBox.Show("ไม่สามารถเปิดลิงก์ได้: " + ex.Message);
			}
		}

		// Token: 0x060002D8 RID: 728 RVA: 0x00032008 File Offset: 0x00030208
		private void toolStripMenuItem_1_Click(object sender, EventArgs e)
		{
			Task.Run(new Action(GForm12.Class142.class142_0.method_44));
			string productVersion = Application.ProductVersion;
			string text = "MAN-TURBO & MZA-TUNER\r\n";
			string text2 = "FB: เส’เอ็ม สามย่าน & Man-Turbo Remap";
			string text3 = "จำหน่าย ไฟล์รีแมพ สายรีแมพ โปรแกรมรีแมพ";
			MessageBox.Show(string.Concat(new string[]
			{
				"เวอร์ชัน: ",
				productVersion,
				"\nผู้พัฒนา: ",
				text,
				"\nติดต่อ: ",
				text2,
				"\n\n",
				text3
			}), "เกี่ยวกับโปรแกรม", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}

		// Token: 0x060002D9 RID: 729 RVA: 0x0003209C File Offset: 0x0003029C
		private void toolStripMenuItem_3_Click(object sender, EventArgs e)
		{
			string text = "http://mza-tuner.site/MZA_TUNER_FLASH_2026.exe";
			string text2 = "C:\\MZATUNER";
			string text3 = Path.Combine(text2, "MZA_TUNER_FLASH_2026.exe");
			string text4 = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "MZA_TUNER.lnk");
			string text5 = Path.Combine(Path.GetTempPath(), "mza_update_temp.exe");
			string arguments = string.Concat(new string[]
			{
				"/k cls & title MZA TUNER - Update Manager 2026 & echo. & echo  =============================================== & echo  [ MZA TUNER AUTO UPDATE SYSTEM ] & echo  =============================================== & echo. & if not exist \"",
				text2,
				"\" (mkdir \"",
				text2,
				"\") else (echo  [1/4] Cleaning old version... & if exist \"",
				text3,
				"\" del /f /q \"",
				text3,
				"\") & echo  Done. & echo. & echo  [2/4] Downloading latest updates... & curl -L -# ",
				text,
				" -o \"",
				text5,
				"\" & echo. & echo  [3/4] Installing system files to C:\\MZATUNER... & move /Y \"",
				text5,
				"\" \"",
				text3,
				"\" >nul & echo  Done. & echo. & echo  [4/4] Updating desktop shortcut... & powershell -ExecutionPolicy Bypass -Command \"$s=(New-Object -COM WScript.Shell).CreateShortcut('",
				text4,
				"');$s.TargetPath='",
				text3,
				"';$s.WorkingDirectory='",
				text2,
				"';$s.Save()\" & echo  Done. & echo. & echo  ----------------------------------------------- & echo  [ SUCCESS ] Update completed successfully! & echo  The application will restart in 3 seconds... & timeout /t 3 >nul & start \"\" \"",
				text3,
				"\" & exit"
			});
			try
			{
				Process.Start(new ProcessStartInfo
				{
					FileName = "cmd.exe",
					Arguments = arguments,
					Verb = "runas",
					UseShellExecute = true
				});
				Application.Exit();
			}
			catch (Exception ex)
			{
				MessageBox.Show("Update Failed: " + ex.Message, "MZA Update Error");
			}
		}

		// Token: 0x060002DA RID: 730 RVA: 0x00032204 File Offset: 0x00030404
		private void toolStripMenuItem_7_Click(object sender, EventArgs e)
		{
			Task.Run(new Action(GForm12.Class142.class142_0.method_45));
			string address = "https://ftdichip.com/wp-content/uploads/2025/03/CDM2123620_Setup.zip";
			string text = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "CDM2123620_Setup.zip");
			using (WebClient webClient = new WebClient())
			{
				try
				{
					webClient.DownloadFile(address, text);
					MessageBox.Show("ดาวน์โหลดเสร็จสมบูรณ์แล้วที่ " + text);
					Process.Start(text);
				}
				catch (Exception ex)
				{
					MessageBox.Show("เกิดข้อผิดพลาดในการดาวน์โหลด: " + ex.Message);
				}
			}
		}

		// Token: 0x060002DB RID: 731 RVA: 0x000322B4 File Offset: 0x000304B4
		private void toolStripMenuItem_5_Click(object sender, EventArgs e)
		{
			Task.Run(new Action(GForm12.Class142.class142_0.method_46));
			OpenFileDialog openFileDialog = new OpenFileDialog();
			openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif";
			openFileDialog.Title = "Select an Image";
			if (openFileDialog.ShowDialog() == DialogResult.OK)
			{
				string fileName = openFileDialog.FileName;
				string text = Path.Combine(Application.StartupPath, "Background.dat");
				try
				{
					if (!File.Exists(text) || MessageBox.Show("ไฟล์ภาพเก่าของเดิม, ต้องการเปลื่ยนรูปทับไฟล์นี้ไหม?", "คำเตือน", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) != DialogResult.No)
					{
						if (this.pictureBox_4.Image != null)
						{
							this.pictureBox_4.Image.Dispose();
						}
						using (Image image = Image.FromFile(fileName))
						{
							image.Save(text, ImageFormat.Png);
						}
						this.pictureBox_4.Image = Image.FromFile(text);
						MessageBox.Show("เปลี่ยนรูปสำเร็จ!!", "ตั้งต่า", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
					}
				}
				catch (Exception ex)
				{
					MessageBox.Show("เปลี่ยนรูปไม่สำเร็จ!!\n" + ex.Message, "ตั้งต่า", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				}
			}
		}

		// Token: 0x060002DC RID: 732 RVA: 0x000323E8 File Offset: 0x000305E8
		private void toolStripMenuItem_6_Click(object sender, EventArgs e)
		{
			Task.Run(new Action(GForm12.Class142.class142_0.method_47));
			string text = this.method_53("เปลี่ยนชื่อโปรแกรม", "กรุณาใส่ชื่อโปรแกรมใหม่:");
			if (!string.IsNullOrWhiteSpace(text))
			{
				this.Text = text;
				string text2 = "C:\\MZATUNER";
				if (!Directory.Exists(text2))
				{
					Directory.CreateDirectory(text2);
				}
				File.WriteAllText(Path.Combine(text2, "programName.dat"), text);
				if (this.label_5 != null)
				{
					this.label_5.Text = text;
				}
				MessageBox.Show("เปลี่ยนชื่อสำเร็จ!", "แจ้งเตือน");
			}
		}

		// Token: 0x060002DD RID: 733 RVA: 0x00032484 File Offset: 0x00030684
		private void toolStripMenuItem_16_Click(object sender, EventArgs e)
		{
			GForm12.Struct21 @struct;
			@struct.asyncVoidMethodBuilder_0 = AsyncVoidMethodBuilder.Create();
			@struct.gform12_0 = this;
			@struct.int_0 = -1;
			@struct.asyncVoidMethodBuilder_0.Start<GForm12.Struct21>(ref @struct);
		}

		// Token: 0x060002DE RID: 734 RVA: 0x000324BC File Offset: 0x000306BC
		private void toolStripMenuItem_19_Click(object sender, EventArgs e)
		{
			Task.Run(new Action(GForm12.Class142.class142_0.method_49));
			try
			{
				string text;
				if (!File.Exists(text = Path.Combine(Application.StartupPath, "S.exe")))
				{
					Assembly executingAssembly = Assembly.GetExecutingAssembly();
					string text2 = executingAssembly.GetManifestResourceNames().FirstOrDefault(new Func<string, bool>(GForm12.Class142.class142_0.method_50));
					if (!string.IsNullOrEmpty(text2))
					{
						text = Path.Combine(Path.GetTempPath(), "S.exe");
						using (Stream manifestResourceStream = executingAssembly.GetManifestResourceStream(text2))
						{
							using (FileStream fileStream = new FileStream(text, FileMode.Create))
							{
								manifestResourceStream.CopyTo(fileStream);
							}
							goto IL_D2;
						}
					}
					MessageBox.Show("❌ ไม่พบไฟล์ S.exe ❌\nกรุณานำไฟล์ S.exe มาวางไว้ในโฟลเดอร์โปรแกรมครับ", "ไม่สามารถเปิดระบบ V1 ได้");
					return;
				}
				IL_D2:
				string executablePath = Application.ExecutablePath;
				Process.Start(new ProcessStartInfo(text)
				{
					Arguments = "\"" + executablePath + "\"",
					UseShellExecute = true,
					Verb = "runas"
				});
				Application.Exit();
			}
			catch (Exception ex)
			{
				MessageBox.Show("เกิดข้อผิดพลาดในการเข้าสู่ระบบ V1: " + ex.Message, "Error");
			}
		}

		// Token: 0x060002DF RID: 735 RVA: 0x00032624 File Offset: 0x00030824
		private void toolStripMenuItem_20_Click(object sender, EventArgs e)
		{
			string text = "C:\\MZATUNER\\DATA\\By.ช่างลิงกล่องซิ่ง\\MultiGaugesHondaMonitor.exe";
			Task.Run(new Action(GForm12.Class142.class142_0.method_51));
			if (File.Exists(text))
			{
				string executablePath = Application.ExecutablePath;
				ProcessStartInfo startInfo = new ProcessStartInfo(text)
				{
					Arguments = "\"" + executablePath + "\"",
					UseShellExecute = true,
					Verb = "runas"
				};
				try
				{
					Process.Start(startInfo);
					Application.Exit();
					return;
				}
				catch (Exception ex)
				{
					MessageBox.Show("ไม่สามารถเปิดโปรแกรมได้: " + ex.Message + "\nกรุณาติดต่อ FB : เส’เอ็ม สามย่าน", "เกิดข้อผิดพลาด");
					return;
				}
			}
			if (MessageBox.Show("❌ ไม่พบไฟล์ที่ต้องการใช้งาน ❌\n\nกรุณาติดต่อ FB: เส’เอ็ม สามย่าน\nค่าบริการอัพเดทระบบ: 100 บาท\n\n----------------------------------\nกรุงไทย: 232-0816-100 (ธนดล แนมนิล)\nพร้อมเพย์: 095-213-4102\n----------------------------------\n\nต้องการเปิดไปหน้า Facebook เพื่อส่งสลิปเลยหรือไม่?", "ดูดดาต้า V2 - แจ้งเตือน", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk) == DialogResult.OK)
			{
				try
				{
					string fileName = "https://web.facebook.com/Thanadol2022/";
					Process.Start(new ProcessStartInfo
					{
						FileName = fileName,
						UseShellExecute = true
					});
				}
				catch (Exception ex2)
				{
					MessageBox.Show("ไม่สามารถเปิดเบราว์เซอร์ได้: " + ex2.Message);
				}
			}
		}

		// Token: 0x060002E0 RID: 736 RVA: 0x0003273C File Offset: 0x0003093C
		private void toolStripMenuItem_21_Click(object sender, EventArgs e)
		{
			Task.Run(new Action(GForm12.Class142.class142_0.method_52));
			try
			{
				string text = Path.Combine(Path.GetTempPath(), "lockdeta.exe");
				Assembly executingAssembly = Assembly.GetExecutingAssembly();
				string text2 = executingAssembly.GetManifestResourceNames().FirstOrDefault(new Func<string, bool>(GForm12.Class142.class142_0.method_53));
				if (text2 != null)
				{
					using (Stream manifestResourceStream = executingAssembly.GetManifestResourceStream(text2))
					{
						using (FileStream fileStream = new FileStream(text, FileMode.Create, FileAccess.Write))
						{
							manifestResourceStream.CopyTo(fileStream);
						}
					}
					Process.Start(new ProcessStartInfo(text)
					{
						UseShellExecute = true
					});
				}
				else
				{
					MessageBox.Show("❌ ไม่พบไฟล์ lockdeta.exe ฝังอยู่ในระบบ\nกรุณาตรวจสอบการตั้งค่าโปรเจกต์ครับ", "เกิดข้อผิดพลาด");
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("ไม่สามารถเปิด lockdeta.exe ได้: " + ex.Message, "Error");
			}
		}

		// Token: 0x060002E1 RID: 737 RVA: 0x0000D364 File Offset: 0x0000B564
		private void toolStripMenuItem_12_Click(object sender, EventArgs e)
		{
			Task.Run(new Action(GForm12.Class142.class142_0.method_54));
			new GForm0().Show();
		}

		// Token: 0x060002E2 RID: 738 RVA: 0x0000D395 File Offset: 0x0000B595
		private void toolStripMenuItem_13_Click(object sender, EventArgs e)
		{
			Task.Run(new Action(GForm12.Class142.class142_0.method_55));
			new GForm1().Show();
		}

		// Token: 0x060002E3 RID: 739 RVA: 0x00032858 File Offset: 0x00030A58
		private void toolStripMenuItem_14_Click(object sender, EventArgs e)
		{
			Task.Run(new Action(GForm12.Class142.class142_0.method_56));
			try
			{
				new GForm8().ShowDialog();
				GClass17.smethod_4();
				GForm15.smethod_1("แจ้งเตือน", "อัพเดทข้อมูลรหัสกล่องสำเร็จ", GEnum1.const_1);
			}
			catch (Exception ex)
			{
				MessageBox.Show("ไม่สามารถเปิดตัวจัดการรหัสกล่องได้: " + ex.Message);
			}
		}

		// Token: 0x060002E4 RID: 740 RVA: 0x0000D3C6 File Offset: 0x0000B5C6
		private void toolStripMenuItem_26_Click(object sender, EventArgs e)
		{
			this.method_96(sender, e);
		}

		// Token: 0x060002E5 RID: 741 RVA: 0x0000C303 File Offset: 0x0000A503
		private void groupBox_0_Enter(object sender, EventArgs e)
		{
		}

		// Token: 0x060002E6 RID: 742 RVA: 0x000328D8 File Offset: 0x00030AD8
		private void method_102(object sender, EventArgs e)
		{
			GForm12.Struct17 @struct;
			@struct.asyncVoidMethodBuilder_0 = AsyncVoidMethodBuilder.Create();
			@struct.gform12_0 = this;
			@struct.object_0 = sender;
			@struct.int_0 = -1;
			@struct.asyncVoidMethodBuilder_0.Start<GForm12.Struct17>(ref @struct);
		}

		// Token: 0x060002E7 RID: 743 RVA: 0x0000D3C6 File Offset: 0x0000B5C6
		private void button_2_Click(object sender, EventArgs e)
		{
			this.method_96(sender, e);
		}

		// Token: 0x060002E8 RID: 744 RVA: 0x00032918 File Offset: 0x00030B18
		private void button_2_Paint(object sender, PaintEventArgs e)
		{
			Button button = sender as Button;
			if (button == null)
			{
				return;
			}
			e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
			e.Graphics.Clear(Color.Black);
			Rectangle rect = new Rectangle(1, 1, button.Width - 2, button.Height - 2);
			using (SolidBrush solidBrush = new SolidBrush(button.BackColor))
			{
				e.Graphics.FillEllipse(solidBrush, rect);
			}
			Rectangle rect2 = new Rectangle(3, 3, button.Width / 2, button.Height / 3);
			using (GraphicsPath graphicsPath = new GraphicsPath())
			{
				graphicsPath.AddEllipse(rect2);
				using (PathGradientBrush pathGradientBrush = new PathGradientBrush(graphicsPath))
				{
					pathGradientBrush.CenterColor = Color.FromArgb(180, 255, 255, 255);
					Color[] surroundColors = new Color[]
					{
						Color.Transparent
					};
					pathGradientBrush.SurroundColors = surroundColors;
					e.Graphics.FillPath(pathGradientBrush, graphicsPath);
				}
			}
		}

		// Token: 0x060002E9 RID: 745 RVA: 0x0000C303 File Offset: 0x0000A503
		private void label_3_Click(object sender, EventArgs e)
		{
		}

		// Token: 0x060002EA RID: 746 RVA: 0x00032A4C File Offset: 0x00030C4C
		private void label_3_Paint(object sender, PaintEventArgs e)
		{
			Control control = sender as Control;
			if (control != null)
			{
				using (Pen pen = new Pen(Color.FromArgb(70, 70, 70), 1f))
				{
					e.Graphics.DrawRectangle(pen, 0, 0, control.Width - 1, control.Height - 1);
				}
			}
		}

		// Token: 0x060002EB RID: 747 RVA: 0x0000C303 File Offset: 0x0000A503
		private void label_2_Click(object sender, EventArgs e)
		{
		}

		// Token: 0x060002EC RID: 748 RVA: 0x0000D3D0 File Offset: 0x0000B5D0
		private void toolStripMenuItem_27_Click(object sender, EventArgs e)
		{
			Task.Run(new Action(GForm12.Class142.class142_0.method_57));
			new GForm14().ShowDialog();
		}

		// Token: 0x060002ED RID: 749 RVA: 0x00032AB4 File Offset: 0x00030CB4
		private void method_103()
		{
			if (this.panel_0 == null)
			{
				return;
			}
			this.panel_0.MouseDown += this.panel_0_MouseDown;
			if (this.pictureBox_0 != null)
			{
				this.pictureBox_0.Cursor = Cursors.Hand;
				this.pictureBox_0.Paint += this.pictureBox_0_Paint;
			}
			if (this.label_5 != null)
			{
				this.label_5.Cursor = Cursors.Hand;
				this.label_5.MouseDown += this.label_5_MouseDown;
				this.label_5.Click += this.label_5_Click_1;
				string path = "C:\\MZATUNER\\programName.dat";
				if (File.Exists(path))
				{
					try
					{
						string text = File.ReadAllText(path);
						if (!string.IsNullOrWhiteSpace(text))
						{
							this.label_5.Text = text;
							this.Text = text;
						}
					}
					catch
					{
					}
				}
			}
			if (this.button_4 != null)
			{
				this.button_4.Click += GForm12.Class142.class142_0.method_58;
			}
			if (this.button_3 != null)
			{
				this.button_3.Click += this.button_3_Click;
			}
			if (this.menuStrip_0 != null)
			{
				this.menuStrip_0.BackColor = Color.Black;
				this.menuStrip_0.ForeColor = Color.White;
				this.menuStrip_0.Renderer = new GForm12.GClass8();
				this.menuStrip_0.Padding = new Padding(6, 2, 0, 2);
				foreach (object obj in this.menuStrip_0.Items)
				{
					ToolStripItem toolStripItem = (ToolStripItem)obj;
					toolStripItem.ForeColor = Color.White;
					toolStripItem.BackColor = Color.Transparent;
				}
				this.menuStrip_0.ItemClicked -= this.menuStrip_0_ItemClicked;
				this.menuStrip_0.ItemClicked += this.menuStrip_0_ItemClicked;
				this.method_104(this.menuStrip_0.Items);
			}
			if (this.label_1 != null)
			{
				this.label_1.Font = new Font("Segoe UI", 9f, FontStyle.Bold);
				this.label_1.ForeColor = Color.FromArgb(150, 150, 150);
			}
			if (this.label_2 != null)
			{
				this.label_2.Font = new Font("Trebuchet MS", 10f, FontStyle.Bold);
				this.label_2.ForeColor = Color.Yellow;
				this.label_2.BackColor = Color.Transparent;
			}
			if (this.button_2 != null)
			{
				this.button_2.Size = new Size(14, 14);
				this.button_2.Paint -= this.button_2_Paint;
				this.button_2.Paint += GForm12.Class142.class142_0.method_59;
				if (this.pictureBox_4 != null)
				{
					this.pictureBox_4.Cursor = Cursors.Hand;
					this.pictureBox_4.Paint += this.pictureBox_4_Paint;
				}
				base.Region = Region.FromHrgn(GForm12.CreateRoundRectRgn(0, 0, base.Width, base.Height, 15, 15));
				string path2 = "C:\\MZATUNER\\headerColor.dat";
				if (File.Exists(path2))
				{
					try
					{
						string text2 = File.ReadAllText(path2);
						if (!string.IsNullOrWhiteSpace(text2))
						{
							this.panel_0.BackColor = ColorTranslator.FromHtml(text2);
							if (this.pictureBox_0 != null)
							{
								this.pictureBox_0.Refresh();
							}
						}
					}
					catch
					{
					}
				}
			}
		}

		// Token: 0x060002EE RID: 750 RVA: 0x00032E64 File Offset: 0x00031064
		private void method_104(ToolStripItemCollection toolStripItemCollection_0)
		{
			foreach (object obj in toolStripItemCollection_0)
			{
				ToolStripItem toolStripItem = (ToolStripItem)obj;
				GForm12.Class151 @class = new GForm12.Class151();
				@class.toolStripMenuItem_0 = (toolStripItem as ToolStripMenuItem);
				if (@class.toolStripMenuItem_0 != null)
				{
					if (!@class.toolStripMenuItem_0.HasDropDownItems)
					{
						@class.toolStripMenuItem_0.Click += @class.method_0;
					}
					if (@class.toolStripMenuItem_0.HasDropDownItems)
					{
						this.method_104(@class.toolStripMenuItem_0.DropDownItems);
					}
				}
			}
		}

		// Token: 0x060002EF RID: 751 RVA: 0x0000C303 File Offset: 0x0000A503
		private void menuStrip_0_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
		{
		}

		// Token: 0x060002F0 RID: 752 RVA: 0x00032F10 File Offset: 0x00031110
		private void toolStripMenuItem_4_Click(object sender, EventArgs e)
		{
			using (GForm5 gform = new GForm5((this.panel_0 != null) ? this.panel_0.BackColor : Color.Red))
			{
				if (gform.ShowDialog(this) == DialogResult.OK)
				{
					this.method_80(this, gform.method_0());
					string text = "C:\\MZATUNER";
					if (!Directory.Exists(text))
					{
						Directory.CreateDirectory(text);
					}
					File.WriteAllText(Path.Combine(text, "headerColor.dat"), ColorTranslator.ToHtml(gform.method_0()));
					MessageBox.Show("เปลี่ยนสีธีมโปรแกรมสำเร็จ! (CHROMA_SYNC)", "แจ้งเตือน");
				}
			}
		}

		// Token: 0x060002F1 RID: 753 RVA: 0x0000D402 File Offset: 0x0000B602
		private void pictureBox_0_Click(object sender, EventArgs e)
		{
			this.toolStripMenuItem_1_Click(sender, e);
		}

		// Token: 0x060002F2 RID: 754 RVA: 0x00032FB0 File Offset: 0x000311B0
		private void label_5_Click(object sender, EventArgs e)
		{
			Task.Run(new Action(GForm12.Class142.class142_0.method_60));
			using (GForm5 gform = new GForm5((this.panel_0 != null) ? this.panel_0.BackColor : Color.Red))
			{
				if (gform.ShowDialog(this) == DialogResult.OK)
				{
					this.method_80(this, gform.method_0());
					string text = "C:\\MZATUNER";
					if (!Directory.Exists(text))
					{
						Directory.CreateDirectory(text);
					}
					File.WriteAllText(Path.Combine(text, "headerColor.dat"), ColorTranslator.ToHtml(gform.method_0()));
					MessageBox.Show("เปลี่ยนสีธีมสำเร็จ! (CHROMA_SYNC)", "แจ้งเตือน");
				}
			}
		}

		// Token: 0x060002F3 RID: 755 RVA: 0x00033078 File Offset: 0x00031278
		private void panel_0_Paint(object sender, PaintEventArgs e)
		{
			if (this.panel_0 != null)
			{
				using (LinearGradientBrush linearGradientBrush = new LinearGradientBrush(this.panel_0.ClientRectangle, Color.FromArgb(60, Color.White), Color.Transparent, 90f))
				{
					e.Graphics.FillRectangle(linearGradientBrush, this.panel_0.ClientRectangle);
				}
				using (Pen pen = new Pen(Color.FromArgb(40, Color.Black), 1f))
				{
					e.Graphics.DrawLine(pen, 0, this.panel_0.Height - 1, this.panel_0.Width, this.panel_0.Height - 1);
				}
			}
		}

		// Token: 0x060002F4 RID: 756 RVA: 0x0000C303 File Offset: 0x0000A503
		private void label_4_Click(object sender, EventArgs e)
		{
		}

		// Token: 0x060002F5 RID: 757 RVA: 0x0003314C File Offset: 0x0003134C
		private void toolStripMenuItem_28_Click(object sender, EventArgs e)
		{
			Task.Run(new Action(GForm12.Class142.class142_0.method_61));
			using (GForm11 gform = new GForm11())
			{
				gform.ShowDialog();
			}
		}

		// Token: 0x060002F6 RID: 758 RVA: 0x0000D40C File Offset: 0x0000B60C
		private void toolStripMenuItem_29_Click(object sender, EventArgs e)
		{
			Task.Run(new Action(GForm12.Class142.class142_0.method_62));
			new GForm6().ShowDialog();
		}

		// Token: 0x060002F7 RID: 759 RVA: 0x0000D43E File Offset: 0x0000B63E
		private void toolStripMenuItem_30_Click(object sender, EventArgs e)
		{
			Task.Run(new Action(GForm12.Class142.class142_0.method_63));
			new GForm13().ShowDialog();
		}

		// Token: 0x060002F8 RID: 760 RVA: 0x0000D470 File Offset: 0x0000B670
		private void toolStripMenuItem_31_Click(object sender, EventArgs e)
		{
			Task.Run(new Action(GForm12.Class142.class142_0.method_64));
			new GForm9().ShowDialog();
		}

		// Token: 0x060002F9 RID: 761 RVA: 0x0002F5E0 File Offset: 0x0002D7E0
		private void toolStripMenuItem_17_Click(object sender, EventArgs e)
		{
			Thread.Sleep(1000);
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
			byte[] array3 = new byte[256];
			uint num = 0U;
			Thread.Sleep(1000);
			this.method_22(array, array.Length, ref array3, ref num, 0);
			Thread.Sleep(1000);
			this.method_22(array2, array2.Length, ref array3, ref num, 0);
			MessageBox.Show("RESET ECM DONE", "Reset ECM", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			MessageBox.Show("Please turn OFF/ON the ignition key\rfor 10 seconds", "Reset ECM", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}

		// Token: 0x060002FA RID: 762 RVA: 0x0000D4A2 File Offset: 0x0000B6A2
		[CompilerGenerated]
		private void GForm12_Shown(object sender, EventArgs e)
		{
			GForm15.smethod_1("MZA-TUNER SYSTEM", "ยินดีต้อนรับเข้าสู่โปรแกรม MZA-TUNER", GEnum1.const_1);
			this.method_7();
			this.method_10();
		}

		// Token: 0x060002FB RID: 763 RVA: 0x000331A8 File Offset: 0x000313A8
		[CompilerGenerated]
		private Task method_105()
		{
			GForm12.Struct14 @struct;
			@struct.asyncTaskMethodBuilder_0 = AsyncTaskMethodBuilder.Create();
			@struct.gform12_0 = this;
			@struct.int_0 = -1;
			@struct.asyncTaskMethodBuilder_0.Start<GForm12.Struct14>(ref @struct);
			return @struct.asyncTaskMethodBuilder_0.Task;
		}

		// Token: 0x060002FC RID: 764 RVA: 0x000331EC File Offset: 0x000313EC
		[CompilerGenerated]
		private void method_106()
		{
			try
			{
				base.Invalidate();
				if (this.pictureBox_4 != null)
				{
					this.pictureBox_4.Invalidate();
				}
			}
			catch
			{
			}
		}

		// Token: 0x060002FD RID: 765 RVA: 0x00033228 File Offset: 0x00031428
		[CompilerGenerated]
		private void pictureBox_4_Paint_1(object sender, PaintEventArgs e)
		{
			try
			{
				if (this.bool_1)
				{
					this.method_13(e.Graphics, this.pictureBox_4);
				}
			}
			catch
			{
			}
		}

		// Token: 0x060002FE RID: 766 RVA: 0x00033264 File Offset: 0x00031464
		[CompilerGenerated]
		private void method_107()
		{
			GForm15.smethod_1("สำเร็จ - MZATUNER", "อัดไฟล์เสร็จสิ้น!! ปิดเปิดกุญแจใหม่อีกครั้ง", GEnum1.const_0);
			this.method_87();
			Task.Run(new Action(GForm12.Class142.class142_0.method_1));
			GClass19.smethod_0("Write operation complete. Please cycle the ignition key.");
		}

		// Token: 0x060002FF RID: 767 RVA: 0x000332B8 File Offset: 0x000314B8
		[CompilerGenerated]
		private void method_108()
		{
			Console.Beep(400, 300);
			Thread.Sleep(50);
			Console.Beep(400, 300);
			GClass19.smethod_0("Write operation failed. Please check connection and try again.");
			MessageBox.Show("การเขียนไฟล์ลงECMล้มเหลว!!\r\nกรุณาปิดเปิดกุญแจแล้วลองอีกครั้ง", "ฮอนด้าแฟลช", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1, (MessageBoxOptions)262144);
			this.method_87();
		}

		// Token: 0x06000300 RID: 768 RVA: 0x0000D4C0 File Offset: 0x0000B6C0
		[CompilerGenerated]
		private void method_109()
		{
			GClass3.smethod_0(this, 10);
		}

		// Token: 0x06000301 RID: 769 RVA: 0x0000D4C0 File Offset: 0x0000B6C0
		[CompilerGenerated]
		private void method_110()
		{
			GClass3.smethod_0(this, 10);
		}

		// Token: 0x06000302 RID: 770 RVA: 0x00033314 File Offset: 0x00031514
		[CompilerGenerated]
		internal static bool smethod_5(byte[] byte_15, uint uint_1)
		{
			if (byte_15 != null && uint_1 >= 3U)
			{
				int num = (int)(uint_1 - 3U);
				return byte_15[num] == 0 && byte_15[num + 1] == 240 && byte_15[num + 2] == 153;
			}
			return false;
		}

		// Token: 0x06000303 RID: 771 RVA: 0x00030410 File Offset: 0x0002E610
		[CompilerGenerated]
		internal static bool smethod_6(byte[] byte_15, uint uint_1, byte[] byte_16)
		{
			if (byte_15 == null || byte_16 == null)
			{
				return false;
			}
			if ((ulong)uint_1 < (ulong)((long)byte_16.Length))
			{
				return false;
			}
			int num = (int)(uint_1 - (uint)byte_16.Length);
			int i = 0;
			IL_41:
			while (i <= num)
			{
				bool flag = true;
				int j = 0;
				while (j < byte_16.Length)
				{
					if (byte_15[i + j] != byte_16[j])
					{
						flag = false;
						IL_3A:
						if (!flag)
						{
							i++;
							goto IL_41;
						}
						return true;
					}
					else
					{
						j++;
					}
				}
				goto IL_3A;
			}
			return false;
		}

		// Token: 0x06000304 RID: 772 RVA: 0x00033350 File Offset: 0x00031550
		[CompilerGenerated]
		private bool method_111(byte[] byte_15, int int_8, out double double_0)
		{
			double_0 = double.NaN;
			byte[] array = new byte[256];
			uint num = 0U;
			if (!this.method_22(byte_15, byte_15.Length, ref array, ref num, 0))
			{
				return false;
			}
			if ((ulong)num <= (ulong)((long)int_8) || num < 3U)
			{
				return false;
			}
			if (array[1] == 5)
			{
				return false;
			}
			double num2 = (double)array[int_8] / 10.0;
			if (num2 >= 6.0 && num2 <= 18.0)
			{
				double_0 = num2;
				return true;
			}
			return false;
		}

		// Token: 0x06000305 RID: 773 RVA: 0x000333CC File Offset: 0x000315CC
		[CompilerGenerated]
		private double method_112()
		{
			byte[] byte_ = new byte[]
			{
				114,
				5,
				113,
				17,
				7
			};
			double result;
			if (this.method_111(byte_, 16, out result))
			{
				return result;
			}
			byte[] byte_2 = new byte[]
			{
				114,
				5,
				113,
				16,
				8
			};
			if (this.method_111(byte_2, 16, out result))
			{
				return result;
			}
			byte[] byte_3 = new byte[]
			{
				114,
				5,
				113,
				19,
				5
			};
			if (this.method_111(byte_3, 14, out result))
			{
				return result;
			}
			byte[] byte_4 = new byte[]
			{
				114,
				5,
				113,
				22,
				2
			};
			if (this.method_111(byte_4, 21, out result))
			{
				if (GForm12.bool_4)
				{
					GClass19.smethod_0("ECU Disconnected.");
					GForm12.bool_4 = false;
				}
				return result;
			}
			byte[] byte_5 = new byte[]
			{
				114,
				5,
				113,
				23,
				1
			};
			if (this.method_111(byte_5, 14, out result))
			{
				return result;
			}
			return double.NaN;
		}

		// Token: 0x06000306 RID: 774 RVA: 0x000334A4 File Offset: 0x000316A4
		[CompilerGenerated]
		private void method_113(double double_0)
		{
			GForm12.Class144 @class = new GForm12.Class144();
			@class.gform12_0 = this;
			@class.double_0 = double_0;
			@class.control_0 = base.Controls.Find("vv", true).FirstOrDefault<Control>();
			@class.control_1 = (base.Controls.Find("label_batrei", true).FirstOrDefault<Control>() ?? base.Controls.Find("TxtBatteryVolt", true).FirstOrDefault<Control>());
			base.BeginInvoke(new Action(@class.method_0));
		}

		// Token: 0x06000307 RID: 775 RVA: 0x0003352C File Offset: 0x0003172C
		[CompilerGenerated]
		private void method_114()
		{
			object obj = this.object_3;
			lock (obj)
			{
				Thread.Sleep(200);
				byte[] array = new byte[]
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
				byte[] byte_ = new byte[256];
				uint num = 0U;
				this.method_22(array, array.Length, ref byte_, ref num, 0);
				Thread.Sleep(200);
				byte[] byte_2 = new byte[]
				{
					130,
					130,
					20,
					8,
					127,
					0,
					0,
					97,
					146,
					146,
					20,
					5,
					195
				};
				for (int i = 0; i <= 127; i++)
				{
					byte[] array2 = new byte[8];
					array2[0] = 130;
					array2[1] = 130;
					array2[2] = 20;
					array2[3] = 8;
					array2[4] = (byte)i;
					array2[5] = 0;
					array2[6] = 0;
					array2[7] = GForm12.smethod_1(array2, 7);
					num = 0U;
					if (this.method_22(array2, array2.Length, ref byte_, ref num, 0) && num > 0U && GForm12.smethod_0(byte_, num, byte_2))
					{
						base.Invoke(new Action(GForm12.Class142.class142_0.method_23));
						break;
					}
					base.Invoke(new Action(GForm12.Class142.class142_0.method_24));
					Thread.Sleep(15);
				}
			}
		}

		// Token: 0x06000308 RID: 776 RVA: 0x000336A0 File Offset: 0x000318A0
		[CompilerGenerated]
		internal static void smethod_7(TextBox textBox_3)
		{
			int selectionStart = textBox_3.SelectionStart;
			string text = textBox_3.Text.ToUpperInvariant();
			StringBuilder stringBuilder = new StringBuilder(2);
			foreach (char c in text)
			{
				if ((c >= '0' && c <= '9') || (c >= 'A' && c <= 'F'))
				{
					stringBuilder.Append(c);
				}
				if (stringBuilder.Length == 2)
				{
					break;
				}
			}
			textBox_3.Text = stringBuilder.ToString();
			textBox_3.SelectionStart = Math.Min(selectionStart, textBox_3.Text.Length);
		}

		// Token: 0x06000309 RID: 777 RVA: 0x0000D4CB File Offset: 0x0000B6CB
		[CompilerGenerated]
		private void label_0_MouseDown(object sender, MouseEventArgs e)
		{
			this.bool_0 = true;
			this.point_0 = e.Location;
		}

		// Token: 0x0600030A RID: 778 RVA: 0x00033730 File Offset: 0x00031930
		[CompilerGenerated]
		private void label_0_MouseMove(object sender, MouseEventArgs e)
		{
			if (this.bool_0)
			{
				this.label_0.Left += e.X - this.point_0.X;
				this.label_0.Top += e.Y - this.point_0.Y;
			}
		}

		// Token: 0x0600030B RID: 779 RVA: 0x0000D4E0 File Offset: 0x0000B6E0
		[CompilerGenerated]
		private void label_0_MouseUp(object sender, MouseEventArgs e)
		{
			this.bool_0 = false;
		}

		// Token: 0x0600030C RID: 780 RVA: 0x00033790 File Offset: 0x00031990
		[CompilerGenerated]
		private void method_115(object sender, EventArgs e)
		{
			bool flag = SerialPort.GetPortNames().Length != 0;
			this.label_0.BackColor = (flag ? Color.Lime : Color.Red);
		}

		// Token: 0x0600030D RID: 781 RVA: 0x0000D4E9 File Offset: 0x0000B6E9
		[CompilerGenerated]
		private void panel_0_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				GForm12.ReleaseCapture();
				GForm12.SendMessage_1(base.Handle, 161, 2, 0);
			}
		}

		// Token: 0x0600030E RID: 782 RVA: 0x000337C4 File Offset: 0x000319C4
		[CompilerGenerated]
		private void pictureBox_0_Paint(object sender, PaintEventArgs e)
		{
			e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
			e.Graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
			using (SolidBrush solidBrush = new SolidBrush(Color.White))
			{
				e.Graphics.FillEllipse(solidBrush, 1, 1, 24, 24);
				using (Font font = new Font("Segoe UI", 11f, FontStyle.Bold))
				{
					using (SolidBrush solidBrush2 = new SolidBrush((this.panel_0 != null) ? this.panel_0.BackColor : Color.FromArgb(215, 15, 15)))
					{
						SizeF sizeF = e.Graphics.MeasureString("M", font);
						e.Graphics.DrawString("M", font, solidBrush2, new PointF(13f - sizeF.Width / 2f + 0.5f, 13f - sizeF.Height / 2f + 0.5f));
					}
				}
			}
		}

		// Token: 0x0600030F RID: 783 RVA: 0x0000D4E9 File Offset: 0x0000B6E9
		[CompilerGenerated]
		private void label_5_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				GForm12.ReleaseCapture();
				GForm12.SendMessage_1(base.Handle, 161, 2, 0);
			}
		}

		// Token: 0x06000310 RID: 784 RVA: 0x0000D511 File Offset: 0x0000B711
		[CompilerGenerated]
		private void label_5_Click_1(object sender, EventArgs e)
		{
			this.toolStripMenuItem_6_Click(sender, e);
		}

		// Token: 0x06000311 RID: 785 RVA: 0x0000D51B File Offset: 0x0000B71B
		[CompilerGenerated]
		private void button_3_Click(object sender, EventArgs e)
		{
			base.WindowState = FormWindowState.Minimized;
		}

		// Token: 0x0400018D RID: 397
		private string string_0;

		// Token: 0x0400018E RID: 398
		private const string string_1 = "buttonColors.dat";

		// Token: 0x0400018F RID: 399
		private readonly object object_0 = new object();

		// Token: 0x04000190 RID: 400
		private Label label_0;

		// Token: 0x04000191 RID: 401
		private bool bool_0;

		// Token: 0x04000192 RID: 402
		private Button button_0;

		// Token: 0x04000193 RID: 403
		private Button button_1;

		// Token: 0x04000194 RID: 404
		private Button button_2;

		// Token: 0x04000195 RID: 405
		private Label label_1;

		// Token: 0x04000196 RID: 406
		private Label label_2;

		// Token: 0x04000197 RID: 407
		private ToolStripMenuItem toolStripMenuItem_0;

		// Token: 0x04000198 RID: 408
		private ToolStripMenuItem toolStripMenuItem_1;

		// Token: 0x04000199 RID: 409
		private ToolStripMenuItem toolStripMenuItem_2;

		// Token: 0x0400019A RID: 410
		private ToolStripMenuItem toolStripMenuItem_3;

		// Token: 0x0400019B RID: 411
		private ToolStripMenuItem toolStripMenuItem_4;

		// Token: 0x0400019C RID: 412
		private ToolStripMenuItem toolStripMenuItem_5;

		// Token: 0x0400019D RID: 413
		private ToolStripMenuItem toolStripMenuItem_6;

		// Token: 0x0400019E RID: 414
		private ToolStripMenuItem toolStripMenuItem_7;

		// Token: 0x0400019F RID: 415
		private ToolStripMenuItem toolStripMenuItem_8;

		// Token: 0x040001A0 RID: 416
		private ToolStripMenuItem toolStripMenuItem_9;

		// Token: 0x040001A1 RID: 417
		private ToolStripMenuItem toolStripMenuItem_10;

		// Token: 0x040001A2 RID: 418
		private ToolStripMenuItem toolStripMenuItem_11;

		// Token: 0x040001A3 RID: 419
		private ToolStripMenuItem toolStripMenuItem_12;

		// Token: 0x040001A4 RID: 420
		private ToolStripMenuItem toolStripMenuItem_13;

		// Token: 0x040001A5 RID: 421
		private ToolStripMenuItem toolStripMenuItem_14;

		// Token: 0x040001A6 RID: 422
		private ToolStripMenuItem toolStripMenuItem_15;

		// Token: 0x040001A7 RID: 423
		private ToolStripMenuItem toolStripMenuItem_16;

		// Token: 0x040001A8 RID: 424
		private ToolStripMenuItem toolStripMenuItem_17;

		// Token: 0x040001A9 RID: 425
		private ToolStripMenuItem toolStripMenuItem_18;

		// Token: 0x040001AA RID: 426
		private ToolStripMenuItem toolStripMenuItem_19;

		// Token: 0x040001AB RID: 427
		private ToolStripMenuItem toolStripMenuItem_20;

		// Token: 0x040001AC RID: 428
		private ToolStripMenuItem toolStripMenuItem_21;

		// Token: 0x040001AD RID: 429
		private ToolStripMenuItem toolStripMenuItem_22;

		// Token: 0x040001AE RID: 430
		private ToolStripMenuItem toolStripMenuItem_23;

		// Token: 0x040001AF RID: 431
		private ToolStripMenuItem toolStripMenuItem_24;

		// Token: 0x040001B0 RID: 432
		private ToolStripMenuItem toolStripMenuItem_25;

		// Token: 0x040001B1 RID: 433
		private MenuStrip menuStrip_0;

		// Token: 0x040001B2 RID: 434
		private ToolStripMenuItem toolStripMenuItem_26;

		// Token: 0x040001B3 RID: 435
		private GroupBox groupBox_0;

		// Token: 0x040001B4 RID: 436
		private Label label_3;

		// Token: 0x040001B5 RID: 437
		private ToolStripMenuItem toolStripMenuItem_27;

		// Token: 0x040001B6 RID: 438
		private Label label_4;

		// Token: 0x040001B7 RID: 439
		private ToolStripMenuItem toolStripMenuItem_28;

		// Token: 0x040001B8 RID: 440
		private ToolStripMenuItem toolStripMenuItem_29;

		// Token: 0x040001B9 RID: 441
		private ToolStripMenuItem toolStripMenuItem_30;

		// Token: 0x040001BA RID: 442
		private ToolStripMenuItem toolStripMenuItem_31;

		// Token: 0x040001BB RID: 443
		private Point point_0;

		// Token: 0x040001BC RID: 444
		private const uint uint_0 = 1040U;

		// Token: 0x040001BD RID: 445
		private Panel panel_0;

		// Token: 0x040001BE RID: 446
		private PictureBox pictureBox_0;

		// Token: 0x040001BF RID: 447
		private Label label_5;

		// Token: 0x040001C0 RID: 448
		private Button button_3;

		// Token: 0x040001C1 RID: 449
		private Button button_4;

		// Token: 0x040001C2 RID: 450
		private bool bool_1;

		// Token: 0x040001C3 RID: 451
		private List<GForm12.GClass6> list_0 = new List<GForm12.GClass6>();

		// Token: 0x040001C4 RID: 452
		private Random random_0 = new Random();

		// Token: 0x040001C5 RID: 453
		private Color color_0 = Color.Red;

		// Token: 0x040001C6 RID: 454
		private readonly object object_1 = new object();

		// Token: 0x040001C7 RID: 455
		private bool bool_2;

		// Token: 0x040001C8 RID: 456
		private List<string> list_1 = new List<string>();

		// Token: 0x040001C9 RID: 457
		private float float_0;

		// Token: 0x040001CA RID: 458
		private string string_2 = "";

		// Token: 0x040001CB RID: 459
		private readonly object object_2 = new object();

		// Token: 0x040001CC RID: 460
		private const int int_0 = 161;

		// Token: 0x040001CD RID: 461
		private const int int_1 = 2;

		// Token: 0x040001CE RID: 462
		private static readonly string[] string_3;

		// Token: 0x040001CF RID: 463
		private Thread thread_0;

		// Token: 0x040001D0 RID: 464
		private static readonly string[] string_4;

		// Token: 0x040001D1 RID: 465
		private static readonly string[] string_5;

		// Token: 0x040001D2 RID: 466
		private static readonly string[] string_6;

		// Token: 0x040001D3 RID: 467
		private static readonly string[] string_7;

		// Token: 0x040001D4 RID: 468
		private static readonly string[] string_8;

		// Token: 0x040001D5 RID: 469
		private static readonly byte[] byte_0;

		// Token: 0x040001D6 RID: 470
		private static readonly byte[] byte_1;

		// Token: 0x040001D7 RID: 471
		private static readonly byte[] byte_2;

		// Token: 0x040001D8 RID: 472
		private static readonly byte[] byte_3;

		// Token: 0x040001D9 RID: 473
		private static readonly byte[] byte_4;

		// Token: 0x040001DA RID: 474
		private static readonly byte[] byte_5;

		// Token: 0x040001DB RID: 475
		private static readonly byte[] byte_6;

		// Token: 0x040001DC RID: 476
		private static readonly byte[] byte_7;

		// Token: 0x040001DD RID: 477
		private static readonly byte[] byte_8;

		// Token: 0x040001DE RID: 478
		private static readonly byte[] byte_9;

		// Token: 0x040001DF RID: 479
		private static readonly byte[] byte_10;

		// Token: 0x040001E0 RID: 480
		private static readonly byte[] byte_11;

		// Token: 0x040001E1 RID: 481
		private static IntPtr intptr_0;

		// Token: 0x040001E2 RID: 482
		private static byte[] byte_12;

		// Token: 0x040001E3 RID: 483
		private static int int_2;

		// Token: 0x040001E4 RID: 484
		private static int int_3;

		// Token: 0x040001E5 RID: 485
		private readonly object object_3 = new object();

		// Token: 0x040001E6 RID: 486
		private static int int_4;

		// Token: 0x040001E7 RID: 487
		private static bool bool_3;

		// Token: 0x040001E8 RID: 488
		private static bool bool_4;

		// Token: 0x040001E9 RID: 489
		private static bool bool_5;

		// Token: 0x040001EA RID: 490
		private static bool bool_6;

		// Token: 0x040001EB RID: 491
		private static bool bool_7;

		// Token: 0x040001EC RID: 492
		private static bool bool_8;

		// Token: 0x040001ED RID: 493
		private volatile bool bool_9;

		// Token: 0x040001EE RID: 494
		private static bool bool_10;

		// Token: 0x040001F0 RID: 496
		private Label label_6;

		// Token: 0x040001F1 RID: 497
		private Label label_7;

		// Token: 0x040001F2 RID: 498
		private static readonly IntPtr intptr_1 = new IntPtr(1);

		// Token: 0x040001F3 RID: 499
		private Label label_8;

		// Token: 0x040001F4 RID: 500
		private TextBox textBox_0;

		// Token: 0x040001F5 RID: 501
		private TextBox textBox_1;

		// Token: 0x040001F6 RID: 502
		private Button button_5;

		// Token: 0x040001F7 RID: 503
		private GClass13 gclass13_0;

		// Token: 0x040001F8 RID: 504
		private Label label_9;

		// Token: 0x040001F9 RID: 505
		private bool bool_11;

		// Token: 0x040001FA RID: 506
		private Label label_10;

		// Token: 0x040001FB RID: 507
		private bool bool_12;

		// Token: 0x040001FC RID: 508
		private TextBox textBox_2;

		// Token: 0x040001FD RID: 509
		private volatile bool bool_13;

		// Token: 0x040001FE RID: 510
		private GClass15 gclass15_0;

		// Token: 0x040001FF RID: 511
		private volatile byte byte_13 = 65;

		// Token: 0x04000200 RID: 512
		private volatile byte byte_14 = 114;

		// Token: 0x04000201 RID: 513
		private PictureBox pictureBox_1;

		// Token: 0x04000202 RID: 514
		private Label label_11;

		// Token: 0x04000203 RID: 515
		private volatile int int_5;

		// Token: 0x04000204 RID: 516
		private Button button_6;

		// Token: 0x04000205 RID: 517
		private const int int_6 = 2;

		// Token: 0x04000206 RID: 518
		private Label label_12;

		// Token: 0x04000207 RID: 519
		private volatile bool bool_14;

		// Token: 0x04000208 RID: 520
		private int int_7;

		// Token: 0x04000209 RID: 521
		private PictureBox pictureBox_2;

		// Token: 0x0400020A RID: 522
		private Label label_13;

		// Token: 0x0400020B RID: 523
		private GForm12.Enum5 enum5_0;

		// Token: 0x0400020C RID: 524
		private PictureBox pictureBox_3;

		// Token: 0x0400020D RID: 525
		private Label label_14;

		// Token: 0x0400020E RID: 526
		private Label label_15;

		// Token: 0x0400020F RID: 527
		private Label label_16;

		// Token: 0x04000210 RID: 528
		private Label label_17;

		// Token: 0x04000211 RID: 529
		private ToolStripMenuItem toolStripMenuItem_32;

		// Token: 0x04000212 RID: 530
		private ToolStripMenuItem toolStripMenuItem_33;

		// Token: 0x04000213 RID: 531
		private ToolStripMenuItem toolStripMenuItem_34;

		// Token: 0x04000214 RID: 532
		private ToolStripMenuItem toolStripMenuItem_35;

		// Token: 0x04000215 RID: 533
		private ToolStripMenuItem toolStripMenuItem_36;

		// Token: 0x04000216 RID: 534
		private Button button_7;

		// Token: 0x04000217 RID: 535
		private Button button_8;

		// Token: 0x04000218 RID: 536
		private PictureBox pictureBox_4;

		// Token: 0x04000219 RID: 537
		private ToolStripMenuItem toolStripMenuItem_37;

		// Token: 0x0400021A RID: 538
		private ToolStripMenuItem toolStripMenuItem_38;

		// Token: 0x0400021B RID: 539
		private ToolStripMenuItem toolStripMenuItem_39;

		// Token: 0x0400021C RID: 540
		private ToolStripMenuItem toolStripMenuItem_40;

		// Token: 0x0400021D RID: 541
		private ToolStripMenuItem toolStripMenuItem_41;

		// Token: 0x0400021E RID: 542
		private ToolStripMenuItem toolStripMenuItem_42;

		// Token: 0x0400021F RID: 543
		private ToolStripMenuItem toolStripMenuItem_43;

		// Token: 0x04000220 RID: 544
		private ToolStripMenuItem toolStripMenuItem_44;

		// Token: 0x04000221 RID: 545
		private ToolStripMenuItem toolStripMenuItem_45;

		// Token: 0x04000222 RID: 546
		private ToolStripMenuItem toolStripMenuItem_46;

		// Token: 0x04000223 RID: 547
		private ToolStripMenuItem toolStripMenuItem_47;

		// Token: 0x04000224 RID: 548
		private ToolStripMenuItem toolStripMenuItem_48;

		// Token: 0x04000225 RID: 549
		private ToolStripMenuItem toolStripMenuItem_49;

		// Token: 0x04000226 RID: 550
		private ToolStripMenuItem toolStripMenuItem_50;

		// Token: 0x04000227 RID: 551
		private ToolStripMenuItem toolStripMenuItem_51;

		// Token: 0x04000228 RID: 552
		private ToolStripMenuItem toolStripMenuItem_52;

		// Token: 0x04000229 RID: 553
		private ToolStripMenuItem toolStripMenuItem_53;

		// Token: 0x0400022A RID: 554
		private ToolStripMenuItem toolStripMenuItem_54;

		// Token: 0x0400022B RID: 555
		private ToolStripMenuItem toolStripMenuItem_55;

		// Token: 0x0400022C RID: 556
		private ToolStripMenuItem toolStripMenuItem_56;

		// Token: 0x0400022D RID: 557
		private GroupBox groupBox_1;

		// Token: 0x0400022E RID: 558
		private GroupBox groupBox_2;

		// Token: 0x0400022F RID: 559
		private Button button_9;

		// Token: 0x04000230 RID: 560
		private GClass4 gclass4_0;

		// Token: 0x04000231 RID: 561
		private Button button_10;

		// Token: 0x04000232 RID: 562
		private GClass4 gclass4_1;

		// Token: 0x04000233 RID: 563
		private GClass4 gclass4_2;

		// Token: 0x04000234 RID: 564
		private Button button_11;

		// Token: 0x04000235 RID: 565
		private PictureBox pictureBox_5;

		// Token: 0x04000236 RID: 566
		private Label label_18;

		// Token: 0x04000237 RID: 567
		private GClass15 gclass15_1;

		// Token: 0x04000238 RID: 568
		private Button button_12;

		// Token: 0x04000239 RID: 569
		private System.Windows.Forms.Timer timer_0;

		// Token: 0x0400023A RID: 570
		[CompilerGenerated]
		private object object_4;

		// Token: 0x0400023B RID: 571
		[CompilerGenerated]
		private object object_5;

		// Token: 0x020000B6 RID: 182
		// (Invoke) Token: 0x06000313 RID: 787
		private delegate void Delegate0(Control control_0, string string_0);

		// Token: 0x020000B7 RID: 183
		// (Invoke) Token: 0x06000317 RID: 791
		private delegate void Delegate1(ComboBox comboBox_0, int int_0);

		// Token: 0x020000B8 RID: 184
		// (Invoke) Token: 0x0600031B RID: 795
		private delegate void Delegate2(ComboBox comboBox_0, bool bool_0);

		// Token: 0x020000B9 RID: 185
		// (Invoke) Token: 0x0600031F RID: 799
		private delegate void Delegate3(ProgressBar progressBar_0, int int_0);

		// Token: 0x020000BA RID: 186
		internal struct Struct12
		{
			// Token: 0x0400023C RID: 572
			public GForm12.Enum1 enum1_0;

			// Token: 0x0400023D RID: 573
			public IntPtr intptr_0;

			// Token: 0x0400023E RID: 574
			public int int_0;
		}

		// Token: 0x020000BB RID: 187
		internal enum Enum1
		{
			// Token: 0x04000240 RID: 576
			const_0 = 19
		}

		// Token: 0x020000BC RID: 188
		internal enum Enum2
		{
			// Token: 0x04000242 RID: 578
			const_0,
			// Token: 0x04000243 RID: 579
			const_1,
			// Token: 0x04000244 RID: 580
			const_2,
			// Token: 0x04000245 RID: 581
			const_3,
			// Token: 0x04000246 RID: 582
			const_4,
			// Token: 0x04000247 RID: 583
			const_5
		}

		// Token: 0x020000BD RID: 189
		internal struct Struct13
		{
			// Token: 0x04000248 RID: 584
			public GForm12.Enum2 enum2_0;

			// Token: 0x04000249 RID: 585
			public int int_0;

			// Token: 0x0400024A RID: 586
			public int int_1;

			// Token: 0x0400024B RID: 587
			public int int_2;
		}

		// Token: 0x020000BE RID: 190
		private enum Enum3
		{
			// Token: 0x0400024D RID: 589
			const_0,
			// Token: 0x0400024E RID: 590
			const_1,
			// Token: 0x0400024F RID: 591
			const_2,
			// Token: 0x04000250 RID: 592
			const_3,
			// Token: 0x04000251 RID: 593
			const_4,
			// Token: 0x04000252 RID: 594
			const_5
		}

		// Token: 0x020000BF RID: 191
		private enum Enum4
		{
			// Token: 0x04000254 RID: 596
			const_0,
			// Token: 0x04000255 RID: 597
			const_1
		}

		// Token: 0x020000C0 RID: 192
		private enum Enum5
		{
			// Token: 0x04000257 RID: 599
			const_0,
			// Token: 0x04000258 RID: 600
			const_1,
			// Token: 0x04000259 RID: 601
			const_2
		}

		// Token: 0x020000C1 RID: 193
		public class GClass6
		{
			// Token: 0x0400025A RID: 602
			public float float_0;

			// Token: 0x0400025B RID: 603
			public float float_1;

			// Token: 0x0400025C RID: 604
			public float float_2;

			// Token: 0x0400025D RID: 605
			public float float_3;

			// Token: 0x0400025E RID: 606
			public float float_4;

			// Token: 0x0400025F RID: 607
			public int int_0;
		}

		// Token: 0x020000C2 RID: 194
		public class GClass7 : ProfessionalColorTable
		{
			// Token: 0x17000004 RID: 4
			// (get) Token: 0x06000323 RID: 803 RVA: 0x0000D524 File Offset: 0x0000B724
			public override Color MenuItemSelected
			{
				get
				{
					return Color.FromArgb(255, 60, 60);
				}
			}

			// Token: 0x17000007 RID: 7
			// (get) Token: 0x06000324 RID: 804 RVA: 0x0000D524 File Offset: 0x0000B724
			public override Color MenuItemSelectedGradientBegin
			{
				get
				{
					return Color.FromArgb(255, 60, 60);
				}
			}

			// Token: 0x17000008 RID: 8
			// (get) Token: 0x06000325 RID: 805 RVA: 0x0000D524 File Offset: 0x0000B724
			public override Color MenuItemSelectedGradientEnd
			{
				get
				{
					return Color.FromArgb(255, 60, 60);
				}
			}

			// Token: 0x17000009 RID: 9
			// (get) Token: 0x06000326 RID: 806 RVA: 0x0000D534 File Offset: 0x0000B734
			public override Color MenuItemPressedGradientBegin
			{
				get
				{
					return Color.FromArgb(180, 0, 0);
				}
			}

			// Token: 0x1700000A RID: 10
			// (get) Token: 0x06000327 RID: 807 RVA: 0x0000D534 File Offset: 0x0000B734
			public override Color MenuItemPressedGradientEnd
			{
				get
				{
					return Color.FromArgb(180, 0, 0);
				}
			}

			// Token: 0x17000005 RID: 5
			// (get) Token: 0x06000328 RID: 808 RVA: 0x0000D542 File Offset: 0x0000B742
			public override Color MenuItemBorder
			{
				get
				{
					return Color.Transparent;
				}
			}

			// Token: 0x17000006 RID: 6
			// (get) Token: 0x06000329 RID: 809 RVA: 0x0000D542 File Offset: 0x0000B742
			public override Color MenuBorder
			{
				get
				{
					return Color.Transparent;
				}
			}

			// Token: 0x1700000B RID: 11
			// (get) Token: 0x0600032A RID: 810 RVA: 0x0000D549 File Offset: 0x0000B749
			public override Color ToolStripDropDownBackground
			{
				get
				{
					return Color.FromArgb(30, 30, 30);
				}
			}

			// Token: 0x17000001 RID: 1
			// (get) Token: 0x0600032B RID: 811 RVA: 0x0000D549 File Offset: 0x0000B749
			public override Color ImageMarginGradientBegin
			{
				get
				{
					return Color.FromArgb(30, 30, 30);
				}
			}

			// Token: 0x17000002 RID: 2
			// (get) Token: 0x0600032C RID: 812 RVA: 0x0000D549 File Offset: 0x0000B749
			public override Color ImageMarginGradientMiddle
			{
				get
				{
					return Color.FromArgb(30, 30, 30);
				}
			}

			// Token: 0x17000003 RID: 3
			// (get) Token: 0x0600032D RID: 813 RVA: 0x0000D549 File Offset: 0x0000B749
			public override Color ImageMarginGradientEnd
			{
				get
				{
					return Color.FromArgb(30, 30, 30);
				}
			}

			// Token: 0x04000260 RID: 608
			private Color color_0 = Color.FromArgb(215, 15, 15);
		}

		// Token: 0x020000C3 RID: 195
		public class GClass8 : ToolStripProfessionalRenderer
		{
			// Token: 0x0600032F RID: 815 RVA: 0x0000D572 File Offset: 0x0000B772
			public GClass8() : base(new GForm12.GClass7())
			{
			}

			// Token: 0x06000330 RID: 816 RVA: 0x0000C303 File Offset: 0x0000A503
			protected override void OnRenderToolStripBorder(ToolStripRenderEventArgs e)
			{
			}
		}

		// Token: 0x020000C4 RID: 196
		public class GClass9 : ProfessionalColorTable
		{
			// Token: 0x1700000F RID: 15
			// (get) Token: 0x06000331 RID: 817 RVA: 0x0000D57F File Offset: 0x0000B77F
			public override Color MenuItemSelected
			{
				get
				{
					return Color.FromArgb(45, 45, 45);
				}
			}

			// Token: 0x17000012 RID: 18
			// (get) Token: 0x06000332 RID: 818 RVA: 0x0000D57F File Offset: 0x0000B77F
			public override Color MenuItemSelectedGradientBegin
			{
				get
				{
					return Color.FromArgb(45, 45, 45);
				}
			}

			// Token: 0x17000013 RID: 19
			// (get) Token: 0x06000333 RID: 819 RVA: 0x0000D57F File Offset: 0x0000B77F
			public override Color MenuItemSelectedGradientEnd
			{
				get
				{
					return Color.FromArgb(45, 45, 45);
				}
			}

			// Token: 0x17000010 RID: 16
			// (get) Token: 0x06000334 RID: 820 RVA: 0x0000D58C File Offset: 0x0000B78C
			public override Color MenuItemBorder
			{
				get
				{
					return Color.FromArgb(70, 70, 70);
				}
			}

			// Token: 0x17000014 RID: 20
			// (get) Token: 0x06000335 RID: 821 RVA: 0x0000D599 File Offset: 0x0000B799
			public override Color MenuItemPressedGradientBegin
			{
				get
				{
					return Color.FromArgb(35, 35, 35);
				}
			}

			// Token: 0x17000015 RID: 21
			// (get) Token: 0x06000336 RID: 822 RVA: 0x0000D599 File Offset: 0x0000B799
			public override Color MenuItemPressedGradientEnd
			{
				get
				{
					return Color.FromArgb(35, 35, 35);
				}
			}

			// Token: 0x17000016 RID: 22
			// (get) Token: 0x06000337 RID: 823 RVA: 0x0000D5A6 File Offset: 0x0000B7A6
			public override Color ToolStripDropDownBackground
			{
				get
				{
					return Color.FromArgb(20, 20, 20);
				}
			}

			// Token: 0x17000011 RID: 17
			// (get) Token: 0x06000338 RID: 824 RVA: 0x0000D5B3 File Offset: 0x0000B7B3
			public override Color MenuBorder
			{
				get
				{
					return Color.FromArgb(60, 60, 60);
				}
			}

			// Token: 0x1700000C RID: 12
			// (get) Token: 0x06000339 RID: 825 RVA: 0x0000D5A6 File Offset: 0x0000B7A6
			public override Color ImageMarginGradientBegin
			{
				get
				{
					return Color.FromArgb(20, 20, 20);
				}
			}

			// Token: 0x1700000D RID: 13
			// (get) Token: 0x0600033A RID: 826 RVA: 0x0000D5A6 File Offset: 0x0000B7A6
			public override Color ImageMarginGradientMiddle
			{
				get
				{
					return Color.FromArgb(20, 20, 20);
				}
			}

			// Token: 0x1700000E RID: 14
			// (get) Token: 0x0600033B RID: 827 RVA: 0x0000D5A6 File Offset: 0x0000B7A6
			public override Color ImageMarginGradientEnd
			{
				get
				{
					return Color.FromArgb(20, 20, 20);
				}
			}
		}

		// Token: 0x020000C5 RID: 197
		public class GClass10 : ToolStripProfessionalRenderer
		{
			// Token: 0x0600033D RID: 829 RVA: 0x0000D5C8 File Offset: 0x0000B7C8
			public GClass10() : base(new GForm12.GClass9())
			{
			}

			// Token: 0x0600033E RID: 830 RVA: 0x000338E4 File Offset: 0x00031AE4
			protected override void OnRenderItemText(ToolStripItemTextRenderEventArgs e)
			{
				e.Item.ForeColor = Color.LightGray;
				if (e.Item.Selected || e.Item.Pressed)
				{
					e.Item.ForeColor = Color.White;
				}
				base.OnRenderItemText(e);
			}
		}

		// Token: 0x020000C7 RID: 199
		[CompilerGenerated]
		[Serializable]
		private sealed class Class142
		{
			// Token: 0x06000343 RID: 835 RVA: 0x0000D5EF File Offset: 0x0000B7EF
			internal void method_0()
			{
				Console.Beep(400, 300);
				Thread.Sleep(50);
				Console.Beep(400, 300);
				MessageBox.Show(EC2D41B1-A2F9-4664-90D8-86645EE2E753.smethod_938(), EC2D41B1-A2F9-4664-90D8-86645EE2E753.smethod_939(), MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1, (MessageBoxOptions)262144);
			}

			// Token: 0x06000344 RID: 836 RVA: 0x0000D62F File Offset: 0x0000B82F
			internal void method_1()
			{
				RuntimeHelpers.InitializeArray(new int[4], fieldof(Class190.struct30_0).FieldHandle);
			}

			// Token: 0x06000345 RID: 837 RVA: 0x00033A24 File Offset: 0x00031C24
			internal void method_2()
			{
				GForm15.smethod_1(EC2D41B1-A2F9-4664-90D8-86645EE2E753.smethod_694(), EC2D41B1-A2F9-4664-90D8-86645EE2E753.smethod_940(), GEnum1.const_0);
				Task.Run(new Action(GForm12.Class142.class142_0.method_3));
				GClass19.smethod_0(EC2D41B1-A2F9-4664-90D8-86645EE2E753.smethod_759());
			}

			// Token: 0x06000346 RID: 838 RVA: 0x00033A70 File Offset: 0x00031C70
			internal void method_3()
			{
				int[] array = new int[]
				{
					1000,
					1200,
					1500,
					2000
				};
				for (int i = 0; i < array.Length; i++)
				{
					Console.Beep(array[i], 80);
					Thread.Sleep(20);
				}
				Thread.Sleep(100);
				Console.Beep(2200, 200);
				Thread.Sleep(50);
				Console.Beep(2200, 100);
			}

			// Token: 0x06000347 RID: 839 RVA: 0x0000D643 File Offset: 0x0000B843
			internal void method_4()
			{
				Console.Beep(500, 200);
				Thread.Sleep(50);
				Console.Beep(500, 200);
				MessageBox.Show(EC2D41B1-A2F9-4664-90D8-86645EE2E753.smethod_941(), EC2D41B1-A2F9-4664-90D8-86645EE2E753.smethod_375(), MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1, (MessageBoxOptions)262144);
			}

			// Token: 0x06000348 RID: 840 RVA: 0x0000D683 File Offset: 0x0000B883
			internal void method_5()
			{
				Console.Beep(400, 300);
				Thread.Sleep(50);
				Console.Beep(400, 300);
				MessageBox.Show(EC2D41B1-A2F9-4664-90D8-86645EE2E753.smethod_942(), EC2D41B1-A2F9-4664-90D8-86645EE2E753.smethod_375(), MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1, (MessageBoxOptions)262144);
			}

			// Token: 0x06000349 RID: 841 RVA: 0x0000D6C3 File Offset: 0x0000B8C3
			internal void method_6()
			{
				Console.Beep(900, 100);
			}

			// Token: 0x0600034A RID: 842 RVA: 0x0000D6D1 File Offset: 0x0000B8D1
			internal void method_7()
			{
				Console.Beep(1000, 150);
			}

			// Token: 0x0600034B RID: 843 RVA: 0x0000D6D1 File Offset: 0x0000B8D1
			internal void method_8()
			{
				Console.Beep(1000, 150);
			}

			// Token: 0x0600034C RID: 844 RVA: 0x0000D6D1 File Offset: 0x0000B8D1
			internal void method_9()
			{
				Console.Beep(1000, 150);
			}

			// Token: 0x0600034D RID: 845 RVA: 0x0000D6E2 File Offset: 0x0000B8E2
			internal bool method_10(string string_0)
			{
				return string_0.EndsWith(EC2D41B1-A2F9-4664-90D8-86645EE2E753.smethod_637());
			}

			// Token: 0x0600034E RID: 846 RVA: 0x0000D6D1 File Offset: 0x0000B8D1
			internal void method_11()
			{
				Console.Beep(1000, 150);
			}

			// Token: 0x0600034F RID: 847 RVA: 0x0000D6D1 File Offset: 0x0000B8D1
			internal void method_12()
			{
				Console.Beep(1000, 150);
			}

			// Token: 0x06000350 RID: 848 RVA: 0x0000D6EF File Offset: 0x0000B8EF
			internal bool method_13(string string_0)
			{
				return string_0.EndsWith(EC2D41B1-A2F9-4664-90D8-86645EE2E753.smethod_642());
			}

			// Token: 0x06000351 RID: 849 RVA: 0x0000D6D1 File Offset: 0x0000B8D1
			internal void method_14()
			{
				Console.Beep(1000, 150);
			}

			// Token: 0x06000352 RID: 850 RVA: 0x0000D6D1 File Offset: 0x0000B8D1
			internal void method_15()
			{
				Console.Beep(1000, 150);
			}

			// Token: 0x06000353 RID: 851 RVA: 0x0000D6D1 File Offset: 0x0000B8D1
			internal void method_16()
			{
				Console.Beep(1000, 150);
			}

			// Token: 0x06000354 RID: 852 RVA: 0x0000D6FC File Offset: 0x0000B8FC
			internal bool method_17(string string_0)
			{
				return string_0.EndsWith(EC2D41B1-A2F9-4664-90D8-86645EE2E753.smethod_647());
			}

			// Token: 0x06000355 RID: 853 RVA: 0x0000D6D1 File Offset: 0x0000B8D1
			internal void method_18()
			{
				Console.Beep(1000, 150);
			}

			// Token: 0x06000356 RID: 854 RVA: 0x0000D709 File Offset: 0x0000B909
			internal bool method_19(string string_0)
			{
				return string_0.EndsWith(EC2D41B1-A2F9-4664-90D8-86645EE2E753.smethod_648());
			}

			// Token: 0x06000357 RID: 855 RVA: 0x0000D6D1 File Offset: 0x0000B8D1
			internal void method_20()
			{
				Console.Beep(1000, 150);
			}

			// Token: 0x06000358 RID: 856 RVA: 0x0000D6D1 File Offset: 0x0000B8D1
			internal void method_21()
			{
				Console.Beep(1000, 150);
			}

			// Token: 0x06000359 RID: 857 RVA: 0x0000D716 File Offset: 0x0000B916
			internal void method_22()
			{
				Console.Beep(800, 100);
			}

			// Token: 0x0600035A RID: 858 RVA: 0x0000C303 File Offset: 0x0000A503
			internal void method_23()
			{
			}

			// Token: 0x0600035B RID: 859 RVA: 0x0000C303 File Offset: 0x0000A503
			internal void method_24()
			{
			}

			// Token: 0x0600035C RID: 860 RVA: 0x0000D6D1 File Offset: 0x0000B8D1
			internal void method_25()
			{
				Console.Beep(1000, 150);
			}

			// Token: 0x0600035D RID: 861 RVA: 0x0000D6D1 File Offset: 0x0000B8D1
			internal void method_26()
			{
				Console.Beep(1000, 150);
			}

			// Token: 0x0600035E RID: 862 RVA: 0x0000D6D1 File Offset: 0x0000B8D1
			internal void method_27()
			{
				Console.Beep(1000, 150);
			}

			// Token: 0x0600035F RID: 863 RVA: 0x0000D6D1 File Offset: 0x0000B8D1
			internal void method_28()
			{
				Console.Beep(1000, 150);
			}

			// Token: 0x06000360 RID: 864 RVA: 0x0000D6D1 File Offset: 0x0000B8D1
			internal void method_29()
			{
				Console.Beep(1000, 150);
			}

			// Token: 0x06000361 RID: 865 RVA: 0x0000D6D1 File Offset: 0x0000B8D1
			internal void method_30()
			{
				Console.Beep(1000, 150);
			}

			// Token: 0x06000362 RID: 866 RVA: 0x0000D6D1 File Offset: 0x0000B8D1
			internal void method_31()
			{
				Console.Beep(1000, 150);
			}

			// Token: 0x06000363 RID: 867 RVA: 0x0000D6D1 File Offset: 0x0000B8D1
			internal void method_32()
			{
				Console.Beep(1000, 150);
			}

			// Token: 0x06000364 RID: 868 RVA: 0x0000D6D1 File Offset: 0x0000B8D1
			internal void method_33()
			{
				Console.Beep(1000, 150);
			}

			// Token: 0x06000365 RID: 869 RVA: 0x0000D6D1 File Offset: 0x0000B8D1
			internal void method_34()
			{
				Console.Beep(1000, 150);
			}

			// Token: 0x06000366 RID: 870 RVA: 0x0000D6E2 File Offset: 0x0000B8E2
			internal bool method_35(string string_0)
			{
				return string_0.EndsWith(EC2D41B1-A2F9-4664-90D8-86645EE2E753.smethod_637());
			}

			// Token: 0x06000367 RID: 871 RVA: 0x0000D724 File Offset: 0x0000B924
			internal void method_36()
			{
				Console.Beep(1000, 100);
			}

			// Token: 0x06000368 RID: 872 RVA: 0x0000D6D1 File Offset: 0x0000B8D1
			internal void method_37()
			{
				Console.Beep(1000, 150);
			}

			// Token: 0x06000369 RID: 873 RVA: 0x0000D6D1 File Offset: 0x0000B8D1
			internal void method_38()
			{
				Console.Beep(1000, 150);
			}

			// Token: 0x0600036A RID: 874 RVA: 0x0000D6D1 File Offset: 0x0000B8D1
			internal void method_39()
			{
				Console.Beep(1000, 150);
			}

			// Token: 0x0600036B RID: 875 RVA: 0x0000D6EF File Offset: 0x0000B8EF
			internal bool method_40(string string_0)
			{
				return string_0.EndsWith(EC2D41B1-A2F9-4664-90D8-86645EE2E753.smethod_642());
			}

			// Token: 0x0600036C RID: 876 RVA: 0x0000D6D1 File Offset: 0x0000B8D1
			internal void method_41()
			{
				Console.Beep(1000, 150);
			}

			// Token: 0x0600036D RID: 877 RVA: 0x0000D6D1 File Offset: 0x0000B8D1
			internal void method_42()
			{
				Console.Beep(1000, 150);
			}

			// Token: 0x0600036E RID: 878 RVA: 0x0000D6D1 File Offset: 0x0000B8D1
			internal void method_43()
			{
				Console.Beep(1000, 150);
			}

			// Token: 0x0600036F RID: 879 RVA: 0x0000D6D1 File Offset: 0x0000B8D1
			internal void method_44()
			{
				Console.Beep(1000, 150);
			}

			// Token: 0x06000370 RID: 880 RVA: 0x0000D6D1 File Offset: 0x0000B8D1
			internal void method_45()
			{
				Console.Beep(1000, 150);
			}

			// Token: 0x06000371 RID: 881 RVA: 0x0000D6D1 File Offset: 0x0000B8D1
			internal void method_46()
			{
				Console.Beep(1000, 150);
			}

			// Token: 0x06000372 RID: 882 RVA: 0x0000D6D1 File Offset: 0x0000B8D1
			internal void method_47()
			{
				Console.Beep(1000, 150);
			}

			// Token: 0x06000373 RID: 883 RVA: 0x0000D6D1 File Offset: 0x0000B8D1
			internal void method_48()
			{
				Console.Beep(1000, 150);
			}

			// Token: 0x06000374 RID: 884 RVA: 0x0000D6D1 File Offset: 0x0000B8D1
			internal void method_49()
			{
				Console.Beep(1000, 150);
			}

			// Token: 0x06000375 RID: 885 RVA: 0x0000D6E2 File Offset: 0x0000B8E2
			internal bool method_50(string string_0)
			{
				return string_0.EndsWith(EC2D41B1-A2F9-4664-90D8-86645EE2E753.smethod_637());
			}

			// Token: 0x06000376 RID: 886 RVA: 0x0000D6D1 File Offset: 0x0000B8D1
			internal void method_51()
			{
				Console.Beep(1000, 150);
			}

			// Token: 0x06000377 RID: 887 RVA: 0x0000D6D1 File Offset: 0x0000B8D1
			internal void method_52()
			{
				Console.Beep(1000, 150);
			}

			// Token: 0x06000378 RID: 888 RVA: 0x0000D6FC File Offset: 0x0000B8FC
			internal bool method_53(string string_0)
			{
				return string_0.EndsWith(EC2D41B1-A2F9-4664-90D8-86645EE2E753.smethod_647());
			}

			// Token: 0x06000379 RID: 889 RVA: 0x0000D6D1 File Offset: 0x0000B8D1
			internal void method_54()
			{
				Console.Beep(1000, 150);
			}

			// Token: 0x0600037A RID: 890 RVA: 0x0000D6D1 File Offset: 0x0000B8D1
			internal void method_55()
			{
				Console.Beep(1000, 150);
			}

			// Token: 0x0600037B RID: 891 RVA: 0x0000D732 File Offset: 0x0000B932
			internal void method_56()
			{
				Console.Beep(1200, 100);
			}

			// Token: 0x0600037C RID: 892 RVA: 0x0000D740 File Offset: 0x0000B940
			internal void method_57()
			{
				Console.Beep(1200, 80);
			}

			// Token: 0x0600037D RID: 893 RVA: 0x0000D74E File Offset: 0x0000B94E
			internal void method_58(object sender, EventArgs e)
			{
				Application.Exit();
			}

			// Token: 0x0600037E RID: 894 RVA: 0x00033AD8 File Offset: 0x00031CD8
			internal void method_59(object sender, PaintEventArgs e)
			{
				e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
				Color backColor = ((Button)sender).BackColor;
				using (GraphicsPath graphicsPath = new GraphicsPath())
				{
					graphicsPath.AddEllipse(0, 0, 13, 13);
					using (PathGradientBrush pathGradientBrush = new PathGradientBrush(graphicsPath))
					{
						pathGradientBrush.CenterColor = Color.FromArgb(100, backColor);
						pathGradientBrush.SurroundColors = new Color[]
						{
							Color.Transparent
						};
						e.Graphics.FillPath(pathGradientBrush, graphicsPath);
					}
				}
				using (LinearGradientBrush linearGradientBrush = new LinearGradientBrush(new Rectangle(2, 2, 10, 10), backColor, Color.FromArgb(50, Color.Black), 45f))
				{
					e.Graphics.FillEllipse(linearGradientBrush, 2, 2, 10, 10);
				}
				using (SolidBrush solidBrush = new SolidBrush(Color.FromArgb(180, Color.White)))
				{
					e.Graphics.FillEllipse(solidBrush, 4, 4, 3, 3);
				}
			}

			// Token: 0x0600037F RID: 895 RVA: 0x0000D724 File Offset: 0x0000B924
			internal void method_60()
			{
				Console.Beep(1000, 100);
			}

			// Token: 0x06000380 RID: 896 RVA: 0x0000D755 File Offset: 0x0000B955
			internal void method_61()
			{
				Console.Beep(1500, 100);
			}

			// Token: 0x06000381 RID: 897 RVA: 0x0000D6D1 File Offset: 0x0000B8D1
			internal void method_62()
			{
				Console.Beep(1000, 150);
			}

			// Token: 0x06000382 RID: 898 RVA: 0x0000D6D1 File Offset: 0x0000B8D1
			internal void method_63()
			{
				Console.Beep(1000, 150);
			}

			// Token: 0x06000383 RID: 899 RVA: 0x0000D6D1 File Offset: 0x0000B8D1
			internal void method_64()
			{
				Console.Beep(1000, 150);
			}

			// Token: 0x04000265 RID: 613
			public static readonly GForm12.Class142 class142_0 = new GForm12.Class142();

			// Token: 0x04000266 RID: 614
			public static Action action_0;

			// Token: 0x04000267 RID: 615
			public static Action action_1;

			// Token: 0x04000268 RID: 616
			public static Action action_2;

			// Token: 0x04000269 RID: 617
			public static Action action_3;

			// Token: 0x0400026A RID: 618
			public static Action action_4;

			// Token: 0x0400026B RID: 619
			public static Action action_5;

			// Token: 0x0400026C RID: 620
			public static Action action_6;

			// Token: 0x0400026D RID: 621
			public static Action action_7;

			// Token: 0x0400026E RID: 622
			public static Action action_8;

			// Token: 0x0400026F RID: 623
			public static Action action_9;

			// Token: 0x04000270 RID: 624
			public static Func<string, bool> func_0;

			// Token: 0x04000271 RID: 625
			public static Action action_10;

			// Token: 0x04000272 RID: 626
			public static Action action_11;

			// Token: 0x04000273 RID: 627
			public static Func<string, bool> func_1;

			// Token: 0x04000274 RID: 628
			public static Action action_12;

			// Token: 0x04000275 RID: 629
			public static Action action_13;

			// Token: 0x04000276 RID: 630
			public static Action action_14;

			// Token: 0x04000277 RID: 631
			public static Func<string, bool> func_2;

			// Token: 0x04000278 RID: 632
			public static Action action_15;

			// Token: 0x04000279 RID: 633
			public static Func<string, bool> func_3;

			// Token: 0x0400027A RID: 634
			public static Action action_16;

			// Token: 0x0400027B RID: 635
			public static Action action_17;

			// Token: 0x0400027C RID: 636
			public static Action action_18;

			// Token: 0x0400027D RID: 637
			public static Action action_19;

			// Token: 0x0400027E RID: 638
			public static Action action_20;

			// Token: 0x0400027F RID: 639
			public static Action action_21;

			// Token: 0x04000280 RID: 640
			public static Action action_22;

			// Token: 0x04000281 RID: 641
			public static Action action_23;

			// Token: 0x04000282 RID: 642
			public static Action action_24;

			// Token: 0x04000283 RID: 643
			public static Action action_25;

			// Token: 0x04000284 RID: 644
			public static Action action_26;

			// Token: 0x04000285 RID: 645
			public static Action action_27;

			// Token: 0x04000286 RID: 646
			public static Action action_28;

			// Token: 0x04000287 RID: 647
			public static Action action_29;

			// Token: 0x04000288 RID: 648
			public static Action action_30;

			// Token: 0x04000289 RID: 649
			public static Func<string, bool> func_4;

			// Token: 0x0400028A RID: 650
			public static Action action_31;

			// Token: 0x0400028B RID: 651
			public static Action action_32;

			// Token: 0x0400028C RID: 652
			public static Action action_33;

			// Token: 0x0400028D RID: 653
			public static Action action_34;

			// Token: 0x0400028E RID: 654
			public static Func<string, bool> func_5;

			// Token: 0x0400028F RID: 655
			public static Action action_35;

			// Token: 0x04000290 RID: 656
			public static Action action_36;

			// Token: 0x04000291 RID: 657
			public static Action action_37;

			// Token: 0x04000292 RID: 658
			public static Action action_38;

			// Token: 0x04000293 RID: 659
			public static Action action_39;

			// Token: 0x04000294 RID: 660
			public static Action action_40;

			// Token: 0x04000295 RID: 661
			public static Action action_41;

			// Token: 0x04000296 RID: 662
			public static Action action_42;

			// Token: 0x04000297 RID: 663
			public static Action action_43;

			// Token: 0x04000298 RID: 664
			public static Func<string, bool> func_6;

			// Token: 0x04000299 RID: 665
			public static Action action_44;

			// Token: 0x0400029A RID: 666
			public static Action action_45;

			// Token: 0x0400029B RID: 667
			public static Func<string, bool> func_7;

			// Token: 0x0400029C RID: 668
			public static Action action_46;

			// Token: 0x0400029D RID: 669
			public static Action action_47;

			// Token: 0x0400029E RID: 670
			public static Action action_48;

			// Token: 0x0400029F RID: 671
			public static Action action_49;

			// Token: 0x040002A0 RID: 672
			public static EventHandler eventHandler_0;

			// Token: 0x040002A1 RID: 673
			public static PaintEventHandler paintEventHandler_0;

			// Token: 0x040002A2 RID: 674
			public static Action action_50;

			// Token: 0x040002A3 RID: 675
			public static Action action_51;

			// Token: 0x040002A4 RID: 676
			public static Action action_52;

			// Token: 0x040002A5 RID: 677
			public static Action action_53;

			// Token: 0x040002A6 RID: 678
			public static Action action_54;
		}

		// Token: 0x020000C8 RID: 200
		[CompilerGenerated]
		private sealed class Class143
		{
			// Token: 0x06000385 RID: 901 RVA: 0x00033C0C File Offset: 0x00031E0C
			internal Task method_0()
			{
				GForm12.Class143.Struct15 @struct;
				@struct.asyncTaskMethodBuilder_0 = AsyncTaskMethodBuilder.Create();
				@struct.class143_0 = this;
				@struct.int_0 = -1;
				@struct.asyncTaskMethodBuilder_0.Start<GForm12.Class143.Struct15>(ref @struct);
				return @struct.asyncTaskMethodBuilder_0.Task;
			}

			// Token: 0x06000386 RID: 902 RVA: 0x0000D763 File Offset: 0x0000B963
			internal void method_1()
			{
				this.gform12_0.Invalidate();
			}

			// Token: 0x06000387 RID: 903 RVA: 0x0000D763 File Offset: 0x0000B963
			internal void method_2()
			{
				this.gform12_0.Invalidate();
			}

			// Token: 0x06000388 RID: 904 RVA: 0x0000D763 File Offset: 0x0000B963
			internal void method_3()
			{
				this.gform12_0.Invalidate();
			}

			// Token: 0x040002A7 RID: 679
			public string[] string_0;

			// Token: 0x040002A8 RID: 680
			public GForm12 gform12_0;

			// Token: 0x040002A9 RID: 681
			public Action action_0;

			// Token: 0x040002AA RID: 682
			public Action action_1;

			// Token: 0x040002AB RID: 683
			public Action action_2;

			// Token: 0x020000C9 RID: 201
			[StructLayout(LayoutKind.Auto)]
			private struct Struct15 : IAsyncStateMachine
			{
				// Token: 0x06000389 RID: 905 RVA: 0x00033C50 File Offset: 0x00031E50
				void IAsyncStateMachine.MoveNext()
				{
					int num = this.int_0;
					GForm12.Class143 @class = this.class143_0;
					try
					{
						TaskAwaiter awaiter;
						switch (num)
						{
						case 0:
						{
							awaiter = this.taskAwaiter_0;
							this.taskAwaiter_0 = default(TaskAwaiter);
							int num2 = -1;
							num = -1;
							this.int_0 = num2;
							goto IL_1BE;
						}
						case 1:
						{
							awaiter = this.taskAwaiter_0;
							this.taskAwaiter_0 = default(TaskAwaiter);
							int num3 = -1;
							num = -1;
							this.int_0 = num3;
							goto IL_207;
						}
						case 2:
						{
							awaiter = this.taskAwaiter_0;
							this.taskAwaiter_0 = default(TaskAwaiter);
							int num4 = -1;
							num = -1;
							this.int_0 = num4;
							goto IL_2CB;
						}
						case 3:
						{
							awaiter = this.taskAwaiter_0;
							this.taskAwaiter_0 = default(TaskAwaiter);
							int num5 = -1;
							num = -1;
							this.int_0 = num5;
							goto IL_38D;
						}
						default:
							this.string_0 = @class.string_0;
							this.int_1 = 0;
							break;
						}
						IL_F8:
						if (this.int_1 < this.string_0.Length)
						{
							this.string_1 = this.string_0[this.int_1];
							this.int_2 = 0;
							goto IL_1D7;
						}
						this.string_0 = null;
						awaiter = Task.Delay(1000).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							int num6 = 2;
							num = 2;
							this.int_0 = num6;
							this.taskAwaiter_0 = awaiter;
							this.asyncTaskMethodBuilder_0.AwaitUnsafeOnCompleted<TaskAwaiter, GForm12.Class143.Struct15>(ref awaiter, ref this);
							return;
						}
						goto IL_2CB;
						IL_1BE:
						awaiter.GetResult();
						int num7 = this.int_2;
						this.int_2 = num7 + 1;
						IL_1D7:
						object object_;
						bool flag;
						if (this.int_2 > this.string_1.Length)
						{
							awaiter = Task.Delay(200).GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								int num8 = 1;
								num = 1;
								this.int_0 = num8;
								this.taskAwaiter_0 = awaiter;
								this.asyncTaskMethodBuilder_0.AwaitUnsafeOnCompleted<TaskAwaiter, GForm12.Class143.Struct15>(ref awaiter, ref this);
								return;
							}
						}
						else
						{
							object_ = @class.gform12_0.object_2;
							flag = false;
							try
							{
								Monitor.Enter(object_, ref flag);
								@class.gform12_0.string_2 = this.string_1.Substring(0, this.int_2) + EC2D41B1-A2F9-4664-90D8-86645EE2E753.smethod_901();
							}
							finally
							{
								if (num < 0 && flag)
								{
									Monitor.Exit(object_);
								}
							}
							Control gform12_ = @class.gform12_0;
							Action method;
							if ((method = @class.action_0) == null)
							{
								method = (@class.action_0 = new Action(@class.method_1));
							}
							gform12_.BeginInvoke(method);
							awaiter = Task.Delay(10).GetAwaiter();
							if (awaiter.IsCompleted)
							{
								goto IL_1BE;
							}
							int num9 = 0;
							num = 0;
							this.int_0 = num9;
							this.taskAwaiter_0 = awaiter;
							this.asyncTaskMethodBuilder_0.AwaitUnsafeOnCompleted<TaskAwaiter, GForm12.Class143.Struct15>(ref awaiter, ref this);
							return;
						}
						IL_207:
						awaiter.GetResult();
						object_ = @class.gform12_0.object_2;
						flag = false;
						try
						{
							Monitor.Enter(object_, ref flag);
							@class.gform12_0.list_1.Add(this.string_1);
							if (@class.gform12_0.list_1.Count > 10)
							{
								@class.gform12_0.list_1.RemoveAt(0);
							}
							@class.gform12_0.string_2 = EC2D41B1-A2F9-4664-90D8-86645EE2E753.smethod_3();
						}
						finally
						{
							if (num < 0 && flag)
							{
								Monitor.Exit(object_);
							}
						}
						this.string_1 = null;
						this.int_1++;
						goto IL_F8;
						IL_2CB:
						awaiter.GetResult();
						IL_2F6:
						if (@class.gform12_0.float_0 <= 0f)
						{
							@class.gform12_0.bool_2 = false;
							Control gform12_2 = @class.gform12_0;
							Action method2;
							if ((method2 = @class.action_2) == null)
							{
								method2 = (@class.action_2 = new Action(@class.method_3));
							}
							gform12_2.BeginInvoke(method2);
							GClass19.smethod_0(EC2D41B1-A2F9-4664-90D8-86645EE2E753.smethod_1000());
							goto IL_418;
						}
						@class.gform12_0.float_0 = @class.gform12_0.float_0 - 15f;
						if (@class.gform12_0.float_0 < 0f)
						{
							@class.gform12_0.float_0 = 0f;
						}
						Control gform12_3 = @class.gform12_0;
						Action method3;
						if ((method3 = @class.action_1) == null)
						{
							method3 = (@class.action_1 = new Action(@class.method_2));
						}
						gform12_3.BeginInvoke(method3);
						awaiter = Task.Delay(30).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							int num10 = 3;
							num = 3;
							this.int_0 = num10;
							this.taskAwaiter_0 = awaiter;
							this.asyncTaskMethodBuilder_0.AwaitUnsafeOnCompleted<TaskAwaiter, GForm12.Class143.Struct15>(ref awaiter, ref this);
							return;
						}
						IL_38D:
						awaiter.GetResult();
						goto IL_2F6;
					}
					catch (Exception exception)
					{
						this.int_0 = -2;
						this.asyncTaskMethodBuilder_0.SetException(exception);
						return;
					}
					IL_418:
					this.int_0 = -2;
					this.asyncTaskMethodBuilder_0.SetResult();
				}

				// Token: 0x0600038A RID: 906 RVA: 0x0000D770 File Offset: 0x0000B970
				[DebuggerHidden]
				void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
				{
					this.asyncTaskMethodBuilder_0.SetStateMachine(stateMachine);
				}

				// Token: 0x040002AC RID: 684
				public int int_0;

				// Token: 0x040002AD RID: 685
				public AsyncTaskMethodBuilder asyncTaskMethodBuilder_0;

				// Token: 0x040002AE RID: 686
				public GForm12.Class143 class143_0;

				// Token: 0x040002AF RID: 687
				private string[] string_0;

				// Token: 0x040002B0 RID: 688
				private int int_1;

				// Token: 0x040002B1 RID: 689
				private string string_1;

				// Token: 0x040002B2 RID: 690
				private int int_2;

				// Token: 0x040002B3 RID: 691
				private TaskAwaiter taskAwaiter_0;
			}
		}

		// Token: 0x020000CA RID: 202
		[CompilerGenerated]
		private sealed class Class144
		{
			// Token: 0x0600038C RID: 908 RVA: 0x000340D4 File Offset: 0x000322D4
			internal void method_0()
			{
				Color foreColor = (this.double_0 >= 15.0) ? Color.Red : ((this.double_0 >= 13.6) ? Color.Lime : ((this.double_0 >= 12.4) ? Color.Lime : ((this.double_0 >= 11.0) ? Color.Yellow : Color.Red)));
				string text = (this.double_0 >= 15.0) ? EC2D41B1-A2F9-4664-90D8-86645EE2E753.smethod_946() : ((this.double_0 >= 13.6) ? EC2D41B1-A2F9-4664-90D8-86645EE2E753.smethod_945() : ((this.double_0 >= 12.4) ? EC2D41B1-A2F9-4664-90D8-86645EE2E753.smethod_945() : ((this.double_0 >= 11.0) ? EC2D41B1-A2F9-4664-90D8-86645EE2E753.smethod_944() : EC2D41B1-A2F9-4664-90D8-86645EE2E753.smethod_943())));
				if (this.control_0 != null)
				{
					this.control_0.Text = string.Format(EC2D41B1-A2F9-4664-90D8-86645EE2E753.smethod_947(), this.double_0, text);
					this.control_0.ForeColor = foreColor;
				}
				Label label = this.control_1 as Label;
				if (label != null)
				{
					if (label.Name == EC2D41B1-A2F9-4664-90D8-86645EE2E753.smethod_359())
					{
						label.Text = string.Format(EC2D41B1-A2F9-4664-90D8-86645EE2E753.smethod_948(), this.double_0);
					}
					else
					{
						label.Text = string.Format(EC2D41B1-A2F9-4664-90D8-86645EE2E753.smethod_948(), this.double_0);
					}
					label.ForeColor = foreColor;
				}
				Label label2 = this.gform12_0.Controls.Find(EC2D41B1-A2F9-4664-90D8-86645EE2E753.smethod_949(), true).FirstOrDefault<Control>() as Label;
				if (label2 != null)
				{
					label2.Text = text;
					label2.ForeColor = foreColor;
				}
			}

			// Token: 0x040002B4 RID: 692
			public double double_0;

			// Token: 0x040002B5 RID: 693
			public Control control_0;

			// Token: 0x040002B6 RID: 694
			public Control control_1;

			// Token: 0x040002B7 RID: 695
			public GForm12 gform12_0;
		}

		// Token: 0x020000CB RID: 203
		[CompilerGenerated]
		private sealed class Class145
		{
			// Token: 0x0600038E RID: 910 RVA: 0x0000D77E File Offset: 0x0000B97E
			internal void method_0()
			{
				MessageBox.Show(this.gform12_0, this.exception_0.ToString(), EC2D41B1-A2F9-4664-90D8-86645EE2E753.smethod_950());
				this.gform12_0.method_40(this.gform12_0.label_11, EC2D41B1-A2F9-4664-90D8-86645EE2E753.smethod_951());
			}

			// Token: 0x040002B8 RID: 696
			public Exception exception_0;

			// Token: 0x040002B9 RID: 697
			public GForm12 gform12_0;
		}

		// Token: 0x020000CC RID: 204
		[CompilerGenerated]
		private sealed class Class146
		{
			// Token: 0x06000390 RID: 912 RVA: 0x0000D7B7 File Offset: 0x0000B9B7
			internal void method_0()
			{
				this.control_0.Text = this.string_0;
			}

			// Token: 0x040002BA RID: 698
			public Control control_0;

			// Token: 0x040002BB RID: 699
			public string string_0;
		}

		// Token: 0x020000CD RID: 205
		[CompilerGenerated]
		private sealed class Class147
		{
			// Token: 0x06000392 RID: 914 RVA: 0x0000D7CA File Offset: 0x0000B9CA
			internal void method_0()
			{
				this.string_0 = this.control_0.Text;
			}

			// Token: 0x040002BC RID: 700
			public Control control_0;

			// Token: 0x040002BD RID: 701
			public string string_0;
		}

		// Token: 0x020000CE RID: 206
		[CompilerGenerated]
		private sealed class Class148
		{
			// Token: 0x06000394 RID: 916 RVA: 0x0000D7DD File Offset: 0x0000B9DD
			internal void method_0(object sender, MouseEventArgs e)
			{
				if (e.Button == MouseButtons.Left)
				{
					this.gform12_0.bool_0 = true;
					this.gform12_0.point_0 = e.Location;
				}
			}

			// Token: 0x06000395 RID: 917 RVA: 0x00034274 File Offset: 0x00032474
			internal void method_1(object sender, MouseEventArgs e)
			{
				if (this.gform12_0.bool_0)
				{
					this.gform12_0.Location = new Point(this.gform12_0.Location.X - this.gform12_0.point_0.X + e.X, this.gform12_0.Location.Y - this.gform12_0.point_0.Y + e.Y);
				}
			}

			// Token: 0x06000396 RID: 918 RVA: 0x0000D809 File Offset: 0x0000BA09
			internal void method_2(object sender, MouseEventArgs e)
			{
				this.gform12_0.bool_0 = false;
			}

			// Token: 0x06000397 RID: 919 RVA: 0x0000D817 File Offset: 0x0000BA17
			internal void method_3(object sender, EventArgs e)
			{
				this.gform12_0.label_3_Click(sender, e);
			}

			// Token: 0x06000398 RID: 920 RVA: 0x000342F4 File Offset: 0x000324F4
			internal void method_4(object sender, EventArgs e)
			{
				this.label_0.Left--;
				if (this.label_0.Right < 0)
				{
					this.label_0.Left = this.gform12_0.label_3.Width;
				}
				if (this.bool_0)
				{
					this.int_0 += 4;
					if (this.int_0 >= 255)
					{
						this.int_0 = 255;
						this.bool_0 = false;
					}
				}
				else
				{
					this.int_0 -= 4;
					if (this.int_0 <= 100)
					{
						this.int_0 = 100;
						this.bool_0 = true;
					}
				}
				this.label_0.ForeColor = Color.FromArgb(this.int_0, this.int_0, (int)((double)this.int_0 * 0.8));
			}

			// Token: 0x040002BE RID: 702
			public GForm12 gform12_0;

			// Token: 0x040002BF RID: 703
			public Label label_0;

			// Token: 0x040002C0 RID: 704
			public bool bool_0;

			// Token: 0x040002C1 RID: 705
			public int int_0;
		}

		// Token: 0x020000CF RID: 207
		[CompilerGenerated]
		private sealed class Class149
		{
			// Token: 0x0600039A RID: 922 RVA: 0x0000D826 File Offset: 0x0000BA26
			internal void method_0()
			{
				this.control_0.Enabled = this.bool_0;
			}

			// Token: 0x040002C2 RID: 706
			public Control control_0;

			// Token: 0x040002C3 RID: 707
			public bool bool_0;
		}

		// Token: 0x020000D0 RID: 208
		[CompilerGenerated]
		private sealed class Class150
		{
			// Token: 0x0600039C RID: 924 RVA: 0x000343CC File Offset: 0x000325CC
			internal void method_0()
			{
				GForm12.smethod_7(this.textBox_0);
				GForm12.smethod_7(this.textBox_1);
				byte b;
				bool flag = GForm12.smethod_2(this.textBox_0.Text, out b);
				byte b2;
				bool flag2 = GForm12.smethod_2(this.textBox_1.Text, out b2);
				this.button_0.Enabled = (flag && flag2);
				this.button_0.BackColor = ((flag && flag2) ? Color.FromArgb(180, 0, 0) : Color.FromArgb(60, 20, 20));
				this.button_0.FlatAppearance.BorderColor = ((flag && flag2) ? Color.Red : Color.DarkRed);
				if (flag && flag2)
				{
					this.label_0.Text = string.Format(EC2D41B1-A2F9-4664-90D8-86645EE2E753.smethod_952(), new object[]
					{
						b,
						b2,
						b,
						b2,
						(char)b,
						(char)b2
					});
					return;
				}
				this.label_0.Text = EC2D41B1-A2F9-4664-90D8-86645EE2E753.smethod_953();
			}

			// Token: 0x0600039D RID: 925 RVA: 0x0000D839 File Offset: 0x0000BA39
			internal void method_1(object sender, EventArgs e)
			{
				this.method_0();
			}

			// Token: 0x0600039E RID: 926 RVA: 0x0000D839 File Offset: 0x0000BA39
			internal void method_2(object sender, EventArgs e)
			{
				this.method_0();
			}

			// Token: 0x040002C4 RID: 708
			public TextBox textBox_0;

			// Token: 0x040002C5 RID: 709
			public TextBox textBox_1;

			// Token: 0x040002C6 RID: 710
			public Button button_0;

			// Token: 0x040002C7 RID: 711
			public Label label_0;
		}

		// Token: 0x020000D1 RID: 209
		[CompilerGenerated]
		private sealed class Class151
		{
			// Token: 0x060003A0 RID: 928 RVA: 0x000344D4 File Offset: 0x000326D4
			internal void method_0(object sender, EventArgs e)
			{
				string str = Regex.Replace(this.toolStripMenuItem_0.Text, EC2D41B1-A2F9-4664-90D8-86645EE2E753.smethod_954(), EC2D41B1-A2F9-4664-90D8-86645EE2E753.smethod_3()).Trim();
				GForm15.smethod_1(EC2D41B1-A2F9-4664-90D8-86645EE2E753.smethod_182(), EC2D41B1-A2F9-4664-90D8-86645EE2E753.smethod_955() + str, GEnum1.const_1);
			}

			// Token: 0x040002C8 RID: 712
			public ToolStripMenuItem toolStripMenuItem_0;
		}
	}
}
