import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'filterVacationrequest'
})
export class FilterVacationRequestPipe implements PipeTransform {

  transform(vacationRequests: any, term: any): any {

    //if dont have search
    if (term === undefined) { return vacationRequests }

    return vacationRequests.filter(function (vacationRequest) {
      return vacationRequest.userName.toLowerCase().includes(term.toLowerCase());
    })

  }

}
