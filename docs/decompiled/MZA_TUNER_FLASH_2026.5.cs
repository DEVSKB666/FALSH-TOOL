using System;
using System.Diagnostics;
using System.Management;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using <PrivateImplementationDetails>{68F2EF73-9355-4257-ADA6-397CF8BB8E72};

namespace Attr_2
{
	// Token: 0x02000006 RID: 6
	public static class Type_6
	{
		// Token: 0x0600000F RID: 15
		[DllImport("kernel32.dll", EntryPoint = "CheckRemoteDebuggerPresent", ExactSpelling = true, SetLastError = true)]
		private static extern bool M_F(IntPtr, ref bool);

		// Token: 0x06000010 RID: 16 RVA: 0x0000287C File Offset: 0x00000A7C
		public static void M_F()
		{
			\u2002.\u1680();
			\u2002.\u2000();
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002888 File Offset: 0x00000A88
		private static void M_11()
		{
			if (Debugger.IsAttached)
			{
				\u2002.\u00A0("Debugger Detected (Managed)");
			}
			bool flag = false;
			\u2002.\u00A0(Process.GetCurrentProcess().Handle, ref flag);
			if (flag)
			{
				\u2002.\u00A0("Debugger Detected (Native)");
			}
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000028C8 File Offset: 0x00000AC8
		private static void M_12()
		{
			string[] array = new string[]
			{
				"vboxservice",
				"vboxtray",
				"vmtoolsd",
				"vgauthservice",
				"vmacthlp",
				"xenservice",
				"vmsrvc",
				"vmusrvc"
			};
			foreach (Process process in Process.GetProcesses())
			{
				foreach (string value in array)
				{
					if (process.ProcessName.ToLower().Contains(value))
					{
						\u2002.\u00A0("Virtual Machine / Sandbox Detected");
					}
				}
			}
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002970 File Offset: 0x00000B70
		public static string M_13()
		{
			string text = "";
			try
			{
				using (ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher("SELECT ProcessorId FROM Win32_Processor"))
				{
					using (ManagementObjectCollection.ManagementObjectEnumerator enumerator = managementObjectSearcher.Get().GetEnumerator())
					{
						if (enumerator.MoveNext())
						{
							ManagementObject managementObject = (ManagementObject)enumerator.Current;
							text += managementObject["ProcessorId"].ToString();
						}
					}
				}
				using (ManagementObjectSearcher managementObjectSearcher2 = new ManagementObjectSearcher("SELECT SerialNumber FROM Win32_BaseBoard"))
				{
					using (ManagementObjectCollection.ManagementObjectEnumerator enumerator = managementObjectSearcher2.Get().GetEnumerator())
					{
						if (enumerator.MoveNext())
						{
							ManagementObject managementObject2 = (ManagementObject)enumerator.Current;
							text += managementObject2["SerialNumber"].ToString();
						}
					}
				}
			}
			catch
			{
				text = "UNKNOWN_ID";
			}
			return text.Trim();
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002A98 File Offset: 0x00000C98
		private static void M_F(string A_0)
		{
			MessageBox.Show("ระบบรักษาความปลอดภัย: " + A_0 + "\nโปรแกรมจะปิดตัวลงเพื่อป้องกันลิขสิทธิ์", "MZA_TUNER_FLASH Security", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			Environment.Exit(0);
		}
	}
}
