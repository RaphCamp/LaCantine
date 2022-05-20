import { Plat } from "./plat";

export interface Menu {
    id: number;
    name: string;
    plats: Plat[];
    prix: number;
  }