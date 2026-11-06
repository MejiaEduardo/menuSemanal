// src/features/menus/types/menus.types.ts

// Espejo exacto de nuestro menuGlobalDto del Backend en C#
export interface MenuGlobal {
    id: number;
    nombre: string;
}

// 2. Contrato de Escritura / Entrada (Payload que enviamos en el POST hacia .NET)
export interface CrearMenuGlobalDto {
    nombre: string;
}

// 3. Contrato de Error de FluentValidation (.NET ValidationFailure)
export interface FluentValidationError {
    propertyName: string;
    errorMessage: string;
    attemptedValue?: unknown;
    errorCode?: string;
}