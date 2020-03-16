import { CitiesService } from './../services/cities.service';
import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';

@Component({
  selector: 'app-cities',
  templateUrl: './cities.component.html',
  styleUrls: ['./cities.component.css']
})
export class CitiesComponent implements OnInit {
  public cities: string[] = [];
  public errorMsg: string;
  public lastOpenedCityID: string;

  constructor(private citiesService: CitiesService,
              private router: Router,
              private route: ActivatedRoute) { }

  ngOnInit() {
    this.citiesService.GetCities()
      .subscribe(data => this.cities = data,
        error => this.errorMsg = error);
    this.route.paramMap.subscribe((params: ParamMap) => {
      // tslint:disable-next-line: radix
      const id = params.get('name');
      this.lastOpenedCityID = id;
    });
  }

  OnClick(city) {
    // this.router.navigate(['/cities', city.id]);
    this.router.navigate([city], {relativeTo: this.route});
  }

  IsSelected(city) {
    return city === this.lastOpenedCityID;
  }
}
