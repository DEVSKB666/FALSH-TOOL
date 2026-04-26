using System;
using System.Runtime.InteropServices;

namespace Attr_2
{
	// Token: 0x02000007 RID: 7
	public class Type_7
	{
		// Token: 0x06000015 RID: 21
		[DllImport("advapi32.dll", CharSet = CharSet.Auto, EntryPoint = "CryptAcquireContext", SetLastError = true)]
		public static extern bool M_15(ref UIntPtr, string, string, uint, uint);

		// Token: 0x06000016 RID: 22
		[DllImport("advapi32.dll", EntryPoint = "CryptCreateHash", SetLastError = true)]
		public static extern bool M_15(UIntPtr, uint, UIntPtr, uint, ref UIntPtr);

		// Token: 0x06000017 RID: 23
		[DllImport("advapi32.dll", EntryPoint = "CryptHashData", SetLastError = true)]
		public static extern bool M_15(UIntPtr, byte[], uint, uint);

		// Token: 0x06000018 RID: 24
		[DllImport("advapi32.dll", EntryPoint = "CryptDeriveKey", SetLastError = true)]
		public static extern bool M_18(UIntPtr, uint, UIntPtr, uint, ref UIntPtr);

		// Token: 0x06000019 RID: 25
		[DllImport("advapi32.dll", EntryPoint = "CryptDecrypt", SetLastError = true)]
		public static extern bool M_15(UIntPtr, UIntPtr, uint, uint, byte[], ref uint);

		// Token: 0x0400000C RID: 12
		public const uint M_15 = 1U;

		// Token: 0x0400000D RID: 13
		public const uint M_18 = 1U;

		// Token: 0x0400000E RID: 14
		public const uint f_E = 32771U;

		// Token: 0x0400000F RID: 15
		public const uint f_F = 26625U;

		// Token: 0x04000010 RID: 16
		public const uint f_E = 1U;

		// Token: 0x04000011 RID: 17
		public const uint \u2003 = 0U;
	}
}
