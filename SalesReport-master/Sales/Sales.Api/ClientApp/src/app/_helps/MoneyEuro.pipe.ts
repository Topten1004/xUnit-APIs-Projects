import { CurrencyPipe } from '@angular/common';
import { Pipe, PipeTransform } from '@angular/core';
import { Constants } from '../util/Constants';

@Pipe({
    name: 'MoneyEuroPipe'
})
export class MoneyEuroPipe extends CurrencyPipe implements PipeTransform {
    transform(value: any, args?: any): any {
        return super.transform(value, Constants.EURO);
    }
}
