// src/App.tsx
import { AppLayout } from './layouts/AppLayout'
import { CatalogoMenusView } from './Features/Menus/Components/CatalogoMenusView'


function App() {
  return (
    <AppLayout>
      <CatalogoMenusView />
    </AppLayout>
  )
}

export default App
