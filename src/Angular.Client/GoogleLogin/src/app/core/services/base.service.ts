import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root'
})

export class BaseService {
  protected baseUrl = environment.baseUrl;
  constructor(protected httpClient: HttpClient) {
  }
}

