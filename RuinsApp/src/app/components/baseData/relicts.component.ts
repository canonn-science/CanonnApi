import {Component, OnInit} from '@angular/core';
import {RelictApiService} from '../../services/api/relictApi.service';
import {RelictModel} from '../../models/relictModel';
import {AuthenticationService} from '../../services/api/authentication.service';

@Component({
	selector: 'app-relicts',
	templateUrl: './relicts.component.html',
	styleUrls: ['./relicts.component.less'],
})
export class RelictsComponent implements OnInit {

	public relicts: RelictModel[];
	public editingRelict: RelictModel;

	constructor(private _relictsApi: RelictApiService, public auth: AuthenticationService) {
	}

	ngOnInit() {
		this.loadRelicts();
	}

	private loadRelicts() {
		this._relictsApi.getAll()
			.subscribe(relicts => this.relicts = relicts);
	}

	public edit(relict: RelictModel) {
		this.editingRelict = Object.assign({}, relict);
	}

	public createNew() {
		this.editingRelict = new RelictModel(0);
	}

	public delete(relict: RelictModel) {
		if (relict && window.confirm(`Really delete relict ${relict.id} - ${relict.name}?`)) {
			this._relictsApi.delete(relict.id)
				.do(() => this.loadRelicts())
				.subscribe();
		}
	}

	public save() {
		const relict = this.editingRelict;
		this.editingRelict = void 0;

		this._relictsApi.saveOrUpdate(relict)
			.do(() => this.loadRelicts())
			.subscribe();
	}

	public discard() {
		this.editingRelict = void 0;
	}
}
