import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, Subject, tap } from 'rxjs';
import { EnvironmentUrlService } from './environment-url.service';
import { GetLocationsResponse } from 'src/app/interfaces/get-locations-response.dto';
import { Location } from 'src/app/interfaces/location.dto';


@Injectable()
export class LocationsRepositoryService {

  public locationAdded = new Subject<void>();

  public dataSubject = new Subject<void>();

  constructor(private http: HttpClient, private envUrl: EnvironmentUrlService) { }

  public createLocation(route: string, locationDTO: Location): Observable<Location> {
    return this.http.post<Location>(this.createCompleteRoute(route, this.envUrl.urlAddress), locationDTO)
      .pipe(
        tap(() => {
          this.locationAdded.next()
        }
        )
      )
  }

  getAllLocations(route: string): Observable<GetLocationsResponse> {
    return this.http.get<GetLocationsResponse>(this.createCompleteRoute(route, this.envUrl.urlAddress));
  }

  getLocationById(route: string, id: number): Observable<GetLocationsResponse> {
    let params = new HttpParams().set('id', id);
    return this.http.get<GetLocationsResponse>(this.createCompleteRoute(route + '/ById', this.envUrl.urlAddress), {params: params})
  }

  private createCompleteRoute(route: string, envAddress: string) {
    return envAddress + '/' + route;
  }
}
