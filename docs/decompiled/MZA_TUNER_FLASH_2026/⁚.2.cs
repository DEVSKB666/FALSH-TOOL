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
using Attr_2;
using Form_4;

namespace Attr_3
{
	// Token: 0x020000B5 RID: 181
	public partial class Type_50 : Form
	{
		// Token: 0x06000234 RID: 564
		[DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
		private static extern IntPtr \u00A0(int, int, int, int, int, int);

		// Token: 0x06000235 RID: 565 RVA: 0x000147F0 File Offset: 0x000129F0
		private string \u00A0()
		{
			return "ANTI";
		}

		// Token: 0x06000236 RID: 566 RVA: 0x000147F7 File Offset: 0x000129F7
		private int \u00A0(int A_1)
		{
			return A_1 << 2 ^ 90;
		}

		// Token: 0x06000237 RID: 567 RVA: 0x000147FF File Offset: 0x000129FF
		private bool \u1680()
		{
			return new Random().Next(0, 100) > -1;
		}

		// Token: 0x06000238 RID: 568
		[DllImport("user32.dll", CharSet = CharSet.Auto, EntryPoint = "SendMessage")]
		private static extern IntPtr \u00A0(IntPtr, uint, IntPtr, IntPtr);

		// Token: 0x06000239 RID: 569
		[DllImport("user32.dll", EntryPoint = "ReleaseCapture")]
		public static extern bool \u2000();

		// Token: 0x0600023A RID: 570
		[DllImport("user32.dll", EntryPoint = "SendMessage")]
		public static extern IntPtr \u00A0(IntPtr, int, int, int);

		// Token: 0x0600023B RID: 571
		[DllImport("user32.dll", EntryPoint = "SetWindowCompositionAttribute")]
		internal static extern int \u00A0(IntPtr, ref \u205A.\u2002);

		// Token: 0x0600023C RID: 572 RVA: 0x00014811 File Offset: 0x00012A11
		[CompilerGenerated]
		public object \u2001()
		{
			return this.\u2002;
		}

		// Token: 0x0600023D RID: 573 RVA: 0x00014819 File Offset: 0x00012A19
		[CompilerGenerated]
		private void \u00A0(object A_1)
		{
			this.\u2002 = A_1;
		}

		// Token: 0x0600023E RID: 574 RVA: 0x00014822 File Offset: 0x00012A22
		[CompilerGenerated]
		public object \u2002()
		{
			return this.\u2003;
		}

		// Token: 0x0600023F RID: 575 RVA: 0x0001482A File Offset: 0x00012A2A
		[CompilerGenerated]
		private void \u1680(object A_1)
		{
			this.\u2003 = A_1;
		}

		// Token: 0x06000240 RID: 576 RVA: 0x00014834 File Offset: 0x00012A34
		public Type_50()
		{
			this.\u2011();
			this.\u2027();
			this.\u2007();
			try
			{
				global::Attr_3.Type_5F.\u1680();
			}
			catch
			{
			}
			global::Attr_3.Type_50.\u2006 = true;
			this.\u2001(this);
			this.\u2025();
			if (this.\u2002 != null)
			{
				this.Type_6.Click -= this.\u1680\u202D;
				this.Type_6.Click += this.\u1680\u202D;
			}
			base.Shown += this.\u1680\u2048;
			this.\u00A0(null);
		}

		// Token: 0x06000241 RID: 577 RVA: 0x00014954 File Offset: 0x00012B54
		private void \u2003()
		{
			if (LicenseManager.UsageMode == LicenseUsageMode.Designtime || Process.GetCurrentProcess().ProcessName.ToLower().Contains("devenv"))
			{
				return;
			}
			this.DoubleBuffered = true;
			this.\u00A0 = ((this.\u00A0 != null) ? this.Attr_2.BackColor : Color.Red);
			for (int i = 0; i < 60; i++)
			{
				this.Attr_2.Add(this.\u00A0(true));
			}
			this.\u1680 = true;
			Task.Run(new Func<Task>(this.\u2028));
			if (this.\u2002 != null)
			{
				this.Type_6.Paint += this.\u2002;
			}
		}

		// Token: 0x06000242 RID: 578 RVA: 0x00014A03 File Offset: 0x00012C03
		protected override void OnFormClosing(FormClosingEventArgs A_1)
		{
			global::Form_4.Attr_5.\u00A0("Goodbye Master. Shutting down.");
			this.\u1680 = false;
			base.OnFormClosing(A_1);
		}

		// Token: 0x06000243 RID: 579 RVA: 0x00014A20 File Offset: 0x00012C20
		private \u205A.\u2009 \u00A0(bool A_1 = false)
		{
			return new \u205A.\u2009
			{
				\u00A0 = (float)this.Attr_2.Next(0, base.Width),
				\u1680 = (float)(A_1 ? this.Attr_2.Next(0, base.Height) : -10),
				\u2000 = (float)(this.Attr_2.NextDouble() * 1.5 + 0.5),
				\u2001 = (float)(this.Attr_2.NextDouble() * 3.0 + 1.5),
				\u2002 = (float)(this.Attr_2.NextDouble() * 1.5 - 0.75),
				\u00A0 = this.Attr_2.Next(150, 255)
			};
		}

		// Token: 0x06000244 RID: 580 RVA: 0x00014AF8 File Offset: 0x00012CF8
		private void \u2004()
		{
			object u = this.\u1680;
			lock (u)
			{
				for (int i = 0; i < this.Attr_2.Count; i++)
				{
					\u205A.\u2009 u2 = this.\u00A0[i];
					u2.\u1680 += u2.\u2000;
					u2.\u00A0 += u2.\u2002;
					if (u2.\u1680 > (float)base.Height)
					{
						this.\u00A0[i] = this.\u00A0(false);
					}
				}
			}
		}

		// Token: 0x06000245 RID: 581 RVA: 0x00014BA0 File Offset: 0x00012DA0
		private void \u2005()
		{
			\u205A.\u2014 u = new \u205A.\u2014();
			u.\u00A0 = this;
			this.\u2000 = true;
			this.\u00A0 = 230f;
			u.\u00A0 = new string[]
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
			Task.Run(new Func<Task>(u.\u00A0));
		}

		// Token: 0x06000246 RID: 582 RVA: 0x00014C33 File Offset: 0x00012E33
		protected override void OnPaint(PaintEventArgs A_1)
		{
			base.OnPaint(A_1);
			this.\u00A0(A_1.Graphics, this);
			this.\u00A0(A_1.Graphics);
		}

		// Token: 0x06000247 RID: 583 RVA: 0x00014C58 File Offset: 0x00012E58
		private void \u00A0(Graphics A_1)
		{
			if (!this.\u2000)
			{
				return;
			}
			A_1.SmoothingMode = SmoothingMode.AntiAlias;
			Rectangle rect = new Rectangle(0, 0, base.Width, base.Height);
			using (SolidBrush solidBrush = new SolidBrush(Color.FromArgb((int)this.\u00A0, 5, 5, 5)))
			{
				A_1.FillRectangle(solidBrush, rect);
			}
			using (Pen pen = new Pen(Color.FromArgb(20, 0, 255, 0), 1f))
			{
				for (int i = 0; i < base.Height; i += 3)
				{
					A_1.DrawLine(pen, 0, i, base.Width, i);
				}
			}
			int num = 500;
			int num2 = 300;
			Rectangle rectangle = new Rectangle((base.Width - num) / 2, (base.Height - num2) / 2, num, num2);
			using (GraphicsPath graphicsPath = this.\u00A0(rectangle, 10))
			{
				using (PathGradientBrush pathGradientBrush = new PathGradientBrush(graphicsPath))
				{
					pathGradientBrush.CenterColor = Color.FromArgb(40, this.\u00A0);
					pathGradientBrush.SurroundColors = new Color[]
					{
						Color.Transparent
					};
					A_1.FillPath(pathGradientBrush, graphicsPath);
				}
			}
			Color color = Color.FromArgb((int)this.\u00A0, this.\u00A0);
			using (Font font = new Font("Consolas", 10f, FontStyle.Bold))
			{
				float num3 = (float)(rectangle.Y + 20);
				object u = this.\u2000;
				lock (u)
				{
					foreach (string s in this.\u00A0)
					{
						A_1.DrawString(s, font, new SolidBrush(Color.FromArgb((int)((double)this.\u00A0 * 0.7), color)), (float)(rectangle.X + 20), num3);
						num3 += 20f;
					}
					if (!string.IsNullOrEmpty(this.\u2000))
					{
						A_1.DrawString(this.\u2000, font, new SolidBrush(color), (float)(rectangle.X + 20), num3);
					}
				}
			}
			using (Pen pen2 = new Pen(Color.FromArgb((int)this.\u00A0, this.\u00A0), 1f))
			{
				A_1.DrawRectangle(pen2, rectangle);
				int num4 = 15;
				using (Pen pen3 = new Pen(color, 3f))
				{
					A_1.DrawLine(pen3, rectangle.X, rectangle.Y, rectangle.X + num4, rectangle.Y);
					A_1.DrawLine(pen3, rectangle.X, rectangle.Y, rectangle.X, rectangle.Y + num4);
					A_1.DrawLine(pen3, rectangle.Right - num4, rectangle.Bottom, rectangle.Right, rectangle.Bottom);
					A_1.DrawLine(pen3, rectangle.Right, rectangle.Bottom - num4, rectangle.Right, rectangle.Bottom);
				}
			}
		}

		// Token: 0x06000248 RID: 584 RVA: 0x00014FF4 File Offset: 0x000131F4
		private GraphicsPath \u00A0(Rectangle A_1, int A_2)
		{
			GraphicsPath graphicsPath = new GraphicsPath();
			float num = (float)(A_2 * 2);
			graphicsPath.AddArc((float)A_1.X, (float)A_1.Y, num, num, 180f, 90f);
			graphicsPath.AddArc((float)A_1.Right - num, (float)A_1.Y, num, num, 270f, 90f);
			graphicsPath.AddArc((float)A_1.Right - num, (float)A_1.Bottom - num, num, num, 0f, 90f);
			graphicsPath.AddArc((float)A_1.X, (float)A_1.Bottom - num, num, num, 90f, 90f);
			graphicsPath.CloseFigure();
			return graphicsPath;
		}

		// Token: 0x06000249 RID: 585 RVA: 0x000150A4 File Offset: 0x000132A4
		private void \u00A0(Graphics A_1, Control A_2)
		{
			try
			{
				if (A_2 != null && A_1 != null && this.\u1680)
				{
					if (A_2.IsHandleCreated && !A_2.IsDisposed)
					{
						A_1.SmoothingMode = SmoothingMode.AntiAlias;
						object u = this.\u1680;
						\u205A.\u2009[] array;
						lock (u)
						{
							array = this.Attr_2.ToArray();
						}
						foreach (\u205A.\u2009 u2 in array)
						{
							Point p = new Point((int)u2.\u00A0, (int)u2.\u1680);
							Point p2 = base.PointToScreen(p);
							Point point = A_2.PointToClient(p2);
							float num = (float)point.X;
							float num2 = (float)point.Y;
							if (num >= -10f && num <= (float)(A_2.Width + 10) && num2 >= -10f && num2 <= (float)(A_2.Height + 10))
							{
								using (SolidBrush solidBrush = new SolidBrush(Color.FromArgb(u2.\u00A0 / 5, this.\u00A0)))
								{
									A_1.FillEllipse(solidBrush, num - u2.\u2001, num2 - u2.\u2001, u2.\u2001 * 3f, u2.\u2001 * 3f);
								}
								using (SolidBrush solidBrush2 = new SolidBrush(Color.FromArgb(u2.\u00A0, this.\u00A0)))
								{
									A_1.FillEllipse(solidBrush2, num, num2, u2.\u2001, u2.\u2001);
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

		// Token: 0x0600024A RID: 586 RVA: 0x000152AC File Offset: 0x000134AC
		private void \u00A0(Color? A_1 = null)
		{
			try
			{
				Color backColor = Color.FromArgb(25, 25, 25);
				Color white = Color.White;
				Color color = A_1 ?? ((this.\u00A0 != null) ? this.Attr_2.BackColor : Color.FromArgb(200, 20, 20));
				this.\u00A0 = color;
				Font font = new Font("Segoe UI", 10f, FontStyle.Bold);
				foreach (Control control in new Control[]
				{
					this.\u2000,
					this.\u1680,
					this.\u00A0
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
				if (this.\u1680 != null)
				{
					this.Attr_3.\u00A0(color);
				}
			}
			catch
			{
			}
		}

		// Token: 0x0600024B RID: 587 RVA: 0x000154D8 File Offset: 0x000136D8
		private void \u2006()
		{
			try
			{
				\u205A.\u2005 structure = new \u205A.\u2005
				{
					\u00A0 = global::Attr_3.Type_50.Form_8.\u2002,
					\u1680 = -1123415542
				};
				int num = Marshal.SizeOf<\u205A.\u2005>(structure);
				IntPtr intPtr = Marshal.AllocHGlobal(num);
				Marshal.StructureToPtr<\u205A.\u2005>(structure, intPtr, false);
				\u205A.\u2002 u = default(\u205A.\u2002);
				u.\u00A0 = global::Attr_3.Type_50.Type_7.\u00A0;
				u.\u00A0 = num;
				u.\u00A0 = intPtr;
				global::Attr_3.Type_50.\u00A0(base.Handle, ref u);
				Marshal.FreeHGlobal(intPtr);
				this.BackColor = Color.FromArgb(10, 10, 10);
				base.Opacity = 1.0;
			}
			catch
			{
				base.Opacity = 0.9;
			}
		}

		// Token: 0x0600024C RID: 588 RVA: 0x00015590 File Offset: 0x00013790
		private void \u2007()
		{
			try
			{
				Control control = base.Controls.Find("label_batrei", true).FirstOrDefault<Control>() ?? base.Controls.Find("TxtBatteryVolt", true).FirstOrDefault<Control>();
				if (control != null)
				{
					\u206B u206B = new Type_5C();
					u206B.Name = "modernBatteryGauge";
					u206B.Size = new Size(180, 80);
					u206B.Location = new Point(control.Location.X - 10, control.Location.Y - 15);
					u206B.Parent = control.Parent;
					control.Visible = false;
					u206B.BringToFront();
				}
			}
			catch
			{
			}
		}

		// Token: 0x0600024D RID: 589 RVA: 0x00015650 File Offset: 0x00013850
		private byte \u00A0(byte[] A_1, int A_2, int A_3 = 0)
		{
			int num = A_3 + A_2;
			byte b = 0;
			while (A_3 < num)
			{
				b += A_1[A_3];
				A_3++;
			}
			return b;
		}

		// Token: 0x0600024E RID: 590 RVA: 0x00015678 File Offset: 0x00013878
		private uint \u1680(byte[] A_1, int A_2, int A_3 = 0)
		{
			int num = A_3 + A_2;
			uint num2 = 0U;
			while (A_3 + 4 <= num)
			{
				num2 += BitConverter.ToUInt32(A_1, A_3);
				A_3 += 4;
			}
			return num2;
		}

		// Token: 0x0600024F RID: 591 RVA: 0x000156A8 File Offset: 0x000138A8
		private byte \u2000(byte[] A_1, int A_2, int A_3 = 0)
		{
			int num = A_3 + A_2;
			int num2 = 0;
			while (A_3 < num)
			{
				num2 += (int)A_1[A_3];
				A_3++;
			}
			return (byte)(255 - (num2 - 1 >> 8));
		}

		// Token: 0x06000250 RID: 592 RVA: 0x000156DC File Offset: 0x000138DC
		private byte \u2001(byte[] A_1, int A_2, int A_3 = 0)
		{
			int num = A_3 + A_2;
			int num2 = 0;
			while (A_3 < num)
			{
				num2 += (int)A_1[A_3];
				A_3++;
			}
			return (byte)((num2 ^ 255) + 1 & 255);
		}

		// Token: 0x06000251 RID: 593 RVA: 0x00015712 File Offset: 0x00013912
		private bool \u00A0(byte[] A_1, int A_2)
		{
			return this.\u2001(A_1, A_2 - 1, 0) == A_1[A_2 - 1];
		}

		// Token: 0x06000252 RID: 594 RVA: 0x00015728 File Offset: 0x00013928
		private bool \u00A0(byte[] A_1, int A_2, ref byte[] A_3, ref uint A_4, int A_5 = 0)
		{
			byte[] array = new byte[256];
			byte[] array2 = new byte[256];
			uint num = 0U;
			uint num2 = 0U;
			uint num3 = 0U;
			uint num4 = 0U;
			uint num5 = 0U;
			long num6 = (long)(50 + 2 * A_2);
			bool result;
			if (FTDI.FT_Write(global::Attr_3.Type_50.\u00A0, A_1, (uint)A_2, ref num) > FTDI.FT_STATUS.FT_OK)
			{
				result = false;
			}
			else if (FTDI.FT_SetLatencyTimer(global::Attr_3.Type_50.\u00A0, 8) > FTDI.FT_STATUS.FT_OK)
			{
				result = false;
			}
			else
			{
				if (A_5 > 0)
				{
					Thread.Sleep(A_5);
				}
				Stopwatch stopwatch = new Stopwatch();
				stopwatch.Start();
				do
				{
					if (FTDI.FT_GetQueueStatus(global::Attr_3.Type_50.\u00A0, ref num2) == FTDI.FT_STATUS.FT_OK && num2 != 0U && (ulong)num2 < (ulong)((long)array.Length) && FTDI.FT_Read(global::Attr_3.Type_50.\u00A0, array, num2, ref num3) == FTDI.FT_STATUS.FT_OK && num3 != 0U)
					{
						Array.Copy(array, 0L, array2, (long)((ulong)num4), (long)((ulong)num3));
						num4 += num3;
						if ((ulong)num4 >= (ulong)((long)(A_2 + 2)))
						{
							if (num5 == 0U)
							{
								num5 = (uint)array2[A_2 + 1];
							}
							if ((ulong)num4 - (ulong)((long)A_2) == (ulong)num5)
							{
								A_4 = num5;
								Array.Copy(array2, (long)((ulong)(num4 - num5)), A_3, 0L, (long)((ulong)num5));
								if (this.\u00A0(A_3, (int)num5))
								{
									goto IL_11E;
								}
							}
						}
					}
				}
				while (stopwatch.ElapsedMilliseconds < num6);
				stopwatch.Stop();
				FTDI.FT_Purge(global::Attr_3.Type_50.\u00A0, 3U);
				return false;
				IL_11E:
				stopwatch.Stop();
				FTDI.FT_Purge(global::Attr_3.Type_50.\u00A0, 3U);
				result = true;
			}
			return result;
		}

		// Token: 0x06000253 RID: 595 RVA: 0x0001586C File Offset: 0x00013A6C
		private void \u00A0(int A_1, \u205A.\u2006 A_2, bool A_3 = false)
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
			int num2 = 0;
			int num3 = 0;
			int num4 = 128;
			int num5 = 8;
			int num6 = A_1 / 16;
			int num7 = 0;
			int i = 0;
			int num8 = global::Attr_3.Type_50.\u2000 / 128;
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
			if (A_2 == global::Attr_3.Type_50.Form_A.\u00A0 && A_3)
			{
				if (this.\u00A0(array, array.Length, ref array14, ref num, 0) && array14[1] == 5 && array14[3] == 0)
				{
					FTDI.FT_SetBaudRate(global::Attr_3.Type_50.\u00A0, 921600U);
				}
			}
			else
			{
				switch (A_2)
				{
				case global::Attr_3.Type_50.Form_A.\u1680:
					num4 = 64;
					num5 = 4;
					num8 = global::Attr_3.Type_50.\u2000 / 64;
					array15[0] = 126;
					array15[1] = 75;
					array15[2] = 1;
					array15[3] = 6;
					flag = false;
					break;
				case global::Attr_3.Type_50.Form_A.\u2000:
					flag = false;
					break;
				case global::Attr_3.Type_50.Form_A.\u2001:
					array15[0] = 124;
					array15[1] = 139;
					array15[2] = 2;
					array15[3] = 6;
					i = A_1 / num4;
					if (!A_3)
					{
						num2 = 150;
					}
					break;
				case global::Attr_3.Type_50.Form_A.\u2002:
					array15[0] = 124;
					array15[1] = 139;
					array15[2] = 2;
					array15[3] = 6;
					i = A_1 / num4;
					flag = false;
					if (!A_3)
					{
						num2 = 150;
					}
					break;
				case global::Attr_3.Type_50.Form_A.\u2003:
					array15[0] = 124;
					array15[1] = 139;
					array15[2] = 2;
					array15[3] = 6;
					i = A_1 / num4;
					flag = false;
					if (!A_3)
					{
						num2 = 150;
					}
					break;
				}
			}
			stopwatch.Start();
			while (i < num8)
			{
				array15[4] = (byte)(num6 >> 8 & 255);
				array15[5] = (byte)(num6 & 255);
				Array.Copy(global::Attr_3.Type_50.\u200A, i * num4, array15, 6, num4);
				if (flag)
				{
					for (int k = 1; k < num8 - i; k++)
					{
						if (!this.\u00A0(global::Attr_3.Type_50.\u200A, (i + k) * num4, array16, num4))
						{
							num7 = num6 + k * num5;
							i += k - 1;
							break;
						}
						if (i + k + 1 == num8)
						{
							i += k;
						}
					}
				}
				else
				{
					num7 = num6 + num5;
				}
				if (i + 1 == num8)
				{
					num7 = 0;
				}
				array15[6 + num4] = (byte)(num7 >> 8 & 255);
				array15[6 + num4 + 1] = (byte)(num7 & 255);
				array15[6 + num4 + 2] = this.\u2000(array15, num4 + 4, 4);
				array15[6 + num4 + 3] = this.\u2001(array15, num4 + 4, 4);
				array15[6 + num4 + 4] = this.\u2001(array15, (int)(array15[1] - 1), 0);
				while (this.\u00A0(array15, (int)array15[1], ref array14, ref num, num2))
				{
					double num9 = (double)(i + 1) * (double)num4;
					double num10 = num9 / (double)global::Attr_3.Type_50.\u2000;
					\u205A.\u2007 u;
					if (array14[1] == 7)
					{
						if (array14[4] == 0 && array14[5] == 0)
						{
							if (num3 < 2)
							{
								num3++;
								continue;
							}
							u = global::Attr_3.Type_50.Type_14.\u1680;
						}
						else
						{
							u = global::Attr_3.Type_50.Type_14.\u1680;
						}
					}
					else if (array14[1] == 5)
					{
						TimeSpan elapsed = stopwatch.Elapsed;
						if (num4 == 64 && (i + 1) % 2 == 0)
						{
							this.\u00A0(array3, (int)array3[1], ref array14, ref num, 0);
							Thread.Sleep(100);
						}
						u = global::Attr_3.Type_50.Type_14.\u00A0;
						this.\u1680(this.\u2013, string.Concat(new string[]
						{
							"กำลังเขียน : ",
							(num9 / 1024.0).ToString("F2"),
							" KB / ",
							((double)global::Attr_3.Type_50.\u2000 / 1024.0).ToString("F2"),
							" KB (",
							(num10 * 100.0).ToString("F2"),
							"%)"
						}));
					}
					else
					{
						u = global::Attr_3.Type_50.Type_14.\u1680;
					}
					this.\u00A0(this.\u00A0, (int)(num10 * 10000.0));
					if (u != global::Attr_3.Type_50.Type_14.\u1680)
					{
						num6 = num7;
						i++;
						break;
					}
					stopwatch.Stop();
					base.BeginInvoke(new Action(global::Attr_3.Type_50.Type_29.Attr_2.\u00A0));
					this.\u00A0(this.\u00A0, 0);
					this.\u1680(this.\u2013, "");
					return;
				}
			}
			stopwatch.Stop();
			if (A_2 == global::Attr_3.Type_50.Form_A.\u2001 || A_2 == global::Attr_3.Type_50.Form_A.\u2002 || A_2 == global::Attr_3.Type_50.Form_A.\u2003)
			{
				this.\u00A0(array9, array9.Length, ref array14, ref num, num2);
				Thread.Sleep(1000);
				this.\u00A0(array10, array10.Length, ref array14, ref num, num2);
				Thread.Sleep(1000);
				this.\u00A0(array11, array11.Length, ref array14, ref num, num2);
				Thread.Sleep(1000);
				this.\u00A0(array12, array12.Length, ref array14, ref num, num2);
				Thread.Sleep(3000);
				if (this.\u00A0(array13, array13.Length, ref array14, ref num, num2) && array14[3] == 15)
				{
					base.BeginInvoke(new Action(this.\u202A));
					this.\u00A0(this.\u00A0, 0);
					this.\u1680(this.\u2013, "");
					return;
				}
			}
			else
			{
				if (num4 == 64)
				{
					this.\u00A0(array3, (int)array3[1], ref array14, ref num, 0);
					Thread.Sleep(100);
				}
				this.\u00A0(array4, array4.Length, ref array14, ref num, 0);
				Thread.Sleep(1000);
				this.\u00A0(array2, array2.Length, ref array14, ref num, 0);
				this.\u00A0(array5, array5.Length, ref array14, ref num, 0);
				Thread.Sleep(1000);
				this.\u00A0(array2, array2.Length, ref array14, ref num, 0);
				this.\u00A0(array6, array6.Length, ref array14, ref num, 0);
				Thread.Sleep(1000);
				this.\u00A0(array2, array2.Length, ref array14, ref num, 0);
				this.\u00A0(array7, array7.Length, ref array14, ref num, 0);
				Thread.Sleep(1000);
				if (this.\u00A0(array2, array2.Length, ref array14, ref num, 0) && array14[3] == 15 && this.\u00A0(array8, array8.Length, ref array14, ref num, 0) && array14[3] == 15)
				{
					base.BeginInvoke(new Action(global::Attr_3.Type_50.Type_29.Attr_2.\u2000));
					this.\u1680(this.\u200A, "อัดไฟล์สำเร็จ!! ปิดเปิดกุญแจ ใหม่อีกครั้ง");
					this.Struct_17.ForeColor = Color.Lime;
					this.\u1680(this.\u2013, "");
					this.\u00A0(this.\u00A0, 0);
					return;
				}
			}
			base.BeginInvoke(new Action(this.\u202B));
			this.\u1680(this.\u2013, "");
			this.\u00A0(this.\u00A0, 0);
		}

		// Token: 0x06000254 RID: 596 RVA: 0x00016050 File Offset: 0x00014250
		private bool \u00A0(byte[] A_1, ref byte[] A_2)
		{
			for (int i = 0; i < global::Attr_3.Type_50.\u2000 - global::Attr_3.Type_50.Form_4.Length; i++)
			{
				if (this.\u00A0(global::Attr_3.Type_50.\u200A, i, global::Attr_3.Type_50.\u2000, global::Attr_3.Type_50.Form_4.Length))
				{
					int j = i + global::Attr_3.Type_50.Form_4.Length;
					while (j < global::Attr_3.Type_50.\u2000 - 10)
					{
						if (global::Attr_3.Type_50.\u200A[j + 8] == 238 && (global::Attr_3.Type_50.\u200A[j + 9] == 0 || global::Attr_3.Type_50.\u200A[j + 9] == 255))
						{
							ushort num = (ushort)((int)A_1[0] * 256 + (int)A_1[1]);
							ushort num2 = (ushort)((int)global::Attr_3.Type_50.\u200A[j + 1] * 256 + (int)global::Attr_3.Type_50.\u200A[j]);
							ushort num3 = (ushort)((int)global::Attr_3.Type_50.\u200A[j + 3] * 256 + (int)global::Attr_3.Type_50.\u200A[j + 2]);
							ushort num4 = (ushort)((int)global::Attr_3.Type_50.\u200A[j + 5] * 256 + (int)global::Attr_3.Type_50.\u200A[j + 4]);
							ushort num5 = num3 * (num + num2);
							if (num4 > 0)
							{
								A_2[0] = (byte)(num5 + num % num4 >> 8 & 255);
								A_2[1] = (byte)(num5 + num % num4 & 255);
								return true;
							}
							A_2[0] = (byte)(num5 + num >> 8 & 255);
							A_2[1] = (byte)(num5 + num & 255);
							return true;
						}
						else
						{
							j++;
						}
					}
				}
			}
			return false;
		}

		// Token: 0x06000255 RID: 597 RVA: 0x000161A4 File Offset: 0x000143A4
		private bool \u1680(byte[] A_1, ref byte[] A_2)
		{
			for (int i = 0; i < global::Attr_3.Type_50.\u2000 - global::Attr_3.Type_50.Attr_5.Length; i++)
			{
				if (this.\u00A0(global::Attr_3.Type_50.\u200A, i + 6, global::Attr_3.Type_50.\u2001, global::Attr_3.Type_50.Attr_5.Length))
				{
					ushort num = (ushort)((int)A_1[0] * 256 + (int)A_1[1]);
					ushort num2 = (ushort)((int)global::Attr_3.Type_50.\u200A[i + 1] * 256 + (int)global::Attr_3.Type_50.\u200A[i]);
					ushort num3 = (ushort)((int)global::Attr_3.Type_50.\u200A[i + 3] * 256 + (int)global::Attr_3.Type_50.\u200A[i + 2]);
					ushort num4 = (ushort)((int)global::Attr_3.Type_50.\u200A[i + 5] * 256 + (int)global::Attr_3.Type_50.\u200A[i + 4]);
					ushort num5 = num3 * (num + num2);
					bool result;
					if (num4 > 0)
					{
						A_2[0] = (byte)(num5 + num % num4 >> 8 & 255);
						A_2[1] = (byte)(num5 + num % num4 & 255);
						result = true;
					}
					else
					{
						A_2[0] = (byte)(num5 + num >> 8 & 255);
						A_2[1] = (byte)(num5 + num & 255);
						result = true;
					}
					return result;
				}
			}
			return false;
		}

		// Token: 0x06000256 RID: 598 RVA: 0x000162A8 File Offset: 0x000144A8
		private void \u00A0(\u205A.\u2006 A_1)
		{
			for (int i = 0; i < global::Attr_3.Type_50.\u2000 - global::Attr_3.Type_50.Form_4.Length; i++)
			{
				if (this.\u00A0(global::Attr_3.Type_50.\u200A, i, global::Attr_3.Type_50.\u2000, global::Attr_3.Type_50.Form_4.Length))
				{
					for (int j = i + global::Attr_3.Type_50.Form_4.Length; j < global::Attr_3.Type_50.\u2000 - 6; j++)
					{
						if (global::Attr_3.Type_50.\u200A[j] == 208 && global::Attr_3.Type_50.\u200A[j + 1] == 7)
						{
							int num = j;
							if ((long)j - ((long)j + (long)((ulong)((uint)(j - 4 >> 31) >> 30)) - 4L & (long)((ulong)-4)) == 6L)
							{
								num = j - 6;
							}
							this.\u1680(this.\u00A0, (num - 4).ToString("X"));
							uint num2 = this.\u1680(global::Attr_3.Type_50.\u200A, num - 4, 0) + this.\u1680(global::Attr_3.Type_50.\u200A, global::Attr_3.Type_50.\u2000 - num, num);
							byte[] bytes = BitConverter.GetBytes(0U - num2);
							global::Attr_3.Type_50.\u200A[num - 4] = bytes[0];
							global::Attr_3.Type_50.\u200A[num - 3] = bytes[1];
							global::Attr_3.Type_50.\u200A[num - 2] = bytes[2];
							global::Attr_3.Type_50.\u200A[num - 1] = bytes[3];
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

		// Token: 0x06000257 RID: 599 RVA: 0x0001645C File Offset: 0x0001465C
		private bool \u00A0(bool A_1 = false, bool A_2 = false)
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
			int num2 = 0;
			if (!A_1)
			{
				num2 = 150;
			}
			this.\u1680(this.\u2013, "กำลังส่งสัญญาณปลุกกล่อง Shindengen...");
			this.\u00A0(array, array.Length, ref array10, ref num, num2);
			this.\u00A0(array2, array2.Length, ref array10, ref num, num2);
			base.Invoke(new Action(this.\u202C));
			this.\u1680(this.\u2013, "กำลังตรวจสอบ Seed/Key...");
			this.\u00A0(this.\u00A0, 0);
			this.\u00A0(array3, array3.Length, ref array10, ref num, num2);
			bool result;
			if (array10[1] == 7 && array10[3] == 0)
			{
				byte[] array11 = new byte[2];
				byte[] array12 = new byte[2];
				array11[0] = array10[4];
				array11[1] = array10[5];
				if ((!A_2) ? this.\u00A0(array11, ref array12) : this.\u1680(array11, ref array12))
				{
					array4[4] = array12[0];
					array4[5] = array12[1];
					array4[6] = this.\u2001(array4, (int)(array10[1] - 1), 0);
				}
				this.\u00A0(array4, array4.Length, ref array10, ref num, num2);
				if (array10[1] == 5 && array10[3] == 0)
				{
					this.\u1680(this.\u2013, "กำลังเริ่ม ประมวลผล");
					this.\u00A0(array5, array5.Length, ref array10, ref num, num2);
					this.\u00A0(array6, array6.Length, ref array10, ref num, num2);
					this.\u00A0(array7, array7.Length, ref array10, ref num, num2);
					if (this.\u00A0(array8, array8.Length, ref array10, ref num, num2) && array10[3] == 0)
					{
						Thread.Sleep(2000);
						if (this.\u00A0(array9, array9.Length, ref array10, ref num, num2) && array10[3] == 0)
						{
							this.\u1680(this.\u2013, "เสร็จสิ้นการประมวลผล เริ่มเขียนไฟล์");
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

		// Token: 0x06000258 RID: 600 RVA: 0x00016738 File Offset: 0x00014938
		private void \u1680(int A_1 = 0)
		{
			byte b = (A_1 != 0) ? (this.\u00A0(global::Attr_3.Type_50.\u200A, A_1, 0) + this.\u00A0(global::Attr_3.Type_50.\u200A, global::Attr_3.Type_50.\u2000 - (A_1 + 1), A_1 + 1)) : this.\u00A0(global::Attr_3.Type_50.\u200A, global::Attr_3.Type_50.\u2000 - 1, 1);
			global::Attr_3.Type_50.\u200A[A_1] = -b;
		}

		// Token: 0x06000259 RID: 601 RVA: 0x00016790 File Offset: 0x00014990
		private bool \u2008()
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
			this.\u1680(this.\u2013, "กำลังส่งสัญญาณปลุกกล่อง ECU...");
			while (!this.\u00A0(array, array.Length, ref array10, ref num, 0))
			{
				Thread.Sleep(500);
				if (num2 >= 1)
				{
					IL_103:
					Thread.Sleep(200);
					this.\u00A0(array9, array9.Length, ref array10, ref num, 0);
					base.Invoke(new Action(this.\u202D));
					this.\u1680(this.\u2013, "กำลังประมวลผลเตรียมอัดไฟล์...");
					this.\u00A0(this.\u00A0, 0);
					Thread.Sleep(500);
					this.\u00A0(array3, array3.Length, ref array10, ref num, 250);
					this.\u00A0(array4, array4.Length, ref array10, ref num, 0);
					this.\u00A0(array9, array9.Length, ref array10, ref num, 0);
					this.\u00A0(array5, array5.Length, ref array10, ref num, 0);
					this.\u00A0(array9, array9.Length, ref array10, ref num, 0);
					this.\u00A0(array6, array6.Length, ref array10, ref num, 0);
					if (this.\u00A0(array7, array7.Length, ref array10, ref num, 0) && array10[3] == 0)
					{
						Thread.Sleep(2000);
						if (this.\u00A0(array8, array8.Length, ref array10, ref num, 250) && array10[3] == 0)
						{
							this.\u1680(this.\u2013, "ประมวลผลสำเร็จ กำลังเริ่มเขียนไฟล์...");
							this.\u00A0(array9, array9.Length, ref array10, ref num, 0);
							return true;
						}
						if (array10[3] == 250)
						{
							base.BeginInvoke(new Action(global::Attr_3.Type_50.Type_29.Attr_2.\u2002));
							this.\u1680(this.\u2013, "ล้มเหลว: ECM บล็อค");
							return false;
						}
					}
					base.BeginInvoke(new Action(global::Attr_3.Type_50.Type_29.Attr_2.\u2003));
					this.\u1680(this.\u2013, "เชื่อมต่อเตรียมอัดไฟล์ล้มเหลว");
					return false;
				}
				num2++;
			}
			this.\u00A0(array2, array2.Length, ref array10, ref num, 0);
			goto IL_103;
		}

		// Token: 0x0600025A RID: 602 RVA: 0x00016A4C File Offset: 0x00014C4C
		private bool \u2009()
		{
			byte[] array = new byte[1];
			byte[] array2 = new byte[]
			{
				1
			};
			uint num = 0U;
			if (FTDI.FT_Open(0U, ref global::Attr_3.Type_50.\u00A0) > FTDI.FT_STATUS.FT_OK)
			{
				FTDI.FT_Close(global::Attr_3.Type_50.\u00A0);
				return false;
			}
			if (FTDI.FT_Purge(global::Attr_3.Type_50.\u00A0, 3U) > FTDI.FT_STATUS.FT_OK)
			{
				FTDI.FT_Close(global::Attr_3.Type_50.\u00A0);
				return false;
			}
			if (FTDI.FT_SetBitMode(global::Attr_3.Type_50.\u00A0, 0, 0) > FTDI.FT_STATUS.FT_OK)
			{
				FTDI.FT_Close(global::Attr_3.Type_50.\u00A0);
				return false;
			}
			if (FTDI.FT_SetDataCharacteristics(global::Attr_3.Type_50.\u00A0, 8, 0, 0) > FTDI.FT_STATUS.FT_OK)
			{
				FTDI.FT_Close(global::Attr_3.Type_50.\u00A0);
				return false;
			}
			if (FTDI.FT_SetBaudRate(global::Attr_3.Type_50.\u00A0, 921600U) > FTDI.FT_STATUS.FT_OK)
			{
				FTDI.FT_Close(global::Attr_3.Type_50.\u00A0);
				return false;
			}
			if (FTDI.FT_SetTimeouts(global::Attr_3.Type_50.\u00A0, 50U, 0U) > FTDI.FT_STATUS.FT_OK)
			{
				FTDI.FT_Close(global::Attr_3.Type_50.\u00A0);
				return false;
			}
			if (FTDI.FT_SetLatencyTimer(global::Attr_3.Type_50.\u00A0, 8) > FTDI.FT_STATUS.FT_OK)
			{
				FTDI.FT_Close(global::Attr_3.Type_50.\u00A0);
				return false;
			}
			if (FTDI.FT_SetBitMode(global::Attr_3.Type_50.\u00A0, 1, 1) > FTDI.FT_STATUS.FT_OK)
			{
				FTDI.FT_Close(global::Attr_3.Type_50.\u00A0);
				return false;
			}
			if (FTDI.FT_Write(global::Attr_3.Type_50.\u00A0, array, (uint)array.Length, ref num) > FTDI.FT_STATUS.FT_OK)
			{
				FTDI.FT_Close(global::Attr_3.Type_50.\u00A0);
				return false;
			}
			Thread.Sleep(70);
			if (FTDI.FT_Write(global::Attr_3.Type_50.\u00A0, array2, (uint)array2.Length, ref num) > FTDI.FT_STATUS.FT_OK)
			{
				FTDI.FT_Close(global::Attr_3.Type_50.\u00A0);
				return false;
			}
			if (FTDI.FT_SetBitMode(global::Attr_3.Type_50.\u00A0, 0, 0) > FTDI.FT_STATUS.FT_OK)
			{
				FTDI.FT_Close(global::Attr_3.Type_50.\u00A0);
				return false;
			}
			if (FTDI.FT_SetBaudRate(global::Attr_3.Type_50.\u00A0, 10400U) > FTDI.FT_STATUS.FT_OK)
			{
				FTDI.FT_Close(global::Attr_3.Type_50.\u00A0);
				return false;
			}
			if (FTDI.FT_Purge(global::Attr_3.Type_50.\u00A0, 3U) > FTDI.FT_STATUS.FT_OK)
			{
				FTDI.FT_Close(global::Attr_3.Type_50.\u00A0);
				return false;
			}
			Thread.Sleep(130);
			return true;
		}

		// Token: 0x0600025B RID: 603 RVA: 0x00016BF8 File Offset: 0x00014DF8
		private void \u200A()
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
				byte[] array10 = new byte[]
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
				byte[] array11 = new byte[256];
				uint num = 0U;
				int num2 = 0;
				IL_6DA:
				while (!global::Attr_3.Type_50.\u2001)
				{
					if (this.\u2009())
					{
						if (global::Attr_3.Type_50.\u2003)
						{
							if (!global::Attr_3.Type_50.\u2002)
							{
								global::Form_4.Attr_5.\u00A0("ECU Connected.");
							}
							global::Attr_3.Type_50.\u2002 = true;
							global::Attr_3.Type_50.\u2003 = false;
						}
						this.\u00A0(array, array.Length, ref array11, ref num, 0);
						if (this.\u00A0(array2, array2.Length, ref array11, ref num, 0))
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
						else if (this.\u00A0(array6, array6.Length, ref array11, ref num, 0))
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
						while (!global::Attr_3.Type_50.\u2001)
						{
							if (!this.\u2000)
							{
								if (!this.\u00A0(array8, array8.Length, ref array11, ref num, 0))
								{
									if (global::Attr_3.Type_50.\u2002)
									{
										global::Attr_3.Type_50.\u2002 = false;
										this.\u1680(this.\u2009, "OFF");
										this.Type_16.ForeColor = Color.Red;
										this.\u1680(this.\u2008, "-");
										this.\u1680(this.\u200A, "กรุณาเชื่อมต่อรถ/เปิดกุญแจรถ !!");
										this.\u1680(this.\u2007, "-");
										this.\u1680(this.\u1680, "");
										this.\u1680(this.\u00A0, "");
									}
								}
								else
								{
									if (++num2 >= 4)
									{
										num2 = 0;
										double num3 = this.\u202E();
										if (!double.IsNaN(num3))
										{
											this.\u00A0(num3);
										}
									}
									if (!string.Equals(this.\u00A0(this.\u2009), "ON"))
									{
										string text = "-";
										string text2 = "-";
										global::Attr_3.Type_50.\u2002 = true;
										if (this.\u00A0(array3, array3.Length, ref array11, ref num, 0) && num >= 10U)
										{
											text = BitConverter.ToString(array11, 5, 5).Replace("-", "");
										}
										if (this.\u00A0(array4, array4.Length, ref array11, ref num, 0))
										{
											if (num >= 8U)
											{
												text2 = string.Format("{0}/{1}", array11[6], array11[7]);
											}
										}
										else if (this.\u00A0(array5, array5.Length, ref array11, ref num, 0) && num >= 8U)
										{
											text2 = string.Format("{0}/{1}", array11[6], array11[7]);
										}
										this.\u1680(this.\u2009, "ON");
										this.Type_16.ForeColor = Color.Green;
										this.\u1680(this.\u2008, text);
										this.\u1680(this.\u2007, text2);
										if (this.\u00A0(text) == 0)
										{
											global::Attr_3.Type_50.\u2005 = true;
										}
									}
									if (this.\u00A0)
									{
										bool flag = this.\u00A0(array, array.Length, ref array11, ref num, 0);
										bool flag2 = this.\u00A0(array2, array2.Length, ref array11, ref num, 0);
										bool flag3 = this.\u00A0(array9, array9.Length, ref array11, ref num, 0);
										object u00A = this.\u00A0;
										byte u00A2;
										byte u;
										lock (u00A)
										{
											u00A2 = this.\u00A0;
											u = this.\u1680;
										}
										byte[] array12 = global::Attr_3.Type_50.\u00A0(u00A2, u);
										bool flag5 = this.\u00A0(array12, array12.Length, ref array11, ref num, 0);
										if (!flag || !flag2 || !flag3 || !flag5 || num < 8U)
										{
											global::Attr_3.Type_50.\u2002 = false;
											this.\u00A0 = false;
											this.\u00A0 = global::Attr_3.Type_50.Type_15.\u00A0;
											this.\u1680(this.\u2009, "เชื่อมต่อไม่สำเร็จ");
											this.\u1680(this.\u200A, "ตรวจสอบสาย/สวิตช์ แล้วลองใหม่");
											goto IL_657;
										}
										if (global::Attr_3.Type_50.\u1680(array11, num, array10))
										{
											this.\u2000 = true;
											this.\u00A0 = false;
											this.\u00A0 = global::Attr_3.Type_50.Type_15.\u00A0;
											this.\u00A0(this.\u200A, "เข้าสู่โหมด Security แล้ว");
											continue;
										}
										if (this.\u00A0 == global::Attr_3.Type_50.Type_15.\u1680)
										{
											if (global::Attr_3.Type_50.\u00A0(array11, num))
											{
												int num4 = this.\u00A0 + 1;
												this.\u00A0 = num4;
												if (num4 >= 2)
												{
													this.\u1680(this.\u200A, "ปิดแล้ว ✅ กรุณาเปิดสวิตช์กุญแจ (ON)");
													this.\u00A0 = global::Attr_3.Type_50.Type_15.\u2000;
												}
											}
											else
											{
												this.\u00A0 = 0;
											}
											Thread.Sleep(200);
											continue;
										}
										if (this.\u00A0 == global::Attr_3.Type_50.Type_15.\u2000)
										{
											if (!global::Attr_3.Type_50.\u00A0(array11, num))
											{
												this.\u1680(this.\u200A, "ส่งคำสั่งพิเศษเรียบร้อย ✅");
												this.\u00A0 = false;
												this.\u00A0 = global::Attr_3.Type_50.Type_15.\u00A0;
												this.\u00A0 = 0;
											}
											Thread.Sleep(200);
											continue;
										}
									}
									if (this.\u2000 || !global::Attr_3.Type_50.\u2007)
									{
										Thread.Sleep(300);
										continue;
									}
									global::Attr_3.Type_50.\u2007 = false;
									\u205A.\u2006 u2 = this.\u2010();
									if (false)
									{
										this.\u1680(this.\u2013, "กำลังทำการล็อคข้อมูล (Anti-Read)...");
										this.\u1680(u2);
										Thread.Sleep(500);
									}
									if (u2 == global::Attr_3.Type_50.Form_A.\u2001 || u2 == global::Attr_3.Type_50.Form_A.\u2002 || u2 == global::Attr_3.Type_50.Form_A.\u2003)
									{
										this.\u00A0(u2);
										if (this.\u00A0(global::Attr_3.Type_50.\u2006, global::Attr_3.Type_50.\u2005))
										{
											this.\u00A0(global::Attr_3.Type_50.\u2001, u2, global::Attr_3.Type_50.\u2006);
										}
										while (this.\u00A0(array7, array7.Length, ref array11, ref num, 0))
										{
											if (global::Attr_3.Type_50.\u2001)
											{
												return;
											}
											Thread.Sleep(1000);
										}
									}
									else
									{
										this.\u1680(global::Attr_3.Type_50.\u2002);
										if (this.\u2008())
										{
											this.\u00A0(global::Attr_3.Type_50.\u2001, u2, global::Attr_3.Type_50.\u2006);
										}
										while (this.\u00A0(array6, array6.Length, ref array11, ref num, 0))
										{
											if (global::Attr_3.Type_50.\u2001)
											{
												return;
											}
											Thread.Sleep(1000);
										}
									}
								}
								IL_657:
								try
								{
									FTDI.FT_Close(global::Attr_3.Type_50.\u00A0);
									goto IL_6DA;
								}
								catch
								{
									goto IL_6DA;
								}
								goto IL_668;
							}
							Thread.Sleep(1000);
						}
						break;
					}
					IL_668:
					if (!global::Attr_3.Type_50.\u2003)
					{
						global::Attr_3.Type_50.\u2003 = true;
						this.Type_16.ForeColor = Color.Red;
						this.\u1680(this.\u2008, "-");
						this.\u1680(this.\u200A, "MZA-TUNER | FLASHERNEW 2026");
						this.\u1680(this.\u2007, "-");
						this.\u1680(this.\u1680, "");
						this.\u1680(this.\u00A0, "");
					}
				}
			}
			catch (Exception ex)
			{
				\u205A.\u2022 u3 = new \u205A.\u2022();
				u3.\u00A0 = this;
				Exception u00A3 = ex;
				u3.\u00A0 = u00A3;
				base.BeginInvoke(new Action(u3.\u00A0));
			}
			finally
			{
				try
				{
					FTDI.FT_Close(global::Attr_3.Type_50.\u00A0);
				}
				catch
				{
				}
			}
		}

		// Token: 0x0600025C RID: 604 RVA: 0x000173AC File Offset: 0x000155AC
		private void \u1680(\u205A.\u2006 A_1)
		{
			try
			{
				if (global::Attr_3.Type_50.\u200A != null && global::Attr_3.Type_50.Struct_17.Length >= 1024)
				{
					byte[] bytes = Encoding.ASCII.GetBytes("MZA_SHIELD_2026_ULTRA_PRO");
					int destinationIndex = global::Attr_3.Type_50.Struct_17.Length / 2;
					Array.Copy(bytes, 0, global::Attr_3.Type_50.\u200A, destinationIndex, bytes.Length);
					if (A_1 == global::Attr_3.Type_50.Form_A.\u1680 || A_1 == global::Attr_3.Type_50.Form_A.\u2000 || A_1 == global::Attr_3.Type_50.Form_A.\u00A0)
					{
						if (global::Attr_3.Type_50.Struct_17.Length >= 32768)
						{
							global::Attr_3.Type_50.\u200A[32764] = 85;
							global::Attr_3.Type_50.\u200A[32765] = 170;
							global::Attr_3.Type_50.\u200A[32766] = byte.MaxValue;
							global::Attr_3.Type_50.\u200A[32767] = 0;
							for (int i = 0; i < 4; i++)
							{
								byte[] u200A = global::Attr_3.Type_50.\u200A;
								int num = global::Attr_3.Type_50.Struct_17.Length - 10 - i;
								u200A[num] ^= 90;
							}
						}
					}
					else
					{
						for (int j = 0; j < 64; j++)
						{
							if (global::Attr_3.Type_50.Struct_17.Length > 512 + j)
							{
								global::Attr_3.Type_50.\u200A[512 + j] = (global::Attr_3.Type_50.\u200A[512 + j] ^ 51);
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
						Array.Copy(array, 0, global::Attr_3.Type_50.\u200A, global::Attr_3.Type_50.Struct_17.Length - 20, array.Length);
					}
					global::Attr_3.Type_58.\u00A0("MZA SHIELD", "ระบบล็อคข้อมูล (Deep Lock) ทำงานเรียบร้อย", global::Attr_3.Type_57.\u00A0);
				}
			}
			catch
			{
			}
		}

		// Token: 0x0600025D RID: 605 RVA: 0x0001751C File Offset: 0x0001571C
		private void \u00A0(Control A_1, string A_2)
		{
			\u205A.\u2024 u = new \u205A.\u2024();
			u.\u00A0 = A_1;
			u.\u00A0 = A_2;
			if (u.Attr_2.InvokeRequired)
			{
				u.Attr_2.BeginInvoke(new Action(u.\u00A0));
				return;
			}
			u.Attr_2.Text = u.\u00A0;
		}

		// Token: 0x0600025E RID: 606 RVA: 0x00017574 File Offset: 0x00015774
		private bool \u00A0(byte[] A_1, int A_2, byte[] A_3, int A_4)
		{
			bool result;
			if (A_2 + A_4 > A_1.Length)
			{
				result = false;
			}
			else
			{
				for (int i = 0; i < A_4; i++)
				{
					if (A_1[A_2 + i] != A_3[i])
					{
						return false;
					}
				}
				result = true;
			}
			return result;
		}

		// Token: 0x0600025F RID: 607 RVA: 0x000175AC File Offset: 0x000157AC
		private string \u00A0(Control A_1)
		{
			\u205A.\u2025 u = new \u205A.\u2025();
			u.\u00A0 = A_1;
			if (u.\u00A0 == null)
			{
				return string.Empty;
			}
			if (u.Attr_2.InvokeRequired)
			{
				u.\u00A0 = string.Empty;
				u.Attr_2.Invoke(new Action(u.\u00A0));
				return u.\u00A0;
			}
			return u.Attr_2.Text;
		}

		// Token: 0x06000260 RID: 608 RVA: 0x00017618 File Offset: 0x00015818
		private int \u00A0(string A_1)
		{
			int result;
			if (A_1.EndsWith("0000"))
			{
				for (int i = 0; i < global::Attr_3.Type_50.Attr_3.Length; i++)
				{
					if (global::Attr_3.Type_50.\u1680[i].Substring(0, 6).Equals(A_1.Substring(0, 6)))
					{
						this.\u1680(this.\u200A, "!!! ว้ายๆ กล่องไปดิ้ !!!");
						this.\u1680(this.\u1680, global::Attr_3.Type_50.\u2000[i]);
						this.\u1680(this.\u00A0, global::Attr_3.Type_50.\u2001[i]);
						global::Attr_3.Type_50.\u2004 = false;
						return 0;
					}
				}
				for (int j = 0; j < global::Attr_3.Type_50.Type_7.Length; j++)
				{
					if (global::Attr_3.Type_50.\u2003[j].Substring(0, 6).Equals(A_1.Substring(0, 6)))
					{
						this.\u1680(this.\u200A, "!!! ว้ายๆ กล่องไปดิ้ !!!");
						this.\u1680(this.\u1680, "auto");
						this.\u1680(this.\u00A0, "auto");
						global::Attr_3.Type_50.\u2004 = false;
						return 0;
					}
				}
				this.\u1680(this.\u200A, "!!! ว้ายๆ กล่องไปดิ้ !!!");
				this.\u1680(this.\u1680, "");
				this.\u1680(this.\u00A0, "");
				global::Attr_3.Type_50.\u2004 = true;
				result = 0;
			}
			else if (A_1.Contains("-"))
			{
				this.\u1680(this.\u200A, "!!! ว้ายๆ กล่องไปดิ้ !!!");
				global::Attr_3.Type_50.\u2004 = true;
				result = 0;
			}
			else
			{
				for (int k = 0; k < global::Attr_3.Type_50.Attr_3.Length; k++)
				{
					if (global::Attr_3.Type_50.\u1680[k].Equals(A_1))
					{
						this.\u1680(this.\u200A, global::Attr_3.Type_50.\u00A0[k]);
						this.\u1680(this.\u1680, global::Attr_3.Type_50.\u2000[k]);
						this.\u1680(this.\u00A0, global::Attr_3.Type_50.\u2001[k]);
						global::Attr_3.Type_50.\u2004 = false;
						return 1;
					}
				}
				for (int l = 0; l < global::Attr_3.Type_50.Type_7.Length; l++)
				{
					if (global::Attr_3.Type_50.\u2003[l].Equals(A_1))
					{
						this.\u1680(this.\u200A, global::Attr_3.Type_50.\u2002[l]);
						this.\u1680(this.\u1680, "auto");
						this.\u1680(this.\u00A0, "auto");
						global::Attr_3.Type_50.\u2004 = false;
						return 1;
					}
				}
				this.\u1680(this.\u200A, "!!! อัพเดทรหัสกล่องบ้างนะ !!!");
				this.\u1680(this.\u1680, "");
				this.\u1680(this.\u00A0, "");
				global::Attr_3.Type_50.\u2004 = true;
				result = 1;
			}
			return result;
		}

		// Token: 0x06000261 RID: 609 RVA: 0x0001787C File Offset: 0x00015A7C
		private void \u200B()
		{
			if (global::Attr_3.Type_50.\u2000 == 49152)
			{
				this.Attr_3.Text = "4000";
				this.Attr_2.Text = "7600";
				return;
			}
			if (global::Attr_3.Type_50.\u2000 == 57344)
			{
				this.Attr_3.Text = "0";
				this.Attr_2.Text = "DFEF";
				return;
			}
			if (global::Attr_3.Type_50.\u2000 == 65536 || global::Attr_3.Type_50.\u2000 == 98304)
			{
				this.Attr_3.Text = "8000";
				this.Attr_2.Text = "0";
				return;
			}
			if (global::Attr_3.Type_50.\u2000 == 131072)
			{
				this.Attr_3.Text = "E0000";
				this.Attr_2.Text = "B000";
				return;
			}
			if (global::Attr_3.Type_50.\u2000 < 262144)
			{
				return;
			}
			if (this.\u00A0(global::Attr_3.Type_50.\u200A, global::Attr_3.Type_50.\u2000 - global::Attr_3.Type_50.Attr_2.Length, global::Attr_3.Type_50.\u00A0, global::Attr_3.Type_50.Attr_2.Length))
			{
				for (int i = 0; i < global::Attr_3.Type_50.\u2000 - 5; i++)
				{
					if (this.\u00A0(global::Attr_3.Type_50.\u200A, i, global::Attr_3.Type_50.\u2002, global::Attr_3.Type_50.Type_6.Length) || this.\u00A0(global::Attr_3.Type_50.\u200A, i, global::Attr_3.Type_50.\u2003, global::Attr_3.Type_50.Type_7.Length))
					{
						this.\u1680(this.\u1680, "auto");
						this.\u1680(this.\u00A0, "auto");
						return;
					}
				}
				return;
			}
			if (global::Attr_3.Type_50.\u2000 == 262144 && (this.\u00A0(global::Attr_3.Type_50.\u200A, global::Attr_3.Type_50.\u2000 - global::Attr_3.Type_50.Form_A.Length, global::Attr_3.Type_50.\u2006, global::Attr_3.Type_50.Form_A.Length) || this.\u00A0(global::Attr_3.Type_50.\u200A, global::Attr_3.Type_50.\u2000 - global::Attr_3.Type_50.Form_9.Length, global::Attr_3.Type_50.\u2005, global::Attr_3.Type_50.Form_9.Length) || this.\u00A0(global::Attr_3.Type_50.\u200A, global::Attr_3.Type_50.\u2000 - global::Attr_3.Type_50.Type_14.Length, global::Attr_3.Type_50.\u2007, global::Attr_3.Type_50.Type_14.Length)))
			{
				this.Attr_3.Text = "0";
				this.Attr_2.Text = "3FFF8";
				return;
			}
			if (global::Attr_3.Type_50.\u2000 == 524288)
			{
				if (this.\u00A0(global::Attr_3.Type_50.\u200A, global::Attr_3.Type_50.\u2000 - global::Attr_3.Type_50.Type_14.Length, global::Attr_3.Type_50.\u2007, global::Attr_3.Type_50.Type_14.Length))
				{
					this.Attr_3.Text = "0";
					this.Attr_2.Text = "7FFF8";
					return;
				}
			}
			else if (global::Attr_3.Type_50.\u2000 == 1048576)
			{
				if (this.\u00A0(global::Attr_3.Type_50.\u200A, global::Attr_3.Type_50.\u2000 - global::Attr_3.Type_50.Type_16.Length, global::Attr_3.Type_50.\u2009, global::Attr_3.Type_50.Type_16.Length))
				{
					this.Attr_3.Text = "0";
					this.Attr_2.Text = "FFFF8";
					return;
				}
				this.Attr_3.Text = "0";
				this.Attr_2.Text = "7FFF8";
			}
		}

		// Token: 0x06000262 RID: 610 RVA: 0x00017B54 File Offset: 0x00015D54
		private \u205A.\u2006 \u2010()
		{
			if (this.\u00A0(global::Attr_3.Type_50.\u200A, global::Attr_3.Type_50.\u2000 - global::Attr_3.Type_50.Attr_2.Length, global::Attr_3.Type_50.\u00A0, global::Attr_3.Type_50.Attr_2.Length))
			{
				bool flag = false;
				int i = 0;
				while (i < global::Attr_3.Type_50.\u2000 - global::Attr_3.Type_50.Form_4.Length)
				{
					if (this.\u00A0(global::Attr_3.Type_50.\u200A, i, global::Attr_3.Type_50.\u2000, global::Attr_3.Type_50.Form_4.Length))
					{
						flag = true;
					}
					if (this.\u00A0(global::Attr_3.Type_50.\u200A, i, global::Attr_3.Type_50.\u2002, global::Attr_3.Type_50.Type_6.Length))
					{
						this.\u1680(this.\u1680, "10000");
						return global::Attr_3.Type_50.Form_A.\u2001;
					}
					if (this.\u00A0(global::Attr_3.Type_50.\u200A, i, global::Attr_3.Type_50.\u2003, global::Attr_3.Type_50.Type_7.Length))
					{
						if (this.\u00A0(global::Attr_3.Type_50.\u200A, 40960, global::Attr_3.Type_50.\u1680, global::Attr_3.Type_50.Attr_3.Length))
						{
							this.\u1680(this.\u1680, "A000");
							return global::Attr_3.Type_50.Form_A.\u2003;
						}
						this.\u1680(this.\u1680, "10000");
						return global::Attr_3.Type_50.Form_A.\u2002;
					}
					else
					{
						i++;
					}
				}
				if (flag)
				{
					this.\u1680(this.\u1680, "10000");
					return global::Attr_3.Type_50.Form_A.\u2001;
				}
			}
			if ((global::Attr_3.Type_50.\u2000 == 57344 || global::Attr_3.Type_50.\u2000 == 32768) && this.\u00A0(global::Attr_3.Type_50.\u200A, global::Attr_3.Type_50.\u2000 - global::Attr_3.Type_50.Form_8.Length, global::Attr_3.Type_50.\u2004, global::Attr_3.Type_50.Form_8.Length))
			{
				return global::Attr_3.Type_50.Form_A.\u1680;
			}
			if (global::Attr_3.Type_50.\u2000 != 262144 || !this.\u00A0(global::Attr_3.Type_50.\u200A, global::Attr_3.Type_50.\u2000 - global::Attr_3.Type_50.Form_9.Length, global::Attr_3.Type_50.\u2005, global::Attr_3.Type_50.Form_9.Length))
			{
				return global::Attr_3.Type_50.Form_A.\u00A0;
			}
			return global::Attr_3.Type_50.Form_A.\u2000;
		}

		// Token: 0x06000263 RID: 611 RVA: 0x00017CE0 File Offset: 0x00015EE0
		private void \u00A0(object A_1, EventArgs A_2)
		{
			\u205A.\u2027 u = new \u205A.\u2027();
			u.\u00A0 = this;
			base.Region = Region.FromHrgn(global::Attr_3.Type_50.\u00A0(0, 0, base.Width, base.Height, 15, 15));
			this.Attr_2.MouseDown += u.\u00A0;
			this.Attr_2.MouseMove += u.\u1680;
			this.Attr_2.MouseUp += u.\u2000;
			this.Attr_2.Renderer = new \u205A.\u2011();
			this.Attr_2.BackColor = Color.Black;
			this.Attr_2.Padding = new Padding(3, 3, 0, 3);
			string text = this.Attr_5.Text;
			this.Attr_5.Text = "";
			u.\u00A0 = new Label();
			u.Attr_2.Text = text;
			u.Attr_2.Font = this.Attr_5.Font;
			u.Attr_2.ForeColor = this.Attr_5.ForeColor;
			u.Attr_2.BackColor = Color.Transparent;
			u.Attr_2.AutoSize = true;
			u.Attr_2.Cursor = Cursors.Hand;
			u.Attr_2.Click += u.\u00A0;
			this.Attr_5.Controls.Add(u.\u00A0);
			u.Attr_2.Location = new Point(this.Attr_5.Width, (this.Attr_5.Height - 15) / 2);
			u.\u00A0 = 100;
			u.\u00A0 = true;
			System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
			timer.Interval = 15;
			timer.Tick += u.\u1680;
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
				this.Type_6.Image = image;
			}
			catch (Exception)
			{
			}
			this.Form_4.Enabled = false;
			this.Type_7.Enabled = false;
			global::Attr_3.Type_50.\u2002 = true;
			global::Attr_3.Type_50.\u2003 = false;
			this.Type_29.Text = "";
			this.Attr_2.Checked = global::Attr_3.Type_50.\u2006;
			this.Attr_2.Checked = true;
			new Thread(new ThreadStart(this.\u200A))
			{
				IsBackground = true
			}.Start();
		}

		// Token: 0x06000264 RID: 612 RVA: 0x00017F78 File Offset: 0x00016178
		private void \u00A0(object A_1, FormClosingEventArgs A_2)
		{
			global::Attr_3.Type_50.\u2001 = true;
		}

		// Token: 0x06000265 RID: 613 RVA: 0x0000208B File Offset: 0x0000028B
		private void \u1680(object A_1, EventArgs A_2)
		{
		}

		// Token: 0x06000266 RID: 614 RVA: 0x00017F80 File Offset: 0x00016180
		private void \u2000(object A_1, EventArgs A_2)
		{
			this.Type_7.Enabled = false;
			this.Form_4.Enabled = false;
			global::Attr_3.Type_58.\u00A0("เริ่มทำงาน - MZATUNER", "กรุณาเปิดสวิตช์กุญแจรถ (ON) แล้วรอระบบเชื่อมต่อ", global::Attr_3.Type_57.\u1680);
			global::Attr_3.Type_50.\u2007 = true;
			this.Refresh();
		}

		// Token: 0x06000267 RID: 615 RVA: 0x00017FB8 File Offset: 0x000161B8
		private void \u1680(Control A_1, string A_2)
		{
			if (A_1.InvokeRequired)
			{
				\u205A.\u00A0 method = new \u205A.\u00A0(this.\u1680);
				A_1.Invoke(method, new object[]
				{
					A_1,
					A_2
				});
				return;
			}
			A_1.Text = A_2;
		}

		// Token: 0x06000268 RID: 616 RVA: 0x00017FF8 File Offset: 0x000161F8
		private void \u00A0(ComboBox A_1, bool A_2)
		{
			if (A_1.InvokeRequired)
			{
				\u205A.\u2000 method = new \u205A.\u2000(this.\u00A0);
				A_1.Invoke(method, new object[]
				{
					A_1,
					A_2
				});
				return;
			}
			A_1.Enabled = A_2;
		}

		// Token: 0x06000269 RID: 617 RVA: 0x00018040 File Offset: 0x00016240
		private void \u00A0(ProgressBar A_1, int A_2)
		{
			if (A_1.InvokeRequired)
			{
				\u205A.\u2001 method = new \u205A.\u2001(this.\u00A0);
				A_1.Invoke(method, new object[]
				{
					A_1,
					A_2
				});
				return;
			}
			A_1.Value = A_2;
		}

		// Token: 0x0600026A RID: 618 RVA: 0x00018088 File Offset: 0x00016288
		private void \u2001(object A_1, EventArgs A_2)
		{
			if (this.Form_4.TextLength > 0)
			{
				this.Type_7.Enabled = true;
				return;
			}
			if (global::Attr_3.Type_50.\u2004)
			{
				this.Attr_3.Text = "";
				this.Attr_2.Text = "";
			}
			this.Type_7.Enabled = false;
		}

		// Token: 0x0600026B RID: 619 RVA: 0x000180E3 File Offset: 0x000162E3
		private void \u2002(object A_1, EventArgs A_2)
		{
			if (this.Attr_2.CheckState == CheckState.Checked)
			{
				global::Attr_3.Type_50.\u2006 = true;
				return;
			}
			global::Attr_3.Type_50.\u2006 = false;
		}

		// Token: 0x0600026C RID: 620 RVA: 0x00018100 File Offset: 0x00016300
		private void \u2003(object A_1, EventArgs A_2)
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
				global::Attr_3.Type_50.\u200A = File.ReadAllBytes(fileName);
				global::Attr_3.Type_50.\u2000 = global::Attr_3.Type_50.Struct_17.Length;
				double num = (double)global::Attr_3.Type_50.\u2000 / 1024.0;
				this.Form_4.Text = string.Format("{0} ({1:F2} KB)", fileName2, num);
				if (Path.GetExtension(fileName).ToLower() != ".bin")
				{
					MessageBox.Show("ไฟล์ที่เลือกไม่ใช่ .bin", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
					this.Form_4.Text = "(กรุณาเลือกไฟล์ BIN) ..";
					global::Attr_3.Type_50.\u200A = null;
					global::Attr_3.Type_50.\u2000 = 0;
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
				if (global::Attr_3.Type_50.\u2004)
				{
					this.\u200B();
					return;
				}
			}
			else
			{
				this.Form_4.Text = "(กรุณาเลือกไฟล์ BIN) ..";
				global::Attr_3.Type_50.\u200A = null;
				global::Attr_3.Type_50.\u2000 = 0;
			}
		}

		// Token: 0x0600026D RID: 621 RVA: 0x00018240 File Offset: 0x00016440
		private void \u2004(object A_1, EventArgs A_2)
		{
			if (this.Attr_3.TextLength > 0 && !this.Attr_3.Text.Contains("auto"))
			{
				int u;
				if (int.TryParse(this.Attr_3.Text, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out u))
				{
					global::Attr_3.Type_50.\u2001 = u;
					return;
				}
			}
			else
			{
				global::Attr_3.Type_50.\u2001 = 0;
			}
		}

		// Token: 0x0600026E RID: 622 RVA: 0x000182A0 File Offset: 0x000164A0
		private void \u2005(object A_1, EventArgs A_2)
		{
			if (this.Attr_2.TextLength > 0 && !this.Attr_2.Text.Contains("auto"))
			{
				int u;
				if (int.TryParse(this.Attr_2.Text, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out u))
				{
					global::Attr_3.Type_50.\u2002 = u;
					return;
				}
			}
			else
			{
				global::Attr_3.Type_50.\u2002 = 0;
			}
		}

		// Token: 0x0600026F RID: 623 RVA: 0x000021C8 File Offset: 0x000003C8
		private void \u2006(object A_1, EventArgs A_2)
		{
			base.Close();
		}

		// Token: 0x06000270 RID: 624 RVA: 0x000182FD File Offset: 0x000164FD
		private void \u2007(object A_1, EventArgs A_2)
		{
			new global::Attr_2.\u2000().Show();
		}

		// Token: 0x06000271 RID: 625 RVA: 0x000021C8 File Offset: 0x000003C8
		private void \u2008(object A_1, EventArgs A_2)
		{
			base.Close();
		}

		// Token: 0x06000272 RID: 626 RVA: 0x00018309 File Offset: 0x00016509
		private void \u2009(object A_1, EventArgs A_2)
		{
			Process.Start("https://www.facebook.com/profile.php?id=100086932872601");
		}

		// Token: 0x06000273 RID: 627 RVA: 0x00018316 File Offset: 0x00016516
		private void \u200A(object A_1, EventArgs A_2)
		{
			Process.Start("https://www.facebook.com/juniel.pontongan/");
		}

		// Token: 0x06000275 RID: 629 RVA: 0x00018344 File Offset: 0x00016544
		private void \u2011()
		{
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(Type_50));
			this.\u2008 = new Label();
			this.\u2007 = new Label();
			this.\u2009 = new Label();
			this.\u2004 = new Label();
			this.\u2005 = new Label();
			this.\u2006 = new Label();
			this.\u00A0 = new TextBox();
			this.\u1680 = new TextBox();
			this.\u2000 = new TextBox();
			this.\u1680 = new PictureBox();
			this.\u200A = new Label();
			this.\u2003 = new Button();
			this.\u2000 = new PictureBox();
			this.\u200B = new Label();
			this.\u2001 = new PictureBox();
			this.\u2003 = new PictureBox();
			this.\u2013 = new Label();
			this.\u2010 = new Label();
			this.\u2011 = new Label();
			this.\u2012 = new Label();
			this.\u2049 = new ToolStripMenuItem();
			this.\u204B = new ToolStripMenuItem();
			this.\u204C = new ToolStripMenuItem();
			this.\u204D = new ToolStripMenuItem();
			this.\u204E = new ToolStripMenuItem();
			this.\u204A = new ToolStripMenuItem();
			this.\u2053 = new ToolStripMenuItem();
			this.\u2057 = new ToolStripMenuItem();
			this.\u2058 = new ToolStripMenuItem();
			this.\u205A = new ToolStripMenuItem();
			this.\u2059 = new ToolStripMenuItem();
			this.\u2052 = new ToolStripMenuItem();
			this.\u2032 = new ToolStripMenuItem();
			this.\u2035 = new ToolStripMenuItem();
			this.\u2033 = new ToolStripMenuItem();
			this.\u2036 = new ToolStripMenuItem();
			this.\u203E = new ToolStripMenuItem();
			this.\u2048 = new ToolStripMenuItem();
			this.\u2047 = new ToolStripMenuItem();
			this.\u204F = new ToolStripMenuItem();
			this.\u2050 = new ToolStripMenuItem();
			this.\u2051 = new ToolStripMenuItem();
			this.\u2055 = new ToolStripMenuItem();
			this.\u2056 = new ToolStripMenuItem();
			this.\u2054 = new ToolStripMenuItem();
			this.\u2005 = new Button();
			this.\u2006 = new Button();
			this.\u2002 = new PictureBox();
			this.\u1680 = new GroupBox();
			this.\u2007 = new Button();
			this.\u1680 = new Button();
			this.\u2008 = new Button();
			this.\u2009 = new Button();
			this.\u2000 = new GroupBox();
			this.\u2000 = new Label();
			this.\u1680 = new Label();
			this.\u00A0 = new GroupBox();
			this.\u1680 = new Type_5B();
			this.\u00A0 = new Type_5B();
			this.\u00A0 = new Type_47();
			this.\u2000 = new Type_47();
			this.\u1680 = new Type_47();
			this.\u200A = new Button();
			this.\u00A0 = new Type_59();
			this.\u2000 = new Button();
			this.\u00A0 = new Button();
			this.\u00A0 = new ToolStripMenuItem();
			this.\u1680 = new ToolStripMenuItem();
			this.\u2000 = new ToolStripMenuItem();
			this.\u2001 = new ToolStripMenuItem();
			this.\u2002 = new ToolStripMenuItem();
			this.\u2003 = new ToolStripMenuItem();
			this.\u2004 = new ToolStripMenuItem();
			this.\u2005 = new ToolStripMenuItem();
			this.\u2006 = new ToolStripMenuItem();
			this.\u2007 = new ToolStripMenuItem();
			this.\u2008 = new ToolStripMenuItem();
			this.\u2009 = new ToolStripMenuItem();
			this.\u200A = new ToolStripMenuItem();
			this.\u200B = new ToolStripMenuItem();
			this.\u2010 = new ToolStripMenuItem();
			this.\u2011 = new ToolStripMenuItem();
			this.\u2012 = new ToolStripMenuItem();
			this.\u2013 = new ToolStripMenuItem();
			this.\u2014 = new ToolStripMenuItem();
			this.\u2015 = new ToolStripMenuItem();
			this.\u2022 = new ToolStripMenuItem();
			this.\u2024 = new ToolStripMenuItem();
			this.\u2025 = new ToolStripMenuItem();
			this.\u2027 = new ToolStripMenuItem();
			this.\u2028 = new ToolStripMenuItem();
			this.\u2029 = new ToolStripMenuItem();
			this.\u202A = new ToolStripMenuItem();
			this.\u202B = new ToolStripMenuItem();
			this.\u202C = new ToolStripMenuItem();
			this.\u202D = new ToolStripMenuItem();
			this.\u202E = new ToolStripMenuItem();
			this.\u202F = new ToolStripMenuItem();
			this.\u00A0 = new MenuStrip();
			this.\u2001 = new Label();
			this.\u00A0 = new Panel();
			this.\u00A0 = new PictureBox();
			this.\u2003 = new Label();
			this.\u2001 = new Button();
			this.\u2002 = new Button();
			this.\u2002 = new Label();
			((ISupportInitialize)this.\u1680).BeginInit();
			((ISupportInitialize)this.\u2000).BeginInit();
			((ISupportInitialize)this.\u2001).BeginInit();
			((ISupportInitialize)this.\u2003).BeginInit();
			((ISupportInitialize)this.\u2002).BeginInit();
			this.Attr_3.SuspendLayout();
			this.Form_4.SuspendLayout();
			this.Attr_2.SuspendLayout();
			this.Attr_2.SuspendLayout();
			this.Attr_2.SuspendLayout();
			((ISupportInitialize)this.\u00A0).BeginInit();
			base.SuspendLayout();
			this.Type_15.AutoSize = true;
			this.Type_15.BackColor = Color.Transparent;
			this.Type_15.Font = new Font("Microsoft New Tai Lue", 9.75f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.Type_15.ForeColor = Color.Red;
			this.Type_15.Location = new Point(134, 46);
			this.Type_15.Name = "TxtEcmId";
			this.Type_15.Size = new Size(13, 17);
			this.Type_15.TabIndex = 6;
			this.Type_15.Text = "-";
			this.Type_15.Click += this.\u2033;
			this.Type_14.BackColor = Color.Transparent;
			this.Type_14.Font = new Font("Microsoft New Tai Lue", 9.75f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.Type_14.ForeColor = Color.Red;
			this.Type_14.Location = new Point(105, 153);
			this.Type_14.Name = "TxtFlashCount";
			this.Type_14.Size = new Size(105, 20);
			this.Type_14.TabIndex = 8;
			this.Type_14.Text = "-";
			this.Type_14.TextAlign = ContentAlignment.MiddleLeft;
			this.Type_14.Click += this.\u2024;
			this.Type_16.AutoSize = true;
			this.Type_16.BackColor = Color.Transparent;
			this.Type_16.FlatStyle = FlatStyle.Flat;
			this.Type_16.Font = new Font("Times New Roman", 12f, FontStyle.Bold);
			this.Type_16.ForeColor = Color.Red;
			this.Type_16.Location = new Point(149, 20);
			this.Type_16.Name = "TxtConnStat";
			this.Type_16.Size = new Size(14, 19);
			this.Type_16.TabIndex = 32;
			this.Type_16.Text = "-";
			this.Type_16.Click += this.\u2027;
			this.Form_8.AutoSize = true;
			this.Form_8.Location = new Point(132, 418);
			this.Form_8.Name = "LblConnection";
			this.Form_8.Size = new Size(64, 13);
			this.Form_8.TabIndex = 0;
			this.Form_8.Text = "Connection:";
			this.Form_9.BackColor = Color.Black;
			this.Form_9.Font = new Font("Times New Roman", 12f, FontStyle.Bold);
			this.Form_9.ForeColor = Color.White;
			this.Form_9.Location = new Point(119, -3);
			this.Form_9.Name = "LblChecksumOffset";
			this.Form_9.Size = new Size(100, 15);
			this.Form_9.TabIndex = 9;
			this.Form_9.Text = "เช็คซั่มออฟเซ็ค";
			this.Form_9.TextAlign = ContentAlignment.MiddleCenter;
			this.Form_9.Click += this.\u2011;
			this.Form_A.BackColor = Color.Black;
			this.Form_A.Font = new Font("Times New Roman", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.Form_A.ForeColor = Color.White;
			this.Form_A.Location = new Point(15, -3);
			this.Form_A.Name = "LblStartOffset";
			this.Form_A.Size = new Size(88, 19);
			this.Form_A.TabIndex = 8;
			this.Form_A.Text = "สตาร์ทออฟเซ็ต";
			this.Attr_2.Font = new Font("Microsoft Sans Serif", 11.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.Attr_2.Location = new Point(117, 19);
			this.Attr_2.Name = "TbChecksumOffset";
			this.Attr_2.ReadOnly = true;
			this.Attr_2.Size = new Size(107, 24);
			this.Attr_2.TabIndex = 5;
			this.Attr_2.Text = "-";
			this.Attr_2.TextAlign = HorizontalAlignment.Center;
			this.Attr_2.TextChanged += this.\u2005;
			this.Attr_3.Font = new Font("Microsoft Sans Serif", 11.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.Attr_3.Location = new Point(5, 19);
			this.Attr_3.Name = "TbStartOffset";
			this.Attr_3.ReadOnly = true;
			this.Attr_3.Size = new Size(107, 24);
			this.Attr_3.TabIndex = 4;
			this.Attr_3.Text = "-";
			this.Attr_3.TextAlign = HorizontalAlignment.Center;
			this.Attr_3.TextChanged += this.\u2004;
			this.Form_4.BackColor = Color.DimGray;
			this.Form_4.BorderStyle = BorderStyle.None;
			this.Form_4.Font = new Font("Microsoft Tai Le", 9f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.Form_4.ForeColor = Color.White;
			this.Form_4.Location = new Point(6, 52);
			this.Form_4.Multiline = true;
			this.Form_4.Name = "TbFileName";
			this.Form_4.ReadOnly = true;
			this.Form_4.Size = new Size(453, 19);
			this.Form_4.TabIndex = 0;
			this.Form_4.Text = "กรุณาเลือกไฟล์ BIN ..";
			this.Form_4.TextChanged += this.\u2001;
			this.Attr_3.Location = new Point(800, 800);
			this.Attr_3.Margin = new Padding(2);
			this.Attr_3.Name = "pictureBox2";
			this.Attr_3.Size = new Size(98, 90);
			this.Attr_3.SizeMode = PictureBoxSizeMode.StretchImage;
			this.Attr_3.TabIndex = 30;
			this.Attr_3.TabStop = false;
			this.Attr_3.Click += this.\u2006;
			this.Struct_17.AccessibleRole = AccessibleRole.Text;
			this.Struct_17.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this.Struct_17.BackColor = Color.Black;
			this.Struct_17.FlatStyle = FlatStyle.System;
			this.Struct_17.Font = new Font("Segoe UI Black", 18f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.Struct_17.ForeColor = Color.White;
			this.Struct_17.ImageAlign = ContentAlignment.MiddleLeft;
			this.Struct_17.Location = new Point(5, 14);
			this.Struct_17.Margin = new Padding(2, 0, 2, 2);
			this.Struct_17.Name = "TxtPartCode";
			this.Struct_17.Size = new Size(591, 34);
			this.Struct_17.TabIndex = 8;
			this.Struct_17.Text = "-";
			this.Struct_17.TextAlign = ContentAlignment.BottomCenter;
			this.Struct_17.Click += this.\u2022;
			this.Type_7.BackColor = Color.Transparent;
			this.Type_7.BackgroundImageLayout = ImageLayout.Stretch;
			this.Type_7.FlatStyle = FlatStyle.Flat;
			this.Type_7.Font = new Font("Microsoft YaHei", 15.75f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.Type_7.ForeColor = Color.Fuchsia;
			this.Type_7.Location = new Point(19, 673);
			this.Type_7.Name = "BtnWrite";
			this.Type_7.Size = new Size(447, 44);
			this.Type_7.TabIndex = 2;
			this.Type_7.Text = "อัดไฟล์ลงกล่อง ECU";
			this.Type_7.UseVisualStyleBackColor = false;
			this.Type_7.Click += this.\u2000;
			this.Form_4.Location = new Point(800, 800);
			this.Form_4.Margin = new Padding(2);
			this.Form_4.Name = "pictureBox5";
			this.Form_4.Size = new Size(100, 61);
			this.Form_4.SizeMode = PictureBoxSizeMode.StretchImage;
			this.Form_4.TabIndex = 39;
			this.Form_4.TabStop = false;
			this.Struct_18.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.Struct_18.ForeColor = Color.Red;
			this.Struct_18.Location = new Point(800, 800);
			this.Struct_18.Margin = new Padding(2, 0, 2, 0);
			this.Struct_18.Name = "label1";
			this.Struct_18.Size = new Size(178, 18);
			this.Struct_18.TabIndex = 40;
			this.Struct_18.Text = "HONDA PART CODE";
			this.Attr_5.BackColor = Color.Transparent;
			this.Attr_5.Location = new Point(800, 800);
			this.Attr_5.Margin = new Padding(2);
			this.Attr_5.Name = "pictureBox6";
			this.Attr_5.Size = new Size(101, 41);
			this.Attr_5.SizeMode = PictureBoxSizeMode.StretchImage;
			this.Attr_5.TabIndex = 41;
			this.Attr_5.TabStop = false;
			this.Attr_5.Click += this.\u2009;
			this.Type_7.Location = new Point(800, 800);
			this.Type_7.Margin = new Padding(2);
			this.Type_7.Name = "pictureBox7";
			this.Type_7.Size = new Size(47, 41);
			this.Type_7.SizeMode = PictureBoxSizeMode.StretchImage;
			this.Type_7.TabIndex = 42;
			this.Type_7.TabStop = false;
			this.Type_7.Click += this.\u200A;
			this.Type_29.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this.Type_29.BackColor = Color.Transparent;
			this.Type_29.Font = new Font("Segoe UI", 9.75f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.Type_29.ForeColor = Color.Red;
			this.Type_29.Location = new Point(629, 282);
			this.Type_29.Name = "TxtPb";
			this.Type_29.Size = new Size(602, 25);
			this.Type_29.TabIndex = 8;
			this.Type_29.Text = "-";
			this.Type_29.TextAlign = ContentAlignment.MiddleCenter;
			this.Type_29.Click += this.\u200B;
			this.Type_26.AutoSize = true;
			this.Type_26.Font = new Font("Times New Roman", 12f, FontStyle.Bold);
			this.Type_26.ForeColor = Color.White;
			this.Type_26.Location = new Point(35, 20);
			this.Type_26.Name = "label2";
			this.Type_26.Size = new Size(115, 19);
			this.Type_26.TabIndex = 45;
			this.Type_26.Text = "สถานะการชื่อกุญแจ :";
			this.Type_26.Click += this.\u2025;
			this.Type_27.AutoSize = true;
			this.Type_27.BackColor = Color.Transparent;
			this.Type_27.Font = new Font("Times New Roman", 12f, FontStyle.Bold);
			this.Type_27.ForeColor = Color.White;
			this.Type_27.Location = new Point(6, 153);
			this.Type_27.Name = "label3";
			this.Type_27.Size = new Size(104, 19);
			this.Type_27.TabIndex = 46;
			this.Type_27.Text = "🛡จำนวนการอัด : ";
			this.Type_27.Click += this.\u2028;
			this.Type_28.AutoSize = true;
			this.Type_28.Font = new Font("Times New Roman", 12f, FontStyle.Bold);
			this.Type_28.ForeColor = Color.White;
			this.Type_28.Location = new Point(18, 43);
			this.Type_28.Name = "label4";
			this.Type_28.Size = new Size(118, 19);
			this.Type_28.TabIndex = 47;
			this.Type_28.Text = "📈 : ไอดีกล่องอีซียู : ";
			this.Type_28.Click += this.\u2029;
			this.Type_3F.BackColor = Color.Black;
			this.Type_3F.DropDownItems.AddRange(new ToolStripItem[]
			{
				this.\u204B,
				this.\u204C,
				this.\u204D,
				this.\u204E,
				this.\u204A,
				this.\u2053,
				this.\u2057,
				this.\u2058,
				this.\u205A,
				this.\u2059,
				this.\u2052
			});
			this.Type_3F.ForeColor = Color.White;
			this.Type_3F.Name = "ไฟลToolStripMenuItem";
			this.Type_3F.Size = new Size(58, 24);
			this.Type_3F.Text = "ไฟล์";
			this.Type_3F.Click += this.\u2048;
			this.Type_41.Name = "เปลยนชอโปรแกรมToolStripMenuItem";
			this.Type_41.Size = new Size(171, 22);
			this.Type_42.Name = "เปลยนรปToolStripMenuItem1";
			this.Type_42.Size = new Size(171, 22);
			this.Type_43.Name = "ปลดรหสXDFADXToolStripMenuItem";
			this.Type_43.Size = new Size(171, 22);
			this.Type_44.Name = "แปลงไฟลECUACGToolStripMenuItem";
			this.Type_44.Size = new Size(171, 22);
			this.Type_40.BackColor = SystemColors.ActiveCaptionText;
			this.Type_40.ForeColor = Color.White;
			this.Type_40.Name = "เลอกไฟลแฟชรToolStripMenuItem";
			this.Type_40.Size = new Size(171, 22);
			this.Type_40.Text = "เลือกไฟล์ Bin";
			this.Type_40.Click += this.\u202F;
			this.Type_49.Name = "ตดตงไดรเวอรToolStripMenuItem1";
			this.Type_49.Size = new Size(171, 22);
			this.Type_4D.Name = "รสตารทโปรแกรมToolStripMenuItem";
			this.Type_4D.Size = new Size(171, 22);
			this.Type_4E.BackColor = SystemColors.ActiveCaptionText;
			this.Type_4E.ForeColor = Color.White;
			this.Type_4E.Name = "อพเดทรหสกลองลาสดToolStripMenuItem";
			this.Type_4E.Size = new Size(171, 22);
			this.Type_4E.Text = "อัพเดทรหัสกล่องล่าสุด";
			this.Type_4E.Click += this.\u203E;
			this.Type_50.Name = "ไฟลเดมโรงงานToolStripMenuItem";
			this.Type_50.Size = new Size(171, 22);
			this.Type_4F.Name = "ไฟลโมสำหรบมอใหมToolStripMenuItem";
			this.Type_4F.Size = new Size(171, 22);
			this.Type_48.Name = "toolStripMenuItem1";
			this.Type_48.Size = new Size(171, 22);
			this.Type_38.Name = "ลบออโตToolStripMenuItem";
			this.Type_38.Size = new Size(32, 19);
			this.Type_39.Name = "ลบออโตToolStripMenuItem1";
			this.Type_39.Size = new Size(32, 19);
			this.Type_3A.BackColor = SystemColors.ActiveCaptionText;
			this.Type_3A.ForeColor = Color.White;
			this.Type_3A.Name = "รเซตกลองToolStripMenuItem";
			this.Type_3A.Size = new Size(199, 22);
			this.Type_3A.Text = "รีเช็ตกล่อง";
			this.Type_3A.Click += this.\u202B;
			this.Type_3B.BackColor = SystemColors.ActiveCaptionText;
			this.Type_3B.ForeColor = Color.White;
			this.Type_3B.Name = "ลบโคตToolStripMenuItem";
			this.Type_3B.Size = new Size(199, 22);
			this.Type_3B.Text = "ลบโค็ต";
			this.Type_3B.Click += this.\u202C;
			this.Type_3C.BackColor = Color.Black;
			this.Type_3C.ForeColor = Color.White;
			this.Type_3C.Name = "ลบแฟลชเคาทShindengenToolStripMenuItem";
			this.Type_3C.Size = new Size(199, 22);
			this.Type_3C.Text = "ลบแฟลชเคาท์Shindengen";
			this.Type_3C.Click += this.\u202D;
			this.Type_3E.Name = "ดดดาตาToolStripMenuItem";
			this.Type_3E.Size = new Size(32, 19);
			this.Type_3D.Name = "จนโปรToolStripMenuItem";
			this.Type_3D.Size = new Size(32, 19);
			this.Type_45.Name = "เปดจนโปรToolStripMenuItem";
			this.Type_45.Size = new Size(32, 19);
			this.Type_46.Name = "ลงทะเบยนToolStripMenuItem";
			this.Type_46.Size = new Size(32, 19);
			this.Type_47.Name = "อพเดทจนโปรToolStripMenuItem";
			this.Type_47.Size = new Size(32, 19);
			this.Type_4B.Name = "ลอคฝงชนToolStripMenuItem";
			this.Type_4B.Size = new Size(32, 19);
			this.Type_4C.Name = "เพมรหสกลองToolStripMenuItem";
			this.Type_4C.Size = new Size(32, 19);
			this.Type_4A.Name = "อพเดทโปรแกรมToolStripMenuItem";
			this.Type_4A.Size = new Size(32, 19);
			this.Form_9.BackColor = Color.White;
			this.Form_9.FlatAppearance.BorderColor = Color.White;
			this.Form_9.Font = new Font("Microsoft Sans Serif", 7.8f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.Form_9.ForeColor = Color.Black;
			this.Form_9.Location = new Point(615, 673);
			this.Form_9.Margin = new Padding(2);
			this.Form_9.Name = "button2";
			this.Form_9.Size = new Size(137, 36);
			this.Form_9.TabIndex = 35;
			this.Form_9.Text = "ACG/ECU TO BIN";
			this.Form_9.UseVisualStyleBackColor = false;
			this.Form_9.Click += this.\u2007;
			this.Form_A.BackColor = Color.Transparent;
			this.Form_A.BackgroundImageLayout = ImageLayout.Stretch;
			this.Form_A.FlatStyle = FlatStyle.Flat;
			this.Form_A.Font = new Font("Microsoft YaHei", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.Form_A.ForeColor = Color.Fuchsia;
			this.Form_A.Location = new Point(771, 669);
			this.Form_A.Name = "button1";
			this.Form_A.Size = new Size(103, 57);
			this.Form_A.TabIndex = 10;
			this.Form_A.Text = "เลือกไฟล์";
			this.Form_A.UseVisualStyleBackColor = false;
			this.Form_A.Click += this.\u2003;
			this.Type_6.Cursor = Cursors.No;
			this.Type_6.Location = new Point(7, 60);
			this.Type_6.Name = "pictureBox1";
			this.Type_6.Size = new Size(602, 261);
			this.Type_6.SizeMode = PictureBoxSizeMode.StretchImage;
			this.Type_6.TabIndex = 53;
			this.Type_6.TabStop = false;
			this.Type_6.Click += this.\u2035;
			this.Attr_3.Controls.Add(this.\u200A);
			this.Attr_3.Location = new Point(7, 321);
			this.Attr_3.Name = "groupBox3";
			this.Attr_3.Size = new Size(602, 60);
			this.Attr_3.TabIndex = 59;
			this.Attr_3.TabStop = false;
			this.Type_14.BackColor = Color.Transparent;
			this.Type_14.BackgroundImageLayout = ImageLayout.Stretch;
			this.Type_14.FlatStyle = FlatStyle.Flat;
			this.Type_14.Font = new Font("Microsoft YaHei", 9f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.Type_14.ForeColor = Color.Fuchsia;
			this.Type_14.Location = new Point(1110, 62);
			this.Type_14.Name = "button11";
			this.Type_14.Size = new Size(198, 26);
			this.Type_14.TabIndex = 63;
			this.Type_14.Text = "➕ เพิ่มรหัสกล่องโปรแกรม";
			this.Type_14.UseVisualStyleBackColor = false;
			this.Type_14.Click += this.\u2053;
			this.Attr_3.BackColor = Color.Transparent;
			this.Attr_3.BackgroundImageLayout = ImageLayout.Stretch;
			this.Attr_3.FlatStyle = FlatStyle.Flat;
			this.Attr_3.Font = new Font("Microsoft YaHei", 9f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.Attr_3.ForeColor = Color.Fuchsia;
			this.Attr_3.Location = new Point(685, 592);
			this.Attr_3.Name = "button28";
			this.Attr_3.Size = new Size(199, 26);
			this.Attr_3.TabIndex = 69;
			this.Attr_3.Text = "⚙️ ดูดไฟล์รถเกียร์ 48 - 64";
			this.Attr_3.UseVisualStyleBackColor = false;
			this.Attr_3.Click += this.\u206C;
			this.Type_15.BackColor = Color.Transparent;
			this.Type_15.BackgroundImageLayout = ImageLayout.Stretch;
			this.Type_15.FlatStyle = FlatStyle.Flat;
			this.Type_15.Font = new Font("Microsoft YaHei", 9f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.Type_15.ForeColor = Color.Fuchsia;
			this.Type_15.Location = new Point(686, 560);
			this.Type_15.Name = "button15";
			this.Type_15.Size = new Size(198, 26);
			this.Type_15.TabIndex = 67;
			this.Type_15.Text = "⚙️ ลบจำนวนการอัดรถเกียร์";
			this.Type_15.UseVisualStyleBackColor = false;
			this.Type_15.Click += this.\u2057;
			this.Type_16.BackColor = Color.Transparent;
			this.Type_16.BackgroundImageLayout = ImageLayout.Stretch;
			this.Type_16.FlatStyle = FlatStyle.Flat;
			this.Type_16.Font = new Font("Microsoft YaHei", 9f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.Type_16.ForeColor = Color.Fuchsia;
			this.Type_16.Location = new Point(868, 673);
			this.Type_16.Name = "button21";
			this.Type_16.Size = new Size(180, 26);
			this.Type_16.TabIndex = 68;
			this.Type_16.Text = "น้ำมัน TPS 0";
			this.Type_16.UseVisualStyleBackColor = false;
			this.Form_4.Controls.Add(this.\u2007);
			this.Form_4.Controls.Add(this.\u2000);
			this.Form_4.Controls.Add(this.\u1680);
			this.Form_4.Controls.Add(this.\u00A0);
			this.Form_4.Controls.Add(this.\u00A0);
			this.Form_4.Controls.Add(this.\u2011);
			this.Form_4.Controls.Add(this.\u2000);
			this.Form_4.Controls.Add(this.\u1680);
			this.Form_4.Controls.Add(this.\u200A);
			this.Form_4.Controls.Add(this.\u2000);
			this.Form_4.Controls.Add(this.\u00A0);
			this.Form_4.Controls.Add(this.\u2010);
			this.Form_4.Controls.Add(this.\u2009);
			this.Form_4.Location = new Point(7, 380);
			this.Form_4.Name = "groupBox9";
			this.Form_4.Size = new Size(602, 216);
			this.Form_4.TabIndex = 62;
			this.Form_4.TabStop = false;
			this.Form_4.Enter += this.\u204A;
			this.Form_4.BackColor = Color.Transparent;
			this.Form_4.Font = new Font("Microsoft New Tai Lue", 9.75f, FontStyle.Bold);
			this.Form_4.ForeColor = Color.Yellow;
			this.Form_4.Location = new Point(351, 150);
			this.Form_4.Name = "TxtBatteryVolt";
			this.Form_4.Size = new Size(52, 23);
			this.Form_4.TabIndex = 22;
			this.Form_4.Text = "0.0 V";
			this.Form_4.TextAlign = ContentAlignment.MiddleRight;
			this.Form_4.Click += this.\u1680\u202B;
			this.Attr_3.AutoSize = true;
			this.Attr_3.Font = new Font("Times New Roman", 12f, FontStyle.Bold);
			this.Attr_3.ForeColor = Color.White;
			this.Attr_3.Location = new Point(273, 153);
			this.Attr_3.Name = "label_batTitle";
			this.Attr_3.Size = new Size(82, 19);
			this.Attr_3.TabIndex = 70;
			this.Attr_3.Text = "🔋แบตเตอรรี่:";
			this.Attr_2.BackColor = Color.Transparent;
			this.Attr_2.Controls.Add(this.\u2012);
			this.Attr_2.Controls.Add(this.\u1680);
			this.Attr_2.Controls.Add(this.\u2008);
			this.Attr_2.Controls.Add(this.\u00A0);
			this.Attr_2.Controls.Add(this.\u2006);
			this.Attr_2.Controls.Add(this.\u1680);
			this.Attr_2.Controls.Add(this.\u2005);
			this.Attr_2.Controls.Add(this.\u00A0);
			this.Attr_2.ForeColor = Color.Transparent;
			this.Attr_2.Location = new Point(6, 77);
			this.Attr_2.Name = "groupBox1";
			this.Attr_2.Size = new Size(395, 67);
			this.Attr_2.TabIndex = 54;
			this.Attr_2.TabStop = false;
			this.Attr_2.Enter += this.\u1680\u2027;
			this.Attr_3.Cursor = Cursors.Hand;
			this.Attr_3.Font = new Font("Times New Roman", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.Attr_3.ForeColor = Color.White;
			this.Attr_3.Location = new Point(230, 36);
			this.Attr_3.Name = "checkBox1";
			this.Attr_3.\u1680(Color.FromArgb(100, 100, 100));
			this.Attr_3.\u00A0(Color.FromArgb(0, 192, 0));
			this.Attr_3.Size = new Size(160, 25);
			this.Attr_3.TabIndex = 54;
			this.Attr_3.Text = "ออฟเซ็ตแบบแมนนวล";
			this.Attr_3.UseVisualStyleBackColor = true;
			this.Attr_3.CheckedChanged += this.\u2060;
			this.Attr_2.Cursor = Cursors.Hand;
			this.Attr_2.Font = new Font("Times New Roman", 8.25f, FontStyle.Bold);
			this.Attr_2.ForeColor = Color.White;
			this.Attr_2.Location = new Point(230, 13);
			this.Attr_2.Name = "CbFastWrite";
			this.Attr_2.\u1680(Color.FromArgb(100, 100, 100));
			this.Attr_2.\u00A0(Color.FromArgb(0, 192, 0));
			this.Attr_2.Size = new Size(160, 25);
			this.Attr_2.TabIndex = 28;
			this.Attr_2.Text = "เขียนไฟล์เร็ว";
			this.Attr_2.UseVisualStyleBackColor = true;
			this.Attr_2.CheckedChanged += this.\u2002;
			this.Attr_2.BackColor = Color.Black;
			this.Attr_2.BackgroundImageLayout = ImageLayout.Stretch;
			this.Attr_2.\u00A0(8);
			this.Attr_2.Cursor = Cursors.Hand;
			this.Attr_2.FlatStyle = FlatStyle.Flat;
			this.Attr_2.Font = new Font("Microsoft YaHei", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.Attr_2.ForeColor = Color.Red;
			this.Attr_2.\u00A0(false);
			this.Attr_2.Location = new Point(326, 12);
			this.Attr_2.\u00A0(global::Attr_3.Type_46.\u1680);
			this.Attr_2.Name = "button12";
			this.Attr_2.Size = new Size(133, 37);
			this.Attr_2.TabIndex = 64;
			this.Attr_2.Text = "รีสตาร์ทโปรแกรม";
			this.Attr_2.UseVisualStyleBackColor = false;
			this.Attr_2.Click += this.\u2056;
			this.Form_4.BackColor = Color.Black;
			this.Form_4.BackgroundImageLayout = ImageLayout.Stretch;
			this.Form_4.\u00A0(8);
			this.Form_4.Cursor = Cursors.Hand;
			this.Form_4.FlatStyle = FlatStyle.Flat;
			this.Form_4.Font = new Font("Microsoft YaHei", 20.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.Form_4.ForeColor = Color.Red;
			this.Form_4.\u00A0(false);
			this.Form_4.Location = new Point(407, 77);
			this.Form_4.\u00A0(global::Attr_3.Type_46.\u2001);
			this.Form_4.Name = "button20";
			this.Form_4.Size = new Size(189, 97);
			this.Form_4.TabIndex = 68;
			this.Form_4.Text = "อัดไฟล์";
			this.Form_4.UseVisualStyleBackColor = false;
			this.Form_4.Click += this.\u205C;
			this.Attr_3.BackColor = Color.Black;
			this.Attr_3.BackgroundImageLayout = ImageLayout.Stretch;
			this.Attr_3.\u00A0(8);
			this.Attr_3.Cursor = Cursors.Hand;
			this.Attr_3.FlatStyle = FlatStyle.Flat;
			this.Attr_3.Font = new Font("Microsoft YaHei", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.Attr_3.ForeColor = Color.Red;
			this.Attr_3.\u00A0(false);
			this.Attr_3.Location = new Point(465, 12);
			this.Attr_3.\u00A0(global::Attr_3.Type_46.\u2000);
			this.Attr_3.Name = "button19";
			this.Attr_3.Size = new Size(131, 60);
			this.Attr_3.TabIndex = 68;
			this.Attr_3.Text = "เลือกไฟล์";
			this.Attr_3.UseVisualStyleBackColor = false;
			this.Attr_3.Click += this.\u205B;
			this.Struct_17.BackColor = Color.Transparent;
			this.Struct_17.BackgroundImageLayout = ImageLayout.Stretch;
			this.Struct_17.FlatStyle = FlatStyle.Flat;
			this.Struct_17.Font = new Font("Microsoft YaHei", 9.75f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.Struct_17.ForeColor = Color.Red;
			this.Struct_17.Location = new Point(800, 800);
			this.Struct_17.Name = "button26";
			this.Struct_17.Size = new Size(103, 31);
			this.Struct_17.TabIndex = 69;
			this.Struct_17.Text = "🔄 Resset";
			this.Struct_17.UseVisualStyleBackColor = false;
			this.Struct_17.Visible = false;
			this.Struct_17.Click += this.\u2062;
			this.Attr_2.AccessibleRole = AccessibleRole.ProgressBar;
			this.Attr_2.BackColor = Color.Black;
			this.Attr_2.Cursor = Cursors.Default;
			this.Attr_2.ForeColor = Color.Red;
			this.Attr_2.Location = new Point(6, 180);
			this.Attr_2.Maximum = 10000;
			this.Attr_2.Name = "PbProgress";
			this.Attr_2.Size = new Size(590, 27);
			this.Attr_2.Step = 1;
			this.Attr_2.Style = ProgressBarStyle.Continuous;
			this.Attr_2.TabIndex = 10;
			this.Attr_2.Click += this.\u2063;
			this.Form_4.BackColor = Color.Red;
			this.Form_4.Cursor = Cursors.Default;
			this.Form_4.FlatAppearance.BorderSize = 0;
			this.Form_4.FlatStyle = FlatStyle.Flat;
			this.Form_4.Location = new Point(593, 40);
			this.Form_4.Name = "sx1";
			this.Form_4.Size = new Size(14, 12);
			this.Form_4.TabIndex = 20;
			this.Form_4.UseVisualStyleBackColor = false;
			this.Form_4.Click += this.\u1680\u2029;
			this.Form_4.Paint += this.\u1680;
			this.Attr_2.BackColor = Color.Transparent;
			this.Attr_2.BackgroundImageLayout = ImageLayout.Stretch;
			this.Attr_2.FlatStyle = FlatStyle.Flat;
			this.Attr_2.Font = new Font("Microsoft YaHei", 9f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.Attr_2.ForeColor = Color.Fuchsia;
			this.Attr_2.Location = new Point(14, 752);
			this.Attr_2.Name = "button22";
			this.Attr_2.Size = new Size(180, 26);
			this.Attr_2.TabIndex = 69;
			this.Attr_2.Text = "ดูดไฟล์ 48-64 KB";
			this.Attr_2.UseVisualStyleBackColor = false;
			this.Attr_2.DropDownItems.AddRange(new ToolStripItem[]
			{
				this.\u1680,
				this.\u2000,
				this.\u2001,
				this.\u2002,
				this.\u2003,
				this.\u2004,
				this.\u2005,
				this.\u2006,
				this.\u2007,
				this.\u2008,
				this.\u2009,
				this.\u200A,
				this.\u200B,
				this.\u2010
			});
			this.Attr_2.Font = new Font("Segoe UI", 9f);
			this.Attr_2.ForeColor = Color.White;
			this.Attr_2.Name = "ไฟลToolStripMenuItem1";
			this.Attr_2.Size = new Size(53, 20);
			this.Attr_2.Text = "🗂️ ไฟล์";
			this.Attr_3.Name = "เกยวกบโปรแกรมToolStripMenuItem";
			this.Attr_3.Size = new Size(214, 22);
			this.Attr_3.Text = "ℹ️ เกี่ยวกับโปรแกรม";
			this.Attr_3.Click += this.\u1680\u2008;
			this.Form_4.Name = "ตดตอสอบถามปญหาToolStripMenuItem";
			this.Form_4.Size = new Size(214, 22);
			this.Form_4.Text = "📞 ติดต่อสอบถาม/ปัญหา";
			this.Form_4.Click += this.\u1680\u2007;
			this.Attr_5.Name = "อพเดทตรวจสอบเวอรชนToolStripMenuItem";
			this.Attr_5.Size = new Size(214, 22);
			this.Attr_5.Text = "🔄 อัพเดท/ตรวจสอบเวอร์ชั่น";
			this.Attr_5.Click += this.\u1680\u2009;
			this.Type_6.Name = "เปลยนสโปรแกรมToolStripMenuItem";
			this.Type_6.Size = new Size(214, 22);
			this.Type_6.Text = "🎨 เปลี่ยนสีโปรแกรม";
			this.Type_6.Click += this.\u1680\u202D;
			this.Type_7.Name = "ปรบเปลยนแตงโลโกToolStripMenuItem";
			this.Type_7.Size = new Size(214, 22);
			this.Type_7.Text = "🖼️ ปรับเปลี่ยนแต่งโลโก้";
			this.Type_7.Click += this.\u1680\u200B;
			this.Form_8.Name = "ตงคาชอโปรแกรมToolStripMenuItem";
			this.Form_8.Size = new Size(214, 22);
			this.Form_8.Text = "⚙️ ตั้งค่าชื่อโปรแกรม";
			this.Form_8.Click += this.\u1680\u2010;
			this.Form_9.Name = "ตดตงไดรเวอรFTDIToolStripMenuItem";
			this.Form_9.Size = new Size(214, 22);
			this.Form_9.Text = "🔌 ติดตั้งไดรเวอร์ (FTDI)";
			this.Form_9.Click += this.\u1680\u200A;
			this.Form_A.Name = "เปดโปรแกรมTunerProRTToolStripMenuItem";
			this.Form_A.Size = new Size(214, 22);
			this.Form_A.Text = "🚀 เปิดโปรแกรม TunerPro RT";
			this.Form_A.Click += this.\u1680\u2002;
			this.Type_14.Name = "ลงทะเบยนTunerProToolStripMenuItem";
			this.Type_14.Size = new Size(214, 22);
			this.Type_14.Text = "🔑 ลงทะเบียน TunerPro";
			this.Type_14.Click += this.\u1680\u2003;
			this.Type_15.Name = "ตดตงTunerProRTToolStripMenuItem";
			this.Type_15.Size = new Size(214, 22);
			this.Type_15.Text = "📥 ติดตั้ง TunerPro RT";
			this.Type_15.Click += this.\u1680\u2004;
			this.Type_16.Name = "ตดตงTunerProRTTHToolStripMenuItem";
			this.Type_16.Size = new Size(214, 22);
			this.Type_16.Text = "📥 ติดตั้ง  TunerPro RT TH";
			this.Type_16.Click += this.\u1680\u2005;
			this.Struct_17.Name = "แปลงไฟลECUACGToolStripMenuItem1";
			this.Struct_17.Size = new Size(214, 22);
			this.Struct_17.Text = "🧬 แปลงไฟล์ ECU - ACG";
			this.Struct_17.Click += this.\u1680\u2015;
			this.Struct_18.Name = "ปลดรหสXDFADXToolStripMenuItem1";
			this.Struct_18.Size = new Size(214, 22);
			this.Struct_18.Text = "🔓 ปลดรหัส XDF - ADX";
			this.Struct_18.Click += this.\u1680\u2022;
			this.Type_26.Name = "เพมรหสกลองโปรแกรมToolStripMenuItem";
			this.Type_26.Size = new Size(214, 22);
			this.Type_26.Text = "➕ เพิ่มรหัสกล่องโปรแกรม";
			this.Type_26.Click += this.\u1680\u2024;
			this.Type_27.DropDownItems.AddRange(new ToolStripItem[]
			{
				this.\u2012,
				this.\u2013
			});
			this.Type_27.ForeColor = Color.White;
			this.Type_27.Name = "จดการรถToolStripMenuItem";
			this.Type_27.Size = new Size(76, 20);
			this.Type_27.Text = "🛵 จัดการรถ";
			this.Type_28.Name = "ลบโคดToolStripMenuItem";
			this.Type_28.Size = new Size(160, 22);
			this.Type_28.Text = "❌ ลบโค้ด";
			this.Type_28.Click += this.\u1680\u2011;
			this.Type_29.Name = "รเซตกลองECUToolStripMenuItem";
			this.Type_29.Size = new Size(160, 22);
			this.Type_29.Text = "♻️ รีเซ็ตกล่อง ECU";
			this.Type_29.Click += this.\u1680\u2047;
			this.Type_2A.DropDownItems.AddRange(new ToolStripItem[]
			{
				this.\u2015,
				this.\u2022
			});
			this.Type_2A.ForeColor = Color.White;
			this.Type_2A.Name = "ดดาตารถToolStripMenuItem";
			this.Type_2A.Size = new Size(77, 20);
			this.Type_2A.Text = "📊 ดูดาต้ารถ";
			this.Type_2B.Name = "อานขอมลจากรถV1ToolStripMenuItem";
			this.Type_2B.Size = new Size(182, 22);
			this.Type_2B.Text = "📊 อ่านข้อมูลจากรถ V.1";
			this.Type_2B.Click += this.\u1680\u2012;
			this.Type_2C.Name = "อานขอมลจากรถV2ToolStripMenuItem";
			this.Type_2C.Size = new Size(182, 22);
			this.Type_2C.Text = "📈 อ่านข้อมูลจากรถ V.2";
			this.Type_2C.Click += this.\u1680\u2013;
			this.Type_2D.ForeColor = Color.White;
			this.Type_2D.Name = "ลอคดดลอคโหมดToolStripMenuItem";
			this.Type_2D.Size = new Size(121, 20);
			this.Type_2D.Text = "🔓 ล็อคดูด - ล็อคโหมด";
			this.Type_2D.Click += this.\u1680\u2014;
			this.Type_2E.DropDownItems.AddRange(new ToolStripItem[]
			{
				this.\u2027,
				this.\u2028,
				this.\u2029,
				this.\u202A,
				this.\u202B,
				this.\u202C,
				this.\u202D,
				this.\u202E,
				this.\u202F
			});
			this.Type_2E.ForeColor = Color.White;
			this.Type_2E.Name = "ฟToolStripMenuItem";
			this.Type_2E.Size = new Size(86, 20);
			this.Type_2E.Text = "🚀 ฟังชั่นพิเศษ";
			this.Type_2F.Name = "ลบจำนวนการอดรถออโตToolStripMenuItem";
			this.Type_2F.Size = new Size(298, 22);
			this.Type_2F.Text = "🧹 ลบจำนวนการอัดรถออโต้";
			this.Type_2F.Click += this.\u1680\u2000;
			this.Type_30.Name = "ลบจำนวนการอดรถเกยรToolStripMenuItem";
			this.Type_30.Size = new Size(298, 22);
			this.Type_30.Text = "⚙️ ลบจำนวนการอัดรถเกียร์";
			this.Type_30.Click += this.\u1680\u2001;
			this.Type_31.Name = "ดดไฟลรถเกยร4864ToolStripMenuItem";
			this.Type_31.Size = new Size(298, 22);
			this.Type_31.Text = "⚙️ ดูดไฟล์รถเกียร์ 48 - 64";
			this.Type_31.Click += this.\u1680\u2006;
			this.Type_32.Name = "คอนเนกเพอเขาสดดไฟลสถานนะอยขวาบนToolStripMenuItem";
			this.Type_32.Size = new Size(298, 22);
			this.Type_32.Text = "⚙️ คอนเน็กเพื่อเข้าสู่ดูดไฟล์ !! สถานนะอยุ่ขวาบน  !!";
			this.Type_32.Click += this.\u1680\u2025;
			this.Type_33.Name = "คำToolStripMenuItem";
			this.Type_33.Size = new Size(298, 22);
			this.Type_33.Text = "⚙️ คำนวนหัวฉีดรถ";
			this.Type_33.Click += this.\u1680\u202C;
			this.Type_34.Name = "ตดตอไฟล32KBเปน64KBToolStripMenuItem";
			this.Type_34.Size = new Size(298, 22);
			this.Type_34.Text = "⚙️ ตัดต่อไฟล์ 32KB เป้น 64KB";
			this.Type_34.Click += this.\u1680\u2035;
			this.Type_35.Name = "จนยงออโตToolStripMenuItem";
			this.Type_35.Size = new Size(298, 22);
			this.Type_35.Text = "⚙️ ช่วยจูนยิงอัตโนมัติ ";
			this.Type_35.Click += this.\u1680\u2033;
			this.Type_36.Name = "ปลดการจายนำมนTPS0ToolStripMenuItem";
			this.Type_36.Size = new Size(298, 22);
			this.Type_36.Text = "⚙️ ปลดการจ่ายน้ำมัน TPS 0%";
			this.Type_36.Click += this.\u1680\u2036;
			this.Type_37.Name = "ชวยจนหอบToolStripMenuItem";
			this.Type_37.Size = new Size(298, 22);
			this.Type_37.Text = "⚙️ ช่วยจูนหอบ";
			this.Type_37.Click += this.\u1680\u203E;
			this.Attr_2.BackColor = Color.Transparent;
			this.Attr_2.Items.AddRange(new ToolStripItem[]
			{
				this.\u00A0,
				this.\u2011,
				this.\u2014,
				this.\u2024,
				this.\u2025
			});
			this.Attr_2.Location = new Point(0, 35);
			this.Attr_2.Name = "menuStrip1";
			this.Attr_2.Size = new Size(616, 24);
			this.Attr_2.TabIndex = 71;
			this.Attr_2.Text = "📊 ดูดาต้ารถ";
			this.Attr_2.ItemClicked += this.\u00A0;
			this.Attr_5.BackColor = Color.FromArgb(25, 25, 25);
			this.Attr_5.Cursor = Cursors.Hand;
			this.Attr_5.Font = new Font("Segoe UI", 9f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.Attr_5.ForeColor = Color.LightGray;
			this.Attr_5.Location = new Point(7, 601);
			this.Attr_5.Name = "label5";
			this.Attr_5.Size = new Size(602, 26);
			this.Attr_5.TabIndex = 100;
			this.Attr_5.Text = componentResourceManager.GetString("label5.Text");
			this.Attr_5.TextAlign = ContentAlignment.MiddleCenter;
			this.Attr_5.Click += this.\u1680\u202A;
			this.Attr_5.Paint += this.\u2000;
			this.Attr_2.BackColor = Color.FromArgb(215, 15, 15);
			this.Attr_2.Controls.Add(this.\u00A0);
			this.Attr_2.Controls.Add(this.\u2003);
			this.Attr_2.Controls.Add(this.\u2001);
			this.Attr_2.Controls.Add(this.\u2002);
			this.Attr_2.Dock = DockStyle.Top;
			this.Attr_2.Location = new Point(0, 0);
			this.Attr_2.Name = "pnlTitleBar";
			this.Attr_2.Size = new Size(616, 35);
			this.Attr_2.TabIndex = 100;
			this.Attr_2.Paint += this.\u2001;
			this.Attr_2.BackColor = Color.Transparent;
			this.Attr_2.Location = new Point(10, 4);
			this.Attr_2.Name = "pbAppLogo";
			this.Attr_2.Size = new Size(26, 26);
			this.Attr_2.TabIndex = 101;
			this.Attr_2.TabStop = false;
			this.Attr_2.Click += this.\u1680\u202E;
			this.Type_7.AutoSize = true;
			this.Type_7.BackColor = Color.Transparent;
			this.Type_7.Font = new Font("Segoe UI", 10f, FontStyle.Bold);
			this.Type_7.ForeColor = Color.White;
			this.Type_7.Location = new Point(42, 8);
			this.Type_7.Name = "lblAppTitle";
			this.Type_7.Size = new Size(101, 19);
			this.Type_7.TabIndex = 102;
			this.Type_7.Text = "เส’เอ็ม สามย่าน";
			this.Type_7.Click += this.\u1680\u202F;
			this.Attr_5.Dock = DockStyle.Right;
			this.Attr_5.FlatAppearance.BorderSize = 0;
			this.Attr_5.FlatStyle = FlatStyle.Flat;
			this.Attr_5.Font = new Font("Microsoft YaHei", 12f, FontStyle.Bold);
			this.Attr_5.ForeColor = Color.White;
			this.Attr_5.Location = new Point(516, 0);
			this.Attr_5.Name = "btnMinCustom";
			this.Attr_5.Size = new Size(50, 35);
			this.Attr_5.TabIndex = 103;
			this.Attr_5.Text = "—";
			this.Type_6.Dock = DockStyle.Right;
			this.Type_6.FlatAppearance.BorderSize = 0;
			this.Type_6.FlatStyle = FlatStyle.Flat;
			this.Type_6.Font = new Font("Microsoft YaHei", 12f, FontStyle.Bold);
			this.Type_6.ForeColor = Color.White;
			this.Type_6.Location = new Point(566, 0);
			this.Type_6.Name = "btnCloseCustom";
			this.Type_6.Size = new Size(50, 35);
			this.Type_6.TabIndex = 104;
			this.Type_6.Text = "✕";
			this.Type_6.AutoSize = true;
			this.Type_6.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 222);
			this.Type_6.ForeColor = Color.White;
			this.Type_6.Location = new Point(538, 40);
			this.Type_6.Name = "label6";
			this.Type_6.Size = new Size(53, 13);
			this.Type_6.TabIndex = 71;
			this.Type_6.Text = "STATUS:";
			this.Type_6.Click += this.\u1680\u2032;
			base.AutoScaleDimensions = new SizeF(96f, 96f);
			base.AutoScaleMode = AutoScaleMode.Dpi;
			this.BackColor = Color.Black;
			this.BackgroundImageLayout = ImageLayout.None;
			base.ClientSize = new Size(616, 635);
			base.Controls.Add(this.\u2002);
			base.Controls.Add(this.\u2002);
			base.Controls.Add(this.\u2001);
			base.Controls.Add(this.\u1680);
			base.Controls.Add(this.\u2013);
			base.Controls.Add(this.\u2000);
			base.Controls.Add(this.\u2007);
			base.Controls.Add(this.\u1680);
			base.Controls.Add(this.\u00A0);
			base.Controls.Add(this.\u2009);
			base.Controls.Add(this.\u2003);
			base.Controls.Add(this.\u2006);
			base.Controls.Add(this.\u2000);
			base.Controls.Add(this.\u2003);
			base.Controls.Add(this.\u2001);
			base.Controls.Add(this.\u200B);
			base.Controls.Add(this.\u2000);
			base.Controls.Add(this.\u1680);
			base.Controls.Add(this.\u2004);
			base.Controls.Add(this.\u2005);
			base.Controls.Add(this.\u00A0);
			base.Controls.Add(this.\u00A0);
			this.ForeColor = Color.Black;
			base.FormBorderStyle = FormBorderStyle.None;
			base.Icon = (Icon)componentResourceManager.GetObject("$this.Icon");
			base.MainMenuStrip = this.\u00A0;
			base.MaximizeBox = false;
			this.MaximumSize = new Size(616, 635);
			this.MinimumSize = new Size(616, 635);
			base.Name = "FormMain";
			base.StartPosition = FormStartPosition.CenterScreen;
			base.FormClosing += this.\u00A0;
			base.Load += this.\u00A0;
			((ISupportInitialize)this.\u1680).EndInit();
			((ISupportInitialize)this.\u2000).EndInit();
			((ISupportInitialize)this.\u2001).EndInit();
			((ISupportInitialize)this.\u2003).EndInit();
			((ISupportInitialize)this.\u2002).EndInit();
			this.Attr_3.ResumeLayout(false);
			this.Form_4.ResumeLayout(false);
			this.Form_4.PerformLayout();
			this.Attr_2.ResumeLayout(false);
			this.Attr_2.PerformLayout();
			this.Attr_2.ResumeLayout(false);
			this.Attr_2.PerformLayout();
			this.Attr_2.ResumeLayout(false);
			this.Attr_2.PerformLayout();
			((ISupportInitialize)this.\u00A0).EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x06000276 RID: 630 RVA: 0x0001C15C File Offset: 0x0001A35C
		static Type_50()
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
			global::Attr_3.Type_50.\u00A0 = list.ToArray();
			global::Attr_3.Type_50.\u1680 = list2.ToArray();
			global::Attr_3.Type_50.\u2000 = list3.ToArray();
			global::Attr_3.Type_50.\u2001 = list4.ToArray();
			global::Attr_3.Type_50.\u2002 = list5.ToArray();
			global::Attr_3.Type_50.\u2003 = list6.ToArray();
			global::Attr_3.Type_50.\u00A0 = new byte[]
			{
				90,
				90,
				90,
				90
			};
			global::Attr_3.Type_50.\u1680 = new byte[]
			{
				165,
				165,
				165,
				165,
				0
			};
			global::Attr_3.Type_50.\u2000 = new byte[]
			{
				17,
				18,
				19,
				20,
				21,
				21,
				23
			};
			global::Attr_3.Type_50.\u2001 = new byte[]
			{
				145,
				145,
				13,
				0,
				158,
				141
			};
			global::Attr_3.Type_50.\u2002 = new byte[]
			{
				83,
				86,
				56,
				53,
				48
			};
			global::Attr_3.Type_50.\u2003 = new byte[]
			{
				83,
				72,
				56,
				53,
				48
			};
			global::Attr_3.Type_50.\u2004 = new byte[]
			{
				byte.MaxValue,
				byte.MaxValue,
				170,
				170
			};
			global::Attr_3.Type_50.\u2005 = new byte[]
			{
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				170,
				170,
				170,
				170
			};
			global::Attr_3.Type_50.\u2006 = new byte[]
			{
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				0,
				0,
				0,
				0
			};
			global::Attr_3.Type_50.\u2007 = new byte[]
			{
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue
			};
			global::Attr_3.Type_50.\u2008 = new byte[7];
			global::Attr_3.Type_50.\u2009 = new byte[]
			{
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				170,
				170,
				170,
				170
			};
			global::Attr_3.Type_50.\u00A0 = IntPtr.Zero;
			global::Attr_3.Type_50.\u2001 = 0;
			global::Attr_3.Type_50.\u2002 = 0;
			global::Attr_3.Type_50.\u2001 = false;
			global::Attr_3.Type_50.\u2002 = false;
			global::Attr_3.Type_50.\u2003 = true;
			global::Attr_3.Type_50.\u2004 = false;
			global::Attr_3.Type_50.\u2005 = false;
			global::Attr_3.Type_50.\u2006 = false;
			global::Attr_3.Type_50.\u2007 = false;
		}

		// Token: 0x06000277 RID: 631 RVA: 0x0001C4B0 File Offset: 0x0001A6B0
		private void \u200B(object A_1, EventArgs A_2)
		{
			this.Struct_18.Text = "สวัสดีครับ";
			this.Struct_18.TextAlign = ContentAlignment.MiddleCenter;
		}

		// Token: 0x06000278 RID: 632 RVA: 0x0000208B File Offset: 0x0000028B
		private void \u2010(object A_1, EventArgs A_2)
		{
		}

		// Token: 0x06000279 RID: 633 RVA: 0x0000208B File Offset: 0x0000028B
		private void \u2011(object A_1, EventArgs A_2)
		{
		}

		// Token: 0x0600027A RID: 634 RVA: 0x0000208B File Offset: 0x0000028B
		private void \u2012(object A_1, EventArgs A_2)
		{
		}

		// Token: 0x0600027B RID: 635 RVA: 0x0000208B File Offset: 0x0000028B
		private void \u2013(object A_1, EventArgs A_2)
		{
		}

		// Token: 0x0600027C RID: 636 RVA: 0x0000208B File Offset: 0x0000028B
		private void \u2014(object A_1, EventArgs A_2)
		{
		}

		// Token: 0x0600027D RID: 637 RVA: 0x0000208B File Offset: 0x0000028B
		private void \u2015(object A_1, EventArgs A_2)
		{
		}

		// Token: 0x0600027E RID: 638 RVA: 0x0001C4D0 File Offset: 0x0001A6D0
		private void \u2022(object A_1, EventArgs A_2)
		{
			string[] array = new string[]
			{
				"Segoe UI Black",
				"Impact",
				"Bahnschrift SemiBold Condensed",
				"Consolas",
				"Copperplate Gothic Bold"
			};
			this.\u2004 = (this.\u2004 + 1) % array.Length;
			string familyName = array[this.\u2004];
			try
			{
				this.Struct_17.Font = new Font(familyName, 18f, FontStyle.Bold);
				Task.Run(new Action(global::Attr_3.Type_50.Type_29.Attr_2.\u2004));
			}
			catch
			{
				this.Struct_17.Font = new Font("Arial", 16f, FontStyle.Bold);
			}
		}

		// Token: 0x0600027F RID: 639 RVA: 0x0000208B File Offset: 0x0000028B
		private void \u2024(object A_1, EventArgs A_2)
		{
		}

		// Token: 0x06000280 RID: 640 RVA: 0x0001C594 File Offset: 0x0001A794
		private void \u2025(object A_1, EventArgs A_2)
		{
			if (this.\u00A0 != null)
			{
				this.Attr_2.BringToFront();
				bool flag = SerialPort.GetPortNames().Length != 0;
				this.Attr_2.BackColor = (flag ? Color.Lime : Color.Red);
			}
		}

		// Token: 0x06000281 RID: 641 RVA: 0x0000208B File Offset: 0x0000028B
		private void \u2027(object A_1, EventArgs A_2)
		{
		}

		// Token: 0x06000282 RID: 642 RVA: 0x0000208B File Offset: 0x0000028B
		private void \u2028(object A_1, EventArgs A_2)
		{
		}

		// Token: 0x06000283 RID: 643 RVA: 0x0000208B File Offset: 0x0000028B
		private void \u2029(object A_1, EventArgs A_2)
		{
		}

		// Token: 0x06000284 RID: 644 RVA: 0x0000208B File Offset: 0x0000028B
		private void \u202A(object A_1, EventArgs A_2)
		{
		}

		// Token: 0x06000285 RID: 645 RVA: 0x0001C5D8 File Offset: 0x0001A7D8
		private void \u202B(object A_1, EventArgs A_2)
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
			this.\u00A0(array, array.Length, ref array3, ref num, 0);
			Thread.Sleep(1000);
			this.\u00A0(array2, array2.Length, ref array3, ref num, 0);
			MessageBox.Show("RESET ECM DONE", "Reset ECM", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			MessageBox.Show("Please turn OFF/ON the ignition key\rfor 10 seconds", "Reset ECM", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}

		// Token: 0x06000286 RID: 646 RVA: 0x0001C67C File Offset: 0x0001A87C
		private void \u202C(object A_1, EventArgs A_2)
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
			new byte[5];
			RuntimeHelpers.InitializeArray(new byte[5], fieldof(global::Form_4.Type_36.\u2010).FieldHandle);
			new byte[5];
			RuntimeHelpers.InitializeArray(new byte[5], fieldof(global::Form_4.Type_36.\u2013).FieldHandle);
			new byte[5];
			RuntimeHelpers.InitializeArray(new byte[5], fieldof(global::Form_4.Type_36.\u2012).FieldHandle);
			byte[] array4 = new byte[256];
			uint num = 0U;
			Thread.Sleep(1000);
			this.\u00A0(array, array.Length, ref array4, ref num, 0);
			Thread.Sleep(1000);
			this.\u00A0(array2, array2.Length, ref array4, ref num, 0);
			Thread.Sleep(1000);
			this.\u00A0(array3, array3.Length, ref array4, ref num, 0);
			MessageBox.Show("ลบโค๊ดสำเร็จ", "ลบโค๊ด", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}

		// Token: 0x06000287 RID: 647 RVA: 0x0001C784 File Offset: 0x0001A984
		private void \u202D(object A_1, EventArgs A_2)
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
			this.\u00A0(array, array.Length, ref array4, ref num, 0);
			Thread.Sleep(100);
			this.\u00A0(array2, array2.Length, ref array4, ref num, 0);
			Thread.Sleep(100);
			this.\u00A0(array3, array3.Length, ref array4, ref num, 0);
			Thread.Sleep(100);
			MessageBox.Show("ลบแฟลชเคาท์!!สำเร็จ!!", "รีเซ็ทแฟลชเคาท์", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}

		// Token: 0x06000288 RID: 648 RVA: 0x0001C858 File Offset: 0x0001AA58
		private void \u202E(object A_1, EventArgs A_2)
		{
			string empty = string.Empty;
			OpenFileDialog openFileDialog = new OpenFileDialog();
			openFileDialog.Filter = "Bin file(*.bin) | *.bin|ACG Files(*.acg)|*.acg|ECU Files(*.ECU)|*.ECU";
			if (openFileDialog.ShowDialog() == DialogResult.OK)
			{
				this.Form_4.Text = openFileDialog.FileName;
				global::Attr_3.Type_50.\u200A = File.ReadAllBytes(openFileDialog.FileName);
				global::Attr_3.Type_50.\u2000 = global::Attr_3.Type_50.Struct_17.Length;
				if (global::Attr_3.Type_50.\u2004)
				{
					this.\u200B();
					return;
				}
			}
			else
			{
				this.Form_4.Text = "(กรุณาเลือกไฟล์ BIN) ..";
				global::Attr_3.Type_50.\u200A = null;
				global::Attr_3.Type_50.\u2000 = 0;
			}
		}

		// Token: 0x06000289 RID: 649 RVA: 0x0001C8E0 File Offset: 0x0001AAE0
		private void \u202F(object A_1, EventArgs A_2)
		{
			string empty = string.Empty;
			OpenFileDialog openFileDialog = new OpenFileDialog();
			openFileDialog.Filter = "Bin file(*.bin) | *.bin|ACG Files(*.acg)|*.acg|ECU Files(*.ECU)|*.ECU";
			if (openFileDialog.ShowDialog() == DialogResult.OK)
			{
				this.Form_4.Text = openFileDialog.FileName;
				global::Attr_3.Type_50.\u200A = File.ReadAllBytes(openFileDialog.FileName);
				global::Attr_3.Type_50.\u2000 = global::Attr_3.Type_50.Struct_17.Length;
				if (global::Attr_3.Type_50.\u2004)
				{
					this.\u200B();
					return;
				}
			}
			else
			{
				this.Form_4.Text = "(กรุณาเลือกไฟล์ BIN) ..";
				global::Attr_3.Type_50.\u200A = null;
				global::Attr_3.Type_50.\u2000 = 0;
			}
		}

		// Token: 0x0600028A RID: 650 RVA: 0x0000208B File Offset: 0x0000028B
		private void \u2032(object A_1, EventArgs A_2)
		{
		}

		// Token: 0x0600028B RID: 651 RVA: 0x0001C968 File Offset: 0x0001AB68
		private void \u00A0(object A_1, PaintEventArgs A_2)
		{
			Control control = A_1 as Control;
			if (control != null)
			{
				using (Pen pen = new Pen(Color.White, 1f))
				{
					A_2.Graphics.DrawRectangle(pen, 0, 0, control.Width - 1, control.Height - 1);
				}
			}
		}

		// Token: 0x0600028C RID: 652 RVA: 0x0001C9CC File Offset: 0x0001ABCC
		private void \u2035(object A_1, EventArgs A_2)
		{
			this.\u1680\u200B(A_1, A_2);
		}

		// Token: 0x0600028D RID: 653 RVA: 0x0000208B File Offset: 0x0000028B
		private void \u2033(object A_1, EventArgs A_2)
		{
		}

		// Token: 0x0600028E RID: 654 RVA: 0x0001C9D8 File Offset: 0x0001ABD8
		private string \u00A0(string A_1, string A_2)
		{
			Form form = new Form();
			form.Width = 300;
			form.Height = 150;
			form.Text = A_1;
			Label value = new Label
			{
				Left = 20,
				Top = 20,
				Text = A_2,
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

		// Token: 0x0600028F RID: 655 RVA: 0x0001CAC2 File Offset: 0x0001ACC2
		private void \u2012()
		{
			this.\u2009 = true;
			global::Attr_3.Type_50.\u2001 = true;
			Thread.Sleep(500);
		}

		// Token: 0x06000290 RID: 656 RVA: 0x0001CADB File Offset: 0x0001ACDB
		private void \u2013()
		{
			this.\u2009 = false;
			global::Attr_3.Type_50.\u2001 = false;
			new Thread(new ThreadStart(this.\u200A)).Start();
		}

		// Token: 0x06000291 RID: 657 RVA: 0x0001CB00 File Offset: 0x0001AD00
		private void \u00A0(IntPtr A_1)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000292 RID: 658 RVA: 0x0000208B File Offset: 0x0000028B
		private void \u2036(object A_1, EventArgs A_2)
		{
		}

		// Token: 0x06000293 RID: 659 RVA: 0x0001CB08 File Offset: 0x0001AD08
		private void \u203E(object A_1, EventArgs A_2)
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

		// Token: 0x06000294 RID: 660 RVA: 0x0001CBC8 File Offset: 0x0001ADC8
		private void \u2047(object A_1, EventArgs A_2)
		{
			this.Struct_18.Text = DateTime.Now.ToString("dd MMMM yyyy", new CultureInfo("th-TH"));
		}

		// Token: 0x06000295 RID: 661 RVA: 0x0000208B File Offset: 0x0000028B
		private void \u2048(object A_1, EventArgs A_2)
		{
		}

		// Token: 0x06000296 RID: 662 RVA: 0x0000208B File Offset: 0x0000028B
		private void \u2049(object A_1, EventArgs A_2)
		{
		}

		// Token: 0x06000297 RID: 663 RVA: 0x0001CBFC File Offset: 0x0001ADFC
		private void \u204A(object A_1, EventArgs A_2)
		{
			if (this.\u00A0 == null)
			{
				this.\u2025();
			}
			this.Attr_2.BringToFront();
		}

		// Token: 0x06000298 RID: 664 RVA: 0x0001CC18 File Offset: 0x0001AE18
		private void \u204B(object A_1, EventArgs A_2)
		{
			Task.Run(new Action(global::Attr_3.Type_50.Type_29.Attr_2.\u2005));
			string text = this.\u00A0("เปลี่ยนชื่อโปรแกรม", "กรุณาใส่ชื่อโปรแกรมใหม่:");
			if (!string.IsNullOrWhiteSpace(text))
			{
				this.Text = text;
				string text2 = "C:\\MZATUNER";
				if (!Directory.Exists(text2))
				{
					Directory.CreateDirectory(text2);
				}
				File.WriteAllText(Path.Combine(text2, "programName.dat"), text);
				if (this.\u2003 != null)
				{
					this.Type_7.Text = text;
				}
				MessageBox.Show("เปลี่ยนชื่อสำเร็จ!", "แจ้งเตือน");
			}
		}

		// Token: 0x06000299 RID: 665 RVA: 0x0001CCB4 File Offset: 0x0001AEB4
		private void \u204C(object A_1, EventArgs A_2)
		{
			Task.Run(new Action(global::Attr_3.Type_50.Type_29.Attr_2.\u2006));
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
						if (this.Type_6.Image != null)
						{
							this.Type_6.Image.Dispose();
						}
						using (Image image = Image.FromFile(fileName))
						{
							image.Save(text, ImageFormat.Png);
						}
						this.Type_6.Image = Image.FromFile(text);
						MessageBox.Show("เปลี่ยนรูปสำเร็จ!!", "ตั้งต่า", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
					}
				}
				catch (Exception ex)
				{
					MessageBox.Show("เปลี่ยนรูปไม่สำเร็จ!!\n" + ex.Message, "ตั้งต่า", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				}
			}
		}

		// Token: 0x0600029A RID: 666 RVA: 0x0001CDE8 File Offset: 0x0001AFE8
		private void \u204D(object A_1, EventArgs A_2)
		{
			Task.Run(new Action(global::Attr_3.Type_50.Type_29.Attr_2.\u2007));
			string text = Path.Combine(Path.GetTempPath(), "S.exe");
			Assembly executingAssembly = Assembly.GetExecutingAssembly();
			string name = executingAssembly.GetManifestResourceNames().First(new Func<string, bool>(global::Attr_3.Type_50.Type_29.Attr_2.\u00A0));
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

		// Token: 0x0600029B RID: 667 RVA: 0x0001CEE8 File Offset: 0x0001B0E8
		private void \u204E(object A_1, EventArgs A_2)
		{
			Task.Run(new Action(global::Attr_3.Type_50.Type_29.Attr_2.\u2008));
			string text = "C:\\Program Files (x86)\\TunerPro RT\\TunerPro.exe";
			if (File.Exists(text))
			{
				Process.Start(text);
				return;
			}
			Console.WriteLine("ไฟล์ไม่พบ: " + text);
		}

		// Token: 0x0600029C RID: 668 RVA: 0x0001CF40 File Offset: 0x0001B140
		private void \u204F(object A_1, EventArgs A_2)
		{
			Task.Run(new Action(global::Attr_3.Type_50.Type_29.Attr_2.\u2009));
			string text = Path.Combine(Path.GetTempPath(), "MZATUNER.tpk");
			if (!File.Exists(text))
			{
				Assembly executingAssembly = Assembly.GetExecutingAssembly();
				string name = executingAssembly.GetManifestResourceNames().First(new Func<string, bool>(global::Attr_3.Type_50.Type_29.Attr_2.\u1680));
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

		// Token: 0x0600029D RID: 669 RVA: 0x0001D00C File Offset: 0x0001B20C
		private void \u2050(object A_1, EventArgs A_2)
		{
			Task.Run(new Action(global::Attr_3.Type_50.Type_29.Attr_2.\u200A));
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

		// Token: 0x0600029E RID: 670 RVA: 0x0001D0BC File Offset: 0x0001B2BC
		private void \u2051(object A_1, EventArgs A_2)
		{
			Task.Run(new Action(global::Attr_3.Type_50.Type_29.Attr_2.\u200B));
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

		// Token: 0x0600029F RID: 671 RVA: 0x0001D16C File Offset: 0x0001B36C
		private void \u2052(object A_1, EventArgs A_2)
		{
			Task.Run(new Action(global::Attr_3.Type_50.Type_29.Attr_2.\u2010));
			string text = Path.Combine(Path.GetTempPath(), "lockdeta.exe");
			if (!File.Exists(text))
			{
				Assembly executingAssembly = Assembly.GetExecutingAssembly();
				string name = executingAssembly.GetManifestResourceNames().First(new Func<string, bool>(global::Attr_3.Type_50.Type_29.Attr_2.\u2000));
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

		// Token: 0x060002A0 RID: 672 RVA: 0x0001D238 File Offset: 0x0001B438
		private void \u2053(object A_1, EventArgs A_2)
		{
			Task.Run(new Action(global::Attr_3.Type_50.Type_29.Attr_2.\u2011));
			string text = Path.Combine(Path.GetTempPath(), "WindowsFormsApp2.exe");
			if (!File.Exists(text))
			{
				Assembly executingAssembly = Assembly.GetExecutingAssembly();
				string name = executingAssembly.GetManifestResourceNames().First(new Func<string, bool>(global::Attr_3.Type_50.Type_29.Attr_2.\u2001));
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

		// Token: 0x060002A1 RID: 673 RVA: 0x0001D304 File Offset: 0x0001B504
		private void \u2054(object A_1, EventArgs A_2)
		{
			Task.Run(new Action(global::Attr_3.Type_50.Type_29.Attr_2.\u2012));
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
			this.\u00A0(array, array.Length, ref array4, ref num, 0);
			Thread.Sleep(100);
			this.\u00A0(array2, array2.Length, ref array4, ref num, 0);
			Thread.Sleep(100);
			this.\u00A0(array3, array3.Length, ref array4, ref num, 0);
			Thread.Sleep(1000);
			MessageBox.Show("รีเซ็ตจำนวนการ Flash เรียบร้อยแล้ว\r\nกรุณา ปิด และ เปิด สวิตช์กุญแจ (OFF - ON)\r\nอีกครั้งเป็นเวลา 5 วินาที", "ลบจำนวนการ Flash", MessageBoxButtons.OK);
		}

		// Token: 0x060002A2 RID: 674 RVA: 0x0001D3E8 File Offset: 0x0001B5E8
		private void \u2055(object A_1, EventArgs A_2)
		{
			Task.Run(new Action(global::Attr_3.Type_50.Type_29.Attr_2.\u2013));
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
			this.\u00A0(array, array.Length, ref array3, ref num, 0);
			Thread.Sleep(1000);
			this.\u00A0(array2, array2.Length, ref array3, ref num, 0);
			MessageBox.Show("รีเซ็ต ECM เรียบร้อยแล้ว", "รีเซ็ต ECM", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			MessageBox.Show("กรุณา ปิด และ เปิด กุญแจรถใหม่อีกครั้ง\r\nแล้วรอประมาณ 10 วินาที", "รีเซ็ต ECM", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}

		// Token: 0x060002A3 RID: 675 RVA: 0x0001D4B0 File Offset: 0x0001B6B0
		private void \u2056(object A_1, EventArgs A_2)
		{
			\u205A.\u202E u202E;
			u202E.\u00A0 = AsyncVoidMethodBuilder.Create();
			u202E.\u00A0 = -1;
			u202E.Attr_2.Start<\u205A.\u202E>(ref u202E);
		}

		// Token: 0x060002A4 RID: 676 RVA: 0x0001D4E0 File Offset: 0x0001B6E0
		private void \u2057(object A_1, EventArgs A_2)
		{
			\u205A.\u202F u202F;
			u202F.\u00A0 = AsyncVoidMethodBuilder.Create();
			u202F.\u00A0 = this;
			u202F.\u00A0 = -1;
			u202F.Attr_2.Start<\u205A.\u202F>(ref u202F);
		}

		// Token: 0x060002A5 RID: 677 RVA: 0x0001D518 File Offset: 0x0001B718
		private static bool \u00A0(byte[] A_0, uint A_1, byte[] A_2)
		{
			if (A_0 == null || A_2 == null)
			{
				return false;
			}
			if ((ulong)A_1 < (ulong)((long)A_2.Length))
			{
				return false;
			}
			int num = (int)(A_1 - (uint)A_2.Length);
			for (int i = 0; i <= num; i++)
			{
				bool flag = true;
				for (int j = 0; j < A_2.Length; j++)
				{
					if (A_0[i + j] != A_2[j])
					{
						flag = false;
						break;
					}
				}
				if (flag)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060002A6 RID: 678 RVA: 0x0001D570 File Offset: 0x0001B770
		private void \u00A0(Control A_1, bool A_2)
		{
			\u205A.\u2028 u = new \u205A.\u2028();
			u.\u00A0 = A_1;
			u.\u00A0 = A_2;
			if (u.\u00A0 == null)
			{
				return;
			}
			if (u.Attr_2.InvokeRequired)
			{
				u.Attr_2.BeginInvoke(new Action(u.\u00A0));
				return;
			}
			u.Attr_2.Enabled = u.\u00A0;
		}

		// Token: 0x060002A7 RID: 679 RVA: 0x0001D5D4 File Offset: 0x0001B7D4
		private static byte \u1680(byte[] A_0, int A_1)
		{
			int num = 0;
			for (int i = 0; i < A_1; i++)
			{
				num += (int)A_0[i];
			}
			return (byte)(256 - (num & 255) & 255);
		}

		// Token: 0x060002A8 RID: 680 RVA: 0x0001D60C File Offset: 0x0001B80C
		private bool \u00A0(byte[] A_1)
		{
			if (A_1 == null || A_1.Length < 32768)
			{
				return false;
			}
			int num = A_1.Length - 1;
			uint num2 = 0U;
			for (int i = 0; i < num; i++)
			{
				num2 += (uint)A_1[i];
			}
			byte b = (byte)(256U - (num2 & 255U) & 255U);
			byte b2 = A_1[num];
			return b == b2;
		}

		// Token: 0x060002A9 RID: 681 RVA: 0x0001D660 File Offset: 0x0001B860
		private void \u1680(byte[] A_1)
		{
			if (A_1 == null || A_1.Length < 32768)
			{
				return;
			}
			int num = A_1.Length - 1;
			uint num2 = 0U;
			for (int i = 0; i < num; i++)
			{
				num2 += (uint)A_1[i];
			}
			A_1[num] = (byte)(256U - (num2 & 255U) & 255U);
		}

		// Token: 0x060002AA RID: 682 RVA: 0x0001D6AC File Offset: 0x0001B8AC
		private DialogResult \u00A0(out byte A_1, out byte A_2)
		{
			A_1 = this.\u00A0;
			A_2 = this.\u1680;
			DialogResult result;
			using (Form form = new Form())
			{
				\u205A.\u2029 u = new \u205A.\u2029();
				form.Text = "ตั้งค่ารหัสผ่าน ECU (Security Mode)";
				form.FormBorderStyle = FormBorderStyle.FixedDialog;
				form.StartPosition = FormStartPosition.CenterParent;
				form.ClientSize = new Size(420, 210);
				form.MaximizeBox = (form.MinimizeBox = false);
				form.BackColor = Color.FromArgb(12, 12, 12);
				form.ForeColor = Color.White;
				form.Font = new Font("Segoe UI", 10f, FontStyle.Regular);
				Label label = new Label
				{
					Text = "🔑 แก้ไข PASSWORD",
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
				u.\u00A0 = new TextBox
				{
					Location = new Point(110, 58),
					Width = 80,
					CharacterCasing = CharacterCasing.Upper,
					Text = global::Attr_3.Type_50.\u00A0(this.\u00A0),
					BackColor = Color.Black,
					ForeColor = Color.Red,
					Font = new Font("Consolas", 14f, FontStyle.Bold),
					BorderStyle = BorderStyle.FixedSingle,
					TextAlign = HorizontalAlignment.Center
				};
				u.\u1680 = new TextBox
				{
					Location = new Point(110, 98),
					Width = 80,
					CharacterCasing = CharacterCasing.Upper,
					Text = global::Attr_3.Type_50.\u00A0(this.\u1680),
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
				u.\u00A0 = new Label
				{
					Location = new Point(18, 142),
					AutoSize = true,
					Font = new Font("Consolas", 9.5f, FontStyle.Regular),
					ForeColor = Color.Tomato
				};
				u.\u00A0 = new Button
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
				u.Attr_2.FlatAppearance.BorderSize = 1;
				u.Attr_2.FlatAppearance.BorderColor = Color.Red;
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
				u.Attr_2.TextChanged += u.\u00A0;
				u.Attr_3.TextChanged += u.\u1680;
				u.\u00A0();
				form.Controls.AddRange(new Control[]
				{
					label,
					label2,
					label3,
					u.\u00A0,
					u.\u1680,
					u.\u00A0,
					u.\u00A0,
					button,
					pictureBox
				});
				form.AcceptButton = u.\u00A0;
				form.CancelButton = button;
				DialogResult dialogResult = form.ShowDialog(this);
				if (dialogResult == DialogResult.OK)
				{
					global::Attr_3.Type_50.\u00A0(u.Attr_2.Text, out A_1);
					global::Attr_3.Type_50.\u00A0(u.Attr_3.Text, out A_2);
				}
				result = dialogResult;
			}
			return result;
		}

		// Token: 0x060002AB RID: 683 RVA: 0x0001DC04 File Offset: 0x0001BE04
		private static bool \u00A0(string A_0, out byte A_1)
		{
			A_1 = 0;
			if (string.IsNullOrWhiteSpace(A_0))
			{
				return false;
			}
			A_0 = A_0.Trim();
			if (A_0.StartsWith("0x", StringComparison.OrdinalIgnoreCase))
			{
				A_0 = A_0.Substring(2);
			}
			else if (A_0.EndsWith("h", StringComparison.OrdinalIgnoreCase))
			{
				A_0 = A_0.Substring(0, A_0.Length - 1);
			}
			return A_0.Length != 0 && A_0.Length <= 2 && byte.TryParse(A_0, NumberStyles.HexNumber, null, out A_1);
		}

		// Token: 0x060002AC RID: 684 RVA: 0x0001DC84 File Offset: 0x0001BE84
		private Task<bool> \u2000(int A_1)
		{
			\u205A.\u202C u202C;
			u202C.\u00A0 = AsyncTaskMethodBuilder<bool>.Create();
			u202C.\u00A0 = this;
			u202C.\u1680 = A_1;
			u202C.\u00A0 = -1;
			u202C.Attr_2.Start<\u205A.\u202C>(ref u202C);
			return u202C.Attr_2.Task;
		}

		// Token: 0x060002AD RID: 685 RVA: 0x0001DCD0 File Offset: 0x0001BED0
		private void \u2014()
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

		// Token: 0x060002AE RID: 686 RVA: 0x0001DD24 File Offset: 0x0001BF24
		private static string \u00A0(byte A_0)
		{
			return A_0.ToString("X2");
		}

		// Token: 0x060002AF RID: 687 RVA: 0x0001DD34 File Offset: 0x0001BF34
		private static byte[] \u00A0(byte A_0, byte A_1)
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
			array[4] = A_0;
			array[5] = A_1;
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

		// Token: 0x060002B0 RID: 688 RVA: 0x0000208B File Offset: 0x0000028B
		private void \u2058(object A_1, EventArgs A_2)
		{
		}

		// Token: 0x060002B1 RID: 689 RVA: 0x0001DDAC File Offset: 0x0001BFAC
		private void \u2015()
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

		// Token: 0x060002B2 RID: 690 RVA: 0x0001DE6C File Offset: 0x0001C06C
		private void \u2059(object A_1, EventArgs A_2)
		{
			Task.Run(new Action(global::Attr_3.Type_50.Type_29.Attr_2.\u2024));
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

		// Token: 0x060002B3 RID: 691 RVA: 0x0001DEE8 File Offset: 0x0001C0E8
		private void \u205A(object A_1, EventArgs A_2)
		{
			using (\u2053 u = new Type_49(this.Attr_2.BackColor))
			{
				if (u.ShowDialog(this) == DialogResult.OK)
				{
					this.\u00A0(this, u.\u00A0());
					string text = "C:\\MZATUNER";
					if (!Directory.Exists(text))
					{
						Directory.CreateDirectory(text);
					}
					File.WriteAllText(Path.Combine(text, "headerColor.dat"), ColorTranslator.ToHtml(u.\u00A0()));
				}
			}
		}

		// Token: 0x060002B4 RID: 692 RVA: 0x0001DF6C File Offset: 0x0001C16C
		private void \u00A0(Control A_1, Color A_2)
		{
			foreach (object obj in A_1.Controls)
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
						control.BackColor = A_2;
					}
					else
					{
						control.ForeColor = A_2;
					}
				}
				if (control.HasChildren)
				{
					this.\u00A0(control, A_2);
				}
			}
			this.\u2000(this);
			if (this.\u00A0 != null)
			{
				this.Attr_2.Refresh();
			}
			this.\u00A0(new Color?(A_2));
			if (this.\u00A0 != null)
			{
				string text = "C:\\MZATUNER";
				if (!Directory.Exists(text))
				{
					Directory.CreateDirectory(text);
				}
				File.WriteAllText(Path.Combine(text, "headerColor.dat"), ColorTranslator.ToHtml(this.Attr_2.BackColor));
			}
		}

		// Token: 0x060002B5 RID: 693 RVA: 0x0001E120 File Offset: 0x0001C320
		private IEnumerable<Control> \u1680(Control A_1)
		{
			\u205A.\u202B u202B = new \u205A.\u202B(-2);
			u202B.\u00A0 = this;
			u202B.\u2000 = A_1;
			return u202B;
		}

		// Token: 0x060002B6 RID: 694 RVA: 0x0001E138 File Offset: 0x0001C338
		private void \u2000(Control A_1)
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
					foreach (Control control in this.\u1680(A_1))
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

		// Token: 0x060002B7 RID: 695 RVA: 0x0001E2D4 File Offset: 0x0001C4D4
		private void \u2001(Control A_1)
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
				foreach (Control control in this.\u1680(A_1))
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
				this.\u00A0(null);
			}
			catch (Exception ex)
			{
				MessageBox.Show("โหลดสีล้มเหลว: " + ex.Message);
			}
		}

		// Token: 0x060002B8 RID: 696 RVA: 0x0001E40C File Offset: 0x0001C60C
		private void \u205B(object A_1, EventArgs A_2)
		{
			Task.Run(new Action(global::Attr_3.Type_50.Type_29.Attr_2.\u2025));
			using (OpenFileDialog openFileDialog = new OpenFileDialog
			{
				Title = "Open Binary File",
				Filter = "Bin file (*.bin)|*.bin",
				InitialDirectory = Application.StartupPath
			})
			{
				if (openFileDialog.ShowDialog() != DialogResult.OK)
				{
					this.\u2022();
				}
				else
				{
					string fileName = openFileDialog.FileName;
					FileInfo fileInfo = new FileInfo(fileName);
					if (!fileInfo.Extension.Equals(".bin", StringComparison.OrdinalIgnoreCase))
					{
						this.\u1680("ไฟล์ที่เลือกไม่ใช่รูปแบบ .bin กรุณาตรวจสอบใหม่");
						this.\u2022();
					}
					else
					{
						try
						{
							global::Attr_3.Type_50.\u200A = File.ReadAllBytes(fileName);
							global::Attr_3.Type_50.\u2000 = global::Attr_3.Type_50.Struct_17.Length;
						}
						catch (Exception ex)
						{
							this.\u1680("ไม่สามารถอ่านไฟล์ได้เนื่องจาก: " + ex.Message);
							this.\u2022();
							return;
						}
						this.\u00A0(fileInfo);
						if (!this.\u00A0(global::Attr_3.Type_50.\u200A))
						{
							global::Form_4.Attr_5.\u00A0("Warning. Checksum invalid.");
							if (MessageBox.Show("ตรวจพบว่า Checksum ของไฟล์ไม่ถูกต้อง!\nต้องการให้ระบบแก้ไขให้โดยอัตโนมัติหรือไม่?", "Checksum Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
							{
								this.\u1680(global::Attr_3.Type_50.\u200A);
								global::Form_4.Attr_5.\u00A0("Checksum fixed successfully.");
								global::Attr_3.Type_58.\u00A0("สำเร็จ", "แก้ไข Checksum เรียบร้อยแล้ว", global::Attr_3.Type_57.\u00A0);
							}
						}
						else
						{
							global::Form_4.Attr_5.\u00A0("Checksum verified.");
						}
						if (global::Attr_3.Type_50.\u2004)
						{
							this.\u200B();
						}
						this.Form_4.Enabled = true;
					}
				}
			}
		}

		// Token: 0x060002B9 RID: 697 RVA: 0x0001E5AC File Offset: 0x0001C7AC
		private void \u2022()
		{
			this.Form_4.Text = "(กรุณาเลือกไฟล์ BIN) ..";
			global::Attr_3.Type_50.\u200A = null;
			global::Attr_3.Type_50.\u2000 = 0;
			this.Form_4.Enabled = false;
			this.Type_7.Enabled = false;
			global::Attr_3.Type_58.\u00A0("แจ้งเตือน - MZATUNER", "ล้างข้อมูลไฟล์ .bin เรียบร้อย", global::Attr_3.Type_57.\u1680);
		}

		// Token: 0x060002BA RID: 698 RVA: 0x0001E600 File Offset: 0x0001C800
		private void \u00A0(FileInfo A_1)
		{
			if (A_1 == null || !A_1.Exists)
			{
				MessageBox.Show("ไม่พบข้อมูลไฟล์ที่เลือก กรุณาลองใหม่อีกครั้ง", "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				return;
			}
			double num = (double)A_1.Length;
			string str = (num >= 1048576.0) ? string.Format("{0:N2} MB", num / 1048576.0) : string.Format("{0:N2} KB", num / 1024.0);
			this.Form_4.Text = A_1.Name + " [" + str + "]";
			global::Attr_3.Type_58.\u00A0("สำเร็จ - MZATUNER", "เลือกไฟล์สำเร็จ เริ่มอัดได้เลย", global::Attr_3.Type_57.\u00A0);
			global::Form_4.Attr_5.\u00A0("Binary file loaded successfully.");
		}

		// Token: 0x060002BB RID: 699 RVA: 0x0001E6B2 File Offset: 0x0001C8B2
		private void \u1680(string A_1)
		{
			MessageBox.Show(A_1, "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}

		// Token: 0x060002BC RID: 700 RVA: 0x0001E6C4 File Offset: 0x0001C8C4
		private void \u205C(object A_1, EventArgs A_2)
		{
			Task.Run(new Action(global::Attr_3.Type_50.Type_29.Attr_2.\u2027));
			if (global::Attr_3.Type_50.\u200A == null || global::Attr_3.Type_50.\u2000 == 0)
			{
				MessageBox.Show("กรุณาเลือกไฟล์ .bin ก่อน", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			global::Attr_3.Type_58.\u00A0("เริ่มทำงาน - MZATUNER", "กำลังเตรียมความพร้อม... กรุณารอระบบเชื่อมต่อ", global::Attr_3.Type_57.\u1680);
			global::Form_4.Attr_5.\u00A0("ECU Write operation started. Please do not disconnect.");
			global::Attr_3.Type_50.\u2007 = true;
			base.Update();
			Application.DoEvents();
			this.Type_7.Enabled = false;
			this.Form_4.Enabled = false;
			this.Attr_3.Enabled = true;
			this.Refresh();
		}

		// Token: 0x060002BD RID: 701 RVA: 0x0001E76D File Offset: 0x0001C96D
		private void \u2024()
		{
			if (base.InvokeRequired)
			{
				base.BeginInvoke(new Action(this.\u2024));
				return;
			}
			this.Type_7.Enabled = true;
			this.Form_4.Enabled = true;
			this.Refresh();
		}

		// Token: 0x060002BE RID: 702 RVA: 0x0001E7A9 File Offset: 0x0001C9A9
		private void \u205D(object A_1, EventArgs A_2)
		{
			Task.Run(new Action(global::Attr_3.Type_50.Type_29.Attr_2.\u2028));
			new global::Attr_2.\u2004().Show();
		}

		// Token: 0x060002BF RID: 703 RVA: 0x0001E7DA File Offset: 0x0001C9DA
		private void \u205E(object A_1, EventArgs A_2)
		{
			Task.Run(new Action(global::Attr_3.Type_50.Type_29.Attr_2.\u2029));
			new global::Attr_2.\u2000().Show();
		}

		// Token: 0x060002C0 RID: 704 RVA: 0x0000208B File Offset: 0x0000028B
		private void \u205F(object A_1, EventArgs A_2)
		{
		}

		// Token: 0x060002C1 RID: 705 RVA: 0x0001E80C File Offset: 0x0001CA0C
		private void \u2025()
		{
			if (this.\u00A0 == null)
			{
				this.\u00A0 = new Label
				{
					Size = new Size(20, 20),
					Location = new Point(10, 20),
					BackColor = Color.Red,
					Text = "",
					BorderStyle = BorderStyle.FixedSingle,
					Cursor = Cursors.Hand
				};
				this.Attr_2.MouseDown += this.\u00A0;
				this.Attr_2.MouseMove += this.\u1680;
				this.Attr_2.MouseUp += this.\u2000;
				this.Form_4.Controls.Add(this.\u00A0);
				this.Attr_2.BringToFront();
			}
			System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
			timer.Interval = 1000;
			timer.Tick += this.\u1680\u2049;
			timer.Start();
		}

		// Token: 0x060002C2 RID: 706 RVA: 0x0001E904 File Offset: 0x0001CB04
		private void \u2060(object A_1, EventArgs A_2)
		{
			bool @checked = this.Attr_3.Checked;
			this.Attr_3.ReadOnly = !@checked;
			this.Attr_2.ReadOnly = !@checked;
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x0001E93C File Offset: 0x0001CB3C
		private void \u2061(object A_1, EventArgs A_2)
		{
			Task.Run(new Action(global::Attr_3.Type_50.Type_29.Attr_2.\u202A));
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

		// Token: 0x060002C4 RID: 708 RVA: 0x0001E9CF File Offset: 0x0001CBCF
		private void \u2062(object A_1, EventArgs A_2)
		{
			Task.Run(new Action(global::Attr_3.Type_50.Type_29.Attr_2.\u202B));
			Application.Restart();
			Environment.Exit(0);
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x0000208B File Offset: 0x0000028B
		private void \u2063(object A_1, EventArgs A_2)
		{
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x0000208B File Offset: 0x0000028B
		private void \u2064(object A_1, EventArgs A_2)
		{
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x0001EA04 File Offset: 0x0001CC04
		private void \u206A(object A_1, EventArgs A_2)
		{
			Task.Run(new Action(global::Attr_3.Type_50.Type_29.Attr_2.\u202C));
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

		// Token: 0x060002C8 RID: 712 RVA: 0x0000208B File Offset: 0x0000028B
		private void \u206B(object A_1, EventArgs A_2)
		{
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x0001EAB4 File Offset: 0x0001CCB4
		private void \u206C(object A_1, EventArgs A_2)
		{
			try
			{
				global::Attr_3.Type_50.\u2001 = true;
				if (this.\u00A0 != null && this.Attr_2.IsAlive && !this.Attr_2.Join(2000))
				{
					MessageBox.Show("Thread ยังไม่ยอมจบเอง อาจมีปัญหาในการคืน resource", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				}
				if (global::Attr_3.Type_50.\u00A0 != IntPtr.Zero)
				{
					FTDI.FT_Close(global::Attr_3.Type_50.\u00A0);
					global::Attr_3.Type_50.\u00A0 = IntPtr.Zero;
					Thread.Sleep(100);
				}
				using (global::Attr_2.\u2006 u = new global::Attr_2.\u2006(base.Icon))
				{
					u.Enabled = true;
					u.ShowDialog();
				}
				if (!this.\u2009())
				{
					MessageBox.Show("Failed to initialize FTDI", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				}
				else
				{
					global::Attr_3.Type_50.\u2001 = false;
					this.\u00A0 = new Thread(new ThreadStart(this.\u200A))
					{
						IsBackground = true
					};
					this.Attr_2.Start();
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("Error in sceToolStripMenuItem_Click: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}

		// Token: 0x060002CA RID: 714 RVA: 0x0001EBE0 File Offset: 0x0001CDE0
		private void \u206D(object A_1, EventArgs A_2)
		{
			\u205A.\u2032 u;
			u.\u00A0 = AsyncVoidMethodBuilder.Create();
			u.\u00A0 = this;
			u.\u00A0 = -1;
			u.Attr_2.Start<\u205A.\u2032>(ref u);
		}

		// Token: 0x060002CB RID: 715 RVA: 0x0000208B File Offset: 0x0000028B
		private void \u206E(object A_1, EventArgs A_2)
		{
		}

		// Token: 0x060002CC RID: 716 RVA: 0x0001EC18 File Offset: 0x0001CE18
		private void \u206F(object A_1, EventArgs A_2)
		{
			string text = "C:\\MZATUNER\\DATA\\By.ช่างลิงกล่องซิ่ง\\MultiGaugesHondaMonitor.exe";
			Task.Run(new Action(global::Attr_3.Type_50.Type_29.Attr_2.\u202D));
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

		// Token: 0x060002CD RID: 717 RVA: 0x0001ED30 File Offset: 0x0001CF30
		private void \u3000(object A_1, EventArgs A_2)
		{
			Task.Run(new Action(global::Attr_3.Type_50.Type_29.Attr_2.\u202E));
			string text = Path.Combine(Path.GetTempPath(), "S.exe");
			Assembly executingAssembly = Assembly.GetExecutingAssembly();
			string name = executingAssembly.GetManifestResourceNames().First(new Func<string, bool>(global::Attr_3.Type_50.Type_29.Attr_2.\u2002));
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

		// Token: 0x060002CE RID: 718 RVA: 0x0000208B File Offset: 0x0000028B
		private void \u1680\u00A0(object A_1, EventArgs A_2)
		{
		}

		// Token: 0x060002CF RID: 719 RVA: 0x0001EE30 File Offset: 0x0001D030
		private void \u1680\u1680(object A_1, EventArgs A_2)
		{
			Task.Run(new Action(global::Attr_3.Type_50.Type_29.Attr_2.\u202F));
			new Type_52().ShowDialog();
		}

		// Token: 0x060002D0 RID: 720 RVA: 0x0001EE64 File Offset: 0x0001D064
		private void \u1680\u2000(object A_1, EventArgs A_2)
		{
			Task.Run(new Action(global::Attr_3.Type_50.Type_29.Attr_2.\u2032));
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
			this.\u00A0(array, array.Length, ref array4, ref num, 0);
			Thread.Sleep(100);
			this.\u00A0(array2, array2.Length, ref array4, ref num, 0);
			Thread.Sleep(100);
			this.\u00A0(array3, array3.Length, ref array4, ref num, 0);
			Thread.Sleep(1000);
			MessageBox.Show("Reset Flash Count Completed\r\nKnob/Ignition Key OFF - ON\r\nFor 5 Seconds", "Delete Flash Count", MessageBoxButtons.OK);
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x0001EF47 File Offset: 0x0001D147
		private void \u1680\u2001(object A_1, EventArgs A_2)
		{
			this.\u2057(A_1, A_2);
		}

		// Token: 0x060002D2 RID: 722 RVA: 0x0001EF54 File Offset: 0x0001D154
		private void \u1680\u2002(object A_1, EventArgs A_2)
		{
			Task.Run(new Action(global::Attr_3.Type_50.Type_29.Attr_2.\u2035));
			string text = "C:\\Program Files (x86)\\TunerPro RT\\TunerPro.exe";
			if (File.Exists(text))
			{
				Process.Start(text);
				return;
			}
			Console.WriteLine("ไฟล์ไม่พบ: " + text);
		}

		// Token: 0x060002D3 RID: 723 RVA: 0x0001EFAC File Offset: 0x0001D1AC
		private void \u1680\u2003(object A_1, EventArgs A_2)
		{
			Task.Run(new Action(global::Attr_3.Type_50.Type_29.Attr_2.\u2033));
			string text = Path.Combine(Path.GetTempPath(), "MZATUNER.tpk");
			if (!File.Exists(text))
			{
				Assembly executingAssembly = Assembly.GetExecutingAssembly();
				string name = executingAssembly.GetManifestResourceNames().First(new Func<string, bool>(global::Attr_3.Type_50.Type_29.Attr_2.\u2003));
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

		// Token: 0x060002D4 RID: 724 RVA: 0x0001F078 File Offset: 0x0001D278
		private void \u1680\u2004(object A_1, EventArgs A_2)
		{
			Task.Run(new Action(global::Attr_3.Type_50.Type_29.Attr_2.\u2036));
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

		// Token: 0x060002D5 RID: 725 RVA: 0x0001F128 File Offset: 0x0001D328
		private void \u1680\u2005(object A_1, EventArgs A_2)
		{
			Task.Run(new Action(global::Attr_3.Type_50.Type_29.Attr_2.\u203E));
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

		// Token: 0x060002D6 RID: 726 RVA: 0x0001F1D8 File Offset: 0x0001D3D8
		private void \u1680\u2006(object A_1, EventArgs A_2)
		{
			this.\u206C(A_1, A_2);
		}

		// Token: 0x060002D7 RID: 727 RVA: 0x0001F1E4 File Offset: 0x0001D3E4
		private void \u1680\u2007(object A_1, EventArgs A_2)
		{
			Task.Run(new Action(global::Attr_3.Type_50.Type_29.Attr_2.\u2047));
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

		// Token: 0x060002D8 RID: 728 RVA: 0x0001F260 File Offset: 0x0001D460
		private void \u1680\u2008(object A_1, EventArgs A_2)
		{
			Task.Run(new Action(global::Attr_3.Type_50.Type_29.Attr_2.\u2048));
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

		// Token: 0x060002D9 RID: 729 RVA: 0x0001F2F4 File Offset: 0x0001D4F4
		private void \u1680\u2009(object A_1, EventArgs A_2)
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

		// Token: 0x060002DA RID: 730 RVA: 0x0001F45C File Offset: 0x0001D65C
		private void \u1680\u200A(object A_1, EventArgs A_2)
		{
			Task.Run(new Action(global::Attr_3.Type_50.Type_29.Attr_2.\u2049));
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

		// Token: 0x060002DB RID: 731 RVA: 0x0001F50C File Offset: 0x0001D70C
		private void \u1680\u200B(object A_1, EventArgs A_2)
		{
			Task.Run(new Action(global::Attr_3.Type_50.Type_29.Attr_2.\u204A));
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
						if (this.Type_6.Image != null)
						{
							this.Type_6.Image.Dispose();
						}
						using (Image image = Image.FromFile(fileName))
						{
							image.Save(text, ImageFormat.Png);
						}
						this.Type_6.Image = Image.FromFile(text);
						MessageBox.Show("เปลี่ยนรูปสำเร็จ!!", "ตั้งต่า", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
					}
				}
				catch (Exception ex)
				{
					MessageBox.Show("เปลี่ยนรูปไม่สำเร็จ!!\n" + ex.Message, "ตั้งต่า", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				}
			}
		}

		// Token: 0x060002DC RID: 732 RVA: 0x0001F640 File Offset: 0x0001D840
		private void \u1680\u2010(object A_1, EventArgs A_2)
		{
			Task.Run(new Action(global::Attr_3.Type_50.Type_29.Attr_2.\u204B));
			string text = this.\u00A0("เปลี่ยนชื่อโปรแกรม", "กรุณาใส่ชื่อโปรแกรมใหม่:");
			if (!string.IsNullOrWhiteSpace(text))
			{
				this.Text = text;
				string text2 = "C:\\MZATUNER";
				if (!Directory.Exists(text2))
				{
					Directory.CreateDirectory(text2);
				}
				File.WriteAllText(Path.Combine(text2, "programName.dat"), text);
				if (this.\u2003 != null)
				{
					this.Type_7.Text = text;
				}
				MessageBox.Show("เปลี่ยนชื่อสำเร็จ!", "แจ้งเตือน");
			}
		}

		// Token: 0x060002DD RID: 733 RVA: 0x0001F6DC File Offset: 0x0001D8DC
		private void \u1680\u2011(object A_1, EventArgs A_2)
		{
			\u205A.\u2035 u;
			u.\u00A0 = AsyncVoidMethodBuilder.Create();
			u.\u00A0 = this;
			u.\u00A0 = -1;
			u.Attr_2.Start<\u205A.\u2035>(ref u);
		}

		// Token: 0x060002DE RID: 734 RVA: 0x0001F714 File Offset: 0x0001D914
		private void \u1680\u2012(object A_1, EventArgs A_2)
		{
			Task.Run(new Action(global::Attr_3.Type_50.Type_29.Attr_2.\u204D));
			try
			{
				string text;
				if (!File.Exists(text = Path.Combine(Application.StartupPath, "S.exe")))
				{
					Assembly executingAssembly = Assembly.GetExecutingAssembly();
					string text2 = executingAssembly.GetManifestResourceNames().FirstOrDefault(new Func<string, bool>(global::Attr_3.Type_50.Type_29.Attr_2.\u2004));
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

		// Token: 0x060002DF RID: 735 RVA: 0x0001F87C File Offset: 0x0001DA7C
		private void \u1680\u2013(object A_1, EventArgs A_2)
		{
			string text = "C:\\MZATUNER\\DATA\\By.ช่างลิงกล่องซิ่ง\\MultiGaugesHondaMonitor.exe";
			Task.Run(new Action(global::Attr_3.Type_50.Type_29.Attr_2.\u204E));
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

		// Token: 0x060002E0 RID: 736 RVA: 0x0001F994 File Offset: 0x0001DB94
		private void \u1680\u2014(object A_1, EventArgs A_2)
		{
			Task.Run(new Action(global::Attr_3.Type_50.Type_29.Attr_2.\u204F));
			try
			{
				string text = Path.Combine(Path.GetTempPath(), "lockdeta.exe");
				Assembly executingAssembly = Assembly.GetExecutingAssembly();
				string text2 = executingAssembly.GetManifestResourceNames().FirstOrDefault(new Func<string, bool>(global::Attr_3.Type_50.Type_29.Attr_2.\u2005));
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

		// Token: 0x060002E1 RID: 737 RVA: 0x0001FAB0 File Offset: 0x0001DCB0
		private void \u1680\u2015(object A_1, EventArgs A_2)
		{
			Task.Run(new Action(global::Attr_3.Type_50.Type_29.Attr_2.\u2050));
			new global::Attr_2.\u2000().Show();
		}

		// Token: 0x060002E2 RID: 738 RVA: 0x0001FAE1 File Offset: 0x0001DCE1
		private void \u1680\u2022(object A_1, EventArgs A_2)
		{
			Task.Run(new Action(global::Attr_3.Type_50.Type_29.Attr_2.\u2051));
			new global::Attr_2.\u2004().Show();
		}

		// Token: 0x060002E3 RID: 739 RVA: 0x0001FB14 File Offset: 0x0001DD14
		private void \u1680\u2024(object A_1, EventArgs A_2)
		{
			Task.Run(new Action(global::Attr_3.Type_50.Type_29.Attr_2.\u2052));
			try
			{
				new Type_4C().ShowDialog();
				global::Attr_3.Type_5F.\u1680();
				global::Attr_3.Type_58.\u00A0("แจ้งเตือน", "อัพเดทข้อมูลรหัสกล่องสำเร็จ", global::Attr_3.Type_57.\u1680);
			}
			catch (Exception ex)
			{
				MessageBox.Show("ไม่สามารถเปิดตัวจัดการรหัสกล่องได้: " + ex.Message);
			}
		}

		// Token: 0x060002E4 RID: 740 RVA: 0x0001FB94 File Offset: 0x0001DD94
		private void \u1680\u2025(object A_1, EventArgs A_2)
		{
			this.\u206D(A_1, A_2);
		}

		// Token: 0x060002E5 RID: 741 RVA: 0x0000208B File Offset: 0x0000028B
		private void \u1680\u2027(object A_1, EventArgs A_2)
		{
		}

		// Token: 0x060002E6 RID: 742 RVA: 0x0001FBA0 File Offset: 0x0001DDA0
		private void \u1680\u2028(object A_1, EventArgs A_2)
		{
			\u205A.\u202D u202D;
			u202D.\u00A0 = AsyncVoidMethodBuilder.Create();
			u202D.\u00A0 = this;
			u202D.\u00A0 = A_1;
			u202D.\u00A0 = -1;
			u202D.Attr_2.Start<\u205A.\u202D>(ref u202D);
		}

		// Token: 0x060002E7 RID: 743 RVA: 0x0001FB94 File Offset: 0x0001DD94
		private void \u1680\u2029(object A_1, EventArgs A_2)
		{
			this.\u206D(A_1, A_2);
		}

		// Token: 0x060002E8 RID: 744 RVA: 0x0001FBE0 File Offset: 0x0001DDE0
		private void \u1680(object A_1, PaintEventArgs A_2)
		{
			Button button = A_1 as Button;
			if (button == null)
			{
				return;
			}
			A_2.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
			A_2.Graphics.Clear(Color.Black);
			Rectangle rect = new Rectangle(1, 1, button.Width - 2, button.Height - 2);
			using (SolidBrush solidBrush = new SolidBrush(button.BackColor))
			{
				A_2.Graphics.FillEllipse(solidBrush, rect);
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
					A_2.Graphics.FillPath(pathGradientBrush, graphicsPath);
				}
			}
		}

		// Token: 0x060002E9 RID: 745 RVA: 0x0000208B File Offset: 0x0000028B
		private void \u1680\u202A(object A_1, EventArgs A_2)
		{
		}

		// Token: 0x060002EA RID: 746 RVA: 0x0001FD14 File Offset: 0x0001DF14
		private void \u2000(object A_1, PaintEventArgs A_2)
		{
			Control control = A_1 as Control;
			if (control != null)
			{
				using (Pen pen = new Pen(Color.FromArgb(70, 70, 70), 1f))
				{
					A_2.Graphics.DrawRectangle(pen, 0, 0, control.Width - 1, control.Height - 1);
				}
			}
		}

		// Token: 0x060002EB RID: 747 RVA: 0x0000208B File Offset: 0x0000028B
		private void \u1680\u202B(object A_1, EventArgs A_2)
		{
		}

		// Token: 0x060002EC RID: 748 RVA: 0x0001FD7C File Offset: 0x0001DF7C
		private void \u1680\u202C(object A_1, EventArgs A_2)
		{
			Task.Run(new Action(global::Attr_3.Type_50.Type_29.Attr_2.\u2053));
			new Type_52().ShowDialog();
		}

		// Token: 0x060002ED RID: 749 RVA: 0x0001FDB0 File Offset: 0x0001DFB0
		private void \u2027()
		{
			if (this.\u00A0 == null)
			{
				return;
			}
			this.Attr_2.MouseDown += this.\u2001;
			if (this.\u00A0 != null)
			{
				this.Attr_2.Cursor = Cursors.Hand;
				this.Attr_2.Paint += this.\u2003;
			}
			if (this.\u2003 != null)
			{
				this.Type_7.Cursor = Cursors.Hand;
				this.Type_7.MouseDown += this.\u2002;
				this.Type_7.Click += this.\u1680\u204A;
				string path = "C:\\MZATUNER\\programName.dat";
				if (File.Exists(path))
				{
					try
					{
						string text = File.ReadAllText(path);
						if (!string.IsNullOrWhiteSpace(text))
						{
							this.Type_7.Text = text;
							this.Text = text;
						}
					}
					catch
					{
					}
				}
			}
			if (this.\u2002 != null)
			{
				this.Type_6.Click += global::Attr_3.Type_50.Type_29.Attr_2.\u00A0;
			}
			if (this.\u2001 != null)
			{
				this.Attr_5.Click += this.\u1680\u204B;
			}
			if (this.\u00A0 != null)
			{
				this.Attr_2.BackColor = Color.Black;
				this.Attr_2.ForeColor = Color.White;
				this.Attr_2.Renderer = new \u205A.\u200B();
				this.Attr_2.Padding = new Padding(6, 2, 0, 2);
				foreach (object obj in this.Attr_2.Items)
				{
					ToolStripItem toolStripItem = (ToolStripItem)obj;
					toolStripItem.ForeColor = Color.White;
					toolStripItem.BackColor = Color.Transparent;
				}
				this.Attr_2.ItemClicked -= this.\u00A0;
				this.Attr_2.ItemClicked += this.\u00A0;
				this.\u00A0(this.Attr_2.Items);
			}
			if (this.\u1680 != null)
			{
				this.Attr_3.Font = new Font("Segoe UI", 9f, FontStyle.Bold);
				this.Attr_3.ForeColor = Color.FromArgb(150, 150, 150);
			}
			if (this.\u2000 != null)
			{
				this.Form_4.Font = new Font("Trebuchet MS", 10f, FontStyle.Bold);
				this.Form_4.ForeColor = Color.Yellow;
				this.Form_4.BackColor = Color.Transparent;
			}
			if (this.\u2000 != null)
			{
				this.Form_4.Size = new Size(14, 14);
				this.Form_4.Paint -= this.\u1680;
				this.Form_4.Paint += global::Attr_3.Type_50.Type_29.Attr_2.\u00A0;
				if (this.\u2002 != null)
				{
					this.Type_6.Cursor = Cursors.Hand;
					this.Type_6.Paint += this.\u00A0;
				}
				base.Region = Region.FromHrgn(global::Attr_3.Type_50.\u00A0(0, 0, base.Width, base.Height, 15, 15));
				string path2 = "C:\\MZATUNER\\headerColor.dat";
				if (File.Exists(path2))
				{
					try
					{
						string text2 = File.ReadAllText(path2);
						if (!string.IsNullOrWhiteSpace(text2))
						{
							this.Attr_2.BackColor = ColorTranslator.FromHtml(text2);
							if (this.\u00A0 != null)
							{
								this.Attr_2.Refresh();
							}
						}
					}
					catch
					{
					}
				}
			}
		}

		// Token: 0x060002EE RID: 750 RVA: 0x00020160 File Offset: 0x0001E360
		private void \u00A0(ToolStripItemCollection A_1)
		{
			foreach (object obj in A_1)
			{
				ToolStripItem toolStripItem = (ToolStripItem)obj;
				\u205A.\u202A u202A = new \u205A.\u202A();
				u202A.\u00A0 = (toolStripItem as ToolStripMenuItem);
				if (u202A.\u00A0 != null)
				{
					if (!u202A.Attr_2.HasDropDownItems)
					{
						u202A.Attr_2.Click += u202A.\u00A0;
					}
					if (u202A.Attr_2.HasDropDownItems)
					{
						this.\u00A0(u202A.Attr_2.DropDownItems);
					}
				}
			}
		}

		// Token: 0x060002EF RID: 751 RVA: 0x0000208B File Offset: 0x0000028B
		private void \u00A0(object A_1, ToolStripItemClickedEventArgs A_2)
		{
		}

		// Token: 0x060002F0 RID: 752 RVA: 0x0002020C File Offset: 0x0001E40C
		private void \u1680\u202D(object A_1, EventArgs A_2)
		{
			using (\u2053 u = new Type_49((this.\u00A0 != null) ? this.Attr_2.BackColor : Color.Red))
			{
				if (u.ShowDialog(this) == DialogResult.OK)
				{
					this.\u00A0(this, u.\u00A0());
					string text = "C:\\MZATUNER";
					if (!Directory.Exists(text))
					{
						Directory.CreateDirectory(text);
					}
					File.WriteAllText(Path.Combine(text, "headerColor.dat"), ColorTranslator.ToHtml(u.\u00A0()));
					MessageBox.Show("เปลี่ยนสีธีมโปรแกรมสำเร็จ! (CHROMA_SYNC)", "แจ้งเตือน");
				}
			}
		}

		// Token: 0x060002F1 RID: 753 RVA: 0x000202AC File Offset: 0x0001E4AC
		private void \u1680\u202E(object A_1, EventArgs A_2)
		{
			this.\u1680\u2008(A_1, A_2);
		}

		// Token: 0x060002F2 RID: 754 RVA: 0x000202B8 File Offset: 0x0001E4B8
		private void \u1680\u202F(object A_1, EventArgs A_2)
		{
			Task.Run(new Action(global::Attr_3.Type_50.Type_29.Attr_2.\u2054));
			using (\u2053 u = new Type_49((this.\u00A0 != null) ? this.Attr_2.BackColor : Color.Red))
			{
				if (u.ShowDialog(this) == DialogResult.OK)
				{
					this.\u00A0(this, u.\u00A0());
					string text = "C:\\MZATUNER";
					if (!Directory.Exists(text))
					{
						Directory.CreateDirectory(text);
					}
					File.WriteAllText(Path.Combine(text, "headerColor.dat"), ColorTranslator.ToHtml(u.\u00A0()));
					MessageBox.Show("เปลี่ยนสีธีมสำเร็จ! (CHROMA_SYNC)", "แจ้งเตือน");
				}
			}
		}

		// Token: 0x060002F3 RID: 755 RVA: 0x00020380 File Offset: 0x0001E580
		private void \u2001(object A_1, PaintEventArgs A_2)
		{
			if (this.\u00A0 != null)
			{
				using (LinearGradientBrush linearGradientBrush = new LinearGradientBrush(this.Attr_2.ClientRectangle, Color.FromArgb(60, Color.White), Color.Transparent, 90f))
				{
					A_2.Graphics.FillRectangle(linearGradientBrush, this.Attr_2.ClientRectangle);
				}
				using (Pen pen = new Pen(Color.FromArgb(40, Color.Black), 1f))
				{
					A_2.Graphics.DrawLine(pen, 0, this.Attr_2.Height - 1, this.Attr_2.Width, this.Attr_2.Height - 1);
				}
			}
		}

		// Token: 0x060002F4 RID: 756 RVA: 0x0000208B File Offset: 0x0000028B
		private void \u1680\u2032(object A_1, EventArgs A_2)
		{
		}

		// Token: 0x060002F5 RID: 757 RVA: 0x00020454 File Offset: 0x0001E654
		private void \u1680\u2035(object A_1, EventArgs A_2)
		{
			Task.Run(new Action(global::Attr_3.Type_50.Type_29.Attr_2.\u2055));
			using (\u2059 u = new Type_4F())
			{
				u.ShowDialog();
			}
		}

		// Token: 0x060002F6 RID: 758 RVA: 0x000204B0 File Offset: 0x0001E6B0
		private void \u1680\u2033(object A_1, EventArgs A_2)
		{
			Task.Run(new Action(global::Attr_3.Type_50.Type_29.Attr_2.\u2056));
			new Type_4A().ShowDialog();
		}

		// Token: 0x060002F7 RID: 759 RVA: 0x000204E2 File Offset: 0x0001E6E2
		private void \u1680\u2036(object A_1, EventArgs A_2)
		{
			Task.Run(new Action(global::Attr_3.Type_50.Type_29.Attr_2.\u2057));
			new Type_51().ShowDialog();
		}

		// Token: 0x060002F8 RID: 760 RVA: 0x00020514 File Offset: 0x0001E714
		private void \u1680\u203E(object A_1, EventArgs A_2)
		{
			Task.Run(new Action(global::Attr_3.Type_50.Type_29.Attr_2.\u2058));
			new Type_4D().ShowDialog();
		}

		// Token: 0x060002F9 RID: 761 RVA: 0x00020548 File Offset: 0x0001E748
		private void \u1680\u2047(object A_1, EventArgs A_2)
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
			this.\u00A0(array, array.Length, ref array3, ref num, 0);
			Thread.Sleep(1000);
			this.\u00A0(array2, array2.Length, ref array3, ref num, 0);
			MessageBox.Show("RESET ECM DONE", "Reset ECM", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			MessageBox.Show("Please turn OFF/ON the ignition key\rfor 10 seconds", "Reset ECM", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}

		// Token: 0x060002FA RID: 762 RVA: 0x000205EA File Offset: 0x0001E7EA
		[CompilerGenerated]
		private void \u1680\u2048(object A_1, EventArgs A_2)
		{
			global::Attr_3.Type_58.\u00A0("MZA-TUNER SYSTEM", "ยินดีต้อนรับเข้าสู่โปรแกรม MZA-TUNER", global::Attr_3.Type_57.\u1680);
			this.\u2003();
			this.\u2005();
		}

		// Token: 0x060002FB RID: 763 RVA: 0x00020608 File Offset: 0x0001E808
		[CompilerGenerated]
		private Task \u2028()
		{
			\u205A.\u2012 u;
			u.\u00A0 = AsyncTaskMethodBuilder.Create();
			u.\u00A0 = this;
			u.\u00A0 = -1;
			u.Attr_2.Start<\u205A.\u2012>(ref u);
			return u.Attr_2.Task;
		}

		// Token: 0x060002FC RID: 764 RVA: 0x0002064C File Offset: 0x0001E84C
		[CompilerGenerated]
		private void \u2029()
		{
			try
			{
				base.Invalidate();
				if (this.\u2002 != null)
				{
					this.Type_6.Invalidate();
				}
			}
			catch
			{
			}
		}

		// Token: 0x060002FD RID: 765 RVA: 0x00020688 File Offset: 0x0001E888
		[CompilerGenerated]
		private void \u2002(object A_1, PaintEventArgs A_2)
		{
			try
			{
				if (this.\u1680)
				{
					this.\u00A0(A_2.Graphics, this.\u2002);
				}
			}
			catch
			{
			}
		}

		// Token: 0x060002FE RID: 766 RVA: 0x000206C4 File Offset: 0x0001E8C4
		[CompilerGenerated]
		private void \u202A()
		{
			global::Attr_3.Type_58.\u00A0("สำเร็จ - MZATUNER", "อัดไฟล์เสร็จสิ้น!! ปิดเปิดกุญแจใหม่อีกครั้ง", global::Attr_3.Type_57.\u00A0);
			this.\u2024();
			Task.Run(new Action(global::Attr_3.Type_50.Type_29.Attr_2.\u1680));
			global::Form_4.Attr_5.\u00A0("Write operation complete. Please cycle the ignition key.");
		}

		// Token: 0x060002FF RID: 767 RVA: 0x00020718 File Offset: 0x0001E918
		[CompilerGenerated]
		private void \u202B()
		{
			Console.Beep(400, 300);
			Thread.Sleep(50);
			Console.Beep(400, 300);
			global::Form_4.Attr_5.\u00A0("Write operation failed. Please check connection and try again.");
			MessageBox.Show("การเขียนไฟล์ลงECMล้มเหลว!!\r\nกรุณาปิดเปิดกุญแจแล้วลองอีกครั้ง", "ฮอนด้าแฟลช", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1, (MessageBoxOptions)262144);
			this.\u2024();
		}

		// Token: 0x06000300 RID: 768 RVA: 0x00020773 File Offset: 0x0001E973
		[CompilerGenerated]
		private void \u202C()
		{
			global::Attr_3.Type_45.\u00A0(this, 10);
		}

		// Token: 0x06000301 RID: 769 RVA: 0x00020773 File Offset: 0x0001E973
		[CompilerGenerated]
		private void \u202D()
		{
			global::Attr_3.Type_45.\u00A0(this, 10);
		}

		// Token: 0x06000302 RID: 770 RVA: 0x00020780 File Offset: 0x0001E980
		[CompilerGenerated]
		internal static bool \u00A0(byte[] A_0, uint A_1)
		{
			if (A_0 == null || A_1 < 3U)
			{
				return false;
			}
			int num = (int)(A_1 - 3U);
			return A_0[num] == 0 && A_0[num + 1] == 240 && A_0[num + 2] == 153;
		}

		// Token: 0x06000303 RID: 771 RVA: 0x000207BC File Offset: 0x0001E9BC
		[CompilerGenerated]
		internal static bool \u1680(byte[] A_0, uint A_1, byte[] A_2)
		{
			if (A_0 == null || A_2 == null)
			{
				return false;
			}
			if ((ulong)A_1 < (ulong)((long)A_2.Length))
			{
				return false;
			}
			int num = (int)(A_1 - (uint)A_2.Length);
			for (int i = 0; i <= num; i++)
			{
				bool flag = true;
				for (int j = 0; j < A_2.Length; j++)
				{
					if (A_0[i + j] != A_2[j])
					{
						flag = false;
						break;
					}
				}
				if (flag)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000304 RID: 772 RVA: 0x00020814 File Offset: 0x0001EA14
		[CompilerGenerated]
		private bool \u00A0(byte[] A_1, int A_2, out double A_3)
		{
			A_3 = double.NaN;
			byte[] array = new byte[256];
			uint num = 0U;
			if (!this.\u00A0(A_1, A_1.Length, ref array, ref num, 0))
			{
				return false;
			}
			if ((ulong)num <= (ulong)((long)A_2) || num < 3U)
			{
				return false;
			}
			if (array[1] == 5)
			{
				return false;
			}
			double num2 = (double)array[A_2] / 10.0;
			if (num2 < 6.0 || num2 > 18.0)
			{
				return false;
			}
			A_3 = num2;
			return true;
		}

		// Token: 0x06000305 RID: 773 RVA: 0x00020890 File Offset: 0x0001EA90
		[CompilerGenerated]
		private double \u202E()
		{
			byte[] array = new byte[]
			{
				114,
				5,
				113,
				17,
				7
			};
			double result;
			if (this.\u00A0(array, 16, out result))
			{
				return result;
			}
			byte[] array2 = new byte[]
			{
				114,
				5,
				113,
				16,
				8
			};
			if (this.\u00A0(array2, 16, out result))
			{
				return result;
			}
			byte[] array3 = new byte[]
			{
				114,
				5,
				113,
				19,
				5
			};
			if (this.\u00A0(array3, 14, out result))
			{
				return result;
			}
			byte[] array4 = new byte[]
			{
				114,
				5,
				113,
				22,
				2
			};
			if (this.\u00A0(array4, 21, out result))
			{
				if (global::Attr_3.Type_50.\u2002)
				{
					global::Form_4.Attr_5.\u00A0("ECU Disconnected.");
					global::Attr_3.Type_50.\u2002 = false;
				}
				return result;
			}
			byte[] array5 = new byte[]
			{
				114,
				5,
				113,
				23,
				1
			};
			if (this.\u00A0(array5, 14, out result))
			{
				return result;
			}
			return double.NaN;
		}

		// Token: 0x06000306 RID: 774 RVA: 0x00020968 File Offset: 0x0001EB68
		[CompilerGenerated]
		private void \u00A0(double A_1)
		{
			\u205A.\u2015 u = new \u205A.\u2015();
			u.\u00A0 = this;
			u.\u00A0 = A_1;
			u.\u00A0 = base.Controls.Find("vv", true).FirstOrDefault<Control>();
			u.\u1680 = (base.Controls.Find("label_batrei", true).FirstOrDefault<Control>() ?? base.Controls.Find("TxtBatteryVolt", true).FirstOrDefault<Control>());
			base.BeginInvoke(new Action(u.\u00A0));
		}

		// Token: 0x06000307 RID: 775 RVA: 0x000209F0 File Offset: 0x0001EBF0
		[CompilerGenerated]
		private void \u202F()
		{
			object u = this.\u2001;
			lock (u)
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
				byte[] array2 = new byte[256];
				uint num = 0U;
				this.\u00A0(array, array.Length, ref array2, ref num, 0);
				Thread.Sleep(200);
				byte[] array3 = new byte[]
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
					byte[] array4 = new byte[8];
					array4[0] = 130;
					array4[1] = 130;
					array4[2] = 20;
					array4[3] = 8;
					array4[4] = (byte)i;
					array4[5] = 0;
					array4[6] = 0;
					array4[7] = global::Attr_3.Type_50.\u1680(array4, 7);
					num = 0U;
					if (this.\u00A0(array4, array4.Length, ref array2, ref num, 0) && num > 0U && global::Attr_3.Type_50.\u00A0(array2, num, array3))
					{
						base.Invoke(new Action(global::Attr_3.Type_50.Type_29.Attr_2.\u2015));
						break;
					}
					double num2 = (double)(i + 1) / 128.0;
					base.Invoke(new Action(global::Attr_3.Type_50.Type_29.Attr_2.\u2022));
					Thread.Sleep(15);
				}
			}
		}

		// Token: 0x06000308 RID: 776 RVA: 0x00020B74 File Offset: 0x0001ED74
		[CompilerGenerated]
		internal static void \u00A0(TextBox A_0)
		{
			int selectionStart = A_0.SelectionStart;
			string text = A_0.Text.ToUpperInvariant();
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
			A_0.Text = stringBuilder.ToString();
			A_0.SelectionStart = Math.Min(selectionStart, A_0.Text.Length);
		}

		// Token: 0x06000309 RID: 777 RVA: 0x00020C02 File Offset: 0x0001EE02
		[CompilerGenerated]
		private void \u00A0(object A_1, MouseEventArgs A_2)
		{
			this.\u00A0 = true;
			this.\u00A0 = A_2.Location;
		}

		// Token: 0x0600030A RID: 778 RVA: 0x00020C18 File Offset: 0x0001EE18
		[CompilerGenerated]
		private void \u1680(object A_1, MouseEventArgs A_2)
		{
			if (this.\u00A0)
			{
				this.Attr_2.Left += A_2.X - this.Attr_2.X;
				this.Attr_2.Top += A_2.Y - this.Attr_2.Y;
			}
		}

		// Token: 0x0600030B RID: 779 RVA: 0x00020C75 File Offset: 0x0001EE75
		[CompilerGenerated]
		private void \u2000(object A_1, MouseEventArgs A_2)
		{
			this.\u00A0 = false;
		}

		// Token: 0x0600030C RID: 780 RVA: 0x00020C80 File Offset: 0x0001EE80
		[CompilerGenerated]
		private void \u1680\u2049(object A_1, EventArgs A_2)
		{
			bool flag = SerialPort.GetPortNames().Length != 0;
			this.Attr_2.BackColor = (flag ? Color.Lime : Color.Red);
		}

		// Token: 0x0600030D RID: 781 RVA: 0x00020CB1 File Offset: 0x0001EEB1
		[CompilerGenerated]
		private void \u2001(object A_1, MouseEventArgs A_2)
		{
			if (A_2.Button == MouseButtons.Left)
			{
				global::Attr_3.Type_50.\u2000();
				global::Attr_3.Type_50.\u00A0(base.Handle, 161, 2, 0);
			}
		}

		// Token: 0x0600030E RID: 782 RVA: 0x00020CDC File Offset: 0x0001EEDC
		[CompilerGenerated]
		private void \u2003(object A_1, PaintEventArgs A_2)
		{
			A_2.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
			A_2.Graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
			using (SolidBrush solidBrush = new SolidBrush(Color.White))
			{
				A_2.Graphics.FillEllipse(solidBrush, 1, 1, 24, 24);
				using (Font font = new Font("Segoe UI", 11f, FontStyle.Bold))
				{
					using (SolidBrush solidBrush2 = new SolidBrush((this.\u00A0 != null) ? this.Attr_2.BackColor : Color.FromArgb(215, 15, 15)))
					{
						SizeF sizeF = A_2.Graphics.MeasureString("M", font);
						A_2.Graphics.DrawString("M", font, solidBrush2, new PointF(13f - sizeF.Width / 2f + 0.5f, 13f - sizeF.Height / 2f + 0.5f));
					}
				}
			}
		}

		// Token: 0x0600030F RID: 783 RVA: 0x00020CB1 File Offset: 0x0001EEB1
		[CompilerGenerated]
		private void \u2002(object A_1, MouseEventArgs A_2)
		{
			if (A_2.Button == MouseButtons.Left)
			{
				global::Attr_3.Type_50.\u2000();
				global::Attr_3.Type_50.\u00A0(base.Handle, 161, 2, 0);
			}
		}

		// Token: 0x06000310 RID: 784 RVA: 0x00020DFC File Offset: 0x0001EFFC
		[CompilerGenerated]
		private void \u1680\u204A(object A_1, EventArgs A_2)
		{
			this.\u1680\u2010(A_1, A_2);
		}

		// Token: 0x06000311 RID: 785 RVA: 0x00020E06 File Offset: 0x0001F006
		[CompilerGenerated]
		private void \u1680\u204B(object A_1, EventArgs A_2)
		{
			base.WindowState = FormWindowState.Minimized;
		}

		// Token: 0x0400018D RID: 397
		private string \u00A0;

		// Token: 0x0400018E RID: 398
		private const string \u1680 = "buttonColors.dat";

		// Token: 0x0400018F RID: 399
		private readonly object \u00A0 = new object();

		// Token: 0x04000190 RID: 400
		private Label \u00A0;

		// Token: 0x04000191 RID: 401
		private bool \u00A0;

		// Token: 0x04000192 RID: 402
		private Button \u00A0;

		// Token: 0x04000193 RID: 403
		private Button \u1680;

		// Token: 0x04000194 RID: 404
		private Button \u2000;

		// Token: 0x04000195 RID: 405
		private Label \u1680;

		// Token: 0x04000196 RID: 406
		private Label \u2000;

		// Token: 0x04000197 RID: 407
		private ToolStripMenuItem \u00A0;

		// Token: 0x04000198 RID: 408
		private ToolStripMenuItem \u1680;

		// Token: 0x04000199 RID: 409
		private ToolStripMenuItem \u2000;

		// Token: 0x0400019A RID: 410
		private ToolStripMenuItem \u2001;

		// Token: 0x0400019B RID: 411
		private ToolStripMenuItem \u2002;

		// Token: 0x0400019C RID: 412
		private ToolStripMenuItem \u2003;

		// Token: 0x0400019D RID: 413
		private ToolStripMenuItem \u2004;

		// Token: 0x0400019E RID: 414
		private ToolStripMenuItem \u2005;

		// Token: 0x0400019F RID: 415
		private ToolStripMenuItem \u2006;

		// Token: 0x040001A0 RID: 416
		private ToolStripMenuItem \u2007;

		// Token: 0x040001A1 RID: 417
		private ToolStripMenuItem \u2008;

		// Token: 0x040001A2 RID: 418
		private ToolStripMenuItem \u2009;

		// Token: 0x040001A3 RID: 419
		private ToolStripMenuItem \u200A;

		// Token: 0x040001A4 RID: 420
		private ToolStripMenuItem \u200B;

		// Token: 0x040001A5 RID: 421
		private ToolStripMenuItem \u2010;

		// Token: 0x040001A6 RID: 422
		private ToolStripMenuItem \u2011;

		// Token: 0x040001A7 RID: 423
		private ToolStripMenuItem \u2012;

		// Token: 0x040001A8 RID: 424
		private ToolStripMenuItem \u2013;

		// Token: 0x040001A9 RID: 425
		private ToolStripMenuItem \u2014;

		// Token: 0x040001AA RID: 426
		private ToolStripMenuItem \u2015;

		// Token: 0x040001AB RID: 427
		private ToolStripMenuItem \u2022;

		// Token: 0x040001AC RID: 428
		private ToolStripMenuItem \u2024;

		// Token: 0x040001AD RID: 429
		private ToolStripMenuItem \u2025;

		// Token: 0x040001AE RID: 430
		private ToolStripMenuItem \u2027;

		// Token: 0x040001AF RID: 431
		private ToolStripMenuItem \u2028;

		// Token: 0x040001B0 RID: 432
		private ToolStripMenuItem \u2029;

		// Token: 0x040001B1 RID: 433
		private MenuStrip \u00A0;

		// Token: 0x040001B2 RID: 434
		private ToolStripMenuItem \u202A;

		// Token: 0x040001B3 RID: 435
		private GroupBox \u00A0;

		// Token: 0x040001B4 RID: 436
		private Label \u2001;

		// Token: 0x040001B5 RID: 437
		private ToolStripMenuItem \u202B;

		// Token: 0x040001B6 RID: 438
		private Label \u2002;

		// Token: 0x040001B7 RID: 439
		private ToolStripMenuItem \u202C;

		// Token: 0x040001B8 RID: 440
		private ToolStripMenuItem \u202D;

		// Token: 0x040001B9 RID: 441
		private ToolStripMenuItem \u202E;

		// Token: 0x040001BA RID: 442
		private ToolStripMenuItem \u202F;

		// Token: 0x040001BB RID: 443
		private Point \u00A0;

		// Token: 0x040001BC RID: 444
		private const uint \u00A0 = 1040U;

		// Token: 0x040001BD RID: 445
		private Panel \u00A0;

		// Token: 0x040001BE RID: 446
		private PictureBox \u00A0;

		// Token: 0x040001BF RID: 447
		private Label \u2003;

		// Token: 0x040001C0 RID: 448
		private Button \u2001;

		// Token: 0x040001C1 RID: 449
		private Button \u2002;

		// Token: 0x040001C2 RID: 450
		private bool \u1680;

		// Token: 0x040001C3 RID: 451
		private List<\u205A.\u2009> \u00A0 = new List<\u205A.\u2009>();

		// Token: 0x040001C4 RID: 452
		private Random \u00A0 = new Random();

		// Token: 0x040001C5 RID: 453
		private Color \u00A0 = Color.Red;

		// Token: 0x040001C6 RID: 454
		private readonly object \u1680 = new object();

		// Token: 0x040001C7 RID: 455
		private bool \u2000;

		// Token: 0x040001C8 RID: 456
		private List<string> \u00A0 = new List<string>();

		// Token: 0x040001C9 RID: 457
		private float \u00A0;

		// Token: 0x040001CA RID: 458
		private string \u2000 = "";

		// Token: 0x040001CB RID: 459
		private readonly object \u2000 = new object();

		// Token: 0x040001CC RID: 460
		private const int \u00A0 = 161;

		// Token: 0x040001CD RID: 461
		private const int \u1680 = 2;

		// Token: 0x040001CE RID: 462
		private static readonly string[] \u00A0;

		// Token: 0x040001CF RID: 463
		private Thread \u00A0;

		// Token: 0x040001D0 RID: 464
		private static readonly string[] \u1680;

		// Token: 0x040001D1 RID: 465
		private static readonly string[] \u2000;

		// Token: 0x040001D2 RID: 466
		private static readonly string[] \u2001;

		// Token: 0x040001D3 RID: 467
		private static readonly string[] \u2002;

		// Token: 0x040001D4 RID: 468
		private static readonly string[] \u2003;

		// Token: 0x040001D5 RID: 469
		private static readonly byte[] \u00A0;

		// Token: 0x040001D6 RID: 470
		private static readonly byte[] \u1680;

		// Token: 0x040001D7 RID: 471
		private static readonly byte[] \u2000;

		// Token: 0x040001D8 RID: 472
		private static readonly byte[] \u2001;

		// Token: 0x040001D9 RID: 473
		private static readonly byte[] \u2002;

		// Token: 0x040001DA RID: 474
		private static readonly byte[] \u2003;

		// Token: 0x040001DB RID: 475
		private static readonly byte[] \u2004;

		// Token: 0x040001DC RID: 476
		private static readonly byte[] \u2005;

		// Token: 0x040001DD RID: 477
		private static readonly byte[] \u2006;

		// Token: 0x040001DE RID: 478
		private static readonly byte[] \u2007;

		// Token: 0x040001DF RID: 479
		private static readonly byte[] \u2008;

		// Token: 0x040001E0 RID: 480
		private static readonly byte[] \u2009;

		// Token: 0x040001E1 RID: 481
		private static IntPtr \u00A0;

		// Token: 0x040001E2 RID: 482
		private static byte[] \u200A;

		// Token: 0x040001E3 RID: 483
		private static int \u2000;

		// Token: 0x040001E4 RID: 484
		private static int \u2001;

		// Token: 0x040001E5 RID: 485
		private readonly object \u2001 = new object();

		// Token: 0x040001E6 RID: 486
		private static int \u2002;

		// Token: 0x040001E7 RID: 487
		private static bool \u2001;

		// Token: 0x040001E8 RID: 488
		private static bool \u2002;

		// Token: 0x040001E9 RID: 489
		private static bool \u2003;

		// Token: 0x040001EA RID: 490
		private static bool \u2004;

		// Token: 0x040001EB RID: 491
		private static bool \u2005;

		// Token: 0x040001EC RID: 492
		private static bool \u2006;

		// Token: 0x040001ED RID: 493
		private volatile bool \u00A0;

		// Token: 0x040001EE RID: 494
		private static bool \u2007;

		// Token: 0x040001F0 RID: 496
		private Label \u2004;

		// Token: 0x040001F1 RID: 497
		private Label \u2005;

		// Token: 0x040001F2 RID: 498
		private static readonly IntPtr \u1680 = new IntPtr(1);

		// Token: 0x040001F3 RID: 499
		private Label \u2006;

		// Token: 0x040001F4 RID: 500
		private TextBox \u00A0;

		// Token: 0x040001F5 RID: 501
		private TextBox \u1680;

		// Token: 0x040001F6 RID: 502
		private Button \u2003;

		// Token: 0x040001F7 RID: 503
		private \u2063 \u00A0;

		// Token: 0x040001F8 RID: 504
		private Label \u2007;

		// Token: 0x040001F9 RID: 505
		private bool \u2008;

		// Token: 0x040001FA RID: 506
		private Label \u2008;

		// Token: 0x040001FB RID: 507
		private bool \u2009;

		// Token: 0x040001FC RID: 508
		private TextBox \u2000;

		// Token: 0x040001FD RID: 509
		private volatile bool \u1680;

		// Token: 0x040001FE RID: 510
		private \u206A \u00A0;

		// Token: 0x040001FF RID: 511
		private volatile byte \u00A0 = 65;

		// Token: 0x04000200 RID: 512
		private volatile byte \u1680 = 114;

		// Token: 0x04000201 RID: 513
		private PictureBox \u1680;

		// Token: 0x04000202 RID: 514
		private Label \u2009;

		// Token: 0x04000203 RID: 515
		private volatile int \u00A0;

		// Token: 0x04000204 RID: 516
		private Button \u2004;

		// Token: 0x04000205 RID: 517
		private const int \u2003 = 2;

		// Token: 0x04000206 RID: 518
		private Label \u200A;

		// Token: 0x04000207 RID: 519
		private volatile bool \u2000;

		// Token: 0x04000208 RID: 520
		private int \u2004;

		// Token: 0x04000209 RID: 521
		private PictureBox \u2000;

		// Token: 0x0400020A RID: 522
		private Label \u200B;

		// Token: 0x0400020B RID: 523
		private \u205A.\u2008 \u00A0;

		// Token: 0x0400020C RID: 524
		private PictureBox \u2001;

		// Token: 0x0400020D RID: 525
		private Label \u2010;

		// Token: 0x0400020E RID: 526
		private Label \u2011;

		// Token: 0x0400020F RID: 527
		private Label \u2012;

		// Token: 0x04000210 RID: 528
		private Label \u2013;

		// Token: 0x04000211 RID: 529
		private ToolStripMenuItem \u2032;

		// Token: 0x04000212 RID: 530
		private ToolStripMenuItem \u2035;

		// Token: 0x04000213 RID: 531
		private ToolStripMenuItem \u2033;

		// Token: 0x04000214 RID: 532
		private ToolStripMenuItem \u2036;

		// Token: 0x04000215 RID: 533
		private ToolStripMenuItem \u203E;

		// Token: 0x04000216 RID: 534
		private Button \u2005;

		// Token: 0x04000217 RID: 535
		private Button \u2006;

		// Token: 0x04000218 RID: 536
		private PictureBox \u2002;

		// Token: 0x04000219 RID: 537
		private ToolStripMenuItem \u2047;

		// Token: 0x0400021A RID: 538
		private ToolStripMenuItem \u2048;

		// Token: 0x0400021B RID: 539
		private ToolStripMenuItem \u2049;

		// Token: 0x0400021C RID: 540
		private ToolStripMenuItem \u204A;

		// Token: 0x0400021D RID: 541
		private ToolStripMenuItem \u204B;

		// Token: 0x0400021E RID: 542
		private ToolStripMenuItem \u204C;

		// Token: 0x0400021F RID: 543
		private ToolStripMenuItem \u204D;

		// Token: 0x04000220 RID: 544
		private ToolStripMenuItem \u204E;

		// Token: 0x04000221 RID: 545
		private ToolStripMenuItem \u204F;

		// Token: 0x04000222 RID: 546
		private ToolStripMenuItem \u2050;

		// Token: 0x04000223 RID: 547
		private ToolStripMenuItem \u2051;

		// Token: 0x04000224 RID: 548
		private ToolStripMenuItem \u2052;

		// Token: 0x04000225 RID: 549
		private ToolStripMenuItem \u2053;

		// Token: 0x04000226 RID: 550
		private ToolStripMenuItem \u2054;

		// Token: 0x04000227 RID: 551
		private ToolStripMenuItem \u2055;

		// Token: 0x04000228 RID: 552
		private ToolStripMenuItem \u2056;

		// Token: 0x04000229 RID: 553
		private ToolStripMenuItem \u2057;

		// Token: 0x0400022A RID: 554
		private ToolStripMenuItem \u2058;

		// Token: 0x0400022B RID: 555
		private ToolStripMenuItem \u2059;

		// Token: 0x0400022C RID: 556
		private ToolStripMenuItem \u205A;

		// Token: 0x0400022D RID: 557
		private GroupBox \u1680;

		// Token: 0x0400022E RID: 558
		private GroupBox \u2000;

		// Token: 0x0400022F RID: 559
		private Button \u2007;

		// Token: 0x04000230 RID: 560
		private \u2051 \u00A0;

		// Token: 0x04000231 RID: 561
		private Button \u2008;

		// Token: 0x04000232 RID: 562
		private \u2051 \u1680;

		// Token: 0x04000233 RID: 563
		private \u2051 \u2000;

		// Token: 0x04000234 RID: 564
		private Button \u2009;

		// Token: 0x04000235 RID: 565
		private PictureBox \u2003;

		// Token: 0x04000236 RID: 566
		private Label \u2014;

		// Token: 0x04000237 RID: 567
		private \u206A \u1680;

		// Token: 0x04000238 RID: 568
		private Button \u200A;

		// Token: 0x04000239 RID: 569
		private System.Windows.Forms.Timer \u00A0;

		// Token: 0x0400023A RID: 570
		[CompilerGenerated]
		private object \u2002;

		// Token: 0x0400023B RID: 571
		[CompilerGenerated]
		private object \u2003;

		// Token: 0x020000B6 RID: 182
		// (Invoke) Token: 0x06000313 RID: 787
		private delegate void \u00A0(Control, string);

		// Token: 0x020000B7 RID: 183
		// (Invoke) Token: 0x06000317 RID: 791
		private delegate void \u1680(ComboBox, int);

		// Token: 0x020000B8 RID: 184
		// (Invoke) Token: 0x0600031B RID: 795
		private delegate void \u2000(ComboBox, bool);

		// Token: 0x020000B9 RID: 185
		// (Invoke) Token: 0x0600031F RID: 799
		private delegate void \u2001(ProgressBar, int);

		// Token: 0x020000BA RID: 186
		internal struct Type_6
		{
			// Token: 0x0400023C RID: 572
			public \u205A.\u2003 \u00A0;

			// Token: 0x0400023D RID: 573
			public IntPtr \u00A0;

			// Token: 0x0400023E RID: 574
			public int \u00A0;
		}

		// Token: 0x020000BB RID: 187
		internal enum Type_7
		{
			// Token: 0x04000240 RID: 576
			\u00A0 = 19
		}

		// Token: 0x020000BC RID: 188
		internal enum Form_8
		{
			// Token: 0x04000242 RID: 578
			\u00A0,
			// Token: 0x04000243 RID: 579
			\u1680,
			// Token: 0x04000244 RID: 580
			\u2000,
			// Token: 0x04000245 RID: 581
			\u2001,
			// Token: 0x04000246 RID: 582
			\u2002,
			// Token: 0x04000247 RID: 583
			\u2003
		}

		// Token: 0x020000BD RID: 189
		internal struct Form_9
		{
			// Token: 0x04000248 RID: 584
			public \u205A.\u2004 \u00A0;

			// Token: 0x04000249 RID: 585
			public int \u00A0;

			// Token: 0x0400024A RID: 586
			public int \u1680;

			// Token: 0x0400024B RID: 587
			public int \u2000;
		}

		// Token: 0x020000BE RID: 190
		private enum Form_A
		{
			// Token: 0x0400024D RID: 589
			\u00A0,
			// Token: 0x0400024E RID: 590
			\u1680,
			// Token: 0x0400024F RID: 591
			\u2000,
			// Token: 0x04000250 RID: 592
			\u2001,
			// Token: 0x04000251 RID: 593
			\u2002,
			// Token: 0x04000252 RID: 594
			\u2003
		}

		// Token: 0x020000BF RID: 191
		private enum Type_14
		{
			// Token: 0x04000254 RID: 596
			\u00A0,
			// Token: 0x04000255 RID: 597
			\u1680
		}

		// Token: 0x020000C0 RID: 192
		private enum Type_15
		{
			// Token: 0x04000257 RID: 599
			\u00A0,
			// Token: 0x04000258 RID: 600
			\u1680,
			// Token: 0x04000259 RID: 601
			\u2000
		}

		// Token: 0x020000C1 RID: 193
		public class Type_16
		{
			// Token: 0x0400025A RID: 602
			public float \u00A0;

			// Token: 0x0400025B RID: 603
			public float \u1680;

			// Token: 0x0400025C RID: 604
			public float \u2000;

			// Token: 0x0400025D RID: 605
			public float \u2001;

			// Token: 0x0400025E RID: 606
			public float \u2002;

			// Token: 0x0400025F RID: 607
			public int \u00A0;
		}

		// Token: 0x020000C2 RID: 194
		public class Struct_17 : ProfessionalColorTable
		{
			// Token: 0x06000323 RID: 803 RVA: 0x00020E0F File Offset: 0x0001F00F
			public override Color get_MenuItemSelected()
			{
				return Color.FromArgb(255, 60, 60);
			}

			// Token: 0x06000324 RID: 804 RVA: 0x00020E0F File Offset: 0x0001F00F
			public override Color get_MenuItemSelectedGradientBegin()
			{
				return Color.FromArgb(255, 60, 60);
			}

			// Token: 0x06000325 RID: 805 RVA: 0x00020E0F File Offset: 0x0001F00F
			public override Color get_MenuItemSelectedGradientEnd()
			{
				return Color.FromArgb(255, 60, 60);
			}

			// Token: 0x06000326 RID: 806 RVA: 0x00020E1F File Offset: 0x0001F01F
			public override Color get_MenuItemPressedGradientBegin()
			{
				return Color.FromArgb(180, 0, 0);
			}

			// Token: 0x06000327 RID: 807 RVA: 0x00020E1F File Offset: 0x0001F01F
			public override Color get_MenuItemPressedGradientEnd()
			{
				return Color.FromArgb(180, 0, 0);
			}

			// Token: 0x06000328 RID: 808 RVA: 0x00020E2D File Offset: 0x0001F02D
			public override Color get_MenuItemBorder()
			{
				return Color.Transparent;
			}

			// Token: 0x06000329 RID: 809 RVA: 0x00020E2D File Offset: 0x0001F02D
			public override Color get_MenuBorder()
			{
				return Color.Transparent;
			}

			// Token: 0x0600032A RID: 810 RVA: 0x00020E34 File Offset: 0x0001F034
			public override Color get_ToolStripDropDownBackground()
			{
				return Color.FromArgb(30, 30, 30);
			}

			// Token: 0x0600032B RID: 811 RVA: 0x00020E34 File Offset: 0x0001F034
			public override Color get_ImageMarginGradientBegin()
			{
				return Color.FromArgb(30, 30, 30);
			}

			// Token: 0x0600032C RID: 812 RVA: 0x00020E34 File Offset: 0x0001F034
			public override Color get_ImageMarginGradientMiddle()
			{
				return Color.FromArgb(30, 30, 30);
			}

			// Token: 0x0600032D RID: 813 RVA: 0x00020E34 File Offset: 0x0001F034
			public override Color get_ImageMarginGradientEnd()
			{
				return Color.FromArgb(30, 30, 30);
			}

			// Token: 0x04000260 RID: 608
			private Color \u00A0 = Color.FromArgb(215, 15, 15);
		}

		// Token: 0x020000C3 RID: 195
		public class Struct_18 : ToolStripProfessionalRenderer
		{
			// Token: 0x0600032F RID: 815 RVA: 0x00020E5D File Offset: 0x0001F05D
			public \u200B() : base(new \u205A.\u200A())
			{
			}

			// Token: 0x06000330 RID: 816 RVA: 0x0000208B File Offset: 0x0000028B
			protected override void OnRenderToolStripBorder(ToolStripRenderEventArgs A_1)
			{
			}
		}

		// Token: 0x020000C4 RID: 196
		public class Type_26 : ProfessionalColorTable
		{
			// Token: 0x06000331 RID: 817 RVA: 0x00020E6A File Offset: 0x0001F06A
			public override Color get_MenuItemSelected()
			{
				return Color.FromArgb(45, 45, 45);
			}

			// Token: 0x06000332 RID: 818 RVA: 0x00020E6A File Offset: 0x0001F06A
			public override Color get_MenuItemSelectedGradientBegin()
			{
				return Color.FromArgb(45, 45, 45);
			}

			// Token: 0x06000333 RID: 819 RVA: 0x00020E6A File Offset: 0x0001F06A
			public override Color get_MenuItemSelectedGradientEnd()
			{
				return Color.FromArgb(45, 45, 45);
			}

			// Token: 0x06000334 RID: 820 RVA: 0x00020E77 File Offset: 0x0001F077
			public override Color get_MenuItemBorder()
			{
				return Color.FromArgb(70, 70, 70);
			}

			// Token: 0x06000335 RID: 821 RVA: 0x00020E84 File Offset: 0x0001F084
			public override Color get_MenuItemPressedGradientBegin()
			{
				return Color.FromArgb(35, 35, 35);
			}

			// Token: 0x06000336 RID: 822 RVA: 0x00020E84 File Offset: 0x0001F084
			public override Color get_MenuItemPressedGradientEnd()
			{
				return Color.FromArgb(35, 35, 35);
			}

			// Token: 0x06000337 RID: 823 RVA: 0x00020E91 File Offset: 0x0001F091
			public override Color get_ToolStripDropDownBackground()
			{
				return Color.FromArgb(20, 20, 20);
			}

			// Token: 0x06000338 RID: 824 RVA: 0x00020E9E File Offset: 0x0001F09E
			public override Color get_MenuBorder()
			{
				return Color.FromArgb(60, 60, 60);
			}

			// Token: 0x06000339 RID: 825 RVA: 0x00020E91 File Offset: 0x0001F091
			public override Color get_ImageMarginGradientBegin()
			{
				return Color.FromArgb(20, 20, 20);
			}

			// Token: 0x0600033A RID: 826 RVA: 0x00020E91 File Offset: 0x0001F091
			public override Color get_ImageMarginGradientMiddle()
			{
				return Color.FromArgb(20, 20, 20);
			}

			// Token: 0x0600033B RID: 827 RVA: 0x00020E91 File Offset: 0x0001F091
			public override Color get_ImageMarginGradientEnd()
			{
				return Color.FromArgb(20, 20, 20);
			}
		}

		// Token: 0x020000C5 RID: 197
		public class Type_27 : ToolStripProfessionalRenderer
		{
			// Token: 0x0600033D RID: 829 RVA: 0x00020EB3 File Offset: 0x0001F0B3
			public \u2011() : base(new \u205A.\u2010())
			{
			}

			// Token: 0x0600033E RID: 830 RVA: 0x00020EC0 File Offset: 0x0001F0C0
			protected override void OnRenderItemText(ToolStripItemTextRenderEventArgs A_1)
			{
				A_1.Item.ForeColor = Color.LightGray;
				if (A_1.Item.Selected || A_1.Item.Pressed)
				{
					A_1.Item.ForeColor = Color.White;
				}
				base.OnRenderItemText(A_1);
			}
		}

		// Token: 0x020000C7 RID: 199
		[CompilerGenerated]
		[Serializable]
		private sealed class Type_29
		{
			// Token: 0x06000343 RID: 835 RVA: 0x0002101E File Offset: 0x0001F21E
			internal void \u00A0()
			{
				Console.Beep(400, 300);
				Thread.Sleep(50);
				Console.Beep(400, 300);
				MessageBox.Show("การเขียน ไฟล์ เสร็จสิ้น\r\nผล = ล้มเหลว!!\r\nโปรด ปิดเปิดกุญแจ ใหม่อีกครั้ง", "การเขียนไฟล์เสร็จสิ้น", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1, (MessageBoxOptions)262144);
			}

			// Token: 0x06000344 RID: 836 RVA: 0x0002105E File Offset: 0x0001F25E
			internal void \u1680()
			{
				RuntimeHelpers.InitializeArray(new int[4], fieldof(global::Form_4.Type_36.\u00A0).FieldHandle);
			}

			// Token: 0x06000345 RID: 837 RVA: 0x00021074 File Offset: 0x0001F274
			internal void \u2000()
			{
				global::Attr_3.Type_58.\u00A0("สำเร็จ - MZATUNER", "อัดไฟล์สำเร็จ!! ปิดเปิดกุญแจใหม่อีกครั้ง", global::Attr_3.Type_57.\u00A0);
				Task.Run(new Action(global::Attr_3.Type_50.Type_29.Attr_2.\u2001));
				global::Form_4.Attr_5.\u00A0("Write operation complete. Please cycle the ignition key.");
			}

			// Token: 0x06000346 RID: 838 RVA: 0x000210C0 File Offset: 0x0001F2C0
			internal void \u2001()
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

			// Token: 0x06000347 RID: 839 RVA: 0x00021127 File Offset: 0x0001F327
			internal void \u2002()
			{
				Console.Beep(500, 200);
				Thread.Sleep(50);
				Console.Beep(500, 200);
				MessageBox.Show("ลบ ล้มเหลว, ECM บล็อค", "ฮอนด้า แฟลช", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1, (MessageBoxOptions)262144);
			}

			// Token: 0x06000348 RID: 840 RVA: 0x00021167 File Offset: 0x0001F367
			internal void \u2003()
			{
				Console.Beep(400, 300);
				Thread.Sleep(50);
				Console.Beep(400, 300);
				MessageBox.Show("การเชื่อมต่อล้มเหลว กรุณาลองใหม่อีกครั้ง", "ฮอนด้า แฟลช", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1, (MessageBoxOptions)262144);
			}

			// Token: 0x06000349 RID: 841 RVA: 0x000211A7 File Offset: 0x0001F3A7
			internal void \u2004()
			{
				Console.Beep(900, 100);
			}

			// Token: 0x0600034A RID: 842 RVA: 0x000211B5 File Offset: 0x0001F3B5
			internal void \u2005()
			{
				Console.Beep(1000, 150);
			}

			// Token: 0x0600034B RID: 843 RVA: 0x000211B5 File Offset: 0x0001F3B5
			internal void \u2006()
			{
				Console.Beep(1000, 150);
			}

			// Token: 0x0600034C RID: 844 RVA: 0x000211B5 File Offset: 0x0001F3B5
			internal void \u2007()
			{
				Console.Beep(1000, 150);
			}

			// Token: 0x0600034D RID: 845 RVA: 0x000211C6 File Offset: 0x0001F3C6
			internal bool \u00A0(string A_1)
			{
				return A_1.EndsWith("S.exe");
			}

			// Token: 0x0600034E RID: 846 RVA: 0x000211B5 File Offset: 0x0001F3B5
			internal void \u2008()
			{
				Console.Beep(1000, 150);
			}

			// Token: 0x0600034F RID: 847 RVA: 0x000211B5 File Offset: 0x0001F3B5
			internal void \u2009()
			{
				Console.Beep(1000, 150);
			}

			// Token: 0x06000350 RID: 848 RVA: 0x000211D3 File Offset: 0x0001F3D3
			internal bool \u1680(string A_1)
			{
				return A_1.EndsWith("MZATUNER.tpk");
			}

			// Token: 0x06000351 RID: 849 RVA: 0x000211B5 File Offset: 0x0001F3B5
			internal void \u200A()
			{
				Console.Beep(1000, 150);
			}

			// Token: 0x06000352 RID: 850 RVA: 0x000211B5 File Offset: 0x0001F3B5
			internal void \u200B()
			{
				Console.Beep(1000, 150);
			}

			// Token: 0x06000353 RID: 851 RVA: 0x000211B5 File Offset: 0x0001F3B5
			internal void \u2010()
			{
				Console.Beep(1000, 150);
			}

			// Token: 0x06000354 RID: 852 RVA: 0x000211E0 File Offset: 0x0001F3E0
			internal bool \u2000(string A_1)
			{
				return A_1.EndsWith("lockdeta.exe");
			}

			// Token: 0x06000355 RID: 853 RVA: 0x000211B5 File Offset: 0x0001F3B5
			internal void \u2011()
			{
				Console.Beep(1000, 150);
			}

			// Token: 0x06000356 RID: 854 RVA: 0x000211ED File Offset: 0x0001F3ED
			internal bool \u2001(string A_1)
			{
				return A_1.EndsWith("WindowsFormsApp2.exe");
			}

			// Token: 0x06000357 RID: 855 RVA: 0x000211B5 File Offset: 0x0001F3B5
			internal void \u2012()
			{
				Console.Beep(1000, 150);
			}

			// Token: 0x06000358 RID: 856 RVA: 0x000211B5 File Offset: 0x0001F3B5
			internal void \u2013()
			{
				Console.Beep(1000, 150);
			}

			// Token: 0x06000359 RID: 857 RVA: 0x000211FA File Offset: 0x0001F3FA
			internal void \u2014()
			{
				Console.Beep(800, 100);
			}

			// Token: 0x0600035A RID: 858 RVA: 0x0000208B File Offset: 0x0000028B
			internal void \u2015()
			{
			}

			// Token: 0x0600035B RID: 859 RVA: 0x0000208B File Offset: 0x0000028B
			internal void \u2022()
			{
			}

			// Token: 0x0600035C RID: 860 RVA: 0x000211B5 File Offset: 0x0001F3B5
			internal void \u2024()
			{
				Console.Beep(1000, 150);
			}

			// Token: 0x0600035D RID: 861 RVA: 0x000211B5 File Offset: 0x0001F3B5
			internal void \u2025()
			{
				Console.Beep(1000, 150);
			}

			// Token: 0x0600035E RID: 862 RVA: 0x000211B5 File Offset: 0x0001F3B5
			internal void \u2027()
			{
				Console.Beep(1000, 150);
			}

			// Token: 0x0600035F RID: 863 RVA: 0x000211B5 File Offset: 0x0001F3B5
			internal void \u2028()
			{
				Console.Beep(1000, 150);
			}

			// Token: 0x06000360 RID: 864 RVA: 0x000211B5 File Offset: 0x0001F3B5
			internal void \u2029()
			{
				Console.Beep(1000, 150);
			}

			// Token: 0x06000361 RID: 865 RVA: 0x000211B5 File Offset: 0x0001F3B5
			internal void \u202A()
			{
				Console.Beep(1000, 150);
			}

			// Token: 0x06000362 RID: 866 RVA: 0x000211B5 File Offset: 0x0001F3B5
			internal void \u202B()
			{
				Console.Beep(1000, 150);
			}

			// Token: 0x06000363 RID: 867 RVA: 0x000211B5 File Offset: 0x0001F3B5
			internal void \u202C()
			{
				Console.Beep(1000, 150);
			}

			// Token: 0x06000364 RID: 868 RVA: 0x000211B5 File Offset: 0x0001F3B5
			internal void \u202D()
			{
				Console.Beep(1000, 150);
			}

			// Token: 0x06000365 RID: 869 RVA: 0x000211B5 File Offset: 0x0001F3B5
			internal void \u202E()
			{
				Console.Beep(1000, 150);
			}

			// Token: 0x06000366 RID: 870 RVA: 0x000211C6 File Offset: 0x0001F3C6
			internal bool \u2002(string A_1)
			{
				return A_1.EndsWith("S.exe");
			}

			// Token: 0x06000367 RID: 871 RVA: 0x00021208 File Offset: 0x0001F408
			internal void \u202F()
			{
				Console.Beep(1000, 100);
			}

			// Token: 0x06000368 RID: 872 RVA: 0x000211B5 File Offset: 0x0001F3B5
			internal void \u2032()
			{
				Console.Beep(1000, 150);
			}

			// Token: 0x06000369 RID: 873 RVA: 0x000211B5 File Offset: 0x0001F3B5
			internal void \u2035()
			{
				Console.Beep(1000, 150);
			}

			// Token: 0x0600036A RID: 874 RVA: 0x000211B5 File Offset: 0x0001F3B5
			internal void \u2033()
			{
				Console.Beep(1000, 150);
			}

			// Token: 0x0600036B RID: 875 RVA: 0x000211D3 File Offset: 0x0001F3D3
			internal bool \u2003(string A_1)
			{
				return A_1.EndsWith("MZATUNER.tpk");
			}

			// Token: 0x0600036C RID: 876 RVA: 0x000211B5 File Offset: 0x0001F3B5
			internal void \u2036()
			{
				Console.Beep(1000, 150);
			}

			// Token: 0x0600036D RID: 877 RVA: 0x000211B5 File Offset: 0x0001F3B5
			internal void \u203E()
			{
				Console.Beep(1000, 150);
			}

			// Token: 0x0600036E RID: 878 RVA: 0x000211B5 File Offset: 0x0001F3B5
			internal void \u2047()
			{
				Console.Beep(1000, 150);
			}

			// Token: 0x0600036F RID: 879 RVA: 0x000211B5 File Offset: 0x0001F3B5
			internal void \u2048()
			{
				Console.Beep(1000, 150);
			}

			// Token: 0x06000370 RID: 880 RVA: 0x000211B5 File Offset: 0x0001F3B5
			internal void \u2049()
			{
				Console.Beep(1000, 150);
			}

			// Token: 0x06000371 RID: 881 RVA: 0x000211B5 File Offset: 0x0001F3B5
			internal void \u204A()
			{
				Console.Beep(1000, 150);
			}

			// Token: 0x06000372 RID: 882 RVA: 0x000211B5 File Offset: 0x0001F3B5
			internal void \u204B()
			{
				Console.Beep(1000, 150);
			}

			// Token: 0x06000373 RID: 883 RVA: 0x000211B5 File Offset: 0x0001F3B5
			internal void \u204C()
			{
				Console.Beep(1000, 150);
			}

			// Token: 0x06000374 RID: 884 RVA: 0x000211B5 File Offset: 0x0001F3B5
			internal void \u204D()
			{
				Console.Beep(1000, 150);
			}

			// Token: 0x06000375 RID: 885 RVA: 0x000211C6 File Offset: 0x0001F3C6
			internal bool \u2004(string A_1)
			{
				return A_1.EndsWith("S.exe");
			}

			// Token: 0x06000376 RID: 886 RVA: 0x000211B5 File Offset: 0x0001F3B5
			internal void \u204E()
			{
				Console.Beep(1000, 150);
			}

			// Token: 0x06000377 RID: 887 RVA: 0x000211B5 File Offset: 0x0001F3B5
			internal void \u204F()
			{
				Console.Beep(1000, 150);
			}

			// Token: 0x06000378 RID: 888 RVA: 0x000211E0 File Offset: 0x0001F3E0
			internal bool \u2005(string A_1)
			{
				return A_1.EndsWith("lockdeta.exe");
			}

			// Token: 0x06000379 RID: 889 RVA: 0x000211B5 File Offset: 0x0001F3B5
			internal void \u2050()
			{
				Console.Beep(1000, 150);
			}

			// Token: 0x0600037A RID: 890 RVA: 0x000211B5 File Offset: 0x0001F3B5
			internal void \u2051()
			{
				Console.Beep(1000, 150);
			}

			// Token: 0x0600037B RID: 891 RVA: 0x00021216 File Offset: 0x0001F416
			internal void \u2052()
			{
				Console.Beep(1200, 100);
			}

			// Token: 0x0600037C RID: 892 RVA: 0x00021224 File Offset: 0x0001F424
			internal void \u2053()
			{
				Console.Beep(1200, 80);
			}

			// Token: 0x0600037D RID: 893 RVA: 0x00021232 File Offset: 0x0001F432
			internal void \u00A0(object A_1, EventArgs A_2)
			{
				Application.Exit();
			}

			// Token: 0x0600037E RID: 894 RVA: 0x0002123C File Offset: 0x0001F43C
			internal void \u00A0(object A_1, PaintEventArgs A_2)
			{
				A_2.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
				Color backColor = ((Button)A_1).BackColor;
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
						A_2.Graphics.FillPath(pathGradientBrush, graphicsPath);
					}
				}
				using (LinearGradientBrush linearGradientBrush = new LinearGradientBrush(new Rectangle(2, 2, 10, 10), backColor, Color.FromArgb(50, Color.Black), 45f))
				{
					A_2.Graphics.FillEllipse(linearGradientBrush, 2, 2, 10, 10);
				}
				using (SolidBrush solidBrush = new SolidBrush(Color.FromArgb(180, Color.White)))
				{
					A_2.Graphics.FillEllipse(solidBrush, 4, 4, 3, 3);
				}
			}

			// Token: 0x0600037F RID: 895 RVA: 0x00021208 File Offset: 0x0001F408
			internal void \u2054()
			{
				Console.Beep(1000, 100);
			}

			// Token: 0x06000380 RID: 896 RVA: 0x00021370 File Offset: 0x0001F570
			internal void \u2055()
			{
				Console.Beep(1500, 100);
			}

			// Token: 0x06000381 RID: 897 RVA: 0x000211B5 File Offset: 0x0001F3B5
			internal void \u2056()
			{
				Console.Beep(1000, 150);
			}

			// Token: 0x06000382 RID: 898 RVA: 0x000211B5 File Offset: 0x0001F3B5
			internal void \u2057()
			{
				Console.Beep(1000, 150);
			}

			// Token: 0x06000383 RID: 899 RVA: 0x000211B5 File Offset: 0x0001F3B5
			internal void \u2058()
			{
				Console.Beep(1000, 150);
			}

			// Token: 0x04000265 RID: 613
			public static readonly \u205A.\u2013 \u00A0 = new \u205A.\u2013();

			// Token: 0x04000266 RID: 614
			public static Action \u00A0;

			// Token: 0x04000267 RID: 615
			public static Action \u1680;

			// Token: 0x04000268 RID: 616
			public static Action \u2000;

			// Token: 0x04000269 RID: 617
			public static Action \u2001;

			// Token: 0x0400026A RID: 618
			public static Action \u2002;

			// Token: 0x0400026B RID: 619
			public static Action \u2003;

			// Token: 0x0400026C RID: 620
			public static Action \u2004;

			// Token: 0x0400026D RID: 621
			public static Action \u2005;

			// Token: 0x0400026E RID: 622
			public static Action \u2006;

			// Token: 0x0400026F RID: 623
			public static Action \u2007;

			// Token: 0x04000270 RID: 624
			public static Func<string, bool> \u00A0;

			// Token: 0x04000271 RID: 625
			public static Action \u2008;

			// Token: 0x04000272 RID: 626
			public static Action \u2009;

			// Token: 0x04000273 RID: 627
			public static Func<string, bool> \u1680;

			// Token: 0x04000274 RID: 628
			public static Action \u200A;

			// Token: 0x04000275 RID: 629
			public static Action \u200B;

			// Token: 0x04000276 RID: 630
			public static Action \u2010;

			// Token: 0x04000277 RID: 631
			public static Func<string, bool> \u2000;

			// Token: 0x04000278 RID: 632
			public static Action \u2011;

			// Token: 0x04000279 RID: 633
			public static Func<string, bool> \u2001;

			// Token: 0x0400027A RID: 634
			public static Action \u2012;

			// Token: 0x0400027B RID: 635
			public static Action \u2013;

			// Token: 0x0400027C RID: 636
			public static Action \u2014;

			// Token: 0x0400027D RID: 637
			public static Action \u2015;

			// Token: 0x0400027E RID: 638
			public static Action \u2022;

			// Token: 0x0400027F RID: 639
			public static Action \u2024;

			// Token: 0x04000280 RID: 640
			public static Action \u2025;

			// Token: 0x04000281 RID: 641
			public static Action \u2027;

			// Token: 0x04000282 RID: 642
			public static Action \u2028;

			// Token: 0x04000283 RID: 643
			public static Action \u2029;

			// Token: 0x04000284 RID: 644
			public static Action \u202A;

			// Token: 0x04000285 RID: 645
			public static Action \u202B;

			// Token: 0x04000286 RID: 646
			public static Action \u202C;

			// Token: 0x04000287 RID: 647
			public static Action \u202D;

			// Token: 0x04000288 RID: 648
			public static Action \u202E;

			// Token: 0x04000289 RID: 649
			public static Func<string, bool> \u2002;

			// Token: 0x0400028A RID: 650
			public static Action \u202F;

			// Token: 0x0400028B RID: 651
			public static Action \u2032;

			// Token: 0x0400028C RID: 652
			public static Action \u2035;

			// Token: 0x0400028D RID: 653
			public static Action \u2033;

			// Token: 0x0400028E RID: 654
			public static Func<string, bool> \u2003;

			// Token: 0x0400028F RID: 655
			public static Action \u2036;

			// Token: 0x04000290 RID: 656
			public static Action \u203E;

			// Token: 0x04000291 RID: 657
			public static Action \u2047;

			// Token: 0x04000292 RID: 658
			public static Action \u2048;

			// Token: 0x04000293 RID: 659
			public static Action \u2049;

			// Token: 0x04000294 RID: 660
			public static Action \u204A;

			// Token: 0x04000295 RID: 661
			public static Action \u204B;

			// Token: 0x04000296 RID: 662
			public static Action \u204C;

			// Token: 0x04000297 RID: 663
			public static Action \u204D;

			// Token: 0x04000298 RID: 664
			public static Func<string, bool> \u2004;

			// Token: 0x04000299 RID: 665
			public static Action \u204E;

			// Token: 0x0400029A RID: 666
			public static Action \u204F;

			// Token: 0x0400029B RID: 667
			public static Func<string, bool> \u2005;

			// Token: 0x0400029C RID: 668
			public static Action \u2050;

			// Token: 0x0400029D RID: 669
			public static Action \u2051;

			// Token: 0x0400029E RID: 670
			public static Action \u2052;

			// Token: 0x0400029F RID: 671
			public static Action \u2053;

			// Token: 0x040002A0 RID: 672
			public static EventHandler \u00A0;

			// Token: 0x040002A1 RID: 673
			public static PaintEventHandler \u00A0;

			// Token: 0x040002A2 RID: 674
			public static Action \u2054;

			// Token: 0x040002A3 RID: 675
			public static Action \u2055;

			// Token: 0x040002A4 RID: 676
			public static Action \u2056;

			// Token: 0x040002A5 RID: 677
			public static Action \u2057;

			// Token: 0x040002A6 RID: 678
			public static Action \u2058;
		}

		// Token: 0x020000C8 RID: 200
		[CompilerGenerated]
		private sealed class Type_2A
		{
			// Token: 0x06000385 RID: 901 RVA: 0x00021380 File Offset: 0x0001F580
			internal Task \u00A0()
			{
				\u205A.Type_2A.\u00A0 u00A;
				u00A.\u00A0 = AsyncTaskMethodBuilder.Create();
				u00A.\u00A0 = this;
				u00A.\u00A0 = -1;
				u00A.Attr_2.Start<\u205A.Type_2A.\u00A0>(ref u00A);
				return u00A.Attr_2.Task;
			}

			// Token: 0x06000386 RID: 902 RVA: 0x000213C3 File Offset: 0x0001F5C3
			internal void \u1680()
			{
				this.Attr_2.Invalidate();
			}

			// Token: 0x06000387 RID: 903 RVA: 0x000213C3 File Offset: 0x0001F5C3
			internal void \u2000()
			{
				this.Attr_2.Invalidate();
			}

			// Token: 0x06000388 RID: 904 RVA: 0x000213C3 File Offset: 0x0001F5C3
			internal void \u2001()
			{
				this.Attr_2.Invalidate();
			}

			// Token: 0x040002A7 RID: 679
			public string[] \u00A0;

			// Token: 0x040002A8 RID: 680
			public \u205A \u00A0;

			// Token: 0x040002A9 RID: 681
			public Action \u00A0;

			// Token: 0x040002AA RID: 682
			public Action \u1680;

			// Token: 0x040002AB RID: 683
			public Action \u2000;

			// Token: 0x020000C9 RID: 201
			[StructLayout(LayoutKind.Auto)]
			private struct Attr_2 : IAsyncStateMachine
			{
				// Token: 0x06000389 RID: 905 RVA: 0x000213D0 File Offset: 0x0001F5D0
				void IAsyncStateMachine.MoveNext()
				{
					int num = this.\u00A0;
					\u205A.\u2014 u00A = this.\u00A0;
					try
					{
						TaskAwaiter u00A2;
						switch (num)
						{
						case 0:
							u00A2 = this.\u00A0;
							this.\u00A0 = default(TaskAwaiter);
							num = (this.\u00A0 = -1);
							break;
						case 1:
							u00A2 = this.\u00A0;
							this.\u00A0 = default(TaskAwaiter);
							num = (this.\u00A0 = -1);
							goto IL_1B7;
						case 2:
							u00A2 = this.\u00A0;
							this.\u00A0 = default(TaskAwaiter);
							num = (this.\u00A0 = -1);
							goto IL_2B9;
						case 3:
							u00A2 = this.\u00A0;
							this.\u00A0 = default(TaskAwaiter);
							num = (this.\u00A0 = -1);
							goto IL_388;
						default:
							this.\u00A0 = u00A.\u00A0;
							this.\u1680 = 0;
							goto IL_244;
						}
						IL_12D:
						u00A2.GetResult();
						int u = this.\u2000;
						this.\u2000 = u + 1;
						IL_146:
						object u2;
						bool flag;
						if (this.\u2000 > this.Attr_2.Length)
						{
							u00A2 = Task.Delay(200).GetAwaiter();
							if (!u00A2.IsCompleted)
							{
								num = (this.\u00A0 = 1);
								this.\u00A0 = u00A2;
								this.Attr_2.AwaitUnsafeOnCompleted<TaskAwaiter, \u205A.Type_2A.\u00A0>(ref u00A2, ref this);
								return;
							}
						}
						else
						{
							u2 = u00A.Attr_2.\u2000;
							flag = false;
							try
							{
								Monitor.Enter(u2, ref flag);
								u00A.Attr_2.\u2000 = this.Attr_2.Substring(0, this.\u2000) + "_";
							}
							finally
							{
								if (num < 0 && flag)
								{
									Monitor.Exit(u2);
								}
							}
							Control u00A3 = u00A.\u00A0;
							Action method;
							if ((method = u00A.\u00A0) == null)
							{
								method = (u00A.\u00A0 = new Action(u00A.\u1680));
							}
							u00A3.BeginInvoke(method);
							u00A2 = Task.Delay(10).GetAwaiter();
							if (!u00A2.IsCompleted)
							{
								num = (this.\u00A0 = 0);
								this.\u00A0 = u00A2;
								this.Attr_2.AwaitUnsafeOnCompleted<TaskAwaiter, \u205A.Type_2A.\u00A0>(ref u00A2, ref this);
								return;
							}
							goto IL_12D;
						}
						IL_1B7:
						u00A2.GetResult();
						u2 = u00A.Attr_2.\u2000;
						flag = false;
						try
						{
							Monitor.Enter(u2, ref flag);
							u00A.Attr_2.Attr_2.Add(this.\u00A0);
							if (u00A.Attr_2.Attr_2.Count > 10)
							{
								u00A.Attr_2.Attr_2.RemoveAt(0);
							}
							u00A.Attr_2.\u2000 = "";
						}
						finally
						{
							if (num < 0 && flag)
							{
								Monitor.Exit(u2);
							}
						}
						this.\u00A0 = null;
						this.\u1680++;
						IL_244:
						if (this.\u1680 < this.Attr_2.Length)
						{
							this.\u00A0 = this.\u00A0[this.\u1680];
							this.\u2000 = 0;
							goto IL_146;
						}
						this.\u00A0 = null;
						u00A2 = Task.Delay(1000).GetAwaiter();
						if (!u00A2.IsCompleted)
						{
							num = (this.\u00A0 = 2);
							this.\u00A0 = u00A2;
							this.Attr_2.AwaitUnsafeOnCompleted<TaskAwaiter, \u205A.Type_2A.\u00A0>(ref u00A2, ref this);
							return;
						}
						IL_2B9:
						u00A2.GetResult();
						goto IL_38F;
						IL_388:
						u00A2.GetResult();
						IL_38F:
						if (u00A.Attr_2.\u00A0 <= 0f)
						{
							u00A.Attr_2.\u2000 = false;
							Control u00A4 = u00A.\u00A0;
							Action method2;
							if ((method2 = u00A.\u2000) == null)
							{
								method2 = (u00A.\u2000 = new Action(u00A.\u2001));
							}
							u00A4.BeginInvoke(method2);
							global::Form_4.Attr_5.\u00A0("System Ready. Welcome Master.");
						}
						else
						{
							u00A.Attr_2.\u00A0 = u00A.Attr_2.\u00A0 - 15f;
							if (u00A.Attr_2.\u00A0 < 0f)
							{
								u00A.Attr_2.\u00A0 = 0f;
							}
							Control u00A5 = u00A.\u00A0;
							Action method3;
							if ((method3 = u00A.\u1680) == null)
							{
								method3 = (u00A.\u1680 = new Action(u00A.\u2000));
							}
							u00A5.BeginInvoke(method3);
							u00A2 = Task.Delay(30).GetAwaiter();
							if (!u00A2.IsCompleted)
							{
								num = (this.\u00A0 = 3);
								this.\u00A0 = u00A2;
								this.Attr_2.AwaitUnsafeOnCompleted<TaskAwaiter, \u205A.Type_2A.\u00A0>(ref u00A2, ref this);
								return;
							}
							goto IL_388;
						}
					}
					catch (Exception exception)
					{
						this.\u00A0 = -2;
						this.Attr_2.SetException(exception);
						return;
					}
					this.\u00A0 = -2;
					this.Attr_2.SetResult();
				}

				// Token: 0x0600038A RID: 906 RVA: 0x00021840 File Offset: 0x0001FA40
				[DebuggerHidden]
				void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine A_1)
				{
					this.Attr_2.SetStateMachine(A_1);
				}

				// Token: 0x040002AC RID: 684
				public int \u00A0;

				// Token: 0x040002AD RID: 685
				public AsyncTaskMethodBuilder \u00A0;

				// Token: 0x040002AE RID: 686
				public \u205A.\u2014 \u00A0;

				// Token: 0x040002AF RID: 687
				private string[] \u00A0;

				// Token: 0x040002B0 RID: 688
				private int \u1680;

				// Token: 0x040002B1 RID: 689
				private string \u00A0;

				// Token: 0x040002B2 RID: 690
				private int \u2000;

				// Token: 0x040002B3 RID: 691
				private TaskAwaiter \u00A0;
			}
		}

		// Token: 0x020000CA RID: 202
		[CompilerGenerated]
		private sealed class Type_2B
		{
			// Token: 0x0600038C RID: 908 RVA: 0x00021850 File Offset: 0x0001FA50
			internal void \u00A0()
			{
				Color foreColor = (this.\u00A0 >= 15.0) ? Color.Red : ((this.\u00A0 >= 13.6) ? Color.Lime : ((this.\u00A0 >= 12.4) ? Color.Lime : ((this.\u00A0 >= 11.0) ? Color.Yellow : Color.Red)));
				string text = (this.\u00A0 >= 15.0) ? "Over" : ((this.\u00A0 >= 13.6) ? "ปกติ" : ((this.\u00A0 >= 12.4) ? "ปกติ" : ((this.\u00A0 >= 11.0) ? "อ่อนนิดๆ" : "ต่ำมาก")));
				if (this.\u00A0 != null)
				{
					this.Attr_2.Text = string.Format(" V  {0:F1} ({1})", this.\u00A0, text);
					this.Attr_2.ForeColor = foreColor;
				}
				Label label = this.\u1680 as Label;
				if (label != null)
				{
					if (label.Name == "TxtBatteryVolt")
					{
						label.Text = string.Format("{0:F1} V", this.\u00A0);
					}
					else
					{
						label.Text = string.Format("{0:F1} V", this.\u00A0);
					}
					label.ForeColor = foreColor;
				}
				Label label2 = this.Attr_2.Controls.Find("label_batStatus", true).FirstOrDefault<Control>() as Label;
				if (label2 != null)
				{
					label2.Text = text;
					label2.ForeColor = foreColor;
				}
			}

			// Token: 0x040002B4 RID: 692
			public double \u00A0;

			// Token: 0x040002B5 RID: 693
			public Control \u00A0;

			// Token: 0x040002B6 RID: 694
			public Control \u1680;

			// Token: 0x040002B7 RID: 695
			public \u205A \u00A0;
		}

		// Token: 0x020000CB RID: 203
		[CompilerGenerated]
		private sealed class Type_2C
		{
			// Token: 0x0600038E RID: 910 RVA: 0x000219F0 File Offset: 0x0001FBF0
			internal void \u00A0()
			{
				MessageBox.Show(this.\u00A0, this.Attr_2.ToString(), "Worker crashed");
				this.Attr_2.\u1680(this.Attr_2.\u2009, "เกิดข้อผิดพลาดในเธรดสื่อสาร");
			}

			// Token: 0x040002B8 RID: 696
			public Exception \u00A0;

			// Token: 0x040002B9 RID: 697
			public \u205A \u00A0;
		}

		// Token: 0x020000CC RID: 204
		[CompilerGenerated]
		private sealed class Type_2D
		{
			// Token: 0x06000390 RID: 912 RVA: 0x00021A29 File Offset: 0x0001FC29
			internal void \u00A0()
			{
				this.Attr_2.Text = this.\u00A0;
			}

			// Token: 0x040002BA RID: 698
			public Control \u00A0;

			// Token: 0x040002BB RID: 699
			public string \u00A0;
		}

		// Token: 0x020000CD RID: 205
		[CompilerGenerated]
		private sealed class Type_2E
		{
			// Token: 0x06000392 RID: 914 RVA: 0x00021A3C File Offset: 0x0001FC3C
			internal void \u00A0()
			{
				this.\u00A0 = this.Attr_2.Text;
			}

			// Token: 0x040002BC RID: 700
			public Control \u00A0;

			// Token: 0x040002BD RID: 701
			public string \u00A0;
		}

		// Token: 0x020000CE RID: 206
		[CompilerGenerated]
		private sealed class Type_2F
		{
			// Token: 0x06000394 RID: 916 RVA: 0x00021A4F File Offset: 0x0001FC4F
			internal void \u00A0(object A_1, MouseEventArgs A_2)
			{
				if (A_2.Button == MouseButtons.Left)
				{
					this.Attr_2.\u00A0 = true;
					this.Attr_2.\u00A0 = A_2.Location;
				}
			}

			// Token: 0x06000395 RID: 917 RVA: 0x00021A7C File Offset: 0x0001FC7C
			internal void \u1680(object A_1, MouseEventArgs A_2)
			{
				if (this.Attr_2.\u00A0)
				{
					this.Attr_2.Location = new Point(this.Attr_2.Location.X - this.Attr_2.Attr_2.X + A_2.X, this.Attr_2.Location.Y - this.Attr_2.Attr_2.Y + A_2.Y);
				}
			}

			// Token: 0x06000396 RID: 918 RVA: 0x00021AFC File Offset: 0x0001FCFC
			internal void \u2000(object A_1, MouseEventArgs A_2)
			{
				this.Attr_2.\u00A0 = false;
			}

			// Token: 0x06000397 RID: 919 RVA: 0x00021B0A File Offset: 0x0001FD0A
			internal void \u00A0(object A_1, EventArgs A_2)
			{
				this.Attr_2.\u1680\u202A(A_1, A_2);
			}

			// Token: 0x06000398 RID: 920 RVA: 0x00021B1C File Offset: 0x0001FD1C
			internal void \u1680(object A_1, EventArgs A_2)
			{
				this.Attr_2.Left--;
				if (this.Attr_2.Right < 0)
				{
					this.Attr_2.Left = this.Attr_2.Attr_5.Width;
				}
				if (this.\u00A0)
				{
					this.\u00A0 += 4;
					if (this.\u00A0 >= 255)
					{
						this.\u00A0 = 255;
						this.\u00A0 = false;
					}
				}
				else
				{
					this.\u00A0 -= 4;
					if (this.\u00A0 <= 100)
					{
						this.\u00A0 = 100;
						this.\u00A0 = true;
					}
				}
				this.Attr_2.ForeColor = Color.FromArgb(this.\u00A0, this.\u00A0, (int)((double)this.\u00A0 * 0.8));
			}

			// Token: 0x040002BE RID: 702
			public \u205A \u00A0;

			// Token: 0x040002BF RID: 703
			public Label \u00A0;

			// Token: 0x040002C0 RID: 704
			public bool \u00A0;

			// Token: 0x040002C1 RID: 705
			public int \u00A0;
		}

		// Token: 0x020000CF RID: 207
		[CompilerGenerated]
		private sealed class Type_30
		{
			// Token: 0x0600039A RID: 922 RVA: 0x00021BF1 File Offset: 0x0001FDF1
			internal void \u00A0()
			{
				this.Attr_2.Enabled = this.\u00A0;
			}

			// Token: 0x040002C2 RID: 706
			public Control \u00A0;

			// Token: 0x040002C3 RID: 707
			public bool \u00A0;
		}

		// Token: 0x020000D0 RID: 208
		[CompilerGenerated]
		private sealed class Type_31
		{
			// Token: 0x0600039C RID: 924 RVA: 0x00021C04 File Offset: 0x0001FE04
			internal void \u00A0()
			{
				global::Attr_3.Type_50.\u00A0(this.\u00A0);
				global::Attr_3.Type_50.\u00A0(this.\u1680);
				byte b;
				bool flag = global::Attr_3.Type_50.\u00A0(this.Attr_2.Text, out b);
				byte b2;
				bool flag2 = global::Attr_3.Type_50.\u00A0(this.Attr_3.Text, out b2);
				this.Attr_2.Enabled = (flag && flag2);
				this.Attr_2.BackColor = ((flag && flag2) ? Color.FromArgb(180, 0, 0) : Color.FromArgb(60, 20, 20));
				this.Attr_2.FlatAppearance.BorderColor = ((flag && flag2) ? Color.Red : Color.DarkRed);
				if (flag && flag2)
				{
					this.Attr_2.Text = string.Format("DEC: {0} {1}   HEX: {2:X2} {3:X2}   ASCII: '{4}{5}'", new object[]
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
				this.Attr_2.Text = "STATUS: รอข้อมูลรหัสผ่าน (Hex 2 หลัก)";
			}

			// Token: 0x0600039D RID: 925 RVA: 0x00021D0C File Offset: 0x0001FF0C
			internal void \u00A0(object A_1, EventArgs A_2)
			{
				this.\u00A0();
			}

			// Token: 0x0600039E RID: 926 RVA: 0x00021D0C File Offset: 0x0001FF0C
			internal void \u1680(object A_1, EventArgs A_2)
			{
				this.\u00A0();
			}

			// Token: 0x040002C4 RID: 708
			public TextBox \u00A0;

			// Token: 0x040002C5 RID: 709
			public TextBox \u1680;

			// Token: 0x040002C6 RID: 710
			public Button \u00A0;

			// Token: 0x040002C7 RID: 711
			public Label \u00A0;
		}

		// Token: 0x020000D1 RID: 209
		[CompilerGenerated]
		private sealed class Type_32
		{
			// Token: 0x060003A0 RID: 928 RVA: 0x00021D14 File Offset: 0x0001FF14
			internal void \u00A0(object A_1, EventArgs A_2)
			{
				string str = Regex.Replace(this.Attr_2.Text, "[^\\u0000-\\u007F\\u0E00-\\u0E7F]+", "").Trim();
				global::Attr_3.Type_58.\u00A0("MZA-TUNER", "กำลังเรียกใช้งาน: " + str, global::Attr_3.Type_57.\u1680);
			}

			// Token: 0x040002C8 RID: 712
			public ToolStripMenuItem \u00A0;
		}
	}
}
