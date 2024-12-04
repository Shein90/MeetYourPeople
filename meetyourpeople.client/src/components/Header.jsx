import "../styles/Header.css";
import { Link } from "react-router-dom";
import { useUser } from "../user/UseUser";
import { useNavigate } from "react-router-dom";


function Header() {
    const { user, logout } = useUser();
    const navigate = useNavigate();

    const handleLogout = (event) => {

        event.preventDefault();

        logout();

        navigate("/");
    };

    return (
        <header className="header">
            <div className="container">
                <div className="logo">MYP</div>
                <nav>
                    <Link to="/">Home</Link>
                    <Link to="/events">Events</Link>
                </nav>

                {location.pathname === "/profile" && user ? (
                    <Link onClick={handleLogout} className="sign-in-btn">
                        Logout
                    </Link>
                ) : (
                    user? (
                        <Link to="/profile" className="sign-in-btn">
                            Profile
                        </Link>
                    ) : (
                        <Link to="/login" className="sign-in-btn">
                            Sign In
                        </Link>
                    )
                )}
            </div>
        </header>
    );
}

export default Header;