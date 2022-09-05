import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BomDetailComponent } from './bom-detail.component';

describe('BomDetailComponent', () => {
  let component: BomDetailComponent;
  let fixture: ComponentFixture<BomDetailComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ BomDetailComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(BomDetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
