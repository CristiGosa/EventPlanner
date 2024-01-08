import { EventStatus } from "../shared/enums/event-status";

export interface UpdateEventStatusRequest {
    eventId: number,
    newStatus: EventStatus
}