import { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import { useUser } from "../user/useUser";
import "../styles/ProfilePage.css";

function ProfilePage() {
    const { user, updateProfile, registerProfile } = useUser();
    const [email, setEmail] = useState("");
    const [userName, setUserName] = useState("");
    const [dob, setDob] = useState("");
    const [address, setAddress] = useState("");
    const [password, setPassword] = useState("");
    const [confirmPassword, setConfirmPassword] = useState("");

    const navigate = useNavigate();

    useEffect(() => {
        if (user) {
            setEmail(user.email);
            setUserName(user.userName);
            setDob(user.dateOfBirth);
            setAddress(user.address)
        }
    }, [user]);

    const handleSubmit = async (e) => {
        e.preventDefault();

        if (password !== confirmPassword) {
            alert("Passwords do not match!");
            return;
        }

        let furtherAction;
        let resultMessage;
        let navigationRoute;

        if (user) {
            furtherAction = updateProfile;
            resultMessage = "Profile updated!";
            navigationRoute = "/profile"
        } else {
            furtherAction = registerProfile;
            resultMessage = "You have been registered!"
            navigationRoute = "/"
        }

        const userData = {
            email: email,
            userName: userName,
            dateOfBirth: dob,
            address: address,
            password: password
        };

        try {

            await furtherAction(userData);

            alert(resultMessage);

            navigate(navigationRoute);

        } catch (error) {
            console.error("Profile page error:", error);
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
                        placeholder="Email/Login"
                        value={email}
                        onChange={(e) => setEmail(e.target.value)}
                    />
                    <input
                        type="text"
                        placeholder="User Name"
                        value={userName}
                        onChange={(e) => setUserName(e.target.value)}
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
