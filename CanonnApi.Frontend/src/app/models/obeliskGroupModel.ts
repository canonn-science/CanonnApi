export class ObeliskGroupModel {
	public id: number;
	public ruintypeId: number;
	public name: string;
	public count: number;
	public created: string;
	public updated: string;
	public active: boolean;

	constructor(id?: number) {
		this.id = id;
	}
}
