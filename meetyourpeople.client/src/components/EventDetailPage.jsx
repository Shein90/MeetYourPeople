import { useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import PropTypes from "prop-types";
import "../styles/EventDetailPage.css";

EventDetailPage.propTypes = {
    events: PropTypes.arrayOf(
        PropTypes.shape({
            id: PropTypes.oneOfType([PropTypes.string, PropTypes.number]).isRequired,
            image: PropTypes.string.isRequired,
            title: PropTypes.string.isRequired,
            description: PropTypes.string,
            fullDescription: PropTypes.string,
            date: PropTypes.string.isRequired,
            time: PropTypes.string.isRequired,
            address: PropTypes.string.isRequired,
            registered: PropTypes.number.isRequired,
            maxParticipants: PropTypes.number.isRequired,
        })
    ).isRequired,
    user: PropTypes.shape({
        id: PropTypes.oneOfType([PropTypes.string, PropTypes.number]),
        name: PropTypes.string,
    }),
};

function EventDetailPage({ events, user }) {
    const { id } = useParams();
    const event = events.find((event) => event.id === parseInt(id));
    const [isJoined, setIsRegistered] = useState(false);
    const navigate = useNavigate();

    if (!event) {
        return <p>Event not found.</p>;
    }

    const handleJoinLeaveEvent = () => {
        if (!user) {
            navigate("/login");
            return;
        } else {
            //Вот тут вероятно надо сделать запрос на сервер
        }

        if (isJoined) {
            setIsRegistered(false);
        } else {
            setIsRegistered(true);
        }
    };

    return (
        <div className="event-detail">
            <img src={event.image} alt={event.title} />
            <div className="description">
                <h1>{event.title}</h1>
                <p>{event.description}</p>
                <p>
                    <strong>Full Description:</strong> {event.fullDescription}
                </p>
                <p>
                    <strong>Date & Time:</strong> {event.date} at {event.time}
                </p>
                <p>
                    <strong>Address:</strong> {event.address}
                </p>
                <p>
                    <strong>Registered Participants:</strong> {event.registered} /{" "}
                    {event.maxParticipants}
                </p>
                <button onClick={handleJoinLeaveEvent}>
                    {isJoined ? "Leave Event" : "Join Event"}
                </button>
            </div>
        </div>
    );
}

export default EventDetailPage;
