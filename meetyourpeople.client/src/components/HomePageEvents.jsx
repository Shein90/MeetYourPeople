import EventCard from "./EventCard";
import { useEffect } from "react";
import "../styles/HomePageEvents.css";
import { useEvent } from "../hooks/useEvent"; 

function HomePageEvents() {
    const { events, getEvents } = useEvent();

    useEffect(() => {
        getEvents();
    }, [getEvents]);

    if (!events || events.length === 0) {
        return <p>Loading events...</p>;
    }

    const shuffledEvents = [...events];
    for (let i = shuffledEvents.length - 1; i > 0; i--) {
        const j = Math.floor(Math.random() * (i + 1));
        [shuffledEvents[i], shuffledEvents[j]] = [shuffledEvents[j], shuffledEvents[i]];
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
