import EventCard from "./EventCard";
import "../styles/HomePageEvents.css";
import PropTypes from "prop-types";

HomePageEvents.propTypes = {
    events: PropTypes.arrayOf(
        PropTypes.shape({
            id: PropTypes.oneOfType([PropTypes.string, PropTypes.number]).isRequired,
            title: PropTypes.string.isRequired,
            description: PropTypes.string,
            image: PropTypes.string,
            date: PropTypes.string,
        })
    ).isRequired,
};

function HomePageEvents({ events }) {
    return (
        <section className="events">
            <h2>Upcoming Events</h2>
            <p>Recommended</p>
            <div className="event-grid">
                {events.map((event, index) => (
                    <EventCard key={index} event={event} />
                ))}
            </div>
        </section>
    );
}

export default HomePageEvents;
