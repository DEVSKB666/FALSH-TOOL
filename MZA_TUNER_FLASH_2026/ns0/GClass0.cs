using System;
using System.Diagnostics;
using System.Management;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace ns0
{
	// Token: 0x02000006 RID: 6
	public static class GClass0
	{
		// Token: 0x0600000F RID: 15
		[DllImport("kernel32.dll", ExactSpelling = true, SetLastError = true)]
		private static extern bool CheckRemoteDebuggerPresent(IntPtr intptr_0, ref bool bool_0);

		// Token: 0x06000010 RID: 16 RVA: 0x0000C34E File Offset: 0x0000A54E
		public static void smethod_0()
		{
			GClass0.smethod_1();
			GClass0.smethod_2();
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00016768 File Offset: 0x00014968
		private static void smethod_1()
		{
			if (Debugger.IsAttached)
			{
				GClass0.smethod_4("Debugger Detected (Managed)");
			}
			bool flag = false;
			GClass0.CheckRemoteDebuggerPresent(Process.GetCurrentProcess().Handle, ref flag);
			if (flag)
			{
				GClass0.smethod_4("Debugger Detected (Native)");
			}
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000167A8 File Offset: 0x000149A8
		private static void smethod_2()
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
						GClass0.smethod_4("Virtual Machine / Sandbox Detected");
					}
				}
			}
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00016850 File Offset: 0x00014A50
		public static string smethod_3()
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

		// Token: 0x06000014 RID: 20 RVA: 0x0000C35A File Offset: 0x0000A55A
		private static void smethod_4(string string_0)
		{
			MessageBox.Show("ระบบรักษาความปลอดภัย: " + string_0 + "\nโปรแกรมจะปิดตัวลงเพื่อป้องกันลิขสิทธิ์", "MZA_TUNER_FLASH Security", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			Environment.Exit(0);
		}
	}
}
