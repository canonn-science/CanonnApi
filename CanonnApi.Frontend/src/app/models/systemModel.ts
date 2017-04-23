export class SystemModel {
	public id: number;
	public name: string;
	public edsmCoordX: number;
	public edsmCoordY: number;
	public edsmCoordZ: number;
	public edsmCoordUpdated: string;
	public edsmExtId: number;
	public eddbExtId: number;
	public created: string;
	public updated: string;

	constructor(id?: number) {
		this.id = id;
	}
}
