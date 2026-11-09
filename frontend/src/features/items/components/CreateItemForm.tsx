import { useForm } from 'react-hook-form';
import { zodResolver } from '@hookform/resolvers/zod';
import { createItemSchema, type CreateItemSchemaType } from '../schemas/item.schemas.ts';

interface CreateItemFormProps {
  onSuccess: (item: CreateItemSchemaType) => Promise<void>;
  onCancel: () => void;
  errorServidor?: string | null;
}

export function CreateItemForm({ onSuccess, onCancel, errorServidor }: CreateItemFormProps) {
  const { register, handleSubmit, formState: { errors, isSubmitting } } = useForm<CreateItemSchemaType>({
    resolver: zodResolver(createItemSchema),
    mode: 'onChange',
  });

  return (
    <form onSubmit={handleSubmit(onSuccess)} className="bg-slate-800 border border-slate-700 rounded-xl p-6 mb-8 space-y-4">
      <h3 className="text-lg font-bold text-white">Registrar Nuevo Objeto RPG</h3>

      {errorServidor && (
        <div className="bg-red-950/80 border border-red-500/50 text-red-300 p-3 rounded-lg text-xs">
          🚨 <strong>Rechazo del Servidor (.NET):</strong> {errorServidor}
        </div>
      )}

      <div>
        <label className="block text-xs font-semibold text-slate-300 mb-1">Nombre del Objeto</label>
        <input 
          {...register('name')} 
          className="w-full bg-slate-900 border border-slate-700 rounded-lg px-3 py-2 text-sm text-white focus:outline-none focus:border-indigo-500"
        />
        {errors.name && <p className="text-xs text-red-400 mt-1">{errors.name.message}</p>}
      </div>

      <div className="grid grid-cols-2 gap-4">
        <div>
          <label className="block text-xs font-semibold text-slate-300 mb-1">Categoría</label>
          <select 
            {...register('category')} 
            className="w-full bg-slate-900 border border-slate-700 rounded-lg px-3 py-2 text-sm text-white focus:outline-none focus:border-indigo-500"
          >
            <option value="Arma">Arma</option>
            <option value="Armadura">Armadura</option>
            <option value="Consumible">Consumible</option>
            <option value="Material">Material</option>
          </select>
        </div>

        <div>
          <label className="block text-xs font-semibold text-slate-300 mb-1">Rareza</label>
          <select 
            {...register('rarity')} 
            className="w-full bg-slate-900 border border-slate-700 rounded-lg px-3 py-2 text-sm text-white focus:outline-none focus:border-indigo-500"
          >
            <option value="Común">Común</option>
            <option value="Raro">Raro</option>
            <option value="Épico">Épico</option>
            <option value="Legendario">Legendario</option>
          </select>
        </div>
      </div>

      <div>
        <label className="block text-xs font-semibold text-slate-300 mb-1">Precio Base (Gold)</label>
        <input 
          type="number" 
          step="0.01" 
          {...register('basePrice', { valueAsNumber: true })} 
          className="w-full bg-slate-900 border border-slate-700 rounded-lg px-3 py-2 text-sm text-white focus:outline-none focus:border-indigo-500"
        />
        {errors.basePrice && <p className="text-xs text-red-400 mt-1">{errors.basePrice.message}</p>}
      </div>

      <div className="flex justify-end gap-3 pt-2">
        <button 
          type="button" 
          onClick={onCancel} 
          className="px-4 py-2 bg-slate-700 hover:bg-slate-600 text-slate-200 text-xs font-semibold rounded-lg transition-colors"
        >
          Cancelar
        </button>
        <button 
          type="submit" 
          disabled={isSubmitting} 
          className="px-4 py-2 bg-indigo-600 hover:bg-indigo-500 text-white text-xs font-semibold rounded-lg transition-colors disabled:opacity-50"
        >
          {isSubmitting ? 'Guardando...' : 'Guardar en PostgreSQL'}
        </button>
      </div>
    </form>
  );
}
