import { useState } from "react";
import "../styles/EventCreationPage.css";
import { useEvent } from "../event/UseEvent";
import { useUser } from "../user/UseUser";

function EventCreationPage() {
    const { createEvent } = useEvent();
    const { user } = useUser();
    const [form, setForm] = useState({
        title: "",
        description: "",
        detailedDescription: "",
        date: "",
        time: "",
        address: "",
        maxParticipants: "",
        eventImage: null
    });

    const [imagePreview, setImagePreview] = useState(null);
    const [isSubmitting, setIsSubmitting] = useState(false);
    const [isCreated, setIsCreated] = useState(false);

    const handleChange = (e) => {
        const { name, value } = e.target;
        setForm((prevForm) => ({ ...prevForm, [name]: value }));
    };

    const handleImageUpload = (e) => {
        const file = e.target.files[0];
        if (file && file.type.startsWith("image/")) {

            setForm((prevForm) => ({
                ...prevForm,
                eventImage: file
            }));

            setImagePreview(URL.createObjectURL(file));
        } else {
            alert("Please upload a valid image file.");
        }
    };

    const validateForm = () => {
        if (!form.title ||
            !form.date ||
            !form.time ||
            !form.address ||
            !form.description ||
            !form.detailedDescription ||
            !form.maxParticipants ||
            !form.eventImage) {
            alert("Please fill in all required fields.");
            return false;
        }
        return true;
    };

    const handleSubmit = async (e) => {
        e.preventDefault();

        setIsSubmitting(true);

        const updatedForm = {
            ...form,
            ownerId: user.id,
            ownerName: user.userName
        };

        if (!validateForm()) {

            setIsSubmitting(false);

            return;
        }

        try {
            await createEvent(updatedForm);
            setIsCreated(true);
        }
        catch (err) {
            alert(`Error creating event: ${err.message}`);
        }
        finally {
            setIsSubmitting(false);
        }
    };

    return (
        <div className="event-creation-page">
            <h2>Create Event</h2>
            {isCreated ? (<p className="message">Event has been created successfully!</p>)
                : (<form onSubmit={handleSubmit}>
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
                                name="detailedDescription"
                                placeholder="Full Description"
                                value={form.detailedDescription}
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
                </form>)}

        </div>
    );
}

export default EventCreationPage;
