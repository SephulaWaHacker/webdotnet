import { TestBed } from '@angular/core/testing';

import { ProfileService } from './profile.service';
import { TranslateModule, TranslateService } from '@ngx-translate/core';
import { HttpClientTestingModule } from '@angular/common/http/testing';

describe('ProfileService', () => {
  let service: ProfileService;
  let translator: TranslateService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [ TranslateModule.forRoot(), HttpClientTestingModule ]
    });
    service = TestBed.inject(ProfileService);
    translator = TestBed.inject(TranslateService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  describe('the language setter', () => {
    it('should sync with the translator', () => {
      service.language = 'de';
      expect(translator.currentLang).toBe('de');
      service.language = 'en';
      expect(translator.currentLang).toBe('en');
    });
  });
});
