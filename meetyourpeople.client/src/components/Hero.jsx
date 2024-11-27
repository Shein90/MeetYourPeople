import "../styles/Hero.css";
import { useNavigate } from "react-router-dom";

function Hero() {
  const navigate = useNavigate();
  return (
    <div className="hero">
      <div className="hero-content">
        <h1>MeetYourPeople</h1>
        <h2>Connect with Like-Minded People</h2>
        <h3>Find and join events based on your interests.</h3>
        <button className="cta-btn" onClick={() => navigate("/events")}>
          Join to your people
        </button>
      </div>
    </div>
  );
}

export default Hero;
