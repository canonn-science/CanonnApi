import {NgModule} from '@angular/core';
import {Routes, RouterModule} from '@angular/router';
import {environment} from '../environments/environment';
import {ArtifactComponent} from './components/baseData/artifact.component';
import {CodexCategoryComponent} from './components/baseData/codexCategory.component';
import {CodexDataComponent} from './components/baseData/codexData.component';
import {IntroComponent} from './components/intro/intro.component';
import {RuinTypeComponent} from './components/baseData/ruintype.component';
import {ObeliskGroupComponent} from 'app/components/baseData/obeliskGroup.component';
import {RuinlayoutVariantComponent} from './components/baseData/ruinlayoutVariant.component';
import {RuinLayoutComponent} from './components/baseData/ruinLayout.component';

const routes: Routes = [
	{
		path: '',
		component: IntroComponent,
	},
	{
		path: 'basedata',
		children: [{
			path: 'artifacts',
			component: ArtifactComponent,
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
			path: 'ruintypes',
			component: RuinTypeComponent,
		},
		{
			path: 'ruinlayouts',
			component: RuinLayoutComponent,
		},
		{
			path: 'obeliskgroups',
			component: ObeliskGroupComponent,
		},
		{
			path: 'ruinlayoutvariants',
			component: RuinlayoutVariantComponent,
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
