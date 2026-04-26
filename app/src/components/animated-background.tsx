"use client";

import { useEffect, useRef } from "react";
import { useSettings } from "@/lib/settings";

/**
 * Ambient animated background. The user can switch between several presets
 * from the Settings page; each preset renders into its own layer so the
 * transition is just CSS opacity.
 */
export function AnimatedBackground() {
  const bg = useSettings((s) => s.background);

  return (
    <div
      aria-hidden
      className="pointer-events-none fixed inset-0 z-0 overflow-hidden bg-background"
    >
      {bg === "particles" && <ParticlesLayer />}
      {bg === "circuit" && <CircuitLayer />}
      {bg === "scanlines" && <ScanlinesLayer />}
      {bg === "aurora" && <AuroraLayer />}
      {/* "off" -> render nothing extra, only the solid background colour */}
    </div>
  );
}

/* -------------------------------------------------------------- */
/*  particles - canvas particle field with light parallax           */
/* -------------------------------------------------------------- */

function ParticlesLayer() {
  const canvasRef = useRef<HTMLCanvasElement>(null);

  useEffect(() => {
    const canvas = canvasRef.current!;
    const ctx = canvas.getContext("2d")!;
    let raf = 0;

    const dpr = Math.max(1, window.devicePixelRatio || 1);
    const resize = () => {
      canvas.width = window.innerWidth * dpr;
      canvas.height = window.innerHeight * dpr;
      canvas.style.width = window.innerWidth + "px";
      canvas.style.height = window.innerHeight + "px";
      ctx.scale(dpr, dpr);
    };
    resize();
    window.addEventListener("resize", resize);

    type P = { x: number; y: number; vx: number; vy: number; r: number };
    const N = Math.min(120, Math.floor((window.innerWidth * window.innerHeight) / 14_000));
    const ps: P[] = Array.from({ length: N }, () => ({
      x: Math.random() * window.innerWidth,
      y: Math.random() * window.innerHeight,
      vx: (Math.random() - 0.5) * 0.25,
      vy: (Math.random() - 0.5) * 0.25,
      r: Math.random() * 1.6 + 0.4,
    }));

    const tick = () => {
      const w = window.innerWidth;
      const h = window.innerHeight;
      ctx.clearRect(0, 0, w, h);

      // Resolve --primary CSS var so the particle hue follows the theme.
      const styles = getComputedStyle(document.documentElement);
      const primary = styles.getPropertyValue("--primary").trim() || "0 92% 50%";

      // Draw connecting lines between near neighbours
      for (let i = 0; i < ps.length; i++) {
        const a = ps[i];
        for (let j = i + 1; j < ps.length; j++) {
          const b = ps[j];
          const dx = a.x - b.x;
          const dy = a.y - b.y;
          const d2 = dx * dx + dy * dy;
          if (d2 < 140 * 140) {
            const alpha = (1 - Math.sqrt(d2) / 140) * 0.18;
            ctx.strokeStyle = `hsla(${primary} / ${alpha})`;
            ctx.lineWidth = 0.6;
            ctx.beginPath();
            ctx.moveTo(a.x, a.y);
            ctx.lineTo(b.x, b.y);
            ctx.stroke();
          }
        }
      }

      // Draw + advance particles
      for (const p of ps) {
        p.x += p.vx;
        p.y += p.vy;
        if (p.x < -10) p.x = w + 10;
        if (p.x > w + 10) p.x = -10;
        if (p.y < -10) p.y = h + 10;
        if (p.y > h + 10) p.y = -10;
        ctx.fillStyle = `hsla(${primary} / 0.5)`;
        ctx.beginPath();
        ctx.arc(p.x, p.y, p.r, 0, Math.PI * 2);
        ctx.fill();
      }

      raf = requestAnimationFrame(tick);
    };
    raf = requestAnimationFrame(tick);
    return () => {
      window.removeEventListener("resize", resize);
      cancelAnimationFrame(raf);
    };
  }, []);

  return (
    <>
      <canvas ref={canvasRef} className="absolute inset-0" />
      <div className="absolute inset-0 bg-radial-fade" />
    </>
  );
}

/* -------------------------------------------------------------- */
/*  circuit - subtle grid + floating "pulse" overlays              */
/* -------------------------------------------------------------- */

function CircuitLayer() {
  return (
    <>
      <div className="absolute inset-0 bg-grid opacity-60" />
      <div className="absolute -top-24 left-1/2 h-72 w-72 -translate-x-1/2 rounded-full bg-primary/30 blur-3xl animate-glow" />
      <div className="absolute bottom-0 right-1/4 h-60 w-60 rounded-full bg-accent/30 blur-3xl animate-glow [animation-delay:1.2s]" />
      <div className="absolute inset-0 bg-radial-fade" />
    </>
  );
}

/* -------------------------------------------------------------- */
/*  scanlines - retro CRT vibe                                     */
/* -------------------------------------------------------------- */

function ScanlinesLayer() {
  return (
    <>
      <div className="absolute inset-0 bg-grid opacity-40" />
      <div
        className="absolute inset-0 opacity-20"
        style={{
          backgroundImage:
            "repeating-linear-gradient(to bottom, transparent 0 2px, rgba(255,255,255,0.04) 2px 3px)",
        }}
      />
      <div className="absolute left-0 right-0 h-32 bg-gradient-to-b from-primary/20 to-transparent animate-scan-line" />
      <div className="absolute inset-0 bg-radial-fade" />
    </>
  );
}

/* -------------------------------------------------------------- */
/*  aurora - flowing gradient blobs                                */
/* -------------------------------------------------------------- */

function AuroraLayer() {
  return (
    <>
      <div className="absolute -inset-32 opacity-70">
        <div className="absolute left-1/4 top-1/4 h-[32rem] w-[32rem] rounded-full bg-primary/30 blur-3xl animate-glow" />
        <div className="absolute right-1/4 top-1/3 h-[28rem] w-[28rem] rounded-full bg-accent/30 blur-3xl animate-glow [animation-delay:1.5s]" />
        <div className="absolute bottom-0 left-1/3 h-[26rem] w-[26rem] rounded-full bg-secondary/40 blur-3xl animate-glow [animation-delay:2.4s]" />
      </div>
      <div className="absolute inset-0 bg-radial-fade" />
    </>
  );
}
