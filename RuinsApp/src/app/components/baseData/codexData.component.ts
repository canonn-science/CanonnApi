import {Component} from '@angular/core';
import {CodexDataModel} from '../../models/codexDataModel';
import {CodexDataApiService} from '../../services/api/codexDataApi.service';
import { BaseDataLookupService } from '../../services/baseDataLookupService';
import { AuthenticationService } from 'app/services/api/authentication.service';
import {BaseDataComponent} from './baseData.component';

@Component({
	selector: 'app-codex',
	templateUrl: './codexData.component.html',
	styleUrls: ['./codexData.component.less']
})
export class CodexDataComponent extends BaseDataComponent<CodexDataModel> {

	constructor(
		api: CodexDataApiService,
		auth: AuthenticationService,
		baseData: BaseDataLookupService) {
		super(api, auth, baseData);
	}

	public getNewDto() {
		return new CodexDataModel(0);
	}

	public delete(data: CodexDataModel) {
		if (data && window.confirm(`Really delete codex entry ${this.baseData.codexCategoryLookup[data.categoryId].name} ${data.entryNumber}?`)) {
			this.api.delete(data.id)
				.do(() => this.loadData())
				.subscribe();
		}
	}
}
