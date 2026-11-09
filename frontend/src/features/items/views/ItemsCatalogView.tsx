import { useState, useEffect } from 'react';
import type { ItemResponseDto } from '../types/item.types';
import { obtenerItemsAsync, crearItemAsync, extraerMensajeErrorBackend } from '../api/itemsApi';
import { ItemCard } from '../components/ItemCard';
import { CreateItemForm } from '../components/CreateItemForm';
import type { CreateItemSchemaType } from '../schemas/item.schemas.ts';

export function ItemsCatalogView() {
  const [items, setItems] = useState<ItemResponseDto[]>([]);
  const [cargando, setCargando] = useState(true);
  const [errorRed, setErrorRed] = useState<string | null>(null);
  const [mostrarForm, setMostrarForm] = useState(false);
  const [errorForm, setErrorForm] = useState<string | null>(null);

  const cargarItems = async () => {
    try {
      setCargando(true);
      const datos = await obtenerItemsAsync();
      setItems(datos);
      setErrorRed(null);
    } catch (err) {
      setErrorRed(extraerMensajeErrorBackend(err));
    } finally {
      setCargando(false);
    }
  };

  useEffect(() => {
    cargarItems();
  }, []);

  const handleCrearItem = async (values: CreateItemSchemaType) => {
    try {
      setErrorForm(null);
      const nuevoItem = await crearItemAsync(values);
      setItems((prev) => [...prev, nuevoItem]); // Actualización inmutable en RAM
      setMostrarForm(false);
    } catch (err) {
      setErrorForm(extraerMensajeErrorBackend(err));
    }
  };

  return (
    <div className="space-y-6">
      <div className="flex flex-col sm:flex-row sm:items-center justify-between gap-4 pb-4 border-b border-slate-800">
        <div>
          <h1 className="text-2xl font-bold text-white">Catálogo Global de Objetos</h1>
          <p className="text-xs text-slate-400 mt-1">Gestión centralizada del mercado RPG conectado a PostgreSQL</p>
        </div>
        {!mostrarForm && (
          <button
            onClick={() => setMostrarForm(true)}
            className="bg-indigo-600 hover:bg-indigo-500 text-white text-xs font-semibold px-4 py-2.5 rounded-lg shadow-lg shadow-indigo-600/20 transition-all self-start sm:self-auto"
          >
            + Registrar Objeto
          </button>
        )}
      </div>

      {mostrarForm && (
        <CreateItemForm
          onSuccess={handleCrearItem}
          onCancel={() => setMostrarForm(false)}
          errorServidor={errorForm}
        />
      )}

      {errorRed && (
        <div className="bg-red-950/80 border border-red-500/50 p-4 rounded-xl text-center">
          <p className="text-red-400 text-sm font-semibold">⚠️ Fallo de Integración Full-Stack</p>
          <p className="text-slate-300 text-xs mt-1">{errorRed}</p>
        </div>
      )}

      {cargando ? (
        <div className="text-center py-12 text-slate-400 text-sm animate-pulse">
          ⏳ Cargando objetos desde PostgreSQL vía REST API...
        </div>
      ) : (
        <div className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 gap-5">
          {items.map((item) => (
            <ItemCard key={item.id} item={item} />
          ))}
        </div>
      )}
    </div>
  );
}
