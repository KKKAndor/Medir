import {Inject, Injectable, Optional} from '@angular/core';
import {ClientBase} from "../../ClientBase";
import {HttpClient, HttpHeaders, HttpResponse, HttpResponseBase} from "@angular/common/http";
import {from as _observableFrom, Observable, of as _observableOf, throwError as _observableThrow} from "rxjs";
import {mergeMap as _observableMergeMap} from "rxjs/operators";
import {catchError as _observableCatch} from "rxjs/internal/operators/catchError";
import { API_BASE_URL, blobToText, ProblemDetails,  throwException} from "./medir-service";
import { MedicPositionsListVm } from 'src/app/_interfaces/medicposition/MedicPositionsListVm';
import { MedicPositionDetailsVm } from 'src/app/_interfaces/medicposition/MedicPositionDetailsVm';
import { CreateMedicPositionDto } from 'src/app/_interfaces/medicposition/CreateMedicPositionDto';
import { UpdateMedicPositionDto } from 'src/app/_interfaces/medicposition/UpdateMedicPositionDto';

@Injectable({
  providedIn: 'root'
})
export class MedicPositionsRepositoryService extends ClientBase {
  private http: HttpClient;
  private baseUrl: string;
  protected jsonParseReviver: ((key: string, value: any) => any) | undefined = undefined;

  constructor(@Inject(HttpClient) http: HttpClient, @Optional() @Inject(API_BASE_URL) baseUrl?: string) {
    super();
    this.http = http;
    this.baseUrl = baseUrl !== undefined && baseUrl !== null ? baseUrl : "https://localhost:7192";
  }

  /**
   * Gets the list of MedicPositions
   * @param medicId (optional)
   * @return Success
   */
  getAllMedicPositions(medicId: string | undefined): Observable<MedicPositionsListVm> {
    let url_ = this.baseUrl + "/api/Administrator/MedicPositions/GetMedicPosition?";
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
      return this.processGetAllMedicPositions(response_);
    })).pipe(_observableCatch((response_: any) => {
      if (response_ instanceof HttpResponseBase) {
        try {
          return this.processGetAllMedicPositions(response_ as any);
        } catch (e) {
          return _observableThrow(e) as any as Observable<MedicPositionsListVm>;
        }
      } else
        return _observableThrow(response_) as any as Observable<MedicPositionsListVm>;
    }));
  }

  protected processGetAllMedicPositions(response: HttpResponseBase): Observable<MedicPositionsListVm> {
    const status = response.status;
    const responseBlob =
      response instanceof HttpResponse ? response.body :
        (response as any).error instanceof Blob ? (response as any).error : undefined;

    let _headers: any = {}; if (response.headers) { for (let key of response.headers.keys()) { _headers[key] = response.headers.get(key); }}
    if (status === 200) {
      return blobToText(responseBlob).pipe(_observableMergeMap((_responseText: string) => {
        let result200: any = null;
        result200 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver) as MedicPositionsListVm;
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
   * Gets the MedicPosition by id
   * @param medicId (optional)
   * @param positionId (optional)
   * @return Success
   */
  getMedicPosition(medicId: string | undefined, positionId: string | undefined): Observable<MedicPositionDetailsVm> {
    let url_ = this.baseUrl + "/api/Administrator/MedicPositions/GetMedicPositionId?";
    if (medicId === null)
      throw new Error("The parameter 'medicId' cannot be null.");
    else if (medicId !== undefined)
      url_ += "MedicId=" + encodeURIComponent("" + medicId) + "&";
    if (positionId === null)
      throw new Error("The parameter 'positionId' cannot be null.");
    else if (positionId !== undefined)
      url_ += "PositionId=" + encodeURIComponent("" + positionId) + "&";
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
      return this.processGetMedicPosition(response_);
    })).pipe(_observableCatch((response_: any) => {
      if (response_ instanceof HttpResponseBase) {
        try {
          return this.processGetMedicPosition(response_ as any);
        } catch (e) {
          return _observableThrow(e) as any as Observable<MedicPositionDetailsVm>;
        }
      } else
        return _observableThrow(response_) as any as Observable<MedicPositionDetailsVm>;
    }));
  }

  protected processGetMedicPosition(response: HttpResponseBase): Observable<MedicPositionDetailsVm> {
    const status = response.status;
    const responseBlob =
      response instanceof HttpResponse ? response.body :
        (response as any).error instanceof Blob ? (response as any).error : undefined;

    let _headers: any = {}; if (response.headers) { for (let key of response.headers.keys()) { _headers[key] = response.headers.get(key); }}
    if (status === 200) {
      return blobToText(responseBlob).pipe(_observableMergeMap((_responseText: string) => {
        let result200: any = null;
        result200 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver) as MedicPositionDetailsVm;
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
   * Creates the MedicPosition
   * @param createMedicPositionDto createMedicPositionDto object
   * @return Success
   */
  createMedicPosition(createMedicPositionDto: CreateMedicPositionDto): Observable<string> {
    let url_ = this.baseUrl + "/api/Administrator/MedicPositions";
    url_ = url_.replace(/[?&]$/, "");

    const content_ = JSON.stringify(createMedicPositionDto);

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
      return this.processCreateMedicPosition(response_);
    })).pipe(_observableCatch((response_: any) => {
      if (response_ instanceof HttpResponseBase) {
        try {
          return this.processCreateMedicPosition(response_ as any);
        } catch (e) {
          return _observableThrow(e) as any as Observable<string>;
        }
      } else
        return _observableThrow(response_) as any as Observable<string>;
    }));
  }

  protected processCreateMedicPosition(response: HttpResponseBase): Observable<string> {
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
   * Updates the MedicPosition
   * @param updateMedicPositionDto UpdateMedicPositionDto object
   * @return Success
   */
  updateMedicPosition(updateMedicPositionDto: UpdateMedicPositionDto): Observable<void> {
    let url_ = this.baseUrl + "/api/Administrator/MedicPositions";
    url_ = url_.replace(/[?&]$/, "");

    const content_ = JSON.stringify(updateMedicPositionDto);

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
      return this.processUpdateMedicPosition(response_);
    })).pipe(_observableCatch((response_: any) => {
      if (response_ instanceof HttpResponseBase) {
        try {
          return this.processUpdateMedicPosition(response_ as any);
        } catch (e) {
          return _observableThrow(e) as any as Observable<void>;
        }
      } else
        return _observableThrow(response_) as any as Observable<void>;
    }));
  }

  protected processUpdateMedicPosition(response: HttpResponseBase): Observable<void> {
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
   * Deletes  the MedicPosition
   * @param medicId (optional)
   * @param positionId (optional)
   * @return Success
   */
  deleteMedicPosition(medicId: string | undefined, positionId: string | undefined): Observable<void> {
    let url_ = this.baseUrl + "/api/Administrator/MedicPositions/DeleteMedicPositionId?";
    if (medicId === null)
      throw new Error("The parameter 'medicId' cannot be null.");
    else if (medicId !== undefined)
      url_ += "MedicId=" + encodeURIComponent("" + medicId) + "&";
    if (positionId === null)
      throw new Error("The parameter 'positionId' cannot be null.");
    else if (positionId !== undefined)
      url_ += "PositionId=" + encodeURIComponent("" + positionId) + "&";
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
      return this.processDeleteMedicPosition(response_);
    })).pipe(_observableCatch((response_: any) => {
      if (response_ instanceof HttpResponseBase) {
        try {
          return this.processDeleteMedicPosition(response_ as any);
        } catch (e) {
          return _observableThrow(e) as any as Observable<void>;
        }
      } else
        return _observableThrow(response_) as any as Observable<void>;
    }));
  }

  protected processDeleteMedicPosition(response: HttpResponseBase): Observable<void> {
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
