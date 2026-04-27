using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace ns2
{
	// Token: 0x02000110 RID: 272
	[CompilerGenerated]
	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
	[DebuggerNonUserCode]
	internal class Class187
	{
		// Token: 0x17000027 RID: 39
		// (get) Token: 0x0600048B RID: 1163 RVA: 0x0000DDB7 File Offset: 0x0000BFB7
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static ResourceManager ResourceManager_0
		{
			get
			{
				if (Class187.resourceManager_0 == null)
				{
					Class187.resourceManager_0 = new ResourceManager("Honda_Flash_Tools.Net.Properties.Resources", typeof(Class187).Assembly);
				}
				return Class187.resourceManager_0;
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600048C RID: 1164 RVA: 0x0000DDE3 File Offset: 0x0000BFE3
		// (set) Token: 0x0600048D RID: 1165 RVA: 0x0000DDEA File Offset: 0x0000BFEA
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static CultureInfo CultureInfo_0
		{
			get
			{
				return Class187.cultureInfo_0;
			}
			set
			{
				Class187.cultureInfo_0 = value;
			}
		}

		// Token: 0x0600048E RID: 1166 RVA: 0x0000DDF2 File Offset: 0x0000BFF2
		internal static Bitmap smethod_0()
		{
			return (Bitmap)Class187.ResourceManager_0.GetObject("Green-Button-PNG", Class187.cultureInfo_0);
		}

		// Token: 0x0600048F RID: 1167 RVA: 0x0000DE0D File Offset: 0x0000C00D
		internal static Bitmap smethod_1()
		{
			return (Bitmap)Class187.ResourceManager_0.GetObject("honda-motorcycles-logo-wing-10", Class187.cultureInfo_0);
		}

		// Token: 0x06000490 RID: 1168 RVA: 0x0000DE28 File Offset: 0x0000C028
		internal static Bitmap smethod_2()
		{
			return (Bitmap)Class187.ResourceManager_0.GetObject("tik", Class187.cultureInfo_0);
		}

		// Token: 0x06000491 RID: 1169 RVA: 0x0000C380 File Offset: 0x0000A580
		internal Class187()
		{
		}

		// Token: 0x04000350 RID: 848
		private static ResourceManager resourceManager_0;

		// Token: 0x04000351 RID: 849
		private static CultureInfo cultureInfo_0;
	}
}
