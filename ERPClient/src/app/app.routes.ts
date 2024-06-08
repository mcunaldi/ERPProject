import { Routes } from '@angular/router';
import { LoginComponent } from './components/login/login.component';
import { LayoutsComponent } from './components/layouts/layouts.component';
import { HomeComponent } from './components/home/home.component';
import { AuthService } from './services/auth.service';
import { inject } from '@angular/core';
import { CustomersComponent } from './components/customers/customers.component';
import { DepotsComponent } from './components/depots/depots.component';
import { ProductsComponent } from './components/products/products.component';
import { RecipesComponent } from './components/recipes/recipes.component';
import { RecipeDetailsComponent } from './components/recipe-details/recipe-details.component';
import { OrdersComponent } from './components/orders/orders.component';
import { RequirementsPlannigComponent } from './components/requirements-plannig/requirements-plannig.component';

export const routes: Routes = [
    {
        path: "login",
        component: LoginComponent
    },
    {
        path: "requirements-plannig/:orderId",
        component: RequirementsPlannigComponent,
        canActivate: [()=> inject(AuthService).isAuthenticated()]
    },
    {
        path: "",
        component: LayoutsComponent,
        canActivateChild: [() => inject(AuthService).isAuthenticated()],
        children: [
            {
                path: "",
                component: HomeComponent
            },
            {
                path: "customers",
                component: CustomersComponent
            },
            {
                path: "depots",
                component: DepotsComponent
            },
            {
                path: "products",
                component: ProductsComponent
            },
            {
                path: "recipes",
                component: RecipesComponent
            },
            {
                path: "recipe-details/:id",
                component: RecipeDetailsComponent
            },
            {
                path: "orders",
                component: OrdersComponent
            }
            
        ]
    }
];
