using System;
using System.IO.Ports;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ns1
{
	// Token: 0x02000096 RID: 150
	public class GClass5
	{
		// Token: 0x06000168 RID: 360 RVA: 0x0001EFA4 File Offset: 0x0001D1A4
		public Task method_0(string string_0, SerialPort serialPort_0, ProgressBar progressBar_0)
		{
			GClass5.Struct6 @struct;
			@struct.asyncTaskMethodBuilder_0 = AsyncTaskMethodBuilder.Create();
			@struct.string_0 = string_0;
			@struct.serialPort_0 = serialPort_0;
			@struct.progressBar_0 = progressBar_0;
			@struct.int_0 = -1;
			@struct.asyncTaskMethodBuilder_0.Start<GClass5.Struct6>(ref @struct);
			return @struct.asyncTaskMethodBuilder_0.Task;
		}

		// Token: 0x06000169 RID: 361 RVA: 0x0001EFF8 File Offset: 0x0001D1F8
		public void method_1(string string_0, SerialPort serialPort_0)
		{
			ProgressBar progressBar_ = new ProgressBar();
			this.method_0(string_0, serialPort_0, progressBar_);
		}

		// Token: 0x02000097 RID: 151
		[CompilerGenerated]
		private sealed class Class125
		{
			// Token: 0x0600016C RID: 364 RVA: 0x0001F018 File Offset: 0x0001D218
			internal void method_0()
			{
				GClass5.Class126 @class = new GClass5.Class126();
				@class.class125_0 = this;
				@class.int_0 = 0;
				while (@class.int_0 < this.byte_0.Length)
				{
					this.serialPort_0.Write(this.byte_0, @class.int_0, 1);
					this.progressBar_0.Invoke(new Action(@class.method_0));
					Thread.Sleep(5);
					int int_ = @class.int_0;
					@class.int_0 = int_ + 1;
				}
			}

			// Token: 0x040000D4 RID: 212
			public SerialPort serialPort_0;

			// Token: 0x040000D5 RID: 213
			public ProgressBar progressBar_0;

			// Token: 0x040000D6 RID: 214
			public byte[] byte_0;
		}

		// Token: 0x02000098 RID: 152
		[CompilerGenerated]
		private sealed class Class126
		{
			// Token: 0x0600016E RID: 366 RVA: 0x0000C955 File Offset: 0x0000AB55
			internal void method_0()
			{
				this.class125_0.progressBar_0.Value = this.int_0 + 1;
			}

			// Token: 0x040000D7 RID: 215
			public int int_0;

			// Token: 0x040000D8 RID: 216
			public GClass5.Class125 class125_0;
		}
	}
}
