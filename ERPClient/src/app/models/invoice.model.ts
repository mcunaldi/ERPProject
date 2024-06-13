import { InvoiceDetailModel } from "./Invoice-detail.model";
import { CustomerModel } from "./customer.model";

export class InvoiceModel{
    id: string = "";
    invoiceNumber: string = "";
    date: string = "";
    customerId: string = "";
    customer: CustomerModel = new CustomerModel();
    type: InvoiceTypeEnum = new InvoiceTypeEnum();
    typeValue: number = 1;
    details: InvoiceDetailModel[] = [];
    orderId?: string | null = null;
}

export class InvoiceTypeEnum{
    value: number = 1;
    name: string = "";
}