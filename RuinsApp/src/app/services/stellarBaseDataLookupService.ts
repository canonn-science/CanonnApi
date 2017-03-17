import {Observable, Subscription} from 'rxjs/Rx';
import 'rxjs/add/observable/interval';
import {Injectable} from '@angular/core';
import {Logger} from 'angular2-logger/core';
import {SystemModel} from '../models/systemModel';
import {BodyModel} from '../models/bodyModel';
import {SystemApiService} from './api/systemApi.service';
import {BodyApiService} from './api/bodyApi.service';

@Injectable()
export class StellarBaseDataLookupService {

	private timer: Subscription;
	private request$: Observable<any>;

	public systemData: SystemModel[] = [];
	public systemLookup: {
		[key: number]: SystemModel,
	} = {};

	public bodyData: BodyModel[] = [];
	public bodyLookup: {
		[key: number]: BodyModel,
	} = {};

	constructor(private _logger: Logger,
					private _systemApi: SystemApiService,
					private _bodyApi: BodyApiService) {
		const systems = this._systemApi.getAll();
		const bodies = this._bodyApi.getAll();

		this.request$ = Observable.forkJoin(systems, bodies);

		this.timer = Observable
			.interval(2 * 60 * 1000) // repeat every 2 minutes
			.do(() => this.refreshData())
			.subscribe();

		this.refreshData();
	}

	private refreshData() {
		this._logger.log('Refreshing STELLAR base data lookup information...');
		this.request$.subscribe(
			(res) => {
				this.systemData = res[0];
				this.systemLookup = {};
				this.systemData.forEach((item) => this.systemLookup[item.id] = item);

				this.bodyData = res[1];
				this.bodyLookup = {};
				this.bodyData.forEach((item) => this.bodyLookup[item.id] = item);
			}
		);
	}
}
