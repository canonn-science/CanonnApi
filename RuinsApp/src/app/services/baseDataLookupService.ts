import {Observable, Subscription} from 'rxjs/Rx';
import 'rxjs/add/observable/interval';
import {ArtifactModel} from '../models/artifactModel';
import {CodexCategoryModel} from '../models/codexCategoryModel';
import {ArtifactApiService} from './api/artifactApi.service';
import {CodexCategoryApiService} from './api/codexCategoryApi.service';
import {Injectable} from '@angular/core';
import {RuinTypeModel} from 'app/models/ruintypeModel';
import {RuinTypeApiService} from './api/ruinTypeApi.service';
import {Logger} from 'angular2-logger/core';

@Injectable()
export class BaseDataLookupService {

	private timer: Subscription;
	private request$: Observable<any>;

	public artifactData: ArtifactModel[] = [];
	public artifactLookup: {
		[key: number]: ArtifactModel,
	} = {};

	public codexCategoryData: CodexCategoryModel[] = [];
	public codexCategoryLookup: {
		[key: number]: CodexCategoryModel,
	} = {};

	public ruinTypeData: RuinTypeModel[] = [];
	public ruinTypeLookup: {
		[key: number]: RuinTypeModel,
	} = {};
/*
	public ruinLayoutData: RuinLayoutModel[] = [];
	public ruinLayoutLookup: {
		[key: number]: RuinLayoutModel,
	} = {};
*/
	constructor(private _logger: Logger,
					private _artifactsApi: ArtifactApiService,
					private _codexCategoryApi: CodexCategoryApiService,
					private _ruinTypeApi: RuinTypeApiService) {
		const relicts = this._artifactsApi.getAll();
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
		this._logger.log('Refreshing base data lookup information...');
		this.request$.subscribe(
			(res) => {
				this.artifactData = res[0];
				this.artifactLookup = {};
				this.artifactData.forEach((item) => this.artifactLookup[item.id] = item);

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
