namespace Form_4
{
	// Token: 0x020000F5 RID: 245
	public partial class Form_4 : global::System.Windows.Forms.Form
	{
		// Token: 0x0600046B RID: 1131 RVA: 0x000280B0 File Offset: 0x000262B0
		protected override void Dispose(bool A_1)
		{
			if (A_1)
			{
				global::System.Windows.Forms.Timer u00A = this.M_4;
				if (u00A != null)
				{
					u00A.Stop();
				}
				global::System.Windows.Forms.Timer u00A2 = this.M_4;
				if (u00A2 != null)
				{
					u00A2.Dispose();
				}
			}
			base.Dispose(A_1);
		}

		// Token: 0x04000345 RID: 837
		private global::System.Windows.Forms.Timer \u00A0;
	}
}
