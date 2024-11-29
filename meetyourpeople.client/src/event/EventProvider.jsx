import { createContext, useState, useEffect } from "react";
import PropTypes from "prop-types";

EventProvider.propTypes = {
    children: PropTypes.node.isRequired,
};

const initialEventsState = [{
    id: null,
    title: null,
    image: null,
    description: null,
    fullDescription: null,
    date: null,
    time: null,
    registered: null,
    maxParticipants: null,
    address: null,
}];

export const EventContext = createContext();

export const EventProvider = ({ children }) => {
    const [events, setEvents] = useState(initialEventsState);

    // Получаем список событий для отображения на главной странице
    const getEvents = async () => {
        try {
            const res = await fetch("/api/events");
            if (!res.ok) {
                throw new Error("Failed to fetch events");
            }
            const data = await res.json();
            setEvents(data); // Сохраняем события в контексте
        } catch (error) {
            console.error("Error fetching events:", error);
        }
    };

    // Получение событий при заходе на главную страницу
    useEffect(() => {
        getEvents();
    }, []);

    return (
        <EventContext.Provider value={{ events, getEvents }}>
            {children}
        </EventContext.Provider>
    );
};
