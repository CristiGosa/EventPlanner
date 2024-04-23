import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { Loader } from '@googlemaps/js-api-loader';
import { CreateEventRequest } from 'src/app/interfaces/create-event-request.dto';
import { Location } from 'src/app/interfaces/location.dto';
import { EventsRepositoryService } from 'src/app/shared/services/events-repository.service';
import { LocationsRepositoryService } from 'src/app/shared/services/locations-repository.service';

@Component({
  selector: 'app-add-event',
  templateUrl: './add-event.component.html',
  styleUrls: ['./add-event.component.css']
})
export class AddEventComponent {
  public locations: Location[] = [];
  public eventForm: FormGroup;
  public coordinates: [number, number][] = [];
  public map:  google.maps.Map;
  public selectedLocation: Location | undefined;
  private lastMarker: google.maps.Marker;


  constructor(
    private locationsRepositoryService: LocationsRepositoryService,
    private eventsRepositoryService: EventsRepositoryService,
    public dialogRef: MatDialogRef<AddEventComponent>
  ) { }

  ngOnInit(): void {
    this.getLocations();
    this.buildForm();
    setTimeout(() => {
      this.generateMap();
    }, 100);
  }

  private buildForm(): void {
    this.eventForm = new FormGroup(
      {
        name: new FormControl('', [Validators.required]),
        location: new FormControl('', [Validators.required]),
        ticketPrice: new FormControl('', [Validators.required]),
        description: new FormControl('', [Validators.required]),
        startDate: new FormControl('', [Validators.required]),
        endDate: new FormControl('', [Validators.required]),
      }
    );
  }

  private getLocations(){
    this.locationsRepositoryService.getAllLocations("Location").subscribe({
      next: (response) => {
        this.locations = response.locations;
        this.locations.forEach(location => {
          this.coordinates.push([location.mapLatitude, location.mapLongitude]);
        });
      }
    });
  }

  public addEvent = () => {
    var location = this.selectedLocation as Location;

    var startingDate = new Date(this.eventForm.controls["startDate"].value);
    var endingDate = new Date(this.eventForm.controls["endDate"].value);
    startingDate.setDate(startingDate.getDate() + 1);
    endingDate.setDate(endingDate.getDate() + 1);

    const createdEvent : CreateEventRequest = {
      name: this.eventForm.controls["name"].value,
      locationId: location.id,
      ticketPrice: this.eventForm.controls["ticketPrice"].value,
      description: this.eventForm.controls["description"].value,
      startDate: startingDate,
      endDate: endingDate,
    };
    this.eventForm.markAllAsTouched();
    this.eventsRepositoryService.createEvent("Event", createdEvent).subscribe({
      next: () => {
        this.dialogRef.close();
      }
    });
  }

  private generateMap(){
    let loader = new Loader({
      apiKey: 'AIzaSyB4peyn0G6T8Kcg3UemZx146WJ94LgPfX4'
    });

    var element = document.getElementById('map')!;
      loader.load().then(() => {
         const map = new google.maps.Map(element, {
          center: {lat: 45.755440, lng: 21.228242},
          zoom: 11
        });
        this.map = map;
        this.coordinates.forEach(coordinate => {
          this.placeMarker(coordinate[0], coordinate[1]);
        });
      });
  }

  private placeMarker(latitude: any, longitude: any): void {
    var marker = new google.maps.Marker({
      position: {
        lat: latitude,
        lng: longitude
      },
      map: this.map
    });
    this.lastMarker = marker;
    marker.addListener("click", (event: any) => {
      this.lastMarker.setIcon(null);
      marker.setIcon('http://maps.google.com/mapfiles/ms/icons/blue-dot.png')
      this.selectedLocation = this.locations.find(location => 
        location.mapLatitude == event.latLng.lat() 
        && location.mapLongitude == event.latLng.lng());
      this.lastMarker = marker;
    });
  }

  public showSelectedLocation(): string{
    if(!this.selectedLocation){
      return "Click pe un punct de pe harta pentru a selecta locatia";
    }
    return this.selectedLocation.name + " (Capacitate: " + this.selectedLocation.capacity + ")";
  }

  public closeDialog(){
    this.dialogRef.close();
  }
}
