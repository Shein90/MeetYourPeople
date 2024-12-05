import { useState, useEffect } from "react";
import { useNavigate, useParams } from "react-router-dom";
import "../styles/EventDetailPage.css";
import { useUser } from "../user/UseUser";
import { useEvent } from "../event/UseEvent";

function EventDetailPage() {
    const { id } = useParams();
    const { user } = useUser();
    const { events, joinLeaveEvent, deleteEvent } = useEvent();
    const [isJoined, setIsJoined] = useState(false);
    const navigate = useNavigate();

    const event = events.find((event) => event.id === parseInt(id));

    useEffect(() => {
        if (user && event) {
            if (user) {
                setIsJoined(user.eventsIds.includes(event?.id));
            }
        }
    }, [user, event]);

    if (!event) {
        return <p>Event not found.</p>;
    }

    const handleJoinLeaveEvent = async () => {

        if (!user) {
            navigate("/login");
            return;
        }

        try {
            await joinLeaveEvent(event.id, user.id, isJoined);

            setIsJoined(!isJoined);
        }
        catch (error) {
            alert('Error while handling event registration:', error)
        }
    };

    const handleDeleteEvent = async () => {

        if (!user) {
            throw Error("Unauthorised user cannot delete events!");
        }

        try {
            await deleteEvent(event.id);

            navigate("/events");
        }
        catch (error) {
            alert('Error while deleting event:', error)
        }
    };

    return (
        <div className="event-detail">
            <img src={event.eventImageUrl} alt={event.title} />
            <div className="description">
                <h1>{event.title}</h1>
                <p>{event.description}</p>
                <p>
                    <strong>Full Description:</strong> {event.detailedDescription}
                </p>
                <p>
                    <strong>Date & Time:</strong> {event.date} at {event.time}
                </p>
                <p>
                    <strong>Address:</strong> {event.address}
                </p>
                <p>
                    <strong>Event owner:</strong> {event.ownerName}
                </p>
                <p>
                    <strong>Registered Participants:</strong> {event.participants + isJoined}/{event.maxParticipants}
                </p>
                <button
                    className={user?.id === event.ownerId ? 'delete-event' : 'loin-leave'}
                    onClick={user?.id === event.ownerId ? handleDeleteEvent : handleJoinLeaveEvent}>
                    {user?.id === event.ownerId
                        ? "Delete event"
                        : isJoined
                            ? "Leave Event"
                            : "Join Event"}
                </button>
            </div>
        </div>
    );
}

export default EventDetailPage;
