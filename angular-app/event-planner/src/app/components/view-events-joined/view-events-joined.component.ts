import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatTableDataSource } from '@angular/material/table';
import { Event } from 'src/app/interfaces/event.dto';
import { Location } from 'src/app/interfaces/location.dto';
import { EventStatus } from 'src/app/shared/enums/event-status';
import { EventsRepositoryService } from 'src/app/shared/services/events-repository.service';
import { LocationsRepositoryService } from 'src/app/shared/services/locations-repository.service';
import { MapLocationComponent } from '../map-location/map-location.component';
import { ViewParticipantsComponent } from '../view-participants/view-participants.component';
import { EventReservationsRepositoryService } from 'src/app/shared/services/event-reservations-repository.service';
import { Currency } from 'src/app/shared/enums/currency';
import { Router } from '@angular/router';

@Component({
  selector: 'app-view-events-joined',
  templateUrl: './view-events-joined.component.html',
  styleUrls: ['./view-events-joined.component.css']
})
export class ViewEventsJoinedComponent implements OnInit {
  columnsToDisplay: string[] = ["name", "location", "organizer", "ticketPrice", "startDate", "endDate", "description", "participants"];
  dataSource:  MatTableDataSource<Event>;
  locations: Location[];
  filteredData: MatTableDataSource<Event>;
  
  constructor(
    private eventsService: EventsRepositoryService,
    private eventReservationsService: EventReservationsRepositoryService,
    private locationsService: LocationsRepositoryService,
    public dialog: MatDialog,
    public router: Router,
     ) { }

  ngOnInit(): void {
    this.refreshTable();
    this.dataSource = new MatTableDataSource<Event>();
    this.filteredData = new MatTableDataSource(this.dataSource.data);
  }

  refreshTable(): void {
    this.eventsService.getAllEvents("Event/Joined").subscribe({
      next: (response) => {
        this.dataSource.data = response.events;
        this.filteredData.data = response.events;
      }
    })
    this.getLocations()
  }

  getLocations(): void {
    this.locationsService.getAllLocations("Location").subscribe({
      next: (response) => {
        this.locations = response.locations;
      }
    });
  }

  getParticipantsNumber(event: Event): string {
    if(event.status != EventStatus.Accepted)
      return " - ";
    var location = this.locations.find(x => x.id == event.locationId);
    return event.participantsNumber + "/" + location?.capacity;
  }

  getLocation(locationId: number): string | undefined {
    var location = this.locations.find(x => x.id == locationId);
    return location?.name;
  }

  openMapLocationDialog(locationId: number){
    var location = this.locations.find(x => x.id == locationId);
    this.dialog.open(MapLocationComponent, { data: location });
  }

  getLink(locationId: number): void{
    var location = this.locations.find(x => x.id == locationId);
    window.open("https://www.google.com/maps/search/?api=1&query=Google&query_place_id=" + location?.placeId)?.focus();
  }

  openParticipantsDialog(eventId: number, eventName: string){
    this.eventReservationsService.getParticipantsListByEventId("EventReservations", eventId).subscribe((result) => {
      if(result.participants != null){
        this.dialog.open(ViewParticipantsComponent, { data: { participants: result.participants, eventName: eventName } });
      }
    });
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
    this.filteredData.data = events;
  }

  redirect(event: Event){
    this.router.navigateByUrl("/app/view-event-details", { state: { eventData: event } } );
  }
}
