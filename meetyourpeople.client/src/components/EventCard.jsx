import "../styles/EventCard.css";
import { useNavigate } from "react-router-dom";
import PropTypes from "prop-types";

EventCard.propTypes = {
    event: PropTypes.shape({
        id: PropTypes.oneOfType([PropTypes.string, PropTypes.number]).isRequired,
        image: PropTypes.string.isRequired,
        title: PropTypes.string.isRequired,
        description: PropTypes.string,
    }).isRequired,
};

function EventCard({ event }) {
    const navigate = useNavigate();

    return (
        <div className="event-card">
            <img src={`${event.image}`} alt={event.title} />
            <h3>{event.title}</h3>
            <p>{event.description}</p>
            <button onClick={() => navigate(`/event/${event.id}`)}>
                Discover event
            </button>
        </div>
    );
}

export default EventCard;
