import {Component, OnInit} from '@angular/core';
import {CodexCategoryModel} from '../../models/codexCategoryModel';
import {CodexDataModel} from '../../models/codexDataModel';
import {CodexDataApiService} from '../../services/api/codexDataApi.service';
import { BaseDataLookupService } from '../../services/baseDataLookupService';
import { AuthenticationService } from 'app/services/api/authentication.service';

@Component({
	selector: 'app-codex',
	templateUrl: './codexData.component.html',
	styleUrls: ['./codexData.component.less']
})
export class CodexDataComponent implements OnInit {

	public codexData: CodexDataModel[];
	public editing: CodexDataModel;

	constructor(
		private _codexDataApi: CodexDataApiService,
		public auth: AuthenticationService,
		public baseData: BaseDataLookupService) {
	}

	ngOnInit() {
		this.loadData();
	}

	private loadData() {
		this._codexDataApi.getAll()
			.subscribe(data => this.codexData = data);
	}

		public edit(data: CodexDataModel) {
		this.editing = Object.assign({}, data);
	}

	public createNew() {
		this.editing = new CodexDataModel(0);
	}

	public delete(data: CodexDataModel) {
		if (data && window.confirm(`Really delete codex entry ${this.baseData.codexCategoryLookup[data.categoryId].name} ${data.entryNumber}?`)) {
			this._codexDataApi.delete(data.id)
				.do(() => this.loadData())
				.subscribe();
		}
	}

	public save() {
		const data = this.editing;
		this.editing = void 0;

		this._codexDataApi.saveOrUpdate(data)
			.do(() => this.loadData())
			.subscribe();
	}

	public discard() {
		this.editing = void 0;
	}
}
