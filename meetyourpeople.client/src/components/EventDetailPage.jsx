import { useState, useEffect  } from "react";
import { useNavigate, useParams } from "react-router-dom";
import PropTypes from "prop-types";
import "../styles/EventDetailPage.css";
import { useUser } from "../UserProvider";

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
};

function EventDetailPage({ events }) {
    const { id } = useParams();
    const { user } = useUser();
    const event = events.find((event) => event.id === parseInt(id));
    const [isJoined, setIsRegistered] = useState(false);
    const navigate = useNavigate();

    if (!event) {
        return <p>Event not found.</p>;
    }

    // Эффект для проверки, зарегистрирован ли пользователь
    //useEffect(() => {
    //    // Можно добавить логику для запроса с сервера, чтобы получить информацию, зарегистрирован ли пользователь на событие
    //    if (user && event) {
    //        // Пример: fetch для получения статуса участия пользователя
    //        // fetch(`/api/events/${event.id}/participants`, { headers: { Authorization: `Bearer ${token}` } })
    //        //     .then((res) => res.json())
    //        //     .then((data) => setIsRegistered(data.isRegistered))
    //    }
    //}, [user, event]);

    // Функция для присоединения/покидания события
    const handleJoinLeaveEvent = () => {
        if (!user) {
            navigate("/login");
            return;
        } else {
            // Пример запроса для присоединения/отмены регистрации
            // fetch(`/api/events/${event.id}/join`, {
            //     method: isJoined ? "DELETE" : "POST",
            //     headers: { Authorization: `Bearer ${user.token}` },
            // })
            //     .then((res) => res.json())
            //     .then((data) => {
            //         setIsRegistered(!isJoined); // Обновляем статус
            //     })
            //     .catch((error) => console.error("Error:", error));
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
