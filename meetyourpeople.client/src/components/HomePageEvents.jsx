import EventCard from "./EventCard";
import "../styles/HomePageEvents.css";
import { useEvent } from "../event/UseEvent";
import { useState, useEffect } from "react";

function HomePageEvents() {
    const { events } = useEvent();
    const [shuffledEvents, setShuffledEvents] = useState([]);

    useEffect(() => {
        if (events.length > 0 && shuffledEvents.length === 0) {
            const shuffled = [...events];
            for (let i = shuffled.length - 1; i > 0; i--) {
                const j = Math.floor(Math.random() * (i + 1));
                [shuffled[i], shuffled[j]] = [shuffled[j], shuffled[i]];
            }
            setShuffledEvents(shuffled);
        }
    }, [events, shuffledEvents]);

    if (!events) {
        return <p>Loading events...</p>;
    }

    if (events.length === 0) {
        return <p>The events have not yet been created.</p>
    }

    const selectedEvents = shuffledEvents.slice(0, 6);

    return (
        <section className="events">
            <h2>Upcoming Events</h2>
            <p>Recommended</p>
            <div className="event-grid">
                {selectedEvents.map((event) => (
                    <EventCard key={event.id} event={event} />
                ))}
            </div>
        </section>
    );
}

export default HomePageEvents;
