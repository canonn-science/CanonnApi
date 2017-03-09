import {Component, OnInit} from '@angular/core';
import {CodexCategoryApiService} from '../../services/api/codexCategoryApi.service';
import {RelictModel} from '../../models/relictModel';
import {CodexCategoryModel} from '../../models/codexCategoryModel';
import {BaseDataLookupService} from '../../services/baseDataLookupService';

@Component({
	selector: 'app-codex',
	templateUrl: './codexCategory.component.html',
	styleUrls: ['./codexCategory.component.less']
})
export class CodexCategoryComponent implements OnInit {

	private _relicts: RelictModel[] = null;
	public codexCategories: CodexCategoryModel[] = null;

	constructor(
		public baseData: BaseDataLookupService,
		private _codexCategoryApi: CodexCategoryApiService) {
	}

	ngOnInit() {
		this.loadCodexCategories();
	}

	private loadCodexCategories() {
		this._codexCategoryApi.getAll()
			.subscribe(codexCategories => this.codexCategories = codexCategories);
	}
}
