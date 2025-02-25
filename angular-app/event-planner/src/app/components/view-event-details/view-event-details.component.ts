import { DatePipe } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { Event } from 'src/app/interfaces/event.dto';
import { JoinEventRequest } from 'src/app/interfaces/join-event.dto';
import { Location } from 'src/app/interfaces/location.dto';
import { Currency } from 'src/app/shared/enums/currency';
import { EventReservationsRepositoryService } from 'src/app/shared/services/event-reservations-repository.service';
import { EventsRepositoryService } from 'src/app/shared/services/events-repository.service';
import { LocationsRepositoryService } from 'src/app/shared/services/locations-repository.service';
import { RolesService } from 'src/app/shared/services/roles.service';

@Component({
  selector: 'app-view-event-details',
  templateUrl: './view-event-details.component.html',
  styleUrls: ['./view-event-details.component.css']
})
export class ViewEventDetailsComponent implements OnInit {
  event: Event;
  joinedEvents: Event[] = [];
  locations: Location[] = [];
  isButtonDisabled: boolean = false;

  constructor(
    private datePipe: DatePipe,
    private locationsService: LocationsRepositoryService,
    private eventReservationsService: EventReservationsRepositoryService,
    private eventsService: EventsRepositoryService,
    private rolesService: RolesService,
  ) {}

  ngOnInit(): void {
    this.event = history.state.eventData;
    console.log(this.event);
    this.getLocations();
    this.getJoinedEvents();
  }

  isUser(): boolean {
    return this.rolesService.isUser();
  }

  verifyPhotoUrl(photoUrl: string | undefined) {
    return photoUrl != undefined ? photoUrl : "../assets/images/no-image.jpg"
  }

  getDateString(startDate: Date, endDate:Date): string | undefined{
    if(startDate === endDate){
      return this.datePipe.transform(startDate, "dd/MM/yyyy")?.toString();
    }
    return this.datePipe.transform(startDate, "dd/MM/yyyy")?.toString() + " - "  + this.datePipe.transform(startDate, "dd/MM/yyyy")?.toString();
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

  getLocations(): void {
    this.locationsService.getAllLocations("Location").subscribe({
      next: (response) => {
        this.locations = response.locations;
      }
    });
  }

  getLink(locationId: number): void{
    var location = this.locations.find(x => x.id == locationId);
    window.open("https://www.google.com/maps/search/?api=1&query=Google&query_place_id=" + location?.placeId)?.focus();
  }

  getLocation(locationId: number): string | undefined {
    var location = this.locations.find(x => x.id == locationId);
    return location?.name;
  }

  getJoinedEvents(){
    this.eventsService.getAllEvents("Event/Joined").subscribe({
      next: (response) => {
        this.joinedEvents = response.events;
        console.log(this.joinedEvents)
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
          this.getJoinedEvents();
          this.isButtonDisabled = false;
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
}
