import '../styles/Header.css';

function Header() {
    return (
        <header className="header">
            <div className="container">
                <div className="logo">MYP</div>
                <nav>
                    <a href="#">Home</a>
                    <a href="#">Events</a>
                    <a href="#">Profile</a>
                   
                </nav>
                <button className="sign-in-btn">Sign In</button>
            </div>
        </header>
    );
}

export default Header;