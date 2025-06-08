import { HttpErrorResponse, HttpEventType } from '@angular/common/http';
import { Component } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';
import { Event } from 'src/app/interfaces/event.dto';
import { Location } from 'src/app/interfaces/location.dto';
import { UpdateEventStatusRequest } from 'src/app/interfaces/update-event-status-request.dto';
import { DialogWindowComponent } from 'src/app/shared/components/dialog-window/dialog-window.component';
import { Currency } from 'src/app/shared/enums/currency';
import { EventStatus } from 'src/app/shared/enums/event-status';
import { EventsRepositoryService } from 'src/app/shared/services/events-repository.service';
import { LocationsRepositoryService } from 'src/app/shared/services/locations-repository.service';
import { RolesService } from 'src/app/shared/services/roles.service';

@Component({
  selector: 'app-view-event-requests',
  templateUrl: './view-event-requests.component.html',
  styleUrls: ['./view-event-requests.component.css']
})
export class ViewEventRequestsComponent {
  columnsToDisplay: string[] = ["name", "location", "organizer", "ticketPrice", "startDate", "endDate", "description", "status", "buttons"];
  dataSource: Event[] = [];
  errorResp: HttpErrorResponse | null = null;
  locations: Location[] = [];
  isButtonDisabled: boolean = false;
  filtersOn: boolean = false;
  filteredData: Event[] = [];

  constructor(private eventsService: EventsRepositoryService,
    private rolesService: RolesService,
    private locationsService: LocationsRepositoryService,
    private router: Router,
    public dialog: MatDialog) {}

  ngOnInit(): void {
    this.refreshTable();
    this.eventsService.eventAdded.subscribe(() => {
      this.refreshTable();
    });
  }

  refreshTable(): void {
    this.locationsService.getAllLocations("Location").subscribe((response) => {
      this.locations = response.locations;
    });
    if(this.rolesService.isAdmin())
    {
      this.eventsService.getByStatus("Event", EventStatus.Pending).subscribe({
        next: (response) => {
          this.dataSource = response.events;
          this.filteredData = response.events;
        },
        error: (errorResponse) => {
          this.errorResp = errorResponse;
        } 
      });
    }
    else{
      this.eventsService.getAllEvents("Event/Owned").subscribe({
        next: (response) => {
          this.dataSource = response.events;
          this.filteredData = response.events;
        }
      });
    }
    this.isButtonDisabled = false;
  }

  getStatus(status: EventStatus): string {
    switch(status)
    {
      case EventStatus.Accepted: return "Approved";
      case EventStatus.Rejected: return "Rejectd";
      case EventStatus.Pending: return "Pending";
      case EventStatus.Cancelled: return "Canceled";
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
    this.isButtonDisabled = true;
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
    this.isButtonDisabled = true;
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
    this.isButtonDisabled = true;
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

  getLocation(locationId: number): string | undefined {
    var location = this.locations.find(x => x.id == locationId);
    return location?.name;
  }

  openDescriptionDialog(event: Event){
    this.router.navigateByUrl("/app/view-event-details", { state: { eventData: event } } );
  }

  getLink(locationId: number): void{
    var location = this.locations.find(x => x.id == locationId);
    window.open("https://www.google.com/maps/search/?api=1&query=Google&query_place_id=" + location?.placeId)?.focus();
  }

  getTicketPrice(event: Event): string{
    switch(event.priceCurrency){
      case Currency.Ron: {
        return event.ticketPrice + " RON";
      }
      case Currency.Euro: {
        return event.ticketPrice + " €";
      }
      case Currency.Free: {
        return "Intrare libera";
      }
      default: {
        return event.ticketPrice.toString();
      }
    }
  }

  toggleFilters(){
    this.filtersOn = !this.filtersOn
  }

  handleSearchEvents(events: Array<Event>): void {
    this.filteredData = events;
  }
}
