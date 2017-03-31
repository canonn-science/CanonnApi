import {Component, OnInit} from '@angular/core';

declare var $: any;

@Component({
	selector: 'app-root',
	templateUrl: './app.component.html',
	styleUrls: ['./app.component.less'],
	providers: [],
})
export class AppComponent implements OnInit {

	constructor() {
	}

	ngOnInit() {

/*
      const o = $.AdminLTE.options;
		// Activate the layout maker
		$.AdminLTE.layout.activate();

		// Enable sidebar tree view controls
		$.AdminLTE.tree('.sidebar');

		// Enable control sidebar
		if (o.enableControlSidebar) {
			$.AdminLTE.controlSidebar.activate();
		}
*/
	}
}
