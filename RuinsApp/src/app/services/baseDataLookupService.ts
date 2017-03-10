import {Observable, Subscription} from 'rxjs/Rx';
import 'rxjs/add/observable/interval';
import {RelictModel} from '../models/relictModel';
import {CodexCategoryModel} from '../models/codexCategoryModel';
import {RelictApiService} from './api/relictApi.service';
import {CodexCategoryApiService} from './api/codexCategoryApi.service';
import {Injectable} from '@angular/core';
import {RuinTypeModel} from 'app/models/ruintypeModel';
import {RuinTypeApiService} from './api/ruinTypeApi.service';

@Injectable()
export class BaseDataLookupService {

	private timer: Subscription;
	private request$: Observable<any>;

	public relictData: RelictModel[] = [];
	public relictLookup: {
		[key: number]: RelictModel,
	} = {};

	public codexCategoryData: CodexCategoryModel[] = [];
	public codexCategoryLookup: {
		[key: number]: CodexCategoryModel,
	} = {};

	public ruinTypeData: RuinTypeModel[] = [];
	public ruinTypeLookup: {
		[key: number]: RuinTypeModel,
	} = {};

	constructor(private _relictsApi: RelictApiService,
					private _codexCategoryApi: CodexCategoryApiService,
					private _ruinTypeApi: RuinTypeApiService) {
		const relicts = this._relictsApi.getAll();
		const codexCategories = this._codexCategoryApi.getAll();
		const ruinTypes = this._ruinTypeApi.getAll();

		this.request$ = Observable.forkJoin(relicts, codexCategories, ruinTypes);

		this.timer = Observable
			.interval(2 * 60 * 1000) // repeat every 2 minutes
			.do(() => this.refreshData())
			.subscribe();

		this.refreshData();
	}

	private refreshData() {
		this.request$.subscribe(
			(res) => {
				this.relictData = res[0];
				this.relictLookup = {};
				this.relictData.forEach((item) => this.relictLookup[item.id] = item);

				this.codexCategoryData = res[1];
				this.codexCategoryLookup = {};
				this.codexCategoryData.forEach((item) => this.codexCategoryLookup[item.id] = item);

				this.ruinTypeData = res[2];
				this.ruinTypeLookup = {};
				this.ruinTypeData.forEach((item) => this.ruinTypeLookup[item.id] = item);
			}
		);
	}
}
