import { z } from 'zod';


export const createItemSchema = z.object({
  name: z.string()
    .trim()
    .min(1, 'El nombre no puede estar vacío.')
    .min(3, 'El nombre debe tener al menos 3 caracteres.')
    .max(100, 'El nombre no puede superar 100 caracteres.'),
  category: z.enum(['Arma', 'Armadura', 'Consumible', 'Material'] as const, {
    message: 'Categoría inválida.',
  }),

  rarity: z.enum(['Común', 'Raro', 'Épico', 'Legendario'] as const, {
    message: 'Rareza inválida.',
  }),
  
  basePrice: z
    .number({ message: 'Debe ingresar un número.' })
    .gt(0, 'El precio base debe ser mayor a cero.'),
  imageUrl: z.string().optional(),
});

export type CreateItemSchemaType = z.infer<typeof createItemSchema>;
