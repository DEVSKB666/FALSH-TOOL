using System;
using System.IO;
using System.Management;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ns0
{
	// Token: 0x02000019 RID: 25
	public static class GClass2
	{
		// Token: 0x0600008C RID: 140 RVA: 0x0001AE28 File Offset: 0x00019028
		public static void smethod_0()
		{
			GClass2.Struct3 @struct;
			@struct.asyncVoidMethodBuilder_0 = AsyncVoidMethodBuilder.Create();
			@struct.int_0 = -1;
			@struct.asyncVoidMethodBuilder_0.Start<GClass2.Struct3>(ref @struct);
		}

		// Token: 0x0600008D RID: 141 RVA: 0x0001AE58 File Offset: 0x00019058
		public static bool smethod_1()
		{
			if (GClass2.smethod_2())
			{
				return true;
			}
			string string_ = GClass2.smethod_3();
			bool result;
			using (GForm4 gform = new GForm4(string_))
			{
				if (gform.ShowDialog() == DialogResult.OK)
				{
					string text = gform.method_0();
					if (string.Equals(text, GClass2.smethod_5(string_), StringComparison.OrdinalIgnoreCase))
					{
						if (!Directory.Exists(GClass2.string_4))
						{
							Directory.CreateDirectory(GClass2.string_4);
						}
						File.WriteAllText(GClass2.string_5, GClass2.smethod_7(text, "MZA_AUTO_99_PROTECT_ULTRA_V8"));
						MessageBox.Show("✅ การยืนยันเสร็จสมบูรณ์ ระบบ MZA-TUNER ถูกปลดล็อคสำเร็จ", "การยืนยันตัวตนสำเร็จ", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
						return true;
					}
				}
				result = false;
			}
			return result;
		}

		// Token: 0x0600008E RID: 142 RVA: 0x0001AEFC File Offset: 0x000190FC
		public static bool smethod_2()
		{
			bool result;
			try
			{
				if (!File.Exists(GClass2.string_5))
				{
					result = false;
				}
				else
				{
					result = string.Equals(GClass2.smethod_8(File.ReadAllText(GClass2.string_5), "MZA_AUTO_99_PROTECT_ULTRA_V8"), GClass2.smethod_5(GClass2.smethod_3()), StringComparison.OrdinalIgnoreCase);
				}
			}
			catch
			{
				result = false;
			}
			return result;
		}

		// Token: 0x0600008F RID: 143 RVA: 0x0001AF58 File Offset: 0x00019158
		public static string smethod_3()
		{
			if (!string.IsNullOrEmpty(GClass2.string_6))
			{
				return GClass2.string_6;
			}
			string text = GClass2.smethod_4("Win32_Processor", "ProcessorId") + GClass2.smethod_4("Win32_BaseBoard", "SerialNumber");
			if (string.IsNullOrEmpty(text))
			{
				text = Environment.MachineName + Environment.UserName;
			}
			string result;
			using (SHA512 sha = SHA512.Create())
			{
				GClass2.string_6 = BitConverter.ToString(sha.ComputeHash(Encoding.UTF8.GetBytes(text + "SHARK_V8_PREMIUM_SYSTEM"))).Replace("-", "").Substring(0, 40);
				result = GClass2.string_6;
			}
			return result;
		}

		// Token: 0x06000090 RID: 144 RVA: 0x0001B018 File Offset: 0x00019218
		private static string smethod_4(string string_7, string string_8)
		{
			try
			{
				using (ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher("SELECT " + string_8 + " FROM " + string_7))
				{
					using (ManagementObjectCollection.ManagementObjectEnumerator enumerator = managementObjectSearcher.Get().GetEnumerator())
					{
						if (enumerator.MoveNext())
						{
							object obj = enumerator.Current[string_8];
							string result;
							if (obj != null)
							{
								string text = obj.ToString();
								if (text != null)
								{
									if ((result = text.Trim()) != null)
									{
										goto IL_5A;
									}
								}
							}
							result = "";
							IL_5A:
							return result;
						}
					}
				}
			}
			catch
			{
			}
			return "";
		}

		// Token: 0x06000091 RID: 145 RVA: 0x0001B0CC File Offset: 0x000192CC
		public static string smethod_5(string string_7)
		{
			string result;
			using (SHA256 sha = SHA256.Create())
			{
				string text = BitConverter.ToString(sha.ComputeHash(Encoding.UTF8.GetBytes(string_7 + "MZA_SECRET_2026_PRO"))).Replace("-", "");
				StringBuilder stringBuilder = new StringBuilder();
				for (int i = 0; i < 20; i++)
				{
					if (i > 0 && i % 4 == 0)
					{
						stringBuilder.Append("-");
					}
					stringBuilder.Append(text[i]);
				}
				result = stringBuilder.ToString().ToUpper();
			}
			return result;
		}

		// Token: 0x06000092 RID: 146 RVA: 0x0001B170 File Offset: 0x00019370
		public static Task smethod_6()
		{
			GClass2.Struct2 @struct;
			@struct.asyncTaskMethodBuilder_0 = AsyncTaskMethodBuilder.Create();
			@struct.int_0 = -1;
			@struct.asyncTaskMethodBuilder_0.Start<GClass2.Struct2>(ref @struct);
			return @struct.asyncTaskMethodBuilder_0.Task;
		}

		// Token: 0x06000093 RID: 147 RVA: 0x0001B1AC File Offset: 0x000193AC
		private static string smethod_7(string string_7, string string_8)
		{
			byte[] bytes = Encoding.UTF8.GetBytes(string_7);
			for (int i = 0; i < bytes.Length; i++)
			{
				bytes[i] = (byte)((char)bytes[i] ^ string_8[i % string_8.Length]);
			}
			return Convert.ToBase64String(bytes);
		}

		// Token: 0x06000094 RID: 148 RVA: 0x0001B1F0 File Offset: 0x000193F0
		private static string smethod_8(string string_7, string string_8)
		{
			string result;
			try
			{
				byte[] array = Convert.FromBase64String(string_7);
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = (byte)((char)array[i] ^ string_8[i % string_8.Length]);
				}
				result = Encoding.UTF8.GetString(array);
			}
			catch
			{
				result = "";
			}
			return result;
		}

		// Token: 0x04000065 RID: 101
		private const string string_0 = "MZA_AUTO_99_PROTECT_ULTRA_V8";

		// Token: 0x04000066 RID: 102
		private const string string_1 = "SHARK_V8_PREMIUM_SYSTEM";

		// Token: 0x04000067 RID: 103
		private const string string_2 = "MZA_SECRET_2026_PRO";

		// Token: 0x04000068 RID: 104
		public const string string_3 = "http://82.26.104.124/mza-tuner/is_blacklisted.php?hwid=";

		// Token: 0x04000069 RID: 105
		private static readonly string string_4 = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "RemapMZA_Pro");

		// Token: 0x0400006A RID: 106
		private static readonly string string_5 = Path.Combine(GClass2.string_4, "system.dat");

		// Token: 0x0400006B RID: 107
		private static string string_6 = null;
	}
}
