import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PartFamilyComponent } from './part-family.component';

describe('PartFamilyComponent', () => {
  let component: PartFamilyComponent;
  let fixture: ComponentFixture<PartFamilyComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PartFamilyComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PartFamilyComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
