import { MatTableDataSource } from '@angular/material/table';

export interface ITicket {
    fromWhere: string;
    toWhere: string;
    arrivalTime: string;
    departureTime: string;
}

export interface IBigTicket {
    guid: string;
    wholePrice: string;
    fromWhere: string;
    toWhere: string;
    arrivalTime: string;
    departureTime: string;
    tickets: ITicket[]; // | MatTableDataSource<ITicket>;
}
