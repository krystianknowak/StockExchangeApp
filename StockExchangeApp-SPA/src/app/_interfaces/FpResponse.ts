export interface Item {
  name: string;
  code: string;
  unit: number;
  price: number;
}

export interface FpResponse {
  publicationDate: Date;
  items: Item[];
}
