import type { ItemResponseDto } from '../types/item.types.ts';

interface ItemCardProps { 
    item: ItemResponseDto; 
    onSelect?: (id: number) => void; 
}

export function ItemCard({ item, onSelect }: ItemCardProps) { 
    const badgeColor = item.rarity === 'Legendario' ? 'bg-amber-500/20 text-amber-300 border-amber-500/40' : 
    item.rarity === 'Épico' ? 'bg-purple-500/20 text-purple-300 border-purple-500/40' : 
    item.rarity === 'Raro' ? 'bg-blue-500/20 text-blue-300 border-blue-500/40' : 
    'bg-slate-700/50 text-slate-300 border-slate-600';


    return (
  <article className="bg-slate-800/90 border border-slate-700 rounded-xl p-5 shadow-lg flex flex-col justify-between hover:border-slate-500 transition-all">
    <div>
      <div className="flex items-center justify-between mb-3">
        <span className={`text-xs px-2.5 py-1 rounded-md border font-semibold ${badgeColor}`}>
          {item.rarity}
        </span>
        <span className="text-xs text-slate-400 font-mono">ID #{item.id}</span>
      </div>
      <h3 className="text-lg font-bold text-white mb-1">{item.name}</h3>
      <p className="text-xs text-slate-400 mb-4">{item.category}</p>
    </div>

    <div className="pt-3 border-t border-slate-700/60 flex items-center justify-between">
      <div>
        <span className="text-xs text-slate-400 block">Precio Base</span>
        <span className="text-base font-bold text-amber-400 font-mono">
          💰 {item.basePrice.toFixed(2)} Gold
        </span>
      </div>
      <button
        onClick={() => onSelect?.(item.id)}
        className="bg-indigo-600 hover:bg-indigo-500 text-white text-xs font-semibold px-3 py-2 rounded-lg transition-colors"
      >
        Comprar
      </button>
    </div>
  </article>
);
}

