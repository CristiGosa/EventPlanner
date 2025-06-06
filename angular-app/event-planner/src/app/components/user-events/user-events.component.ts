import { Component, ViewChild } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { Event } from 'src/app/interfaces/event.dto';
import { EventsRepositoryService } from 'src/app/shared/services/events-repository.service';
import { AddEventComponent } from '../add-event/add-event.component';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { EventStatus } from 'src/app/shared/enums/event-status';
import { RolesService } from 'src/app/shared/services/roles.service';
import { LocationsRepositoryService } from 'src/app/shared/services/locations-repository.service';
import { Location } from 'src/app/interfaces/location.dto';
import { SearchEventsComponent } from '../search-events/search-events.component';
import { Router } from '@angular/router';
import { PageEvent } from '@angular/material/paginator';

@Component({
  selector: 'app-user-events',
  templateUrl: './user-events.component.html',
  styleUrls: ['./user-events.component.css']
})
export class UserEventsComponent {
  @ViewChild(SearchEventsComponent) searchEventsComponent: SearchEventsComponent;
  columnsToDisplay: string[] = ["name", "location", "organizer", "ticketPrice", "startDate", "endDate", "description", "participants"];
  dataSource:  MatTableDataSource<Event>;
  dialogRef: MatDialogRef<AddEventComponent>;
  joinedEvents: Event[] = [];
  locations: Location[] = [];
  isButtonDisabled: boolean = false;
  filtersOn: boolean = false;
  filteredData: MatTableDataSource<Event>;
  currentPage: number = 0;
  pageSize: number = 6;

  constructor(
    private eventsService: EventsRepositoryService,
    public dialog: MatDialog,
    private router: Router,
    private rolesService: RolesService,
    private locationsService: LocationsRepositoryService
     ) { }

  ngOnInit(): void {
    if(this.rolesService.isAdmin())
    {
      this.columnsToDisplay.push("status");
      this.columnsToDisplay.push("buttons");
    }
    if(this.rolesService.isUser())
    {
      this.columnsToDisplay.push("buttons");
    }
    this.refreshTable();
    this.eventsService.eventAdded.subscribe(() => {
      this.refreshTable();
    });

    this.dataSource = new MatTableDataSource<Event>();
    this.filteredData = new MatTableDataSource(this.dataSource.data);
  }

  isUser(): boolean {
    return this.rolesService.isUser();
  }

  refreshTable(): void {
    if(this.rolesService.isAdmin())
    {
      this.eventsService.getAllEvents("Event").subscribe({
        next: (response) => {
          this.dataSource.data = response.events;
          this.filteredData.data = response.events;
        }
      });
    }
    else
    {
      this.eventsService.getByStatus("Event", EventStatus.Accepted).subscribe({
        next: (response) => {
          this.dataSource.data = response.events;
          this.filteredData.data = response.events;
        }
      });
    }
    this.eventsService.getAllEvents("Event/Joined").subscribe({
      next: (response) => {
        this.joinedEvents = response.events;
      }
    });
    this.getLocations()
    this.isButtonDisabled = false;
  }

  getLocations(): void {
    this.locationsService.getAllLocations("Location").subscribe({
      next: (response) => {
        this.locations = response.locations;
      }
    });
  }

  redirect(event: Event){
    this.router.navigateByUrl("/app/view-event-details", { state: { eventData: event } } );
  }

  handleSearchEvents(events: Array<Event>): void {
    this.filteredData.data = events;
  }

  verifyPhotoUrl(photoUrl: string | undefined) {
    return photoUrl != undefined ? photoUrl : "../assets/images/no-image.jpg"
  }

  toggleFilters(){
    this.filtersOn = !this.filtersOn
  }

  get paginatedData() {
    const start = this.currentPage * this.pageSize;
    const end = start + this.pageSize;
    return this.filteredData.data.slice(start, end);
  }

  onPageChange(event: PageEvent) {
    this.pageSize = event.pageSize;
    this.currentPage = event.pageIndex;
  }
}


