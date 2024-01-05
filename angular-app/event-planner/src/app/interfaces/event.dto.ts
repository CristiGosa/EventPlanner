import { Location } from "./location.dto";

export interface Event {
    name: string,
    location: Location,
    ticketPrice: number,
    organizerEmail: string,
    description: string,
    startDate: Date,
    endDate: Date
}