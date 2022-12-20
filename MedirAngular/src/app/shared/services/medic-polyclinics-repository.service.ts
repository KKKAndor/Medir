import {Inject, Injectable, Optional} from '@angular/core';
import {ClientBase} from "../../ClientBase";
import {HttpClient, HttpHeaders, HttpResponse, HttpResponseBase} from "@angular/common/http";
import {from as _observableFrom, Observable, of as _observableOf, throwError as _observableThrow} from "rxjs";
import {mergeMap as _observableMergeMap} from "rxjs/operators";
import {catchError as _observableCatch} from "rxjs/internal/operators/catchError";
import { API_BASE_URL, blobToText, ProblemDetails, throwException} from "./medir-service";
import { MedicPolyclinicsListVm } from 'src/app/_interfaces/medicpolyclinics/MedicPolyclinicsListVm';
import { MedicPolyclinicDetailsVm } from 'src/app/_interfaces/medicpolyclinics/MedicPolyclinicDetailsVm';
import { CreateMedicPolyclinicDto } from 'src/app/_interfaces/medicpolyclinics/CreateMedicPolyclinicDto';
import { UpdateMedicPolyclinicDto } from 'src/app/_interfaces/medicpolyclinics/UpdateMedicPolyclinicDto';

@Injectable({
  providedIn: 'root'
})
export class MedicPolyclinicsRepositoryService extends ClientBase {
  private http: HttpClient;
  private baseUrl: string;
  protected jsonParseReviver: ((key: string, value: any) => any) | undefined = undefined;

  constructor(@Inject(HttpClient) http: HttpClient, @Optional() @Inject(API_BASE_URL) baseUrl?: string) {
    super();
    this.http = http;
    this.baseUrl = baseUrl !== undefined && baseUrl !== null ? baseUrl : "https://localhost:7192";
  }

  /**
   * Gets the list of MedicPolyclinics
   * @param medicId (optional)
   * @return Success
   */
  getAllMedicPolyclinics(medicId: string | undefined): Observable<MedicPolyclinicsListVm> {
    let url_ = this.baseUrl + "/api/Administrator/MedicPolyclinics/GetMedicPolyclinic?";
    if (medicId === null)
      throw new Error("The parameter 'medicId' cannot be null.");
    else if (medicId !== undefined)
      url_ += "MedicId=" + encodeURIComponent("" + medicId) + "&";
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
      return this.processGetAllMedicPolyclinics(response_);
    })).pipe(_observableCatch((response_: any) => {
      if (response_ instanceof HttpResponseBase) {
        try {
          return this.processGetAllMedicPolyclinics(response_ as any);
        } catch (e) {
          return _observableThrow(e) as any as Observable<MedicPolyclinicsListVm>;
        }
      } else
        return _observableThrow(response_) as any as Observable<MedicPolyclinicsListVm>;
    }));
  }

  protected processGetAllMedicPolyclinics(response: HttpResponseBase): Observable<MedicPolyclinicsListVm> {
    const status = response.status;
    const responseBlob =
      response instanceof HttpResponse ? response.body :
        (response as any).error instanceof Blob ? (response as any).error : undefined;

    let _headers: any = {}; if (response.headers) { for (let key of response.headers.keys()) { _headers[key] = response.headers.get(key); }}
    if (status === 200) {
      return blobToText(responseBlob).pipe(_observableMergeMap((_responseText: string) => {
        let result200: any = null;
        result200 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver) as MedicPolyclinicsListVm;
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
   * Gets the MedicPolyclinic by id
   * @param medicId (optional)
   * @param polyclinicId (optional)
   * @return Success
   */
  getMedicPolyclinic(medicId: string | undefined, polyclinicId: string | undefined): Observable<MedicPolyclinicDetailsVm> {
    let url_ = this.baseUrl + "/api/Administrator/MedicPolyclinics/GetMedicPolyclinicId?";
    if (medicId === null)
      throw new Error("The parameter 'medicId' cannot be null.");
    else if (medicId !== undefined)
      url_ += "MedicId=" + encodeURIComponent("" + medicId) + "&";
    if (polyclinicId === null)
      throw new Error("The parameter 'polyclinicId' cannot be null.");
    else if (polyclinicId !== undefined)
      url_ += "PolyclinicId=" + encodeURIComponent("" + polyclinicId) + "&";
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
      return this.processGetMedicPolyclinic(response_);
    })).pipe(_observableCatch((response_: any) => {
      if (response_ instanceof HttpResponseBase) {
        try {
          return this.processGetMedicPolyclinic(response_ as any);
        } catch (e) {
          return _observableThrow(e) as any as Observable<MedicPolyclinicDetailsVm>;
        }
      } else
        return _observableThrow(response_) as any as Observable<MedicPolyclinicDetailsVm>;
    }));
  }

  protected processGetMedicPolyclinic(response: HttpResponseBase): Observable<MedicPolyclinicDetailsVm> {
    const status = response.status;
    const responseBlob =
      response instanceof HttpResponse ? response.body :
        (response as any).error instanceof Blob ? (response as any).error : undefined;

    let _headers: any = {}; if (response.headers) { for (let key of response.headers.keys()) { _headers[key] = response.headers.get(key); }}
    if (status === 200) {
      return blobToText(responseBlob).pipe(_observableMergeMap((_responseText: string) => {
        let result200: any = null;
        result200 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver) as MedicPolyclinicDetailsVm;
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
   * Creates the MedicPolyclinic
   * @param createMedicPolyclinicDto createMedicPolyclinicDto object
   * @return Success
   */
  createMedicPolyclinic(createMedicPolyclinicDto: CreateMedicPolyclinicDto): Observable<string> {
    let url_ = this.baseUrl + "/api/Administrator/MedicPolyclinics";
    url_ = url_.replace(/[?&]$/, "");

    const content_ = JSON.stringify(createMedicPolyclinicDto);

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
      return this.processCreateMedicPolyclinic(response_);
    })).pipe(_observableCatch((response_: any) => {
      if (response_ instanceof HttpResponseBase) {
        try {
          return this.processCreateMedicPolyclinic(response_ as any);
        } catch (e) {
          return _observableThrow(e) as any as Observable<string>;
        }
      } else
        return _observableThrow(response_) as any as Observable<string>;
    }));
  }

  protected processCreateMedicPolyclinic(response: HttpResponseBase): Observable<string> {
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
   * Updates the MedicPolyclinic
   * @param updateMedicPolyclinicDto UpdateMedicPolyclinicDto object
   * @return Success
   */
  updateMedicPolyclinic(updateMedicPolyclinicDto: UpdateMedicPolyclinicDto): Observable<void> {
    let url_ = this.baseUrl + "/api/Administrator/MedicPolyclinics";
    url_ = url_.replace(/[?&]$/, "");

    const content_ = JSON.stringify(updateMedicPolyclinicDto);

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
      return this.processUpdateMedicPolyclinic(response_);
    })).pipe(_observableCatch((response_: any) => {
      if (response_ instanceof HttpResponseBase) {
        try {
          return this.processUpdateMedicPolyclinic(response_ as any);
        } catch (e) {
          return _observableThrow(e) as any as Observable<void>;
        }
      } else
        return _observableThrow(response_) as any as Observable<void>;
    }));
  }

  protected processUpdateMedicPolyclinic(response: HttpResponseBase): Observable<void> {
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
   * Deletes  the MedicPolyclinic
   * @param medicId (optional)
   * @param polyclinicId (optional)
   * @return Success
   */
  deleteMedicPolyclinic(medicId: string | undefined, polyclinicId: string | undefined): Observable<void> {
    let url_ = this.baseUrl + "/api/Administrator/MedicPolyclinics/DeleteMedicPolyclinicId?";
    if (medicId === null)
      throw new Error("The parameter 'medicId' cannot be null.");
    else if (medicId !== undefined)
      url_ += "MedicId=" + encodeURIComponent("" + medicId) + "&";
    if (polyclinicId === null)
      throw new Error("The parameter 'polyclinicId' cannot be null.");
    else if (polyclinicId !== undefined)
      url_ += "PolyclinicId=" + encodeURIComponent("" + polyclinicId) + "&";
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
      return this.processDeleteMedicPolyclinic(response_);
    })).pipe(_observableCatch((response_: any) => {
      if (response_ instanceof HttpResponseBase) {
        try {
          return this.processDeleteMedicPolyclinic(response_ as any);
        } catch (e) {
          return _observableThrow(e) as any as Observable<void>;
        }
      } else
        return _observableThrow(response_) as any as Observable<void>;
    }));
  }

  protected processDeleteMedicPolyclinic(response: HttpResponseBase): Observable<void> {
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
