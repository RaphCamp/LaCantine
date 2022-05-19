import { Component, OnInit, Input} from '@angular/core';

import { Plat } from '../models/plat';
import { ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';
import { PlatService } from '../Services/plat.service';
import {MatDialog, MatDialogConfig} from '@angular/material/dialog';
import { BadPriceComponent } from '../dialogs/bad-price/bad-price.component';


@Component({
  selector: 'app-plat',
  templateUrl: './plat.component.html',
  styleUrls: ['./plat.component.css','../../../node_modules/bootstrap/dist/css/bootstrap.css','../../../node_modules/bootswatch/dist/Litera/bootstrap.css']
})

export class PlatComponent implements OnInit {

  @Input() plat?: Plat;

  constructor(
    private route: ActivatedRoute,
    private platService: PlatService,
    private location: Location,
    public dialog: MatDialog
  ) { }

  ngOnInit(): void {
    this.getPlat();
  }

  getPlat(): void {
    const id = Number(this.route.snapshot.paramMap.get('id'));
    this.platService.getPlat(id).subscribe(plat => this.plat = plat);
  }

  goBack(): void {
    this.location.back();
  }

  save(): void {
    if (this.plat) {
      if (!Number(this.plat.prix)) {
        this.dialog.closeAll();
        window.scroll(0,0);
        this.openDialog();
      }
      else {
        this.platService.updatePlat(this.plat).subscribe(() => this.goBack());
      }
    }
  }

  delete(plat: Plat): void {
    this.platService.deletePlat(plat.id).subscribe(() => this.goBack());
  }

  //dialog
  config: MatDialogConfig = {
    hasBackdrop: false,
    position: {
        top: '',
        bottom: '',
        left: '',
        right: ''
    }
  }
        
  openDialog() {
    const dialogRef = this.dialog.open(BadPriceComponent, this.config);
  }

}
