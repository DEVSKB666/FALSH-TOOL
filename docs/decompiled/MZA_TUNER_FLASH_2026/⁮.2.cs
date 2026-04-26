using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using <PrivateImplementationDetails>{68F2EF73-9355-4257-ADA6-397CF8BB8E72};

namespace Attr_3
{
	// Token: 0x020000EB RID: 235
	public static class Type_5F
	{
		// Token: 0x06000438 RID: 1080 RVA: 0x00026D51 File Offset: 0x00024F51
		[CompilerGenerated]
		public static List<Type_5E> \u00A0()
		{
			return \u206E.\u00A0;
		}

		// Token: 0x06000439 RID: 1081 RVA: 0x00026D58 File Offset: 0x00024F58
		[CompilerGenerated]
		private static void \u00A0(List<Type_5E> A_0)
		{
			\u206E.\u00A0 = A_0;
		}

		// Token: 0x0600043A RID: 1082
		[DllImport("kernel32.dll", CharSet = CharSet.Auto, EntryPoint = "GetPrivateProfileString")]
		private static extern int \u00A0(string, string, string, StringBuilder, int, string);

		// Token: 0x0600043B RID: 1083
		[DllImport("kernel32.dll", CharSet = CharSet.Auto, EntryPoint = "WritePrivateProfileString", SetLastError = true)]
		private static extern bool \u00A0(string, string, string, string);

		// Token: 0x0600043C RID: 1084 RVA: 0x00026D60 File Offset: 0x00024F60
		public static string \u00A0(string A_0, string A_1, string A_2 = "")
		{
			StringBuilder stringBuilder = new StringBuilder(1024);
			\u206E.\u00A0(A_0, A_1, A_2, stringBuilder, stringBuilder.Capacity, \u206E.\u00A0);
			return stringBuilder.ToString();
		}

		// Token: 0x0600043D RID: 1085 RVA: 0x00026D93 File Offset: 0x00024F93
		public static bool \u1680(string A_0, string A_1, string A_2)
		{
			return \u206E.\u00A0(A_0, A_1, A_2, \u206E.\u00A0);
		}

		// Token: 0x0600043E RID: 1086 RVA: 0x00026DA4 File Offset: 0x00024FA4
		public static void \u1680()
		{
			if (!File.Exists(\u206E.\u00A0))
			{
				return;
			}
			\u206E.\u00A0().Clear();
			int num = 1;
			for (;;)
			{
				string text = string.Format("ID{0:D4}", num);
				string text2 = \u206E.\u00A0("khPartCode", text, null);
				if (string.IsNullOrEmpty(text2))
				{
					break;
				}
				List<Type_5E> list = \u206E.\u00A0();
				\u206D u206D = new Type_5E();
				u206D.\u00A0(text2);
				u206D.\u1680(\u206E.\u00A0("khEcmId", text, ""));
				u206D.\u2000(\u206E.\u00A0("khStartOffset", text, ""));
				u206D.\u2001(\u206E.\u00A0("khCksumOffset", text, ""));
				u206D.\u00A0(\u206C.\u00A0);
				list.Add(u206D);
				num++;
			}
			num = 1;
			for (;;)
			{
				string text3 = string.Format("ID{0:D4}", num);
				string text4 = \u206E.\u00A0("shPartCode", text3, null);
				if (string.IsNullOrEmpty(text4))
				{
					break;
				}
				List<Type_5E> list2 = \u206E.\u00A0();
				\u206D u206D2 = new Type_5E();
				u206D2.\u00A0(text4);
				u206D2.\u1680(\u206E.\u00A0("shEcmId", text3, ""));
				u206D2.\u00A0(\u206C.\u1680);
				list2.Add(u206D2);
				num++;
			}
		}

		// Token: 0x0600043F RID: 1087 RVA: 0x00026EBC File Offset: 0x000250BC
		public static void \u2000()
		{
			List<Type_5E> list = \u206E.\u00A0().Where(new Func<Type_5E, bool>(\u206E.Attr_2.Attr_2.\u00A0)).ToList<Type_5E>();
			int num = 1;
			foreach (\u206D u206D in list)
			{
				string text = string.Format("ID{0:D4}", num);
				\u206E.\u1680("khPartCode", text, u206D.\u00A0());
				\u206E.\u1680("khEcmId", text, u206D.\u1680());
				\u206E.\u1680("khStartOffset", text, u206D.\u2000());
				\u206E.\u1680("khCksumOffset", text, u206D.\u2001());
				num++;
			}
			\u206E.\u1680("khPartCode", string.Format("ID{0:D4}", num), null);
			List<Type_5E> list2 = \u206E.\u00A0().Where(new Func<Type_5E, bool>(\u206E.Attr_2.Attr_2.\u1680)).ToList<Type_5E>();
			num = 1;
			foreach (\u206D u206D2 in list2)
			{
				string text2 = string.Format("ID{0:D4}", num);
				\u206E.\u1680("shPartCode", text2, u206D2.\u00A0());
				\u206E.\u1680("shEcmId", text2, u206D2.\u1680());
				num++;
			}
			\u206E.\u1680("shPartCode", string.Format("ID{0:D4}", num), null);
		}

		// Token: 0x06000440 RID: 1088 RVA: 0x0002706C File Offset: 0x0002526C
		public static \u206D \u00A0(byte[] A_0)
		{
			if (A_0 == null || A_0.Length == 0)
			{
				return null;
			}
			string text = BitConverter.ToString(A_0).Replace("-", "");
			foreach (\u206D u206D in \u206E.\u00A0())
			{
				if (!string.IsNullOrEmpty(u206D.\u1680()))
				{
					string value = u206D.\u1680().Replace(" ", "").ToUpper();
					if (text.Contains(value))
					{
						return u206D;
					}
				}
			}
			return null;
		}

		// Token: 0x06000441 RID: 1089 RVA: 0x00027110 File Offset: 0x00025310
		public static bool \u2001()
		{
			return File.Exists(\u206E.\u00A0);
		}

		// Token: 0x04000339 RID: 825
		private static string \u00A0 = "c:\\MZATUNER\\data.ini";

		// Token: 0x0400033A RID: 826
		[CompilerGenerated]
		private static List<Type_5E> \u00A0 = new List<Type_5E>();

		// Token: 0x020000EC RID: 236
		[CompilerGenerated]
		[Serializable]
		private sealed class Attr_2
		{
			// Token: 0x06000445 RID: 1093 RVA: 0x0002713E File Offset: 0x0002533E
			internal bool \u00A0(\u206D A_1)
			{
				return A_1.\u2002() == \u206C.\u00A0;
			}

			// Token: 0x06000446 RID: 1094 RVA: 0x00027149 File Offset: 0x00025349
			internal bool \u1680(\u206D A_1)
			{
				return A_1.\u2002() == \u206C.\u1680;
			}

			// Token: 0x0400033B RID: 827
			public static readonly \u206E.\u00A0 \u00A0 = new \u206E.\u00A0();

			// Token: 0x0400033C RID: 828
			public static Func<Type_5E, bool> \u00A0;

			// Token: 0x0400033D RID: 829
			public static Func<Type_5E, bool> \u1680;
		}
	}
}
