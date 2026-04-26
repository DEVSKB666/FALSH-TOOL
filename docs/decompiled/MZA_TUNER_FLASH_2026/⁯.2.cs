using System;
using System.Threading;
using System.Windows.Forms;
using <PrivateImplementationDetails>{68F2EF73-9355-4257-ADA6-397CF8BB8E72};
using Attr_2;
using Form_4;

namespace Attr_3
{
	// Token: 0x020000ED RID: 237
	internal static class Type_60
	{
		// Token: 0x06000447 RID: 1095 RVA: 0x00027154 File Offset: 0x00025354
		[STAThread]
		private static void \u00A0()
		{
			bool flag;
			using (new Mutex(true, "MZA_TUNER_2026_SINGLE_INSTANCE_MUTEX", ref flag))
			{
				if (!flag)
				{
					MessageBox.Show("โปรแกรม MZA-TUNER กำลังทำงานอยู่แล้ว ไม่สามารถเปิดซ้อนได้ครับ!", "แจ้งเตือนระบบ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				}
				else
				{
					global::Form_4.Attr_3.\u1680();
					try
					{
						Application.EnableVisualStyles();
						Application.SetCompatibleTextRenderingDefault(false);
						if (global::Attr_2.Type_14.\u1680())
						{
							using (global::Form_4.\u2000 u = new global::Form_4.\u2000())
							{
								u.Show();
								Application.DoEvents();
								u.\u00A0("กำลังเริ่มต้นระบบ MZA-TUNER...", 15);
								\u206F.\u00A0(800);
								u.\u00A0("กำลังโหลดข้อมูลตารางแมพเครื่องยนต์...", 45);
								\u206F.\u00A0(1000);
								u.\u00A0("ตรวจสอบความปลอดภัยและสิทธิ์การใช้งาน...", 75);
								\u206F.\u00A0(800);
								u.\u00A0("กำลังเปิดหน้าหลัก HFT v2.0...", 100);
								\u206F.\u00A0(600);
								u.Close();
							}
							Application.Run(new Type_50());
						}
						else
						{
							MessageBox.Show("ระบบความปลอดภัยปฏิเสธการเข้าใช้งาน (Security Check Failed)\nกรุณาติดต่อผู้ดูแลระบบเพื่อขอรหัสปลดล็อค", "MZA-TUNER SECURITY", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
						}
					}
					catch (Exception ex)
					{
						MessageBox.Show("ตรวจพบข้อผิดพลาดร้ายแรงขณะเริ่มโปรแกรม:\n\n" + ex.ToString(), "MZA-TUNER CRITICAL ERROR", MessageBoxButtons.OK, MessageBoxIcon.Hand);
					}
				}
			}
		}

		// Token: 0x06000448 RID: 1096 RVA: 0x00027298 File Offset: 0x00025498
		private static void \u00A0(int A_0)
		{
			DateTime t = DateTime.Now.AddMilliseconds((double)A_0);
			while (DateTime.Now < t)
			{
				Application.DoEvents();
				Thread.Sleep(10);
			}
		}
	}
}
