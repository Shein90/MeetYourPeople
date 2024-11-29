import { useEffect, useState } from "react";
import EventCard from "../components/EventCard";
import { useNavigate } from "react-router-dom";
import PropTypes from "prop-types";
import "../styles/AllEventsPage.css";

AllEventsPage.propTypes = {
    outEvents: PropTypes.arrayOf(
        PropTypes.shape({
            id: PropTypes.number.isRequired,
            title: PropTypes.string.isRequired,
            description: PropTypes.string.isRequired,
            image: PropTypes.string.isRequired,
            date: PropTypes.string.isRequired,
            time: PropTypes.string.isRequired,
            address: PropTypes.string.isRequired,
            maxParticipants: PropTypes.number.isRequired,
            registered: PropTypes.number.isRequired,
        })
    ).isRequired,
    user: PropTypes.shape({
        id: PropTypes.number,
        firstName: PropTypes.string,
        lastName: PropTypes.string,
    }),
};

function AllEventsPage({ outEvents, user }) {
    const navigate = useNavigate();

    const [events] = useState(outEvents);
    const [loading, setLoading] = useState(true);

    const handleCreateEvent = () => {
        if (user) {
            navigate("/create-event");
        } else {
            navigate("/login");
        }
    };

    useEffect(() => {

        const fetchEvents = async () => {
            try {
                //const response = outEvents;//await fetch('https://api.example.com/events');
                //const data = await response.json();

                //if (outEvents.length && events.length === 0) {
                //    setEvents(outEvents);
                //}

            } catch (error) {
                console.error("Error", error);
            } finally {
                setLoading(false);
            }
        };

        fetchEvents();
    }, [events]);

    if (loading) {
        return <p>Loading events...</p>;
    }

    return (
        <section className="all-events">
            <div className="header-container">
                <h1>All Events</h1>
                <button className="create-event-btn" onClick={handleCreateEvent}>
                    Create Event
                </button>
            </div>
            <div className="events-grid">
                {events.map((event, index) => (
                    <EventCard key={index} event={event} />
                ))}
            </div>
        </section>
    );
}

export default AllEventsPage;
