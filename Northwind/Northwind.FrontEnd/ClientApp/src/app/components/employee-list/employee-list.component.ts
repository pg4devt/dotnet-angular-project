import { Component, OnInit } from '@angular/core';
import { PageChangedEvent } from 'src/app/models/common/page-changed.event';
import { Employee } from 'src/app/models/employee/employee.model';
import { EmployeeService } from 'src/app/services/employee/employee.service';

@Component({
  selector: 'app-employee-list',
  templateUrl: './employee-list.component.html',
  styleUrls: ['./employee-list.component.css']
})
export class EmployeeListComponent implements OnInit {

  employees: Employee[] = [];

  isLoading = true;
  pageSize = 10;
  pageIndex = 0;

  constructor(private _employeeService: EmployeeService) { }

  ngOnInit(): void {
    this.onPageChanged({ pageSize: this.pageSize, pageIndex: 0 });
  }

  onPageChanged(event: PageChangedEvent) {
    this.pageSize = event.pageSize;
    this.pageIndex = event.pageIndex;
  }
}
