// frontend/src/features/habitos/schemas/habito.schema.ts
import { z } from 'zod'

// Artefacto Declarativo de Validación Sintáctica (Espejo de FluentValidation en .NET)
export const crearMenuGlobalSchema = z.object({
    nombre: z
        .string()
        .trim() // Sanitiza espacios en blanco al inicio y al final
        .min(1, 'El nombre del menú no puede estar vacío.')
        .min(3, 'El menú debe tener al menos 3 caracteres.')
        .max(100, 'El nombre no puede superar los 100 caracteres permitidos en PostgreSQL.')
        .regex(
            /^[a-zA-ZáéíóúÁÉÍÓÚñÑ0-9\s.,()-]+$/,
            'El menú contiene caracteres especiales no permitidos.'
        ),
})

export type CrearMenuGlobalSchemaType = z.infer<typeof crearMenuGlobalSchema>
