import EventCard from "./EventCard";
import "../styles/HomePageEvents.css";

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
