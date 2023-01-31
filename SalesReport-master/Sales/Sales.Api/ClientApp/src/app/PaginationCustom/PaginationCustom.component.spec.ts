/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { PaginationCustomComponent } from './PaginationCustom.component';

describe('PaginationCustomComponent', () => {
    let component: PaginationCustomComponent;
    let fixture: ComponentFixture<PaginationCustomComponent>;

    beforeEach(async(() => {
        TestBed.configureTestingModule({
            declarations: [PaginationCustomComponent]
        })
            .compileComponents();
    }));

    beforeEach(() => {
        fixture = TestBed.createComponent(PaginationCustomComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });

    it('should create', () => {
        expect(component).toBeTruthy();
    });
});
