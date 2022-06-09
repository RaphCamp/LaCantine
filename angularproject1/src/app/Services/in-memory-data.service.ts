import { Injectable } from '@angular/core';
import { InMemoryDbService } from 'angular-in-memory-web-api';
import { Plat } from '../models/plat.model';
import { Menu } from '../models/menu';

@Injectable({
  providedIn: 'root',
})
export class InMemoryDataService implements InMemoryDbService {
  createDb() {
    const plats: Plat[] = [
      { ID: 0, Libelle: ' ', Description: ' ', Image: " ", Prix: 0 },
      { ID: 1, Libelle: 'Frites', Description: 'seulement avec burger', Image: "../../assets/rizotto.jpg", Prix: 0},
      { ID: 11, Libelle: 'Rizotto', Description: 'très bon très bon', Image: "../../assets/rizotto.jpg", Prix: 16},
      { ID: 12, Libelle: 'Poulet Roti', Description: 'très bon très bon', Image: "../../assets/rizotto.jpg", Prix: 18},
      { ID: 13, Libelle: 'Fondant Chocolat', Description: 'très bon très bon', Image: "../../assets/rizotto.jpg", Prix: 13},
      { ID: 14, Libelle: 'Nems', Description: 'très bon très bon', Image: "../../assets/rizotto.jpg", Prix: 13},
      { ID: 15, Libelle: 'Tomates provencales', Description: 'très bon très bon', Image: "../../assets/rizotto.jpg", Prix: 12},
      { ID: 16, Libelle: 'Burger', Description: 'très bon très bon', Image: "../../assets/rizotto.jpg", Prix: 11},
      { ID: 17, Libelle: 'Tarte au Citron', Description: 'très bon très bon', Image: "../../assets/rizotto.jpg", Prix: 13},
      { ID: 18, Libelle: 'Truffade', Description: 'très bon très bon', Image: "../../assets/rizotto.jpg", Prix: 50},
      { ID: 19, Libelle: 'Salade', Description: 'très bon très bon', Image: "../../assets/rizotto.jpg", Prix: 12},
      { ID: 20, Libelle: 'Escargots qui crient', Description: 'très bon très bon', Image: "../../assets/rizotto.jpg", Prix: 20},
    ];
    const menus: Menu[] = [
      { id: 11, name: 'Ptite Faim Salée',plats : [plats[5],plats[2]], prix: 16},
      { id: 12, name: 'Ptite Faim Sucrée',plats : [plats[3],plats[4]], prix: 16},
      { id: 13, name: 'Grosse Fringale',plats : [plats[10],plats[9],plats[8]], prix: 20},
      { id: 14, name: 'Burger',plats : [plats[7],plats[1]], prix: 13},
    ];
    return { plats,menus };
  }

  // Overrides the genId method to ensure that a plat always has an id.
  // If the plates array is empty,
  // the method below returns the initial number (11).
  // if the plates array is not empty, the method below returns the highest
  // plat id + 1.
  genId(pieces: Plat[]): number {
    return pieces.length > 0 ? Math.max(...pieces.map(piece => piece.ID)) + 1 : 11;
  }
}
