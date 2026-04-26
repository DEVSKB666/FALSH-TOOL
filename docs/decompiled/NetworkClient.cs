using System;
using System.Diagnostics;
using System.IO;
using System.Management;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using <PrivateImplementationDetails>{68F2EF73-9355-4257-ADA6-397CF8BB8E72};
using Attr_3;

namespace Form_4
{
	// Token: 0x020000F0 RID: 240
	[\u3000("\0\0\0\0\0\0\0\0")]
	public static class Attr_3
	{
		// Token: 0x0600044B RID: 1099
		[DllImport("kernel32.dll", EntryPoint = "IsDebuggerPresent")]
		private static extern bool f_1();

		// Token: 0x0600044C RID: 1100
		[DllImport("kernel32.dll", EntryPoint = "GetModuleHandle", SetLastError = true)]
		private static extern IntPtr f_1(string);

		// Token: 0x0600044D RID: 1101
		[DllImport("kernel32.dll", EntryPoint = "VirtualProtect", SetLastError = true)]
		private static extern bool f_1(IntPtr, uint, uint, out uint);

		// Token: 0x0600044E RID: 1102
		[DllImport("kernel32.dll", EntryPoint = "CheckRemoteDebuggerPresent", SetLastError = true)]
		private static extern bool f_1(IntPtr, ref bool);

		// Token: 0x0600044F RID: 1103 RVA: 0x000272D0 File Offset: 0x000254D0
		public static void \u1680()
		{
			try
			{
				if (global::Form_4.Attr_3.\u2003())
				{
					global::Form_4.Attr_3.\u00A0("Security Violation", "SYSTEM_MALWARE_DEBUGGER_DETECTED\nโปรโตคอลความปลอดภัยสั่งระงับการทำงานทันที!");
					Environment.Exit(0);
				}
				if (global::Form_4.Attr_3.\u2004())
				{
					global::Form_4.Attr_3.\u00A0("System Integrity Check", "UNSUPPORTED_ENVIRONMENT_VM\nโปรแกรมไม่สามารถทำงานบนเครื่องเสมือนเพื่อความปลอดภัยของข้อมูล");
					Environment.Exit(0);
				}
				global::Form_4.Attr_3.\u2001();
				global::Form_4.Attr_3.\u2000();
				global::Form_4.Attr_3.\u2002();
			}
			catch
			{
			}
		}

		// Token: 0x06000450 RID: 1104 RVA: 0x0002733C File Offset: 0x0002553C
		private static void \u2000()
		{
			string text = AppDomain.CurrentDomain.BaseDirectory.ToLower();
			if (!text.Contains("\\temp\\"))
			{
				text.Contains("\\artifact\\");
			}
		}

		// Token: 0x06000451 RID: 1105 RVA: 0x00027374 File Offset: 0x00025574
		public static void \u2001()
		{
			try
			{
				Assembly executingAssembly = Assembly.GetExecutingAssembly();
				AssemblyTitleAttribute assemblyTitleAttribute = (AssemblyTitleAttribute)Attribute.GetCustomAttribute(executingAssembly, typeof(AssemblyTitleAttribute));
				AssemblyCompanyAttribute assemblyCompanyAttribute = (AssemblyCompanyAttribute)Attribute.GetCustomAttribute(executingAssembly, typeof(AssemblyCompanyAttribute));
				string a = ((assemblyTitleAttribute != null) ? assemblyTitleAttribute.Title : null) ?? "";
				string a2 = ((assemblyCompanyAttribute != null) ? assemblyCompanyAttribute.Company : null) ?? "";
				if (a != global::Form_4.Attr_3.\u1680("HBl8veffgNnIgeT8ht7oidLGjNTpj8jQUpPM35bPypnC2pzF3J85CWI7NmU+Hg==") || a2 != global::Form_4.Attr_3.\u1680("HBl8veffgNnIgeT8ht7oidLGjNTpj8jQUpPM35bPypnC2pzF3J85CWI7NmU+Hg=="))
				{
					global::Form_4.Attr_3.\u00A0("System Security Breach", "CRITICAL_ERROR: ILLEGAL_MODIFICATION_DETECTED\nตรวจพบการแก้ไขไฟล์โดยไม่ได้รับอนุญาต (Metadata Tampering)\nระบบจะปิดการทำงานเพื่อป้องกันความเสียหาย!");
					Environment.Exit(0);
				}
			}
			catch
			{
				Environment.Exit(0);
			}
		}

		// Token: 0x06000452 RID: 1106 RVA: 0x00027430 File Offset: 0x00025630
		private static void \u2002()
		{
			Task.Run(new Func<Task>(global::Form_4.Attr_3.Form_4.Attr_2.\u00A0));
		}

		// Token: 0x06000453 RID: 1107 RVA: 0x00027457 File Offset: 0x00025657
		private static void f_1(string A_0, string A_1)
		{
			MessageBox.Show(A_1, "MZA-TUNER: " + A_0, MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}

		// Token: 0x06000454 RID: 1108 RVA: 0x00027470 File Offset: 0x00025670
		private static bool \u2003()
		{
			if (Debugger.IsAttached)
			{
				return true;
			}
			if (global::Form_4.Attr_3.\u00A0())
			{
				return true;
			}
			bool flag = false;
			global::Form_4.Attr_3.\u00A0(Process.GetCurrentProcess().Handle, ref flag);
			if (flag)
			{
				return true;
			}
			string[] array = new string[]
			{
				"dnspy",
				"x64dbg",
				"x32dbg",
				"ollydbg",
				"ida64",
				"idag",
				"idaw",
				"scylla",
				"reclass",
				"cheatengine",
				"processhacker",
				"de4dot",
				"fiddler",
				"charles",
				"wireshark",
				"httpanalyzer",
				"httpdebug",
				"analyzer",
				"sniffer"
			};
			foreach (Process process in Process.GetProcesses())
			{
				try
				{
					string text = process.ProcessName.ToLower();
					foreach (string value in array)
					{
						if (text.Contains(value))
						{
							return true;
						}
					}
				}
				catch
				{
				}
			}
			return false;
		}

		// Token: 0x06000455 RID: 1109 RVA: 0x000275C0 File Offset: 0x000257C0
		private static bool \u2004()
		{
			try
			{
				using (ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher("Select * from Win32_ComputerSystem"))
				{
					foreach (ManagementBaseObject managementBaseObject in managementObjectSearcher.Get())
					{
						string text = managementBaseObject["Manufacturer"].ToString().ToLower();
						string text2 = managementBaseObject["Model"].ToString().ToLower();
						if ((text == "microsoft corporation" && text2.Contains("virtual")) || text.Contains("vmware") || text2.Contains("virtualbox"))
						{
							return true;
						}
					}
				}
				string[] array = new string[]
				{
					"C:\\windows\\System32\\Drivers\\VBoxMouse.sys",
					"C:\\windows\\System32\\Drivers\\VBoxGuest.sys",
					"C:\\windows\\System32\\Drivers\\VBoxSF.sys",
					"C:\\windows\\System32\\Drivers\\VBoxVideo.sys",
					"C:\\windows\\System32\\vboxdisp.dll",
					"C:\\windows\\System32\\vboxhook.dll"
				};
				for (int i = 0; i < array.Length; i++)
				{
					if (File.Exists(array[i]))
					{
						return true;
					}
				}
			}
			catch
			{
			}
			return false;
		}

		// Token: 0x06000456 RID: 1110 RVA: 0x00027724 File Offset: 0x00025924
		public static string \u1680(string A_0)
		{
			string result;
			try
			{
				byte[] array = Convert.FromBase64String(A_0);
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = (byte)((int)array[i] ^ 90 + i % 255);
				}
				result = Encoding.UTF8.GetString(array);
			}
			catch
			{
				result = A_0;
			}
			return result;
		}

		// Token: 0x06000457 RID: 1111 RVA: 0x0002777C File Offset: 0x0002597C
		public static string \u2000(string A_0)
		{
			byte[] bytes = Encoding.UTF8.GetBytes(A_0);
			for (int i = 0; i < bytes.Length; i++)
			{
				bytes[i] = (byte)((int)bytes[i] ^ 90 + i % 255);
			}
			return Convert.ToBase64String(bytes);
		}

		// Token: 0x06000458 RID: 1112 RVA: 0x000277BB File Offset: 0x000259BB
		private static string \u2005()
		{
			return "GARBAGE";
		}

		// Token: 0x06000459 RID: 1113 RVA: 0x000277C2 File Offset: 0x000259C2
		private static int f_1(int A_0, int A_1)
		{
			return (A_0 ^ A_1) + 90;
		}

		// Token: 0x0600045A RID: 1114 RVA: 0x000277CC File Offset: 0x000259CC
		private static void \u2006()
		{
			string str = "MZA";
			for (int i = 0; i < 100; i++)
			{
				str += i.ToString();
			}
		}

		// Token: 0x0600045B RID: 1115 RVA: 0x000277FA File Offset: 0x000259FA
		private static bool f_1(object A_0)
		{
			return A_0 != null && A_0.GetHashCode() > 0;
		}

		// Token: 0x0600045C RID: 1116 RVA: 0x0002780C File Offset: 0x00025A0C
		private static void \u2001(string A_0)
		{
			try
			{
				byte[] array = Convert.FromBase64String(A_0);
				global::Form_4.Attr_3.\u2006();
				if (global::Form_4.Attr_3.\u00A0(array))
				{
					global::Form_4.Attr_3.\u00A0(array.Length, 4660);
				}
			}
			catch
			{
			}
		}

		// Token: 0x0600045D RID: 1117 RVA: 0x00027850 File Offset: 0x00025A50
		private static void \u2007()
		{
			string text = "De4Dot_Trap";
			for (int i = 0; i < 100; i++)
			{
				if (i < 0)
				{
					text = text.Substring(i);
				}
			}
			if (text == null)
			{
				new global::Form_4.Attr_3.\u00A0().\u00A0 = new global::Form_4.Attr_3.\u1680();
			}
		}

		// Token: 0x020000F1 RID: 241
		private class Attr_2
		{
			// Token: 0x0400033E RID: 830
			public global::Form_4.Attr_3.\u1680 \u00A0;
		}

		// Token: 0x020000F2 RID: 242
		private class Attr_3
		{
			// Token: 0x0400033F RID: 831
			public global::Form_4.Attr_3.\u00A0 \u00A0;
		}

		// Token: 0x020000F3 RID: 243
		[CompilerGenerated]
		[Serializable]
		private sealed class Form_4
		{
			// Token: 0x06000462 RID: 1122 RVA: 0x0002789C File Offset: 0x00025A9C
			internal Task f_1()
			{
				global::Form_4.Attr_3.Form_4.\u00A0 u00A;
				u00A.\u00A0 = AsyncTaskMethodBuilder.Create();
				u00A.\u00A0 = -1;
				u00A.Attr_2.Start<global::Form_4.Attr_3.Form_4.\u00A0>(ref u00A);
				return u00A.Attr_2.Task;
			}

			// Token: 0x04000340 RID: 832
			public static readonly global::Form_4.Attr_3.\u2000 \u00A0 = new global::Form_4.Attr_3.\u2000();

			// Token: 0x04000341 RID: 833
			public static Func<Task> f_1;

			// Token: 0x020000F4 RID: 244
			[StructLayout(LayoutKind.Auto)]
			private struct Attr_2 : IAsyncStateMachine
			{
				// Token: 0x06000463 RID: 1123 RVA: 0x000278D8 File Offset: 0x00025AD8
				void IAsyncStateMachine.MoveNext()
				{
					int u00A = this.f_1;
					try
					{
						TaskAwaiter u00A2;
						if (u00A == 0)
						{
							u00A2 = this.f_1;
							this.f_1 = default(TaskAwaiter);
							this.f_1 = -1;
							goto IL_5F;
						}
						IL_0A:
						u00A2 = Task.Delay(5000).GetAwaiter();
						if (!u00A2.IsCompleted)
						{
							this.f_1 = 0;
							this.f_1 = u00A2;
							this.f_1.AwaitUnsafeOnCompleted<TaskAwaiter, global::Form_4.Attr_3.Form_4.\u00A0>(ref u00A2, ref this);
							return;
						}
						IL_5F:
						u00A2.GetResult();
						if (global::Form_4.Attr_3.\u2003())
						{
							Environment.Exit(0);
						}
						global::Form_4.Attr_3.\u2001();
						goto IL_0A;
					}
					catch (Exception exception)
					{
						this.f_1 = -2;
						this.f_1.SetException(exception);
					}
				}

				// Token: 0x06000464 RID: 1124 RVA: 0x00027988 File Offset: 0x00025B88
				[DebuggerHidden]
				void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine A_1)
				{
					this.f_1.SetStateMachine(A_1);
				}

				// Token: 0x04000342 RID: 834
				public int f_1;

				// Token: 0x04000343 RID: 835
				public AsyncTaskMethodBuilder f_1;

				// Token: 0x04000344 RID: 836
				private TaskAwaiter f_1;
			}
		}
	}
}
