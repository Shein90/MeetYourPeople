import React, { useState } from "react";
import { useNavigate } from "react-router-dom";
import "../styles/ProfilePage.css";

function ProfilePage({ user }) {
  const [firstName, setFirstName] = useState(user?.firstName || "");
  const [lastName, setLastName] = useState(user?.lastName || "");
  const [dob, setDob] = useState(user?.dob || "");
  const [address, setAddress] = useState(user?.address || "");
  const [password, setPassword] = useState("");
  const [confirmPassword, setConfirmPassword] = useState("");
  const [events, setEvents] = useState(user?.events || []);

  const navigate = useNavigate(); // Хук для навигации

  const handleSubmit = (e) => {
    e.preventDefault();

    if (password !== confirmPassword) {
      alert("Passwords do not match!");
      return;
    }

    if (!user) {
      alert("You have been registered!");
    } else {
      alert("Profile updated!");
    }
  };

  // Переход на страницу события
  const handleEventClick = (eventId) => {
    navigate(`/event/${eventId}`); // Переходим на страницу события с ID
  };

  return (
    <div className="profile-page">
      {/* Форма для профиля */}
      <div className="profile-form">
        <h2>{user ? "Profile" : "Register"}</h2>
        <form onSubmit={handleSubmit}>
          <input
            type="text"
            placeholder="First Name"
            value={firstName}
            onChange={(e) => setFirstName(e.target.value)}
          />
          <input
            type="text"
            placeholder="Last Name"
            value={lastName}
            onChange={(e) => setLastName(e.target.value)}
          />
          <input
            type="date"
            value={dob}
            onChange={(e) => setDob(e.target.value)}
          />
          <input
            type="text"
            placeholder="Address"
            value={address}
            onChange={(e) => setAddress(e.target.value)}
          />
          <input
            type="password"
            placeholder="Password"
            value={password}
            onChange={(e) => setPassword(e.target.value)}
          />
          <input
            type="password"
            placeholder="Confirm Password"
            value={confirmPassword}
            onChange={(e) => setConfirmPassword(e.target.value)}
          />
          <button type="submit">{user ? "Save Changes" : "Register"}</button>
        </form>
      </div>

      {/* Список событий */}
      {user && (
        <div className="events-list">
          <h3>Your Events</h3>
          {events.length > 0 ? (
            events.map((event, index) => (
              <div
                className="event-item"
                key={index}
                onClick={() => handleEventClick(event.id)} // Переход по клику
                style={{ cursor: "pointer" }} // Добавляем указатель для кликабельности
              >
                <h3>{event.title}</h3>
                <p>{event.date}</p>
              </div>
            ))
          ) : (
            <p>You have no events yet.</p>
          )}
        </div>
      )}
    </div>
  );
}

export default ProfilePage;
