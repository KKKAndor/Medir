import {Inject, Injectable, Optional} from '@angular/core';
import {ClientBase} from "../../ClientBase";
import {HttpClient, HttpHeaders, HttpResponse, HttpResponseBase} from "@angular/common/http";
import {from as _observableFrom, Observable, of as _observableOf, throwError as _observableThrow} from "rxjs";
import {mergeMap as _observableMergeMap} from "rxjs/operators";
import {catchError as _observableCatch} from "rxjs/internal/operators/catchError";
import { API_BASE_URL, blobToText, FileResponse, ProblemDetails, throwException} from "./medir-service";
import { CreateMedicAvailabilityDto } from 'src/app/_interfaces/medicavailability/CreateMedicAvailabilityDto';
import { MedicAvailabilityListVm } from 'src/app/_interfaces/medicavailability/MedicAvailabilityListVm';
import { DeleteMedicAvailabilityCommand } from 'src/app/_interfaces/medicavailability/DeleteMedicAvailabilityCommand';

@Injectable({
  providedIn: 'root'
})
export class MedicAvailabilityRepositoryService extends ClientBase {
  private http: HttpClient;
  private baseUrl: string;
  protected jsonParseReviver: ((key: string, value: any) => any) | undefined = undefined;

  constructor(@Inject(HttpClient) http: HttpClient, @Optional() @Inject(API_BASE_URL) baseUrl?: string) {
    super();
    this.http = http;
    this.baseUrl = baseUrl !== undefined && baseUrl !== null ? baseUrl : "https://localhost:7192";
  }

  /**
   * Gets the list of MedicAvailabiltyOnDay
   * @param medicId (optional)
   * @return Success
   */
  getMedicAvailabilitiesList(medicId: string | undefined): Observable<MedicAvailabilityListVm> {
    let url_ = this.baseUrl + "/api/Administrator/MedicAvailability/GetMedicAvailabilitiesList?";
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
      return this.processGetMedicAvailabilitiesList(response_);
    })).pipe(_observableCatch((response_: any) => {
      if (response_ instanceof HttpResponseBase) {
        try {
          return this.processGetMedicAvailabilitiesList(response_ as any);
        } catch (e) {
          return _observableThrow(e) as any as Observable<MedicAvailabilityListVm>;
        }
      } else
        return _observableThrow(response_) as any as Observable<MedicAvailabilityListVm>;
    }));
  }

  protected processGetMedicAvailabilitiesList(response: HttpResponseBase): Observable<MedicAvailabilityListVm> {
    const status = response.status;
    const responseBlob =
      response instanceof HttpResponse ? response.body :
        (response as any).error instanceof Blob ? (response as any).error : undefined;

    let _headers: any = {}; if (response.headers) { for (let key of response.headers.keys()) { _headers[key] = response.headers.get(key); }}
    if (status === 200) {
      return blobToText(responseBlob).pipe(_observableMergeMap((_responseText: string) => {
        let result200: any = null;
        result200 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver) as MedicAvailabilityListVm;
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
   * Creates the MedicAvailability
   * @param createMedicAvailabilityDto CreateMedicAvailabilityDto object
   * @return Success
   */
  createMedicAvailability(createMedicAvailabilityDto: CreateMedicAvailabilityDto): Observable<FileResponse> {
    let url_ = this.baseUrl + "/api/Administrator/MedicAvailability/CreateMedicAvailability";
    url_ = url_.replace(/[?&]$/, "");

    const content_ = JSON.stringify(createMedicAvailabilityDto);

    let options_ : any = {
      body: content_,
      observe: "response",
      responseType: "blob",
      headers: new HttpHeaders({
        "Content-Type": "application/json",
        "Accept": "application/octet-stream"
      })
    };

    return _observableFrom(this.transformOptions(options_)).pipe(_observableMergeMap(transformedOptions_ => {
      return this.http.request("post", url_, transformedOptions_);
    })).pipe(_observableMergeMap((response_: any) => {
      return this.processCreateMedicAvailability(response_);
    })).pipe(_observableCatch((response_: any) => {
      if (response_ instanceof HttpResponseBase) {
        try {
          return this.processCreateMedicAvailability(response_ as any);
        } catch (e) {
          return _observableThrow(e) as any as Observable<FileResponse>;
        }
      } else
        return _observableThrow(response_) as any as Observable<FileResponse>;
    }));
  }

  protected processCreateMedicAvailability(response: HttpResponseBase): Observable<FileResponse> {
    const status = response.status;
    const responseBlob =
      response instanceof HttpResponse ? response.body :
        (response as any).error instanceof Blob ? (response as any).error : undefined;

    let _headers: any = {}; if (response.headers) { for (let key of response.headers.keys()) { _headers[key] = response.headers.get(key); }}
    if (status === 201) {
      const contentDisposition = response.headers ? response.headers.get("content-disposition") : undefined;
      let fileNameMatch = contentDisposition ? /filename\*=(?:(\\?['"])(.*?)\1|(?:[^\s]+'.*?')?([^;\n]*))/g.exec(contentDisposition) : undefined;
      let fileName = fileNameMatch && fileNameMatch.length > 1 ? fileNameMatch[3] || fileNameMatch[2] : undefined;
      if (fileName) {
        fileName = decodeURIComponent(fileName);
      } else {
        fileNameMatch = contentDisposition ? /filename="?([^"]*?)"?(;|$)/g.exec(contentDisposition) : undefined;
        fileName = fileNameMatch && fileNameMatch.length > 1 ? fileNameMatch[1] : undefined;
      }
      return _observableOf({ fileName: fileName, data: responseBlob as any, status: status, headers: _headers });
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
   * Delete the MedicAvailability
   * @param deleteMedicAvailabilityCommand DeleteMedicAvailabilityCommand object
   * @return Success
   */
  deleteMedicAvailability(deleteMedicAvailabilityCommand: DeleteMedicAvailabilityCommand): Observable<void> {
    let url_ = this.baseUrl + "/api/Administrator/MedicAvailability/DeleteMedicAvailability";
    url_ = url_.replace(/[?&]$/, "");

    const content_ = JSON.stringify(deleteMedicAvailabilityCommand);

    let options_ : any = {
      body: content_,
      observe: "response",
      responseType: "blob",
      headers: new HttpHeaders({
        "Content-Type": "application/json",
      })
    };

    return _observableFrom(this.transformOptions(options_)).pipe(_observableMergeMap(transformedOptions_ => {
      return this.http.request("delete", url_, transformedOptions_);
    })).pipe(_observableMergeMap((response_: any) => {
      return this.processDeleteMedicAvailability(response_);
    })).pipe(_observableCatch((response_: any) => {
      if (response_ instanceof HttpResponseBase) {
        try {
          return this.processDeleteMedicAvailability(response_ as any);
        } catch (e) {
          return _observableThrow(e) as any as Observable<void>;
        }
      } else
        return _observableThrow(response_) as any as Observable<void>;
    }));
  }

  protected processDeleteMedicAvailability(response: HttpResponseBase): Observable<void> {
    const status = response.status;
    const responseBlob =
      response instanceof HttpResponse ? response.body :
        (response as any).error instanceof Blob ? (response as any).error : undefined;

    let _headers: any = {}; if (response.headers) { for (let key of response.headers.keys()) { _headers[key] = response.headers.get(key); }}
    if (status === 201) {
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
