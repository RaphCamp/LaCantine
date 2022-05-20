import { Component, OnInit, Input } from '@angular/core';

import { Menu } from '../models/menu';
import { ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';
import { PlatService } from '../Services/plat.service';
import { MenuService } from '../Services/menu.service';
import { Plat } from '../models/plat';



@Component({
  selector: 'app-menu',
  templateUrl: './menu.component.html',
  styleUrls: ['./menu.component.css','../../../node_modules/bootstrap/dist/css/bootstrap.css','../../../node_modules/bootswatch/dist/Litera/bootstrap.css']
})
export class MenuComponent implements OnInit {

  @Input() menu?: Menu;

  constructor(
    private route: ActivatedRoute,
    private menuService: MenuService,
    private platService: PlatService,
    private location: Location
  ) { }

  ngOnInit(): void {
    this.getMenu();
    this.getAllPlat();
  }

  //Concernant le menu:
  getMenu(): void {
    const id = Number(this.route.snapshot.paramMap.get('id'));
    this.menuService.getMenu(id).subscribe(menu => this.menu = menu);
  }

  goBack(): void {
    this.location.back();
  }

  save(): void {
    if (this.menu) {
      this.menuService.updateMenu(this.menu).subscribe(() => this.goBack());
    }
  }

  delete(menu: Menu): void {
    this.menuService.deleteMenu(menu.id).subscribe(() => this.goBack());
  }

  //Concernant les plats du menu:

  //Tous les plats de la bdd
  allPlats: Plat[] = [];

  getAllPlat() {
    this.platService.getPlats().subscribe(plats => this.allPlats = plats);
  }

  //changer les plats du menu
  newPlat! : Plat;
}