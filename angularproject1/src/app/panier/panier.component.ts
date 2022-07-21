import { Component, OnInit } from '@angular/core';
import { PlatService } from '../Services/plat.service';
import { Plat } from '../models/plat';

@Component({
  selector: 'app-panier',
  templateUrl: './panier.component.html',
  styleUrls: ['./panier.component.css']
})
export class PanierComponent implements OnInit {
  /*@Input() plat?: Plat;*/

  constructor() { }

  ngOnInit(): void
  {
    /*this.getPlat();*/
  }

}
