using System;
using System.Runtime.InteropServices;

namespace ns0
{
	// Token: 0x02000007 RID: 7
	public class GClass1
	{
		// Token: 0x06000015 RID: 21
		[DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern bool CryptAcquireContext(ref UIntPtr uintptr_0, string string_0, string string_1, uint uint_6, uint uint_7);

		// Token: 0x06000016 RID: 22
		[DllImport("advapi32.dll", SetLastError = true)]
		public static extern bool CryptCreateHash(UIntPtr uintptr_0, uint uint_6, UIntPtr uintptr_1, uint uint_7, ref UIntPtr uintptr_2);

		// Token: 0x06000017 RID: 23
		[DllImport("advapi32.dll", SetLastError = true)]
		public static extern bool CryptHashData(UIntPtr uintptr_0, byte[] byte_0, uint uint_6, uint uint_7);

		// Token: 0x06000018 RID: 24
		[DllImport("advapi32.dll", SetLastError = true)]
		public static extern bool CryptDeriveKey(UIntPtr uintptr_0, uint uint_6, UIntPtr uintptr_1, uint uint_7, ref UIntPtr uintptr_2);

		// Token: 0x06000019 RID: 25
		[DllImport("advapi32.dll", SetLastError = true)]
		public static extern bool CryptDecrypt(UIntPtr uintptr_0, UIntPtr uintptr_1, uint uint_6, uint uint_7, byte[] byte_0, ref uint uint_8);

		// Token: 0x0400000C RID: 12
		public const uint uint_0 = 1U;

		// Token: 0x0400000D RID: 13
		public const uint uint_1 = 1U;

		// Token: 0x0400000E RID: 14
		public const uint uint_2 = 32771U;

		// Token: 0x0400000F RID: 15
		public const uint uint_3 = 26625U;

		// Token: 0x04000010 RID: 16
		public const uint uint_4 = 1U;

		// Token: 0x04000011 RID: 17
		public const uint uint_5 = 0U;
	}
}
