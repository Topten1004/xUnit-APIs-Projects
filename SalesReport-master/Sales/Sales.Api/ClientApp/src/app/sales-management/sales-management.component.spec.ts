/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { sales-managementComponent } from './sales-management.component';

describe('sales-managementComponent', () => {
    let component: sales-managementComponent;
    let fixture: ComponentFixture<sales-managementComponent >;

    beforeEach(async(() => {
        TestBed.configureTestingModule({
            declarations: [sales - managementComponent]
        })
            .compileComponents();
    }));

    beforeEach(() => {
        fixture = TestBed.createComponent(sales - managementComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });

    it('should create', () => {
        expect(component).toBeTruthy();
    });
});
