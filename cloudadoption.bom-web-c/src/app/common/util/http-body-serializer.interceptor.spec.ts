import { TestBed } from '@angular/core/testing';

import { HttpBodySerializerInterceptor } from './http-body-serializer.interceptor';

describe('HttpBodySerializerInterceptor', () => {
  let interceptor: HttpBodySerializerInterceptor;
  beforeEach(() => TestBed.configureTestingModule({
    providers: [
      HttpBodySerializerInterceptor
    ]
  }));

  beforeEach(() => {
    interceptor = TestBed.inject(HttpBodySerializerInterceptor);
  });

  it('should be created', () => {
    interceptor = TestBed.inject(HttpBodySerializerInterceptor);
    expect(interceptor).toBeTruthy();
  });

  describe('the serialize method', () => {
    it('should convert keys to snake case', () => {
      const object = {
        name: 'as',
        iAmOld: true
      }
      const result = interceptor.serialize(object);
      expect(JSON.stringify(result)).toEqual('{"name":"as","i_am_old":true}');
    });
    it('should serialize the array', () => {
      const object = {
        name: 'as',
        iAmOld: true
      };
      const objects = [{...object}, object]
      const result = interceptor.serialize(objects);
      expect(JSON.stringify(result)).toEqual('[{"name":"as","i_am_old":true},{"name":"as","i_am_old":true}]');
    });
  });

  describe('the deserialize method', () => {
    it('should convert keys to camel case', () => {
      const object = {
        name: 'as',
        i_am_old: true
      }
      const objects = [{...object}, object]
      const result = interceptor.deserialize(objects);
      expect(JSON.stringify(result)).toEqual('[{"name":"as","iAmOld":true},{"name":"as","iAmOld":true}]');
    });
    it('should deserialize the array', () => {
      const object = [{
        name: 'as',
        i_am_old: true,
        i_am_old_list: [ {
          i_am_old: true,
        }]
      }]
      const result = interceptor.deserialize(object);
      expect(JSON.stringify(result)).toEqual('[{"name":"as","iAmOld":true,"iAmOldList":[{"iAmOld":true}]}]');
    });
  });

  describe('the fromCamelToSnakeCase method', () => {
    it('should return "name"', () => {
      const value = interceptor.fromCamelToSnakeCase('name');
      expect(value).toEqual('name');
    });
    it('should return "name_surname"', () => {
      const value = interceptor.fromCamelToSnakeCase('nameSurname');
      expect(value).toEqual('name_surname');
    });
  });

  describe('the fromSnakeToCamelCase method', () => {
    it('should return "name"', () => {
      const value = interceptor.fromSnakeToCamelCase('name');
      expect(value).toEqual('name');
    });
    it('should return "nameSurname"', () => {
      const value = interceptor.fromSnakeToCamelCase('name_surname');
      expect(value).toEqual('nameSurname');
    });
  });
});
