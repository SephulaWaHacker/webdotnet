import { Injectable } from '@angular/core';
import { Profile, User } from 'oidc-client';
import { TranslateService } from '@ngx-translate/core';
import { BehaviorSubject } from 'rxjs';


@Injectable({
  providedIn: 'root'
})
export class ProfileService {
  private _language: BehaviorSubject<string>;

  constructor(private translateSvc: TranslateService) {
    const browserLanguage = navigator.language.split('-')[0];
    this._language = new BehaviorSubject<string>(browserLanguage);
    this._language.subscribe(lang => this.translateSvc.use(lang));
  }

  set language(lang: string) {
    this._language.next(lang);
  }

  get language(): string {
    return this._language.value;
  }

  get user(): Promise<User | null> {
    return Promise.resolve(mockUser);
  }

  get profile(): Promise<Profile | null> {
    return this.user.then(user => user?.profile ?? null);
  }

  get area(): Promise<string | null> {
    return this.profile.then(profile => profile?.['area']);
  }
}

const mockUser: User = {
  access_token: '',
  expired: false,
  expires_at: 0,
  expires_in: 0,
  id_token: '',
  profile: {
    iss: '',
    sub: '',
    aud: '',
    exp: 0,
    iat: 0,
    given_name: 'Bauer',
    family_name: 'Martin',
    area: 'FG-Z-62',
    preferred_username: 'q123456'
  },
  scope: '',
  scopes: [],
  state: undefined,
  token_type: '',
  toStorageString(): string {
    return '';
  }
}
