import { AppLayout } from './layouts/AppLayout';
import { ItemsCatalogView } from './features/items/views/ItemsCatalogView.tsx';

export default function App() {
  return (
    <AppLayout>
      <ItemsCatalogView />
    </AppLayout>
  );
}
