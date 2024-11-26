import '../styles/Footer.css';

function Footer() {
    return (
        <footer className="footer">
            <div className="footer-content">
                <div className="footer-links">
                    <a href="#">About us</a>
                    <a href="#">Terms and conditions</a>
                    <a href="#">Contact</a>
                </div>
                <div className="social-media">
                    <a href="#"><img alt="Facebook" /></a>
                    <a href="#"><img alt="Twitter" /></a>
                    <a href="#"><img alt="Instagram" /></a>
                </div>
            </div>
        </footer>
    );
}

export default Footer;