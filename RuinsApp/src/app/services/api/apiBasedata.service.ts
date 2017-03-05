import {AuthHttp} from 'angular2-jwt';
import {Http} from '@angular/http';
import {Logger} from 'angular2-logger/app/core/logger';
import {ApiBaseService} from './apiBase.service';
import {Observable} from 'rxjs/Rx';
import {IDto} from '../../models/IDto';

export class ApiBasedataService<TDto extends IDto> extends ApiBaseService {

	public baseUrl: string = null;

	constructor(public controllerName: string, logger: Logger, http: Http, authHttp: AuthHttp, apiVersion: string = 'v1') {
		super(logger, http, authHttp);

		this.baseUrl = `${this._apiBaseUrl}/${apiVersion}/${controllerName}/`;
	}

	public getAll(withName: boolean = false): Observable<TDto[]> {
		return this._http.get(`${this.baseUrl}?withCategoryName=${withName}`)
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
		return this._authHttp.post(this.baseUrl, dto)
			.map(res => <TDto>res.json());
	}

	private saveChanges(dto: TDto): Observable<TDto> {
		return this._authHttp.put(this.baseUrl + dto.id, dto)
			.map(res => <TDto>res.json());
	}

	public delete(id: number): Observable<any> {
		return this._authHttp.delete(this.baseUrl + id);
	}
}
