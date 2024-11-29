import "../styles/Header.css";
import { Link } from "react-router-dom";
import PropTypes from "prop-types";

Header.propTypes = {
    user: PropTypes.shape({
        id: PropTypes.oneOfType([PropTypes.string, PropTypes.number]),
        name: PropTypes.string,
    }),
};

function Header({ user }) {
    return (
        <header className="header">
            <div className="container">
                <div className="logo">MYP</div>
                <nav>
                    <Link to="/">Home</Link>
                    <Link to="/events">Events</Link>
                </nav>
                {user ? (
                    <Link to="/profile" className="sign-in-btn">
                        Profile
                    </Link>
                ) : (
                    <Link to="/login" className="sign-in-btn">
                        Sign In
                    </Link>
                )}
            </div>
        </header>
    );
}

export default Header;
