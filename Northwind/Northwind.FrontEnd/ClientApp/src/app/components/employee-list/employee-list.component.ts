import { Component, OnInit, OnChanges, SimpleChanges } from '@angular/core';
import { PageChangedEvent } from 'src/app/models/common/page-changed.event';
import { Employee } from 'src/app/models/employee/employee.model';
import { EmployeeService } from 'src/app/services/employee/employee.service';
import { ListResult } from '../../models/common/list-result.model';

@Component({
  selector: 'app-employee-list',
  templateUrl: './employee-list.component.html',
  styleUrls: ['./employee-list.component.css']
})
export class EmployeeListComponent implements OnInit, OnChanges {

  employees: ListResult<Employee> = { totalCount: 0, items: [] };

  isLoading = true;

  pageSize = 5;
  pageIndex = 0;
  pageSizeOptions = [5, 10, 25, 100];
  totalCount = 0;

  orderBy = 'name';
  orderDirection = 'Asc';

  constructor(private _employeeService: EmployeeService) { }

  ngOnInit(): void {
    this.onPageChanged({ pageSize: this.pageSize, pageIndex: 0 });
  }

  ngOnChanges(changes: SimpleChanges): void {
    const newEmployees = changes.employees.currentValue;

    if (newEmployees === undefined) {
      this.totalCount = undefined;
    } else {
      this.totalCount = undefined;
      this.isLoading = false;
    }
  }

  onPageChanged(event: PageChangedEvent) {
    this.pageSize = event.pageSize;
    this.pageIndex = event.pageIndex;
    this.getSortedEmployees();
  }

  onOrderChanged(selected: string) {
    if (this.orderBy === selected) {
      this.orderDirection = this.orderDirection === 'Asc' ? 'Desc' : 'Asc';
    } else {
      this.orderDirection = 'Asc';
    }
    this.orderBy = selected;
    this.pageIndex = 0;
    this.getSortedEmployees();
  }

  getSortedEmployees() {
    this.isLoading = true;
    this._employeeService.getEmployees(this.pageSize, this.pageSize * this.pageIndex, this.orderBy + this.orderDirection)
      .subscribe((d: ListResult<Employee>) => {
        this.employees = d;
        this.totalCount = d.totalCount;
        this.isLoading = false;
      });
  }
}
