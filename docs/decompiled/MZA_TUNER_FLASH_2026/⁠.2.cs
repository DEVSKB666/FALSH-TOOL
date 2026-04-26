using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Attr_3
{
	// Token: 0x020000E1 RID: 225
	public class Type_56 : ToolStripProfessionalRenderer
	{
		// Token: 0x060003F3 RID: 1011 RVA: 0x00024B72 File Offset: 0x00022D72
		public Type_56() : base(new \u2060.\u00A0())
		{
		}

		// Token: 0x060003F4 RID: 1012 RVA: 0x00024B80 File Offset: 0x00022D80
		protected override void OnRenderToolStripBackground(ToolStripRenderEventArgs A_1)
		{
			using (SolidBrush solidBrush = new SolidBrush(\u2060.\u00A0))
			{
				A_1.Graphics.FillRectangle(solidBrush, A_1.AffectedBounds);
			}
		}

		// Token: 0x060003F5 RID: 1013 RVA: 0x00024BC8 File Offset: 0x00022DC8
		protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs A_1)
		{
			if (!A_1.Item.Enabled)
			{
				return;
			}
			Rectangle rectangle = new Rectangle(2, 1, A_1.Item.Width - 4, A_1.Item.Height - 2);
			A_1.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
			if (A_1.Item.Selected)
			{
				using (SolidBrush solidBrush = new SolidBrush(\u2060.\u1680))
				{
					using (GraphicsPath graphicsPath = this.\u00A0(rectangle, 4))
					{
						A_1.Graphics.FillPath(solidBrush, graphicsPath);
						return;
					}
				}
			}
			if (A_1.Item.Pressed)
			{
				using (SolidBrush solidBrush2 = new SolidBrush(Color.FromArgb(30, 30, 30)))
				{
					using (GraphicsPath graphicsPath2 = this.\u00A0(rectangle, 4))
					{
						A_1.Graphics.FillPath(solidBrush2, graphicsPath2);
					}
				}
			}
		}

		// Token: 0x060003F6 RID: 1014 RVA: 0x00024CD8 File Offset: 0x00022ED8
		protected override void OnRenderItemText(ToolStripItemTextRenderEventArgs A_1)
		{
			A_1.TextColor = (A_1.Item.Enabled ? \u2060.\u2000 : Color.DimGray);
			if (A_1.ToolStrip is ToolStripDropDown)
			{
				Rectangle textRectangle = A_1.TextRectangle;
				textRectangle.Offset(5, 0);
				A_1.TextRectangle = textRectangle;
			}
			base.OnRenderItemText(A_1);
		}

		// Token: 0x060003F7 RID: 1015 RVA: 0x00024D30 File Offset: 0x00022F30
		protected override void OnRenderSeparator(ToolStripSeparatorRenderEventArgs A_1)
		{
			using (Pen pen = new Pen(\u2060.\u2001))
			{
				int x = 32;
				int x2 = A_1.Item.Width - 10;
				int num = A_1.Item.Height / 2;
				A_1.Graphics.DrawLine(pen, x, num, x2, num);
			}
		}

		// Token: 0x060003F8 RID: 1016 RVA: 0x00024D94 File Offset: 0x00022F94
		protected override void OnRenderImageMargin(ToolStripRenderEventArgs A_1)
		{
			using (SolidBrush solidBrush = new SolidBrush(Color.FromArgb(20, 20, 20)))
			{
				A_1.Graphics.FillRectangle(solidBrush, A_1.AffectedBounds);
			}
		}

		// Token: 0x060003F9 RID: 1017 RVA: 0x00024DE0 File Offset: 0x00022FE0
		protected override void OnRenderArrow(ToolStripArrowRenderEventArgs A_1)
		{
			A_1.ArrowColor = \u2060.\u2000;
			base.OnRenderArrow(A_1);
		}

		// Token: 0x060003FA RID: 1018 RVA: 0x00024DF4 File Offset: 0x00022FF4
		private GraphicsPath \u00A0(Rectangle A_1, int A_2)
		{
			GraphicsPath graphicsPath = new GraphicsPath();
			if (A_2 <= 0)
			{
				graphicsPath.AddRectangle(A_1);
				return graphicsPath;
			}
			int num = A_2 * 2;
			graphicsPath.AddArc(A_1.X, A_1.Y, num, num, 180f, 90f);
			graphicsPath.AddArc(A_1.Right - num, A_1.Y, num, num, 270f, 90f);
			graphicsPath.AddArc(A_1.Right - num, A_1.Bottom - num, num, num, 0f, 90f);
			graphicsPath.AddArc(A_1.X, A_1.Bottom - num, num, num, 90f, 90f);
			graphicsPath.CloseFigure();
			return graphicsPath;
		}

		// Token: 0x04000312 RID: 786
		private static readonly Color \u00A0 = Color.FromArgb(15, 15, 15);

		// Token: 0x04000313 RID: 787
		private static readonly Color \u1680 = Color.FromArgb(45, 45, 48);

		// Token: 0x04000314 RID: 788
		private static readonly Color \u2000 = Color.WhiteSmoke;

		// Token: 0x04000315 RID: 789
		private static readonly Color \u2001 = Color.FromArgb(50, 50, 50);

		// Token: 0x04000316 RID: 790
		private static readonly Color \u2002 = Color.FromArgb(0, 122, 204);

		// Token: 0x020000E2 RID: 226
		private class Attr_2 : ProfessionalColorTable
		{
			// Token: 0x060003FC RID: 1020 RVA: 0x00024F01 File Offset: 0x00023101
			public override Color get_ToolStripDropDownBackground()
			{
				return Color.FromArgb(25, 25, 25);
			}

			// Token: 0x060003FD RID: 1021 RVA: 0x00024F0E File Offset: 0x0002310E
			public override Color get_MenuStripGradientBegin()
			{
				return \u2060.\u00A0;
			}

			// Token: 0x060003FE RID: 1022 RVA: 0x00024F0E File Offset: 0x0002310E
			public override Color get_MenuStripGradientEnd()
			{
				return \u2060.\u00A0;
			}

			// Token: 0x060003FF RID: 1023 RVA: 0x00024F15 File Offset: 0x00023115
			public override Color get_MenuItemSelected()
			{
				return \u2060.\u1680;
			}

			// Token: 0x06000400 RID: 1024 RVA: 0x00020E2D File Offset: 0x0001F02D
			public override Color get_MenuItemBorder()
			{
				return Color.Transparent;
			}

			// Token: 0x06000401 RID: 1025 RVA: 0x00024F15 File Offset: 0x00023115
			public override Color get_MenuItemSelectedGradientBegin()
			{
				return \u2060.\u1680;
			}

			// Token: 0x06000402 RID: 1026 RVA: 0x00024F15 File Offset: 0x00023115
			public override Color get_MenuItemSelectedGradientEnd()
			{
				return \u2060.\u1680;
			}

			// Token: 0x06000403 RID: 1027 RVA: 0x00020E34 File Offset: 0x0001F034
			public override Color get_MenuItemPressedGradientBegin()
			{
				return Color.FromArgb(30, 30, 30);
			}

			// Token: 0x06000404 RID: 1028 RVA: 0x00020E34 File Offset: 0x0001F034
			public override Color get_MenuItemPressedGradientEnd()
			{
				return Color.FromArgb(30, 30, 30);
			}

			// Token: 0x06000405 RID: 1029 RVA: 0x00020E91 File Offset: 0x0001F091
			public override Color get_ImageMarginGradientBegin()
			{
				return Color.FromArgb(20, 20, 20);
			}

			// Token: 0x06000406 RID: 1030 RVA: 0x00020E91 File Offset: 0x0001F091
			public override Color get_ImageMarginGradientMiddle()
			{
				return Color.FromArgb(20, 20, 20);
			}

			// Token: 0x06000407 RID: 1031 RVA: 0x00020E91 File Offset: 0x0001F091
			public override Color get_ImageMarginGradientEnd()
			{
				return Color.FromArgb(20, 20, 20);
			}

			// Token: 0x06000408 RID: 1032 RVA: 0x00020E6A File Offset: 0x0001F06A
			public override Color get_MenuBorder()
			{
				return Color.FromArgb(45, 45, 45);
			}

			// Token: 0x06000409 RID: 1033 RVA: 0x00020E6A File Offset: 0x0001F06A
			public override Color get_ToolStripBorder()
			{
				return Color.FromArgb(45, 45, 45);
			}
		}
	}
}
