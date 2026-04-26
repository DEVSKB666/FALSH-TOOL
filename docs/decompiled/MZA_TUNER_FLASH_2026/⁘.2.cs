using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using <PrivateImplementationDetails>{68F2EF73-9355-4257-ADA6-397CF8BB8E72};

namespace Attr_3
{
	// Token: 0x020000AF RID: 175
	public partial class Type_4E : Form
	{
		// Token: 0x0600020B RID: 523
		[DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
		private static extern IntPtr \u00A0(int, int, int, int, int, int);

		// Token: 0x0600020C RID: 524 RVA: 0x000130BC File Offset: 0x000112BC
		public Type_4E()
		{
			base.Size = new Size(500, 450);
			base.FormBorderStyle = FormBorderStyle.None;
			base.StartPosition = FormStartPosition.CenterParent;
			this.BackColor = Color.FromArgb(15, 15, 15);
			this.DoubleBuffered = true;
			base.TopMost = true;
			try
			{
				IntPtr intPtr = \u2058.\u00A0(0, 0, base.Width, base.Height, 30, 30);
				if (intPtr != IntPtr.Zero)
				{
					base.Region = Region.FromHrgn(intPtr);
				}
			}
			catch
			{
			}
			this.\u00A0 = new Font("Bahnschrift SemiBold", 14f, FontStyle.Bold);
			this.\u1680 = new Font("Leelawadee UI", 9f, FontStyle.Bold);
			this.\u2000 = new Font("Consolas", 18f, FontStyle.Bold);
			this.\u00A0();
		}

		// Token: 0x0600020D RID: 525 RVA: 0x000131D4 File Offset: 0x000113D4
		private void \u00A0()
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
			button.Click += this.\u00A0;
			base.Controls.Add(button);
			this.\u00A0("TARGET HP", 100, 80, new Action<float>(this.\u00A0));
			this.\u00A0("CYLINDERS", 100, 140, new Action<float>(this.\u1680));
			this.\u00A0("FUEL", 300, 80, new string[]
			{
				"Gasoline",
				"E85"
			}, new Action<string>(this.\u00A0));
			this.\u00A0("ASPI", 300, 140, new string[]
			{
				"NA",
				"Turbo"
			}, new Action<string>(this.\u1680));
		}

		// Token: 0x0600020E RID: 526 RVA: 0x00013318 File Offset: 0x00011518
		private void \u00A0(string A_1, int A_2, int A_3, Action<float> A_4)
		{
			\u2058.\u00A0 u00A = new \u2058.\u00A0();
			u00A.\u00A0 = A_4;
			u00A.\u00A0 = new TextBox
			{
				Location = new Point(A_2, A_3 + 20),
				Size = new Size(120, 30),
				BackColor = Color.FromArgb(30, 30, 30),
				ForeColor = Color.White,
				BorderStyle = BorderStyle.FixedSingle,
				Font = this.\u2000,
				Text = ((A_1 == "CYLINDERS") ? "4" : "300")
			};
			u00A.Attr_2.TextChanged += u00A.\u00A0;
			Label value = new Label
			{
				Text = A_1,
				Location = new Point(A_2, A_3),
				ForeColor = Color.Gray,
				Font = this.\u1680,
				AutoSize = true
			};
			base.Controls.Add(u00A.\u00A0);
			base.Controls.Add(value);
		}

		// Token: 0x0600020F RID: 527 RVA: 0x00013418 File Offset: 0x00011618
		private void \u00A0(string A_1, int A_2, int A_3, string[] A_4, Action<string> A_5)
		{
			\u2058.\u1680 u = new \u2058.\u1680();
			u.\u00A0 = A_5;
			u.\u00A0 = new ComboBox
			{
				Location = new Point(A_2, A_3 + 20),
				Size = new Size(120, 30),
				BackColor = Color.FromArgb(30, 30, 30),
				ForeColor = Color.White,
				FlatStyle = FlatStyle.Flat,
				Font = this.\u1680,
				DropDownStyle = ComboBoxStyle.DropDownList
			};
			u.Attr_2.Items.AddRange(A_4);
			u.Attr_2.SelectedIndex = 0;
			u.Attr_2.SelectedIndexChanged += u.\u00A0;
			Label value = new Label
			{
				Text = A_1,
				Location = new Point(A_2, A_3),
				ForeColor = Color.Gray,
				Font = this.\u1680,
				AutoSize = true
			};
			base.Controls.Add(u.\u00A0);
			base.Controls.Add(value);
		}

		// Token: 0x06000210 RID: 528 RVA: 0x00013520 File Offset: 0x00011720
		protected override void OnPaint(PaintEventArgs A_1)
		{
			Graphics graphics = A_1.Graphics;
			graphics.SmoothingMode = SmoothingMode.AntiAlias;
			using (Pen pen = new Pen(Color.FromArgb(20, 255, 255, 255), 1f))
			{
				graphics.DrawLine(pen, 20, 50, base.Width - 20, 50);
				graphics.DrawLine(pen, 50, 0, 50, base.Height);
			}
			graphics.DrawString("INJECTOR FLOW CALCULATOR", this.\u00A0, Brushes.IndianRed, 65f, 15f);
			float num = (this.\u1680 == "NA") ? 0.5f : 0.65f;
			if (this.\u00A0 == "E85")
			{
				num *= 1.35f;
			}
			double num2 = (double)this.\u1680 / 100.0;
			double num3 = (double)(this.\u00A0 * num) / ((double)this.\u00A0 * num2);
			double num4 = num3 * 10.5;
			int num5 = 65;
			int num6 = 230;
			this.\u00A0(graphics, "REQUIRED CC/MIN", string.Format("{0:F0}", num4), Color.FromArgb(0, 255, 120), num5, num6);
			this.\u00A0(graphics, "REQUIRED LB/HR", string.Format("{0:F1}", num3), Color.FromArgb(255, 200, 0), num5 + 220, num6);
			int num7 = 370;
			int num8 = 50;
			int num9 = 65;
			int num10 = 330;
			graphics.FillRectangle(new SolidBrush(Color.FromArgb(30, 30, 30)), num9, num10, num7, num8);
			int num11 = (int)(Math.Min(1f, (float)(num4 / 2200.0)) * (float)num7);
			using (LinearGradientBrush linearGradientBrush = new LinearGradientBrush(new Rectangle(num9, num10, num11 + 1, num8), Color.FromArgb(0, 255, 120), Color.FromArgb(255, 30, 30), 0f))
			{
				graphics.FillRectangle(linearGradientBrush, num9, num10, num11, num8);
			}
			graphics.DrawString("FLOW SCALE (0 - 2200cc)", this.\u1680, Brushes.DimGray, (float)num9, (float)(num10 + num8 + 5));
			using (Pen pen2 = new Pen(Color.FromArgb(100, 231, 76, 60), 2f))
			{
				graphics.DrawPath(pen2, this.\u00A0(new Rectangle(1, 1, base.Width - 2, base.Height - 2), 30));
			}
		}

		// Token: 0x06000211 RID: 529 RVA: 0x000137D0 File Offset: 0x000119D0
		private void \u00A0(Graphics A_1, string A_2, string A_3, Color A_4, int A_5, int A_6)
		{
			A_1.DrawString(A_2, this.\u1680, Brushes.Gray, (float)A_5, (float)A_6);
			using (SolidBrush solidBrush = new SolidBrush(Color.FromArgb(100, 255, 0, 0)))
			{
				A_1.DrawString(A_3, this.\u2000, solidBrush, (float)(A_5 - 1), (float)(A_6 + 20));
			}
			using (SolidBrush solidBrush2 = new SolidBrush(Color.FromArgb(100, 0, 255, 255)))
			{
				A_1.DrawString(A_3, this.\u2000, solidBrush2, (float)(A_5 + 1), (float)(A_6 + 20));
			}
			A_1.DrawString(A_3, this.\u2000, new SolidBrush(A_4), (float)A_5, (float)(A_6 + 20));
		}

		// Token: 0x06000212 RID: 530 RVA: 0x000138A4 File Offset: 0x00011AA4
		private GraphicsPath \u00A0(Rectangle A_1, int A_2)
		{
			GraphicsPath graphicsPath = new GraphicsPath();
			graphicsPath.AddArc(A_1.X, A_1.Y, A_2, A_2, 180f, 90f);
			graphicsPath.AddArc(A_1.Right - A_2, A_1.Y, A_2, A_2, 270f, 90f);
			graphicsPath.AddArc(A_1.Right - A_2, A_1.Bottom - A_2, A_2, A_2, 0f, 90f);
			graphicsPath.AddArc(A_1.X, A_1.Bottom - A_2, A_2, A_2, 90f, 90f);
			graphicsPath.CloseFigure();
			return graphicsPath;
		}

		// Token: 0x06000214 RID: 532 RVA: 0x000021C8 File Offset: 0x000003C8
		[CompilerGenerated]
		private void \u00A0(object A_1, EventArgs A_2)
		{
			base.Close();
		}

		// Token: 0x06000215 RID: 533 RVA: 0x00013983 File Offset: 0x00011B83
		[CompilerGenerated]
		private void \u00A0(float A_1)
		{
			this.\u00A0 = A_1;
			base.Invalidate();
		}

		// Token: 0x06000216 RID: 534 RVA: 0x00013992 File Offset: 0x00011B92
		[CompilerGenerated]
		private void \u1680(float A_1)
		{
			this.\u00A0 = (int)A_1;
			base.Invalidate();
		}

		// Token: 0x06000217 RID: 535 RVA: 0x000139A2 File Offset: 0x00011BA2
		[CompilerGenerated]
		private void \u00A0(string A_1)
		{
			this.\u00A0 = A_1;
			base.Invalidate();
		}

		// Token: 0x06000218 RID: 536 RVA: 0x000139B1 File Offset: 0x00011BB1
		[CompilerGenerated]
		private void \u1680(string A_1)
		{
			this.\u1680 = A_1;
			base.Invalidate();
		}

		// Token: 0x04000171 RID: 369
		private float \u00A0 = 300f;

		// Token: 0x04000172 RID: 370
		private int \u00A0 = 4;

		// Token: 0x04000173 RID: 371
		private float \u1680 = 80f;

		// Token: 0x04000174 RID: 372
		private string \u00A0 = "Gasoline";

		// Token: 0x04000175 RID: 373
		private string \u1680 = "NA";

		// Token: 0x020000B0 RID: 176
		[CompilerGenerated]
		private sealed class Attr_2
		{
			// Token: 0x0600021A RID: 538 RVA: 0x000139C0 File Offset: 0x00011BC0
			internal void \u00A0(object A_1, EventArgs A_2)
			{
				float obj;
				if (float.TryParse(this.Attr_2.Text, out obj))
				{
					this.\u00A0(obj);
				}
			}

			// Token: 0x04000179 RID: 377
			public TextBox \u00A0;

			// Token: 0x0400017A RID: 378
			public Action<float> \u00A0;
		}

		// Token: 0x020000B1 RID: 177
		[CompilerGenerated]
		private sealed class Attr_3
		{
			// Token: 0x0600021C RID: 540 RVA: 0x000139ED File Offset: 0x00011BED
			internal void \u00A0(object A_1, EventArgs A_2)
			{
				this.\u00A0(this.Attr_2.SelectedItem.ToString());
			}

			// Token: 0x0400017B RID: 379
			public Action<string> \u00A0;

			// Token: 0x0400017C RID: 380
			public ComboBox \u00A0;
		}
	}
}
