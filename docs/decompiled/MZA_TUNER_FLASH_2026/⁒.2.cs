using System;
using System.IO.Ports;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Attr_3
{
	// Token: 0x02000096 RID: 150
	public class Type_48
	{
		// Token: 0x06000168 RID: 360 RVA: 0x0000B6B8 File Offset: 0x000098B8
		public Task \u00A0(string A_1, SerialPort A_2, ProgressBar A_3)
		{
			\u2052.\u2000 u;
			u.\u00A0 = AsyncTaskMethodBuilder.Create();
			u.\u00A0 = A_1;
			u.\u00A0 = A_2;
			u.\u00A0 = A_3;
			u.\u00A0 = -1;
			u.Attr_2.Start<\u2052.\u2000>(ref u);
			return u.Attr_2.Task;
		}

		// Token: 0x06000169 RID: 361 RVA: 0x0000B70C File Offset: 0x0000990C
		public void \u00A0(string A_1, SerialPort A_2)
		{
			ProgressBar progressBar = new ProgressBar();
			this.\u00A0(A_1, A_2, progressBar);
		}

		// Token: 0x02000097 RID: 151
		[CompilerGenerated]
		private sealed class Attr_2
		{
			// Token: 0x0600016C RID: 364 RVA: 0x0000B72C File Offset: 0x0000992C
			internal void \u00A0()
			{
				\u2052.\u1680 u = new \u2052.\u1680();
				u.\u00A0 = this;
				u.\u00A0 = 0;
				while (u.\u00A0 < this.Attr_2.Length)
				{
					this.Attr_2.Write(this.\u00A0, u.\u00A0, 1);
					this.Attr_2.Invoke(new Action(u.\u00A0));
					Thread.Sleep(5);
					int u00A = u.\u00A0;
					u.\u00A0 = u00A + 1;
				}
			}

			// Token: 0x040000D4 RID: 212
			public SerialPort \u00A0;

			// Token: 0x040000D5 RID: 213
			public ProgressBar \u00A0;

			// Token: 0x040000D6 RID: 214
			public byte[] \u00A0;
		}

		// Token: 0x02000098 RID: 152
		[CompilerGenerated]
		private sealed class Attr_3
		{
			// Token: 0x0600016E RID: 366 RVA: 0x0000B7A5 File Offset: 0x000099A5
			internal void \u00A0()
			{
				this.Attr_2.Attr_2.Value = this.\u00A0 + 1;
			}

			// Token: 0x040000D7 RID: 215
			public int \u00A0;

			// Token: 0x040000D8 RID: 216
			public \u2052.\u00A0 \u00A0;
		}
	}
}
