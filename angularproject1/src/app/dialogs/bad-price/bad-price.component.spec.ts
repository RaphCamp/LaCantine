import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BadPriceComponent } from './bad-price.component';

describe('BadPriceComponent', () => {
  let component: BadPriceComponent;
  let fixture: ComponentFixture<BadPriceComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ BadPriceComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(BadPriceComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
