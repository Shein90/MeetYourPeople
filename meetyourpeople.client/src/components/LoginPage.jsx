import React, { useState } from "react";
import { useNavigate } from "react-router-dom";
import { Link } from "react-router-dom";
import "../styles/LoginPage.css"; // Подключаем стили

function LoginPage() {
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");
  const navigate = useNavigate();

  const handleSubmit = (e) => {
    e.preventDefault();
    // Ваша логика для проверки логина и пароля
    alert("Logged in");
    // После успешного логина можно перейти на главную страницу или на другую
    navigate("/dashboard"); // Переход на другую страницу (например, на главную)
  };

  return (
    <div className="login-page">
      <div className="login-container">
        <h1>Sign In</h1>
        <form onSubmit={handleSubmit}>
          <input
            type="text"
            placeholder="Username"
            value={username}
            onChange={(e) => setUsername(e.target.value)}
            required
          />
          <input
            type="password"
            placeholder="Password"
            value={password}
            onChange={(e) => setPassword(e.target.value)}
            required
          />
          <button type="submit">Log In</button>
        </form>
        <Link to="/profile" className="register-link">
          Create an account
        </Link>
      </div>
    </div>
  );
}

export default LoginPage;
