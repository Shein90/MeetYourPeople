import EventCard from './EventCard';
import '../styles/Events.css';
import audiImage from '../assets/audi.jpg';
import walkImage from '../assets/walk.jpg';
import gamesImage from '../assets/games.jpg';
import paintballImage from '../assets/paintball.jpg';
import coffeeImage from '../assets/coffee.jpg';
import metalImage from '../assets/metal.jpg';

function Events() {
    const events = [
        {
            title: "Audi",
            image: audiImage,
            description: "Explore the world of Audi automobiles with fellow enthusiasts in a day filled with car showcases and discussions."
        },
        {
            title: "Photography Walk",
            image: walkImage,
            description: "Capture the beauty of the city in a group photography walk led by a professional photographer."
        },
        {
            title: "Board Game Night",
            image: gamesImage,
            description: "An evening of strategy and fun with classic and modern board games in a cozy, relaxed setting."
        },
        {
            title: "Paintball",
            image: paintballImage,
            description: "Get ready for an action-packed paintball battle in a scenic outdoor arena. Team up and test your strategy skills."
        },
        {
            title: "Coffee Appreciation",
            image: coffeeImage,
            description: "A workshop for coffee lovers to explore the art of brewing, tasting, and appreciating specialty coffee."
        },
        {
            title: "Metal Music",
            image: metalImage,
            description: "Dive into the raw energy of metal music at a fan meetup featuring live performances and genre discussions."
        }
    ];

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

export default Events;