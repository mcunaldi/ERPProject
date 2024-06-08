import { Component } from '@angular/core';
import { RequirementsPlanningModel } from '../../models/requirements-plannig.model';
import { CommonModule } from '@angular/common';
import { ActivatedRoute } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { HttpService } from '../../services/http.service';

@Component({
  selector: 'app-requirements-plannig',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './requirements-plannig.component.html',
  styleUrl: './requirements-plannig.component.css'
})
export class RequirementsPlannigComponent {
data: RequirementsPlanningModel = new RequirementsPlanningModel();
orderId: string = "";

constructor(
  private http: HttpService,
  private activated: ActivatedRoute
){
  this.activated.params.subscribe(res=> {
    this.orderId = res["orderId"];
    this.get();
  })
}

get(){
  this.http.post<RequirementsPlanningModel>("Orders/ReqirementsPlanningByOrderId", {orderId: this.orderId}, res=> {
    this.data = res;
  })
}

}
