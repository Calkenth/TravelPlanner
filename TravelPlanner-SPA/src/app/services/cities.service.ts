import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { IRoute } from '../route';

@Injectable()
export class CitiesService {
  private getCitiesURL = 'http://localhost:5000/TravelPlanner/GetCities';
  private getTravelsURL = 'http://localhost:5000/TravelPlanner/GetRoutes';
  private getTicketsURL = 'http://localhost:5000/TravelPlanner/GetTickets';

  constructor(private http: HttpClient) { }

  GetCities(): Observable<string[]> {
    return this.http.get<string[]>(this.getCitiesURL)
      .pipe(catchError(this.errorHandler));
  }

  GetAllRoutes() {
    return this.http.get<IRoute[]>(this.getTravelsURL)
    .pipe(catchError(this.errorHandler));
  }

  GetRoutes(fromCity: string, toCity: string): Observable<IRoute[]> {
    return this.http.get<IRoute[]>(this.getTravelsURL + '/' + fromCity + '/' + toCity)
    .pipe(catchError(this.errorHandler));
  }

  errorHandler(error: HttpErrorResponse) {
    return throwError(error.message || 'Server Error');
  }
}
