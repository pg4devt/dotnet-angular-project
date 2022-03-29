import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Observable, of } from 'rxjs';
import { catchError, map, skip } from 'rxjs/operators';
import { Employee } from 'src/app/models/employee/employee.model';
import { ListResult } from 'src/app/models/common/list-result.model';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class EmployeeService {

  baseUrl = environment.baseUrl;

  constructor(private _httpClient: HttpClient, private _router: Router) { }

  public getEmployees(top: number, skip: number, orderBy: string): Observable<ListResult<Employee>> {
    return this._httpClient
      .get<ListResult<Employee>>(`${this.baseUrl}/employees?orderBy=${orderBy}&top=${top}&skip=${skip}`);
  }

  public getEmployee(employeeId: number): Observable<Employee> {
    return this._httpClient
      .get<Employee>(`${this.baseUrl}/employees/${employeeId}`)
      .pipe(
        catchError(error => this.throwError(error))
      )
  }

  createEmployee(employee: Employee): Observable<Employee> {
    return this._httpClient
      .post<Employee>(`${this.baseUrl}/employees`, employee)
      .pipe(
        map((data: Employee) => {
          return data;
        }),
        catchError(error => this.throwError(error))
      )

  }

  updateEmployee(employee: Employee, employeeId: number): Observable<any> {
    return this._httpClient
      .put<Employee>(`${this.baseUrl}/employees/${employeeId}`, employee)
      .pipe(
        map((data: Employee) => {
          return data;
        }),
        catchError(error => this.throwError(error))
      )

  }

  deleteEmployee(employeeId: number): Observable<any> {
    return this._httpClient.delete<Employee>(`${this.baseUrl}/employees/${employeeId}`)
      .pipe(
        map((data: Employee) => {
          return data;
        }),
        catchError(error => this.throwError(error))
      )
  }

  private mapEmployeeListResult(list: ListResult<Employee>): ListResult<Employee> {
    return {
      totalCount: list.totalCount,
      items: list.items.map(item => {
        return {
            ...item
        };
      })
    }
  }

  private throwError(error: any) {
    return Observable.throw(error.json().error || 'Server error');
  }

  private handleServiceError(error: HttpErrorResponse, router: Router) {
    router.navigate(['/error'], { queryParams: { error: JSON.stringify(error), url: router.url }, skipLocationChange: true });
  }
}
