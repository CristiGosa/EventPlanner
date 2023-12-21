import { Component } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { LocationsRepositoryService } from 'src/app/shared/services/locations-repository.service';
import { AddLocationComponent } from '../add-location/add-location.component';
import { MatDialog, MatDialogConfig, MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-view-locations',
  templateUrl: './view-locations.component.html',
  styleUrls: ['./view-locations.component.css']
})
export class ViewLocationsComponent {
  columnsToDisplay: string[] = ["name", "capacity"];
  dataSource:  MatTableDataSource<Location>;
  dialogRef: MatDialogRef<AddLocationComponent>;

  desktopDialogConfig: MatDialogConfig = {
    width: '47%',
    height: '100vh',
    position: {
      left: '26.5%',
      right: '26.5%',
    }
  };

  constructor(private locationsService: LocationsRepositoryService, public dialog: MatDialog) { }

  ngOnInit(): void {
    this.refreshTable();
    this.locationsService.locationAdded.subscribe(() => {
      this.refreshTable();
    });

    this.dataSource = new MatTableDataSource<Location>();
  }

  refreshTable(): void {
    this.locationsService.getAllLocations("Location").subscribe({
      next: (response) => {
        this.dataSource.data = response.locations;
      }
    });
  }

  openDialog(): void {
    this.dialogRef = this.dialog.open(AddLocationComponent, this.desktopDialogConfig);
  }
}
