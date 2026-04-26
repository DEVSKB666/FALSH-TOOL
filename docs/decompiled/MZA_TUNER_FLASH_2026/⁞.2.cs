using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Forms;
using <PrivateImplementationDetails>{68F2EF73-9355-4257-ADA6-397CF8BB8E72};

namespace Attr_3
{
	// Token: 0x020000DF RID: 223
	[ToolboxItem(true)]
	[DesignerCategory("Code")]
	public class Type_54 : Panel
	{
		// Token: 0x060003DE RID: 990 RVA: 0x000241C8 File Offset: 0x000223C8
		[CompilerGenerated]
		public void \u00A0(EventHandler<string> A_1)
		{
			EventHandler<string> eventHandler = this.\u00A0;
			EventHandler<string> eventHandler2;
			do
			{
				eventHandler2 = eventHandler;
				EventHandler<string> value = (EventHandler<string>)Delegate.Combine(eventHandler2, A_1);
				eventHandler = Interlocked.CompareExchange<EventHandler<string>>(ref this.\u00A0, value, eventHandler2);
			}
			while (eventHandler != eventHandler2);
		}

		// Token: 0x060003DF RID: 991 RVA: 0x00024200 File Offset: 0x00022400
		[CompilerGenerated]
		public void \u1680(EventHandler<string> A_1)
		{
			EventHandler<string> eventHandler = this.\u00A0;
			EventHandler<string> eventHandler2;
			do
			{
				eventHandler2 = eventHandler;
				EventHandler<string> value = (EventHandler<string>)Delegate.Remove(eventHandler2, A_1);
				eventHandler = Interlocked.CompareExchange<EventHandler<string>>(ref this.\u00A0, value, eventHandler2);
			}
			while (eventHandler != eventHandler2);
		}

		// Token: 0x060003E0 RID: 992 RVA: 0x00024238 File Offset: 0x00022438
		public Type_54()
		{
			base.Size = new Size(450, 28);
			this.BackColor = Color.FromArgb(25, 25, 25);
			this.AllowDrop = true;
			this.Cursor = Cursors.Hand;
			this.DoubleBuffered = true;
			base.DragEnter += this.\u00A0;
			base.DragDrop += this.\u1680;
			base.Click += this.\u00A0;
		}

		// Token: 0x060003E1 RID: 993 RVA: 0x000242D4 File Offset: 0x000224D4
		private void \u00A0(object A_1, EventArgs A_2)
		{
			using (OpenFileDialog openFileDialog = new OpenFileDialog())
			{
				openFileDialog.Filter = "Bin files (*.bin)|*.bin|All files (*.*)|*.*";
				if (openFileDialog.ShowDialog() == DialogResult.OK)
				{
					this.\u00A0(openFileDialog.FileName);
				}
			}
		}

		// Token: 0x060003E2 RID: 994 RVA: 0x00024324 File Offset: 0x00022524
		private void \u00A0(object A_1, DragEventArgs A_2)
		{
			if (A_2.Data.GetDataPresent(DataFormats.FileDrop))
			{
				string[] array = (string[])A_2.Data.GetData(DataFormats.FileDrop);
				if (array.Length != 0 && Path.GetExtension(array[0]).ToLower() == ".bin")
				{
					A_2.Effect = DragDropEffects.Copy;
					return;
				}
			}
			A_2.Effect = DragDropEffects.None;
		}

		// Token: 0x060003E3 RID: 995 RVA: 0x00024388 File Offset: 0x00022588
		private void \u1680(object A_1, DragEventArgs A_2)
		{
			string[] array = (string[])A_2.Data.GetData(DataFormats.FileDrop);
			if (array.Length != 0)
			{
				this.\u00A0(array[0]);
			}
		}

		// Token: 0x060003E4 RID: 996 RVA: 0x000243B8 File Offset: 0x000225B8
		private void \u00A0(string A_1)
		{
			this.\u1680 = Path.GetFileName(A_1);
			EventHandler<string> u00A = this.\u00A0;
			if (u00A != null)
			{
				u00A(this, A_1);
			}
			this.Refresh();
		}

		// Token: 0x060003E5 RID: 997 RVA: 0x000243DF File Offset: 0x000225DF
		public void \u00A0()
		{
			this.\u1680 = "";
			this.Refresh();
		}

		// Token: 0x060003E6 RID: 998 RVA: 0x000243F4 File Offset: 0x000225F4
		protected override void OnPaint(PaintEventArgs A_1)
		{
			base.OnPaint(A_1);
			Graphics graphics = A_1.Graphics;
			graphics.SmoothingMode = SmoothingMode.AntiAlias;
			using (SolidBrush solidBrush = new SolidBrush(this.BackColor))
			{
				graphics.FillRectangle(solidBrush, base.ClientRectangle);
			}
			using (Pen pen = new Pen(Color.FromArgb(215, 15, 15), 1.5f))
			{
				graphics.DrawLine(pen, 0, base.Height - 1, base.Width, base.Height - 1);
			}
			int num = 10;
			int num2 = 14;
			int num3 = (base.Height - num2) / 2;
			using (SolidBrush solidBrush2 = new SolidBrush(Color.White))
			{
				graphics.FillRectangle(solidBrush2, num, num3, 10, num2);
				Point[] points = new Point[]
				{
					new Point(num + 6, num3),
					new Point(num + 10, num3),
					new Point(num + 10, num3 + 4)
				};
				graphics.FillPolygon(new SolidBrush(this.BackColor), points);
			}
			string text = string.IsNullOrEmpty(this.\u1680) ? this.\u00A0 : this.\u1680;
			using (Font font = new Font("Segoe UI", 8.5f, FontStyle.Bold))
			{
				using (SolidBrush solidBrush3 = new SolidBrush(Color.White))
				{
					SizeF sizeF = graphics.MeasureString(text, font);
					graphics.DrawString(text, font, solidBrush3, (float)(num + 18), ((float)base.Height - sizeF.Height) / 2f + 1f);
				}
			}
		}

		// Token: 0x04000307 RID: 775
		private string \u00A0 = "Drag the File to Select ..";

		// Token: 0x04000308 RID: 776
		private string \u1680 = "";

		// Token: 0x04000309 RID: 777
		[CompilerGenerated]
		private EventHandler<string> \u00A0;
	}
}
