import {Component, OnInit} from '@angular/core';
import {CodexCategoryModel} from '../models/codexCategory';
import {CodexDataModel} from '../models/codexDataModel';
import {CodexApiService} from '../services/api/codexApi.service';

@Component({
	selector: 'app-codex',
	templateUrl: './codexData.component.html',
	styleUrls: ['./codexData.component.less']
})
export class CodexDataComponent implements OnInit {

	public codexData: CodexDataModel[] = null;
	private _codexCategories: CodexCategoryModel[] = null;

	constructor(private _codexApi: CodexApiService) {
	}

	ngOnInit() {
		this.loadCodexCategories();
		this.loadCodexData();
	}

	private loadCodexCategories() {
		this._codexApi.getCodexCategoryBaseData()
			.subscribe(codexCategories => this._codexCategories = codexCategories);
	}

	private loadCodexData() {
		this._codexApi.getCodexDataBaseData()
			.subscribe(codexData => this.codexData = codexData);
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
