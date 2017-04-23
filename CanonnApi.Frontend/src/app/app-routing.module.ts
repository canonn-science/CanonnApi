import {NgModule} from '@angular/core';
import {Routes, RouterModule} from '@angular/router';
import {environment} from '../environments/environment';
import {ArtifactComponent} from './components/baseData/codex/artifact.component';
import {CodexCategoryComponent} from './components/baseData/codex/codexCategory.component';
import {CodexDataComponent} from './components/baseData/codex/codexData.component';
import {AboutComponent} from './components/intro/about.component';
import {IntroComponent} from './components/intro/intro.component';
import {RuinTypeComponent} from './components/baseData/ruins/ruintype.component';
import {ObeliskGroupComponent} from './components/baseData/ruins/obeliskGroup.component';
import {BodyComponent} from './components/baseData/stellar/body.component';
import {SystemComponent} from './components/baseData/stellar/system.component';
import {ObeliskComponent} from './components/baseData/ruins/obelisk.component';
import {SitesComponent} from './components/baseData/sites/sites.component';

const routes: Routes = [
	{
		path: '',
		component: IntroComponent,
	},
	{
		path: 'about',
		component: AboutComponent,
	},
	{
		path: 'basedata',
		children: [
			{
				path: 'stellar',
				children: [
					{
						path: 'systems',
						component: SystemComponent,
					},
					{
						path: 'bodies',
						component: BodyComponent,
					},
				],
			},
			{
				path: 'codex',
				children: [
					{
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
				],
			},
			{
				path: 'ruins',
				children: [
					{
						path: 'ruintypes',
						component: RuinTypeComponent,
					},
					{
						path: 'obeliskgroups',
						component: ObeliskGroupComponent,
					},
					{
						path: 'obelisks',
						component: ObeliskComponent,
					},
					{
						path: 'sites',
						component: SitesComponent
					}
				],
			}
		],
	},
];

@NgModule({
	imports: [RouterModule.forRoot(routes, { enableTracing: !environment.production })],
	exports: [RouterModule],
	providers: []
})
export class AppRoutingModule {
}
