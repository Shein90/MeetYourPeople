import React, { useState } from "react";
import "../styles/EventCreationPage.css";

function EventCreationPage() {
  const [title, setTitle] = useState("");
  const [description, setDescription] = useState("");
  const [fullDescription, setFullDescription] = useState("");
  const [date, setDate] = useState("");
  const [time, setTime] = useState("");
  const [address, setAddress] = useState("");
  const [maxParticipants, setMaxParticipants] = useState("");
  const [image, setImage] = useState(null);

  const handleImageUpload = (e) => {
    setImage(URL.createObjectURL(e.target.files[0]));
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    alert("Event created!");
  };

  return (
    <div className="event-creation-page">
      <h2>Create Event</h2>
      <form onSubmit={handleSubmit}>
        <div className="form-container">
          <div className="image-section">
            <div className="image-preview">
              {image ? (
                <img src={image} alt="Preview" />
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
