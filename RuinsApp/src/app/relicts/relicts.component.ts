import {Component, OnInit} from '@angular/core';
import {RelictsApiService} from '../services/api/relictsApi.service';
import {RelictsModel} from '../models/relictsModel';
import { LinqService } from 'ng2-linq';
import {AuthenticationService} from '../services/api/authentication.service';
import {Observable} from 'rxjs';

@Component({
	selector: 'app-relicts',
	templateUrl: './relicts.component.html',
	styleUrls: ['./relicts.component.less'],
})
export class RelictsComponent implements OnInit {

	public relicts: RelictsModel[] = null;
	public editingRelict: RelictsModel = null;

	constructor(private _relictsApi: RelictsApiService, public auth: AuthenticationService, private linq: LinqService) {
	}

	ngOnInit() {
		this.loadRelicts();
	}

	private loadRelicts() {
		this._relictsApi.getRelicsBaseData()
			.subscribe(relicts => this.relicts = relicts);
	}

	public edit(id: number) {
		Observable.from(this.relicts)
			.filter(r => r.id === id)
			.subscribe(r => this.editingRelict = JSON.parse(JSON.stringify(r)));
	}

	public createNew() {
		this.editingRelict = new RelictsModel();
		this.editingRelict.id = 0;
	}

	public delete(id: number) {
		let relict;

		for (let i = 0; i < this.relicts.length; i ++) {
			if (this.relicts[i].id === id) {
				relict = this.relicts[i];
				break;
			}
		}

		if ((relict !== null) && window.confirm(`Really delete relict ${relict.id} - ${relict.name}?`)) {
			this._relictsApi.delete(relict.id)
				.do(() => this.loadRelicts())
				.subscribe();
		}
	}

	public save() {
		const relict = this.editingRelict;
		this.editingRelict = null;

		this._relictsApi.saveOrUpdate(relict)
			.do(() => this.loadRelicts())
			.subscribe();
	}

	public discard() {
		this.editingRelict = null;
	}
}
