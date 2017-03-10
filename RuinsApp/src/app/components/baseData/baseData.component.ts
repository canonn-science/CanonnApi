import {OnInit} from '@angular/core';
import {ApiBasedataService} from '../../services/api/apiBasedata.service';
import {BaseDataDto} from 'app/models/baseDataDto';
import {AuthenticationService} from 'app/services/api/authentication.service';
import {BaseDataLookupService} from 'app/services/baseDataLookupService';

export abstract class BaseDataComponent<T extends BaseDataDto> implements OnInit {

	public data: T[];
	public editingData: T;

	constructor(
		protected api: ApiBasedataService<T>,
		public auth: AuthenticationService,
		public baseData: BaseDataLookupService
	) { }

	ngOnInit() {
		this.loadData();
	}

	abstract getNewDto(): T;

	protected loadData() {
		this.api.getAll().subscribe(data => this.data = data);
	}

	public edit(data: T) {
		this.editingData = Object.assign({}, data);
	}

	public createNew() {
		this.editingData = this.getNewDto();
	}

	public save() {
		const data = this.editingData;
		this.editingData = void 0;

		this.api.saveOrUpdate(data)
			.do(() => this.loadData())
			.subscribe();
	}

	public discard() {
		this.editingData = void 0;
	}

	public format(data): string {
		return JSON.stringify(data, null, '\t');
	}
}
