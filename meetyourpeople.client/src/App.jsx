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
import { useUser } from "./useUser";
import "./styles/App.css";

function App() {
    const { user } = useUser();

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
                    <Route path="/profile" element={<ProfilePage />} />
                    <Route
                        path="/events"
                        element={<AllEventsPage outEvents={events} />}
                    />
                    <Route
                        path="/event/:id"
                        element={<EventDetailPage events={events} />}
                    />
                </Routes>
                <Footer />
            </div>
        </Router>
    );
}

export default App;
