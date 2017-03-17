import {Observable, Subscription} from 'rxjs/Rx';
import 'rxjs/add/observable/interval';
import {Injectable} from '@angular/core';
import {RuinTypeModel} from 'app/models/ruintypeModel';
import {RuinTypeApiService} from './api/ruinTypeApi.service';
import {Logger} from 'angular2-logger/core';

@Injectable()
export class RuinBaseDataLookupService {

	private timer: Subscription;
	private request$: Observable<any>;

	public ruinTypeData: RuinTypeModel[] = [];
	public ruinTypeLookup: {
		[key: number]: RuinTypeModel,
	} = {};

	constructor(private _logger: Logger,
					private _ruinTypeApi: RuinTypeApiService) {
		const ruinTypes = this._ruinTypeApi.getAll();

		this.request$ = Observable.forkJoin(ruinTypes);

		this.timer = Observable
			.interval(2 * 60 * 1000) // repeat every 2 minutes
			.do(() => this.refreshData())
			.subscribe();

		this.refreshData();
	}

	private refreshData() {
		this._logger.log('Refreshing RUIN base data lookup information...');
		this.request$.subscribe(
			(res) => {
				this.ruinTypeData = res[0];
				this.ruinTypeLookup = {};
				this.ruinTypeData.forEach((item) => this.ruinTypeLookup[item.id] = item);
			}
		);
	}
}
