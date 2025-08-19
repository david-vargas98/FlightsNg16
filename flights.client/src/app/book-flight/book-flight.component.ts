import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { OnInit } from '@angular/core';
import { FlightService } from './../api/services/flight.service';
import { BookDto, FlightRm } from '../api/models';
import { AuthService } from '../auth/auth.service';
import { FormBuilder } from '@angular/forms';

@Component({
  selector: 'app-book-flight',
  templateUrl: './book-flight.component.html',
  styleUrls: ['./book-flight.component.css']
})
export class BookFlightComponent implements OnInit {
  constructor(
    private route: ActivatedRoute,
    private flightService: FlightService,
    private router: Router,
    private AuthService: AuthService,
    private formBuilder: FormBuilder
  ) { }

  flightId: string = 'not loaded';
  flight: FlightRm = {};

  form = this.formBuilder.group({
    number: [1] // default number of seats to book
  });

  ngOnInit(): void {

    if (!this.AuthService.currentUser)
      this.router.navigate(['/register-passenger']);

    this.route.paramMap.
      subscribe(p => this.findFlight(p.get("flightId")));
  }

  private findFlight = (flightId: string | null) => {
    this.flightId = flightId ?? 'not passed';

    this.flightService.findFlight({ id: this.flightId })
      .subscribe(flight => this.flight = flight, this.handleError);
  }

  // Making handleError part of the instance (keep router instance) to avoid -> TypeError: Cannot read properties of undefined (reading 'navigate')
  private handleError = (err: any) => {
    if (err.status == 404) {
      alert("Flight not found");
      this.router.navigate(['/search-flights']);
    }

    console.error("Response Error. Status:", err.status);
    console.error("Response Error. Status Text:", err.statusText);
    console.log(err);
  }

  book() {
    console.log(`Booking ${this.form.get('number')?.value} passengers for the flight: ${this.flight.id}`);

    const booking: BookDto = {
      flightId: this.flight.id,
      passengerEmail: this.AuthService.currentUser?.email,
      numberOfSeats: this.form.get('number')?.value
    }

    this.flightService.bookFlight({ body: booking })
      .subscribe(_ => this.router.navigate(['/my-booking']),
        this.handleError);
  }
}
