export class RuinTypeModel {
	public id: number;
	public name: string;
	public created: string;
	public updated: string;
/*
	public obeliskGroup: ObeliskGroupModel[];
	public ruinLayouts: RuinlayoutModel[];
*/
	constructor(id?: number) {
		this.id = id;
	}
}
