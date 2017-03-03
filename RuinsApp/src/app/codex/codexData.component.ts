import {Component, OnInit} from '@angular/core';
import {CodexCategoryModel} from '../models/codexCategory';
import {CodexDataModel} from '../models/codexDataModel';
import {CodexDataApiService} from '../services/api/codexDataApi.service';
import {CodexCategoryApiService} from '../services/api/codexCategoryApi.service';

@Component({
	selector: 'app-codex',
	templateUrl: './codexData.component.html',
	styleUrls: ['./codexData.component.less']
})
export class CodexDataComponent implements OnInit {

	public codexData: CodexDataModel[] = null;
	private _codexCategories: CodexCategoryModel[] = null;

	constructor(private _codexDataApi: CodexDataApiService, private _codexCategoryApi: CodexCategoryApiService) {
	}

	ngOnInit() {
		this.loadCodexCategories();
		this.loadCodexData();
	}

	private loadCodexData() {
		this._codexDataApi.getAll()
			.subscribe(codexData => this.codexData = codexData);
	}

	private loadCodexCategories() {
		this._codexCategoryApi.getAll()
			.subscribe(codexCategories => this._codexCategories = codexCategories);
	}

	public getCategoryName(id: number) {
		if (this._codexCategories !== null) {
			for (let i = 0; i < this._codexCategories.length; i++) {
				if (this._codexCategories[i].id === id) {
					return this._codexCategories[i].name;
				}
			}
		}

		return '[not available]';
	}
}
