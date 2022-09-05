import {TestBed} from '@angular/core/testing';

import {PartFamilyClientService} from './part-family-client.service';
import {HttpClientTestingModule} from '@angular/common/http/testing';

describe('PartFamilyClientService', () => {
  let service: PartFamilyClientService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
    });
    service = TestBed.inject(PartFamilyClientService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
