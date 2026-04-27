using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace ns1
{
	// Token: 0x020000EB RID: 235
	public static class GClass17
	{
		// Token: 0x06000438 RID: 1080 RVA: 0x0000DCC0 File Offset: 0x0000BEC0
		[CompilerGenerated]
		public static List<GClass16> smethod_0()
		{
			return GClass17.list_0;
		}

		// Token: 0x06000439 RID: 1081 RVA: 0x0000DCC7 File Offset: 0x0000BEC7
		[CompilerGenerated]
		private static void smethod_1(List<GClass16> list_1)
		{
			GClass17.list_0 = list_1;
		}

		// Token: 0x0600043A RID: 1082
		[DllImport("kernel32.dll", CharSet = CharSet.Auto)]
		private static extern int GetPrivateProfileString(string string_1, string string_2, string string_3, StringBuilder stringBuilder_0, int int_0, string string_4);

		// Token: 0x0600043B RID: 1083
		[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern bool WritePrivateProfileString(string string_1, string string_2, string string_3, string string_4);

		// Token: 0x0600043C RID: 1084 RVA: 0x00038EA8 File Offset: 0x000370A8
		public static string smethod_2(string string_1, string string_2, string string_3 = "")
		{
			StringBuilder stringBuilder = new StringBuilder(1024);
			GClass17.GetPrivateProfileString(string_1, string_2, string_3, stringBuilder, stringBuilder.Capacity, GClass17.string_0);
			return stringBuilder.ToString();
		}

		// Token: 0x0600043D RID: 1085 RVA: 0x0000DCCF File Offset: 0x0000BECF
		public static bool smethod_3(string string_1, string string_2, string string_3)
		{
			return GClass17.WritePrivateProfileString(string_1, string_2, string_3, GClass17.string_0);
		}

		// Token: 0x0600043E RID: 1086 RVA: 0x00038EDC File Offset: 0x000370DC
		public static void smethod_4()
		{
			if (!File.Exists(GClass17.string_0))
			{
				return;
			}
			GClass17.smethod_0().Clear();
			int num = 1;
			for (;;)
			{
				string string_ = string.Format("ID{0:D4}", num);
				string text = GClass17.smethod_2("khPartCode", string_, null);
				if (string.IsNullOrEmpty(text))
				{
					break;
				}
				List<GClass16> list = GClass17.smethod_0();
				GClass16 gclass = new GClass16();
				gclass.method_1(text);
				gclass.method_3(GClass17.smethod_2("khEcmId", string_, ""));
				gclass.method_5(GClass17.smethod_2("khStartOffset", string_, ""));
				gclass.method_7(GClass17.smethod_2("khCksumOffset", string_, ""));
				gclass.method_9(GEnum2.const_0);
				list.Add(gclass);
				num++;
			}
			num = 1;
			for (;;)
			{
				string string_2 = string.Format("ID{0:D4}", num);
				string text2 = GClass17.smethod_2("shPartCode", string_2, null);
				if (string.IsNullOrEmpty(text2))
				{
					break;
				}
				List<GClass16> list2 = GClass17.smethod_0();
				GClass16 gclass2 = new GClass16();
				gclass2.method_1(text2);
				gclass2.method_3(GClass17.smethod_2("shEcmId", string_2, ""));
				gclass2.method_9(GEnum2.const_1);
				list2.Add(gclass2);
				num++;
			}
		}

		// Token: 0x0600043F RID: 1087 RVA: 0x00038FF4 File Offset: 0x000371F4
		public static void smethod_5()
		{
			List<GClass16> list = GClass17.smethod_0().Where(new Func<GClass16, bool>(GClass17.Class157.class157_0.method_0)).ToList<GClass16>();
			int num = 1;
			foreach (GClass16 gclass in list)
			{
				string string_ = string.Format("ID{0:D4}", num);
				GClass17.smethod_3("khPartCode", string_, gclass.method_0());
				GClass17.smethod_3("khEcmId", string_, gclass.method_2());
				GClass17.smethod_3("khStartOffset", string_, gclass.method_4());
				GClass17.smethod_3("khCksumOffset", string_, gclass.method_6());
				num++;
			}
			GClass17.smethod_3("khPartCode", string.Format("ID{0:D4}", num), null);
			List<GClass16> list2 = GClass17.smethod_0().Where(new Func<GClass16, bool>(GClass17.Class157.class157_0.method_1)).ToList<GClass16>();
			num = 1;
			foreach (GClass16 gclass2 in list2)
			{
				string string_2 = string.Format("ID{0:D4}", num);
				GClass17.smethod_3("shPartCode", string_2, gclass2.method_0());
				GClass17.smethod_3("shEcmId", string_2, gclass2.method_2());
				num++;
			}
			GClass17.smethod_3("shPartCode", string.Format("ID{0:D4}", num), null);
		}

		// Token: 0x06000440 RID: 1088 RVA: 0x000391A8 File Offset: 0x000373A8
		public static GClass16 smethod_6(byte[] byte_0)
		{
			if (byte_0 != null && byte_0.Length != 0)
			{
				string text = BitConverter.ToString(byte_0).Replace("-", "");
				using (List<GClass16>.Enumerator enumerator = GClass17.smethod_0().GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						GClass16 gclass = enumerator.Current;
						if (!string.IsNullOrEmpty(gclass.method_2()))
						{
							string value = gclass.method_2().Replace(" ", "").ToUpper();
							if (text.Contains(value))
							{
								return gclass;
							}
						}
					}
					goto IL_8B;
				}
				GClass16 result;
				return result;
				IL_8B:
				return null;
			}
			return null;
		}

		// Token: 0x06000441 RID: 1089 RVA: 0x0000DCDE File Offset: 0x0000BEDE
		public static bool smethod_7()
		{
			return File.Exists(GClass17.string_0);
		}

		// Token: 0x04000339 RID: 825
		private static string string_0 = "c:\\MZATUNER\\data.ini";

		// Token: 0x0400033A RID: 826
		[CompilerGenerated]
		private static List<GClass16> list_0 = new List<GClass16>();

		// Token: 0x020000EC RID: 236
		[CompilerGenerated]
		[Serializable]
		private sealed class Class157
		{
			// Token: 0x06000445 RID: 1093 RVA: 0x0000DD0C File Offset: 0x0000BF0C
			internal bool method_0(GClass16 gclass16_0)
			{
				return gclass16_0.method_8() == GEnum2.const_0;
			}

			// Token: 0x06000446 RID: 1094 RVA: 0x0000DD17 File Offset: 0x0000BF17
			internal bool method_1(GClass16 gclass16_0)
			{
				return gclass16_0.method_8() == GEnum2.const_1;
			}

			// Token: 0x0400033B RID: 827
			public static readonly GClass17.Class157 class157_0 = new GClass17.Class157();

			// Token: 0x0400033C RID: 828
			public static Func<GClass16, bool> func_0;

			// Token: 0x0400033D RID: 829
			public static Func<GClass16, bool> func_1;
		}
	}
}
