import { TestBed } from '@angular/core/testing';

import { BomService } from './bom.service';
import {BomClientService} from '../common/api/v1/bom-client.service';

describe('BomService', () => {
  let service: BomService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [
        {
          provide: BomClientService, useValue: {
            getBoms: jest.fn()
          }
        }
      ]
    });
    service = TestBed.inject(BomService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
