import {Observable, Subscription} from 'rxjs/Rx';
import 'rxjs/add/observable/interval';
import {RelictModel} from '../models/relictModel';
import {CodexCategoryModel} from '../models/codexCategoryModel';
import {RelictApiService} from './api/relictApi.service';
import { CodexCategoryApiService } from './api/codexCategoryApi.service';
import { Injectable } from '@angular/core';

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

	constructor(private _relictsApi: RelictApiService, private _codexCategoryApi: CodexCategoryApiService) {
		const relicts = this._relictsApi.getAll();
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
				this.relictData = res[0];
				this.relictLookup = {};
				this.relictData.forEach((relict) => this.relictLookup[relict.id] = relict);

				this.codexCategoryData = res[1];
				this.codexCategoryLookup = {};
				this.codexCategoryData.forEach((codexCategory) => this.codexCategoryLookup[codexCategory.id] = codexCategory);
			}
		);
	}
}
