using System;
using System.Runtime.CompilerServices;
using System.Speech.Synthesis;
using System.Threading.Tasks;

namespace Form_4
{
	// Token: 0x020000F7 RID: 247
	public static class Attr_5
	{
		// Token: 0x0600046F RID: 1135 RVA: 0x0002818C File Offset: 0x0002638C
		static Attr_5()
		{
			try
			{
				\u2001.\u00A0 = new SpeechSynthesizer();
				\u2001.Attr_2.Volume = 100;
				\u2001.Attr_2.Rate = 1;
			}
			catch
			{
				\u2001.\u00A0 = false;
			}
		}

		// Token: 0x06000470 RID: 1136 RVA: 0x000281D8 File Offset: 0x000263D8
		public static void f_B(string A_0)
		{
			\u2001.\u00A0 u00A = new \u2001.\u00A0();
			u00A.\u00A0 = A_0;
			if (!\u2001.\u00A0 || string.IsNullOrEmpty(u00A.\u00A0))
			{
				return;
			}
			Task.Run(new Action(u00A.\u00A0));
		}

		// Token: 0x06000471 RID: 1137 RVA: 0x00028219 File Offset: 0x00026419
		public static void \u1680(string A_0)
		{
			\u2001.\u00A0(A_0);
		}

		// Token: 0x0400034D RID: 845
		private static SpeechSynthesizer f_B;

		// Token: 0x0400034E RID: 846
		private static bool f_B;

		// Token: 0x020000F8 RID: 248
		[CompilerGenerated]
		private sealed class Attr_2
		{
			// Token: 0x06000473 RID: 1139 RVA: 0x00028224 File Offset: 0x00026424
			internal void f_B()
			{
				try
				{
					\u2001.Attr_2.SpeakAsyncCancelAll();
					\u2001.Attr_2.SpeakAsync(this.f_B);
				}
				catch
				{
				}
			}

			// Token: 0x0400034F RID: 847
			public string f_B;
		}
	}
}
