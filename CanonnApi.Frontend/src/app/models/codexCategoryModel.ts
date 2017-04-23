export class CodexCategoryModel {
	public id: number;
	public name: string;
	public artifactId: number;
	public created: string;
	public updated: string;

	constructor(id?: number) {
		this.id = id;
	}
}
