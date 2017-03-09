import {NgModule} from '@angular/core';
import {Routes, RouterModule} from '@angular/router';
import {RelictsComponent} from './components/baseData/relicts.component';
import {CodexCategoryComponent} from './components/baseData/codexCategory.component';
import {CodexDataComponent} from './components/baseData/codexData.component';
import {environment} from '../environments/environment';

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
	imports: [RouterModule.forRoot(routes, { enableTracing: !environment.production })],
	exports: [RouterModule],
	providers: []
})
export class AppRoutingModule {
}
