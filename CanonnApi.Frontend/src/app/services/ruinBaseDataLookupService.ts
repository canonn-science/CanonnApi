import {Observable, Subscription} from 'rxjs/Rx';
import 'rxjs/add/observable/interval';
import {Injectable} from '@angular/core';
import {RuinTypeModel} from 'app/models/ruintypeModel';
import {RuinTypeApiService} from './api/ruinTypeApi.service';
import {ObeliskGroupModel} from '../models/obeliskGroupModel';
import {ObeliskGroupApiService} from './api/obeliskGroupApi.service';

@Injectable()
export class RuinBaseDataLookupService {

	private timer: Subscription;
	private request$: Observable<any>;

	public ruinTypeData: RuinTypeModel[] = [];
	public ruinTypeLookup: {
		[key: number]: RuinTypeModel,
	} = {};

	public obeliskGroupData: ObeliskGroupModel[] = [];
	public obeliskGroupLookup: {
		[key: number]: ObeliskGroupModel,
	} = {};

	constructor(
		private _ruinTypeApi: RuinTypeApiService,
		private _obeliskGroupApi: ObeliskGroupApiService) {
		const ruinTypes = this._ruinTypeApi.getAll();
		const obeliskGroups = this._obeliskGroupApi.getAll();

		this.request$ = Observable.forkJoin(ruinTypes, obeliskGroups);

		this.timer = Observable
			.interval(2 * 60 * 1000) // repeat every 2 minutes
			.do(() => this.refreshData())
			.subscribe();

		this.refreshData();
	}

	private refreshData() {
		this.request$.subscribe(
			(res) => {
				this.ruinTypeData = res[0];
				this.ruinTypeLookup = {};
				this.ruinTypeData.forEach((item) => this.ruinTypeLookup[item.id] = item);

				this.obeliskGroupData = res[1];
				this.obeliskGroupLookup = {};
				this.obeliskGroupData.forEach((item) => this.obeliskGroupLookup[item.id] = item);
			}
		);
	}
}
