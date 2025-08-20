import { Component, OnInit } from '@angular/core';
import { BookingRm, BookDto } from '../api/models';
import { BookingService } from './../api/services/booking.service';
import { AuthService } from './../auth/auth.service';

@Component({
  selector: 'app-my-bookings',
  templateUrl: './my-bookings.component.html',
  styleUrls: ['./my-bookings.component.css']
})
export class MyBookingsComponent implements OnInit {
  
  bookings!: BookingRm[]; // The "!" tells the compiler that cannot be null or undefined

  constructor(private bookingService: BookingService, private authService: AuthService) { }

  ngOnInit() {
    this.bookingService.listBooking({ email: this.authService.currentUser?.email ?? '' })
      .subscribe(result => this.bookings = result, this.handleError);

  }

  private handleError(err: any) {
    console.error("Response Error, Status:", err.status);
    console.error("Response Error, Status Text:", err.statusText);
    console.error(err);
  }

  cancel(booking: BookingRm) {

    const dto: BookDto = {
      flightId: booking.flightId,
      numberOfSeats: booking.numberOfBookedSeats,
      passengerEmail: booking.passengerEmail
    };

    this.bookingService.cancelBooking({ body: dto })
      .subscribe(_ => this.bookings = this.bookings.filter(b => b != booking), this.handleError);

  }
}
