import { createContext, useState, useEffect } from "react";
import PropTypes from "prop-types";

UserProvider.propTypes = {
    children: PropTypes.node.isRequired,
};

const initialUserState = {
    id: null,
    email: null,
    firstName: null,
    lastName: null,
    dob: null,
    address: null,
    events: [],
};

export const UserContext = createContext();

export const UserProvider = ({ children }) => {
    const [user, setUser] = useState(initialUserState);

    useEffect(() => {
        // Проверяем наличие токена при старте приложения
        const token = localStorage.getItem("token");

        if (token) {
            // Если токен есть, отправляем запрос на сервер для получения данных пользователя
            fetch("/api/user", {
                headers: { Authorization: `Bearer ${token}` },
            })
                .then((res) => {
                    if (!res.ok) {
                        throw new Error("Invalid token");
                    }
                    return res.json();
                })
                .then((data) => {
                    setUser(data);  // Сохраняем данные пользователя
                })
                .catch(() => {
                    // Если ошибка, очищаем токен и сбрасываем пользователя
                    localStorage.removeItem("token");
                    setUser(initialUserState);
                });
        } else {
            // Если нет токена, сбрасываем пользователя
            setUser(initialUserState);
        }
    }, []);

    // Функция логина, отправляет данные на сервер и сохраняет ответ
    const login = (email, password) => {
        fetch("/api/login", {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify({ email, password }),
        })
            .then((res) => res.json())
            .then((data) => {
                if (data.token) {
                    // Если токен получен, сохраняем его и данные пользователя
                    localStorage.setItem("token", data.token);
                    setUser({ ...data.user, token: data.token });
                } else {
                    // Обработка ошибки
                    throw new Error(data.message || "Login failed");
                }
            })
            .catch((error) => {
                console.error("Login error:", error);
            });
    };

    // Функция для выхода
    const logout = () => {
        localStorage.removeItem("token");
        setUser(initialUserState);  // Сбрасываем состояние пользователя
    };

    // Функция для регистрации
    const register = (email, password, userData) => {
        fetch("/api/register", {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify({ email, password, ...userData }),
        })
            .then((res) => res.json())
            .then((data) => {
                if (data.token) {
                    localStorage.setItem("token", data.token);
                    setUser({ ...data.user, token: data.token });
                } else {
                    throw new Error(data.message || "Registration failed");
                }
            })
            .catch((error) => {
                console.error("Registration error:", error);
            });
    };

    // Новая функция для обновления профиля
    const updateProfile = async (updatedData) => {
        const token = localStorage.getItem("token");

        if (!token) {
            console.error("No token found, please login.");
            return Promise.reject("Unauthorized");
        }

        try {
            const res = await fetch("/api/user/update", {
                method: "PUT",
                headers: {
                    "Content-Type": "application/json",
                    Authorization: `Bearer ${token}`,
                },
                body: JSON.stringify(updatedData),
            });
            if (!res.ok) {
                throw new Error("Failed to update profile");
            }
            const data_1 = await res.json();
            setUser(data_1); // Обновляем состояние пользователя
            return data_1;
        } catch (error) {
            console.error("Update profile error:", error);
            throw error;
        }
    };

    return (
        <UserContext.Provider value={{ user, login, logout, register, updateProfile }}>
            {children}
        </UserContext.Provider>
    );
};