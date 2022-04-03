import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Employee } from 'src/app/models/employee/employee.model';
import { EmployeeService } from 'src/app/services/employee/employee.service';

@Component({
  selector: 'app-employee-detail',
  templateUrl: './employee-detail.component.html',
  styleUrls: ['./employee-detail.component.css']
})
export class EmployeeDetailComponent implements OnInit {

  isLoading = true;
  employee: Employee;

  constructor(private _route: ActivatedRoute,
    private _employeeService: EmployeeService) { }

  ngOnInit(): void {
    this.processEmployee();
  }

  processEmployee() {
    this._employeeService.getEmployee(parseInt(this._route.snapshot.paramMap.get('employeeId'), 10))
      .subscribe((d: Employee) => {
        this.employee = d;
        this.isLoading = false;
      });
  }
}
