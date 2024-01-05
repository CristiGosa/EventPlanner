import { Component } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { Event } from 'src/app/interfaces/event.dto';
import { EventsRepositoryService } from 'src/app/shared/services/events-repository.service';
import { AddEventComponent } from '../add-event/add-event.component';
import { MatDialog, MatDialogConfig, MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-view-events',
  templateUrl: './view-events.component.html',
  styleUrls: ['./view-events.component.css']
})
export class ViewEventsComponent {
  columnsToDisplay: string[] = ["name", "location", "organizer", "ticketPrice", "startDate", "endDate", "description"];
  dataSource:  MatTableDataSource<Event>;
  dialogRef: MatDialogRef<AddEventComponent>;

  desktopDialogConfig: MatDialogConfig = {
    width: '47%',
    height: '100vh',
    position: {
      left: '26.5%',
      right: '26.5%',
    }
  };

  constructor(private eventsService: EventsRepositoryService, public dialog: MatDialog) { }

  ngOnInit(): void {
    this.refreshTable();
    this.eventsService.eventAdded.subscribe(() => {
      this.refreshTable();
    });

    this.dataSource = new MatTableDataSource<Event>();
  }

  refreshTable(): void {
    this.eventsService.getAllLocations("Event").subscribe({
      next: (response) => {
        this.dataSource.data = response.events;
      }
    });
  }

  openDialog(): void {
    this.dialogRef = this.dialog.open(AddEventComponent, this.desktopDialogConfig);
  }
}
