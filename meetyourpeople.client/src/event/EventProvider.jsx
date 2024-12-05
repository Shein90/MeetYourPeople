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
        const token = localStorage.getItem('token'); 

        const formData = new FormData();
        formData.append("ownerId", parseInt(eventData.ownerId, 10));
        formData.append("ownerName", eventData.ownerName);
        formData.append("title", eventData.title);
        formData.append("description", eventData.description);
        formData.append("detailedDescription", eventData.detailedDescription);
        formData.append("date", eventData.date);
        formData.append("time", eventData.time);
        formData.append("address", eventData.address);
        formData.append("maxParticipants", eventData.maxParticipants);
        formData.append("participants", 1);
        formData.append("eventImage", eventData.eventImage);
        formData.append("eventImageUrl", "");

        const response = await fetch("/api/events", {
            method: "POST",
            body: formData,
            headers: {
                'Authorization': `Bearer ${token}`
            },
        });

        if (response.ok) {
            await getEvents();
        } else {
            throw new Error("Error creating event");
        }
    };

    const joinLeaveEvent = async (eventId, userId, isJoined) => {

        const token = localStorage.getItem('token'); 

        const endpoint = isJoined
            ? `/api/events/${eventId}/leave`
            : `/api/events/${eventId}/join`;

        const response = await fetch(endpoint, {
            method: isJoined ? 'DELETE' : 'POST',
            headers: {
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${token}`
            },
            body: JSON.stringify({ userId: userId }),
        });

        if (!response.ok) {
            throw new Error('Failed to join or leave the event');
        }
    };

    const deleteEvent = async (eventId) => {

        const token = localStorage.getItem('token');

        const endpoint = `/api/events/${eventId}`;

        const response = await fetch(endpoint, {
            method: 'DELETE',
            headers: {
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${token}`
            },
        });

        if (response.ok) {
            await getEvents(); 
        } else {
            throw new Error('Failed to delete the event');
        }
    };

    useEffect(() => {
        getEvents();
    }, []);

    return (
        <EventContext.Provider value={{ events, getEvents, createEvent, joinLeaveEvent, deleteEvent }}>
            {children}
        </EventContext.Provider>
    );
};

EventProvider.propTypes = {
    children: PropTypes.node.isRequired,
};