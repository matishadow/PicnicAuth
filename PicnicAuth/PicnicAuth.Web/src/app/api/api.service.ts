import { Injectable } from "@angular/core";
import {
  Http,
  Response,
  Headers,
  Request,
  RequestOptions,
  RequestMethod
} from "@angular/http";
import { environment } from "../../environments/environment";
import { Observable } from "rxjs/Observable";
import "rxjs/add/operator/map";
import "rxjs/add/operator/catch";

@Injectable()
export class ApiService {
  private sharedHeaders = new Headers({
    'Content-Type': "application/json",
    'Accept': "application/json"
  });

  constructor(
    private readonly http: Http,
  ) { }

  post(endpoint: string, parameters?: any, headers?: Headers) {
    return this.sendRequest(RequestMethod.Post, endpoint, headers, parameters);
  }

  get(endpoint: string, headers?: Headers) {
    return this.sendRequest(RequestMethod.Get, endpoint, headers);
  }

  delete(endpoint: string, headers?: Headers) {
    return this.sendRequest(RequestMethod.Delete, endpoint, headers);
  }

  put(endpoint: string, parameters?: any, headers?: Headers) {
    return this.sendRequest(RequestMethod.Put, endpoint, headers, parameters);
  }

  sendRequest(
    method: RequestMethod,
    endpoint: string,
    headers?: Headers,
    parameters?: any
  ) {
    const mergedHeaders = this.mergeHeaders(
      this.sharedHeaders,
      headers
    );
    const options = new RequestOptions({
      headers: mergedHeaders,
      method: method,
      body: parameters,
      url: [environment.apiUrl, endpoint].join("/")
    });
    return this.http.request(new Request(options))
      .map(response => {
        let body: any = {};
        try {
          body = response.json();
        } catch (e) { }
        body = this.toCamel(body);
        return body;
      }).catch(error => {
        if (error.status === 401) {
          // this.userService.unauthorized();
        }
        let message: string = "Error";
        try {
          message = error;
        } catch (e) { }
        return Observable.throw(message);
      });
  }

  private mergeHeaders(...headers: Headers[]): Headers {
    headers = headers.filter(item => item != null);
    const mergedHeaders = {};
    Object.assign(
      mergedHeaders,
      ...(headers.map(item => item.toJSON()))
    );
    return new Headers(mergedHeaders);
  }

  private toCamel(object: any) {
    let newObject, originalKey, newKey, objectValue;
    if (object instanceof Array) {
      newObject = [];
      for (let originalKey in object) {
        objectValue = object[originalKey];
        if (typeof objectValue === "object") {
          objectValue = this.toCamel(objectValue);
        }
        newObject.push(objectValue);
      }
    } else {
      newObject = {};
      for (let originalKey in object) {
        newKey = (originalKey.charAt(0).toLowerCase() + originalKey.slice(1) || originalKey).toString();
        objectValue = object[originalKey];
        if (typeof objectValue === "object") {
          objectValue = this.toCamel(objectValue);
        }
        newObject[newKey] = objectValue;
      }
    }
    return newObject;
  }

  getUrl(endpoint: string) {
    return [environment.apiUrl, endpoint].join("/");
  }

  getHeaders(bearerTokenValue: string) {
    return { header: "Authorization", value: bearerTokenValue}
  }
}
