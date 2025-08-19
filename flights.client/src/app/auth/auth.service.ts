import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor() { }

  currentUser?: User;

  loginUser(user: User) {
    console.log(`User logged in: ${user.email}`);
    this.currentUser = user;
  }
}

interface User {
  email: string | null | undefined;
}
