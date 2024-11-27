import React, { useState } from "react";
import { useNavigate, useParams } from "react-router-dom"; // Для получения ID события из URL
import "../styles/EventDetailPage.css";

function EventDetailPage({ events, user }) {
  const { id } = useParams(); // Получаем ID события из URL
  const event = events.find((event) => event.id === parseInt(id)); // Ищем событие по ID
  const [isRegistered, setIsRegistered] = useState(false);
  const navigate = useNavigate();

  if (!event) {
    return <p>Event not found.</p>; // Если событие не найдено
  }

  // Обработчик для присоединения/покидания события
  const handleJoinLeaveEvent = () => {
    if (!user) {
      return (
        <div>
          <p>You need to be logged in to join the event.</p>
          <button onClick={() => navigate("/login")}>Go to Login</button>
        </div>
      );
    }

    if (isRegistered) {
      setIsRegistered(false);
      alert("You have left the event.");
    } else {
      setIsRegistered(true);
      alert("You have joined the event!");
    }
  };

  return (
    <div className="event-detail">
      <img src={event.image} alt={event.title} />
      <div>
        <h1>{event.title}</h1>
        <p>{event.description}</p>
        <p>
          <strong>Full Description:</strong> {event.fullDescription}
        </p>
        <p>
          <strong>Date & Time:</strong> {event.date} at {event.time}
        </p>
        <p>
          <strong>Registered Participants:</strong> {event.registered} /{" "}
          {event.maxParticipants}
        </p>
        <button onClick={handleJoinLeaveEvent}>
          {isRegistered ? "Leave Event" : "Join Event"}
        </button>
      </div>
    </div>
  );
}

export default EventDetailPage;
