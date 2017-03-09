import {Component, OnInit} from '@angular/core';
import {CodexCategoryApiService} from '../../services/api/codexCategoryApi.service';
import {RelictModel} from '../../models/relictModel';
import {CodexCategoryModel} from '../../models/codexCategoryModel';
import { BaseDataLookupService } from '../../services/baseDataLookupService';
import { AuthenticationService } from 'app/services/api/authentication.service';

@Component({
	selector: 'app-codex',
	templateUrl: './codexCategory.component.html',
	styleUrls: ['./codexCategory.component.less']
})
export class CodexCategoryComponent implements OnInit {

	private _relicts: RelictModel[] = null;
	public codexCategories: CodexCategoryModel[] = null;
	public editingData: CodexCategoryModel;

	constructor(
		public baseData: BaseDataLookupService,
		public auth: AuthenticationService,
		private _codexCategoryApi: CodexCategoryApiService) {
	}

	ngOnInit() {
		this.loadCodexCategories();
	}

	private loadCodexCategories() {
		this._codexCategoryApi.getAll()
			.subscribe(codexCategories => this.codexCategories = codexCategories);
	}

		public edit(category: CodexCategoryModel) {
		this.editingData = Object.assign({}, category);
	}

	public createNew() {
		this.editingData = new CodexCategoryModel(0);
	}

	public delete(category: CodexCategoryModel) {
		if (category && window.confirm(`Really delete category ${category.id} - ${category.name}?`)) {
			this._codexCategoryApi.delete(category.id)
				.do(() => this.loadCodexCategories())
				.subscribe();
		}
	}

	public save() {
		const category = this.editingData;
		this.editingData = void 0;

		this._codexCategoryApi.saveOrUpdate(category)
			.do(() => this.loadCodexCategories())
			.subscribe();
	}

	public discard() {
		this.editingData = void 0;
	}
}
