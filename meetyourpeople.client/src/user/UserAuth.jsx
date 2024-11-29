import { useEffect } from "react";
import { useUser } from "./useUser";

const UserAuth = () => {
    const { setUser } = useUser();

    useEffect(() => {
        const token = localStorage.getItem("token");
        if (token) {
            fetch("/api/user", { headers: { Authorization: `Bearer ${token}` } })
                .then((res) => res.json())
                .then((data) => setUser(data))
                .catch(() => localStorage.removeItem("token"));
        }
    }, [setUser]);

    return null;
};

export default UserAuth;