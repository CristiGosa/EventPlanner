import { Component, ViewChild } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { Event } from 'src/app/interfaces/event.dto';
import { EventsRepositoryService } from 'src/app/shared/services/events-repository.service';
import { AddEventComponent } from '../add-event/add-event.component';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { EventStatus } from 'src/app/shared/enums/event-status';
import { RolesService } from 'src/app/shared/services/roles.service';
import { UpdateEventStatusRequest } from 'src/app/interfaces/update-event-status-request.dto';
import { JoinEventRequest } from 'src/app/interfaces/join-event.dto';
import { EventReservationsRepositoryService } from 'src/app/shared/services/event-reservations-repository.service';
import { LocationsRepositoryService } from 'src/app/shared/services/locations-repository.service';
import { Location } from 'src/app/interfaces/location.dto';
import { DialogWindowComponent } from 'src/app/shared/components/dialog-window/dialog-window.component';
import { ViewParticipantsComponent } from '../view-participants/view-participants.component';
import { Currency } from 'src/app/shared/enums/currency';
import { SearchEventsComponent } from '../search-events/search-events.component';
import { Router } from '@angular/router';

@Component({
  selector: 'app-view-events',
  templateUrl: './view-events.component.html',
  styleUrls: ['./view-events.component.css']
})
export class ViewEventsComponent {
  @ViewChild(SearchEventsComponent) searchEventsComponent: SearchEventsComponent;
  columnsToDisplay: string[] = ["name", "location", "organizer", "ticketPrice", "startDate", "endDate", "description", "participants"];
  dataSource: Event[] = [];
  filteredData: Event[] = [];
  dialogRef: MatDialogRef<AddEventComponent>;
  joinedEvents: Event[] = [];
  locations: Location[] = [];
  isButtonDisabled: boolean = false;
  filtersOn: boolean = false;

  constructor(
    private eventsService: EventsRepositoryService,
    private router: Router,
    public dialog: MatDialog,
    private rolesService: RolesService,
    private eventReservationsService: EventReservationsRepositoryService,
    private locationsService: LocationsRepositoryService
     ) { }

  ngOnInit(): void {
    if(this.rolesService.isAdmin())
    {
      this.columnsToDisplay.push("status");
      this.columnsToDisplay.push("buttons");
    }
    if(this.rolesService.isUser())
    {
      this.columnsToDisplay.push("buttons");
    }
    this.refreshTable();
    this.eventsService.eventAdded.subscribe(() => {
      this.refreshTable();
    });

    this.filteredData = this.dataSource;
  }

  isAdmin(): boolean {
    return this.rolesService.isAdmin();
  }

  isOrganizer(): boolean {
    return this.rolesService.isOrganizer();
  }

  isUser(): boolean {
    return this.rolesService.isUser();
  }

  getStatus(status: EventStatus): string {
    switch(status)
    {
      case EventStatus.Accepted: return "Approved";
      case EventStatus.Rejected: return "Rejected";
      case EventStatus.Pending: return "Pending";
      case EventStatus.Cancelled: return "Canceled";
      default: return "Undefined";
    }
  }

  getLocations(): void {
    this.locationsService.getAllLocations("Location").subscribe({
      next: (response) => {
        this.locations = response.locations;
      }
    });
  }

  getLocation(locationId: number): string | undefined {
    var location = this.locations.find(x => x.id == locationId);
    return location?.name;
  }

  isPending(event: Event): boolean{
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

  joinEvent(eventId: number): void {
    this.isButtonDisabled = true;
    const joinEventRequest: JoinEventRequest = {
      eventId: eventId
    }

    this.eventReservationsService.createEventReservation("EventReservations", joinEventRequest).subscribe({
      next: () => {
        this.refreshTable();
      }
    });
  }

  isEventJoined(eventId: number): boolean {
    if(this.joinedEvents.find(x => x.id == eventId) != undefined)
          return true;
    return false;
  }

  isEventFull(event: Event): boolean {
    var location = this.locations.find(x => x.id == event.locationId);
    if(location?.capacity != undefined){
      if(location.capacity <= event.participantsNumber){
        return true;
      }
    }
    return false;
  }

  getParticipantsNumber(event: Event): string {
    if(event.status != EventStatus.Accepted)
      return " - ";
    var location = this.locations.find(x => x.id == event.locationId);
    return event.participantsNumber + "/" + location?.capacity;
  }

  refreshTable(): void {
    if(this.rolesService.isAdmin())
    {
      this.eventsService.getAllEvents("Event").subscribe({
        next: (response) => {
          this.dataSource = response.events;
          this.filteredData = response.events;
        }
      });
    }
    else
    {
      this.eventsService.getByStatus("Event", EventStatus.Accepted).subscribe({
        next: (response) => {
          this.dataSource = response.events;
          this.filteredData = response.events;
        }
      });
    }
    this.eventsService.getAllEvents("Event/Joined").subscribe({
      next: (response) => {
        this.joinedEvents = response.events;
      }
    });
    this.getLocations()
    this.isButtonDisabled = false;
  }

  openDialog(): void {
    this.dialogRef = this.dialog.open(AddEventComponent);
    this.dialogRef.afterClosed().subscribe(() => {
      this.searchEventsComponent.clearFilters(new MouseEvent('click'));
    })
  }

  openDescriptionDialog(event: Event){
    this.router.navigateByUrl("/app/view-event-details", { state: { eventData: event } } );
  }

  openParticipantsDialog(eventId: number, eventName: string){
    this.eventReservationsService.getParticipantsListByEventId("EventReservations", eventId).subscribe((result) => {
      if(result.participants != null){
        this.dialog.open(ViewParticipantsComponent, { data: { participants: result.participants, eventName: eventName } });
      }
    });
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

  handleSearchEvents(events: Array<Event>): void {
    this.filteredData = events;
  }

  toggleFilters(){
    this.filtersOn = !this.filtersOn
  }
}
