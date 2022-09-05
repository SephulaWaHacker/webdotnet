import { TestBed } from '@angular/core/testing';

import { BomClientService } from './bom-client.service';
import {HttpClientTestingModule} from '@angular/common/http/testing';

describe('BomClientService', () => {
  let service: BomClientService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
    });
    service = TestBed.inject(BomClientService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
