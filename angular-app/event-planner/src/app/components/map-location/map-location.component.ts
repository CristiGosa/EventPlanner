import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { Loader } from '@googlemaps/js-api-loader';
import { Location } from 'src/app/interfaces/location.dto';

@Component({
  selector: 'app-map-location',
  templateUrl: './map-location.component.html',
  styleUrls: ['./map-location.component.css']
})
export class MapLocationComponent  implements OnInit{
  map: google.maps.Map;

  constructor(
    @Inject(MAT_DIALOG_DATA) public data: Location,
    public dialogRef: MatDialogRef<MapLocationComponent>) {}


  ngOnInit(): void {
    this.generateMap();
  }

  public closeDialog(){
    this.dialogRef.close();
  }

  private generateMap(){
    let loader = new Loader({
      apiKey: 'AIzaSyB4peyn0G6T8Kcg3UemZx146WJ94LgPfX4'
    });

    var element = document.getElementById('map')!;
      loader.load().then(() => {
         const map = new google.maps.Map(element, {
          center: {lat: this.data.mapLatitude, lng: this.data.mapLongitude},
          zoom: 15
        });
        this.map = map;
        var marker = new google.maps.Marker({
          position: {
            lat: this.data.mapLatitude,
            lng: this.data.mapLongitude
          },
          map: this.map
        });
      });
  }
}
