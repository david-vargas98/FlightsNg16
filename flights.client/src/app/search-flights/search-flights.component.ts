import { Component } from '@angular/core';
import { FlightService } from '../api/services/flight.service';
import { FlightRm } from '../api/models';

@Component({
  selector: 'app-search-flights',
  templateUrl: './search-flights.component.html',
  styleUrls: ['./search-flights.component.css']
})
export class SearchFlightsComponent {

  searchResults: FlightRm[] = [];

  constructor(private flightService: FlightService) { }

  search() {
    this.flightService.searchFlight({})
      .subscribe(response => this.searchResults = response,
        this.handleError)
  }

  private handleError(err: any) {
    console.error("Response Error. Status:", err.status);
    console.error("Response Error. Status Text:", err.statusText);
    console.log(err);
  }
}
