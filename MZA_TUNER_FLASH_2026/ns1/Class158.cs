using System;
using System.Threading;
using System.Windows.Forms;
using ns2;

namespace ns1
{
	// Token: 0x020000ED RID: 237
	internal static class Class158
	{
		// Token: 0x06000447 RID: 1095 RVA: 0x00039254 File Offset: 0x00037454
		[STAThread]
		private static void Main()
		{
			bool flag;
			using (new Mutex(true, "MZA_TUNER_2026_SINGLE_INSTANCE_MUTEX", ref flag))
			{
				if (flag)
				{
					try
					{
						Application.EnableVisualStyles();
						Application.SetCompatibleTextRenderingDefault(false);
						using (GForm16 gform = new GForm16())
						{
							gform.Show();
							Application.DoEvents();
							gform.method_0("กำลังเริ่มต้นระบบ MZA-TUNER...", 15);
							Class158.smethod_0(800);
							gform.method_0("กำลังโหลดข้อมูลตารางแมพเครื่องยนต์...", 45);
							Class158.smethod_0(1000);
							gform.method_0("ตรวจสอบความปลอดภัยและสิทธิ์การใช้งาน...", 75);
							Class158.smethod_0(800);
							gform.method_0("กำลังเปิดหน้าหลัก HFT v2.0...", 100);
							Class158.smethod_0(600);
							gform.Close();
						}
						Application.Run(new GForm12());
						return;
					}
					catch (Exception ex)
					{
						MessageBox.Show("ตรวจพบข้อผิดพลาดร้ายแรงขณะเริ่มโปรแกรม:\n\n" + ex.ToString(), "MZA-TUNER CRITICAL ERROR", MessageBoxButtons.OK, MessageBoxIcon.Hand);
						return;
					}
				}
				MessageBox.Show("โปรแกรม MZA-TUNER กำลังทำงานอยู่แล้ว ไม่สามารถเปิดซ้อนได้ครับ!", "แจ้งเตือนระบบ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
		}

		// Token: 0x06000448 RID: 1096 RVA: 0x00039374 File Offset: 0x00037574
		private static void smethod_0(int int_0)
		{
			DateTime t = DateTime.Now.AddMilliseconds((double)int_0);
			while (DateTime.Now < t)
			{
				Application.DoEvents();
				Thread.Sleep(10);
			}
		}
	}
}
