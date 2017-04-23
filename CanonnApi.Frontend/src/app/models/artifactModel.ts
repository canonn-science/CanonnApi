export class ArtifactModel {
	public id: number;
	public name: string;
	public created: string;
	public updated: string;

	constructor(id?: number) {
		this.id = id;
	}
}
