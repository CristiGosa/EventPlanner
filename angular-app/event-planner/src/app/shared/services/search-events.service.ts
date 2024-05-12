import { Injectable } from '@angular/core';
import { Event } from 'src/app/interfaces/event.dto';
import { EventsSearchFilters } from 'src/app/interfaces/events-search-filters.dto';

@Injectable()
export class SearchEventsService {
  constructor() {}

  searchEvents(
    unfilteredEvents: Array<Event>,
    filters: EventsSearchFilters
  ): Array<Event> {
    return unfilteredEvents.filter((event) => this.shouldShowEvent(event, filters));
  }

  shouldShowEvent(event: Event, filters: EventsSearchFilters): boolean {
    const actualStartDate: Date = new Date(event.startDate);
    const actualEndDate: Date = new Date(event.endDate);
    const filterStartDate: Date | null = filters.startDate;
    const filterEndDate: Date | null = filters.endDate;
    actualStartDate.setHours(0, 0, 0, 0);
    actualEndDate.setHours(0, 0, 0, 0);
    filterStartDate?.setHours(0, 0, 0, 0);
    filterEndDate?.setHours(0, 0, 0, 0);
    return (
      this.isPeriodBetweenDates(
        actualStartDate,
        actualEndDate,
        filterStartDate,
        filterEndDate
      )
    );
  }

  isPeriodBetweenDates(
    actualStartDate: Date,
    actualEndDate: Date,
    filterStartDate: Date | null,
    filterEndDate: Date | null
  ): boolean {
    if (filterStartDate == null && filterEndDate == null) {
      return true;
    }
    if (filterStartDate == null) {
      return actualStartDate <= filterEndDate!;
    }
    if (filterEndDate == null) {
      return actualEndDate >= filterStartDate!;
    }
    return (
      actualStartDate >= filterStartDate! && actualEndDate <= filterEndDate!
    );
  }
}
