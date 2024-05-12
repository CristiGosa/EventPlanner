import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { MatDatepicker } from '@angular/material/datepicker';
import { Event } from 'src/app/interfaces/event.dto';
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
  @Output() onSearch: EventEmitter<Array<Event>> = new EventEmitter<Array<Event>>();

  isBto: boolean = false;

  searchFilters: FormGroup = new FormGroup(
    {
      startDate: new FormControl<Date | null>(null, []),
      endDate: new FormControl<Date | null>(null, []),
      location: new FormControl('', []),
    },
    {
      validators: [
        this.timePeriodValidator.isTimePeriodValid,
        this.searchPeriodValidator.areSearchDatesValid,
      ],
      updateOn: 'blur',
    }
  );

  locations: Array<string> = [];

  constructor(
    private searchEventsService: SearchEventsService,
    private timePeriodValidator: TimePeriodValidatorService,
    private searchPeriodValidator: SearchPeriodValidatorService,
  ) {}

  ngOnInit(): void {
    this.markTouchedFields();
  }

  searchEvents(): void {
    if (!this.searchFilters.valid) {
      return;
    }

    const filteredEvents: Array<Event> = this.searchEventsService.searchEvents(
      this.unfilteredEvents,
      this.searchFilters.getRawValue()
    );
    this.onSearch.emit(filteredEvents);
  }

  clearFilters(event: MouseEvent): void {
    event.preventDefault();
    this.searchFilters.reset({
      startDate: null,
      endDate: null,
    });
    this.markTouchedFields();
    this.onSearch.emit(this.unfilteredEvents);
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
}
