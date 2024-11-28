import React, { useEffect, useState } from "react";
import EventCard from "../components/EventCard";
import { useNavigate } from "react-router-dom";
import "../styles/AllEventsPage.css";

function AllEventsPage({ outEvents, user }) {
  const navigate = useNavigate();

  const [events, setEvents] = useState([]); // Состояние для хранения событий
  const [loading, setLoading] = useState(true); // Состояние для отображения загрузки

  const handleCreateEvent = () => {
    if (user) {
      navigate("/create-event"); // Переход на страницу создания события
    } else {
      navigate("/login"); // Переход на страницу авторизации
    }
  };

  useEffect(() => {
    // Функция для получения данных с сервера
    const fetchEvents = async () => {
      try {
        //const response = outEvents;//await fetch('https://api.example.com/events'); // Укажите URL вашего API
        //const data = await response.json();
        setEvents(outEvents); // Сохраняем события в состоянии
      } catch (error) {
        console.error("Ошибка при загрузке событий:", error);
      } finally {
        setLoading(false); // Останавливаем отображение загрузки
      }
    };

    fetchEvents(); // Вызываем функцию при загрузке компонента
  }, []);

  if (loading) {
    return <p>Loading events...</p>; // Отображаем загрузку, пока данные не будут загружены
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
