import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, Subject, tap } from 'rxjs';
import { EnvironmentUrlService } from './environment-url.service';
import { GetEventsResponse } from 'src/app/interfaces/get-events-response.dto';
import { CreateEventRequest } from 'src/app/interfaces/create-event-request.dto';
import { JoinEventRequest } from 'src/app/interfaces/join-event.dto';


@Injectable()
export class EventReservationsRepositoryService {

  public eventReservationAdded = new Subject<void>();

  public dataSubject = new Subject<void>();

  constructor(private http: HttpClient, private envUrl: EnvironmentUrlService) { }

  public createEventReservation(route: string, eventReservationDTO: JoinEventRequest): Observable<JoinEventRequest> {
    return this.http.post<JoinEventRequest>(this.createCompleteRoute(route, this.envUrl.urlAddress), eventReservationDTO)
      .pipe(
        tap(() => {
          this.eventReservationAdded.next()
        }
        )
      )
  }

  private createCompleteRoute(route: string, envAddress: string) {
    return envAddress + '/' + route;
  }
}