import axios from 'axios';
import type { ItemResponseDto, CreateItemRequestDto } from '../types/item.types';

export const apiClient = axios.create({
  baseURL: import.meta.env.VITE_API_BASE_URL,
  headers: { 'Content-Type': 'application/json' },
});

export const obtenerItemsAsync = async (): Promise<ItemResponseDto[]> => {
  const respuesta = await apiClient.get<ItemResponseDto[]>('/items');
  return respuesta.data;
};

export const crearItemAsync = async (nuevoItem: CreateItemRequestDto): Promise<ItemResponseDto> => {
  const respuesta = await apiClient.post<ItemResponseDto>('/items', nuevoItem);
  return respuesta.data;
};

export const extraerMensajeErrorBackend = (error: unknown): string => {
  if (axios.isAxiosError(error)) {
    if (!error.response) return 'No hay respuesta del servidor (.NET apagado o bloqueo CORS).';
    
    const data = error.response.data;
    
    if (Array.isArray(data)) {
      return data
        .map((e: { errorMessage?: string }) => e.errorMessage)
        .filter(Boolean)
        .join(' | ');
    }
    
    if (typeof data === 'string') return data;
  }
  
  return 'Ocurrió un error inesperado al procesar la solicitud.';
};
