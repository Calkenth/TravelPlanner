import { Component, OnInit } from '@angular/core';
import { CitiesService } from '../services/cities.service';
import { ActivatedRoute, Router } from '@angular/router';
import { IRoute } from '../route';

@Component({
  selector: 'app-travels',
  templateUrl: './routes.component.html',
  styleUrls: ['./routes.component.css']
})
export class RoutesComponent implements OnInit {
public routes: IRoute[] = [];
public errorMsg: string;

  constructor(private citiesService: CitiesService,
              private router: Router,
              private route: ActivatedRoute) { }

  ngOnInit() {
  }

  Search(from: string, to: string) {
    this.errorMsg = '';
    this.citiesService.GetRoutes(from, to)
      .subscribe(data => this.routes = data,
        error => this.errorMsg = error);
    console.log('Click happend');
  }

}
