import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'filterlistOfUsers'
})
export class FilterListOfUsersPipe implements PipeTransform {

  transform(listOfUsers: any, term: any): any {

    //if dont have search
    if (term === undefined) { return listOfUsers }

    return listOfUsers.filter(function (user) {
      return (user.firstName+' '+user.lastName).toLowerCase().includes(term.toLowerCase());
    })

  }

}
