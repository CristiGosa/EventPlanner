import { Currency } from "../shared/enums/currency";
import { EventStatus } from "../shared/enums/event-status";

export interface Event {
    id: number,
    name: string,
    locationId: number,
    ticketPrice: number,
    priceCurrency: Currency,
    organizerEmail: string,
    description: string,
    startDate: Date,
    endDate: Date,
    status: EventStatus,
    participantsNumber: number,
    photoUrl: string,
}