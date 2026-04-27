using System;
using System.Runtime.InteropServices;

namespace ns1
{
	// Token: 0x0200008E RID: 142
	internal class Class122
	{
		// Token: 0x0600013A RID: 314
		[DllImport("ftd2xx.dll")]
		public static extern Class122.Enum0 FT_Open(uint uint_0, ref IntPtr intptr_0);

		// Token: 0x0600013B RID: 315
		[DllImport("ftd2xx.dll")]
		public static extern Class122.Enum0 FT_Close(IntPtr intptr_0);

		// Token: 0x0600013C RID: 316
		[DllImport("ftd2xx.dll")]
		public static extern Class122.Enum0 FT_Read(IntPtr intptr_0, byte[] byte_0, uint uint_0, ref uint uint_1);

		// Token: 0x0600013D RID: 317
		[DllImport("ftd2xx.dll")]
		public static extern Class122.Enum0 FT_Write(IntPtr intptr_0, byte[] byte_0, uint uint_0, ref uint uint_1);

		// Token: 0x0600013E RID: 318
		[DllImport("ftd2xx.dll")]
		public static extern Class122.Enum0 FT_GetQueueStatus(IntPtr intptr_0, ref uint uint_0);

		// Token: 0x0600013F RID: 319
		[DllImport("ftd2xx.dll")]
		public static extern Class122.Enum0 FT_SetBaudRate(IntPtr intptr_0, uint uint_0);

		// Token: 0x06000140 RID: 320
		[DllImport("ftd2xx.dll")]
		public static extern Class122.Enum0 FT_SetDataCharacteristics(IntPtr intptr_0, byte byte_0, byte byte_1, byte byte_2);

		// Token: 0x06000141 RID: 321
		[DllImport("ftd2xx.dll")]
		public static extern Class122.Enum0 FT_Purge(IntPtr intptr_0, uint uint_0);

		// Token: 0x06000142 RID: 322
		[DllImport("ftd2xx.dll")]
		public static extern Class122.Enum0 FT_SetTimeouts(IntPtr intptr_0, uint uint_0, uint uint_1);

		// Token: 0x06000143 RID: 323
		[DllImport("ftd2xx.dll")]
		public static extern Class122.Enum0 FT_SetLatencyTimer(IntPtr intptr_0, byte byte_0);

		// Token: 0x06000144 RID: 324
		[DllImport("ftd2xx.dll")]
		public static extern Class122.Enum0 FT_SetBitMode(IntPtr intptr_0, byte byte_0, byte byte_1);

		// Token: 0x0200008F RID: 143
		public enum Enum0
		{
			// Token: 0x040000A9 RID: 169
			const_0,
			// Token: 0x040000AA RID: 170
			const_1,
			// Token: 0x040000AB RID: 171
			const_2,
			// Token: 0x040000AC RID: 172
			const_3,
			// Token: 0x040000AD RID: 173
			const_4,
			// Token: 0x040000AE RID: 174
			const_5,
			// Token: 0x040000AF RID: 175
			const_6,
			// Token: 0x040000B0 RID: 176
			const_7,
			// Token: 0x040000B1 RID: 177
			const_8,
			// Token: 0x040000B2 RID: 178
			const_9,
			// Token: 0x040000B3 RID: 179
			const_10,
			// Token: 0x040000B4 RID: 180
			const_11,
			// Token: 0x040000B5 RID: 181
			const_12,
			// Token: 0x040000B6 RID: 182
			const_13,
			// Token: 0x040000B7 RID: 183
			const_14,
			// Token: 0x040000B8 RID: 184
			const_15,
			// Token: 0x040000B9 RID: 185
			const_16,
			// Token: 0x040000BA RID: 186
			const_17
		}
	}
}
