import { Component, Inject } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { Location } from 'src/app/interfaces/location.dto';
import { LocationsRepositoryService } from 'src/app/shared/services/locations-repository.service';

@Component({
  selector: 'app-add-location',
  templateUrl: './add-location.component.html',
  styleUrls: ['./add-location.component.css']
})
export class AddLocationComponent {
  public locationForm: FormGroup;
  private viewLocationsUrl: string = 'app/view-locations';
  private createdLocation: Location;


  constructor(
    @Inject(MAT_DIALOG_DATA) public data: any,
    private formBuilder: FormBuilder,
    private locationsRepositoryService: LocationsRepositoryService,
    private router: Router,
    public dialogRef: MatDialogRef<AddLocationComponent>
  ) { }

  ngOnInit(): void {
    this.buildLocationForm();
  }

  public buildLocationForm(): void {
    this.locationForm = this.formBuilder.group({
      name: ['', Validators.required],
      capacity: ['', [Validators.required, Validators.pattern('^\\d+$')]]
    });
  }

  public addLocation = () => {
    this.locationForm.markAllAsTouched();
    this.createdLocation = this.locationForm.value as Location
    this.createdLocation.mapLatitude = this.data.location.lat();
    this.createdLocation.mapLongitude = this.data.location.lng();
    this.locationsRepositoryService.createLocation("Location", this.createdLocation).subscribe({
      next: () => {
        this.dialogRef.close();
        this.placeMarker(this.createdLocation.mapLatitude, this.createdLocation.mapLongitude);
        this.router.navigate([this.viewLocationsUrl]);
      }
    });
  }

  private placeMarker(latitude: any, longitude: any): void {
    var marker = new google.maps.Marker({
      position: {
        lat: latitude,
        lng: longitude
      },
      map: this.data.map
    });
  }

  public closeDialog(){
    this.dialogRef.close();
  }
}
