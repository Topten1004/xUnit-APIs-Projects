import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Page } from '../_models/Page';
import { Sales } from '../_models/Sales';

@Injectable({
    providedIn: 'root'
})
export class SalesService {
    baseUrl = 'http://localhost:49210/api/Sales';
    private headers: Headers;
    constructor(
        private http: HttpClient) {
        this.headers = new Headers();
        this.headers.append('Content-Type', 'application/json');
    }

    getSaleById(id: string): Observable<Sales> {
        return this.http.get<Sales>(`${this.baseUrl}/GetSaleById?id=${id}`);
    }

    GetSalesCount(country: string, beginDate: Date, endDate: Date): Observable<number> {
        let urlParameters;

        if (beginDate == null && endDate == null) {
            urlParameters = (`country=${country}`);
        } else {
            urlParameters = (`beginDate=${beginDate}&endDate=${endDate}&country=${country}`);
        }
        return this.http.get<number>(`${this.baseUrl}/CountFiltered?${urlParameters}`);
    }

    GetSalesByRangeDateCountryPage(country: string, beginDate: Date, endDate: Date, page, size): Observable<Sales[]> {
        let urlParameters;
        let pageParameters = '';

        pageParameters = (`page=${page}&size=${size}&`);

        if (beginDate == null && endDate == null) {
            urlParameters = (`country=${country}`);
        } else {
            urlParameters = (`beginDate=${beginDate}&endDate=${endDate}&country=${country}`);
        }

        return this.http.get<Sales[]>(`${this.baseUrl}/GetSalesByRangeDateAndCountry?${pageParameters}${urlParameters}`);
    }

    getListOfCountriesDistinct(): Observable<String[]> {
        return this.http.get<String[]>(`${this.baseUrl}/GetAllCountries`)
    }

    GetSalesByRangeDateAndCountry(country: string, beginDate: Date, endDate: Date): Observable<Sales[]> {
        let urlParameters;
        if (beginDate == null && endDate == null) {
            urlParameters = (`country=${country}`);
        } else {
            urlParameters = (`beginDate=${beginDate}&endDate=${endDate}&country=${country}`);
        }

        return this.http.get<Sales[]>(`${this.baseUrl}/GetSalesByRangeDateAndCountry?${urlParameters}`);
    }

    PutSale(sale: Sales) {
        return this.http.put<Sales>(`${this.baseUrl}/UpdateSale?id=${sale.id}`, sale);
    }

    DeleteSale(id: String) {
        return this.http.delete(`${this.baseUrl}/DeleteSale/${id}`);
    }

    UploadFile(formData: FormData) {
        return this.http.post(`${this.baseUrl}/UploadFile`, formData, { reportProgress: true, observe: 'events' });
    }
}
