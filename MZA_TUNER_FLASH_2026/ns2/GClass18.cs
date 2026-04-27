using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace ns2
{
	// Token: 0x020000F0 RID: 240
	public static class GClass18
	{
		// Token: 0x0600044B RID: 1099
		[DllImport("kernel32.dll")]
		private static extern bool IsDebuggerPresent();

		// Token: 0x0600044C RID: 1100
		[DllImport("kernel32.dll", SetLastError = true)]
		private static extern IntPtr GetModuleHandle(string string_0);

		// Token: 0x0600044D RID: 1101
		[DllImport("kernel32.dll", SetLastError = true)]
		private static extern bool VirtualProtect(IntPtr intptr_0, uint uint_0, uint uint_1, out uint uint_2);

		// Token: 0x0600044E RID: 1102
		[DllImport("kernel32.dll", SetLastError = true)]
		private static extern bool CheckRemoteDebuggerPresent(IntPtr intptr_0, ref bool bool_0);

		// Token: 0x0600044F RID: 1103 RVA: 0x0000C303 File Offset: 0x0000A503
		public static void smethod_0()
		{
		}

		// Token: 0x06000450 RID: 1104 RVA: 0x0000C303 File Offset: 0x0000A503
		private static void smethod_1()
		{
		}

		// Token: 0x06000451 RID: 1105 RVA: 0x0000C303 File Offset: 0x0000A503
		public static void smethod_2()
		{
		}

		// Token: 0x06000452 RID: 1106 RVA: 0x0000C303 File Offset: 0x0000A503
		private static void smethod_3()
		{
		}

		// Token: 0x06000453 RID: 1107 RVA: 0x0000C303 File Offset: 0x0000A503
		private static void smethod_4(string string_0, string string_1)
		{
		}

		// Token: 0x06000454 RID: 1108 RVA: 0x0000DD22 File Offset: 0x0000BF22
		private static bool smethod_5()
		{
			return false;
		}

		// Token: 0x06000455 RID: 1109 RVA: 0x0000DD22 File Offset: 0x0000BF22
		private static bool smethod_6()
		{
			return false;
		}

		// Token: 0x06000456 RID: 1110 RVA: 0x0000DD25 File Offset: 0x0000BF25
		public static string smethod_7(string string_0)
		{
			return "";
		}

		// Token: 0x06000457 RID: 1111 RVA: 0x0000DD25 File Offset: 0x0000BF25
		public static string smethod_8(string string_0)
		{
			return "";
		}

		// Token: 0x06000458 RID: 1112 RVA: 0x0000DD25 File Offset: 0x0000BF25
		private static string smethod_9()
		{
			return "";
		}

		// Token: 0x06000459 RID: 1113 RVA: 0x0000DD22 File Offset: 0x0000BF22
		private static int smethod_10(int int_0, int int_1)
		{
			return 0;
		}

		// Token: 0x0600045A RID: 1114 RVA: 0x0000C303 File Offset: 0x0000A503
		private static void smethod_11()
		{
		}

		// Token: 0x0600045B RID: 1115 RVA: 0x0000DD22 File Offset: 0x0000BF22
		private static bool smethod_12(object object_0)
		{
			return false;
		}

		// Token: 0x0600045C RID: 1116 RVA: 0x0000C303 File Offset: 0x0000A503
		private static void smethod_13(string string_0)
		{
		}

		// Token: 0x0600045D RID: 1117 RVA: 0x0000C303 File Offset: 0x0000A503
		private static void smethod_14()
		{
		}

		// Token: 0x020000F1 RID: 241
		private class Class159
		{
			// Token: 0x0400033E RID: 830
			public GClass18.Class160 class160_0;
		}

		// Token: 0x020000F2 RID: 242
		private class Class160
		{
			// Token: 0x0400033F RID: 831
			public GClass18.Class159 class159_0;
		}

		// Token: 0x020000F3 RID: 243
		[CompilerGenerated]
		[Serializable]
		private sealed class Class161
		{
			// Token: 0x06000462 RID: 1122 RVA: 0x000393AC File Offset: 0x000375AC
			internal Task method_0()
			{
				GClass18.Class161.Struct22 @struct;
				@struct.asyncTaskMethodBuilder_0 = AsyncTaskMethodBuilder.Create();
				@struct.int_0 = -1;
				@struct.asyncTaskMethodBuilder_0.Start<GClass18.Class161.Struct22>(ref @struct);
				return @struct.asyncTaskMethodBuilder_0.Task;
			}

			// Token: 0x04000340 RID: 832
			public static readonly GClass18.Class161 class161_0 = new GClass18.Class161();

			// Token: 0x04000341 RID: 833
			public static Func<Task> func_0;

			// Token: 0x020000F4 RID: 244
			[StructLayout(LayoutKind.Auto)]
			private struct Struct22 : IAsyncStateMachine
			{
				// Token: 0x06000463 RID: 1123 RVA: 0x000393E8 File Offset: 0x000375E8
				void IAsyncStateMachine.MoveNext()
				{
					int num = this.int_0;
					try
					{
						if (num != 0)
						{
							goto IL_3F;
						}
						TaskAwaiter awaiter = this.taskAwaiter_0;
						this.taskAwaiter_0 = default(TaskAwaiter);
						this.int_0 = -1;
						IL_26:
						awaiter.GetResult();
						if (GClass18.smethod_5())
						{
							Environment.Exit(0);
						}
						GClass18.smethod_2();
						IL_3F:
						awaiter = Task.Delay(5000).GetAwaiter();
						if (awaiter.IsCompleted)
						{
							goto IL_26;
						}
						this.int_0 = 0;
						this.taskAwaiter_0 = awaiter;
						this.asyncTaskMethodBuilder_0.AwaitUnsafeOnCompleted<TaskAwaiter, GClass18.Class161.Struct22>(ref awaiter, ref this);
					}
					catch (Exception exception)
					{
						this.int_0 = -2;
						this.asyncTaskMethodBuilder_0.SetException(exception);
					}
				}

				// Token: 0x06000464 RID: 1124 RVA: 0x0000DD38 File Offset: 0x0000BF38
				[DebuggerHidden]
				void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
				{
					this.asyncTaskMethodBuilder_0.SetStateMachine(stateMachine);
				}

				// Token: 0x04000342 RID: 834
				public int int_0;

				// Token: 0x04000343 RID: 835
				public AsyncTaskMethodBuilder asyncTaskMethodBuilder_0;

				// Token: 0x04000344 RID: 836
				private TaskAwaiter taskAwaiter_0;
			}
		}
	}
}
