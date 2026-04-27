using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Forms;

namespace ns1
{
	// Token: 0x020000DF RID: 223
	[ToolboxItem(true)]
	[DesignerCategory("Code")]
	public class GClass11 : Panel
	{
		// Token: 0x060003DE RID: 990 RVA: 0x00036790 File Offset: 0x00034990
		[CompilerGenerated]
		public void method_0(EventHandler<string> eventHandler_1)
		{
			EventHandler<string> eventHandler = this.eventHandler_0;
			EventHandler<string> eventHandler2;
			do
			{
				eventHandler2 = eventHandler;
				EventHandler<string> value = (EventHandler<string>)Delegate.Combine(eventHandler2, eventHandler_1);
				eventHandler = Interlocked.CompareExchange<EventHandler<string>>(ref this.eventHandler_0, value, eventHandler2);
			}
			while (eventHandler != eventHandler2);
		}

		// Token: 0x060003DF RID: 991 RVA: 0x000367C8 File Offset: 0x000349C8
		[CompilerGenerated]
		public void method_1(EventHandler<string> eventHandler_1)
		{
			EventHandler<string> eventHandler = this.eventHandler_0;
			EventHandler<string> eventHandler2;
			do
			{
				eventHandler2 = eventHandler;
				EventHandler<string> value = (EventHandler<string>)Delegate.Remove(eventHandler2, eventHandler_1);
				eventHandler = Interlocked.CompareExchange<EventHandler<string>>(ref this.eventHandler_0, value, eventHandler2);
			}
			while (eventHandler != eventHandler2);
		}

		// Token: 0x060003E0 RID: 992 RVA: 0x00036800 File Offset: 0x00034A00
		public GClass11()
		{
			base.Size = new Size(450, 28);
			this.BackColor = Color.FromArgb(25, 25, 25);
			this.AllowDrop = true;
			this.Cursor = Cursors.Hand;
			this.DoubleBuffered = true;
			base.DragEnter += this.GClass11_DragEnter;
			base.DragDrop += this.GClass11_DragDrop;
			base.Click += this.GClass11_Click;
		}

		// Token: 0x060003E1 RID: 993 RVA: 0x0003689C File Offset: 0x00034A9C
		private void GClass11_Click(object sender, EventArgs e)
		{
			using (OpenFileDialog openFileDialog = new OpenFileDialog())
			{
				openFileDialog.Filter = "Bin files (*.bin)|*.bin|All files (*.*)|*.*";
				if (openFileDialog.ShowDialog() == DialogResult.OK)
				{
					this.method_2(openFileDialog.FileName);
				}
			}
		}

		// Token: 0x060003E2 RID: 994 RVA: 0x000368EC File Offset: 0x00034AEC
		private void GClass11_DragEnter(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent(DataFormats.FileDrop))
			{
				string[] array = (string[])e.Data.GetData(DataFormats.FileDrop);
				if (array.Length != 0 && Path.GetExtension(array[0]).ToLower() == ".bin")
				{
					e.Effect = DragDropEffects.Copy;
					return;
				}
			}
			e.Effect = DragDropEffects.None;
		}

		// Token: 0x060003E3 RID: 995 RVA: 0x00036950 File Offset: 0x00034B50
		private void GClass11_DragDrop(object sender, DragEventArgs e)
		{
			string[] array = (string[])e.Data.GetData(DataFormats.FileDrop);
			if (array.Length != 0)
			{
				this.method_2(array[0]);
			}
		}

		// Token: 0x060003E4 RID: 996 RVA: 0x0000DA1B File Offset: 0x0000BC1B
		private void method_2(string string_2)
		{
			this.string_1 = Path.GetFileName(string_2);
			EventHandler<string> eventHandler = this.eventHandler_0;
			if (eventHandler != null)
			{
				eventHandler(this, string_2);
			}
			this.Refresh();
		}

		// Token: 0x060003E5 RID: 997 RVA: 0x0000DA42 File Offset: 0x0000BC42
		public void method_3()
		{
			this.string_1 = "";
			this.Refresh();
		}

		// Token: 0x060003E6 RID: 998 RVA: 0x00036980 File Offset: 0x00034B80
		protected override void OnPaint(PaintEventArgs pevent)
		{
			base.OnPaint(pevent);
			Graphics graphics = pevent.Graphics;
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
			int height = 14;
			int num2 = (base.Height - 14) / 2;
			using (SolidBrush solidBrush2 = new SolidBrush(Color.White))
			{
				graphics.FillRectangle(solidBrush2, num, num2, 10, height);
				Point[] points = new Point[]
				{
					new Point(num + 6, num2),
					new Point(num + 10, num2),
					new Point(num + 10, num2 + 4)
				};
				graphics.FillPolygon(new SolidBrush(this.BackColor), points);
			}
			string text = string.IsNullOrEmpty(this.string_1) ? this.string_0 : this.string_1;
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
		private string string_0 = "Drag the File to Select ..";

		// Token: 0x04000308 RID: 776
		private string string_1 = "";

		// Token: 0x04000309 RID: 777
		[CompilerGenerated]
		private EventHandler<string> eventHandler_0;
	}
}
