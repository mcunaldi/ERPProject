import { Routes } from '@angular/router';
import { LoginComponent } from './components/login/login.component';
import { LayoutsComponent } from './components/layouts/layouts.component';
import { HomeComponent } from './components/home/home.component';
import { AuthService } from './services/auth.service';
import { inject } from '@angular/core';
import { CustomersComponent } from './components/customers/customers.component';
import { DepoComponent } from './components/depo/depo.component';

export const routes: Routes = [
    {
        path: "login",
        component: LoginComponent
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
                path: "depo",
                component: DepoComponent
            }
        ]
    }
];
