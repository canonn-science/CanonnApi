export class ObeliskModel {
	public id: number;

	public obeliskgroupId: number;
	public number: number;
	public isBroken: boolean;
	public isVerified: boolean;
	public codexdataId: number;
	public active: boolean;

	public created: string;
	public updated: string;

	public ruintypeId: number; // just for editor

	constructor(id?: number) {
		this.id = id;
	}
}
