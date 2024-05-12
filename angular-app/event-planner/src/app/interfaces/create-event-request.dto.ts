import { Currency } from "../shared/enums/currency"

export interface CreateEventRequest {
    locationId: number,
    name: string,
    ticketPrice: number,
    priceCurrency: Currency,
    description: string,
    startDate: Date,
    endDate: Date
}