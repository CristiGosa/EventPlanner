import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
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
  public locations: Location[];
  public eventForm: FormGroup;


  constructor(
    private locationsRepositoryService: LocationsRepositoryService,
    private eventsRepositoryService: EventsRepositoryService,
    public dialogRef: MatDialogRef<AddEventComponent>
  ) { }

  ngOnInit(): void {
    this.locationsRepositoryService.getAllLocations("Location").subscribe({
      next: (response) => {
        this.locations = response.locations;
      }
    });
    this.buildForm();
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

  public addEvent = () => {
    var selectedLocation = this.eventForm.controls["location"].value as Location;
    const createdEvent : CreateEventRequest = {
      name: this.eventForm.controls["name"].value,
      locationId: selectedLocation.id,
      ticketPrice: this.eventForm.controls["ticketPrice"].value,
      description: this.eventForm.controls["description"].value,
      startDate: this.eventForm.controls["startDate"].value,
      endDate: this.eventForm.controls["endDate"].value,
    };
    this.eventForm.markAllAsTouched();
    this.eventsRepositoryService.createEvent("Event", createdEvent).subscribe({
      next: () => {
        this.dialogRef.close();
      }
    });
  }

  public closeDialog(){
    this.dialogRef.close();
  }
}
