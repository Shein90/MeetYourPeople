import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import App from './App.jsx'
import './styles/index.css'
import { UserProvider } from './user/UserProvider.jsx';
import { EventProvider } from './event/EventProvider.jsx';

createRoot(document.getElementById('root')).render(
    <StrictMode>
        <UserProvider>
            <EventProvider>
                <App />
            </EventProvider>
        </UserProvider>
    </StrictMode>,
);