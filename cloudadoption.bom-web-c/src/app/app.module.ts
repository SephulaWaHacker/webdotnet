import {APP_INITIALIZER, Injector, NgModule} from '@angular/core';
import {BrowserModule} from '@angular/platform-browser';
import {AppRoutingModule} from './app-routing.module';
import {AppComponent} from './app.component';
import {HomeComponent} from './common/home/home.component';
import {HeaderComponent} from './common/header/header.component';
import {TranslateLoader, TranslateModule, TranslateService} from '@ngx-translate/core';
import {HTTP_INTERCEPTORS, HttpClient, HttpClientModule} from '@angular/common/http';
import {TranslateHttpLoader} from '@ngx-translate/http-loader';
import {CommonModule, DatePipe, LOCATION_INITIALIZED} from '@angular/common';
import {FormsModule} from '@angular/forms';
import {NotFoundComponent} from './common/not-found/not-found.component';
import {BomListComponent} from './bom/bom-list/bom-list.component';
import {BomDetailComponent} from './bom/bom-detail/bom-detail.component';
import {PartFamilyComponent} from './bom/part-family/part-family.component';
import {BomOverviewComponent} from './bom/bom-overview/bom-overview.component';
import {HttpLoadingInterceptor} from './common/util/http-loading.interceptor';
import {HttpBodySerializerInterceptor} from './common/util/http-body-serializer.interceptor';
import { BmwModule } from './bmw.module';

export function HttpLoaderFactory(http: HttpClient): TranslateHttpLoader {
  return new TranslateHttpLoader(http);
}

export function ApplicationInitializerFactory(translate: TranslateService, injector: Injector) {
  return async () => {
    await injector.get(LOCATION_INITIALIZED, Promise.resolve(null));
    const defaultLang = navigator.language;
    translate.setDefaultLang(defaultLang);
    try {
      await translate.use(defaultLang).toPromise();
      console.log(`Successfully initialized ${(defaultLang)} language.`);
    } catch (err) {
      console.log(err);
    }
  };
}

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    HeaderComponent,
    NotFoundComponent,
    BomListComponent,
    BomDetailComponent,
    PartFamilyComponent,
    BomOverviewComponent,
  ],
  imports: [
    BmwModule,
    BrowserModule,
    CommonModule,
    FormsModule,
    HttpClientModule,
    TranslateModule.forRoot({
      loader: {
        provide: TranslateLoader,
        useFactory: HttpLoaderFactory,
        deps: [ HttpClient ]
      }
    }),
    AppRoutingModule,
  ],
  providers: [
    {
      provide: APP_INITIALIZER,
      useFactory: ApplicationInitializerFactory,
      deps: [TranslateService, Injector],
      multi: true
    },
    {provide: HTTP_INTERCEPTORS, useClass: HttpLoadingInterceptor, multi: true},
    {provide: HTTP_INTERCEPTORS, useClass: HttpBodySerializerInterceptor, multi: true},
    DatePipe,
  ],
  bootstrap: [AppComponent]
})
export class AppModule {
}
