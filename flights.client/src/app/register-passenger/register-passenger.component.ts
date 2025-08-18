import { Component } from '@angular/core';
import { PassengerService } from './../api/services/passenger.service';
import { FormBuilder } from '@angular/forms';

@Component({
  selector: 'app-register-passenger',
  templateUrl: './register-passenger.component.html',
  styleUrls: ['./register-passenger.component.css']
})
export class RegisterPassengerComponent {

  constructor(private passengerService: PassengerService, private formBuilder: FormBuilder) { }

  form = this.formBuilder.group({
    email: [''],
    firstName: [''],
    lastName: [''],
    isFemale: [true],
  })

  register() {
    console.log("Form values:", this.form.value);
    this.passengerService.registerPassenger({ body: this.form.value })
      .subscribe(_ => console.log("Form posted to server."));
  }
}
