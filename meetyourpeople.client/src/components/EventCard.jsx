import '../styles/EventCard.css';

function EventCard({event}) {
    return (
        <div className="event-card">
            <div>
            <img src={`${event.image}`} alt={event.title} />
            </div>
            <h3>{event.title}</h3>
            <p>{event.description}</p>
            <button>Discover event</button>
        </div>
    );
}

export default EventCard;