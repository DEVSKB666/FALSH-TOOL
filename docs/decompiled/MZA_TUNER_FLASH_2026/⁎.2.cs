using System;
using System.Runtime.InteropServices;

namespace Attr_3
{
	// Token: 0x0200008E RID: 142
	internal class Type_44
	{
		// Token: 0x0600013A RID: 314
		[DllImport("ftd2xx.dll", EntryPoint = "FT_Open")]
		public static extern \u204E.\u00A0 \u00A0(uint, ref IntPtr);

		// Token: 0x0600013B RID: 315
		[DllImport("ftd2xx.dll", EntryPoint = "FT_Close")]
		public static extern \u204E.\u00A0 \u00A0(IntPtr);

		// Token: 0x0600013C RID: 316
		[DllImport("ftd2xx.dll", EntryPoint = "FT_Read")]
		public static extern \u204E.\u00A0 \u00A0(IntPtr, byte[], uint, ref uint);

		// Token: 0x0600013D RID: 317
		[DllImport("ftd2xx.dll", EntryPoint = "FT_Write")]
		public static extern \u204E.\u00A0 \u1680(IntPtr, byte[], uint, ref uint);

		// Token: 0x0600013E RID: 318
		[DllImport("ftd2xx.dll", EntryPoint = "FT_GetQueueStatus")]
		public static extern \u204E.\u00A0 \u00A0(IntPtr, ref uint);

		// Token: 0x0600013F RID: 319
		[DllImport("ftd2xx.dll", EntryPoint = "FT_SetBaudRate")]
		public static extern \u204E.\u00A0 \u00A0(IntPtr, uint);

		// Token: 0x06000140 RID: 320
		[DllImport("ftd2xx.dll", EntryPoint = "FT_SetDataCharacteristics")]
		public static extern \u204E.\u00A0 \u00A0(IntPtr, byte, byte, byte);

		// Token: 0x06000141 RID: 321
		[DllImport("ftd2xx.dll", EntryPoint = "FT_Purge")]
		public static extern \u204E.\u00A0 \u1680(IntPtr, uint);

		// Token: 0x06000142 RID: 322
		[DllImport("ftd2xx.dll", EntryPoint = "FT_SetTimeouts")]
		public static extern \u204E.\u00A0 \u00A0(IntPtr, uint, uint);

		// Token: 0x06000143 RID: 323
		[DllImport("ftd2xx.dll", EntryPoint = "FT_SetLatencyTimer")]
		public static extern \u204E.\u00A0 \u00A0(IntPtr, byte);

		// Token: 0x06000144 RID: 324
		[DllImport("ftd2xx.dll", EntryPoint = "FT_SetBitMode")]
		public static extern \u204E.\u00A0 \u00A0(IntPtr, byte, byte);

		// Token: 0x0200008F RID: 143
		public enum Attr_2
		{
			// Token: 0x040000A9 RID: 169
			\u00A0,
			// Token: 0x040000AA RID: 170
			\u1680,
			// Token: 0x040000AB RID: 171
			\u2000,
			// Token: 0x040000AC RID: 172
			\u2001,
			// Token: 0x040000AD RID: 173
			\u2002,
			// Token: 0x040000AE RID: 174
			\u2003,
			// Token: 0x040000AF RID: 175
			\u2004,
			// Token: 0x040000B0 RID: 176
			\u2005,
			// Token: 0x040000B1 RID: 177
			\u2006,
			// Token: 0x040000B2 RID: 178
			\u2007,
			// Token: 0x040000B3 RID: 179
			\u2008,
			// Token: 0x040000B4 RID: 180
			\u2009,
			// Token: 0x040000B5 RID: 181
			\u200A,
			// Token: 0x040000B6 RID: 182
			\u200B,
			// Token: 0x040000B7 RID: 183
			\u2010,
			// Token: 0x040000B8 RID: 184
			\u2011,
			// Token: 0x040000B9 RID: 185
			\u2012,
			// Token: 0x040000BA RID: 186
			\u2013
		}
	}
}
