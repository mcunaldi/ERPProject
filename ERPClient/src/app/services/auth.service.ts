import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { JwtPayload, jwtDecode } from 'jwt-decode';
import { UserModel } from '../models/user.model';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  token: string = "";
  user: UserModel = new UserModel();

  constructor(
    private router: Router
  ) { }

  isAuthenticated(){
    this.token = localStorage.getItem("token") ?? "";

    if(this.token === ""){
      this.router.navigateByUrl("/login");
      return false;
    }

    const decode: JwtPayload | any = jwtDecode(this.token);
    const expire = decode.exp;
    const now = new Date().getTime() / 1000;

    if(now > expire) {
      this.router.navigateByUrl("/login");
      return false;
    }

    this.user.id = decode["Id"];
    this.user.name = decode["Name"];
    this.user.email = decode["Email"];
    this.user.userName = decode["UserName"];
    
    //console.log(this.user);

    return true;
  }
}

// {
//   "Id": "f78778d3-d0e2-44d1-fbcb-08dc817faaf8",
//   "Name": "Taner Saydam",
//   "Email": "admin@admin.com",
//   "UserName": "admin",
//   "nbf": 1717254869,
//   "exp": 1719846869,
//   "iss": "Me",
//   "aud": "My Projects"
// }