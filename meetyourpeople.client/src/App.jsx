import Header from './components/Header';
import Hero from './components/Hero';
import Events from './components/Events';
import Footer from './components/Footer';
import './styles/App.css';

function App() {
    return (
        <div>
            <div className='header-con'>
            <Header />
            <Hero />
            </div>
            <Events />
            <Footer />
        </div>
    );
}

export default App;



