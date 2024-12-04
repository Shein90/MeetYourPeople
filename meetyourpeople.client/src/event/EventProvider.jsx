import { createContext, useState, useEffect } from "react";
import PropTypes from "prop-types";

const initialEventsState = [];

export const EventContext = createContext();

export const EventProvider = ({ children }) => {
    const [events, setEvents] = useState(initialEventsState);

    const getEvents = async () => {
        try {
            const res = await fetch("/api/events");
            if (!res.ok) {
                throw new Error("Failed to fetch events");
            }
            const data = await res.json();
            setEvents(data);
        } catch (error) {
            console.error("Error fetching events:", error);
        }
    };

    const createEvent = async (eventData) => {

        const formData = new FormData();
        formData.append("ownerId", parseInt(eventData.ownerId, 10));
        formData.append("title", eventData.title);
        formData.append("description", eventData.description);
        formData.append("detailedDescription", eventData.detailedDescription);
        formData.append("date", eventData.date);
        formData.append("time", eventData.time);
        formData.append("address", eventData.address);
        formData.append("maxParticipants", eventData.maxParticipants);
        formData.append("eventImage", eventData.eventImage);

        const response = await fetch("/api/events", {
            method: "POST",
            body: formData,
        });

        if (!response.ok) {
            throw new Error("Error creating event");
        }

        //const newEvent = await response.json();
        //setEvents((prevEvents) => [...prevEvents, newEvent]);
    };

    // Функция для присоединения/покидания события
    const joinLeaveEvent = (eventId, userId, isCurrentlyRegistered) => {
        setEvents((prevEvents) =>
            prevEvents.map((event) =>
                event.id === eventId
                    ? {
                        ...event,
                        registered: isCurrentlyRegistered
                            ? event.registered.filter((id) => id !== userId) // Удаляем пользователя
                            : [...event.registered, userId], // Добавляем пользователя
                    }
                    : event
            )
        );
    };

    // Получение событий при заходе на главную страницу
    useEffect(() => {
        getEvents();
    }, []);

    return (
        <EventContext.Provider value={{ events, getEvents, createEvent, joinLeaveEvent }}>
            {children}
        </EventContext.Provider>
    );
};

EventProvider.propTypes = {
    children: PropTypes.node.isRequired,
};