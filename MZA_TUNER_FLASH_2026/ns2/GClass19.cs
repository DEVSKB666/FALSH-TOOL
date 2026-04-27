using System;
using System.Runtime.CompilerServices;
using System.Speech.Synthesis;
using System.Threading.Tasks;

namespace ns2
{
	// Token: 0x020000F7 RID: 247
	public static class GClass19
	{
		// Token: 0x0600046F RID: 1135 RVA: 0x00039B5C File Offset: 0x00037D5C
		static GClass19()
		{
			try
			{
				GClass19.speechSynthesizer_0 = new SpeechSynthesizer();
				GClass19.speechSynthesizer_0.Volume = 100;
				GClass19.speechSynthesizer_0.Rate = 1;
			}
			catch
			{
				GClass19.bool_0 = false;
			}
		}

		// Token: 0x06000470 RID: 1136 RVA: 0x00039BA8 File Offset: 0x00037DA8
		public static void smethod_0(string string_0)
		{
			GClass19.Class163 @class = new GClass19.Class163();
			@class.string_0 = string_0;
			if (GClass19.bool_0 && !string.IsNullOrEmpty(@class.string_0))
			{
				Task.Run(new Action(@class.method_0));
				return;
			}
		}

		// Token: 0x06000471 RID: 1137 RVA: 0x0000DDAF File Offset: 0x0000BFAF
		public static void smethod_1(string string_0)
		{
			GClass19.smethod_0(string_0);
		}

		// Token: 0x0400034D RID: 845
		private static SpeechSynthesizer speechSynthesizer_0;

		// Token: 0x0400034E RID: 846
		private static bool bool_0;

		// Token: 0x020000F8 RID: 248
		[CompilerGenerated]
		private sealed class Class163
		{
			// Token: 0x06000473 RID: 1139 RVA: 0x00039BEC File Offset: 0x00037DEC
			internal void method_0()
			{
				try
				{
					GClass19.speechSynthesizer_0.SpeakAsyncCancelAll();
					GClass19.speechSynthesizer_0.SpeakAsync(this.string_0);
				}
				catch
				{
				}
			}

			// Token: 0x0400034F RID: 847
			public string string_0;
		}
	}
}
