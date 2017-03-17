import {Observable, Subscription} from 'rxjs/Rx';
import 'rxjs/add/observable/interval';
import {ArtifactModel} from '../models/artifactModel';
import {CodexCategoryModel} from '../models/codexCategoryModel';
import {ArtifactApiService} from './api/artifactApi.service';
import {CodexCategoryApiService} from './api/codexCategoryApi.service';
import {Injectable} from '@angular/core';

@Injectable()
export class CodexBaseDataLookupService {

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

	constructor(private _artifactsApi: ArtifactApiService,
					private _codexCategoryApi: CodexCategoryApiService) {
		const relicts = this._artifactsApi.getAll();
		const codexCategories = this._codexCategoryApi.getAll();

		this.request$ = Observable.forkJoin(relicts, codexCategories);

		this.timer = Observable
			.interval(2 * 60 * 1000) // repeat every 2 minutes
			.do(() => this.refreshData())
			.subscribe();

		this.refreshData();
	}

	private refreshData() {
		this.request$.subscribe(
			(res) => {
				this.artifactData = res[0];
				this.artifactLookup = {};
				this.artifactData.forEach((item) => this.artifactLookup[item.id] = item);

				this.codexCategoryData = res[1];
				this.codexCategoryLookup = {};
				this.codexCategoryData.forEach((item) => this.codexCategoryLookup[item.id] = item);
			}
		);
	}
}
