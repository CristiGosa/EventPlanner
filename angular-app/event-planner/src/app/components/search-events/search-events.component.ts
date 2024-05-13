import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { MatDatepicker } from '@angular/material/datepicker';
import { Event } from 'src/app/interfaces/event.dto';
import { EventsSearchFilters } from 'src/app/interfaces/events-search-filters.dto';
import { Location } from 'src/app/interfaces/location.dto';
import { SearchEventsService } from 'src/app/shared/services/search-events.service';
import { SearchPeriodValidatorService } from 'src/app/shared/services/search-period-validator.service';
import { TimePeriodValidatorService } from 'src/app/shared/services/time-period-validator.service';

@Component({
  selector: 'app-search-events',
  templateUrl: './search-events.component.html',
  styleUrls: ['./search-events.component.css']
})
export class SearchEventsComponent implements OnInit{
  @Input() unfilteredEvents: Array<Event> = [];
  @Input() locations: Location[] = [];
  @Output() onSearch: EventEmitter<Array<Event>> = new EventEmitter<Array<Event>>();

  isBto: boolean = false;
  isFreeEventSearch: boolean = false;

  searchFilters: FormGroup = new FormGroup(
    {
      startDate: new FormControl<Date | null>(null, []),
      endDate: new FormControl<Date | null>(null, []),
      locationId: new FormControl(-1, []),
      eventName: new FormControl('', []),
    },
    {
      validators: [
        this.timePeriodValidator.isTimePeriodValid,
        this.searchPeriodValidator.areSearchDatesValid,
      ],
      updateOn: 'blur',
    }
  );

  locationIds: Array<number> = [];

  constructor(
    private searchEventsService: SearchEventsService,
    private timePeriodValidator: TimePeriodValidatorService,
    private searchPeriodValidator: SearchPeriodValidatorService,
  ) {}

  ngOnInit(): void {
    this.markTouchedFields();
  }

  ngOnChanges(): void {
    this.locationIds = this.getAllLocationIds(this.unfilteredEvents);
  }

  searchEvents(): void {
    if (!this.searchFilters.valid) {
      return;
    }

    var eventSearchFilters: EventsSearchFilters = this.searchFilters.getRawValue();
    eventSearchFilters.freeEvent = this.isFreeEventSearch;
    const filteredEvents: Array<Event> = this.searchEventsService.searchEvents(
      this.unfilteredEvents,
      eventSearchFilters
    );
    this.onSearch.emit(filteredEvents);
  }

  clearFilters(event: MouseEvent): void {
    event.preventDefault();
    this.searchFilters.reset({
      startDate: null,
      endDate: null,
      locationId: -1,
      eventName: "",
    });
    this.isFreeEventSearch = false;
    this.markTouchedFields();
    this.onSearch.emit(this.unfilteredEvents);
  }

  getAllLocationIds(events: Array<Event>): Array<number> {
    const locationIds: Set<number> = new Set<number>();
    locationIds.add(-1);
    events.forEach((event) => locationIds.add(event.locationId));
    return Array.from(locationIds);
  }

  getLocationById(locationId: number): string | undefined {
    return this.locations.find(x => x.id == locationId)?.name;
  }

  getEndDateErrorMessage(): string {
    if (this.searchFilters.get('endDate')?.value == null) {
      return this.searchPeriodValidator.getEndDateErrorMessage(
        this.searchFilters
      );
    } else {
      return this.timePeriodValidator.getEndDateErrorMessage(
        this.searchFilters
      );
    }
  }

  getStartDateErrorMessage(): string {
    return this.searchPeriodValidator.getStartDateErrorMessage(
      this.searchFilters
    );
  }

  openCalendar(event: MouseEvent, calendar: MatDatepicker<Date>): void {
    event.preventDefault();
    calendar.open();
  }

  markTouchedFields(): void {
    this.searchFilters.get('startDate')?.markAsTouched();
    this.searchFilters.get('endDate')?.markAsTouched();
  }

  updateFreeEventSearch(){
    this.isFreeEventSearch = !this.isFreeEventSearch;
  }
}
