import {Inject, Injectable, Optional} from '@angular/core';
import {ClientBase} from "../../ClientBase";
import {HttpClient, HttpHeaders, HttpResponse, HttpResponseBase} from "@angular/common/http";
import {from as _observableFrom, Observable, of as _observableOf, throwError as _observableThrow} from "rxjs";
import {mergeMap as _observableMergeMap} from "rxjs/operators";
import {catchError as _observableCatch} from "rxjs/internal/operators/catchError";
import { API_BASE_URL, blobToText, ProblemDetails, throwException} from "./medir-service";
import { CitiesListVm } from 'src/app/_interfaces/cities/CitiesListVm';
import { PolyclinicsForUserListVm } from 'src/app/_interfaces/userui/PolyclinicsForUserListVm';
import { PositionsForUserListVm } from 'src/app/_interfaces/userui/PositionsForUserListVm';
import { MedicsForUserListVm } from 'src/app/_interfaces/userui/MedicsForUserListVm';
import { AppointmentsListVm } from 'src/app/_interfaces/userui/AppointmentsListVm';
import { CreateAppointmentDto } from 'src/app/_interfaces/userui/CreateAppointmentDto';
import { MedicAvailabilityForUserListVm } from 'src/app/_interfaces/userui/MedicAvailabilityForUserListVm';

@Injectable({
  providedIn: 'root'
})
export class UserUiService extends ClientBase {
  private http: HttpClient;
  private baseUrl: string;
  protected jsonParseReviver: ((key: string, value: any) => any) | undefined = undefined;

  constructor(@Inject(HttpClient) http: HttpClient, @Optional() @Inject(API_BASE_URL) baseUrl?: string) {
    super();
    this.http = http;
    this.baseUrl = baseUrl !== undefined && baseUrl !== null ? baseUrl : "https://localhost:7192";
  }

  /**
   * Gets the list of Cities
   * @return Success
   */
  getAllCitiesForChoice(): Observable<CitiesListVm> {
    let url_ = this.baseUrl + "/api/User";
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
      return this.processGetAllCitiesForChoice(response_);
    })).pipe(_observableCatch((response_: any) => {
      if (response_ instanceof HttpResponseBase) {
        try {
          return this.processGetAllCitiesForChoice(response_ as any);
        } catch (e) {
          return _observableThrow(e) as any as Observable<CitiesListVm>;
        }
      } else
        return _observableThrow(response_) as any as Observable<CitiesListVm>;
    }));
  }

  protected processGetAllCitiesForChoice(response: HttpResponseBase): Observable<CitiesListVm> {
    const status = response.status;
    const responseBlob =
      response instanceof HttpResponse ? response.body :
        (response as any).error instanceof Blob ? (response as any).error : undefined;

    let _headers: any = {}; if (response.headers) { for (let key of response.headers.keys()) { _headers[key] = response.headers.get(key); }}
    if (status === 200) {
      return blobToText(responseBlob).pipe(_observableMergeMap((_responseText: string) => {
        let result200: any = null;
        result200 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver) as CitiesListVm;
        return _observableOf(result200);
      }));
    } else if (status === 401) {
      return blobToText(responseBlob).pipe(_observableMergeMap((_responseText: string) => {
        let result401: any = null;
        result401 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver) as ProblemDetails;
        return throwException("User is unauthorized", status, _responseText, _headers, result401);
      }));
    } else if (status === 403) {
      return blobToText(responseBlob).pipe(_observableMergeMap((_responseText: string) => {
        let result403: any = null;
        result403 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver) as ProblemDetails;
        return throwException("A server side error occurred.", status, _responseText, _headers, result403);
      }));
    } else if (status !== 200 && status !== 204) {
      return blobToText(responseBlob).pipe(_observableMergeMap((_responseText: string) => {
        return throwException("An unexpected server error occurred.", status, _responseText, _headers);
      }));
    }
    return _observableOf(null as any);
  }

  /**
   * Gets the list of PositionsForUser
   * @param cityId (optional) City Id (Guid)
   * @return Success
   */
  getPositionsForUserList(cityId: string | undefined): Observable<PositionsForUserListVm> {
    let url_ = this.baseUrl + "/api/User/PositionsForUser?";
    if (cityId === null)
      throw new Error("The parameter 'cityId' cannot be null.");
    else if (cityId !== undefined)
      url_ += "CityId=" + encodeURIComponent("" + cityId) + "&";
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
      return this.processGetPositionsForUserList(response_);
    })).pipe(_observableCatch((response_: any) => {
      if (response_ instanceof HttpResponseBase) {
        try {
          return this.processGetPositionsForUserList(response_ as any);
        } catch (e) {
          return _observableThrow(e) as any as Observable<PositionsForUserListVm>;
        }
      } else
        return _observableThrow(response_) as any as Observable<PositionsForUserListVm>;
    }));
  }

  protected processGetPositionsForUserList(response: HttpResponseBase): Observable<PositionsForUserListVm> {
    const status = response.status;
    const responseBlob =
      response instanceof HttpResponse ? response.body :
        (response as any).error instanceof Blob ? (response as any).error : undefined;

    let _headers: any = {}; if (response.headers) { for (let key of response.headers.keys()) { _headers[key] = response.headers.get(key); }}
    if (status === 200) {
      return blobToText(responseBlob).pipe(_observableMergeMap((_responseText: string) => {
        let result200: any = null;
        result200 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver) as PositionsForUserListVm;
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
   * Gets the list of MedicsForUser
   * @param positionId (optional)
   * @param cityId (optional)
   * @return Success
   */
  getMedicsForUserList(positionId: string | undefined, cityId: string | undefined): Observable<MedicsForUserListVm> {
    let url_ = this.baseUrl + "/api/User/MedicsForUser?";
    if (positionId === null)
      throw new Error("The parameter 'positionId' cannot be null.");
    else if (positionId !== undefined)
      url_ += "PositionId=" + encodeURIComponent("" + positionId) + "&";
    if (cityId === null)
      throw new Error("The parameter 'cityId' cannot be null.");
    else if (cityId !== undefined)
      url_ += "CityId=" + encodeURIComponent("" + cityId) + "&";
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
      return this.processGetMedicsForUserList(response_);
    })).pipe(_observableCatch((response_: any) => {
      if (response_ instanceof HttpResponseBase) {
        try {
          return this.processGetMedicsForUserList(response_ as any);
        } catch (e) {
          return _observableThrow(e) as any as Observable<MedicsForUserListVm>;
        }
      } else
        return _observableThrow(response_) as any as Observable<MedicsForUserListVm>;
    }));
  }

  protected processGetMedicsForUserList(response: HttpResponseBase): Observable<MedicsForUserListVm> {
    const status = response.status;
    const responseBlob =
      response instanceof HttpResponse ? response.body :
        (response as any).error instanceof Blob ? (response as any).error : undefined;

    let _headers: any = {}; if (response.headers) { for (let key of response.headers.keys()) { _headers[key] = response.headers.get(key); }}
    if (status === 200) {
      return blobToText(responseBlob).pipe(_observableMergeMap((_responseText: string) => {
        let result200: any = null;
        result200 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver) as MedicsForUserListVm;
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
   * Gets the list of MedicAvailabilityForUserListVm
   * @param medicId (optional)
   * @param positionId (optional)
   * @param polyclinicId (optional)
   * @param date (optional)
   * @return Success
   */
  getMedicAvailabilitiesForUserList(medicId: string | undefined, positionId: string | undefined, polyclinicId: string | undefined, date: Date | undefined): Observable<MedicAvailabilityForUserListVm> {
    let url_ = this.baseUrl + "/api/User/GetMedicAvailabilitiesForUserList?";
    if (medicId === null)
      throw new Error("The parameter 'medicId' cannot be null.");
    else if (medicId !== undefined)
      url_ += "MedicId=" + encodeURIComponent("" + medicId) + "&";
    if (positionId === null)
      throw new Error("The parameter 'positionId' cannot be null.");
    else if (positionId !== undefined)
      url_ += "PositionId=" + encodeURIComponent("" + positionId) + "&";
    if (polyclinicId === null)
      throw new Error("The parameter 'polyclinicId' cannot be null.");
    else if (polyclinicId !== undefined)
      url_ += "PolyclinicId=" + encodeURIComponent("" + polyclinicId) + "&";
    if (date === null)
      throw new Error("The parameter 'date' cannot be null.");
    else if (date !== undefined)
      url_ += "Date=" + encodeURIComponent(date ? "" + date.toISOString() : "") + "&";
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
      return this.processGetMedicAvailabilitiesForUserList(response_);
    })).pipe(_observableCatch((response_: any) => {
      if (response_ instanceof HttpResponseBase) {
        try {
          return this.processGetMedicAvailabilitiesForUserList(response_ as any);
        } catch (e) {
          return _observableThrow(e) as any as Observable<MedicAvailabilityForUserListVm>;
        }
      } else
        return _observableThrow(response_) as any as Observable<MedicAvailabilityForUserListVm>;
    }));
  }

  protected processGetMedicAvailabilitiesForUserList(response: HttpResponseBase): Observable<MedicAvailabilityForUserListVm> {
    const status = response.status;
    const responseBlob =
      response instanceof HttpResponse ? response.body :
        (response as any).error instanceof Blob ? (response as any).error : undefined;

    let _headers: any = {}; if (response.headers) { for (let key of response.headers.keys()) { _headers[key] = response.headers.get(key); }}
    if (status === 200) {
      return blobToText(responseBlob).pipe(_observableMergeMap((_responseText: string) => {
        let result200: any = null;
        result200 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver) as MedicAvailabilityForUserListVm;
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
   * Creates the Appointment
   * @param createAppointment CreateAppointmentDto object
   * @return Success
   */
  createAppointment(createAppointment: CreateAppointmentDto): Observable<string> {
    let url_ = this.baseUrl + "/api/User/CreateAppointment";
    url_ = url_.replace(/[?&]$/, "");

    const content_ = JSON.stringify(createAppointment);

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
      return this.processCreateAppointment(response_);
    })).pipe(_observableCatch((response_: any) => {
      if (response_ instanceof HttpResponseBase) {
        try {
          return this.processCreateAppointment(response_ as any);
        } catch (e) {
          return _observableThrow(e) as any as Observable<string>;
        }
      } else
        return _observableThrow(response_) as any as Observable<string>;
    }));
  }

  protected processCreateAppointment(response: HttpResponseBase): Observable<string> {
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
   * Gets the list of Appointments
   * @param userEmail (optional)
   * @return Success
   */
  getAppointments(userEmail: string | null | undefined): Observable<AppointmentsListVm> {
    let url_ = this.baseUrl + "/api/User/AppointmentListForUser?";
    if (userEmail !== undefined && userEmail !== null)
      url_ += "UserEmail=" + encodeURIComponent("" + userEmail) + "&";
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
      return this.processGetAppointment(response_);
    })).pipe(_observableCatch((response_: any) => {
      if (response_ instanceof HttpResponseBase) {
        try {
          return this.processGetAppointment(response_ as any);
        } catch (e) {
          return _observableThrow(e) as any as Observable<AppointmentsListVm>;
        }
      } else
        return _observableThrow(response_) as any as Observable<AppointmentsListVm>;
    }));
  }

  protected processGetAppointment(response: HttpResponseBase): Observable<AppointmentsListVm> {
    const status = response.status;
    const responseBlob =
      response instanceof HttpResponse ? response.body :
        (response as any).error instanceof Blob ? (response as any).error : undefined;

    let _headers: any = {}; if (response.headers) { for (let key of response.headers.keys()) { _headers[key] = response.headers.get(key); }}
    if (status === 200) {
      return blobToText(responseBlob).pipe(_observableMergeMap((_responseText: string) => {
        let result200: any = null;
        result200 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver) as AppointmentsListVm;
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
}
