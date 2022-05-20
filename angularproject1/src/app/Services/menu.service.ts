import { Injectable } from '@angular/core';
import { InMemoryDataService } from './in-memory-data.service';
import { Observable, of } from 'rxjs';
import { MessageService } from './message.service';

import { Menu } from '../models/menu';

import { HttpClient, HttpHeaders } from '@angular/common/http';
import { catchError, tap } from 'rxjs/operators';


@Injectable({
  providedIn: 'root'
})
export class MenuService {

  constructor(
    private messageService: MessageService,
    private http: HttpClient
  ) { }

  /** Log a MenuService message with the MessageService */
  private log(message: string) {
    this.messageService.add(`MenuService: ${message}`);
  }

  private menusUrl = 'api/menus';

  getMenus(): Observable<Menu[]> {
    const menus = this.http.get<Menu[]>(this.menusUrl).pipe(tap(_ => this.log('fetched menus')), catchError(this.handleError<Menu[]>('getMenus', []))
    );

    return menus;
  }

  getMenu(id: number): Observable<Menu> {
    const url = `${this.menusUrl}/${id}`;
    return this.http.get<Menu>(url).pipe(tap(_ => this.log(`fetched menu id=${id}`)), catchError(this.handleError<Menu>(`getMenu id=${id}`)));
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

  updateMenu(menu: Menu): Observable<any> {
    return this.http.put(this.menusUrl, menu, this.httpOptions).pipe(
      tap(_ => this.log(`updated menu id=${menu.id}`)),
      catchError(this.handleError<any>('updateMenu'))
    );
  }

  addMenu(menu: Menu): Observable<any> {
    return this.http.post<Menu>(this.menusUrl, menu, this.httpOptions).pipe(
      tap((newMenu: Menu) => this.log(`added pice w/ id=${newMenu.id}`)),
      catchError(this.handleError<Menu>("addMenu"))
    );
  }

  deleteMenu(id: number): Observable<Menu> {
    const url = `${this.menusUrl}/${id}`;

    return this.http.delete<Menu>(url, this.httpOptions).pipe(
      tap(_ => this.log(`deleted menu id=${id}`)),
      catchError(this.handleError<Menu>('deleteMenu'))
    );
  }
}