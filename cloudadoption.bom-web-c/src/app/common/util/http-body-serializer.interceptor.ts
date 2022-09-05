import { Injectable } from '@angular/core';
import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest, HttpResponse } from '@angular/common/http';
import { map, Observable } from 'rxjs';

@Injectable()
export class HttpBodySerializerInterceptor implements HttpInterceptor {

  constructor() {
  }

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    const body = this.serialize(this.clone(request.body));
    const httpRequest = request.clone({ body });
    return next.handle(httpRequest)
      .pipe(map((val: HttpEvent<any>) => {
        if (val instanceof HttpResponse) {
          const body = this.deserialize(this.clone(val.body));
          val = val.clone({ body });
        }
        return val;
      }));
  }

  serialize<T>(object: T): T {
    if (typeof object === 'string') {
      return object;
    }
    for (const key in object) {
      const newKey = this.fromCamelToSnakeCase(key);
      // @ts-ignore
      object[newKey] = this.serialize(object[key]);
      if (newKey !== key) {
        delete object[key];
      }
    }
    return object;
  }

  deserialize<T>(object: T): T {
    if (typeof object === 'string') {
      return object;
    }
    for (const key in object) {
      const newKey = this.fromSnakeToCamelCase(key);
      // @ts-ignore
      object[newKey] = this.deserialize(object[key]);
      if (newKey !== key) {
        delete object[key];
      }
    }
    return object;
  }

  fromCamelToSnakeCase(value: string): string {
    return value.replace(/[A-Z]/g, letter => `_${letter.toLowerCase()}`);
  }

  fromSnakeToCamelCase(value: string): string {
    return value.replace(/_[a-z]/g, letter => `${letter.substring(1).toLocaleUpperCase()}`);
  }

  clone<T>(object: T): T {
    return JSON.parse(JSON.stringify(object));
  }
}
