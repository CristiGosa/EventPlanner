import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, Subject, tap } from 'rxjs';
import { EnvironmentUrlService } from './environment-url.service';
import { GetEventsResponse } from 'src/app/interfaces/get-events-response.dto';
import { CreateEventRequest } from 'src/app/interfaces/create-event-request.dto';
import { UpdateEventStatusRequest } from 'src/app/interfaces/update-event-status-request.dto';


@Injectable()
export class EventsRepositoryService {

  public eventAdded = new Subject<void>();

  public dataSubject = new Subject<void>();

  constructor(private http: HttpClient, private envUrl: EnvironmentUrlService) { }

  public createEvent(route: string, eventDTO: CreateEventRequest): Observable<CreateEventRequest> {
    return this.http.post<CreateEventRequest>(this.createCompleteRoute(route, this.envUrl.urlAddress), eventDTO)
      .pipe(
        tap(() => {
          this.eventAdded.next()
        }
        )
      )
  }

  public getAllEvents(route: string): Observable<GetEventsResponse> {
    return this.http.get<GetEventsResponse>(this.createCompleteRoute(route, this.envUrl.urlAddress));
  }

  public updateStatus(route: string, updateStatusDTO: UpdateEventStatusRequest): Observable<UpdateEventStatusRequest> {
    return this.http.put<UpdateEventStatusRequest>(this.createCompleteRoute(route, this.envUrl.urlAddress), updateStatusDTO)
    .pipe(
      tap(() => {
        this.eventAdded.next()
      }
    )
    )
  }

  private createCompleteRoute(route: string, envAddress: string) {
    return envAddress + '/' + route;
  }
}
