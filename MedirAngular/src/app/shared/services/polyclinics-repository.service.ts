import {Inject, Injectable, Optional} from '@angular/core';
import {ClientBase} from "../../ClientBase";
import {HttpClient, HttpHeaders, HttpResponse, HttpResponseBase} from "@angular/common/http";
import {from as _observableFrom, Observable, of as _observableOf, throwError as _observableThrow} from "rxjs";
import {mergeMap as _observableMergeMap} from "rxjs/operators";
import {catchError as _observableCatch} from "rxjs/internal/operators/catchError";
import { API_BASE_URL, blobToText, ProblemDetails, throwException} from "./medir-service";
import { PolyclinicsListVm } from 'src/app/_interfaces/polyclinics/PolyclinicsListVm';
import { CreatePolyclinicDto } from 'src/app/_interfaces/polyclinics/CreatePolyclinicDto';
import { UpdatePolyclinicDto } from 'src/app/_interfaces/polyclinics/UpdatePolyclinicDto';
import { PolyclinicDetailsVm } from 'src/app/_interfaces/polyclinics/PolyclinicDetailsVm';

@Injectable({
  providedIn: 'root'
})
export class PolyclinicsRepositoryService extends ClientBase {
  private http: HttpClient;
  private baseUrl: string;
  protected jsonParseReviver: ((key: string, value: any) => any) | undefined = undefined;

  constructor(@Inject(HttpClient) http: HttpClient, @Optional() @Inject(API_BASE_URL) baseUrl?: string) {
    super();
    this.http = http;
    this.baseUrl = baseUrl !== undefined && baseUrl !== null ? baseUrl : "https://localhost:7192";
  }

  /**
   * Gets the list of Polyclinics
   * @param cityFilterId (optional)
   * @param pageNumber (optional)
   * @param pageSize (optional)
   * @param orderBy (optional)
   * @param contains (optional)
   * @return Success
   */
  getAllPolyclinics(cityFilterId?: string | null | undefined,
                    pageNumber?: number | undefined,
                    pageSize?: number | undefined,
                    orderBy?: string | null | undefined,
                    contains?: string | null | undefined): Observable<PolyclinicsListVm> {
    let url_ = this.baseUrl + "/api/Administrator/Polyclinics?";
    if (cityFilterId !== undefined && cityFilterId !== null)
      url_ += "CityFilterId=" + encodeURIComponent("" + cityFilterId) + "&";
    if (pageNumber === null)
      throw new Error("The parameter 'pageNumber' cannot be null.");
    else if (pageNumber !== undefined)
      url_ += "PageNumber=" + encodeURIComponent("" + pageNumber) + "&";
    if (pageSize === null)
      throw new Error("The parameter 'pageSize' cannot be null.");
    else if (pageSize !== undefined)
      url_ += "PageSize=" + encodeURIComponent("" + pageSize) + "&";
    if (orderBy !== undefined && orderBy !== null)
      url_ += "OrderBy=" + encodeURIComponent("" + orderBy) + "&";
    if (contains !== undefined && contains !== null)
      url_ += "Contains=" + encodeURIComponent("" + contains) + "&";
    url_ = url_.replace(/[?&]$/, "");

    let options_ : any = {
      observe: "response",
      responseType: "blob",
      headers: new HttpHeaders({
        "Accept": "application/json"
      })
    };

    return _observableFrom(this.transformOptions(options_)).pipe(_observableMergeMap(transformedOptions_ => {
      return this.http.request("get", url_, transformedOptions_);
    })).pipe(_observableMergeMap((response_: any) => {
      return this.processGetAllPolyclinics(response_);
    })).pipe(_observableCatch((response_: any) => {
      if (response_ instanceof HttpResponseBase) {
        try {
          return this.processGetAllPolyclinics(response_ as any);
        } catch (e) {
          return _observableThrow(e) as any as Observable<PolyclinicsListVm>;
        }
      } else
        return _observableThrow(response_) as any as Observable<PolyclinicsListVm>;
    }));
  }

  protected processGetAllPolyclinics(response: HttpResponseBase): Observable<PolyclinicsListVm> {
    const status = response.status;
    const responseBlob =
      response instanceof HttpResponse ? response.body :
        (response as any).error instanceof Blob ? (response as any).error : undefined;

    let _headers: any = {}; if (response.headers) { for (let key of response.headers.keys()) { _headers[key] = response.headers.get(key); }}
    if (status === 200) {
      return blobToText(responseBlob).pipe(_observableMergeMap((_responseText: string) => {
        let result200: any = null;
        result200 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver) as PolyclinicsListVm;
        return _observableOf(result200);
      }));
    } else if (status === 401) {
      return blobToText(responseBlob).pipe(_observableMergeMap((_responseText: string) => {
        let result401: any = null;
        result401 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver) as ProblemDetails;
        return throwException("User is unauthorized", status, _responseText, _headers, result401);
      }));
    } else if (status !== 200 && status !== 204) {
      return blobToText(responseBlob).pipe(_observableMergeMap((_responseText: string) => {
        return throwException("An unexpected server error occurred.", status, _responseText, _headers);
      }));
    }
    return _observableOf(null as any);
  }

  /**
   * Creates the Polyclinic
   * @param createPolyclinicDto createPolyclinicDto object
   * @return Success
   */
  createPolyclinic(createPolyclinicDto: CreatePolyclinicDto): Observable<string> {
    let url_ = this.baseUrl + "/api/Administrator/Polyclinics";
    url_ = url_.replace(/[?&]$/, "");

    const content_ = JSON.stringify(createPolyclinicDto);

    let options_ : any = {
      body: content_,
      observe: "response",
      responseType: "blob",
      headers: new HttpHeaders({
        "Content-Type": "application/json",
        "Accept": "application/json"
      })
    };

    return _observableFrom(this.transformOptions(options_)).pipe(_observableMergeMap(transformedOptions_ => {
      return this.http.request("post", url_, transformedOptions_);
    })).pipe(_observableMergeMap((response_: any) => {
      return this.processCreatePolyclinic(response_);
    })).pipe(_observableCatch((response_: any) => {
      if (response_ instanceof HttpResponseBase) {
        try {
          return this.processCreatePolyclinic(response_ as any);
        } catch (e) {
          return _observableThrow(e) as any as Observable<string>;
        }
      } else
        return _observableThrow(response_) as any as Observable<string>;
    }));
  }

  protected processCreatePolyclinic(response: HttpResponseBase): Observable<string> {
    const status = response.status;
    const responseBlob =
      response instanceof HttpResponse ? response.body :
        (response as any).error instanceof Blob ? (response as any).error : undefined;

    let _headers: any = {}; if (response.headers) { for (let key of response.headers.keys()) { _headers[key] = response.headers.get(key); }}
    if (status === 201) {
      return blobToText(responseBlob).pipe(_observableMergeMap((_responseText: string) => {
        let result201: any = null;
        result201 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver) as string;
        return _observableOf(result201);
      }));
    } else if (status === 401) {
      return blobToText(responseBlob).pipe(_observableMergeMap((_responseText: string) => {
        let result401: any = null;
        result401 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver) as ProblemDetails;
        return throwException("User is unauthorized", status, _responseText, _headers, result401);
      }));
    } else if (status !== 200 && status !== 204) {
      return blobToText(responseBlob).pipe(_observableMergeMap((_responseText: string) => {
        return throwException("An unexpected server error occurred.", status, _responseText, _headers);
      }));
    }
    return _observableOf(null as any);
  }

  /**
   * Updates the Polyclinic
   * @param updatePolyclinicDto UpdatePolyclinicDto object
   * @return Success
   */
  updatePolyclinic(updatePolyclinicDto: UpdatePolyclinicDto): Observable<void> {
    let url_ = this.baseUrl + "/api/Administrator/Polyclinics";
    url_ = url_.replace(/[?&]$/, "");

    const content_ = JSON.stringify(updatePolyclinicDto);

    let options_ : any = {
      body: content_,
      observe: "response",
      responseType: "blob",
      headers: new HttpHeaders({
        "Content-Type": "application/json",
      })
    };

    return _observableFrom(this.transformOptions(options_)).pipe(_observableMergeMap(transformedOptions_ => {
      return this.http.request("put", url_, transformedOptions_);
    })).pipe(_observableMergeMap((response_: any) => {
      return this.processUpdatePolyclinic(response_);
    })).pipe(_observableCatch((response_: any) => {
      if (response_ instanceof HttpResponseBase) {
        try {
          return this.processUpdatePolyclinic(response_ as any);
        } catch (e) {
          return _observableThrow(e) as any as Observable<void>;
        }
      } else
        return _observableThrow(response_) as any as Observable<void>;
    }));
  }

  protected processUpdatePolyclinic(response: HttpResponseBase): Observable<void> {
    const status = response.status;
    const responseBlob =
      response instanceof HttpResponse ? response.body :
        (response as any).error instanceof Blob ? (response as any).error : undefined;

    let _headers: any = {}; if (response.headers) { for (let key of response.headers.keys()) { _headers[key] = response.headers.get(key); }}
    if (status === 204) {
      return blobToText(responseBlob).pipe(_observableMergeMap((_responseText: string) => {
        return _observableOf(null as any);
      }));
    } else if (status === 401) {
      return blobToText(responseBlob).pipe(_observableMergeMap((_responseText: string) => {
        let result401: any = null;
        result401 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver) as ProblemDetails;
        return throwException("User is unauthorized", status, _responseText, _headers, result401);
      }));
    } else if (status !== 200 && status !== 204) {
      return blobToText(responseBlob).pipe(_observableMergeMap((_responseText: string) => {
        return throwException("An unexpected server error occurred.", status, _responseText, _headers);
      }));
    }
    return _observableOf(null as any);
  }

  /**
   * Gets the Polyclinic by id
   * @param polyclinicId Polyclinic id (Guid)
   * @return Success
   */
  getPolyclinic(polyclinicId: string): Observable<PolyclinicDetailsVm> {
    let url_ = this.baseUrl + "/api/Administrator/Polyclinics/{PolyclinicId}";
    if (polyclinicId === undefined || polyclinicId === null)
      throw new Error("The parameter 'polyclinicId' must be defined.");
    url_ = url_.replace("{PolyclinicId}", encodeURIComponent("" + polyclinicId));
    url_ = url_.replace(/[?&]$/, "");

    let options_ : any = {
      observe: "response",
      responseType: "blob",
      headers: new HttpHeaders({
        "Accept": "application/json"
      })
    };

    return _observableFrom(this.transformOptions(options_)).pipe(_observableMergeMap(transformedOptions_ => {
      return this.http.request("get", url_, transformedOptions_);
    })).pipe(_observableMergeMap((response_: any) => {
      return this.processGetPolyclinic(response_);
    })).pipe(_observableCatch((response_: any) => {
      if (response_ instanceof HttpResponseBase) {
        try {
          return this.processGetPolyclinic(response_ as any);
        } catch (e) {
          return _observableThrow(e) as any as Observable<PolyclinicDetailsVm>;
        }
      } else
        return _observableThrow(response_) as any as Observable<PolyclinicDetailsVm>;
    }));
  }

  protected processGetPolyclinic(response: HttpResponseBase): Observable<PolyclinicDetailsVm> {
    const status = response.status;
    const responseBlob =
      response instanceof HttpResponse ? response.body :
        (response as any).error instanceof Blob ? (response as any).error : undefined;

    let _headers: any = {}; if (response.headers) { for (let key of response.headers.keys()) { _headers[key] = response.headers.get(key); }}
    if (status === 200) {
      return blobToText(responseBlob).pipe(_observableMergeMap((_responseText: string) => {
        let result200: any = null;
        result200 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver) as PolyclinicDetailsVm;
        return _observableOf(result200);
      }));
    } else if (status === 401) {
      return blobToText(responseBlob).pipe(_observableMergeMap((_responseText: string) => {
        let result401: any = null;
        result401 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver) as ProblemDetails;
        return throwException("User is unauthorized", status, _responseText, _headers, result401);
      }));
    } else if (status !== 200 && status !== 204) {
      return blobToText(responseBlob).pipe(_observableMergeMap((_responseText: string) => {
        return throwException("An unexpected server error occurred.", status, _responseText, _headers);
      }));
    }
    return _observableOf(null as any);
  }

  /**
   * Deletes  the Polyclinic
   * @param polyclinicId Polyclinic id (Guid)
   * @return Success
   */
  deletePolyclinic(polyclinicId: string): Observable<void> {
    let url_ = this.baseUrl + "/api/Administrator/Polyclinics/{PolyclinicId}";
    if (polyclinicId === undefined || polyclinicId === null)
      throw new Error("The parameter 'polyclinicId' must be defined.");
    url_ = url_.replace("{PolyclinicId}", encodeURIComponent("" + polyclinicId));
    url_ = url_.replace(/[?&]$/, "");

    let options_ : any = {
      observe: "response",
      responseType: "blob",
      headers: new HttpHeaders({
      })
    };

    return _observableFrom(this.transformOptions(options_)).pipe(_observableMergeMap(transformedOptions_ => {
      return this.http.request("delete", url_, transformedOptions_);
    })).pipe(_observableMergeMap((response_: any) => {
      return this.processDeletePolyclinic(response_);
    })).pipe(_observableCatch((response_: any) => {
      if (response_ instanceof HttpResponseBase) {
        try {
          return this.processDeletePolyclinic(response_ as any);
        } catch (e) {
          return _observableThrow(e) as any as Observable<void>;
        }
      } else
        return _observableThrow(response_) as any as Observable<void>;
    }));
  }

  protected processDeletePolyclinic(response: HttpResponseBase): Observable<void> {
    const status = response.status;
    const responseBlob =
      response instanceof HttpResponse ? response.body :
        (response as any).error instanceof Blob ? (response as any).error : undefined;

    let _headers: any = {}; if (response.headers) { for (let key of response.headers.keys()) { _headers[key] = response.headers.get(key); }}
    if (status === 204) {
      return blobToText(responseBlob).pipe(_observableMergeMap((_responseText: string) => {
        return _observableOf(null as any);
      }));
    } else if (status === 401) {
      return blobToText(responseBlob).pipe(_observableMergeMap((_responseText: string) => {
        let result401: any = null;
        result401 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver) as ProblemDetails;
        return throwException("User is unauthorized", status, _responseText, _headers, result401);
      }));
    } else if (status !== 200 && status !== 204) {
      return blobToText(responseBlob).pipe(_observableMergeMap((_responseText: string) => {
        return throwException("An unexpected server error occurred.", status, _responseText, _headers);
      }));
    }
    return _observableOf(null as any);
  }
}
