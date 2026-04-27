namespace ns2
{
	// Token: 0x020000F5 RID: 245
	public partial class GForm16 : global::System.Windows.Forms.Form
	{
		// Token: 0x0600046B RID: 1131 RVA: 0x0000DD54 File Offset: 0x0000BF54
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				global::System.Windows.Forms.Timer timer = this.timer_0;
				if (timer != null)
				{
					timer.Stop();
				}
				global::System.Windows.Forms.Timer timer2 = this.timer_0;
				if (timer2 != null)
				{
					timer2.Dispose();
				}
			}
			base.Dispose(disposing);
		}

		// Token: 0x04000345 RID: 837
		private global::System.Windows.Forms.Timer timer_0;
	}
}
