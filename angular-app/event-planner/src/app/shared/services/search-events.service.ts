import { Injectable } from '@angular/core';
import { Event } from 'src/app/interfaces/event.dto';
import { EventsSearchFilters } from 'src/app/interfaces/events-search-filters.dto';
import { Currency } from '../enums/currency';

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
      && this.doNumbersFullyMatch(event.locationId, filters.locationId)
      && this.doStringsPartiallyMatch(event.name, filters.eventName)
      && this.isFreeEventSearch(event, filters.freeEvent)
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

  doNumbersFullyMatch(actualNumber: number, filterNumber: number): boolean {
    return filterNumber == -1 || actualNumber == filterNumber;
  }

  doStringsPartiallyMatch(actualString: string, filterString: string): boolean {
    return (
      filterString == '' ||
      actualString.toLowerCase().includes(filterString.toLowerCase())
    );
  }

  isFreeEventSearch(event: Event, filterFreeEventSearch: boolean){
    return filterFreeEventSearch == false || event.priceCurrency == Currency.Free
  }
}
