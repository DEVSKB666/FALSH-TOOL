# Generates a 1024x1024 PNG that we then feed to `tauri icon` so it can
# emit all the platform-specific icon sizes/formats Tauri needs.
#
# The icon is drawn programmatically with System.Drawing - no external
# assets, no internet required.

param(
    [string]$Out = "tools\logo.png",
    [int]$Size   = 1024
)

Add-Type -AssemblyName System.Drawing

$bmp = New-Object System.Drawing.Bitmap $Size, $Size
$g   = [System.Drawing.Graphics]::FromImage($bmp)
$g.SmoothingMode     = [System.Drawing.Drawing2D.SmoothingMode]::AntiAlias
$g.InterpolationMode = [System.Drawing.Drawing2D.InterpolationMode]::HighQualityBicubic
$g.TextRenderingHint = [System.Drawing.Text.TextRenderingHint]::AntiAliasGridFit

# Background - rounded square with red gradient
$pad = [int]($Size * 0.08)
$rect = New-Object System.Drawing.Rectangle $pad, $pad, ($Size - 2 * $pad), ($Size - 2 * $pad)

$gp = New-Object System.Drawing.Drawing2D.GraphicsPath
$radius = [int]($Size * 0.18)
$d = $radius * 2
$gp.AddArc($rect.X, $rect.Y, $d, $d, 180, 90)
$gp.AddArc($rect.Right - $d, $rect.Y, $d, $d, 270, 90)
$gp.AddArc($rect.Right - $d, $rect.Bottom - $d, $d, $d, 0, 90)
$gp.AddArc($rect.X, $rect.Bottom - $d, $d, $d, 90, 90)
$gp.CloseFigure()

$gradient = New-Object System.Drawing.Drawing2D.LinearGradientBrush(
    (New-Object System.Drawing.PointF $rect.X, $rect.Y),
    (New-Object System.Drawing.PointF $rect.Right, $rect.Bottom),
    [System.Drawing.Color]::FromArgb(255, 220, 38,  38),
    [System.Drawing.Color]::FromArgb(255, 120,  0,   0))
$g.FillPath($gradient, $gp)

# Subtle inner glow
$inner = New-Object System.Drawing.Pen ([System.Drawing.Color]::FromArgb(80, 255, 255, 255)), 6
$g.DrawPath($inner, $gp)

# Lightning bolt - centered, white
$bolt = @(
    [System.Drawing.PointF]::new($Size * 0.55,  $Size * 0.18),
    [System.Drawing.PointF]::new($Size * 0.30,  $Size * 0.55),
    [System.Drawing.PointF]::new($Size * 0.46,  $Size * 0.55),
    [System.Drawing.PointF]::new($Size * 0.40,  $Size * 0.84),
    [System.Drawing.PointF]::new($Size * 0.72,  $Size * 0.42),
    [System.Drawing.PointF]::new($Size * 0.55,  $Size * 0.42),
    [System.Drawing.PointF]::new($Size * 0.62,  $Size * 0.18)
)
$g.FillPolygon([System.Drawing.Brushes]::White, $bolt)

$bmp.Save($Out, [System.Drawing.Imaging.ImageFormat]::Png)
$g.Dispose()
$bmp.Dispose()

Write-Host "[+] wrote $Out  ($($Size)x$Size PNG)"
