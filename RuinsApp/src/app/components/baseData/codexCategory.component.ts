import {Component} from '@angular/core';
import {CodexCategoryApiService} from '../../services/api/codexCategoryApi.service';
import {CodexCategoryModel} from '../../models/codexCategoryModel';
import { BaseDataLookupService } from '../../services/baseDataLookupService';
import { AuthenticationService } from 'app/services/api/authentication.service';
import {BaseDataComponent} from './baseData.component';

@Component({
	selector: 'app-codex',
	templateUrl: './codexCategory.component.html',
	styleUrls: ['./codexCategory.component.less']
})
export class CodexCategoryComponent extends BaseDataComponent<CodexCategoryModel> {

	constructor(
		api: CodexCategoryApiService,
		auth: AuthenticationService,
		baseData: BaseDataLookupService
	) {
		super(api, auth, baseData);
	}

	public getNewDto() {
		return new CodexCategoryModel(0);
	}

	public delete(entry: CodexCategoryModel) {
		if (entry && window.confirm(`Really delete codex category ${entry.id} - ${entry.name}?`)) {
			this.api.delete(entry.id)
				.do(() => this.loadData())
				.subscribe();
		}
	}
}
