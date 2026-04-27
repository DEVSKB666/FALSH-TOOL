using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace ns1
{
	// Token: 0x020000E1 RID: 225
	public class GClass12 : ToolStripProfessionalRenderer
	{
		// Token: 0x060003F3 RID: 1011 RVA: 0x0000DAE2 File Offset: 0x0000BCE2
		public GClass12() : base(new GClass12.Class156())
		{
		}

		// Token: 0x060003F4 RID: 1012 RVA: 0x00037070 File Offset: 0x00035270
		protected override void OnRenderToolStripBackground(ToolStripRenderEventArgs e)
		{
			using (SolidBrush solidBrush = new SolidBrush(GClass12.color_0))
			{
				e.Graphics.FillRectangle(solidBrush, e.AffectedBounds);
			}
		}

		// Token: 0x060003F5 RID: 1013 RVA: 0x000370B8 File Offset: 0x000352B8
		protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
		{
			if (!e.Item.Enabled)
			{
				return;
			}
			Rectangle rectangle_ = new Rectangle(2, 1, e.Item.Width - 4, e.Item.Height - 2);
			e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
			if (e.Item.Selected)
			{
				using (SolidBrush solidBrush = new SolidBrush(GClass12.color_1))
				{
					using (GraphicsPath graphicsPath = this.method_0(rectangle_, 4))
					{
						e.Graphics.FillPath(solidBrush, graphicsPath);
						return;
					}
				}
			}
			if (e.Item.Pressed)
			{
				using (SolidBrush solidBrush2 = new SolidBrush(Color.FromArgb(30, 30, 30)))
				{
					using (GraphicsPath graphicsPath2 = this.method_0(rectangle_, 4))
					{
						e.Graphics.FillPath(solidBrush2, graphicsPath2);
					}
				}
			}
		}

		// Token: 0x060003F6 RID: 1014 RVA: 0x000371C8 File Offset: 0x000353C8
		protected override void OnRenderItemText(ToolStripItemTextRenderEventArgs e)
		{
			e.TextColor = (e.Item.Enabled ? GClass12.color_2 : Color.DimGray);
			if (e.ToolStrip is ToolStripDropDown)
			{
				Rectangle textRectangle = e.TextRectangle;
				textRectangle.Offset(5, 0);
				e.TextRectangle = textRectangle;
			}
			base.OnRenderItemText(e);
		}

		// Token: 0x060003F7 RID: 1015 RVA: 0x00037220 File Offset: 0x00035420
		protected override void OnRenderSeparator(ToolStripSeparatorRenderEventArgs e)
		{
			using (Pen pen = new Pen(GClass12.color_3))
			{
				int x = e.Item.Width - 10;
				int num = e.Item.Height / 2;
				e.Graphics.DrawLine(pen, 32, num, x, num);
			}
		}

		// Token: 0x060003F8 RID: 1016 RVA: 0x00037284 File Offset: 0x00035484
		protected override void OnRenderImageMargin(ToolStripRenderEventArgs e)
		{
			using (SolidBrush solidBrush = new SolidBrush(Color.FromArgb(20, 20, 20)))
			{
				e.Graphics.FillRectangle(solidBrush, e.AffectedBounds);
			}
		}

		// Token: 0x060003F9 RID: 1017 RVA: 0x0000DAEF File Offset: 0x0000BCEF
		protected override void OnRenderArrow(ToolStripArrowRenderEventArgs e)
		{
			e.ArrowColor = GClass12.color_2;
			base.OnRenderArrow(e);
		}

		// Token: 0x060003FA RID: 1018 RVA: 0x000372D0 File Offset: 0x000354D0
		private GraphicsPath method_0(Rectangle rectangle_0, int int_0)
		{
			GraphicsPath graphicsPath = new GraphicsPath();
			if (int_0 <= 0)
			{
				graphicsPath.AddRectangle(rectangle_0);
				return graphicsPath;
			}
			int num = int_0 * 2;
			graphicsPath.AddArc(rectangle_0.X, rectangle_0.Y, num, num, 180f, 90f);
			graphicsPath.AddArc(rectangle_0.Right - num, rectangle_0.Y, num, num, 270f, 90f);
			graphicsPath.AddArc(rectangle_0.Right - num, rectangle_0.Bottom - num, num, num, 0f, 90f);
			graphicsPath.AddArc(rectangle_0.X, rectangle_0.Bottom - num, num, num, 90f, 90f);
			graphicsPath.CloseFigure();
			return graphicsPath;
		}

		// Token: 0x04000312 RID: 786
		private static readonly Color color_0 = Color.FromArgb(15, 15, 15);

		// Token: 0x04000313 RID: 787
		private static readonly Color color_1 = Color.FromArgb(45, 45, 48);

		// Token: 0x04000314 RID: 788
		private static readonly Color color_2 = Color.WhiteSmoke;

		// Token: 0x04000315 RID: 789
		private static readonly Color color_3 = Color.FromArgb(50, 50, 50);

		// Token: 0x04000316 RID: 790
		private static readonly Color color_4 = Color.FromArgb(0, 122, 204);

		// Token: 0x020000E2 RID: 226
		private class Class156 : ProfessionalColorTable
		{
			// Token: 0x17000026 RID: 38
			// (get) Token: 0x060003FC RID: 1020 RVA: 0x0000DB03 File Offset: 0x0000BD03
			public override Color ToolStripDropDownBackground
			{
				get
				{
					return Color.FromArgb(25, 25, 25);
				}
			}

			// Token: 0x1700001C RID: 28
			// (get) Token: 0x060003FD RID: 1021 RVA: 0x0000DB10 File Offset: 0x0000BD10
			public override Color MenuStripGradientBegin
			{
				get
				{
					return GClass12.color_0;
				}
			}

			// Token: 0x1700001D RID: 29
			// (get) Token: 0x060003FE RID: 1022 RVA: 0x0000DB10 File Offset: 0x0000BD10
			public override Color MenuStripGradientEnd
			{
				get
				{
					return GClass12.color_0;
				}
			}

			// Token: 0x1700001E RID: 30
			// (get) Token: 0x060003FF RID: 1023 RVA: 0x0000DB17 File Offset: 0x0000BD17
			public override Color MenuItemSelected
			{
				get
				{
					return GClass12.color_1;
				}
			}

			// Token: 0x1700001F RID: 31
			// (get) Token: 0x06000400 RID: 1024 RVA: 0x0000D542 File Offset: 0x0000B742
			public override Color MenuItemBorder
			{
				get
				{
					return Color.Transparent;
				}
			}

			// Token: 0x17000021 RID: 33
			// (get) Token: 0x06000401 RID: 1025 RVA: 0x0000DB17 File Offset: 0x0000BD17
			public override Color MenuItemSelectedGradientBegin
			{
				get
				{
					return GClass12.color_1;
				}
			}

			// Token: 0x17000022 RID: 34
			// (get) Token: 0x06000402 RID: 1026 RVA: 0x0000DB17 File Offset: 0x0000BD17
			public override Color MenuItemSelectedGradientEnd
			{
				get
				{
					return GClass12.color_1;
				}
			}

			// Token: 0x17000023 RID: 35
			// (get) Token: 0x06000403 RID: 1027 RVA: 0x0000D549 File Offset: 0x0000B749
			public override Color MenuItemPressedGradientBegin
			{
				get
				{
					return Color.FromArgb(30, 30, 30);
				}
			}

			// Token: 0x17000024 RID: 36
			// (get) Token: 0x06000404 RID: 1028 RVA: 0x0000D549 File Offset: 0x0000B749
			public override Color MenuItemPressedGradientEnd
			{
				get
				{
					return Color.FromArgb(30, 30, 30);
				}
			}

			// Token: 0x17000019 RID: 25
			// (get) Token: 0x06000405 RID: 1029 RVA: 0x0000D5A6 File Offset: 0x0000B7A6
			public override Color ImageMarginGradientBegin
			{
				get
				{
					return Color.FromArgb(20, 20, 20);
				}
			}

			// Token: 0x1700001A RID: 26
			// (get) Token: 0x06000406 RID: 1030 RVA: 0x0000D5A6 File Offset: 0x0000B7A6
			public override Color ImageMarginGradientMiddle
			{
				get
				{
					return Color.FromArgb(20, 20, 20);
				}
			}

			// Token: 0x1700001B RID: 27
			// (get) Token: 0x06000407 RID: 1031 RVA: 0x0000D5A6 File Offset: 0x0000B7A6
			public override Color ImageMarginGradientEnd
			{
				get
				{
					return Color.FromArgb(20, 20, 20);
				}
			}

			// Token: 0x17000020 RID: 32
			// (get) Token: 0x06000408 RID: 1032 RVA: 0x0000D57F File Offset: 0x0000B77F
			public override Color MenuBorder
			{
				get
				{
					return Color.FromArgb(45, 45, 45);
				}
			}

			// Token: 0x17000025 RID: 37
			// (get) Token: 0x06000409 RID: 1033 RVA: 0x0000D57F File Offset: 0x0000B77F
			public override Color ToolStripBorder
			{
				get
				{
					return Color.FromArgb(45, 45, 45);
				}
			}
		}
	}
}
