import "../styles/Header.css";
import { Link } from "react-router-dom";
function Header() {
  return (
    <header className="header">
      <div className="container">
        <div className="logo">MYP</div>
        <nav>
          <Link to="/">Home</Link>
          <Link to="/events">Events</Link>
          <Link to="#">Profile</Link>
        </nav>
        <button className="sign-in-btn">Sign In</button>
      </div>
    </header>
  );
}

export default Header;
