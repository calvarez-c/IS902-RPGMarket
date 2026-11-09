import type { ReactNode } from 'react';

interface AppLayoutProps {
  children: ReactNode;
}

export function AppLayout({ children }: AppLayoutProps) {
  return (
    <div className="min-h-screen bg-slate-900 text-slate-100 flex flex-col md:flex-row">
      <aside className="w-full md:w-64 bg-slate-950 border-r border-slate-800 p-5 flex flex-col justify-between shrink-0">
        <div>
          <div className="flex items-center gap-3 mb-8">
            <div className="w-9 h-9 rounded-lg bg-amber-500 flex items-center justify-center font-black text-slate-950 text-xl shadow-lg shadow-amber-500/20">
              ⚔️
            </div>
            <span className="text-xl font-bold tracking-tight text-white">RPGMarket</span>
          </div>

          <nav className="space-y-1.5">
            <a href="#" className="flex items-center gap-3 px-3 py-2.5 bg-indigo-600/20 text-indigo-400 font-medium text-sm rounded-lg border border-indigo-500/30">
              <span>🛒</span> Mercado Global
            </a>

            <a href="#" className="flex items-center gap-3 px-3 py-2.5 text-slate-400 hover:bg-slate-800 font-medium text-sm rounded-lg transition-colors">
              <span>🎒</span> Mi Inventario
            </a>

            <a href="#" className="flex items-center gap-3 px-3 py-2.5 text-slate-400 hover:bg-slate-800 font-medium text-sm rounded-lg transition-colors">
              <span>📜</span> Historial
            </a>
          </nav>
        </div>
      </aside>

      <main className="flex-1 p-6 md:p-10 overflow-y-auto">
        {children}
      </main>
    </div>
  );
}
