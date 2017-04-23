export class CodexDataModel {
	public id: number;
	public categoryId: number;
	public entryNumber: number;
	public artifactId: number;
	public text: string;
	public created: string;
	public updated: string;

	constructor(id?: number) {
		this.id = id;
	}
}
