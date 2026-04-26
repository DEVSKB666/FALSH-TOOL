using System;
using System.IO;
using System.Management;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using <PrivateImplementationDetails>{68F2EF73-9355-4257-ADA6-397CF8BB8E72};

namespace Attr_2
{
	// Token: 0x02000019 RID: 25
	public static class Type_14
	{
		// Token: 0x0600008C RID: 140 RVA: 0x00007200 File Offset: 0x00005400
		public static void M_83()
		{
			\u2007.\u1680 u;
			u.\u00A0 = AsyncVoidMethodBuilder.Create();
			u.\u00A0 = -1;
			u.Attr_2.Start<\u2007.\u1680>(ref u);
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00007230 File Offset: 0x00005430
		public static bool \u1680()
		{
			if (\u2007.\u2000())
			{
				return true;
			}
			string text = \u2007.\u2001();
			bool result;
			using (\u2008 u = new Type_15(text))
			{
				if (u.ShowDialog() == DialogResult.OK)
				{
					string text2 = u.\u00A0();
					if (string.Equals(text2, \u2007.\u00A0(text), StringComparison.OrdinalIgnoreCase))
					{
						if (!Directory.Exists(\u2007.\u2002))
						{
							Directory.CreateDirectory(\u2007.\u2002);
						}
						File.WriteAllText(\u2007.\u2003, \u2007.\u1680(text2, "MZA_AUTO_99_PROTECT_ULTRA_V8"));
						MessageBox.Show("✅ การยืนยันเสร็จสมบูรณ์ ระบบ MZA-TUNER ถูกปลดล็อคสำเร็จ", "การยืนยันตัวตนสำเร็จ", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
						return true;
					}
				}
				result = false;
			}
			return result;
		}

		// Token: 0x0600008E RID: 142 RVA: 0x000072D4 File Offset: 0x000054D4
		public static bool \u2000()
		{
			bool result;
			try
			{
				if (!File.Exists(\u2007.\u2003))
				{
					result = false;
				}
				else
				{
					result = string.Equals(\u2007.\u2000(File.ReadAllText(\u2007.\u2003), "MZA_AUTO_99_PROTECT_ULTRA_V8"), \u2007.\u00A0(\u2007.\u2001()), StringComparison.OrdinalIgnoreCase);
				}
			}
			catch
			{
				result = false;
			}
			return result;
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00007330 File Offset: 0x00005530
		public static string \u2001()
		{
			if (!string.IsNullOrEmpty(\u2007.\u2004))
			{
				return \u2007.\u2004;
			}
			string text = \u2007.\u00A0("Win32_Processor", "ProcessorId") + \u2007.\u00A0("Win32_BaseBoard", "SerialNumber");
			if (string.IsNullOrEmpty(text))
			{
				text = Environment.MachineName + Environment.UserName;
			}
			string u;
			using (SHA512 sha = SHA512.Create())
			{
				\u2007.\u2004 = BitConverter.ToString(sha.ComputeHash(Encoding.UTF8.GetBytes(text + "SHARK_V8_PREMIUM_SYSTEM"))).Replace("-", "").Substring(0, 40);
				u = \u2007.\u2004;
			}
			return u;
		}

		// Token: 0x06000090 RID: 144 RVA: 0x000073F0 File Offset: 0x000055F0
		private static string M_83(string A_0, string A_1)
		{
			try
			{
				using (ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher("SELECT " + A_1 + " FROM " + A_0))
				{
					using (ManagementObjectCollection.ManagementObjectEnumerator enumerator = managementObjectSearcher.Get().GetEnumerator())
					{
						if (enumerator.MoveNext())
						{
							object obj = enumerator.Current[A_1];
							string text;
							if (obj == null)
							{
								text = null;
							}
							else
							{
								string text2 = obj.ToString();
								text = ((text2 != null) ? text2.Trim() : null);
							}
							return text ?? "";
						}
					}
				}
			}
			catch
			{
			}
			return "";
		}

		// Token: 0x06000091 RID: 145 RVA: 0x000074A8 File Offset: 0x000056A8
		public static string M_83(string A_0)
		{
			string result;
			using (SHA256 sha = SHA256.Create())
			{
				string text = BitConverter.ToString(sha.ComputeHash(Encoding.UTF8.GetBytes(A_0 + "MZA_SECRET_2026_PRO"))).Replace("-", "");
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

		// Token: 0x06000092 RID: 146 RVA: 0x0000754C File Offset: 0x0000574C
		public static Task \u2002()
		{
			\u2007.\u00A0 u00A;
			u00A.\u00A0 = AsyncTaskMethodBuilder.Create();
			u00A.\u00A0 = -1;
			u00A.Attr_2.Start<\u2007.\u00A0>(ref u00A);
			return u00A.Attr_2.Task;
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00007588 File Offset: 0x00005788
		private static string \u1680(string A_0, string A_1)
		{
			byte[] bytes = Encoding.UTF8.GetBytes(A_0);
			for (int i = 0; i < bytes.Length; i++)
			{
				bytes[i] = (byte)((char)bytes[i] ^ A_1[i % A_1.Length]);
			}
			return Convert.ToBase64String(bytes);
		}

		// Token: 0x06000094 RID: 148 RVA: 0x000075CC File Offset: 0x000057CC
		private static string \u2000(string A_0, string A_1)
		{
			string result;
			try
			{
				byte[] array = Convert.FromBase64String(A_0);
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = (byte)((char)array[i] ^ A_1[i % A_1.Length]);
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
		private const string M_83 = "MZA_AUTO_99_PROTECT_ULTRA_V8";

		// Token: 0x04000066 RID: 102
		private const string \u1680 = "SHARK_V8_PREMIUM_SYSTEM";

		// Token: 0x04000067 RID: 103
		private const string \u2000 = "MZA_SECRET_2026_PRO";

		// Token: 0x04000068 RID: 104
		public const string \u2001 = "http://82.26.104.124/mza-tuner/is_blacklisted.php?hwid=";

		// Token: 0x04000069 RID: 105
		private static readonly string \u2002 = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "RemapMZA_Pro");

		// Token: 0x0400006A RID: 106
		private static readonly string \u2003 = Path.Combine(\u2007.\u2002, "system.dat");

		// Token: 0x0400006B RID: 107
		private static string \u2004 = null;
	}
}
