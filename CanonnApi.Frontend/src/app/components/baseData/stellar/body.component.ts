import {Component} from '@angular/core';
import {AuthenticationService} from '../../../services/api/authentication.service';
import {BaseDataComponent} from '../baseData.component';
import {BodyModel} from '../../../models/bodyModel';
import {BodyApiService} from '../../../services/api/bodyApi.service';
import {StellarBaseDataLookupService} from '../../../services/stellarBaseDataLookupService';
import {Observable} from 'rxjs/Observable';

@Component({
	selector: 'app-stellar-bodies',
	templateUrl: './body.component.html',
	styleUrls: ['./body.component.less'],
})
export class BodyComponent extends BaseDataComponent<BodyModel> {

	private fetchIds$: Observable<any> = void 0;

	protected get bodyApi(): BodyApiService {
		return <BodyApiService>this.api;
	}

	constructor(api: BodyApiService, auth: AuthenticationService, public baseData: StellarBaseDataLookupService) {
		super(api, auth);
	}

	public getNewDto() {
		return new BodyModel(0);
	}

	public delete(entry: BodyModel) {
		if (entry && window.confirm(`Really delete body ${entry.id} - ${this.baseData.systemLookup[entry.systemId].name} - ${entry.name}?`)) {
			this.api.delete(entry.id)
				.do(() => this.loadData())
				.subscribe();
		}
	}

	public get sortedSystemData() {
		return this.baseData.systemData.sort((a, b) => (a.name !== b.name) ? a.name < b.name ? -1 : 1 : 0);
	}

	public fetchEdsmIds() {
		if (!this.fetchIds$) {
			this.fetchIds$ = this.bodyApi.fetchEdsmIds();

			this.fetchIds$.subscribe(
				(res) => {
					this.loadData();
					this.fetchIds$ = void 0;
				}
			);
		}
	}
}
