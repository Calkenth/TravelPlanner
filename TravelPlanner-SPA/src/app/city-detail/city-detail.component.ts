import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router, ParamMap } from '@angular/router';

@Component({
  selector: 'app-city-detail',
  templateUrl: './city-detail.component.html',
  styleUrls: ['./city-detail.component.css']
})
export class CityDetailComponent implements OnInit {

  public cityID: string;
  constructor(private route: ActivatedRoute,
              private router: Router) { }

  ngOnInit() {
    // const id = parseInt(this.route.snapshot.paramMap.get('id'), 2);
    // this.cityID = id;
    this.route.paramMap.subscribe((params: ParamMap) => {
      // tslint:disable-next-line: radix
      const id = params.get('name');
      this.cityID = id;
    });
  }
/*
  ToNext() {
    this.router.navigate(['/cities', this.cityID + 1]);
    // this.router.navigate([this.cityID + 1], {relativeTo: this.route});
  }

  ToPrevious() {
    this.router.navigate(['/cities', this.cityID - 1]);
    // this.router.navigate([this.cityID - 1], {relativeTo: this.route});
  }*/

  GoToCities() {
    const currentID = this.cityID ? this.cityID : null;
    // this.router.navigate(['/cities', {id: currentID}]);
    this.router.navigate(['../', {name : currentID}], {relativeTo: this.route});
  }
}
