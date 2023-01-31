export interface Sales {
    id: string;
    region: string;
    country: string;
    itemType: string;
    salesChannel: string;
    orderPriority: string;
    orderDate: Date;
    orderId: string;
    shipDate: Date;
    unitsSold: number;
    unitPrice: number;
    unitCost: number;
    totalRevenue: number;
    totalCost: number;
    totalProfit: number;
}
