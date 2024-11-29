import { useState } from "react";
import "../styles/EventCreationPage.css";

function EventCreationPage() {
    const [title, setTitle] = useState("");
    const [description, setDescription] = useState("");
    const [fullDescription, setFullDescription] = useState("");
    const [date, setDate] = useState("");
    const [time, setTime] = useState("");
    const [address, setAddress] = useState("");
    const [maxParticipants, setMaxParticipants] = useState("");
    const [imageFile, setImageFile] = useState(null);
    const [imagePreview, setImagePreview] = useState(null);

    const handleImageUpload = (e) => {
        const file = e.target.files[0];
        if (file) {
            setImageFile(file);
            setImagePreview(URL.createObjectURL(file));
        }
    };

    const handleSubmit = async (e) => {
        e.preventDefault();

        const formData = new FormData();
        formData.append("title", title);
        formData.append("description", description);
        formData.append("fullDescription", fullDescription);
        formData.append("date", date);
        formData.append("time", time);
        formData.append("address", address);
        formData.append("maxParticipants", maxParticipants);

        if (imageFile) {
            formData.append("image", imageFile);
        }

        try {
            const response = await fetch("/your-server-endpoint", {
                method: "POST",
                body: formData,
            });

            if (response.ok) {
                alert("Event created successfully!");
            } else {
                alert("Error creating event.");
            }
        } catch (error) {
            alert("Error creating event: " + error.message);
        }
    };
    return (
        <div className="event-creation-page">
            <h2>Create Event</h2>
            <form onSubmit={handleSubmit}>
                <div className="form-container">
                    <div className="image-section">
                        <div className="image-preview">
                            {imagePreview ? (
                                <img src={imagePreview} alt="Preview" />
                            ) : (
                                <p className="placeholder">Image Preview</p>
                            )}
                        </div>
                        <label htmlFor="imageInput" className="upload-label">
                            Upload Image
                        </label>
                        <input type="file" id="imageInput" onChange={handleImageUpload} />
                    </div>

                    <div className="fields-section">
                        <input
                            type="text"
                            placeholder="Title"
                            value={title}
                            onChange={(e) => setTitle(e.target.value)}
                        />
                        <textarea
                            placeholder="Description"
                            value={description}
                            onChange={(e) => setDescription(e.target.value)}
                        />
                        <textarea
                            placeholder="Full Description"
                            value={fullDescription}
                            onChange={(e) => setFullDescription(e.target.value)}
                        />
                        <input
                            type="date"
                            value={date}
                            onChange={(e) => setDate(e.target.value)}
                        />
                        <input
                            type="time"
                            value={time}
                            onChange={(e) => setTime(e.target.value)}
                        />
                        <input
                            type="text"
                            placeholder="Address"
                            value={address}
                            onChange={(e) => setAddress(e.target.value)}
                        />
                        <input
                            type="number"
                            placeholder="Max Participants"
                            value={maxParticipants}
                            min="0"
                            step="1"
                            onChange={(e) => setMaxParticipants(e.target.value)}
                        />
                        <button className="btm" type="submit">
                            Create Event
                        </button>
                    </div>
                </div>
            </form>
        </div>
    );
}

export default EventCreationPage;
