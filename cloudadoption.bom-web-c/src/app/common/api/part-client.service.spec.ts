import { TestBed } from '@angular/core/testing';

import { PartClientService } from './part-client.service';

describe('PartClientService', () => {
  let service: PartClientService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(PartClientService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
