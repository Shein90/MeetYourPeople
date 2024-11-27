import React, { useEffect, useState } from "react";
import EventCard from "../components/EventCard";
import "../styles/AllEventsPage.css";

function AllEventsPage({ outEvents }) {
  const [events, setEvents] = useState([]); // Состояние для хранения событий
  const [loading, setLoading] = useState(true); // Состояние для отображения загрузки

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
      <h1>All Events</h1>
      <div className="events-grid">
        {events.map((event, index) => (
          <EventCard key={index} event={event} />
        ))}
      </div>
    </section>
  );
}

export default AllEventsPage;
