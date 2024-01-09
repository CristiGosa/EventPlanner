import { HttpErrorResponse, HttpEventType } from '@angular/common/http';
import { Component } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { Event } from 'src/app/interfaces/event.dto';
import { UpdateEventStatusRequest } from 'src/app/interfaces/update-event-status-request.dto';
import { EventStatus } from 'src/app/shared/enums/event-status';
import { EventsRepositoryService } from 'src/app/shared/services/events-repository.service';
import { RolesService } from 'src/app/shared/services/roles.service';

@Component({
  selector: 'app-view-event-requests',
  templateUrl: './view-event-requests.component.html',
  styleUrls: ['./view-event-requests.component.css']
})
export class ViewEventRequestsComponent {
  columnsToDisplay: string[] = ["name", "location", "organizer", "ticketPrice", "startDate", "endDate", "description", "status", "buttons"];
  dataSource:  MatTableDataSource<Event>;
  errorResp: HttpErrorResponse | null = null;

  constructor(private eventsService: EventsRepositoryService, private rolesService: RolesService) {}

  ngOnInit(): void {
    this.refreshTable();
    this.eventsService.eventAdded.subscribe(() => {
      this.refreshTable();
    });
    this.dataSource = new MatTableDataSource<Event>();
  }

  refreshTable(): void {
    if(this.rolesService.isAdmin())
    {
      this.eventsService.getByStatus("Event", EventStatus.Pending).subscribe({
        next: (response) => {
          this.dataSource.data = response.events;
        },
        error: (errorResponse) => {
          this.errorResp = errorResponse;
        } 
      });
    }
    else{
      this.eventsService.getAllEvents("Event/Owned").subscribe({
        next: (response) => {
          this.dataSource.data = response.events;
        }
      });
    }
  }

  getStatus(status: EventStatus): string {
    switch(status)
    {
      case EventStatus.Accepted: return "Accepted";
      case EventStatus.Rejected: return "Rejected";
      case EventStatus.Pending: return "Pending";
      case EventStatus.Cancelled: return "Cancelled";
      default: return "Undefined";
    }
  }

  isAdmin(): boolean {
    return this.rolesService.isAdmin();
  }

  isOrganizer(): boolean {
    return this.rolesService.isOrganizer();
  }

  isPending(event: Event){
    if(event.status === EventStatus.Pending)
      return true;
    return false;
  }

  acceptEvent(eventId: number): void{
    const updateStatusDTO : UpdateEventStatusRequest = {
      eventId : eventId,
      newStatus: EventStatus.Accepted
    }
    this.eventsService.updateStatus("Event", updateStatusDTO).subscribe({
      next: () => {
        this.refreshTable();
      }
    });
  }

  rejectEvent(eventId: number): void {
    const updateStatusDTO : UpdateEventStatusRequest = {
      eventId : eventId,
      newStatus: EventStatus.Rejected
    }
    this.eventsService.updateStatus("Event", updateStatusDTO).subscribe({
      next: () => {
        this.refreshTable();
      }
    });
  }

  cancelRequest(eventId: number): void {
    const updateStatusDTO : UpdateEventStatusRequest = {
      eventId : eventId,
      newStatus: EventStatus.Cancelled
    }
    this.eventsService.updateStatus("Event", updateStatusDTO).subscribe({
      next: () => {
        this.refreshTable();
      }
    });
  }
}
