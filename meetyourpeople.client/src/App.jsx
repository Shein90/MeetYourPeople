import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import Header from "./components/Header";
import Hero from "./components/Hero";
import HomePageEvents from "./components/HomePageEvents";
import AllEventsPage from "./components/AllEventsPage";
import EventDetailPage from "./components/EventDetailPage";
import Footer from "./components/Footer";
import LoginPage from "./components/LoginPage";
import ProfilePage from "./components/ProfilePage";
import EventCreationPage from "./components/EventCreationPage";

import "./styles/App.css";

import audiImage from "./assets/audi.jpg";
import walkImage from "./assets/walk.jpg";
import gamesImage from "./assets/games.jpg";
import paintballImage from "./assets/paintball.jpg";
import coffeeImage from "./assets/coffee.jpg";
import metalImage from "./assets/metal.jpg";

function App() {

    const events = [
        {
            id: 1,
            title: "Audi",
            image: audiImage,
            description:
                "Explore the world of Audi automobiles with fellow enthusiasts in a day filled with car showcases and discussions.",
            fullDescription:
                "Join us for an exclusive Audi lovers' gathering, where we’ll showcase iconic models, discuss the brand's legacy, and explore upcoming innovations in Audi’s lineup. Whether you’re an Audi owner, a fan, or someone curious about high-performance vehicles, this event offers a mix of expert talks, casual networking, and photo opportunities with stunning cars. Complimentary refreshments and Audi-themed giveaways will also be part of the experience.",
            date: "2024-12-15",
            time: "14:00",
            registered: 10,
            maxParticipants: 20,
            address: "addddddd",
        },
        {
            id: 2,
            title: "Photography Walk",
            image: walkImage,
            description:
                "Capture the beauty of the city in a group photography walk led by a professional photographer.",
            fullDescription:
                "Join us for an exclusive Audi lovers' gathering, where we’ll showcase iconic models, discuss the brand's legacy, and explore upcoming innovations in Audi’s lineup. Whether you’re an Audi owner, a fan, or someone curious about high-performance vehicles, this event offers a mix of expert talks, casual networking, and photo opportunities with stunning cars. Complimentary refreshments and Audi-themed giveaways will also be part of the experience.",
            date: "2024-12-15",
            time: "14:00",
            registered: 10,
            maxParticipants: 20,
            address: "addddddd",
        },
        {
            id: 3,
            title: "Board Game Night",
            image: gamesImage,
            description:
                "An evening of strategy and fun with classic and modern board games in a cozy, relaxed setting.",
            fullDescription:
                "Join us for an exclusive Audi lovers' gathering, where we’ll showcase iconic models, discuss the brand's legacy, and explore upcoming innovations in Audi’s lineup. Whether you’re an Audi owner, a fan, or someone curious about high-performance vehicles, this event offers a mix of expert talks, casual networking, and photo opportunities with stunning cars. Complimentary refreshments and Audi-themed giveaways will also be part of the experience.",
            date: "2024-12-15",
            time: "14:00",
            registered: 10,
            maxParticipants: 20,
            address: "addddddd",
        },
        {
            id: 4,
            title: "Paintball",
            image: paintballImage,
            description:
                "Get ready for an action-packed paintball battle in a scenic outdoor arena. Team up and test your strategy skills.",
            fullDescription:
                "Join us for an exclusive Audi lovers' gathering, where we’ll showcase iconic models, discuss the brand's legacy, and explore upcoming innovations in Audi’s lineup. Whether you’re an Audi owner, a fan, or someone curious about high-performance vehicles, this event offers a mix of expert talks, casual networking, and photo opportunities with stunning cars. Complimentary refreshments and Audi-themed giveaways will also be part of the experience.",
            date: "2024-12-15",
            time: "14:00",
            registered: 10,
            maxParticipants: 20,
            address: "addddddd",
        },
        {
            id: 5,
            title: "Coffee Appreciation",
            image: coffeeImage,
            description:
                "A workshop for coffee lovers to explore the art of brewing, tasting, and appreciating specialty coffee.",
            fullDescription:
                "Join us for an exclusive Audi lovers' gathering, where we’ll showcase iconic models, discuss the brand's legacy, and explore upcoming innovations in Audi’s lineup. Whether you’re an Audi owner, a fan, or someone curious about high-performance vehicles, this event offers a mix of expert talks, casual networking, and photo opportunities with stunning cars. Complimentary refreshments and Audi-themed giveaways will also be part of the experience.",
            date: "2024-12-15",
            time: "14:00",
            registered: 10,
            maxParticipants: 20,
            address: "addddddd",
        },
        {
            id: 6,
            title: "Metal Music",
            image: metalImage,
            description:
                "Dive into the raw energy of metal music at a fan meetup featuring live performances and genre discussions.",
            fullDescription:
                "Join us for an exclusive Audi lovers' gathering, where we’ll showcase iconic models, discuss the brand's legacy, and explore upcoming innovations in Audi’s lineup. Whether you’re an Audi owner, a fan, or someone curious about high-performance vehicles, this event offers a mix of expert talks, casual networking, and photo opportunities with stunning cars. Complimentary refreshments and Audi-themed giveaways will also be part of the experience.",
            date: "2024-12-15",
            time: "14:00",
            registered: 10,
            maxParticipants: 20,
            address: "addddddd",
        },
    ];

    const user = {

        id: 1,
        username: "john_doe",
        firstName: "John",
        lastName: "Doe",
        dob: "1990-01-01",
        address: "Sydney",
        events: [
            { id: 1, title: "Audi", date: "2024-12-15" },
            { id: 1, title: "Photography Walk", date: "2024-12-20" },
        ],
    };

    return (
        <Router>
            <div className="app-container">
                <Header user={user} />
                <Routes>
                    <Route
                        path="/"
                        element={
                            <>
                                <Hero />
                                <HomePageEvents events={events} />
                            </>
                        }
                    />
                    <Route path="/login" element={<LoginPage />} />
                    <Route path="/create-event" element={<EventCreationPage />} />
                    <Route path="/profile" element={<ProfilePage user={user} />} />
                    <Route
                        path="/events"
                        element={<AllEventsPage outEvents={events} user={user} />}
                    />
                    <Route
                        path="/event/:id"
                        element={<EventDetailPage events={events} user={user} />}
                    />
                </Routes>
                <Footer />
            </div>
        </Router>
    );
}

export default App;
