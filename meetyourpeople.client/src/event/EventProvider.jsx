import { createContext, useState, useEffect } from "react";
import PropTypes from "prop-types";

import audiImage from "../assets/audi.jpg";
import walkImage from "../assets/walk.jpg";
import gamesImage from "../assets/games.jpg";
import paintballImage from "../assets/paintball.jpg";
import coffeeImage from "../assets/coffee.jpg";
import metalImage from "../assets/metal.jpg";

const initialEventsState = [];

const eventsmock = [
    {
        id: 1,
        title: "Audi",
        image: audiImage,
        description:
            "Explore the world of Audi automobiles with fellow enthusiasts in a day filled with car showcases and discussions.",
        fullDescription:
            "Join us for an exclusive Audi lovers' gathering, where weâ€™ll showcase iconic models, discuss the brand's legacy, and explore upcoming innovations in Audiâ€™s lineup. Whether youâ€™re an Audi owner, a fan, or someone curious about high-performance vehicles, this event offers a mix of expert talks, casual networking, and photo opportunities with stunning cars. Complimentary refreshments and Audi-themed giveaways will also be part of the experience.",
        date: "2024-12-15",
        time: "14:00",
        registered: [1,2],
        maxParticipants: 20,
        address: "addddddd",
    },
    {
        id: 2,
        title: "Photography Walk",
        image: walkImage,
        description:
            "Capture the beauty of the city in a group photography walk led by a professional photographer.",
        fullDescription:
            "Join us for an exclusive Audi lovers' gathering, where weâ€™ll showcase iconic models, discuss the brand's legacy, and explore upcoming innovations in Audiâ€™s lineup. Whether youâ€™re an Audi owner, a fan, or someone curious about high-performance vehicles, this event offers a mix of expert talks, casual networking, and photo opportunities with stunning cars. Complimentary refreshments and Audi-themed giveaways will also be part of the experience.",
        date: "2024-12-15",
        time: "14:00",
        registered: [1, 2],
        maxParticipants: 20,
        address: "addddddd",
    },
    {
        id: 3,
        title: "Board Game Night",
        image: gamesImage,
        description:
            "An evening of strategy and fun with classic and modern board games in a cozy, relaxed setting.",
        fullDescription:
            "Join us for an exclusive Audi lovers' gathering, where weâ€™ll showcase iconic models, discuss the brand's legacy, and explore upcoming innovations in Audiâ€™s lineup. Whether youâ€™re an Audi owner, a fan, or someone curious about high-performance vehicles, this event offers a mix of expert talks, casual networking, and photo opportunities with stunning cars. Complimentary refreshments and Audi-themed giveaways will also be part of the experience.",
        date: "2024-12-15",
        time: "14:00",
        registered: [1, 2],
        maxParticipants: 20,
        address: "addddddd",
    },
    {
        id: 4,
        title: "Paintball",
        image: paintballImage,
        description:
            "Get ready for an action-packed paintball battle in a scenic outdoor arena. Team up and test your strategy skills.",
        fullDescription:
            "Join us for an exclusive Audi lovers' gathering, where weâ€™ll showcase iconic models, discuss the brand's legacy, and explore upcoming innovations in Audiâ€™s lineup. Whether youâ€™re an Audi owner, a fan, or someone curious about high-performance vehicles, this event offers a mix of expert talks, casual networking, and photo opportunities with stunning cars. Complimentary refreshments and Audi-themed giveaways will also be part of the experience.",
        date: "2024-12-15",
        time: "14:00",
        registered: [1, 2],
        maxParticipants: 20,
        address: "addddddd",
    },
    {
        id: 5,
        title: "Coffee Appreciation",
        image: coffeeImage,
        description:
            "A workshop for coffee lovers to explore the art of brewing, tasting, and appreciating specialty coffee.",
        fullDescription:
            "Join us for an exclusive Audi lovers' gathering, where weâ€™ll showcase iconic models, discuss the brand's legacy, and explore upcoming innovations in Audiâ€™s lineup. Whether youâ€™re an Audi owner, a fan, or someone curious about high-performance vehicles, this event offers a mix of expert talks, casual networking, and photo opportunities with stunning cars. Complimentary refreshments and Audi-themed giveaways will also be part of the experience.",
        date: "2024-12-15",
        time: "14:00",
        registered: [1, 2],
        maxParticipants: 20,
        address: "addddddd",
    },
    {
        id: 6,
        title: "Metal Music",
        image: metalImage,
        description:
            "Dive into the raw energy of metal music at a fan meetup featuring live performances and genre discussions.",
        fullDescription:
            "Join us for an exclusive Audi lovers' gathering, where weâ€™ll showcase iconic models, discuss the brand's legacy, and explore upcoming innovations in Audiâ€™s lineup. Whether youâ€™re an Audi owner, a fan, or someone curious about high-performance vehicles, this event offers a mix of expert talks, casual networking, and photo opportunities with stunning cars. Complimentary refreshments and Audi-themed giveaways will also be part of the experience.",
        date: "2024-12-15",
        time: "14:00",
        registered: [1, 2],
        maxParticipants: 20,
        address: "addddddd",
    },
];

export const EventContext = createContext();

export const EventProvider = ({ children }) => {
    const [events, setEvents] = useState(initialEventsState);

    // Получаем список событий для отображения на главной странице
    //const getEvents = async () => {
    //    try {
    //        const res = await fetch("/api/events");
    //        if (!res.ok) {
    //            throw new Error("Failed to fetch events");
    //        }
    //        const data = await res.json();
    //        setEvents(data); // Сохраняем события в контексте
    //    } catch (error) {
    //        console.error("Error fetching events:", error);
    //    }
    //};

    const getEvents = () => {
        setEvents(eventsmock); // Сохраняем события в контексте
    };

    const createEvent = async (eventData) => {

        const formData = new FormData();
        formData.append("title", eventData.title);
        formData.append("description", eventData.description);
        formData.append("fullDescription", eventData.fullDescription);
        formData.append("date", eventData.date);
        formData.append("time", eventData.time);
        formData.append("address", eventData.address);
        formData.append("maxParticipants", eventData.maxParticipants);
        if (eventData.imageFile) {
            formData.append("image", eventData.imageFile);
        }

        try {
            const response = await fetch("/api/events", {
                method: "POST",
                body: formData,
            });

            if (!response.ok) {
                throw new Error("Error creating event");
            }

            const newEvent = await response.json();
            setEvents((prevEvents) => [...prevEvents, newEvent]);
            alert("Event created successfully!");
        } catch (err) {

            alert(`Error creating event: ${err.message}`);
        }
    };

    // Функция для присоединения/покидания события
    const joinLeaveEvent = (eventId, userId, isCurrentlyRegistered) => {
        setEvents((prevEvents) =>
            prevEvents.map((event) =>
                event.id === eventId
                    ? {
                        ...event,
                        registered: isCurrentlyRegistered
                            ? event.registered.filter((id) => id !== userId) // Удаляем пользователя
                            : [...event.registered, userId], // Добавляем пользователя
                    }
                    : event
            )
        );
    };

    // Получение событий при заходе на главную страницу
    useEffect(() => {
        getEvents();
    }, []);

    return (
        <EventContext.Provider value={{ events, getEvents, createEvent, joinLeaveEvent }}>
            {children}
        </EventContext.Provider>
    );
};

EventProvider.propTypes = {
    children: PropTypes.node.isRequired,
};