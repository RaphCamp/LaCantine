import { Plat } from "./plat.model";

export interface Menu {
    id: number;
    name: string;
    plats: Plat[];
    prix: number;
  }