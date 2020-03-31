import { MatTableDataSource } from '@angular/material/table';
import { IBigTicket, ITicket } from './../bigTicket';
import { Component, OnInit } from '@angular/core';
import { CitiesService } from '../services/cities.service';
import { Router, ActivatedRoute } from '@angular/router';
import { trigger, state, style, transition, animate } from '@angular/animations';

@Component({
  selector: 'app-tickets',
  templateUrl: './tickets.component.html',
  styleUrls: ['./tickets.component.css'],
  animations: [
    trigger('detailExpand', [
      state('collapsed', style({ height: '0px', minHeight: '0' })),
      state('expanded', style({ height: '*' })),
      transition('expanded <=> collapsed', animate('225ms cubic-bezier(0.4, 0.0, 0.2, 1)')),
    ]),
  ],
})
export class TicketsComponent implements OnInit {
  columns = [
    { columnDef: 'fromWhere', header: 'From:',    cell: (ticket: IBigTicket) => `${ticket.fromWhere}` },
    { columnDef: 'departureTime', header: 'Departure time:', cell: (ticket: IBigTicket) => `${ticket.departureTime}` },
    { columnDef: 'toWhere', header: 'To:',   cell: (ticket: IBigTicket) => `${ticket.toWhere}` },
    { columnDef: 'arrivalTime', header: 'Arrival time:', cell: (ticket: IBigTicket) => `${ticket.arrivalTime}` },
    { columnDef: 'wholePrice', header: 'Ticket price:', cell: (ticket: IBigTicket) => `${ticket.wholePrice}` }
  ];
  innerColumns = [
    { columnDef: 'fromWhere', header: 'From:', cell: (route: ITicket) => `${route.fromWhere}` },
    { columnDef: 'departureTime', header: 'Departure:', cell: (route: ITicket) => `${route.departureTime}` },
    { columnDef: 'toWhere', header: 'To:', cell: (route: ITicket) => `${route.toWhere}` },
    { columnDef: 'arrivalTime', header: 'Arrival:', cell: (route: ITicket) => `${route.arrivalTime}` }
  ];
  displayedColumns: string[] = this.columns.map(c => c.columnDef);
  innerDisplayedColumns: string[] = this.innerColumns.map(c => c.columnDef);
  public tickets: IBigTicket[] = [];
  dataSource = new MatTableDataSource(this.tickets);
  expandedElement: ITicket;
  public price: string;
  public errorMsg: string;
  public showConnections = false;

  constructor(private citiesService: CitiesService,
              private router: Router,
              private route: ActivatedRoute) { }

  ngOnInit() {
  }

  ShowConnections() {
    this.showConnections = true;
  }

  SearchTicket(fromCity: string, toCity: string) {
    this.errorMsg = '';
    this.citiesService.GetTicket(fromCity, toCity)
    .subscribe(data => {
      this.tickets = data,
      this.dataSource.data = this.tickets;
    },
    error => this.errorMsg = error);
    // this.price = this.tickets[0].wholePrice;
    console.log('Ticket query send.');
    this.ShowConnections();
  }
}
