import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { RouterModule } from '@angular/router';
import { SearchFlightsComponent } from './search-flights/search-flights.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { BookFlightComponent } from './book-flight/book-flight.component';
import { RegisterPassengerComponent } from './register-passenger/register-passenger.component';

@NgModule({
  declarations: [
    AppComponent,
    SearchFlightsComponent,
    NavMenuComponent,
    BookFlightComponent,
    RegisterPassengerComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    RouterModule.forRoot([
      { path: '', component: SearchFlightsComponent, pathMatch: 'full' },
      { path: 'search-flights', component: SearchFlightsComponent },
      { path: 'book-flight/:flightId', component: BookFlightComponent },
      { path: 'register-passenger', component: RegisterPassengerComponent }



    ])
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
