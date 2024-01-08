import { EventStatus } from "../shared/enums/event-status";
import { Location } from "./location.dto";

export interface Event {
    name: string,
    location: Location,
    ticketPrice: number,
    organizerEmail: string,
    description: string,
    startDate: Date,
    endDate: Date,
    status: EventStatus
}