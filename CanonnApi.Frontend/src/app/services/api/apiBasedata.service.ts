import {AuthHttp} from 'angular2-jwt';
import {Http} from '@angular/http';
import {ApiBaseService} from './apiBase.service';
import {Observable} from 'rxjs/Rx';
import {BaseDataDto} from '../../models/baseDataDto';

export class ApiBasedataService<TDto extends BaseDataDto> extends ApiBaseService {

	public baseUrl: string = null;

	constructor(public controllerName: string, http: Http, authHttp: AuthHttp, apiVersion: string = 'v1') {
		super(http, authHttp);

		this.baseUrl = `${this._apiBaseUrl}/${apiVersion}/${controllerName}/`;
	}

	public getAll(): Observable<TDto[]> {
		return this._http.get(`${this.baseUrl}`)
			.map(res => <TDto[]>res.json());
	}

	public get(id: number): Observable<TDto> {
		return this._http.get(this.baseUrl + id)
			.map(res => <TDto>res.json());
	}

	public saveOrUpdate(dto: TDto): Observable<TDto> {
		if (dto.id === 0) {
			return this.createNew(dto);
		} else {
			return this.saveChanges(dto);
		}
	}

	private createNew(dto: TDto): Observable<TDto> {
		return this._http.post(this.baseUrl, dto)
			.map(res => <TDto>res.json());
	}

	private saveChanges(dto: TDto): Observable<TDto> {
		return this._http.put(this.baseUrl + dto.id, dto)
			.map(res => <TDto>res.json());
	}

	public delete(id: number): Observable<any> {
		return this._authHttp.delete(this.baseUrl + id);
	}
}
