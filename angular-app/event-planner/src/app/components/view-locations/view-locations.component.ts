import { Component, NgZone } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { LocationsRepositoryService } from 'src/app/shared/services/locations-repository.service';
import { AddLocationComponent } from '../add-location/add-location.component';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { Location } from 'src/app/interfaces/location.dto';
import { Loader } from '@googlemaps/js-api-loader';

@Component({
  selector: 'app-view-locations',
  templateUrl: './view-locations.component.html',
  styleUrls: ['./view-locations.component.css']
})
export class ViewLocationsComponent {
  columnsToDisplay: string[] = ["name", "capacity"];
  dataSource:  MatTableDataSource<Location> = new MatTableDataSource<Location>;
  dialogRef: MatDialogRef<AddLocationComponent>;
  map: google.maps.Map;
  coordinates: [number, number][] = [];

  constructor(private locationsService: LocationsRepositoryService, public dialog: MatDialog, private zone: NgZone) { }

  ngOnInit(): void {
    this.refreshTable();
    this.locationsService.locationAdded.subscribe(() => {
      this.refreshTable();
    });
    setTimeout(() => {
      this.generateMap();
    }, 100);
  }

  refreshTable(): void {
    this.locationsService.getAllLocations("Location").subscribe({
      next: (response) => {
        this.dataSource.data = response.locations;
        this.dataSource.data.forEach(location => {
          this.coordinates.push([location.mapLatitude, location.mapLongitude]);
        });
      }
    });
  }

  openDialog(map: google.maps.Map, location: google.maps.LatLng, placeId: string): void {
    this.zone.run(() => {
      this.dialogRef = this.dialog.open(AddLocationComponent, {data: { location: location, map: map, placeId: placeId }});
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
          zoom: 12
        });
        this.map = map;
        map.addListener("click", (event: any) => {
          if(event.placeId != undefined)
            this.openDialog(map, event.latLng, event.placeId);
        });
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
  }
}
