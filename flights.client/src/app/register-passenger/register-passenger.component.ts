import { Component } from '@angular/core';
import { PassengerService } from './../api/services/passenger.service';
import { FormBuilder } from '@angular/forms';
import { AuthService } from './../auth/auth.service';

@Component({
  selector: 'app-register-passenger',
  templateUrl: './register-passenger.component.html',
  styleUrls: ['./register-passenger.component.css']
})
export class RegisterPassengerComponent {

  constructor(private passengerService: PassengerService, private formBuilder: FormBuilder, private authService: AuthService) { }

  form = this.formBuilder.group({
    email: [''],
    firstName: [''],
    lastName: [''],
    isFemale: [true],
  })

  checkPassenger(): void {
    const params = { email: this.form.get('email')?.value ?? '' }

    this.passengerService
      .findPassenger(params)
      .subscribe(this.login);
  }

  register() {
    console.log("Form values:", this.form.value);
    this.passengerService.registerPassenger({ body: this.form.value })
      .subscribe(this.login, console.error);
  }

  private login = () => {
    this.authService.loginUser({ email: this.form.get('email')?.value });
  }
}
