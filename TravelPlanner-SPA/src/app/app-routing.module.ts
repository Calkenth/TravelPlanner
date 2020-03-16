import { PageNotFoundComponent } from './page-not-found/page-not-found.component';
import { CitiesComponent } from './cities/cities.component';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { CityDetailComponent } from './city-detail/city-detail.component';
import { RoutesComponent } from './routes/routes.component';


const routes: Routes = [
  { path: '', redirectTo: '', pathMatch: 'full' },
  { path: 'cities', component: CitiesComponent },
  { path: 'cities/:name', component: CityDetailComponent },
  { path: 'routes', component: RoutesComponent },
  { path: '**', component: PageNotFoundComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
export const RoutingComponents = [
  CitiesComponent,
  CityDetailComponent,
  RoutesComponent,
  PageNotFoundComponent
];
