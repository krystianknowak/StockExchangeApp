import { Stocks } from './stocks';
export interface UserRegister {
  username: string;
  password: string;
  firstname: string;
  lastname: string;
  availablemoney: number;
  stocks: Stocks[];
}
