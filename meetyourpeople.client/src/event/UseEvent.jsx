import { useContext } from "react";
import { EventContext } from "./EventProvider";

export const useEvent = () => useContext(EventContext);