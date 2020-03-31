import { Component, OnInit } from '@angular/core';
import { CitiesService } from '../services/cities.service';
import { ActivatedRoute, Router } from '@angular/router';
import { IRoute } from '../route';
import { MatTableDataSource } from '@angular/material/table';

@Component({
  selector: 'app-travels',
  templateUrl: './routes.component.html',
  styleUrls: ['./routes.component.css']
})
export class RoutesComponent implements OnInit {
columns = [
  { columnDef: 'fromWhere', header: 'From:',    cell: (route: IRoute) => `${route.fromWhere}` },
  { columnDef: 'departureTime',   header: 'Departure hours:', cell: (route: IRoute) => `${route.departureTime}` },
  { columnDef: 'toWhere',     header: 'To:',   cell: (route: IRoute) => `${route.toWhere}` },
  { columnDef: 'arrivalTime',   header: 'Arrival hours:', cell: (route: IRoute) => `${route.arrivalTime}` },
  { columnDef: 'price',   header: 'Ticket price:', cell: (route: IRoute) => `${route.price}` }
];
displayedColumns: string[] = this.columns.map(c => c.columnDef);
public routes: IRoute[] = [];
dataSource = new MatTableDataSource(this.routes);
public errorMsg: string;
public showRoutes = false;

  constructor(private citiesService: CitiesService,
              private router: Router,
              private route: ActivatedRoute) { }

  ngOnInit() {
  }

  ShowRoutes() {
    this.showRoutes = true;
  }

  Search(from: string, to: string) {
    this.errorMsg = '';
    this.citiesService.GetRoutes(from, to)
      .subscribe(data => {
      this.routes = data,
      this.dataSource.data = this.routes;
    },
        error => this.errorMsg = error);
    this.ShowRoutes();
    console.log('Click happend');
  }

}
