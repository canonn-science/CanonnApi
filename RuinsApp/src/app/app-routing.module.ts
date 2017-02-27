import {NgModule} from '@angular/core';
import {Routes, RouterModule} from '@angular/router';
import {RelictsComponent} from './relicts/relicts.component';
import {CodexCategoryComponent} from './codex/codexCategory.component';
import {CodexDataComponent} from './codex/codexData.component';

const routes: Routes = [
	{
		path: 'basedata',
		children: [{
			path: 'relicts',
			component: RelictsComponent,
		},{
			path: 'codexcategories',
			component: CodexCategoryComponent,
		},{
			path: 'codexdata',
			component: CodexDataComponent,
		}]
	}
];

@NgModule({
	imports: [RouterModule.forRoot(routes)],
	exports: [RouterModule],
	providers: []
})
export class AppRoutingModule {
}
