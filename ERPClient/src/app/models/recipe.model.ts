import { ProductModel } from "./product.model";
import { RecipeDetailModel } from "./recipeDetail.model";

export class RecipeModel{
    id: string = "";
    productId: string = "";
    product: ProductModel = new ProductModel();
    details: RecipeDetailModel[] = [];
}