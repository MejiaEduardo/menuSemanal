// src/features/habitos/api/habitosApi.ts
import type { MenuGlobal } from '../types/Menus.types'


// Función asíncrona que retorna una Promesa con el arreglo de hábitos
export const obtenerMenusGlobalesAsync = (): Promise<MenuGlobal[]> => {
    return new Promise((resolve) => {
        setTimeout(() => {
            const datosCatalogo: MenuGlobal[] = [
            { Id: 1, Nombre: 'Menú semana 1' },
            { Id: 2, Nombre: 'Menú semana 2' },
            { Id: 3, Nombre: 'Menú semana 3' },
            { Id: 4, Nombre: 'Menú semana 4' },
            { Id: 5, Nombre: 'Menú familiar' },
            { Id: 6, Nombre: 'Menú Navideño' },
            ]
            resolve(datosCatalogo)
        }, 1000)
    })
}
