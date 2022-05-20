import { Injectable } from '@angular/core';
import { InMemoryDataService } from './in-memory-data.service';
import { Observable, of } from 'rxjs';
import { MessageService } from './message.service';

import { Plat } from '../models/plat';

import { HttpClient, HttpHeaders } from '@angular/common/http';
import { catchError, tap } from 'rxjs/operators';


@Injectable({
  providedIn: 'root'
})
export class PlatService {
  //plats : PLATS[] = [

  constructor(
    private messageService: MessageService,
    private http: HttpClient
  ) { }

  /** Log a PlatService message with the MessageService */
  private log(message: string) {
    this.messageService.add(`PlatService: ${message}`);
  }

  private platsUrl = 'api/plats';

  getPlats(): Observable<Plat[]> {
    const plats = this.http.get<Plat[]>(this.platsUrl).pipe(tap(_ => this.log('fetched plats')), catchError(this.handleError<Plat[]>('getPlats', []))
    );

    return plats;
  }

  getPlat(id: number): Observable<Plat> {
    const url = `${this.platsUrl}/${id}`;
    return this.http.get<Plat>(url).pipe(tap(_ => this.log(`fetched plat id=${id}`)), catchError(this.handleError<Plat>(`getPlat id=${id}`)));
  }



  /**
 * Handle Http operation that failed.
 * Let the app continue.
 *
 * @param operation - name of the operation that failed
 * @param result - optional value to return as the observable result
 */
  private handleError<T>(operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {

      // TODO: send the error to remote logging infrastructure
      console.error(error); // log to console instead
      // TODO: better job of transforming error for user consumption
      this.log(`${operation} failed: ${error.message}`);

      // Let the app keep running by returning an empty result.
      return of(result as T);
    };
  }

  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };

  updatePlat(plat: Plat): Observable<any> {
    return this.http.put(this.platsUrl, plat, this.httpOptions).pipe(
      tap(_ => this.log(`updated plat id=${plat.id}`)),
      catchError(this.handleError<any>('updatePlat'))
    );
  }

  addPlat(plat: Plat): Observable<any> {
    return this.http.post<Plat>(this.platsUrl, plat, this.httpOptions).pipe(
      tap((newPlat: Plat) => this.log(`added pice w/ id=${newPlat.id}`)),
      catchError(this.handleError<Plat>("addPlat"))
    );
  }

  deletePlat(id: number): Observable<Plat> {
    const url = `${this.platsUrl}/${id}`;

    return this.http.delete<Plat>(url, this.httpOptions).pipe(
      tap(_ => this.log(`deleted plat id=${id}`)),
      catchError(this.handleError<Plat>('deletePlat'))
    );
  }
}