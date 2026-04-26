import shutil, os, sys
src = "c:\\Users\\rovki\\Downloads\\MAZA\\MZA_TUNER_FLASH_2026\\\u205a.2.cs"
dst = "c:\\Users\\rovki\\Desktop\\MzaTuner\\mainform_dump.cs"
print("src exists:", os.path.exists(src), "size:", os.path.getsize(src) if os.path.exists(src) else "n/a")
shutil.copy(src, dst)
print("copied to:", dst, "size:", os.path.getsize(dst))
