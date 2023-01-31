/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { SalesService } from './Sales.service';

describe('Service: Sales', () => {
    beforeEach(() => {
        TestBed.configureTestingModule({
            providers: [SalesService]
        });
    });

    it('should ...', inject([SalesService], (service: SalesService) => {
        expect(service).toBeTruthy();
    }));
});
