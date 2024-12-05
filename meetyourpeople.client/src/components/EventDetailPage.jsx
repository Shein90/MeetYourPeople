import { useState, useEffect } from "react";
import { useNavigate, useParams } from "react-router-dom";
import "../styles/EventDetailPage.css";
import { useUser } from "../user/UseUser";
import { useEvent } from "../event/UseEvent";


function EventDetailPage() {
    const { id } = useParams();
    const { user } = useUser();
    const { events, joinLeaveEvent } = useEvent();
    const [isJoined, setIsJoined] = useState(false);
    const navigate = useNavigate();

    const event = events.find((event) => event.id === parseInt(id));

    useEffect(() => {
        if (user && event) {
            // Логика для проверки, зарегистрирован ли пользователь
            // Пример: запрос на сервер для получения статуса регистрации
            // Например, мы используем `joinLeaveEvent` для проверки, зарегистрирован ли пользователь
            //setIsJoined(event.registered.includes(user.id)); // Это пример, нужно адаптировать под вашу логику
        }
    }, [user, event, events]);

    if (!event) {
        return <p>Event not found.</p>;
    }

    const handleJoinLeaveEvent = async () => {

        if (!user) {
            navigate("/login");
            return;
        }

        const isJoined = await joinLeaveEvent(event.id, user.id);

        setIsJoined(isJoined); // Обновляем локальный статус
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
                    <strong>Registered Participants:</strong> {event.participants}/{event.maxParticipants}
                </p>
                <button
                    onClick={handleJoinLeaveEvent}
                    disabled={user?.id === event.ownerId}>
                    {isJoined ? "Leave Event" : "Join Event"}
                </button>
            </div>
        </div>
    );
}

export default EventDetailPage;
