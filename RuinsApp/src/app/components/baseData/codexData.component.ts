import {Component, OnInit} from '@angular/core';
import {CodexCategoryModel} from '../../models/codexCategoryModel';
import {CodexDataModel} from '../../models/codexDataModel';
import {CodexDataApiService} from '../../services/api/codexDataApi.service';
import {BaseDataLookupService} from '../../services/baseDataLookupService';

@Component({
	selector: 'app-codex',
	templateUrl: './codexData.component.html',
	styleUrls: ['./codexData.component.less']
})
export class CodexDataComponent implements OnInit {

	public codexData: CodexDataModel[];

	constructor(
		private _codexDataApi: CodexDataApiService,
		public baseDataLookupService: BaseDataLookupService) {
	}

	ngOnInit() {
		this.loadData();
	}

	private loadData() {
			this._codexDataApi.getAll()
			.subscribe(data => this.codexData = data);
	}
}
