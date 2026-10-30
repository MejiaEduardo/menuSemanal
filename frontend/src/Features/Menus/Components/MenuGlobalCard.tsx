// src/components/MenuGlobalCard.tsx

// --- ZONA 1: EL CONTRATO (INTERFAZ) ---
// Actúa como un espejo exacto de nuestra tabla PostgreSQL / DTO en C#.
// Nuestra tabla actual solo tiene Id y Nombre.
 export interface MenuGlobalProps {
    Id: number;
    Nombre: string;
}

// --- ZONA 2: LA FIRMA (EXPORTACIÓN NOMBRADA) ---
// Desestructuramos las props cumpliendo el contrato de HabitoGlobalProps
export function MenusGlobalCard({ Id, Nombre }: MenuGlobalProps) {

    // --- ZONA 3: EL CEREBRO (LÓGICA) ---
    // (Vacío por los momentos, este componente es puramente presentacional)

    // --- ZONA 4: EL RENDERIZADO (JSX) ---
    return (
        <article className="habit-card" style={{ border: '1px solid #ccc', padding: '1rem', margin: '1rem', borderRadius: '8px' }}>
            <small style={{ color: 'gray' }}>ID Catálogo: {Id}</small>
            <h3>{Nombre}</h3>

            {/* 
               En la Unidad 3, cuando tengamos sistema de Usuarios (IAM), 
               este botón llamará a la tabla transaccional para adoptar el hábito.
            */}
            <button disabled style={{ cursor: 'not-allowed' }}>
                Adoptar Menu (Próximamente)
            </button>
        </article>
    );
}
