import { Sales } from './Sales';

export interface Page {
    content: Array<Sales>;
    totalPages: number;
    totalElements: number;
    last: boolean;
    size: number;
    number: number;
    sort?: any;
    numberOfElements: number;
    first: boolean;
}
