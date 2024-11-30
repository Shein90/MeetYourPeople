import { useState } from "react";
import { useNavigate } from "react-router-dom";
import { useUser } from "../user/useUser";
import "../styles/ProfilePage.css";

function ProfilePage() {
    const { user, updateProfile } = useUser(); 
    const [firstName, setFirstName] = useState(user?.firstName || "");
    const [lastName, setLastName] = useState(user?.lastName || "");
    const [dob, setDob] = useState(user?.dob || "");
    const [address, setAddress] = useState(user?.address || "");
    const [password, setPassword] = useState("");
    const [confirmPassword, setConfirmPassword] = useState("");
    const navigate = useNavigate();

    const handleSubmit = async (e) => {
        e.preventDefault();

        if (password !== confirmPassword) {
            alert("Passwords do not match!");
            return;
        }

        try {
            await updateProfile({
                firstName,
                lastName,
                dob,
                address,
                password,
            });

            alert(user ? "Profile updated!" : "You have been registered!");
        } catch (error) {
            console.error("Profile update error:", error);
            alert("Failed to update profile. Please try again.");
        }
    };

    const handleEventClick = (eventId) => {
        navigate(`/event/${eventId}`);
    };

    return (
        <div className="profile-page">
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

            {user && (
                <div className="events-list">
                    <h3>Your Events</h3>
                    {user.events?.length > 0 ? (
                        user.events.map((event) => (
                            <div
                                className="event-item"
                                key={event.id}
                                onClick={() => handleEventClick(event.id)}
                                style={{ cursor: "pointer" }}
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
