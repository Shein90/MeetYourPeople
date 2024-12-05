import EventCard from "../components/EventCard";
import { useNavigate } from "react-router-dom";
import { useUser } from "../user/UseUser";
import { useEvent } from "../event/UseEvent";
import "../styles/AllEventsPage.css";

function AllEventsPage() {
    const navigate = useNavigate();
    const { user } = useUser();
    const { events } = useEvent();

    const handleCreateEvent = () => {
        if (user) {
            navigate("/create-event");
        } else {
            navigate("/login");
        }
    };

    return (
        <section className="all-events">
            <div className="header-container">
                <h1>All Events</h1>
                <button className="create-event-btn" onClick={handleCreateEvent}>
                    Create Event
                </button>
            </div>
            {events.length === 0 ? (
                <p>The events have not yet been created.</p>
            ) : (
                <div className="events-grid">
                    {events.map((event) => (
                        <EventCard key={event.id} event={event} />
                    ))}
                </div>
            )}
        </section>
    );
}

export default AllEventsPage;
