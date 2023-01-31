import { Component, EventEmitter, OnInit, Output, TemplateRef } from '@angular/core';
import { DatePipe } from '@angular/common';
import { Sales } from '../_models/Sales';
import { SalesService } from '../_services/Sales.service';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { defineLocale, BsLocaleService, enGbLocale } from 'ngx-bootstrap';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { HttpEventType } from '@angular/common/http';
import { Page } from '../_models/Page';
import { PaginatorModule } from 'primeng/paginator';

defineLocale('en-gb', enGbLocale);
@Component({
    selector: 'app-sales-management',
    templateUrl: './sales-management.component.html',
    styleUrls: ['./sales-management.component.scss']
})
export class SalesManagementComponent implements OnInit {
    salesFiltered: Sales[];
    sales: Sales[];
    sale: Sales;
    countries: String[];
    listFiltered = '';
    registerForm: FormGroup;
    rangeDate: string[];
    date: { year: number, month: number };
    bodyDeleteSale = '';
    progress: number;
    message: string;
    @Output() public onUploadFinished = new EventEmitter();
    page: Page;
    size = 10;
    numberOfPages: number;

    constructor(
        private salesService: SalesService
        , private modalService: BsModalService
        , private fb: FormBuilder
        , private localeSevice: BsLocaleService
        , private datepipe: DatePipe
    ) {
        this.localeSevice.use('en-gb');
    }

    ngOnInit() {
        this.filterSales('', null, 0, 10);
        this.getListOfCountriesDistinct();
        this.validation();
        this.getSalesCount('', null, null);
    }

    searchSales(countrySelected: string, rangeDate: string[], page: number, size: number): any {
        let beginDate = null;
        let endDate = null;
        if (rangeDate != null) {
            beginDate = this.datepipe.transform(rangeDate[0], 'yyyy-MM-dd');
            endDate = this.datepipe.transform(rangeDate[1], 'yyyy-MM-dd');
        }
        this.filterSales(countrySelected, rangeDate, page, size);
        this.numberOfPages = this.getSalesCount(countrySelected, beginDate, endDate);
    }
    filterSales(countrySelected: string, rangeDate: string[], page: number, size: number): any {
        let beginDate = null;
        let endDate = null;
        if (rangeDate != null) {
            beginDate = this.datepipe.transform(rangeDate[0], 'yyyy-MM-dd');
            endDate = this.datepipe.transform(rangeDate[1], 'yyyy-MM-dd');
        }
        this.getSalesByRangeDateCountryPage(countrySelected, beginDate, endDate, page, size);
    }

    getSalesByRangeDateCountryPage(countryPar: string, beginDate: Date, endDate: Date, page, size) {
        this.salesService.GetSalesByRangeDateCountryPage(countryPar, beginDate, endDate, page, size).subscribe(
            (_sale: Sales[]) => {
                this.sales = _sale;
                this.salesFiltered = _sale;
            }, error => {
                console.log(error);
            }
        );
    }

    getSalesCount(countryPar: string, beginDate: Date, endDate: Date): any {
        this.salesService.GetSalesCount(countryPar, beginDate, endDate).subscribe(
            (_countSales: number) => {
                this.numberOfPages = _countSales;
            }, error => {
                console.log(error);
            }
        );
    }
    changePage(event) {
        this.filterSales(this.listFiltered, this.rangeDate, event.page, 10);
    }

    editSale(sale: Sales, template: any) {
        this.openModal(template);
        this.sale = Object.assign({}, sale);
        this.registerForm.patchValue(this.sale);
    }

    openModal(template: any) {
        this.registerForm.reset();
        template.show();
    }

    saveEdition(template: any) {
        if (this.registerForm.valid) {
            this.sale = Object.assign({ id: this.sale.id }, this.registerForm.value);
            this.salesService.PutSale(this.sale).subscribe(
                () => {
                    template.hide();
                    this.filterSales(this.listFiltered, this.rangeDate, 0, 10);
                    alert('Sale Edited');
                }, error => {
                    alert(`Ops, there is something wrong: ${error}`);
                }
            );
        }
    }

    deleteSale(sale: Sales, template: any) {
        this.openModal(template);
        this.sale = sale;
        this.bodyDeleteSale = `Are you sure you wanna delete this sale?`;
    }

    confirmDelete(template: any) {
        this.salesService.DeleteSale(this.sale.id).subscribe(
            () => {
                template.hide();
                this.filterSales(this.listFiltered, this.rangeDate, 0, 10);
                alert('Deleted');
            }, error => {
                alert(`Ops, there is something wrong: ${error}`);
                console.log(error);
            }
        );
    }

    getListOfCountriesDistinct() {
        this.salesService.getListOfCountriesDistinct().subscribe(
            (_countries: String[]) => {
                this.countries = _countries;
            }, error => {
                console.log(error);
            }
        );
    }

    getSaleById(id: string) {
        this.salesService.getSaleById(id).subscribe(
            (_sale: Sales) => {
                this.sale = _sale;
            }, error => {
                console.log(error);
            }
        );
    }

    validation() {
        this.registerForm = this.fb.group({
            country: ['', Validators.required],
            region: ['', Validators.required],
            itemType: ['', Validators.required],
            salesChannel: ['', Validators.required],
            orderPriority: ['', Validators.required],
            shipDate: ['', Validators.required],
            orderDate: ['', Validators.required],
            unitsSold: ['', Validators.required],
            unitPrice: ['', Validators.required],
            unitCost: ['', Validators.required],
            totalRevenue: ['', Validators.required],
            totalCost: ['', Validators.required],
            totalProfit: ['', Validators.required]
        });
    }
    // pageProducts(page, size){
    //   this.service.getProductPage(page, size).subscribe(res => {
    //     this.page = res
    //     this.products = this.page.content;
    //   });
    // }

    public uploadFile = (files) => {
        if (files.length === 0) {
            return;
        }
        const fileToUpload = <File>files[0];
        const formData = new FormData();
        formData.append('file', fileToUpload, fileToUpload.name);
        this.salesService.UploadFile(formData).subscribe(event => {
            if (event.type === HttpEventType.UploadProgress) {
                this.progress = Math.round(100 * event.loaded / event.total);
            } else if (event.type === HttpEventType.Response) {
                this.filterSales(this.listFiltered, this.rangeDate, 0, 10);
            }
        });
    }
}
