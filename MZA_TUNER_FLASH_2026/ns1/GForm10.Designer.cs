namespace ns1
{
	// Token: 0x020000AF RID: 175
	public partial class GForm10 : global::System.Windows.Forms.Form
	{
		// Token: 0x06000213 RID: 531 RVA: 0x0000CF64 File Offset: 0x0000B164
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				global::System.Drawing.Font font = this.font_0;
				if (font != null)
				{
					font.Dispose();
				}
				global::System.Drawing.Font font2 = this.font_1;
				if (font2 != null)
				{
					font2.Dispose();
				}
				global::System.Drawing.Font font3 = this.font_2;
				if (font3 != null)
				{
					font3.Dispose();
				}
			}
			base.Dispose(disposing);
		}

		// Token: 0x04000176 RID: 374
		private global::System.Drawing.Font font_0;

		// Token: 0x04000177 RID: 375
		private global::System.Drawing.Font font_1;

		// Token: 0x04000178 RID: 376
		private global::System.Drawing.Font font_2;
	}
}
