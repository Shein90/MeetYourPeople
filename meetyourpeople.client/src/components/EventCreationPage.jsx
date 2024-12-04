import { useState } from "react";
import "../styles/EventCreationPage.css";
import { useEvent } from "../event/UseEvent";

function EventCreationPage() {
    const { createEvent } = useEvent();
    const [form, setForm] = useState({
        title: "",
        description: "",
        fullDescription: "",
        date: "",
        time: "",
        address: "",
        maxParticipants: "",
        imageFile: null,
        imagePreview: null,
    });
    const [message, setMessage] = useState(""); 
    const [isSubmitting, setIsSubmitting] = useState(false);


    // Обработчик изменений для всех полей
    const handleChange = (e) => {
        const { name, value } = e.target;
        setForm((prevForm) => ({ ...prevForm, [name]: value }));
    };

    // Обработчик загрузки изображения
    const handleImageUpload = (e) => {
        const file = e.target.files[0];
        if (file) {
            setForm((prevForm) => ({
                ...prevForm,
                imageFile: file,
                imagePreview: URL.createObjectURL(file),
            }));
        }
    };

    // Валидация формы перед отправкой
    const validateForm = () => {
        if (!form.title || !form.date || !form.time || !form.address || !form.maxParticipants) {
            setMessage("Please fill in all required fields.");
            return false;
        }
        return true;
    };

    // Обработчик отправки формы
    const handleSubmit = (e) => {
        e.preventDefault();

        setIsSubmitting(true); // Включаем блокировку кнопки

        // Валидация формы
        if (!validateForm()) {
            alert("Please check your data!")
            setIsSubmitting(false); // Отключаем блокировку кнопки
            return;
        }


        createEvent(form);  // Вызов функции createEvent из контекста
        setIsSubmitting(true);
    };

    return (
        <div className="event-creation-page">
            <h2>Create Event</h2>
            <form onSubmit={handleSubmit}>
                <div className="form-container">
                    {/* Секция изображения */}
                    <div className="image-section">
                        <div className="image-preview">
                            {form.imagePreview ? (
                                <img src={form.imagePreview} alt="Preview" />
                            ) : (
                                <p className="placeholder">Image Preview</p>
                            )}
                        </div>
                        <label htmlFor="imageInput" className="upload-label">
                            Upload Image
                        </label>
                        <input type="file" id="imageInput" onChange={handleImageUpload} />
                    </div>

                    {/* Секция полей формы */}
                    <div className="fields-section">
                        <input
                            type="text"
                            name="title"
                            placeholder="Title"
                            value={form.title}
                            onChange={handleChange}
                        />
                        <textarea
                            name="description"
                            placeholder="Description"
                            value={form.description}
                            onChange={handleChange}
                        />
                        <textarea
                            name="fullDescription"
                            placeholder="Full Description"
                            value={form.fullDescription}
                            onChange={handleChange}
                        />
                        <input
                            type="date"
                            name="date"
                            value={form.date}
                            onChange={handleChange}
                        />
                        <input
                            type="time"
                            name="time"
                            value={form.time}
                            onChange={handleChange}
                        />
                        <input
                            type="text"
                            name="address"
                            placeholder="Address"
                            value={form.address}
                            onChange={handleChange}
                        />
                        <input
                            type="number"
                            name="maxParticipants"
                            placeholder="Max Participants"
                            value={form.maxParticipants}
                            min="0"
                            step="1"
                            onChange={handleChange}
                        />
                        <button className="btn" type="submit" disabled={isSubmitting}>
                            {isSubmitting ? "Creating..." : "Create Event"}
                        </button>
                    </div>
                </div>
            </form>

            {/* Сообщения об ошибках или успехе */}
            {message && <p className="message">{message}</p>}
        </div>
    );
}

export default EventCreationPage;
