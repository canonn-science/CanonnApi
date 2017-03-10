import {NgModule} from '@angular/core';
import {Routes, RouterModule} from '@angular/router';
import {environment} from '../environments/environment';
import {RelictsComponent} from './components/baseData/relicts.component';
import {CodexCategoryComponent} from './components/baseData/codexCategory.component';
import {CodexDataComponent} from './components/baseData/codexData.component';
import {IntroComponent} from './components/intro/intro.component';
import {RuinTypeComponent} from './components/baseData/ruintype.component';

const routes: Routes = [
	{
		path: '',
		component: IntroComponent,
	},
	{
		path: 'basedata',
		children: [{
			path: 'relicts',
			component: RelictsComponent,
		},
		{
			path: 'codexcategories',
			component: CodexCategoryComponent,
		},
		{
			path: 'codexdata',
			component: CodexDataComponent,
		},
		{
			path: 'ruinstypes',
			component: RuinTypeComponent,
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
