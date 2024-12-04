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
                const res = await fetch("/api/user/me", {
                    headers: { Authorization: `Bearer ${token}` },
                });
                if (res.ok) {
                    const user = await res.json();
                    if (isMounted) setUser(user);
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

    const login = async (email, password) => {
        const res = await fetch("/api/user/login", {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify({ email, password }),
        });

        if (!res.ok) {
            const errorData = await res.text();
            throw new Error(errorData.message);
        }

        const data = await res.json();

        if (data) {
            localStorage.setItem("token", data.token);
            setUser(data.user);
        } else {
            throw new Error(data.message || "Login failed");
        }
    };

    const logout = () => {
        localStorage.removeItem("token");
        setUser(initialUserState);
    };

    const registerProfile = async (userData) => {
        try {
            const response = await fetch("/api/user", {
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

    const updateProfile = async (updatedData) => {
        const token = localStorage.getItem("token");

        if (!token) {
            console.error("No token found, please login.");
            return Promise.reject("Unauthorized");
        }

        try {
            const res = await fetch(`/api/user/me`, {
                method: "PUT",
                headers: {
                    "Content-Type": "application/json",
                    Authorization: `Bearer ${token}`,
                },
                body: JSON.stringify(updatedData),
            });

            if (!res.ok) {
                const error = await res.text();
                throw new Error(error || "Failed to update user");
            }

            const user = await res.json();
            setUser(user);

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