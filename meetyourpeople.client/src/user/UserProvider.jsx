import { createContext, useState, useEffect } from "react";
import PropTypes from "prop-types";

const initialUserState = null;

export const UserContext = createContext();

export const UserProvider = ({ children }) => {
    const [user, setUser] = useState(initialUserState);

    useEffect(() => {
        let isMounted = true;

        const checkUser = async () => {
            const token = localStorage.getItem("token");
            if (!token) {
                setUser(initialUserState);
                return;
            }
            try {
                const res = await fetch("/api/user/check-auth", {
                    headers: { Authorization: `Bearer ${token}` },
                });
                if (res.ok) {
                    const data = await res.json();
                    if (isMounted) setUser(data.user);
                } else {
                    throw new Error("Invalid token");
                }
            } catch (error) {
                console.error(error);
                localStorage.removeItem("token");
                if (isMounted) setUser(initialUserState);
            }
        };

        checkUser();

        return () => {
            isMounted = false;
        };
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
    const registerProfile = async (userData) => {
        try {
            const response = await fetch("/api/user/register", {
                method: "POST",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify(userData),
            });

            if (response.ok) {

                const data = await response.json();

                if (response.ok && data.token) {
                    localStorage.setItem("token", data.token);
                    setUser(data.user);
                } else {
                    throw new Error(data.message || "Registration failed");
                }
            }
            else {
                throw new Error(response.statusText);
            }

        } catch (error) {

            console.error("Registration error:", error);
            throw error;
        }
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
        <UserContext.Provider value={{ user, login, logout, registerProfile, updateProfile }}>
            {children}
        </UserContext.Provider>
    );
};

UserProvider.propTypes = {
    children: PropTypes.node.isRequired,
};