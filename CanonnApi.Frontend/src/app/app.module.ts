import {BrowserModule} from '@angular/platform-browser';
import {NgModule} from '@angular/core';
import {FormsModule} from '@angular/forms';
import {Http, HttpModule, RequestOptions} from '@angular/http';
import {AppRoutingModule} from './app-routing.module';
import {AuthHttp, AuthConfig} from 'angular2-jwt';
import {environment} from '../environments/environment';
import {AuthenticationService} from './services/api/authentication.service';
import {AlertModule, TypeaheadModule} from 'ng2-bootstrap';
// components
import {AppComponent} from './app.component';
import {LoginComponent} from './components/login/login.component';
import {MainSidebarComponent} from './components/main-sidebar/main-sidebar.component';
import {ArtifactComponent} from './components/baseData/codex/artifact.component';
import {CodexCategoryComponent} from './components/baseData/codex/codexCategory.component';
import {CodexDataComponent} from './components/baseData/codex/codexData.component';
import {AppHeaderComponent} from './components/app-header/app-header.component';
import {IntroComponent} from './components/intro/intro.component';
import {ObeliskGroupComponent} from './components/baseData/ruins/obeliskGroup.component';
import {SystemComponent} from './components/baseData/stellar/system.component';
import {BodyComponent} from './components/baseData/stellar/body.component';
import {RuinTypeComponent} from './components/baseData/ruins/ruintype.component';
import {ObeliskComponent} from './components/baseData/ruins/obelisk.component';
import {SitesComponent} from './components/baseData/sites/sites.component';
// base data lookups
import {CodexBaseDataLookupService} from './services/codexBaseDataLookupService';
import {RuinBaseDataLookupService} from './services/ruinBaseDataLookupService';
import {StellarBaseDataLookupService} from './services/stellarBaseDataLookupService';
// Api services
import {ArtifactApiService} from './services/api/artifactApi.service';
import {CodexDataApiService} from './services/api/codexDataApi.service';
import {CodexCategoryApiService} from './services/api/codexCategoryApi.service';
import {RuinTypeApiService} from './services/api/ruinTypeApi.service';
import {ObeliskGroupApiService} from 'app/services/api/obeliskGroupApi.service';
import {SystemApiService} from './services/api/systemApi.service';
import {BodyApiService} from './services/api/bodyApi.service';
import {ObeliskApiService} from './services/api/obeliskApi.service';


// currently angular2-jwt AUTH_PROVIDERS don't work, so use this workaround:
// https://github.com/auth0/angular2-jwt/issues/258
export function authHttpServiceFactory(http: Http, options: RequestOptions) {
	return new AuthHttp(new AuthConfig({
		tokenName: 'id_token',
	}), http, options);
}

@NgModule({
	declarations: [
		AppComponent,
		AppHeaderComponent,
		MainSidebarComponent,
		LoginComponent,
		IntroComponent,
		// Base data components
		ArtifactComponent,
		CodexCategoryComponent,
		CodexDataComponent,
		ObeliskGroupComponent,
		RuinTypeComponent,
		SystemComponent,
		BodyComponent,
		ObeliskComponent,
		SitesComponent,
	],
	imports: [
		BrowserModule,
		FormsModule,
		HttpModule,
		AppRoutingModule,
		AlertModule.forRoot(),
		TypeaheadModule.forRoot()
	],
	providers: [
		{
			provide: AuthHttp,
			useFactory: authHttpServiceFactory,
			deps: [Http, RequestOptions]
		},
		AuthenticationService,
		// Base data Lookups
		CodexBaseDataLookupService,
		RuinBaseDataLookupService,
		StellarBaseDataLookupService,
		// Apis
		ArtifactApiService,
		CodexCategoryApiService,
		CodexDataApiService,
		ObeliskGroupApiService,
		RuinTypeApiService,
		SystemApiService,
		BodyApiService,
		ObeliskApiService,
	],
	bootstrap: [AppComponent]
})
export class AppModule {
}
