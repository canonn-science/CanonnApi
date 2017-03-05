import {Component, OnInit} from '@angular/core';
import {CodexCategoryModel} from '../models/codexCategory';
import {CodexDataModel} from '../models/codexDataModel';
import {CodexDataApiService} from '../services/api/codexDataApi.service';
import {CodexCategoryApiService} from '../services/api/codexCategoryApi.service';
import {Observable} from 'rxjs/Rx';

@Component({
	selector: 'app-codex',
	templateUrl: './codexData.component.html',
	styleUrls: ['./codexData.component.less']
})
export class CodexDataComponent implements OnInit {

	public codexData$: Observable<CodexDataModel[]> = null;
	public codexCategories$: Observable<CodexCategoryModel[]> = null;

	constructor(private _codexDataApi: CodexDataApiService, private _codexCategoryApi: CodexCategoryApiService) {
	}

	ngOnInit() {
		this.loadData();
	}

	private loadData() {
		const codexCategories$ = this._codexCategoryApi.getAll().publishReplay().refCount();
		const codexData$ = this._codexDataApi.getAll(true).publishReplay().refCount();

		const queries$ = Observable.concat(codexCategories$, codexData$);

		queries$.subscribe(
				() => { }, // next
				() => { }, // error
				() => { // success
					this.codexCategories$ = codexCategories$;
					this.codexData$ = codexData$;
				}
			);
	}
/*
	public getCategoryName(id: number) {
		if (this.codexCategories$ !== null) {
			for (let i = 0; i < this.codexCategories$.length; i++) {
				if (this._codexCategories[i].id === id) {
					return this._codexCategories[i].name;
				}
			}
		}

		return '[not available]';
	}
*/
}
