import {Component} from '@angular/core';
import {ArtifactApiService} from '../../../services/api/artifactApi.service';
import {ArtifactModel} from '../../../models/artifactModel';
import {AuthenticationService} from '../../../services/api/authentication.service';
import {BaseDataComponent} from '../baseData.component';

@Component({
	selector: 'app-artifacts',
	templateUrl: './artifact.component.html',
	styleUrls: ['./artifact.component.less'],
})
export class ArtifactComponent extends BaseDataComponent<ArtifactModel> {

	constructor(api: ArtifactApiService, auth: AuthenticationService) {
		super(api, auth);
	}

	public getNewDto() {
		return new ArtifactModel(0);
	}

	public delete(entry: ArtifactModel) {
		if (entry && window.confirm(`Really delete artifact ${entry.id} - ${entry.name}?`)) {
			this.api.delete(entry.id)
				.do(() => this.loadData())
				.subscribe();
		}
	}
}
