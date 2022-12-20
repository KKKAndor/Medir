import { HttpHeaders } from "@angular/common/http";

export class ClientBase{

  protected transformOptions(options: any){
    const token = localStorage.getItem('token');
    options.headers = new HttpHeaders({
      "Content-Type": "application/json",
      "Accept": "application/json",
      "Authorization": `Bearer ${localStorage.getItem("token")}`
    })
    return Promise.resolve(options);
  };
}
