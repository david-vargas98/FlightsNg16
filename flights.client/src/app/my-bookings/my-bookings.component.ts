import { Component, OnInit } from '@angular/core';
import { BookingRm } from '../api/models';
import { BookingService } from './../api/services/booking.service';
import { AuthService } from './../auth/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-my-bookings',
  templateUrl: './my-bookings.component.html',
  styleUrls: ['./my-bookings.component.css']
})
export class MyBookingsComponent implements OnInit {
  
  bookings!: BookingRm[]; // The "!" tells the compiler that cannot be null or undefined

  constructor(private bookingService: BookingService, private authService: AuthService, private router: Router) { }

  ngOnInit() {

    if (!this.authService.currentUser?.email)
      this.router.navigate(['/register-passenger']);

    this.bookingService.listBooking({ email: this.authService.currentUser?.email ?? '' })
      .subscribe(result => this.bookings = result, this.handleError);

  }

  private handleError(err: any) {
    console.error("Response Error, Status:", err.status);
    console.error("Response Error, Status Text:", err.statusText);
    console.error(err);
  }

  cancel(booking: BookingRm) {

  }
}
