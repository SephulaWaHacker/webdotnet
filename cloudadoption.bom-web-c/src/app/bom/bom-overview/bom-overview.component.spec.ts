import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BomOverviewComponent } from './bom-overview.component';

describe('BomOverviewComponent', () => {
  let component: BomOverviewComponent;
  let fixture: ComponentFixture<BomOverviewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ BomOverviewComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(BomOverviewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
