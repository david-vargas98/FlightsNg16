import { Component, OnInit } from '@angular/core';
import { FlightService } from '../api/services/flight.service';
import { FlightRm } from '../api/models';
import { FormBuilder } from '@angular/forms';
import { SearchFlight$Params } from '../api/fn/flight/search-flight';

@Component({
  selector: 'app-search-flights',
  templateUrl: './search-flights.component.html',
  styleUrls: ['./search-flights.component.css']
})
export class SearchFlightsComponent implements OnInit {

  searchResults: FlightRm[] = [];

  constructor(private flightService: FlightService, private formBuilder: FormBuilder) { }

  searchForm = this.formBuilder.group({
    from: [''],
    destination: [''],
    fromDate: [''],
    toDate: [''],
    numberOfPassengers: [1]
  });

  ngOnInit() {
    this.search();
  }

  search() {
    this.flightService.searchFlight(this.searchForm.value as SearchFlight$Params)
      .subscribe(response => this.searchResults = response,
        this.handleError)
  }

  private handleError(err: any) {
    console.error("Response Error. Status:", err.status);
    console.error("Response Error. Status Text:", err.statusText);
    console.log(err);
  }
}
