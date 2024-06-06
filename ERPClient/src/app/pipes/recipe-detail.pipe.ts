import { Pipe, PipeTransform } from '@angular/core';
import { RecipeDetailModel } from '../models/recipeDetail.model';

@Pipe({
  name: 'recipeDetail',
  standalone: true
})
export class RecipeDetailPipe implements PipeTransform {

  transform(value: RecipeDetailModel[], search: string): RecipeDetailModel[] {
    if (!search) {
      return value;
    }

    return value.filter(p =>
      p.product.name.toLocaleLowerCase().includes(search.toLocaleLowerCase())
    )    
  }

}
