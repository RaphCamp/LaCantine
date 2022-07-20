import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SoldeComponent } from './solde.component';

describe('SoldeComponent', () => {
  let component: SoldeComponent;
  let fixture: ComponentFixture<SoldeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SoldeComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SoldeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
