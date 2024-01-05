export interface CreateEventRequest {
    locationId: number,
    name: string,
    ticketPrice: number,
    description: string,
    startDate: Date,
    endDate: Date
}