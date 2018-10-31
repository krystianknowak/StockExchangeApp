export interface Stock {
  id: number;
  companyCode: string;
  ownedUnits: number;
  value?: number;
}

export interface User {
  id: number;
  username: string;
  firstName: string;
  lastName: string;
  availableMoney: number;
  stocks?: Stock[];
}
