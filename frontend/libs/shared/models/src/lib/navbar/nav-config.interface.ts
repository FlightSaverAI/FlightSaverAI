import { NavTypes } from './nav-types-items.type';

export interface NavConfig {
  type: NavTypes;
  name: string;
  routerLink: string;
  image?: {
    alt: string;
    width: number;
    height: number;
  };
}
