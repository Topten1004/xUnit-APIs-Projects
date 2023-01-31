import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { ModalModule } from 'ngx-bootstrap/modal';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { TooltipModule } from 'ngx-bootstrap/tooltip';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';
import { ToastrModule } from 'ngx-toastr';
import { PaginatorModule } from 'primeng/paginator';

// import { PaginationModule } from 'ngx-bootstrap/pagination';

import { SalesService } from './_services/Sales.service';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { SalesManagementComponent } from './sales-management/sales-management.component';
import { NavComponent } from './nav/nav.component';

import { DatePipe } from '@angular/common';
import { MoneyEuroPipe } from './_helps/MoneyEuro.pipe';
import { DateTimeFormatPipe } from './_helps/DateTimeFormat.pipe';
import { PaginationCustomComponent } from './PaginationCustom/PaginationCustom.component';

@NgModule({
    declarations: [
        AppComponent,
        NavMenuComponent,
        HomeComponent,
        CounterComponent,
        FetchDataComponent,
        SalesManagementComponent,
        NavComponent,
        DateTimeFormatPipe,
        MoneyEuroPipe

    ],
    imports: [
        BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
        BrowserAnimationsModule,
        BsDropdownModule.forRoot(),
        BsDatepickerModule.forRoot(),
        TooltipModule.forRoot(),
        ModalModule.forRoot(),
        HttpClientModule,
        FormsModule,
        RouterModule.forRoot([
            { path: '', component: HomeComponent, pathMatch: 'full' },
            { path: 'counter', component: CounterComponent },
            { path: 'fetch-data', component: FetchDataComponent },
            { path: 'sales-management', component: SalesManagementComponent },
        ]),
        ReactiveFormsModule,
        ToastrModule.forRoot({
            timeOut: 3000,
            preventDuplicates: true,
            progressBar: true
        }),
        PaginatorModule
    ],
    providers: [
        SalesService,
        DatePipe
    ],
    bootstrap: [AppComponent]
})
export class AppModule { }
