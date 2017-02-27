import {Component, OnInit} from '@angular/core';
import {RelictsApiService} from '../services/api/relictsApi.service';
import {RelictsModel} from '../models/relictsModel';

@Component({
	selector: 'app-relicts',
	templateUrl: './relicts.component.html',
	styleUrls: ['./relicts.component.less']
})
export class RelictsComponent implements OnInit {

	public relicts: RelictsModel[] = null;

	constructor(private _relictsApi: RelictsApiService) {
	}

	ngOnInit() {
		this.loadRelicts();
	}

	private loadRelicts() {
		this._relictsApi.getRelicsBaseData()
			.subscribe(relicts => this.relicts = relicts);
	}
}
