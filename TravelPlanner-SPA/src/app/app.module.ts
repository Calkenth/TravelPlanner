import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { AppRoutingModule, RoutingComponents } from './app-routing.module';

import { AppComponent } from './app.component';
import { CitiesService } from './services/cities.service';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { Materials } from './materials';

@NgModule({
   declarations: [
      AppComponent,
      RoutingComponents
   ],
   imports: [
      BrowserModule,
      AppRoutingModule,
      HttpClientModule,
      BrowserAnimationsModule,
      Materials
   ],
   providers: [
      CitiesService
   ],
   bootstrap: [
      AppComponent
   ]
})
export class AppModule { }
