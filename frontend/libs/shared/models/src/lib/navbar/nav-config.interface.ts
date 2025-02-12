import { NavTypes } from './nav-types-items.type';

export interface NavConfig {
  type: NavTypes;
  name: string;
  routerLink: string;
  image?: {
    src: string;
    alt: string;
    width: number;
    height: number;
  };
}
