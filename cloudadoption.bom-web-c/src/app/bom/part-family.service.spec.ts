import { TestBed } from '@angular/core/testing';

import { PartFamilyService } from './part-family.service';

describe('PartFamilyService', () => {
  let service: PartFamilyService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(PartFamilyService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
