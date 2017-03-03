import {Component, OnInit} from '@angular/core';
import {CodexCategoryApiService} from '../services/api/codexCategoryApi.service';
import {RelictsApiService} from '../services/api/relictsApi.service';
import {RelictsModel} from '../models/relictsModel';
import {CodexCategoryModel} from '../models/codexCategory';

@Component({
	selector: 'app-codex',
	templateUrl: './codexCategory.component.html',
	styleUrls: ['./codexCategory.component.less']
})
export class CodexCategoryComponent implements OnInit {

	private _relicts: RelictsModel[] = null;
	public codexCategories: CodexCategoryModel[] = null;

	constructor(private _codexCategoryApi: CodexCategoryApiService, private _relictsApi: RelictsApiService) {
	}

	ngOnInit() {
		this.loadRelicts();
		this.loadCodexCategories();
	}

	private loadRelicts() {
		this._relictsApi.getAll()
			.subscribe(relicts => this._relicts = relicts);
	}

	private loadCodexCategories() {
		this._codexCategoryApi.getAll()
			.subscribe(codexCategories => this.codexCategories = codexCategories);
	}

	public getRelictName(id: number) {
		if (this._relicts !== null) {
			for (let i = 0; i < this._relicts.length; i++) {
				if (this._relicts[i].id === id) {
					return this._relicts[i].name;
				}
			}
		}

		return '[not available]';
	}
}
