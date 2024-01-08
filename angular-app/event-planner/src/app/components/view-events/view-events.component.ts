import { Component } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { Event } from 'src/app/interfaces/event.dto';
import { EventsRepositoryService } from 'src/app/shared/services/events-repository.service';
import { AddEventComponent } from '../add-event/add-event.component';
import { MatDialog, MatDialogConfig, MatDialogRef } from '@angular/material/dialog';
import { EventStatus } from 'src/app/shared/enums/event-status';
import { RolesService } from 'src/app/shared/services/roles.service';
import { UpdateEventStatusRequest } from 'src/app/interfaces/update-event-status-request.dto';

@Component({
  selector: 'app-view-events',
  templateUrl: './view-events.component.html',
  styleUrls: ['./view-events.component.css']
})
export class ViewEventsComponent {
  columnsToDisplay: string[] = ["name", "location", "organizer", "ticketPrice", "startDate", "endDate", "description", "status"];
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

  constructor(
    private eventsService: EventsRepositoryService,
    public dialog: MatDialog,
    private rolesService: RolesService
     ) { }

  ngOnInit(): void {
    if(this.rolesService.isAdmin())
    {
      this.columnsToDisplay.push("buttons");
    }
    this.refreshTable();
    this.eventsService.eventAdded.subscribe(() => {
      this.refreshTable();
    });

    this.dataSource = new MatTableDataSource<Event>();
  }

  getStatus(status: EventStatus): string {
    switch(status)
    {
      case EventStatus.Accepted: return "Accepted";
      case EventStatus.Rejected: return "Rejected";
      case EventStatus.Pending: return "Pending";
      case EventStatus.Cancelled: return "Cancelled";
      default: return "Undefined";
    }
  }

  isPending(event: Event){
    if(event.status === EventStatus.Pending)
      return true;
    return false;
  }

  acceptEvent(eventId: number): void{
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
